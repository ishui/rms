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
using RmsPM.DAL.QueryStrategy;

namespace RmsPM.Web
{
	/// <summary>
	/// RejectAccess 的摘要说明。
	/// </summary>
	public partial class RejectAccess :  System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack) 
			{
				string OperationCode = Request.QueryString["OperationCode"] + "";
				string OperationName = Request.QueryString["OperationName"] + "";

				//显示权限名称
				if (OperationName.Length > 0)
				{
					this.lblOperationName.Text = OperationName;
				}
				else if (OperationCode.Length > 0) 
				{
					this.lblOperationName.Text = BLL.SystemRule.GetFunctionStructureName(OperationCode);
				}
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
