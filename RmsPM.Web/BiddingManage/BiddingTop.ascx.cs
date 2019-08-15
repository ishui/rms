namespace RmsPM.Web.BiddingManage
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		BiddingTop ��ժҪ˵����
	/// </summary>
	public partial class BiddingTop : System.Web.UI.UserControl
	{
		/// <summary>
		/// ҵ�����
		/// </summary>
		private string _BiddingCode = "";
        private string _Title;
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
		public string Title
		{
			get
			{
                BLL.Bidding cBidding = new BLL.Bidding();
                cBidding.BiddingCode = this.BiddingCode;
                this._Title = cBidding.Title;
                //this.tdBiddingTitle.InnerHtml = cBidding.Title;
				return _Title;
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
		/// ��ͬ���
		/// </summary>
		public string ContractNember
		{
			get
			{
				return this.tdContractNember.InnerHtml;
			}
			set
			{
				this.tdContractNember.InnerHtml = value;
			}
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
		}
		public void InitControl()
		{
            
			BLL.Bidding cBidding = new BLL.Bidding();
			cBidding.BiddingCode = this.BiddingCode;
            string LinkUrl = "<a onclick=OpenLargeWindow('biddingmodify.aspx?BiddingCode=" + this.BiddingCode + "&State=edit&ProjectCode=" + cBidding.ProjectCode + "')>" + cBidding.Title + "</a>";
			//this.tdBiddingTitle.InnerHtml = cBidding.Title;
            this.tdBiddingTitle.InnerHtml = LinkUrl;
			this.tdProjectName.InnerHtml = BLL.ProjectRule.GetProjectName(cBidding.ProjectCode);
            
			this.ViewState["Money"] = cBidding.Money;
			this.ViewState["mostly"] = cBidding.Accessory;
			this.ViewState["BiddingType"] = cBidding.Type;
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
