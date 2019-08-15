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
	/// BiddingReturnModify1 ��ժҪ˵����
	/// </summary>
	public partial class BiddingReturnModifyPage : PageBase
	{

	
		/// ****************************************************************************
		/// <summary>
		/// ҳ�����
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
		/// ��ʼ��
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
				Lb_Title.Text="�� ��";
			}

			BiddingEmitModify1.InitControl();
			BiddingReturnModify1.InitControl();
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

		protected void btnSave_ServerClick(object sender, System.EventArgs e)
		{
			if(BiddingReturnModify1.BiddingReturnCheck())
			{
				this.RegisterStartupScript("CheckAlert","<script>alert('�ر굥λ�������ڱ���ͬʱ��д��');</script>");
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
                        //�ֹ��رֱ꣬����Ϊ�ѿ���
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
						Response.Write(Rms.Web.JavaScript.Alert(true,"����д�ر굥λ���ڣ�"));
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
