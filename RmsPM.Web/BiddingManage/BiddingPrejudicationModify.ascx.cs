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
	///		BiddingPrejudicationModify ��ժҪ˵����
	/// </summary>
	public partial class BiddingPrejudicationModify : BiddingControlBase
	{

		/// <summary>
		/// ҵ�����
		/// </summary>
		private string _BiddingCode = "";

		/// <summary>
		/// ҵ�����
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
		/// �ⶨ���
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
		/// ����1
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
		/// ����2
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
		/// ����3
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
		/// �������
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
		/// �����ʼ��
		/// </summary>
		/// ****************************************************************************
		public void InitControl()
		{
			if(this.State == ModuleState.Sightless)//���ɼ���
			{
				this.Visible = false;
			}
			else if(this.State == ModuleState.Operable)//�ɲ�����
			{
				LoadData(true);
				EyeableDiv.Visible = false;
				OperableDiv.Visible = true;
			}
			else if(this.State == ModuleState.Eyeable)//�ɼ���
			{
				LoadData(false);
				OperableDiv.Visible = false;
				EyeableDiv.Visible = true;
			}
			else if(this.State == ModuleState.Begin)//�ɼ���
			{
				LoadData(false);
				OperableDiv.Visible = false;
				EyeableDiv.Visible = true;
			}
			else if(this.State == ModuleState.End)//�ɼ���
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
		/// ���ݼ���
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
					//this.EditSupplier.InnerHtml = "<a href=\"#\" onclick=\"javascript:BiddingPrejudicationOpenSupplierPage('"+this.ApplicationCode+"','edit','"+SelectState+"');return false\">�μ��ʸ�Ԥ��ĵ�λ����</a>";
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
					//this.ViewSupplier.InnerHtml = "<a href=\"#\" onclick=\"javascript:BiddingPrejudicationOpenSupplierPage('"+this.ApplicationCode+"','view','"+SelectState+"');return false\">�μ��ʸ�Ԥ��ĵ�λ����</a>";
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
			//this.ViewSupplier.InnerHtml = "<a href=\"#\" onclick=\"javascript:BiddingPrejudicationOpenSupplierPage('"+this.ViewState["tempCode"].ToString()+"','view','"+SelectState+"');return false\">�μ��ʸ�Ԥ��ĵ�λ����</a>";
			//this.EditSupplier.InnerHtml = "<a href=\"#\" onclick=\"javascript:BiddingPrejudicationOpenSupplierPage('"+this.ViewState["tempCode"].ToString()+"','edit','"+SelectState+"');return false\">�μ��ʸ�Ԥ��ĵ�λ����</a>";
		}

		/// ****************************************************************************
		/// <summary>
		/// �ύ����
		/// </summary>
		/// ****************************************************************************
		public void SubmitData()
		{
			if(txtNumber.Value=="")
			{
				//Response.Write(Rms.Web.JavaScript.Alert(true,"��ͬ��Ų���Ϊ��"));
				throw new Exception("��Ų���Ϊ��");
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
		///		�����֧������ķ��� - ��Ҫʹ�ô���༭��
		///		�޸Ĵ˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion

		#region �������г�ʼ��,���������ݵ�
		/// <summary>
		/// �������г�ʼ��,������ ������� Code
		/// </summary>
		/// <param name="WorkFlowToolbar1"></param>
		/// <returns>BiddingPrejudicationCode</returns>
		public string InitOnWorkFlow(RmsPM.Web.WorkFlowControl.WorkFlowToolbar WorkFlowToolbar1)
		{
			ApplicationCode = WorkFlowToolbar1.ApplicationCode;
			State = WorkFlowToolbar1.GetModuleState("�����");
			State1 = WorkFlowToolbar1.GetModuleState("SupplierSelect");
			UserCode = ((User)Session["User"]).UserCode;
			BiddingCode = Request["BiddingCode"]+"";
			InitControl();

			
			if(this.State == ModuleState.Operable)
			{
				WorkFlowToolbar1.ScriptCheck = "javascript:if(BiddingPrejudicationCheckSubmit()) ";
			}		

			//*** UCBiddingSupplierList(�μ��ʸ�Ԥ��ĵ�λ����) �ؼ���ʼ�� **************************************************************************
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
			SetAttachList1=WorkFlowToolbar1.GetModuleState("����1");
			SetAttachList2=WorkFlowToolbar1.GetModuleState("����2");
			SetAttachList3=WorkFlowToolbar1.GetModuleState("����3");

		}
		#endregion
		
	}
}

