namespace RmsPM.Web.ContractFlow
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using RmsPM.Web.WorkFlowControl;

	/// <summary>
	///		ContractModifyButtonControl ��ժҪ˵����
	/// </summary>
	public partial class ContractModifyButtonControl : ContractControlBase
	{
		private string _ProjectCode = "";
		private string _ContractCode = "";
		private string _ActCode = "";

		/// <summary>
		/// ���̵�ǰ�������
		/// </summary>
		public string ActCode
		{
			get
			{
				if ( _ActCode == "" )
				{
					if(this.ViewState["_ActCode"] != null)
						return this.ViewState["_ActCode"].ToString();
					return "";
				}
				return _ActCode;
			}
			set
			{
				_ActCode = value;
				this.ViewState["_ActCode"] = value;
			}
		}

		/// <summary>
		/// ��ͬ����
		/// </summary>
		public string ContractCode
		{
			get
			{
				if ( _ContractCode == "" )
				{
					if(this.ViewState["_ContractCode"] != null)
						return this.ViewState["_ContractCode"].ToString();
					return "";
				}
				return _ContractCode;
			}
			set
			{
				_ContractCode = value;
				this.ViewState["_ContractCode"] = value;
			}
		}
		
		/// <summary>
		/// ��Ŀ����
		/// </summary>
		public string ProjectCode
		{
			get
			{
				if ( _ProjectCode == "" )
				{
					if(this.ViewState["_ProjectCode"] != null)
						return this.ViewState["_ProjectCode"].ToString();
					return "";
				}
				return _ProjectCode;
			}
			set
			{
				_ProjectCode = value;
				this.ViewState["_ProjectCode"] = value;
			}
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
		}
		public void InitControl()
		{
			if(this.State == ModuleState.Sightless)//���ɼ���
			{
				this.Visible = false;
			}
			else if(this.State == ModuleState.Operable)//�ɲ�����
			{
				this.btnModify.Visible = true;
				this.btnModify.Disabled = false;
			}
			else if(this.State == ModuleState.Eyeable)//�ɼ���
			{
				this.btnModify.Visible = false;
				this.btnModify.Disabled = true;
			}
			else if(this.State == ModuleState.Begin)//�ɼ���
			{
				this.btnModify.Visible = false;
				this.btnModify.Disabled = true;
			}
			else if(this.State == ModuleState.End)//�ɼ���
			{
				this.btnModify.Visible = false;
				this.btnModify.Disabled = true;
			}
			else
			{
				this.Visible = false;
			}
			this.SpanScriptForModifyButtonControl.InnerHtml = "<script language=\"javascript\">"
				+"function DoModify()"
				+"{"
				+"    window.location.href='ContractModify.aspx?Act=Edit&ProjectCode="+this.ProjectCode+"&ContractCode="+this.ContractCode+"&ActCode="+this.ActCode+"';"
				+"}"
				+"</script>";
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
		///		�����֧������ķ��� - ��Ҫʹ�ô���༭��
		///		�޸Ĵ˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion
	}
}
