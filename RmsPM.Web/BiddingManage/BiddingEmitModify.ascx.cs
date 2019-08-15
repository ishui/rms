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
	///		BiddingEmitModify ��ժҪ˵����
	/// </summary>
	public partial class BiddingEmitModify : BiddingControlBase
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
		/// ���÷�����
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
		/// �������Ƿ������Լ��޸�
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
		/// �ύ����
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
		/// ɾ������
		/// </summary>
		/// ****************************************************************************
		public void Delete()
		{
			BLL.BiddingEmit cBiddingEmit = new BLL.BiddingEmit();
			cBiddingEmit.BiddingEmitCode = this.ApplicationCode;
			cBiddingEmit.dao = this.dao;
			cBiddingEmit.BiddingEmitDelete();
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
	}
}

