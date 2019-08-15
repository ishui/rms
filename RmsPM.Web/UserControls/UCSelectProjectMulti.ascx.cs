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
	/// UCSelectProjectMulti ��ժҪ˵����
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}

        private void IniPage()
        {
            try
            {
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "��ʼ��ҳ��ʧ��");
                Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ��ʧ�ܣ�" + ex.Message));
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
        /// ��Ȩ�޵���Ŀ or ȫ����Ŀ
        /// </summary>
		public string Access
		{
			set
			{
				this.txtAccess.Value = value;
			}
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
