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
using RmsPM.BLL;
using RmsPM.DAL.QueryStrategy;
using Rms.Web;

namespace RmsPM.Web.Finance
{
	/// <summary>
	/// SubjectSetModify 的摘要说明。
	/// </summary>
	public partial class SubjectSetModify : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlAnchor lnkAddNew;
		protected RmsPM.WebControls.ToolsBar.ToolsButton btnBatchSubjectD;
		protected System.Web.UI.HtmlControls.HtmlSelect sltFinanceInterfaceExportBy;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack)
			{
				IniPage();
				LoadData();
			}
		}

		private void IniPage()
		{
			this.txtSubjectSetCode.Value = Request["SubjectSetCode"]+"";
			BLL.PageFacade.LoadFinanceInterfaceSelect(this.sltFinanceInterface, "");
		}

		private void LoadData()
		{

			string subjectSetCode = this.txtSubjectSetCode.Value;
			if ( subjectSetCode == "" )
				return;

			try
			{
				EntityData entity = DAL.EntityDAO.SubjectDAO.GetSubjectSetByCode(subjectSetCode);
				if ( entity.HasRecord())
				{
					this.txtSubjectSetName.Value = entity.GetString("SubjectSetName");
					this.txtSubjectRule.Value = entity.GetString("SubjectRule");
					this.sltFinanceInterface.Value = entity.GetString("FinanceInterface");
					this.sltFinanceInterfaceExportName.Value = entity.GetString("FinanceInterfaceExportName");
					this.sltFinanceInterfaceUnit.Value = entity.GetString("FinanceInterfaceUnit");
					this.sltFinanceInterfaceUser.Value = entity.GetString("FinanceInterfaceUser");
                    this.sltFinanceInterfaceSupplierCode.Value = entity.GetString("FinanceInterfaceSupplierCode");
                    this.txtRemark.Value = entity.GetString("Remark");
				}
				entity.Dispose();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示出错：" + ex.Message));
			}
		}

		private void CloseWindow()
		{
			Response.Write( Rms.Web.JavaScript.ScriptStart );
			Response.Write( Rms.Web.JavaScript.OpenerReload(false) );
			Response.Write( Rms.Web.JavaScript.WinClose(false) );
			Response.Write( Rms.Web.JavaScript.ScriptEnd );
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

		/// <summary>
		/// 有效性检查
		/// </summary>
		/// <param name="Hint"></param>
		/// <returns></returns>
		private bool CheckValid(ref string Hint) 
		{
			Hint = "";

			if (this.txtSubjectSetName.Value.Trim() == "")
			{
				Hint = "请输入帐套名称 ！";
				return false;
			}

			return true;
		}

		protected void btnSave_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				string Hint = "";
				if (!CheckValid(ref Hint)) 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, Hint));
					return;
				}

				string subjectSetCode = this.txtSubjectSetCode.Value;
				EntityData entity = DAL.EntityDAO.SubjectDAO.GetSubjectSetByCode(subjectSetCode);

				DataRow dr = null;
				bool isNew = !entity.HasRecord();
				if (entity.HasRecord())
				{
					dr = entity.CurrentRow;
				}
				else
				{
					subjectSetCode = BLL.SubjectRule.GetNextSubjectSetCode();
//					subjectSetCode = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("SubjectSetCode");
					this.txtSubjectSetCode.Value = subjectSetCode;
					dr = entity.GetNewRecord();
					dr["SubjectSetCode"] = subjectSetCode;
					entity.AddNewRecord(dr);
				}

				dr["SubjectSetName"] = this.txtSubjectSetName.Value;
				dr["SubjectRule"] = this.txtSubjectRule.Value;
				dr["FinanceInterface"] = this.sltFinanceInterface.Value;
				dr["FinanceInterfaceExportName"] = this.sltFinanceInterfaceExportName.Value;
				dr["FinanceInterfaceUnit"] = this.sltFinanceInterfaceUnit.Value;
                dr["FinanceInterfaceSupplierCode"] = this.sltFinanceInterfaceSupplierCode.Value;
				dr["Remark"] = this.txtRemark.Value;

				if ( isNew )
					DAL.EntityDAO.SubjectDAO.InsertSubjectSet(entity);
				else
					DAL.EntityDAO.SubjectDAO.UpdateSubjectSet(entity);

				entity.Dispose();

			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "保存出错：" + ex.Message));
				return;
			}

			CloseWindow();

		}

		protected void btnDelete_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				string subjectSetCode = this.txtSubjectSetCode.Value;
				bool canDelete = true;
				UnitStrategyBuilder sb = new UnitStrategyBuilder();
				sb.AddStrategy(new Strategy( UnitStrategyName.SubjectSetCode,subjectSetCode ));
				string sql = sb.BuildMainQueryString();
				QueryAgent qa = new QueryAgent();
				EntityData entity = qa.FillEntityData("Unit",sql);
				qa.Dispose();

				if ( entity.HasRecord())
					canDelete = false;
				entity.Dispose();

				if ( ! canDelete )
				{
					Response.Write(Rms.Web.JavaScript.Alert(true,"还有公司使用这个帐套，不能删除 ！"));
					return;
				}

				EntityData subjectSet = DAL.EntityDAO.SubjectDAO.GetSubjectSetByCode(subjectSetCode);
				DAL.EntityDAO.SubjectDAO.DeleteSubjectSet(subjectSet);
				subjectSet.Dispose();

			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "删除出错：" + ex.Message));
				return;
			}

			CloseWindow();
		}

	}
}
