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

namespace RmsPM.Web.SendMsg
{
	/// <summary>
	/// InlineUser ��ժҪ˵����
	/// </summary>
	public partial class InlineUser : PageBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Hashtable UserTable = (Hashtable)Application["UserTable"];
			dgList.DataSource = UserTable;
			dgList.DataBind();
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
        protected void dgList_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dgList.CurrentPageIndex = e.NewPageIndex;
            Hashtable UserTable = (Hashtable)Application["UserTable"];
            dgList.DataSource = UserTable;
            dgList.DataBind();
        }
}
}
