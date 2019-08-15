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
	///		ContractModifyButtonControl 的摘要说明。
	/// </summary>
	public partial class ContractModifyButtonControl : ContractControlBase
	{
		private string _ProjectCode = "";
		private string _ContractCode = "";
		private string _ActCode = "";

		/// <summary>
		/// 流程当前步骤代码
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
		/// 合同代码
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
		/// 项目代码
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
			if(this.State == ModuleState.Sightless)//不可见的
			{
				this.Visible = false;
			}
			else if(this.State == ModuleState.Operable)//可操作的
			{
				this.btnModify.Visible = true;
				this.btnModify.Disabled = false;
			}
			else if(this.State == ModuleState.Eyeable)//可见的
			{
				this.btnModify.Visible = false;
				this.btnModify.Disabled = true;
			}
			else if(this.State == ModuleState.Begin)//可见的
			{
				this.btnModify.Visible = false;
				this.btnModify.Disabled = true;
			}
			else if(this.State == ModuleState.End)//可见的
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
		///		设计器支持所需的方法 - 不要使用代码编辑器
		///		修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion
	}
}
