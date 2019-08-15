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
	///		Control_BiddingEmitHistory ��ժҪ˵����
	/// </summary>
	public partial class Control_BiddingEmitHistory : System.Web.UI.UserControl
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
		#region ��������
		/// <summary>
		/// �õ��б�ƻ�ID
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
		#region ��������
		/// <summary>
		/// ����ҳ��������Ϣ
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
