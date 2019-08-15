namespace RmsPM.Web.BiddingManage
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using RmsPM.BLL;
	using RmsPM.Web.WorkFlowControl;

	/// <summary>
	///		Control_BiddingEmitMoney ��ժҪ˵����
	/// </summary>
	public partial class Control_BiddingEmitMoney : BiddingControlBase
	{
		protected string _BiddingCode;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
		}
		#region ��������

		/// <summary>
		/// ��Ӧ�̱���
		/// </summary>
		public string PjMoney
		{
			get
			{
				return this.Lb_PjMoney.Text;
			}
			set
			{
				Lb_PjMoney.Text = value;
			}
		}
		/// <summary>
		/// �ݶ����
		/// </summary>
		public string ObMoney
		{
			get
			{
				return this.Lb_ObMoney.Text;
			}
			set
			{
				this.Lb_ObMoney.Text = value;
			}
		}
		/// <summary>
		/// ʵ�ʽ��
		/// </summary>
		public string FactMoney
		{
			get
			{
				return this.Lb_FactMoney.Text;
			}
			set
			{
				this.Lb_FactMoney.Text = value;
			}
		}
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
		#endregion
		#region ״̬����
		public void InitPage()
		{
			if(this.State == ModuleState.Eyeable)//�ɼ���
			{
				LoadData();
			}
			else
			{
				this.Visible=false;
			}
		}
		#endregion
		#region ��������
		private void LoadData()
		{
			BLL.Bidding bidding = new RmsPM.BLL.Bidding();
			bidding.BiddingCode = this.BiddingCode;
			this.Lb_ObMoney.Text = String.Format("{0:N}",Convert.ToDecimal(bidding.ObligateMoney));
			//ȡ���̼ұ���
			BLL.BiddingEmit biddEmit = new BiddingEmit();			
			//string lastCode = biddEmit.GetLastEmitCode(BiddingCode);
			//BLL.BiddingReturn biddReturn = new BiddingReturn();
			//biddReturn.Flag="1";
			//biddReturn.BiddingEmitCode = lastCode;
			//Lb_PjMoney.Text = biddReturn.Money;
			//Response.Write(Rms.Web.JavaScript.Alert(true,lastCode+"hgjgj"+biddReturn.Money));
			string pjMoney = biddEmit.GetFactMoneyByBiddingCode(this.BiddingCode);
			Lb_PjMoney.Text =String.Format("{0:N}",Convert.ToDecimal(pjMoney));
			Decimal FactMoney = Convert.ToDecimal(pjMoney) - Convert.ToDecimal(bidding.ObligateMoney);
			//Lb_PjMoney.Text =
			Lb_FactMoney.Text = String.Format("{0:N}",Convert.ToDecimal(FactMoney));//.ToString();
		}
		#endregion


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
