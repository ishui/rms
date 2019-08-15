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
using Rms.Web;

namespace RmsPM.Web.BiddingManage
{
	/// <summary>
	/// BiddingSupplierList 的摘要说明。
	/// </summary>
	public partial class BiddingSupplierList : PageBase
	{

	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !Page.IsPostBack )
			{
				this.IniPage();
				this.LoadData();
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


		/// <summary>
		/// 页面初始化
		/// </summary>
		private void IniPage()
		{
			try
			{
				this.TabAdd.Visible = false;
				this.btnAdd.Visible = false;
				this.btnModify.Visible = false;
				this.btnSave.Visible = false;

				string strBiddingPrejudicationCode = Request.QueryString["BiddingPrejudicationCode"] + "";
				string strState = Request.QueryString["State"] + "";
				string strSelect = Request.QueryString["Select"] + "";

				if ( ""==strBiddingPrejudicationCode || ""==strState )
				{
					Response.Write( JavaScript.ScriptStart );
					Response.Write( JavaScript.Alert(false,"非法请求！") );
					Response.Write( JavaScript.WinClose(false) );
					Response.Write( JavaScript.ScriptEnd );
					return;
				}

				this.HideBiddingPrejudicationCode.Value = strBiddingPrejudicationCode;

				if ( "view"==strState )
				{
				}
				else if ( "edit"==strState )
				{
					this.btnModify.Visible = true;
					this.TabAdd.Visible = true;
					this.btnAdd.Visible = true;
				}

				if ( "true"==strSelect )
				{
					this.btnSave.Visible = true;
				}

				//*** UCBiddingSupplierModify 控件初始化 **************************************************************************
				this.UCBiddingSupplierModify1.BiddingPrejudicationCode = strBiddingPrejudicationCode;
				this.UCBiddingSupplierModify1.BiddingSupplierCode = "";
				this.UCBiddingSupplierModify1.DoType = "SingleModify";
				this.UCBiddingSupplierModify1.IniControl();
				//*****************************************************************************

				//*** UCBiddingSupplierList(参加资格预审的单位名单) 控件初始化 **************************************************************************
				this.UCBiddingSupplierList1.BiddingPrejudicationCode = strBiddingPrejudicationCode;
				this.UCBiddingSupplierList1.CanSelect = this.btnSave.Visible;
				this.UCBiddingSupplierList1.CanModify = this.btnModify.Visible;
				this.UCBiddingSupplierList1.IniControl();
				//*****************************************************************************
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}


		/// <summary>
		/// 装载页面数据
		/// </summary>
		private void LoadData()
		{
			try
			{
				this.UCBiddingSupplierList1.LoadData();
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}


		/// <summary>
		/// 保存按钮事件(预审通过)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnSave_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				this.UCBiddingSupplierList1.BiddingPrejudicationCode = this.HideBiddingPrejudicationCode.Value.Trim();
				this.UCBiddingSupplierList1.SaveData();
				//Response.Write( JavaScript.PageTo(true,"./BiddingSupplierList.aspx?BiddingPrejudicationCode="+this.HideBiddingPrejudicationCode.Value) );
				Response.Write( JavaScript.WinClose(true) );
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}


		/// <summary>
		/// 编辑按钮事件(修改列表内容)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnModify_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				this.UCBiddingSupplierList1.ModifyData();
				this.UCBiddingSupplierList1.LoadData();
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

		protected void btnAdd_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				string myHint = "";

				if ( this.UCBiddingSupplierModify1.CheckData(out myHint) )
				{
					this.UCBiddingSupplierModify1.SaveData();
					this.UCBiddingSupplierList1.LoadData();
				}
				else
				{
					Response.Write( JavaScript.Alert(true,myHint) );
					return;
				}
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}


	}
}
