namespace RmsPM.Web.DTS
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		DtsProgress 的摘要说明。
	/// </summary>
	public partial class DtsProgress : System.Web.UI.UserControl
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

		public void SetCurrentIndex(int CurrentIndex) 
		{
			this.lbIndex.Text = (CurrentIndex + 1).ToString();
		}

		public void Start(int Count) 
		{
			this.tdHint.InnerText = "正在处理，请稍候。。。";
			this.lbCount.Text = Count.ToString();
//			this.Visible = true;
		}

		public void Over() 
		{
			this.tdHint.InnerText = "完成";
//			this.Visible = false;
		}
	}
}
