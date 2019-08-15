namespace RmsPM.Web.BiddingManage
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using RmsPM.Web.WorkFlowControl;

	/// <summary>
	///		BiddingEmitModify 的摘要说明。
	/// </summary>
	public partial class BiddingEmitModify : BiddingControlBase
	{

		/// <summary>
		/// 业务代码
		/// </summary>
		private string _BiddingCode = "";

		/// <summary>
		/// 业务代码
		/// </summary>
		public string BiddingCode
		{
			get
			{
				if ( _BiddingCode == "" )
				{
					if(this.ViewState["_BiddingCode"] != null)
						return this.ViewState["_BiddingCode"].ToString();
					return "";
				}
				return _BiddingCode;
			}
			set
			{
				_BiddingCode = value;
				this.ViewState["_BiddingCode"] = value;
			}
		}
		/// <summary>
		/// 设置发标编号
		/// </summary>
		public string EmitNumber
		{
			get
			{
				if(txtEmitNumber.Value!="")
				{
					return txtEmitNumber.Value;
				}
				else
				{
					return  tdEmitNumber.InnerHtml; 
				}
			}
			set
			{
				txtEmitNumber.Value = value;
			}
		}
		public string NowState
		{
			get
			{
				if(Request.QueryString["NowState"]=="5")
				{
					return "6";
				}
				else
				{
					return "2";
				}
			}
		}
		/// <summary>
		/// 发标编号是否允许自己修改
		/// </summary>
		public bool AllowEmitNumber
		{
			get
			{
				return this.txtEmitNumber.Disabled;
			}
			set
			{
				this.txtEmitNumber.Disabled = value;
			}
		}
			/// ****************************************************************************
			/// <summary>
			/// 组件加载
			/// </summary>
			/// <param name="sender"></param>
			/// <param name="e"></param>
			/// ****************************************************************************
			protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
		}

		/// ****************************************************************************
		/// <summary>
		/// 组件初始化
		/// </summary>
		/// ****************************************************************************
		public void InitControl()
		{
			if(this.State == ModuleState.Sightless)//不可见的
			{
				this.Visible = false;
			}
			else if(this.State == ModuleState.Operable)//可操作的
			{
				LoadData(true);
				EyeableDiv.Visible = false;
				OperableDiv.Visible = true;
			}
			else if(this.State == ModuleState.Eyeable)//可见的
			{
				LoadData(false);
				OperableDiv.Visible = false;
				EyeableDiv.Visible = true;
			}
			else if(this.State == ModuleState.Begin)//可见的
			{
				LoadData(false);
				OperableDiv.Visible = false;
				EyeableDiv.Visible = true;
			}
			else if(this.State == ModuleState.End)//可见的
			{
				LoadData(false);
				OperableDiv.Visible = false;
				EyeableDiv.Visible = true;
			}
			else
			{
				this.Visible = false;
			}
		}
		/// ****************************************************************************
		/// <summary>
		/// 数据加载
		/// </summary>
		/// ****************************************************************************
		private void LoadData(bool Flag)
		{
            if (this.ApplicationCode != "")
			{
				BLL.BiddingEmit cBiddingEmit = new BLL.BiddingEmit();
				cBiddingEmit.BiddingEmitCode = this.ApplicationCode;

				if( Flag )
				{
					this.txtEmitNumber.Value = cBiddingEmit.EmitNumber;
					this.txtEmitDate.Value = cBiddingEmit.EmitDate;
					this.txtEndDate.Value = cBiddingEmit.EndDate;
					this.txtPrejudicationDate.Value = cBiddingEmit.PrejudicationDate;
					this.BiddingCode = cBiddingEmit.BiddingCode;
				}
				else
				{
					this.tdEmitNumber.InnerHtml = cBiddingEmit.EmitNumber;
					this.tdEmitDate.InnerHtml = cBiddingEmit.EmitDate;
					this.tdEndDate.InnerHtml = cBiddingEmit.EndDate;
					this.tdPrejudicationDate.InnerHtml = cBiddingEmit.PrejudicationDate;
					this.BiddingCode = cBiddingEmit.BiddingCode;
				}
			}
			else
			{
				if(Flag)
				{
                    this.txtEmitDate.Value = DateTime.Now.ToString();
                }
			}
		}

		/// ****************************************************************************
		/// <summary>
		/// 提交数据
		/// </summary>
		/// ****************************************************************************
        public void SubmitData()
        {
            BLL.BiddingEmit cBiddingEmit = new BLL.BiddingEmit();
            cBiddingEmit.BiddingEmitCode = this.ApplicationCode;
            cBiddingEmit.BiddingCode = this.BiddingCode;
            cBiddingEmit.EmitNumber = this.txtEmitNumber.Value;
            if (this.txtEmitDate.Value != "")
                cBiddingEmit.EmitDate = this.txtEmitDate.Value;
            if (this.txtEndDate.Value != "")
                cBiddingEmit.EndDate = this.txtEndDate.Value;
            if (this.txtPrejudicationDate.Value != "")
                cBiddingEmit.PrejudicationDate = this.txtPrejudicationDate.Value;
            cBiddingEmit.CreatUser = ((User)Session["user"]).UserCode ;
            cBiddingEmit.CreatDate = DateTime.Now.ToString();
            cBiddingEmit.dao = this.dao;
            cBiddingEmit.BiddingEmitSubmit();

            BLL.Bidding bidding = new BLL.Bidding();
            bidding.BiddingCode = cBiddingEmit.BiddingCode;
            bidding.State = NowState;
            bidding.dao = dao;
            bidding.BiddingSubmit();

            if (this.ApplicationCode == "")
                this.ApplicationCode = cBiddingEmit.BiddingEmitCode;
            /*BLL.BiddingDtl bd = new BLL.BiddingDtl();
            bd.BiddingCode = this.BiddingCode;
            bd.flag = "1";
            DataTable dt = bd.GetBiddingDtls();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                BLL.BiddingEmitDtl bed = new BLL.BiddingEmitDtl();
                bed.BiddingDtlCode = dt.Rows[i]["BiddingDtlCode"].ToString();
                bed.BiddingEmitCode = this.ApplicationCode;
                bed.BiddingEmitDtlSubmit();
            }*/
        }
		/// ****************************************************************************
		/// <summary>
		/// 删除数据
		/// </summary>
		/// ****************************************************************************
		public void Delete()
		{
			BLL.BiddingEmit cBiddingEmit = new BLL.BiddingEmit();
			cBiddingEmit.BiddingEmitCode = this.ApplicationCode;
			cBiddingEmit.dao = this.dao;
			cBiddingEmit.BiddingEmitDelete();
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

