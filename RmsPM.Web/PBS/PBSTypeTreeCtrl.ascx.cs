namespace RmsPM.Web.PBS
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	/// PBSTypeTreeCtrl ��ժҪ˵����
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

		private void IniPage() 
		{
			try
			{
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"��ʼ��ҳ��ʧ��");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ��ʧ�ܣ�" + ex.Message));
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
