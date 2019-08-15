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

namespace RmsPM.Web.Project
{
	/// <summary>
	/// 工作分解树导出
	/// </summary>
	public partial class WBSTempletOut : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtInputCode;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				IniPage();
			}
		}

		private void IniPage()
		{
			try
			{
				ViewState["ProjectCode"] = Request["ProjectCode"].ToString();
				this.xc_date.Value = DateTime.Now.ToString("yyyy-MM-dd");
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"初始化页面出错");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
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
			if (this.txtTempletName.Text.Trim().Length<0)
			{
				Response.Write(Rms.Web.JavaScript.Alert(true,"模板名称不能为空 ！"));
				return;
			}

			string templetType = "WBS";
			try
			{
				string curCode = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("Templet");
				EntityData entity= new EntityData("Standard_WBSTemplet");

				entity.SetCurrentTable("Templet");
				DataRow drTemplet =entity.GetNewRecord();
				drTemplet["TITLE"]=this.txtTempletName.Text;
				drTemplet["TempletCode"]=curCode;
				drTemplet["XcDate"] = this.xc_date.Value;
				drTemplet["TempletType"]=templetType;

				entity.AddNewRecord(drTemplet);

				entity.SetCurrentTable("WBSTemplet");

				DAL.QueryStrategy.WBSStrategyBuilder WSB = new RmsPM.DAL.QueryStrategy.WBSStrategyBuilder();
				WSB.AddStrategy(new Strategy(DAL.QueryStrategy.WBSStrategyName.ProjectCode,(string)ViewState["ProjectCode"]));
				string sql = WSB.BuildMainQueryString();
				QueryAgent QA = new QueryAgent();
				EntityData entityProject = QA.FillEntityData("Task",sql + " and Flag = 1");
				EntityData entityTask = QA.FillEntityData("Task",sql + " and Flag = 0 ");
				
				string ProjectWBSCode = "";
				if (entityProject.HasRecord())
				{
					ProjectWBSCode = entityProject.GetString("WBSCode");
				}
				entityProject.Dispose();
				QA.Dispose();

				DataRow drWBSTemplet =null;
				DataRow drTask = null;

				int iCount = entityTask.CurrentTable.Rows.Count;
				
				for(int i=0;i<iCount;i++)
				{
					// 记录原先TASK表中的所有内容
					entityTask.SetCurrentRow(i);
					drTask = entityTask.CurrentRow;
					string tmpletItemCode=DAL.EntityDAO.SystemManageDAO.GetNewSysCode("WBSTempletItemCode");
					string wbsCode = entityTask.GetString("WBSCode");
					drWBSTemplet=entity.GetNewRecord();
					
					drWBSTemplet["WBSTempletItemCode"]=tmpletItemCode;
					drWBSTemplet["TempletCode"]=curCode;
					
					drWBSTemplet["WBSCode"]=drTask["WBSCode"];
					drWBSTemplet["TaskCode"]=drTask["TaskCode"];
					drWBSTemplet["TaskName"]=drTask["TaskName"];
					drWBSTemplet["OutLineNumber"]=drTask["OutLineNumber"];
					drWBSTemplet["SortID"]=drTask["SortID"];
					drWBSTemplet["ParentCode"]=drTask["ParentCode"].ToString().Replace(ProjectWBSCode,"");;
					drWBSTemplet["Deep"]=drTask["Deep"];
					drWBSTemplet["FullCode"]=drTask["FullCode"].ToString().Replace(ProjectWBSCode + "-","");
					drWBSTemplet["PlannedStartDate"]=drTask["PlannedStartDate"];
					drWBSTemplet["PlannedFinishDate"]=drTask["PlannedFinishDate"];
					drWBSTemplet["ActualStartDate"]=drTask["ActualStartDate"];
					drWBSTemplet["ActualFinishDate"]=drTask["ActualFinishDate"];
					drWBSTemplet["EarlyFinishDate"]=drTask["EarlyFinishDate"];
					drWBSTemplet["EarlyStartDate"]=drTask["EarlyStartDate"];
					drWBSTemplet["LastFinishDate"]=drTask["LastFinishDate"];
					drWBSTemplet["LastStartDate"]=drTask["LastStartDate"];
					drWBSTemplet["Duration"]=drTask["Duration"];
					drWBSTemplet["Remark"]=drTask["Remark"];
					drWBSTemplet["ImportantLevel"]=drTask["ImportantLevel"];
					drWBSTemplet["Flag"]=drTask["Flag"];
					drWBSTemplet["ImageFileName"]=drTask["ImageFileName"];

					entity.AddNewRecord(drWBSTemplet); 
				}

				//entityWBS.Dispose();
				entityTask.Dispose();
				WBSDAO.SubmitAllStandard_WBSTemplet(entity);
				entity.Dispose();

				
				Response.Write(JavaScript.ScriptStart);
//				Response.Write(JavaScript.Alert(false,"导出成功 ！"));
				Response.Write("window.close();");
				Response.Write(JavaScript.ScriptEnd);
			

			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "导出出错：" + ex.Message));
			}
		}


	}
}
