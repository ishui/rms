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

namespace RmsPM.Web.PBS
{
	/// <summary>
	/// PBSTypeTree ��ժҪ˵����
	/// </summary>
	public partial class PBSTypeTree : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtShowItems;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtTreeType;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtCheckBalance;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack) 
			{
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];
				this.txtAct.Value = Request.QueryString["act"];
				this.txtAct.Value = this.txtAct.Value.ToLower();
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
	}
}
