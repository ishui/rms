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
	/// WBS ��ժҪ˵����
	/// </summary>
	public partial class WBS : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlInputButton btnStart;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnGoing;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnCancel;
		protected System.Web.UI.HtmlControls.HtmlInputButton btmHalt;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnComplete;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtSortField;
		protected System.Web.UI.HtmlControls.HtmlInputHidden TaskStatus;
		protected System.Web.UI.WebControls.TextBox TextBox2;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnAll;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if (!this.IsPostBack)
			{
			}

			User myUser = new User(user.UserCode);
			this.spanIOButton.Style["display"] = (myUser.HasOperationRight("070103"))?"":"none"; // 070103ΪWBS���뵼��Ȩ��
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
