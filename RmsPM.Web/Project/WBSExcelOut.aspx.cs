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
	/// WBSExcelOut ��ժҪ˵����
	/// </summary>
	public partial class WBSExcelOut : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
		}

		#region Web ������������ɵĴ���
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: �õ����� ASP.NET Web ���������������ġ�
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
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
					Response.Write(Rms.Web.JavaScript.Alert(true,"Excel���Ʋ���Ϊ�� ��"));
					return;
				}
				*/

				if (this.txtCode.Text.Trim().Length<0)
				{
					Response.Write(Rms.Web.JavaScript.Alert(true,"�Զ����Ų���Ϊ�� ��"));
					return;
				}

				string projectCode = Request["ProjectCode"] + "";

				EntityData wbs = DAL.EntityDAO.WBSDAO.GetTaskByProject(projectCode);
				string vPath = Server.MapPath(ConfigurationSettings.AppSettings["VirtualDirectory"]);
				string path = vPath +  @"\temp\";
				string fileName = "�������" + DateTime.Now.ToString("yyyyMMddhhmmss")  + ".csv";

				StreamWriter w = new StreamWriter( path + fileName, false,	System.Text.Encoding.Default);
				w.WriteLine("�Զ�����,����������,������,����״̬,��Ҫ�̶�,��������,�ƻ���ʼʱ��,�ƻ�����ʱ��,ʵ�ʿ�ʼʱ��,ʵ�ʽ���ʱ��,�������"  );
				if(wbs.CurrentTable.Rows.Count>0)
				{
					DataRow[] dr = wbs.CurrentTable.Select("SortID='"+this.txtCode.Text.Trim()+"'");
					if(dr.Length==1)
					{
						string sTemp = "";
						sTemp += dr[0]["SortID"].ToString() + "," ;
						sTemp += dr[0]["TaskName"].ToString().Replace(",","��") + "," ;
						if(((ArrayList)GetMasterByWBSCode(dr[0]["WBSCode"].ToString())).Count==2)
							sTemp += (GetMasterByWBSCode(dr[0]["WBSCode"].ToString()))[1]+"," ;// ��ǰ���������
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
						// ���뱸ע��Ϣ
//						if(((ArrayList)GetMasterByWBSCode(dr[0]["WBSCode"].ToString())).Count==2)
//							sTemp += (GetMasterByWBSCode(dr[0]["WBSCode"].ToString()))[0]+";";
//						else
//							sTemp += ";";
//						sTemp += dr[0]["Status"].ToString()+";";
//						sTemp += dr[0]["ImportantLevel"].ToString()+";";
//						sTemp += ";";// �趨Ҫ�����ĸ��ڵ���Ϊ��
//						sTemp += dr[0]["WBSCode"].ToString();
						string outLine = "1";
						sTemp += outLine;
						w.WriteLine(sTemp);

						WBSWrite(ref w,dr[0]["WBSCode"].ToString(),wbs.CurrentTable,outLine);
					}
					else
					{
						Response.Write(Rms.Web.JavaScript.Alert(true,"�˽ڵ����ظ��ڵ� ��"));
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
				Response.Write(JavaScript.Alert(true, "��������" + ex.Message));
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
				sTemp += dr1["TaskName"].ToString().Replace(",","��") + "," ;
				if(((ArrayList)GetMasterByWBSCode(dr1["WBSCode"].ToString())).Count==2)
					sTemp += (GetMasterByWBSCode(dr1["WBSCode"].ToString()))[1]+"," ;// ��ǰ���������
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
//				// ���뱸ע��Ϣ
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
					if (dtUserNew.Rows[i]["RoleType"].ToString()=="0") // ����Ϊ��
					{
						if(dtUserNew.Rows[i]["Type"].ToString() == "2") // ����
						{
							// ������ֻ��һ��
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
