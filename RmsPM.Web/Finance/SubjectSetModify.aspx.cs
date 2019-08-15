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
	/// SubjectSetModify ��ժҪ˵����
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ����" + ex.Message));
			}
		}

		private void CloseWindow()
		{
			Response.Write( Rms.Web.JavaScript.ScriptStart );
			Response.Write( Rms.Web.JavaScript.OpenerReload(false) );
			Response.Write( Rms.Web.JavaScript.WinClose(false) );
			Response.Write( Rms.Web.JavaScript.ScriptEnd );
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

		/// <summary>
		/// ��Ч�Լ��
		/// </summary>
		/// <param name="Hint"></param>
		/// <returns></returns>
		private bool CheckValid(ref string Hint) 
		{
			Hint = "";

			if (this.txtSubjectSetName.Value.Trim() == "")
			{
				Hint = "�������������� ��";
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "�������" + ex.Message));
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
					Response.Write(Rms.Web.JavaScript.Alert(true,"���й�˾ʹ��������ף�����ɾ�� ��"));
					return;
				}

				EntityData subjectSet = DAL.EntityDAO.SubjectDAO.GetSubjectSetByCode(subjectSetCode);
				DAL.EntityDAO.SubjectDAO.DeleteSubjectSet(subjectSet);
				subjectSet.Dispose();

			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "ɾ������" + ex.Message));
				return;
			}

			CloseWindow();
		}

	}
}
