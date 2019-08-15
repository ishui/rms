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
	///		BiddingMessageModify ��ժҪ˵����
	/// </summary>
	public partial class BiddingMessageModify : BiddingControlBase
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
        /// 
        /// </summary>
        public string MaxMoney
        {
            get
            {
                if (this.ViewState["MaxMoney"] == null)
                    return "0";
                return this.ViewState["MaxMoney"].ToString();
            }
        }
		/// <summary>
		/// ҵ�����
		/// </summary>
		private string _SupplierCode = "";

		/// <summary>
		/// ҵ�����
		/// </summary>
		public string SupplierCode
		{
			get
			{
				if ( _SupplierCode == "" )
				{
					if(this.ViewState["_SupplierCode"] != null)
						return this.ViewState["_SupplierCode"].ToString();
					return "";
				}
				return _SupplierCode;
			}
			set
			{
				_SupplierCode = value;
				this.ViewState["_SupplierCode"] = value;
			}
		}
		public string ContractName
		{
			get
			{
				return this.txtContractName.Value;
			}
			set
			{
				this.txtContractName.Value = value;
			}
		}
		public bool AllowContractName
		{
			get
			{
				return txtContractNember.Disabled;
			}
			set
			{
				txtContractName.Disabled = value;
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
		/// ���ú�ͬ���
		/// </summary>
		public string ContractNember
		{
			get
			{
				return this.txtContractNember.Value;
			}
			set
			{
				this.txtContractNember.Value = value;
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public ModuleState SetAttachList
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
        /// ���쿴״̬
        /// </summary>
        public ModuleState MoneyState
        {
            set
            {
                this.ViewState["MoneyState"] = value;
            }
            get
            {
                if(this.ViewState["MoneyState"] == null)
                    return ModuleState.Unbeknown;
                return (ModuleState)this.ViewState["MoneyState"];
 
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
				// �ڴ˴������û������Գ�ʼ��ҳ��
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
            this.ViewState["BiddingReturnCodeStr"] = "";
			if(this.ApplicationCode != "")
			{
				BLL.BiddingMessage cBiddingMessage = new BLL.BiddingMessage();
				cBiddingMessage.BiddingMessageCode = this.ApplicationCode;
				this.BiddingCode = cBiddingMessage.BiddingCode;
				this.ProjectCode = cBiddingMessage.ProjectCode;
				this.SupplierCode = cBiddingMessage.Supplier;
				this.ProjectCode = cBiddingMessage.ProjectCode;

                BLL.Bidding bidding = new BLL.Bidding();
                bidding.BiddingCode = cBiddingMessage.BiddingCode;
                this.ViewState["Money"] = bidding.Money;
                this.ViewState["mostly"] = bidding.Accessory;
                this.ViewState["BiddingType"] = bidding.Type;

				if( Flag )
				{
					this.txtProjectCode.InnerHtml = "&nbsp;&nbsp;&nbsp;&nbsp;"+BLL.ProjectRule.GetProjectName(cBiddingMessage.ProjectCode);
					this.txtContractNember.Value = cBiddingMessage.ContractNember;
					this.txtContractName.Value = cBiddingMessage.ContractName;
					this.txtContractType.InnerHtml = "&nbsp;&nbsp;&nbsp;&nbsp;"+BLL.ContractRule.GetContractTypeName(cBiddingMessage.ContractType);
					//this.txtSupplier.InnerHtml = "&nbsp;&nbsp;&nbsp;&nbsp;"+BLL.ProjectRule.GetSupplierName(cBiddingMessage.Supplier);
					this.txtContractDate.Value = cBiddingMessage.ContractDate;
					this.txtRemark.Value = cBiddingMessage.Remark;

                    
                    DataTable dt = bidding.GetBiddingReturnNoMessage();
                    foreach (DataRow dr in dt.Select())
                    {
                        ListItem li = new ListItem(BLL.ProjectRule.GetSupplierName(dr["SupplierCode"].ToString()), dr["SupplierCode"].ToString());
                        if (!DropSupplier.Items.Contains(li))
                            this.DropSupplier.Items.Add(li);
                    }
                    ListItem lis = new ListItem(BLL.ProjectRule.GetSupplierName(cBiddingMessage.Supplier), cBiddingMessage.Supplier);
                    this.DropSupplier.Items.Add(lis);
                    this.DropSupplier.SelectedIndex = this.DropSupplier.Items.IndexOf(this.DropSupplier.Items.FindByValue(cBiddingMessage.Supplier));
                    BoundBiddingDtl(cBiddingMessage.BiddingReturnCode);
                    this.ViewState["BiddingReturnCodeStr"] = cBiddingMessage.BiddingReturnCode;
				}
				else
				{
					this.tdProjectCode.InnerHtml = "&nbsp;&nbsp;&nbsp;&nbsp;"+BLL.ProjectRule.GetProjectName(cBiddingMessage.ProjectCode);
					this.tdContractNember.InnerHtml = "&nbsp;&nbsp;&nbsp;&nbsp;"+cBiddingMessage.ContractNember;
					this.tdContractName.InnerHtml = "&nbsp;&nbsp;&nbsp;&nbsp;"+cBiddingMessage.ContractName;
					this.tdContractType.InnerHtml = "&nbsp;&nbsp;&nbsp;&nbsp;"+BLL.ContractRule.GetContractTypeName(cBiddingMessage.ContractType);
					this.tdSupplier.InnerHtml = "&nbsp;&nbsp;&nbsp;&nbsp;"+BLL.ProjectRule.GetSupplierName(cBiddingMessage.Supplier);
                    this.tdBiddingDtl.InnerHtml = this.GetBiddingDtlListStr(cBiddingMessage.BiddingReturnCode,cBiddingMessage.Supplier);
					this.tdContractDate.InnerHtml = "&nbsp;&nbsp;&nbsp;&nbsp;"+cBiddingMessage.ContractDate;
					this.tdRemark.InnerHtml = "&nbsp;&nbsp;&nbsp;&nbsp;"+cBiddingMessage.Remark.Replace("\n", "<br>");
				}
			}
			else
			{
				BLL.Bidding bidding = new BLL.Bidding();
				bidding.BiddingCode = this.BiddingCode;
                this.ViewState["Money"] = bidding.Money;
                this.ViewState["mostly"] = bidding.Accessory;
                this.ViewState["BiddingType"] = bidding.Type;
				this.txtProjectCode.InnerHtml = "&nbsp;&nbsp;&nbsp;&nbsp;"+BLL.ProjectRule.GetProjectName(bidding.ProjectCode);
				this.txtContractType.InnerHtml = "&nbsp;&nbsp;&nbsp;&nbsp;"+BLL.ContractRule.GetContractTypeName(bidding.Type);
				this.ProjectCode = bidding.ProjectCode;
                DataTable dt = bidding.GetBiddingReturnNoMessage();
                foreach (DataRow dr in dt.Select())
                {
                    ListItem li = new ListItem(BLL.ProjectRule.GetSupplierName(dr["SupplierCode"].ToString()), dr["SupplierCode"].ToString());
                    if (!DropSupplier.Items.Contains(li))
                        this.DropSupplier.Items.Add(li);
                }
                BoundBiddingDtl("");

				this.txtContractName.Value = bidding.Title;
			}
            //�������
            BLL.BiddingMessage biddingMessage = new BLL.BiddingMessage();
            biddingMessage.BiddingCode = this.BiddingCode;
            System.Data.DataTable BiddingMessagedt = biddingMessage.GetBiddingMessages() as System.Data.DataTable;
            string CNnum = "0";
            if (BiddingMessagedt!=null)
                CNnum = BiddingMessagedt.Rows.Count.ToString();

            BLL.BiddingPrejudication bp = new BLL.BiddingPrejudication();
            bp.BiddingCode = this.BiddingCode;
            DataTable dtp = bp.GetBiddingPrejudications();
            DataRow[] drw = dtp.Select("", "CreateDate desc");
            if(drw.Length>0)
                this.ContractNember = drw[0]["Number"].ToString() + "-" + CNnum;
            
		}
        private void BoundBiddingDtl(string BiddingReturnCodeStr)
        {
            this.CheckBoxList1.Items.Clear();
            BLL.Bidding bidding = new BLL.Bidding();
            bidding.BiddingCode = this.BiddingCode;
            DataTable dt = bidding.GetBiddingReturnNoMessage();
            foreach (DataRow dr in dt.Select("SupplierCode='"+DropSupplier.SelectedValue+"'"))
            {
                
                ListItem li = new ListItem(BLL.BiddingDtl.GetBiddingDtlNameByCode(dr["BiddingDtlCode"].ToString()),dr["BiddingReturnCode"].ToString());
                if (this.MoneyState == ModuleState.Eyeable)
                {
                    li.Text += " ���ۣ�"+dr["Money"].ToString();
                }
                this.CheckBoxList1.Items.Add(li);
            }
            dt =bidding.GetBiddingReturn();
            foreach (DataRow dr in dt.Select("BiddingReturnCode in (" + BiddingReturnCodeStr + "'') and flag='1' and SupplierCode='" + DropSupplier.SelectedValue + "'"))
            {
                BLL.BiddingReturn br = new BLL.BiddingReturn();
                br.BiddingReturnCode = dr["BiddingReturnCode"].ToString();
                ListItem li = new ListItem(BLL.BiddingDtl.GetBiddingDtlNameByCode(br.BiddingDtlCode), dr["BiddingReturnCode"].ToString());
                if (this.MoneyState == ModuleState.Eyeable)
                {
                    li.Text += " ���ۣ�"+dr["Money"].ToString();
                }
                li.Selected = true;
                this.CheckBoxList1.Items.Add(li);
             }
        }
        private string GetBiddingDtlListStr(string BiddingReturnCodeStr,string SupplierCode)
        {
            string returnstr = "";
            BLL.Bidding bidding = new BLL.Bidding();
            bidding.BiddingCode = this.BiddingCode;
            DataTable dt = bidding.GetBiddingReturn();
            foreach (DataRow dr in dt.Select("BiddingReturnCode in (" + BiddingReturnCodeStr + "'') and flag='1' and SupplierCode='" + SupplierCode + "'"))
            {
                BLL.BiddingReturn br = new BLL.BiddingReturn();
                br.BiddingReturnCode = dr["BiddingReturnCode"].ToString();
                if (this.MoneyState == ModuleState.Eyeable)
                {
                    returnstr += "&nbsp;&nbsp;&nbsp;&nbsp;" + BLL.BiddingDtl.GetBiddingDtlNameByCode(br.BiddingDtlCode) +" ���ۣ�"+br.Money+ "<br />";
                }
                else
                {
                    returnstr += "&nbsp;&nbsp;&nbsp;&nbsp;" + BLL.BiddingDtl.GetBiddingDtlNameByCode(br.BiddingDtlCode) + "<br />";
                }
            }
            if (returnstr == "")
                returnstr = "&nbsp;";
            return returnstr; 
        }

		/// ****************************************************************************
		/// <summary>
		/// �ύ����
		/// </summary>
		/// ****************************************************************************
		public void SubmitData()
		{
            string BiddingReturnCode = "";
            string BiddingDtlCode = "";
            decimal tempMoney = 0;
            foreach (ListItem li in this.CheckBoxList1.Items)
            {
                if (li.Selected)
                {
                    BLL.BiddingReturn br = new BLL.BiddingReturn();
                    br.BiddingReturnCode = li.Value;
                    BiddingReturnCode += "'"+br.BiddingReturnCode+"',";
                    BiddingDtlCode += "'"+br.BiddingDtlCode + "',";

                    if (System.Convert.ToDecimal(br.Money) > tempMoney)
                    {
                        this.ViewState["MaxMoney"] = br.Money;
                        tempMoney = System.Convert.ToDecimal(br.Money);
                    }
                }
            }
            
			BLL.Bidding bidding = new BLL.Bidding();
			bidding.dao = this.dao;
			bidding.BiddingCode = this.BiddingCode;

			BLL.BiddingMessage cBiddingMessage = new BLL.BiddingMessage();
			cBiddingMessage.BiddingMessageCode = this.ApplicationCode;
			cBiddingMessage.BiddingCode = this.BiddingCode;
			cBiddingMessage.ProjectCode = this.ProjectCode;
			cBiddingMessage.ContractNember = this.txtContractNember.Value;
			cBiddingMessage.ContractName = this.txtContractName.Value;
			cBiddingMessage.ContractType = bidding.Type;
			cBiddingMessage.Supplier = this.DropSupplier.SelectedValue;
			cBiddingMessage.ContractDate = this.txtContractDate.Value;
			cBiddingMessage.Remark = this.txtRemark.Value;
			cBiddingMessage.CreateDate = DateTime.Now.ToShortDateString();
			cBiddingMessage.CreateUser = "";
			cBiddingMessage.State = "0";
			cBiddingMessage.Flag = "0";
            cBiddingMessage.BiddingReturnCode = BiddingReturnCode;
            cBiddingMessage.BiddingDtlCode = BiddingDtlCode;
			cBiddingMessage.dao = this.dao;
			cBiddingMessage.BiddingMessageSubmit();

			if(this.ApplicationCode == "")
				this.ApplicationCode = cBiddingMessage.BiddingMessageCode;
		}
		public void LoadAttach()
		{
			AttachMentList1.AttachMentType="BiddingMessageModify";
			AttachMentList1.MasterCode=this.BiddingCode+"5";
			AttachMentAdd1.AttachMentType="BiddingMessageModify";
			AttachMentAdd1.MasterCode=this.BiddingCode+"5";
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
        protected void DropSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            BoundBiddingDtl(this.ViewState["BiddingReturnCodeStr"].ToString());
        }
}
}

