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
	///		Control_BiddingEmitMoney 的摘要说明。
	/// </summary>
	public partial class Control_BiddingEmitMoney : BiddingControlBase
	{
		protected string _BiddingCode;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
		}
		#region 共有属性

		/// <summary>
		/// 供应商报价
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
		/// 暂定金额
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
		/// 实际金额
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
		#endregion
		#region 状态控制
		public void InitPage()
		{
			if(this.State == ModuleState.Eyeable)//可见的
			{
				LoadData();
			}
			else
			{
				this.Visible=false;
			}
		}
		#endregion
		#region 加载数据
		private void LoadData()
		{
			BLL.Bidding bidding = new RmsPM.BLL.Bidding();
			bidding.BiddingCode = this.BiddingCode;
			this.Lb_ObMoney.Text = String.Format("{0:N}",Convert.ToDecimal(bidding.ObligateMoney));
			//取回商家报价
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
