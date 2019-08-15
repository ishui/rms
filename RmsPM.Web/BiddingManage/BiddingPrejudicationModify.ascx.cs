namespace RmsPM.Web.BiddingManage
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using RmsPM.Web.WorkFlowControl;
	using Rms.ORMap;

	/// <summary>
	///		BiddingPrejudicationModify 的摘要说明。
	/// </summary>
	public partial class BiddingPrejudicationModify : BiddingControlBase
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
		/// 拟定标段
		/// </summary>
		public string BiddingTitle
		{
			get
			{
				return this.ViewState["BiddingTitle"].ToString();
			}
			set
			{
				this.ViewState["BiddingTitle"] = value;
			}
		}
		public string tempCode
		{
			get
			{
				if(this.ViewState["tempCode"] != null)
					return this.ViewState["tempCode"].ToString();
				return "";
			}
			set
			{
				this.ViewState["tempCode"] = value;
			}
		}
        /// <summary>
        /// DepartMentCode
        /// </summary>
        public string DepartMentCode
        {
            get
            {
                if (this.ViewState["DepartMentCode"] != null)
                    return this.ViewState["DepartMentCode"].ToString();
                return "";
            }
            set
            {
                this.ViewState["DepartMentCode"] = value;
            }
        }
		public bool SelectState
		{
			get
			{
				if(this.ViewState["SelectState"] != null)
					return (bool)this.ViewState["SelectState"];
				return false;
			}
			set
			{
				this.ViewState["SelectState"] = value;
			}
		}
		public bool EditState
		{
			get
			{
				if(this.ViewState["EditState"] != null)
					return (bool)this.ViewState["EditState"];
				return false;
			}
			set
			{
				this.ViewState["EditState"] = value;
			}
		}
		public string Money
		{
			get
			{
				return this.ViewState["Money"].ToString();
			}
		}
		public string mostly
		{
			get
			{
				return this.ViewState["mostly"].ToString();
			}
		}
		public string BiddingType
		{
			get
			{
				return this.ViewState["BiddingType"].ToString();
			}
		}
		/// <summary>
		/// 附件1
		/// </summary>
		public ModuleState SetAttachList1
		{
			set
			{
				if(value== ModuleState.Sightless)
				{
					AttachMentList1.Visible=false;
				}				
			}			
		}
		/// <summary>
		/// 附件2
		/// </summary>
		public ModuleState SetAttachList2
		{
			set
			{
				if(value== ModuleState.Sightless)
				{
					AttachMentList2.Visible=false;
				}				
			}			
		}
		/// <summary>
		/// 附件3
		/// </summary>
		public ModuleState SetAttachList3
		{
			set
			{
				if(value== ModuleState.Sightless)
				{
					AttachMentList3.Visible=false;
				}				
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
			AttachMentAdd1.AttachMentType = "BiddingPrejudication1";
			AttachMentList1.AttachMentType = "BiddingPrejudication1";
			AttachMentAdd2.AttachMentType = "BiddingPrejudication2";
			AttachMentList2.AttachMentType = "BiddingPrejudication2";
			AttachMentAdd3.AttachMentType = "BiddingPrejudication3";
			AttachMentList3.AttachMentType = "BiddingPrejudication3";

			if(this.ApplicationCode != "")
			{
				AttachMentAdd1.MasterCode = this.ApplicationCode;
				AttachMentList1.MasterCode = this.ApplicationCode;
				AttachMentAdd2.MasterCode = this.ApplicationCode;
				AttachMentList2.MasterCode = this.ApplicationCode;
				AttachMentAdd3.MasterCode = this.ApplicationCode;
				AttachMentList3.MasterCode = this.ApplicationCode;
			}
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
			if(this.State1 == ModuleState.Operable)
				SelectState = true;

			if(this.ApplicationCode != "")
			{
				BLL.BiddingPrejudication cBiddingPrejudication = new BLL.BiddingPrejudication();
				cBiddingPrejudication.BiddingPrejudicationCode = this.ApplicationCode;
				BLL.Bidding cBidding = new BLL.Bidding();
				cBidding.BiddingCode = cBiddingPrejudication.BiddingCode;

				if( Flag )
				{
					this.txtProjectName.InnerHtml = BLL.ProjectRule.GetProjectName(cBidding.ProjectCode)+"&nbsp; ";
                    string LinkUrl = "<a onclick=OpenLargeWindow('biddingmodify.aspx?BiddingCode=" + cBidding.BiddingCode + "&State=edit&ProjectCode=" + cBidding.ProjectCode + "')>" + cBidding.Title + "</a>";
                    //this.txtBiddingTitle.InnerHtml = cBidding.Title+"&nbsp; ";
                    this.txtBiddingTitle.InnerHtml = LinkUrl;
					this.txtEmitDate.InnerHtml = cBidding.EmitDate+"&nbsp; ";

					this.txtNumber.Value = cBiddingPrejudication.Number;
					this.BiddingCode = cBiddingPrejudication.BiddingCode;
					//this.EditSupplier.InnerHtml = "<a href=\"#\" onclick=\"javascript:BiddingPrejudicationOpenSupplierPage('"+this.ApplicationCode+"','edit','"+SelectState+"');return false\">参加资格预审的单位名单</a>";
					this.EditState = true;
					this.ViewState["BiddingTitle"] = cBidding.Title;
                    this.DepartMentCode = cBidding.BiddingRemark1;
				}
				else
				{
					this.tdProjectName.InnerHtml = BLL.ProjectRule.GetProjectName(cBidding.ProjectCode)+"&nbsp; ";
                    string LinkUrl = "<a onclick=OpenLargeWindow('biddingmodify.aspx?BiddingCode=" + cBidding.BiddingCode + "&State=edit&ProjectCode=" + cBidding.ProjectCode + "')>" + cBidding.Title + "</a>";
                    //this.tdBiddingTitle.InnerHtml = cBidding.Title+"&nbsp; ";
                    this.tdBiddingTitle.InnerHtml = LinkUrl;
					this.tdEmitDate.InnerHtml = cBidding.EmitDate+"&nbsp; ";
					
					this.tdNumber.InnerHtml = cBiddingPrejudication.Number+"&nbsp; ";
					this.BiddingCode = cBiddingPrejudication.BiddingCode;
					//this.ViewSupplier.InnerHtml = "<a href=\"#\" onclick=\"javascript:BiddingPrejudicationOpenSupplierPage('"+this.ApplicationCode+"','view','"+SelectState+"');return false\">参加资格预审的单位名单</a>";
					this.EditState = false;
					this.ViewState["BiddingTitle"] = cBidding.Title;
                    this.DepartMentCode = cBidding.BiddingRemark1;
				}
			}
			else
			{
				if(Flag)
				{
					BLL.Bidding cBidding = new BLL.Bidding();
					cBidding.BiddingCode = this.BiddingCode;
					this.txtProjectName.InnerHtml = BLL.ProjectRule.GetProjectName(cBidding.ProjectCode)+"&nbsp; ";
                    string LinkUrl = "<a onclick=OpenLargeWindow('biddingmodify.aspx?BiddingCode=" + this.BiddingCode + "&State=edit&ProjectCode=" + cBidding.ProjectCode + "')>" + cBidding.Title + "</a>";
                    //this.txtBiddingTitle.InnerHtml = cBidding.Title+"&nbsp; ";
                    this.txtBiddingTitle.InnerHtml = LinkUrl;
					this.txtEmitDate.InnerHtml = cBidding.EmitDate+"&nbsp; ";
					this.ViewState["BiddingTitle"] = cBidding.Title;
					this.EditState = true;
				}
				this.ViewState["tempCode"] = this.UserCode+DateTime.Now.ToString();
			}
			BLL.Bidding ccBidding = new BLL.Bidding();
			ccBidding.BiddingCode = this.BiddingCode;
			this.ViewState["Money"] = ccBidding.Money;
			this.ViewState["mostly"] = ccBidding.Accessory;
			this.ViewState["BiddingType"] = ccBidding.Type;
			this.ProjectCode = ccBidding.ProjectCode;
			//this.ViewSupplier.InnerHtml = "<a href=\"#\" onclick=\"javascript:BiddingPrejudicationOpenSupplierPage('"+this.ViewState["tempCode"].ToString()+"','view','"+SelectState+"');return false\">参加资格预审的单位名单</a>";
			//this.EditSupplier.InnerHtml = "<a href=\"#\" onclick=\"javascript:BiddingPrejudicationOpenSupplierPage('"+this.ViewState["tempCode"].ToString()+"','edit','"+SelectState+"');return false\">参加资格预审的单位名单</a>";
		}

		/// ****************************************************************************
		/// <summary>
		/// 提交数据
		/// </summary>
		/// ****************************************************************************
		public void SubmitData()
		{
			if(txtNumber.Value=="")
			{
				//Response.Write(Rms.Web.JavaScript.Alert(true,"合同编号不能为空"));
				throw new Exception("编号不能为空");
			}
			BLL.BiddingPrejudication cBiddingPrejudication = new BLL.BiddingPrejudication();
			cBiddingPrejudication.BiddingPrejudicationCode = this.ApplicationCode;
			cBiddingPrejudication.BiddingCode = this.BiddingCode;
			cBiddingPrejudication.Number = this.txtNumber.Value;
			cBiddingPrejudication.UserCode = this.UserCode;
			cBiddingPrejudication.CreateDate = DateTime.Now.ToShortDateString();
			cBiddingPrejudication.State = "";
			cBiddingPrejudication.Flag = "";
			cBiddingPrejudication.dao = this.dao;
			cBiddingPrejudication.BiddingPrejudicationSubmit();

			if(this.ApplicationCode == "")
			{
				BLL.BiddingSupplier cBiddingSupplier = new BLL.BiddingSupplier();
				cBiddingSupplier.BiddingPrejudicationCode = this.ViewState["tempCode"].ToString();
				cBiddingSupplier.dao = dao;
				EntityData entity = cBiddingSupplier._GetBiddingSuppliers();
				for(int i=0;i<entity.CurrentTable.Rows.Count;i++)
				{
					entity.CurrentTable.Rows[i]["BiddingPrejudicationCode"] = cBiddingPrejudication.BiddingPrejudicationCode;
				}
				dao.SubmitEntity(entity);
				this.ApplicationCode = cBiddingPrejudication.BiddingPrejudicationCode;
				this.AttachMentAdd1.SaveAttachMent(this.ApplicationCode);
				this.AttachMentAdd2.SaveAttachMent(this.ApplicationCode);
				this.AttachMentAdd3.SaveAttachMent(this.ApplicationCode);
				BLL.BiddingSystem.UpDataPrejudicationCode(cBiddingPrejudication.BiddingPrejudicationCode,this.ViewState["tempCode"].ToString());
			}
		}

		public void SubmitBiddingState()
		{
			BLL.BiddingPrejudication cBiddingPrejudication = new BLL.BiddingPrejudication();
			cBiddingPrejudication.BiddingPrejudicationCode = this.ApplicationCode;
			cBiddingPrejudication.dao = this.dao;

			BLL.Bidding bidding = new BLL.Bidding();
			bidding.dao = this.dao;
			bidding.BiddingCode = cBiddingPrejudication.BiddingCode;
			bidding.State = "1";
			bidding.BiddingSubmit();
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

		#region 在流程中初始化,及保存数据等
		/// <summary>
		/// 在流程中初始化,并返回 审批表的 Code
		/// </summary>
		/// <param name="WorkFlowToolbar1"></param>
		/// <returns>BiddingPrejudicationCode</returns>
		public string InitOnWorkFlow(RmsPM.Web.WorkFlowControl.WorkFlowToolbar WorkFlowToolbar1)
		{
			ApplicationCode = WorkFlowToolbar1.ApplicationCode;
			State = WorkFlowToolbar1.GetModuleState("申请表");
			State1 = WorkFlowToolbar1.GetModuleState("SupplierSelect");
			UserCode = ((User)Session["User"]).UserCode;
			BiddingCode = Request["BiddingCode"]+"";
			InitControl();

			
			if(this.State == ModuleState.Operable)
			{
				WorkFlowToolbar1.ScriptCheck = "javascript:if(BiddingPrejudicationCheckSubmit()) ";
			}		

			//*** UCBiddingSupplierList(参加资格预审的单位名单) 控件初始化 **************************************************************************
			string BiddingPrejudicationCode = "";

			if(ApplicationCode == "")
				BiddingPrejudicationCode = tempCode;
			else
				BiddingPrejudicationCode = ApplicationCode;
			StateControlForWorkFlow(WorkFlowToolbar1);
			return BiddingPrejudicationCode;
		}
		protected void StateControlForWorkFlow(RmsPM.Web.WorkFlowControl.WorkFlowToolbar WorkFlowToolbar1)
		{
			SetAttachList1=WorkFlowToolbar1.GetModuleState("附件1");
			SetAttachList2=WorkFlowToolbar1.GetModuleState("附件2");
			SetAttachList3=WorkFlowToolbar1.GetModuleState("附件3");

		}
		#endregion
		
	}
}

