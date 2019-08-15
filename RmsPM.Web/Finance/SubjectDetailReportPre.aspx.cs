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

using Rms.ORMap;
using RmsPM.DAL.EntityDAO;
using RmsPM.DAL.QueryStrategy;
using Rms.Web;

namespace RmsPM.Web.Finance
{
	/// <summary>
	/// PaymentList ��ժҪ˵����
	/// </summary>
	public partial class SubjectDetailReportPre : PageBase
	{


	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack )
			{
				IniPage();
			}
		}

		private void IniPage()
		{
			this.sltYear.Items.Clear();
			for ( int i = 2000; i< 2020 ; i++)
				this.sltYear.Items.Add ( new ListItem ( i.ToString(),i.ToString()));
			this.sltYear.Value = DateTime.Now.Year.ToString();
			this.sltMonth.Value = DateTime.Now.Month.ToString();
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
