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

namespace RmsPM.Web.Project
{
	/// <summary>
	/// WBSGridModify ��ժҪ˵����
	/// </summary>
    public partial class WBSGridModify : System.Web.UI.Page
	{
		
	
		private void Page_Load(object sender, System.EventArgs e)
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
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{    
			this.SaveToolsButton.ServerClick += new System.EventHandler(this.SaveToolsButton_ServerClick);
			this.btDelete.ServerClick += new System.EventHandler(this.btDelete_ServerClick);

		}
		#endregion

		private void SaveToolsButton_ServerClick(object sender, System.EventArgs e)
		{
		
		}

		private void btDelete_ServerClick(object sender, System.EventArgs e)
		{
		
		}
	}
}
