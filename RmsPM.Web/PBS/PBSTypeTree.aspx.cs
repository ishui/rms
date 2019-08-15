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

namespace RmsPM.Web.PBS
{
	/// <summary>
	/// PBSTypeTree 的摘要说明。
	/// </summary>
	public partial class PBSTypeTree : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtShowItems;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtTreeType;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtCheckBalance;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack) 
			{
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];
				this.txtAct.Value = Request.QueryString["act"];
				this.txtAct.Value = this.txtAct.Value.ToLower();
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
	}
}
