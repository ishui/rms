namespace RmsPM.Web.UserControls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		InputSupplier ��ժҪ˵����
	/// </summary>
	public partial class InputSupplier : System.Web.UI.UserControl
	{
		protected string imagePath = "../images/";

		protected void Page_Load(object sender, System.EventArgs e)
		{
//			this.divName.InnerText = this.txtName.Value;
//			this.divHint.InnerText = this.txtHint.Value;

			if (this.Visible) 
			{
				this.txtInput.Attributes["ClientID"] = this.ClientID;

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
				txtClientID.Value = this.ClientID;
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"��ʼ��ҳ��ʧ��");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ��ʧ�ܣ�" + ex.Message));
			}
		}

		/// <summary>
		/// ���ô���
		/// </summary>
		/// <param name="code"></param>
		private void SetCode(string code) 
		{
			try 
			{
				this.txtCode.Value = code;
				this.txtInput.Value =  BLL.ProjectRule.GetSupplierName( code);

			}
			catch ( Exception ex )
			{
				throw ex;
			}
		}

		public string Value 
		{
			get {return this.txtCode.Value;}
			set {SetCode(value);}
		}

		public string Text 
		{
			get {return this.txtName.Value;}
		}

		public string Hint 
		{
			get {return this.txtHint.Value;}
		}

		public string SortID 
		{
			get {return this.txtSortID.Value;}
		}

		public string ImagePath
		{
			set
			{
				this.imagePath = value;
			}
		}
	}
}
