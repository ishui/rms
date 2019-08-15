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
	public partial class TaskTempletOut : PageBase
	{
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator1;
		protected System.Web.UI.WebControls.RequiredFieldValidator Requiredfieldvalidator2;
	
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
				this.txtWBSCode.Value = Request.QueryString["WBSCode"];

				//工作项信息
				EntityData entity = DAL.EntityDAO.WBSDAO.GetTaskByCode(this.txtWBSCode.Value);
				entity.Dispose();
				if (!entity.HasRecord() )
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "工作项不存在"));
					return;
				}

				this.lblTaskName.Text = entity.GetString("TaskName");
				this.txtProjectCode.Value = entity.GetString("ProjectCode");

				this.xc_date.Value = DateTime.Now.ToString("yyyy-MM-dd");
				PageFacade.LoadTempletSelect(this.sltTemplet,"","WBSTask");

				//缺省模板名称
				this.txtTempletName.Text = this.lblTaskName.Text;
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
			try
			{
//				bool isNew = false;
				string TempletName = "";

				if (this.rdoType0.Checked)
				{
//					isNew = true;

					if (this.txtTempletName.Text.Trim().Length == 0)
					{
						Response.Write(Rms.Web.JavaScript.Alert(true,"模板名称不能为空 ！"));
						return;
					}

					TempletName = this.txtTempletName.Text;

					//模板名称不能重复
					EntityData entityT = DAL.EntityDAO.CBSDAO.GetTempletByType("WBSTask");
					if (entityT.HasRecord()) 
					{
						if (entityT.CurrentTable.Select("Title='" + TempletName + "'").Length > 0) 
						{
							Response.Write(Rms.Web.JavaScript.Alert(true, string.Format("模板名称“{0}”已存在，请重新输入", TempletName)));
							return;
						}
					}
					entityT.Dispose();
				}
				else 
				{
//					isNew = false;

					if (this.sltTemplet.Value.Trim().Length<0)
					{
						Response.Write(Rms.Web.JavaScript.Alert(true,"请选择模板 ！"));
						return;
					}

					TempletName = this.sltTemplet.Items[this.sltTemplet.SelectedIndex].Text;

					//删除原模板
					string TempletCode = this.sltTemplet.Value;
					EntityData entityOld = DAL.EntityDAO.WBSDAO.GetStandard_WBSTempletByCode(TempletCode);
					DAL.EntityDAO.WBSDAO.DeleteStandard_WBSTemplet(entityOld);
					entityOld.Dispose();
				}

				string templetType = "WBSTask";
				//工作项信息
				string RootWBSCode = this.txtWBSCode.Value;
				EntityData entity = DAL.EntityDAO.WBSDAO.GetTaskByCode(RootWBSCode);
				entity.Dispose();
				if (!entity.HasRecord() )
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "工作项不存在"));
					return;
				}
				int RootDeep = entity.GetInt("deep");
				string RootFullCode = entity.GetString("FullCode");
				string RootParentFullCode = entity.GetString("FullCode").Replace("-" + RootWBSCode, "");

				string curCode = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("Templet");
				entity= new EntityData("Standard_WBSTemplet");

				entity.SetCurrentTable("Templet");
				DataRow drTemplet =entity.GetNewRecord();
				drTemplet["TITLE"] = TempletName;
				drTemplet["TempletCode"]=curCode;
				drTemplet["XcDate"] = this.xc_date.Value;
				drTemplet["TempletType"] = templetType;

				entity.AddNewRecord(drTemplet);

				entity.SetCurrentTable("WBSTemplet");

				DAL.QueryStrategy.WBSStrategyBuilder WSB = new RmsPM.DAL.QueryStrategy.WBSStrategyBuilder();
				WSB.AddStrategy(new Strategy(DAL.QueryStrategy.WBSStrategyName.ProjectCode,txtProjectCode.Value));
//				WSB.AddStrategy(new Strategy(DAL.QueryStrategy.WBSStrategyName.WBSCode,txtWBSCode.Value));
				string sql = WSB.BuildMainQueryString();
				QueryAgent QA = new QueryAgent();
				EntityData entityTask = QA.FillEntityData("Task",sql + " and (WBSCode = '" + RootWBSCode + "' or FullCode like '" + RootFullCode + "-%')");
				
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

					if (entityTask.GetString("WBSCode") == RootWBSCode) 
					{
						drWBSTemplet["ParentCode"] = "";
					}
					else 
					{
						drWBSTemplet["ParentCode"] = drTask["ParentCode"];
					}

					drWBSTemplet["FullCode"] = drTask["FullCode"].ToString().Replace(RootParentFullCode + "-","");

					drWBSTemplet["Deep"] = BLL.ConvertRule.ToInt(drTask["Deep"]) - RootDeep + 1;

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
