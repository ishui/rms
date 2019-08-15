namespace RmsPM.Web.DTS
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		DtsProgress ��ժҪ˵����
	/// </summary>
	public partial class DtsProgress : System.Web.UI.UserControl
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
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

		public void SetCurrentIndex(int CurrentIndex) 
		{
			this.lbIndex.Text = (CurrentIndex + 1).ToString();
		}

		public void Start(int Count) 
		{
			this.tdHint.InnerText = "���ڴ������Ժ򡣡���";
			this.lbCount.Text = Count.ToString();
//			this.Visible = true;
		}

		public void Over() 
		{
			this.tdHint.InnerText = "���";
//			this.Visible = false;
		}
	}
}
