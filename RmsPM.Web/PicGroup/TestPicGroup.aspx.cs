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

namespace RmsPM.Web.PicGroup
{
	/// <summary>
	/// TestPicGroup ��ժҪ˵����
	/// </summary>
	public partial class TestPicGroup : PageBase
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !Page.IsPostBack )
			{
				this.IniPage();
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
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion

		private void IniPage()
		{
			try
			{
				this.UCPicGroup1.RootPath = "../";
				//this.UCPicGroup1.MasterType = "TestPG";
				//this.UCPicGroup1.MasterCode = "TestPGMAsterCode";
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

	}
}
