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

namespace RmsPM.Web.DesignChange
{
	/// <summary>
	/// DesignChangeModify ��ժҪ˵����
	/// </summary>
	public partial class DesignChangeModify : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if(!this.IsPostBack)
			{
				PageOnit();
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

		#region ˽������ -----------------------------------------------
		#endregion


		#region �������� -----------------------------------------------
		/// <summary>
		/// �б굥λ����ҳ��
		/// </summary>
		public string DesignAuditingUrl
		{
			get
			{
				return BLL.WorkFlowRule.GetProcedureURLByName("��Ʊ������");
			}
		}
		#endregion


		#region ˽�з��� -----------------------------------------------
		private void PageState()
		{
			if(DesignChangeControl1.State=="0")
			{
				btn_ChangeAuditing.Visible=true;
				Edit_ViewState();
			}
			else
			{
				Edit_ViewState();
				btn_Modify.Visible=false;
				btn_ChangeAuditing.Visible=false;
			}
		}
		private void Edit_ViewState()
		{
			if(DesignChangeControl1.IsEditMode)
			{
				btn_Modify.Visible=false;
				btn_Save.Visible=true;
				Bt_Cancel.Visible=true;
				btn_ChangeAuditing.Visible=false;
			}
			else
			{
				btn_Modify.Visible=true;
				btn_Save.Visible=false;
				Bt_Cancel.Visible=false;
				btn_ChangeAuditing.Visible=true;
			}
		}
		private void OutState()
		{
			string thisState=Request["State"]+"";
			if(thisState=="Edit")
			{
				this.DesignChangeControl1.IsEditMode=true;
			}
			else
			{
				this.DesignChangeControl1.IsEditMode=false;
			}
		}
		private void PageOnit()
		{
			OutState();			
			DesignChangeControl1.InitControl();
			PageState();
		}
		#endregion


		#region �������� -----------------------------------------------
		#endregion


		#region �¼����� -----------------------------------------------
		protected void btn_Save_ServerClick(object sender, System.EventArgs e)
		{
			DesignChangeControl1.SumitData();
			DesignChangeControl1.InitControl();
            DesignChangeControl1.State = "0";
			PageState();
		}

		protected void btn_Modify_ServerClick(object sender, System.EventArgs e)
		{
			DesignChangeControl1.IsEditMode=true;
			DesignChangeControl1.InitControl();
			PageState();
		}

		protected void Bt_Cancel_ServerClick(object sender, System.EventArgs e)
		{
			DesignChangeControl1.IsEditMode=false;
			DesignChangeControl1.InitControl();
			PageState();
		}
		#endregion

		
	}
}
