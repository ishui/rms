using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Rms.Web;
using Rms.ORMap;
using RmsPM.DAL.EntityDAO;
using RmsPM.BLL;
using System.IO;
using System.Configuration;

namespace RmsPM.Web.Project
{
	/// <summary>
	/// WBSExcelOut 的摘要说明。
	/// </summary>
	public partial class WBSExcelOut : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
		}

		#region Web 窗体设计器生成的代码
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion

		protected void btnSave_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				/*
				if (this.txtExcelName.Text.Trim().Length<0)
				{
					Response.Write(Rms.Web.JavaScript.Alert(true,"Excel名称不能为空 ！"));
					return;
				}
				*/

				if (this.txtCode.Text.Trim().Length<0)
				{
					Response.Write(Rms.Web.JavaScript.Alert(true,"自定义编号不能为空 ！"));
					return;
				}

				string projectCode = Request["ProjectCode"] + "";

				EntityData wbs = DAL.EntityDAO.WBSDAO.GetTaskByProject(projectCode);
				string vPath = Server.MapPath(ConfigurationSettings.AppSettings["VirtualDirectory"]);
				string path = vPath +  @"\temp\";
				string fileName = "工作项导出" + DateTime.Now.ToString("yyyyMMddhhmmss")  + ".csv";

				StreamWriter w = new StreamWriter( path + fileName, false,	System.Text.Encoding.Default);
				w.WriteLine("自定义编号,工作项名称,负责人,工作状态,重要程度,工作进度,计划开始时间,计划结束时间,实际开始时间,实际结束时间,大纲数字"  );
				if(wbs.CurrentTable.Rows.Count>0)
				{
					DataRow[] dr = wbs.CurrentTable.Select("SortID='"+this.txtCode.Text.Trim()+"'");
					if(dr.Length==1)
					{
						string sTemp = "";
						sTemp += dr[0]["SortID"].ToString() + "," ;
						sTemp += dr[0]["TaskName"].ToString().Replace(",","，") + "," ;
						if(((ArrayList)GetMasterByWBSCode(dr[0]["WBSCode"].ToString())).Count==2)
							sTemp += (GetMasterByWBSCode(dr[0]["WBSCode"].ToString()))[1]+"," ;// 当前工作项负责人
						else
							sTemp += ",";
						sTemp += BLL.ComSource.GetTaskStatusName(dr[0]["Status"].ToString()) + "," ;
						sTemp += BLL.ComSource.GetImportantName(dr[0]["ImportantLevel"].ToString()) + "," ;
						sTemp += dr[0]["CompletePercent"].ToString() + "%," ;
						string PlannedStartDate = dr[0]["PlannedStartDate"].ToString();
						sTemp += PlannedStartDate.Substring(0,(PlannedStartDate.IndexOf(" ")<0)?0:PlannedStartDate.IndexOf(" ")) + "," ;
						string PlannedFinishDate = dr[0]["PlannedFinishDate"].ToString();
						sTemp += PlannedFinishDate.Substring(0,(PlannedFinishDate.IndexOf(" ")<0)?0:PlannedFinishDate.IndexOf(" ")) + "," ;
						string ActualStartDate = dr[0]["ActualStartDate"].ToString();
						sTemp += ActualStartDate.Substring(0, (ActualStartDate.IndexOf(" ")<0)?0:ActualStartDate.IndexOf(" ")) + "," ;
						string ActualFinishDate = dr[0]["ActualFinishDate"].ToString();
						sTemp += ActualFinishDate.Substring(0, (ActualFinishDate.IndexOf(" ")<0)?0:ActualFinishDate.IndexOf(" "))+",";
						// 导入备注信息
//						if(((ArrayList)GetMasterByWBSCode(dr[0]["WBSCode"].ToString())).Count==2)
//							sTemp += (GetMasterByWBSCode(dr[0]["WBSCode"].ToString()))[0]+";";
//						else
//							sTemp += ";";
//						sTemp += dr[0]["Status"].ToString()+";";
//						sTemp += dr[0]["ImportantLevel"].ToString()+";";
//						sTemp += ";";// 设定要导出的父节点编号为空
//						sTemp += dr[0]["WBSCode"].ToString();
						string outLine = "1";
						sTemp += outLine;
						w.WriteLine(sTemp);

						WBSWrite(ref w,dr[0]["WBSCode"].ToString(),wbs.CurrentTable,outLine);
					}
					else
					{
						Response.Write(Rms.Web.JavaScript.Alert(true,"此节点有重复节点 ！"));
						return;
					}
				}				
				w.Flush(); 
				w.Close();
				wbs.Dispose();
                Response.Redirect("../Temp/" + fileName);
                //Response.Write(Rms.Web.JavaScript.ScriptStart);
				//Response.Write("window.open('../Temp/"+ fileName  + @"');");
				//Response.Write(Rms.Web.JavaScript.WinClose(false));
				//Response.Write(Rms.Web.JavaScript.ScriptEnd);
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog (this.ToString(),ex,"");
				Response.Write(JavaScript.Alert(true, "导出出错：" + ex.Message));
				return;
			}
		}

		private void WBSWrite(ref StreamWriter w,string parentWBSCode,DataTable wbsTable,string outLine)
		{
			DataRow[] dr = wbsTable.Select("ParentCode='"+parentWBSCode+"'");
			int i=1;
			foreach(DataRow dr1 in dr)
			{
				string sTemp = "";
				sTemp += dr1["SortID"].ToString() + "," ;
				sTemp += dr1["TaskName"].ToString().Replace(",","，") + "," ;
				if(((ArrayList)GetMasterByWBSCode(dr1["WBSCode"].ToString())).Count==2)
					sTemp += (GetMasterByWBSCode(dr1["WBSCode"].ToString()))[1]+"," ;// 当前工作项负责人
				else
					sTemp += ",";
				sTemp += BLL.ComSource.GetTaskStatusName(dr1["Status"].ToString()) + "," ;
				sTemp += BLL.ComSource.GetImportantName(dr1["ImportantLevel"].ToString()) + "," ;
				sTemp += dr1["CompletePercent"].ToString() + "%," ;
				string PlannedStartDate = dr1["PlannedStartDate"].ToString();
				sTemp += PlannedStartDate.Substring(0,(PlannedStartDate.IndexOf(" ")<0)?0:PlannedStartDate.IndexOf(" ")) + "," ;
				string PlannedFinishDate = dr1["PlannedFinishDate"].ToString();
				sTemp += PlannedFinishDate.Substring(0,(PlannedFinishDate.IndexOf(" ")<0)?0:PlannedFinishDate.IndexOf(" ")) + "," ;
				string ActualStartDate = dr1["ActualStartDate"].ToString();
				sTemp += ActualStartDate.Substring(0, (ActualStartDate.IndexOf(" ")<0)?0:ActualStartDate.IndexOf(" ")) + "," ;
				string ActualFinishDate = dr1["ActualFinishDate"].ToString();
				sTemp += ActualFinishDate.Substring(0, (ActualFinishDate.IndexOf(" ")<0)?0:ActualFinishDate.IndexOf(" "))+",";
//				// 导入备注信息
//				if(((ArrayList)GetMasterByWBSCode(dr1["WBSCode"].ToString())).Count==2)
//					sTemp += (GetMasterByWBSCode(dr1["WBSCode"].ToString()))[0]+";";
//				else
//					sTemp += ";";
//				sTemp += dr1["Status"].ToString()+";";
//				sTemp += dr1["ImportantLevel"].ToString()+";";
//				sTemp += dr1["ParentCode"].ToString()+";";
//				sTemp += dr1["WBSCode"].ToString();
				string newOutLine = outLine + "."+i.ToString();
				sTemp += newOutLine;
				w.WriteLine(sTemp);

				WBSWrite(ref w,dr1["WBSCode"].ToString(),wbsTable,newOutLine);
				i++;
			}
		}

		private ArrayList GetMasterByWBSCode(string wbsCode)
		{
			ArrayList alUser = new ArrayList() ;
			
			EntityData entityUser = WBSDAO.GetTaskPersonByWBSCode(wbsCode);
			if (entityUser.HasRecord())
			{
				DataTable dtUserNew = entityUser.CurrentTable;						
				for (int i = 0; i < dtUserNew.Rows.Count; i++)
				{
					if (dtUserNew.Rows[i]["RoleType"].ToString()=="0") // 类型为人
					{
						if(dtUserNew.Rows[i]["Type"].ToString() == "2") // 负责
						{
							// 负责人只有一个
							alUser.Add(dtUserNew.Rows[i]["UserCode"].ToString());
							alUser.Add(BLL.SystemRule.GetUserName(dtUserNew.Rows[i]["UserCode"].ToString()));
							return alUser;
						}					
					}
				}
			}
			entityUser.Dispose();
			return alUser;
		}
	}
}
