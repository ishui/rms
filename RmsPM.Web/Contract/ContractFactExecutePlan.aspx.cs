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
using Rms.ORMap;
using RmsPM.DAL;

namespace RmsPM.Web.Contract
{
	/// <summary>
	/// ContractExecutePlanModify 的摘要说明。
	/// </summary>
	public partial class ContractFactExecutePlan : PageBase
	{

	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack )
			{
				IniPage();
				LoadData();
			}
		}



		private void IniPage()
		{
			try
			{
				//BLL.PageFacade.LoadAllUserSelect(this.sltFactPerson,"");
				this.txtFactExecutor.Value = user.UserCode;
				this.txtFactExecutorName.Value = user.UserName;
			}
			catch ( Exception ex)
			{
				ApplicationLog.WriteLog ( this.ToString(),ex,"初始化页面失败。");
			}
		}

		private void LoadData()
		{
			string code = Request["ContractExecutePlanCode"] + "";
			if (code == "" )
				return;

			try
			{
				EntityData entity=DAL.EntityDAO.ContractDAO.GetContractExecutePlanByCode(code);
				if ( entity.HasRecord())
				{
					this.lblExecuteDetail.Text = entity.GetString("ExecuteDetail");
					this.txtDescription.Text = entity.GetString("Description") ;
					this.FactExecuteDate.Value = entity.GetDateTimeOnlyDate("FactExecuteDate");
					//this.sltFactPerson.Value = entity.GetString("FactExecutor") ;
					this.LabelsltPerson.Text = BLL.SystemRule.GetUserName( entity.GetString("Executor"));
					this.LabeldtbExecuteDate.Text = entity.GetDateTimeOnlyDate("ExecuteDate");
					
					string factUserCode = entity.GetString("FactExecutor");
					if ( factUserCode != "" )
					{
						this.txtFactExecutor.Value = factUserCode;
						this.txtFactExecutorName.Value = BLL.SystemRule.GetUserName(factUserCode);
					}
					
				}
				entity.Dispose();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
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
			string contractCode = Request["ContractCode"] + "";
			string projectCode = Request["ProjectCode"] + "";
			string code = Request["ContractExecutePlanCode"] + "";
			if ( code == "" )
				return;

			if ( this.txtFactExecutor.Value=="" )
			{
				Response.Write(Rms.Web.JavaScript.Alert(true,"请填写实际执行人员 ！"));
				return;
			}

			string edt = this.FactExecuteDate.Value;
			if ( edt != "" )
			{
				if ( !Rms.Check.StringCheck.IsDateTime(edt) )
				{
					Response.Write(Rms.Web.JavaScript.Alert(true,"请填写实际执行时间 ！"));
					return;
				}
			}

			try
			{
				EntityData entity=DAL.EntityDAO.ContractDAO.GetContractExecutePlanByCode(code);
				if ( entity.HasRecord())
				{
					//entity.CurrentRow["ExecuteDetail"] = this.ExecuteDetail.Text;
					entity.CurrentRow["Description"] = this.txtDescription.Text;
					if (edt != "" )
						entity.CurrentRow["FactExecuteDate"] = DateTime.Parse(edt);
					else
						entity.CurrentRow["FactExecuteDate"] = System.DBNull.Value;

					entity.CurrentRow["FactExecutor"] = this.txtFactExecutor.Value;
					DAL.EntityDAO.ContractDAO.UpdateContractExecutePlan(entity);
				}
				
				entity.Dispose();

				Response.Write(Rms.Web.JavaScript.ScriptStart);
				Response.Write("window.opener.navigate('../Contract/ContractInfo.aspx?Page=2&ProjectCode="+projectCode+"&ContractCode="+contractCode+"');");
				Response.Write(Rms.Web.JavaScript.WinClose(false));
				Response.Write(Rms.Web.JavaScript.ScriptEnd);
				Response.End();

			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"保存错误");
			}
		}
	}
}
