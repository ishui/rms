namespace RmsPM.Web.PBS
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	/// PBSTypeTreeCtrl 的摘要说明。
	/// </summary>
	public partial class PBSTypeTreeCtrl : System.Web.UI.UserControl
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (this.Visible) 
			{
				string reload = Rms.Web.JavaScript.ScriptStart;
				reload += @"var ClientID = '" + this.ClientID + "';" + "\n" ;
				reload += Rms.Web.JavaScript.ScriptEnd;
				Response.Write(reload);

				if (!Page.IsPostBack) 
				{
					IniPage();
				}
			}
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

		private void IniPage() 
		{
			try
			{
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"初始化页面失败");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面失败：" + ex.Message));
			}
		}

		public void SetParam(string ProjectCode, string PBSTypeCode, string ShowType) 
		{
			this.txtProjectCode.Value = ProjectCode;
			this.txtPBSTypeCode.Value = PBSTypeCode;
			this.txtShowType.Value = ShowType;
		}
	}
}
