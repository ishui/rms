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

namespace RmsPM.Web.BiddingManage
{
	/// <summary>
	/// BiddingReturnList1 的摘要说明。
	/// </summary>
	public partial class BiddingReturnQuery : System.Web.UI.Page
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			LoadData();
		}
		private void LoadData()
		{
			this.BiddingEmitList1.BiddingCode = Request["BiddingCode"]+"";
			this.BiddingEmitList1.DataBound();
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
