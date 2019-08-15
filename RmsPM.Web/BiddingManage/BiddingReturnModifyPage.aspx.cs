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
using System.Text;
using RmsPM.BFL;

using Rms.ORMap;

namespace RmsPM.Web.BiddingManage
{
	/// <summary>
	/// BiddingReturnModify1 的摘要说明。
	/// </summary>
	public partial class BiddingReturnModifyPage : PageBase
	{

	
		/// ****************************************************************************
		/// <summary>
		/// 页面加载
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// ****************************************************************************
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
				InitPage();
			BiddingReturnModify1.Show_ttachMentAdd(BiddingEmitModify1.ApplicationCode);
		}
		/// ****************************************************************************
		/// <summary>
		/// 初始化
		/// </summary>
		/// ****************************************************************************
		private void InitPage()
		{
			string ApplicationCode = Request["BiddingEmitCode"]+"";

			this.BiddingEmitModify1.ApplicationCode = ApplicationCode;
			this.BiddingReturnModify1.BiddingEmitCode = ApplicationCode;
			this.BiddingReturnModify1.ApplicationCode = "NotIsNew";

			if(Request["State"]+"" == "edit")
			{
				BiddingEmitModify1.State = WorkFlowControl.ModuleState.Eyeable;
				BiddingReturnModify1.State = WorkFlowControl.ModuleState.Operable;

				this.btnSave.Visible = true;
			}
			else if(Request["State"]+"" == "view")
			{
				BiddingEmitModify1.State = WorkFlowControl.ModuleState.Eyeable;
				BiddingReturnModify1.State = WorkFlowControl.ModuleState.Other;
				BiddingReturnModify1.BiddingEmitCode=ApplicationCode;
				this.btnSave.Visible = false;
			}
			if(Request["NowState"]+""=="6")
			{
				Lb_Title.Text="回 价";
			}

			BiddingEmitModify1.InitControl();
			BiddingReturnModify1.InitControl();
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

		protected void btnSave_ServerClick(object sender, System.EventArgs e)
		{
			if(BiddingReturnModify1.BiddingReturnCheck())
			{
				this.RegisterStartupScript("CheckAlert","<script>alert('回标单位金额和日期必须同时填写！');</script>");
                return;
			}
			else
			{
				using(StandardEntityDAO dao=new StandardEntityDAO("BiddingReturn"))
				{
					dao.BeginTrans();
					try
					{
						/***********************************************************/
						this.BiddingReturnModify1.dao = dao;
						this.BiddingReturnModify1.SubmitData();

						/*******************************************************/
						dao.CommitTrans();
                        //手工回标，直接置为已开标
                        BiddingBFL.SetEmit_State(Request["BiddingEmitCode"] + "", 1);
                        Response.Write("<script>window.opener.location.reload(true)</script>");
                       // Response.Write("<script>window.close()</script>");
						
						/*Response.Write(Rms.Web.JavaScript.ScriptStart);
						Response.Write(Rms.Web.JavaScript.OpenerReload(false));
						Response.Write(Rms.Web.JavaScript.WinClose(false));
						Response.Write(Rms.Web.JavaScript.ScriptEnd);*/
					}
					catch(Exception ex)
					{
						dao.RollBackTrans();
						Response.Write(Rms.Web.JavaScript.Alert(true,"请填写回标单位日期！"));
						//throw ex;
					}
					finally
					{
					}
				}
			}
			/*******************************************************************/
		}
	}
}
