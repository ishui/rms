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
using RmsPM.BLL;
using RmsPM.DAL;
using RmsPM.DAL.EntityDAO;
using RmsPM.DAL.QueryStrategy;
using Rms.Web;

namespace RmsPM.Web.Cost
{
	/// <summary>
	/// 费用模板
	/// </summary>
	public partial class CBSModuleModify : PageBase
	{



		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack)
			{
				IniPage();
				LoadData();
			}
		}

		private void LoadData()
		{
			string cbsModuleCode = Request["CBSModuleCode"] + "";

			try
			{
				EntityData entity = DAL.EntityDAO.CBSDAO.GetCBSModuleByCode(cbsModuleCode);
				if ( entity.HasRecord())
				{
					this.txtName.Value = entity.GetString("CBSModuleName");
					this.txtRemark.Value = entity.GetString("Remark");
				}
				entity.Dispose();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog( this.ToString(),ex,"" );
			}
		}

		private void IniPage()
		{
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


		// 保存这个空预算
		protected void btnSave_ServerClick(object sender, System.EventArgs e)
		{
			string cbsModuleCode = Request["CBSModuleCode"] + "";
			bool isNew = (cbsModuleCode=="");
			try
			{

				EntityData entity = null;
				DataRow dr = null;
				if ( isNew )
				{
					entity = new EntityData("CBSModule");
					dr = entity.GetNewRecord();
					cbsModuleCode=DAL.EntityDAO.SystemManageDAO.GetNewSysCode("CBSModuleCode");
					dr["CBSModuleCode"] = cbsModuleCode ;
					entity.AddNewRecord(dr);
				}
				else
				{
					entity = DAL.EntityDAO.CBSDAO.GetCBSModuleByCode(cbsModuleCode);
					dr = entity.CurrentRow;
				}

				dr["CBSModuleName"] = this.txtName.Value;
				dr["Remark"] = this.txtRemark.Value;
				dr["CreatePerson"] = ((User)Session["User"]).UserCode;
				dr["CreateDate"] = DateTime.Now.Date;

				if ( isNew )
					DAL.EntityDAO.CBSDAO.InsertCBSModule(entity);
				else
					DAL.EntityDAO.CBSDAO.UpdateCBSModule(entity);

				CloseWindow( cbsModuleCode );
			}
			catch( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}

		}

		private void CloseWindow(string cbsModuleCode)
		{
			Response.Write( Rms.Web.JavaScript.ScriptStart );
			Response.Write( Rms.Web.JavaScript.OpenerReload(false) );
			Response.Write( "window.close();" );
			Response.Write( Rms.Web.JavaScript.ScriptEnd );
		}
	}
}
