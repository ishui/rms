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
using System.Xml;

namespace RmsPM.Web
{
	/// <summary>
	/// SystemLeftBar ��ժҪ˵����
	/// </summary>
	public partial class SystemLeftBar : PageBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			
			XMLTreeViewManager vm = new XMLTreeViewManager( Server.MapPath(System.Configuration.ConfigurationSettings.AppSettings["VirtualDirectory"]) + @"\SystemLeftBar.xml");
			string s = vm.GetLeftBarString( base.user , "" ,"" );
			this.tdBar.InnerHtml = s;
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
