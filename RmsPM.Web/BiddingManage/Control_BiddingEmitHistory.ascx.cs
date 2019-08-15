namespace RmsPM.Web.BiddingManage
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using RmsPM.BLL;

	/// <summary>
	///		Control_BiddingEmitHistory 的摘要说明。
	/// </summary>
	public partial class Control_BiddingEmitHistory : System.Web.UI.UserControl
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
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
		#region 公用属性
		/// <summary>
		/// 得到招标计划ID
		/// </summary>
		public string BiddingCode
		{
			get
			{
				if(this.ViewState["BiddingCode"] != null)
					return this.ViewState["BiddingCode"].ToString();
				return "";
			}
			set
			{
				this.ViewState["BiddingCode"] = value;
			}
		}
		#endregion
		#region 加载数据
		/// <summary>
		/// 加载页面所有信息
		/// </summary>
		public void LoadData()
		{
			Get_BiddingData();
		}
		private void Bind_DataGrid1(DataView dv)
		{
			this.DataGrid1.DataSource = dv;
			this.DataGrid1.DataBind();
		}
		private void Bind_DataGrid1()
		{
		}
		private void Get_BiddingData()
		{
			BLL.BiddingEmit biddemit = new BiddingEmit();
			biddemit.BiddingCode = this.BiddingCode;
			DataTable dt = biddemit.GetBiddingEmits();
			DataView dv = new DataView(dt);
			dv.Sort = "BiddingEmitCode";
			this.Bind_DataGrid1(dv);
		}		
		#endregion
	}
}
