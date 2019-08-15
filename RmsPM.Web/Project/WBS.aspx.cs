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

namespace RmsPM.Web.Project
{
	/// <summary>
	/// WBS 的摘要说明。
	/// </summary>
	public partial class WBS : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlInputButton btnStart;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnGoing;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnCancel;
		protected System.Web.UI.HtmlControls.HtmlInputButton btmHalt;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnComplete;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtSortField;
		protected System.Web.UI.HtmlControls.HtmlInputHidden TaskStatus;
		protected System.Web.UI.WebControls.TextBox TextBox2;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnAll;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if (!this.IsPostBack)
			{
			}

			User myUser = new User(user.UserCode);
			this.spanIOButton.Style["display"] = (myUser.HasOperationRight("070103"))?"":"none"; // 070103为WBS导入导出权限
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
