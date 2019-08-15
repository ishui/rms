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

namespace RmsPM.Web.Systems
{
	/// <summary>
	/// DepartmentView ��ժҪ˵����
	/// </summary>
	public partial class DepartmentView : PageBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if ( !IsPostBack)
			{
				this.txtUnitCode.Value = Request["UnitCode"] + "";
				this.txtAction.Value = Request.QueryString["Action"] + "";
				this.txtCloseScript.Value = Request["CloseScript"] + "";

				string ButtonClose = Request["ButtonClose"] + "";
				if (ButtonClose == "0") 
				{
					this.trButton.Visible = false;
				}

				LoadData();
			}
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

		private void LoadData()
		{
			string UnitCode = this.txtUnitCode.Value;

			if (UnitCode == "")
				return;

			try
			{
				EntityData entity = DAL.EntityDAO.OBSDAO.GetUnitByCode(UnitCode);
				if ( entity.HasRecord())
				{
					this.lblUnitName.Text = entity.GetString("UnitName");
					this.lblPrincipal.Text = entity.GetString("Principal");
					this.lblRemark.Text = entity.GetString("Remark");
				}
				entity.Dispose();
			}
			catch( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"��ȡ���Žڵ����");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ȡ���Žڵ����"));
			}
		}

	}
}
