using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace RmsPM.Web.BiddingManage
{
	/// <summary>
	/// BiddingReturnList1 ��ժҪ˵����
	/// </summary>
	public partial class BiddingReturnQuery : System.Web.UI.Page
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			LoadData();
		}
		private void LoadData()
		{
			this.BiddingEmitList1.BiddingCode = Request["BiddingCode"]+"";
			this.BiddingEmitList1.DataBound();
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
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion
	}
}
