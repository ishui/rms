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

namespace RmsPM.Web.Project
{
	/// <summary>
	/// CBS ��ժҪ˵����
	/// </summary>
	public partial class CBS : PageBase
	{

	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack)
			{
				IniPage();
				if ( !user.HasRight("040102"))
				{
					this.btnInput.Visible = false;
					this.btnOutput.Visible = false;
					this.btnAddChild.Visible = false;
				}
			}
		}

		private void IniPage()
		{
//			string moduleType = Request["ModuleType"] + "";
//			if ( moduleType == "System" )
//			{
//				this.btnInput.Visible = false;
//			}
				

			

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
