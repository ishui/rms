namespace RmsPM.Web.UserControls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		UCSelectContract ��ժҪ˵����
	/// </summary>
	public partial class UCSelectContract : System.Web.UI.UserControl
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
		}


		protected string imagePath = "../images/";
		public string ImagePath
		{
			set
			{
				this.imagePath = value; 
			}
		}

		public string Value
		{
			get
			{
				return this.txtInputHidden.Value;
			}
			set
			{
				this.txtInputHidden.Value = value;
				this.txtInput.Value = DAL.EntityDAO.ContractDAO.GetContractByCode(value).GetString("ContractName");
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
