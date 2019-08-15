namespace RmsPM.Web.UserControls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using Rms.ORMap;

	/// <summary>
	/// UCSelectProjectMulti 的摘要说明。
	/// </summary>
	public partial class UCSelectProjectMulti : System.Web.UI.UserControl
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
                this.divName.InnerText = this.txtName.Value;

                if (this.Visible)
                {
//                    this.txtInput.Attributes["ClientID"] = this.ClientID;

                    if (!Page.IsPostBack)
                    {
                        IniPage();
                    }
                }
            }
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

        private void IniPage()
        {
            try
            {
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "初始化页面失败");
                Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面失败：" + ex.Message));
            }
        }
        
        public string Value
		{
			get
			{
                return this.txtCode.Value;
			}
			set
			{
			}
		}

        /// <summary>
        /// 有权限的项目 or 全部项目
        /// </summary>
		public string Access
		{
			set
			{
				this.txtAccess.Value = value;
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

	}
}
