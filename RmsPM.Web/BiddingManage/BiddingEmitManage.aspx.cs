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
using Rms.ORMap;
using RmsPM.BFL;
using System.Configuration;

namespace RmsPM.Web.BiddingManage
{
	/// <summary>
	/// BiddingEmitManage 的摘要说明。
	/// </summary>
	public partial class BiddingEmitManage : PageBase
	{
		protected string _biddingEmitTitle;
	
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

		}
		#region 公有属性
		public string BiddingCode
		{
			get
			{
				return Request["BiddingCode"]+"";
			}
		}
		public string BiddingEmitTitle
		{
			get
			{
				return _biddingEmitTitle;
			}
			set
			{
				_biddingEmitTitle=value;
			}
		}
		public string BiddingEmitCode
		{
			get
			{
				return Request["BiddingEmitCode"]+"";
			}
		}
		#endregion
		/// ****************************************************************************
		/// <summary>
		/// 初始化
		/// </summary>
		/// ****************************************************************************
		private void InitPage()
		{

            if (Request["State"] + "" == "edit")
			{
				string ApplicationCode = Request["ApplicationCode"]+"";
				string BiddingCode = Request["BiddingCode"]+"";

				this.BiddingEmitModify1.ApplicationCode = ApplicationCode;
				this.BiddingEmitModify1.BiddingCode = BiddingCode;
				this.BiddingReturnModify1.BiddingEmitCode = ApplicationCode;
				this.BiddingReturnModify1.BiddingCode = BiddingCode;
                this.BiddingReturnModify1.IsWSZTB=this.user.HasRight("210301");
				
				BiddingEmitModify1.State = WorkFlowControl.ModuleState.Operable;
				BiddingReturnModify1.State = WorkFlowControl.ModuleState.Eyeable;

				this.btnSave.Visible = true;
                this.btnReSNandPWD.Visible = false;
				if(ApplicationCode != "")
				{
					this.btnDel.Visible = true;
				}
				else
				{
					this.btnDel.Visible = false;
				}
				BiddingEmitModify1.InitControl();
				BiddingReturnModify1.InitControl();
                BiddingDtlModify1.ApplicationCode = BiddingCode;
                BiddingDtlModify1.State = WorkFlowControl.ModuleState.Eyeable;
                if (this.user.HasRight("2106"))
                {
                    BiddingDtlModify1.PriceState = WorkFlowControl.ModuleState.Operable;
                }

                BiddingDtlModify1.InitControl();
				SetBiddingEmitManage();
			}
			else if(Request["State"]+"" == "view")
			{
				
				/*BiddingEmitModify1.State = WorkFlowControl.ModuleState.Eyeable;
				BiddingReturnModify1.State = WorkFlowControl.ModuleState.Other;
				BiddingReturnModify1.BiddingEmitCode=ApplicationCode;*/
                string BiddingEmitCode = Request["BiddingEmitCode"] + "";
				
				this.BiddingEmitModify1.ApplicationCode = BiddingEmitCode;
				BiddingEmitModify1.State = WorkFlowControl.ModuleState.Eyeable;
				BiddingReturnModify1.State = WorkFlowControl.ModuleState.Other;
                this.BiddingReturnModify1.IsWSZTB = this.user.HasRight("210301");
				BiddingReturnModify1.IsReturnView = false;
				BiddingReturnModify1.BiddingEmitCode = BiddingEmitCode;
				this.btnSave.Visible = false;
                this.btnReSNandPWD.Visible = true;
				this.btnDel.Visible = false;
				BiddingEmitModify1.InitControl();
				BiddingReturnModify1.InitControl();
                BiddingDtlModify1.ApplicationCode = BiddingEmitModify1.BiddingCode;
                BiddingDtlModify1.State = WorkFlowControl.ModuleState.Eyeable;
                BiddingDtlModify1.BiddingEmitCode = BiddingEmitCode;
                BiddingDtlModify1.InitControl();

                //判断是否能修改
                BLL.BiddingEmit bidEmit =new BLL.BiddingEmit();
                bidEmit.BiddingEmitCode =BiddingEmitCode;
                if (bidEmit.CreatUser == this.user.UserCode)
                {
                    this.btnChange.Visible = true;
                }
			}
			if(Request["NowState"]+""=="5")
			{
				Lb_Title.Text="压 价";
			}

            this.btnSave.Attributes["OnClick"] = "javascript:if(BiddingEmitCheckSubmit()) ";
            BiddingReturnModify1.Show_ttachMentAdd2(BiddingEmitModify1.ApplicationCode);
            btnOpen.Visible = BiddingBFL.CanOpenNow(this.BiddingReturnModify1.BiddingEmitCode, this.user.UserCode);

		}
		/// <summary>
		/// 设置发标信息及其编号
		/// </summary>
		private void SetBiddingEmitManage()
		{
			BLL.BiddingPrejudication bjd = new RmsPM.BLL.BiddingPrejudication();
			bjd.BiddingCode = BiddingCode;
			DataTable dt = bjd.GetBiddingPrejudications();
            if (dt.Rows.Count > 1)
            {
                DataRow dr = dt.Rows[dt.Rows.Count - 1];
                string bas = dr["Number"].ToString();//读取审合表的ID

                BLL.BiddingEmit be = new RmsPM.BLL.BiddingEmit();
                be.BiddingCode = BiddingCode;
                dt = be.GetBiddingEmits();
                int emitCount = dt.Rows.Count + 1; //发标次数	
                _biddingEmitTitle = emitCount.ToString(); //发标次数
                BiddingEmitModify1.AllowEmitNumber = true;
                BiddingEmitModify1.EmitNumber = bas + emitCount.ToString();// + emitCount.ToString();//string.Format("{0:dd}",emitCount);
            }
            else 
            {
                BLL.BiddingEmit be = new RmsPM.BLL.BiddingEmit();
                be.BiddingCode = BiddingCode;
                dt = be.GetBiddingEmits();
                int emitCount = dt.Rows.Count + 1; //发标次数	
                _biddingEmitTitle = emitCount.ToString(); //发标次数
                BiddingEmitModify1.AllowEmitNumber = true;
                BiddingEmitModify1.EmitNumber = emitCount.ToString();// + emitCount.ToString();//string.Format("{0:dd}",emitCount);
            }
			
			//_biddingEmitTitle = 
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

		/// ****************************************************************************
		/// <summary>
		/// 保存按钮事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// ****************************************************************************
		protected void btnSave_ServerClick(object sender, System.EventArgs e)
		{
			/****************************************************************************/
			using(StandardEntityDAO dao=new StandardEntityDAO("Leave"))
			{
				//dao.BeginTrans();
				try
				{
                    
					this.BiddingEmitModify1.dao = dao;
					this.BiddingEmitModify1.SubmitData();

					this.BiddingReturnModify1.dao = dao;
					this.BiddingReturnModify1.BiddingEmitCode = this.BiddingEmitModify1.ApplicationCode;
					this.BiddingReturnModify1.SubmitData();


                    /*******************************************************/
					//dao.CommitTrans();
                    //增加备注
                    BiddingReturnModify1.UpdateRemark();
                }
				catch(Exception ex)
				{
					//dao.RollBackTrans();

					throw ex;
				}
				finally
				{
					//dao.Dispose();
				}
                InitPage();
			}
			/*******************************************************************/

			//Response.Write(Rms.Web.JavaScript.ScriptStart);
			//Response.Write(Rms.Web.JavaScript.OpenerReload(false));
			Response.Write("<script>window.opener.location.reload(true)</script>");
			Response.Write("<script>window.close()</script>");
			//Response.Write(Rms.Web.JavaScript.WinClose(false));
			//Response.Write(Rms.Web.JavaScript.ScriptEnd);
		}
        /// <summary>
        /// 开标按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOpen_ServerClick(object sender, System.EventArgs e)
        {
            BiddingBFL.BiddingOpen(this.BiddingReturnModify1.BiddingEmitCode, this.user.UserCode);
            InitPage();
            
        }

        protected void btnReSNandPWD_ServerClick(object sender, System.EventArgs e)
        {
            BiddingBFL.Emit_LastSend(this.BiddingReturnModify1.BiddingEmitCode, Server.MapPath(ConfigurationManager.AppSettings["VirtualDirectory"].ToString()) + @"\EmailTemplate.xml");
        }

		/// ****************************************************************************
		/// <summary>
		/// 删除按钮事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// ****************************************************************************
		protected void btnDel_ServerClick(object sender, System.EventArgs e)
		{
			/****************************************************************************/
			using(StandardEntityDAO dao=new StandardEntityDAO("Leave"))
			{
				dao.BeginTrans();
				try
				{
					this.BiddingEmitModify1.dao = dao;
					this.BiddingEmitModify1.Delete();
					this.BiddingReturnModify1.dao = dao;
					this.BiddingReturnModify1.Delete();

					/*******************************************************/
					dao.CommitTrans();
				}
				catch(Exception ex)
				{
					dao.RollBackTrans();
					throw ex;
				}
			}
			/*******************************************************************/

			Response.Write(Rms.Web.JavaScript.ScriptStart);
			Response.Write(Rms.Web.JavaScript.OpenerReload(false));
			Response.Write(Rms.Web.JavaScript.WinClose(false));
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
		}

		protected void btnClose_ServerClick(object sender, System.EventArgs e)
		{
		
		}
	}
}
