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
using System.IO;
using Rms.Web;
using Rms.ORMap;
using RmsPM.DAL.EntityDAO;
using RmsPM.BLL;

namespace RmsPM.Web.Project
{
	/// <summary>
	/// WBSExcelIn 的摘要说明。
	/// </summary>
	public partial class WBSExcelIn : PageBase//System.Web.UI.Page
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
				string projectCode=Request["ProjectCode"] + "" ; 
				if (this.txtFile.PostedFile.FileName == "")
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "请选择文件"));
					return;
				}
				if (this.txtCode.Text.Trim()== "")
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "请输入要导入到指定任务下的自定义编码"));
					return;
				}

				
				string strWBSRoot = "";
				string strFullCode = "";
				EntityData wbs = DAL.EntityDAO.WBSDAO.GetTaskByProject(projectCode);
				DataTable odt = wbs.CurrentTable;
				for(int i=0;i<odt.Rows.Count;i++)
				{
					if(odt.Rows[i]["SortID"].ToString()==this.txtCode.Text.Trim())
					{
						strWBSRoot =odt.Rows[i]["WBSCode"].ToString();
						strFullCode =odt.Rows[i]["FullCode"].ToString();
					}
				}

				StreamReader m_sr = new StreamReader(this.txtFile.PostedFile.InputStream, System.Text.Encoding.Default);
				//第1行是标题,跳过
				if (m_sr.Peek() >= 0) 
				{
					string s = m_sr.ReadLine();
					string[] ar = s.Split(',');
					if(ar.Length!=11)
					{
						Response.Write(Rms.Web.JavaScript.Alert(true, "要导入的文件格式不对"));
						return;
					}
					if(ar[0]!="自定义编号")
					{
						Response.Write(Rms.Web.JavaScript.Alert(true, "要导入的文件格式不对"));
						return;
					}

				}

				// 根节点
				DataTable dt = InitTable(m_sr);				

				DataRow[] arDRRoot = dt.Select(" outLine not like '%.%'");
				for(int j=0;j<arDRRoot.Length;j++)
				{
					DataRow dr = wbs.GetNewRecord();
					string strWBSCode =			DAL.EntityDAO.SystemManageDAO.GetNewSysCode("WBS");
					dr["WBSCode"] =				strWBSCode;
					dr["TaskName"] =			arDRRoot[j]["TaskName"].ToString();
					dr["ProjectCode"] =			projectCode;
					dr["ParentCode"] =			strWBSRoot;
					dr["Deep"] =				strFullCode.Split('-').Length;
					dr["SortID"] =				this.GetSortID(strWBSRoot);//arDRRoot[j]["SortID"].ToString();
					dr["FullCode"] =			strFullCode+"-"+strWBSCode;
					dr["Flag"] =				"0";
					
					if(arDRRoot[j]["PlannedStartDate"].ToString().Length<1)
						dr["PlannedStartDate"] =	System.DBNull.Value;
					else
						dr["PlannedStartDate"] =	arDRRoot[j]["PlannedStartDate"].ToString();
					
					if(arDRRoot[j]["PlannedFinishDate"].ToString().Length<1)
						dr["PlannedFinishDate"] =	System.DBNull.Value;
					else
						dr["PlannedFinishDate"] =	arDRRoot[j]["PlannedFinishDate"].ToString();
					
					if(arDRRoot[j]["ActualStartDate"].ToString().Length<1)
						dr["ActualStartDate"] =		System.DBNull.Value;
					else
						dr["ActualStartDate"] =		arDRRoot[j]["ActualStartDate"].ToString();
					
					if(arDRRoot[j]["ActualFinishDate"].ToString().Length<1)
						dr["ActualFinishDate"] =	System.DBNull.Value;
					else
                        dr["ActualFinishDate"] =	arDRRoot[j]["ActualFinishDate"].ToString();
					
					//dr["Status"] =				0;//0
                    dr["Status"] = BLL.WBSRule.GetTaskStatusNumberByCompletePercent(BLL.ConvertRule.ToString(arDRRoot[j]["CompletePercent"]).Replace("%", ""));
					dr["ImportantLevel"] =		0;

					dr["CompletePercent"] =	arDRRoot[j]["CompletePercent"];

					wbs.CurrentTable.Rows.Add(dr);
					DAL.EntityDAO.WBSDAO.InsertTask(wbs);

					//this.AddRule(strWBSCode);// 只需要设定头节点权限，子节点继承
					
					this.AddChildTask(wbs,dt,projectCode,arDRRoot[j]["outLine"].ToString(),strFullCode+"-"+strWBSCode,strWBSCode);
				}
				wbs.Dispose();

				if(arDRRoot.Length<1)
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "没有可导入的数据！"));
					return;
				}
								
				Response.Write(JavaScript.ScriptStart);
				if(arDRRoot.Length>0)
					Response.Write(JavaScript.Alert(false,"导入成功 ！"));
				else
					Response.Write(JavaScript.Alert(false,"没有数据导入 ！"));
				Response.Write(JavaScript.OpenerReload(false));
				Response.Write("window.close();");
				Response.Write(JavaScript.ScriptEnd);
			
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog (this.ToString(),ex,"");
				Response.Write(JavaScript.Alert(true, "导入出错：" + ex.Message));
				return;
			}
		}

		/*private void AddChildTask(EntityData wbs,DataTable dt,string projectCode,string pOutline,string strpFullCode,string strNewWBSCode)
		{
			//DataRow[] arDR = dt.Select("outLine like '"+pOutLine+".[^.]%'");
			for(int i=0;i<dt.Rows.Count;i++)
			{
				string strLine = dt.Rows[i]["outLine"].ToString(); // 当前节点
				if(strLine.Length<pOutline.Length) continue;
				int ipre = strLine.LastIndexOf('.'); if(ipre<1) continue;
                DataRow[] parentrow = dt.Select(string.Format("outLine='{0}'", pOutline));
                DataRow parentdr = parentrow[0];
                bool isparentdatachange = false;
				if(strLine.Substring(0,ipre)==pOutline)
				{
					DataRow dr = wbs.GetNewRecord();

					string strWBSCode =			DAL.EntityDAO.SystemManageDAO.GetNewSysCode("WBS");
					dr["WBSCode"] =				strWBSCode;
					dr["TaskName"] =			dt.Rows[i]["TaskName"].ToString();
					dr["ProjectCode"] =			projectCode; 
					dr["ParentCode"] =			strNewWBSCode;//pWBSCode为老的ParentCode
					dr["Deep"] =				strpFullCode.Split('-').Length;
					dr["SortID"] =				this.GetSortID(strNewWBSCode);//arDR[i]["SortID"].ToString();								
					dr["FullCode"] =			strpFullCode+"-"+strWBSCode;
					dr["Flag"] =				"0";
					
					if(dt.Rows[i]["PlannedStartDate"].ToString().Length<1)
						dr["PlannedStartDate"] =	System.DBNull.Value;
					else
						dr["PlannedStartDate"] =	dt.Rows[i]["PlannedStartDate"].ToString();
					
					if(dt.Rows[i]["PlannedFinishDate"].ToString().Length<1)
						dr["PlannedFinishDate"] =	System.DBNull.Value;
					else
						dr["PlannedFinishDate"] =	dt.Rows[i]["PlannedFinishDate"].ToString();
					
					if(dt.Rows[i]["ActualStartDate"].ToString().Length<1)
						dr["ActualStartDate"] =		System.DBNull.Value;
					else
						dr["ActualStartDate"] =		dt.Rows[i]["ActualStartDate"].ToString();
					
					if(dt.Rows[i]["ActualFinishDate"].ToString().Length<1)
						dr["ActualFinishDate"] =	System.DBNull.Value;
					else
						dr["ActualFinishDate"] =	dt.Rows[i]["ActualFinishDate"].ToString();
                    //父节点起始值为子结点最小值

                    if (BLL.ConvertRule.ToInt(BLL.ConvertRule.ToDateString(parentdr["PlannedStartDate"], "yyyy-MM-dd").Replace("-", "")) > BLL.ConvertRule.ToInt(BLL.ConvertRule.ToDateString(dt.Rows[i]["PlannedStartDate"], "yyyy-MM-dd").Replace("-", "")))
                    {
                        parentdr["PlannedStartDate"] = dt.Rows[i]["PlannedStartDate"].ToString();
                        isparentdatachange = true;
                    }
                    if (BLL.ConvertRule.ToInt(BLL.ConvertRule.ToDateString(parentdr["PlannedFinishDate"], "yyyy-MM-dd").Replace("-", "")) < BLL.ConvertRule.ToInt(BLL.ConvertRule.ToDateString(dt.Rows[i]["PlannedFinishDate"], "yyyy-MM-dd").Replace("-", "")))
                    {
                        parentdr["PlannedFinishDate"] = dt.Rows[i]["PlannedFinishDate"].ToString();
                        isparentdatachange = true;
                    }
                    if (BLL.ConvertRule.ToInt(BLL.ConvertRule.ToDateString(parentdr["ActualStartDate"], "yyyy-MM-dd").Replace("-", "")) > BLL.ConvertRule.ToInt(BLL.ConvertRule.ToDateString(dt.Rows[i]["ActualStartDate"], "yyyy-MM-dd").Replace("-", "")))
                    {
                        parentdr["ActualStartDate"] = dt.Rows[i]["ActualStartDate"].ToString();
                        isparentdatachange = true;
                    }
                    if (BLL.ConvertRule.ToInt(BLL.ConvertRule.ToDateString(parentdr["ActualFinishDate"], "yyyy-MM-dd").Replace("-", "")) < BLL.ConvertRule.ToInt(BLL.ConvertRule.ToDateString(dt.Rows[i]["ActualFinishDate"], "yyyy-MM-dd").Replace("-", "")))
                    {
                        parentdr["ActualFinishDate"] = dt.Rows[i]["ActualFinishDate"].ToString();
                        isparentdatachange = true;
                    }


                    dr["Status"] = BLL.WBSRule.GetTaskStatusNumberByCompletePercent(BLL.ConvertRule.ToString(dt.Rows[i]["CompletePercent"]).Replace("%", ""));//
					dr["ImportantLevel"] =		0;

					dr["CompletePercent"] = dt.Rows[i]["CompletePercent"];

					wbs.CurrentTable.Rows.Add(dr);
					DAL.EntityDAO.WBSDAO.InsertTask(wbs);
						
					//this.AddRule(strWBSCode);

					this.AddChildTask(wbs,dt,projectCode,dt.Rows[i]["outLine"].ToString(),strpFullCode+"-"+strWBSCode,strWBSCode);
				}
                if (isparentdatachange)
                {
                    DataRow[] parentr = wbs.CurrentTable.Select(string.Format("WBSCode={0}", strNewWBSCode));
                    parentr[0]["PlannedStartDate"] = parentdr["PlannedStartDate"];
                    parentr[0]["PlannedFinishDate"] = parentdr["PlannedFinishDate"];
                    parentr[0]["ActualStartDate"] = parentdr["ActualStartDate"];
                    parentr[0]["ActualFinishDate"] = parentdr["ActualFinishDate"];
                    DAL.EntityDAO.WBSDAO.UpdateTask(wbs);
                }
			}
		}*/

		private DataTable InitTable(StreamReader m_sr)
		{
			DataTable dt = new DataTable();
			dt.Columns.Add("TaskName");
			dt.Columns.Add("ImportantLevel");
			dt.Columns.Add("CompletePercent", typeof(int));
			dt.Columns.Add("Status");
			dt.Columns.Add("PlannedStartDate");
			dt.Columns.Add("PlannedFinishDate");
			dt.Columns.Add("ActualStartDate");
			dt.Columns.Add("ActualFinishDate");
			dt.Columns.Add("outLine");

			while (m_sr.Peek() >= 0) 
			{
				string s = m_sr.ReadLine();
				DataRow dr = dt.NewRow();
                string[] arWBS = BLL.ImportRule.SplitCsvLine(s);
				dr["outLine"] = arWBS[10];

//				dr["WBSCode"] = arWBS[10].Split(';')[4];
//				dr["ParentCode"] = arWBS[10].Split(';')[3];
//				dr["SortID"] = arWBS[0];
				dr["TaskName"] = arWBS[1];
				//dr["Status"] = "0".Length;
                         
				dr["ImportantLevel"] = "0";

				string CompletePercent = arWBS[5].Replace("%", "");
                dr["Status"] = BLL.WBSRule.GetTaskStatusNumberByCompletePercent(CompletePercent);
				dr["CompletePercent"] = BLL.ConvertRule.ToInt(CompletePercent);

				dr["PlannedStartDate"] = arWBS[6];
				dr["PlannedFinishDate"] = arWBS[7];
				dr["ActualStartDate"] = arWBS[8];
				dr["ActualFinishDate"] = arWBS[9];

				dt.Rows.Add(dr);
			}
			return dt;
		}

		private string GetSortID(string strWBSCode)
		{
			string strSortID = "";
			// 取得本级SortID然后+5
			DAL.QueryStrategy.WBSStrategyBuilder WSB = new RmsPM.DAL.QueryStrategy.WBSStrategyBuilder();	
			WSB.AddStrategy(new Strategy(DAL.QueryStrategy.WBSStrategyName.ParentCode,strWBSCode));	
			WSB.AddOrder("SortID", false);
			string sql = WSB.BuildMainQueryString();

			QueryAgent QA = new QueryAgent();
			QA.SetTopNumber(1);
			DataSet dsTask = QA.ExecSqlForDataSet(sql);
			QA.Dispose();
			if(dsTask.Tables.Count>0&&dsTask.Tables[0].Rows.Count>0)
			{
				long intTmp = BLL.ConvertRule.ToLong(dsTask.Tables[0].Rows[0]["SortID"])+1;
				strSortID = "0"+intTmp.ToString();
			}
			else
			{
				EntityData entitySort = WBSDAO.GetV_TaskByCode(strWBSCode);
				strSortID = entitySort.GetString("SortID")+"01";
			}
			return strSortID;
		}

		private void AddRule(string strWBSCode)
		{
			// 此处添加监督人
			this.AddTaskMaster(strWBSCode);
			// 添加权限资源					
			//this.SaveRS(strWBSCode,base.user.UserCode,"","070101,070102,070103,070104,070105,070106,070107,070108,070109,070110");// 初始拥有工作的全部权限
		}

		/// <summary>
		/// 添加初始项目的监督人
		/// </summary>
		/// <param name="strWBSCode"></param>
		private void AddTaskMaster(string strWBSCode)
		{
			string strUser = base.user.UserCode;
			if(strUser.Length>0)
			{
				string[] arUser = strUser.Split(',');
				EntityData entityUser = WBSDAO.GetAllTaskPerson();	
				foreach(string sUser in arUser)
				{
					DataRow drUser = entityUser.GetNewRecord();
					drUser["WBSCode"] = strWBSCode;
					drUser["TaskPersonCode"] = SystemManageDAO.GetNewSysCode("TaskPerson");
					drUser["UserCode"] = sUser;
					drUser["RoleType"] = "0"; // 0代表人
					drUser["Type"] = "2"; // 2代表负责人，在此设定默认为此任务的负责人
					drUser["ExecuteCode"] = "";
					entityUser.AddNewRecord(drUser);
					WBSDAO.InsertTaskPerson(entityUser);
				}				
				entityUser.Dispose();
			}
		}

		/// <summary>
		/// 添加权限资源
		/// </summary>
		private void SaveRS(string strMasterCode,string strUser,string strStation,string strOption)
		{			
			// 责任人分配任务时加入自己的权限
			if(strUser.Length>0&&strUser.IndexOf(base.user.UserCode)<0)
				strUser+=","+base.user.UserCode;

			ArrayList arOperator = new ArrayList();
			if(strUser.Length>0)
			{
				foreach(string strTUser in strUser.Split(','))
				{
					if(strTUser=="") continue;
					AccessRange acRang = new AccessRange();
					acRang.AccessRangeType = 0;
					acRang.RelationCode = strTUser;
					acRang.Operations = strOption;
					arOperator.Add(acRang);
				}
			}			
			if(strStation.Length>0)
			{
				foreach(string strTStation in strStation.Split(','))
				{
					if(strTStation=="") continue;
					AccessRange acRang = new AccessRange();
					acRang.AccessRangeType = 1;
					acRang.RelationCode = strStation;
					acRang.Operations = strOption;
					arOperator.Add(acRang);
				}
			}
			
			if(arOperator.Count>0)
				BLL.ResourceRule.SetResourceAccessRange(strMasterCode,strOption.Substring(0,4),"",arOperator,false);
		}

         private void AddChildTask(EntityData wbs, DataTable dt, string projectCode, string pOutline, string strpFullCode, string strNewWBSCode)
         {
             //DataRow[] arDR = dt.Select("outLine like '"+pOutLine+".[^.]%'");
             for (int i = 0; i < dt.Rows.Count; i++)
             {
                 string strLine = dt.Rows[i]["outLine"].ToString(); // 当前节点
                 if (strLine.Length < pOutline.Length) continue;
                 int ipre = strLine.LastIndexOf('.'); if (ipre < 1) continue;
                 if (strLine.Substring(0, ipre) == pOutline)
                 {
                     DataRow dr = wbs.GetNewRecord();
                     string strWBSCode = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("WBS");
                     dr["WBSCode"] = strWBSCode;
                     dr["TaskName"] = dt.Rows[i]["TaskName"].ToString();
                     dr["ProjectCode"] = projectCode;
                     dr["ParentCode"] = strNewWBSCode;//pWBSCode为老的ParentCode
                     dr["Deep"] = strpFullCode.Split('-').Length;
                     dr["SortID"] = this.GetSortID(strNewWBSCode);//arDR[i]["SortID"].ToString();								
                     dr["FullCode"] = strpFullCode + "-" + strWBSCode;
                     dr["Flag"] = "0";

                     if (dt.Rows[i]["PlannedStartDate"].ToString().Length < 1)
                         dr["PlannedStartDate"] = System.DBNull.Value;
                     else
                         dr["PlannedStartDate"] = dt.Rows[i]["PlannedStartDate"].ToString();

                     if (dt.Rows[i]["PlannedFinishDate"].ToString().Length < 1)
                         dr["PlannedFinishDate"] = System.DBNull.Value;
                     else
                         dr["PlannedFinishDate"] = dt.Rows[i]["PlannedFinishDate"].ToString();

                     if (dt.Rows[i]["ActualStartDate"].ToString().Length < 1)
                         dr["ActualStartDate"] = System.DBNull.Value;
                     else
                         dr["ActualStartDate"] = dt.Rows[i]["ActualStartDate"].ToString();

                     if (dt.Rows[i]["ActualFinishDate"].ToString().Length < 1)
                         dr["ActualFinishDate"] = System.DBNull.Value;
                     else
                         dr["ActualFinishDate"] = dt.Rows[i]["ActualFinishDate"].ToString();

                     dr["Status"] = BLL.WBSRule.GetTaskStatusNumberByCompletePercent(BLL.ConvertRule.ToString(dt.Rows[i]["CompletePercent"]).Replace("%", ""));//
                     dr["ImportantLevel"] = 0;

                     dr["CompletePercent"] = dt.Rows[i]["CompletePercent"];

                     wbs.CurrentTable.Rows.Add(dr);
                     DAL.EntityDAO.WBSDAO.InsertTask(wbs);

                     //this.AddRule(strWBSCode);

                     this.AddChildTask(wbs, dt, projectCode, dt.Rows[i]["outLine"].ToString(), strpFullCode + "-" + strWBSCode, strWBSCode);
                 }

             }
         }

    }
}
