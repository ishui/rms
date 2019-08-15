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
using System.Text;

using RmsPM.BLL;
using RmsPM.DAL.EntityDAO;
using RmsPM.DAL.QueryStrategy;
using RmsPM.Web;
using Rms.ORMap;
using Rms.WorkFlow;

namespace RmsPM.Web.WorkFlowControl
{
	/// <summary>
	/// WorkFlowRouterCopy ��ժҪ˵����
	/// </summary>
	public partial class WorkFlowOpinionView : System.Web.UI.Page
	{
  //


	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			AttachMentList1.AttachMentType = "WorkFlowActOpinion";
			string OpinionCode = Request["OpinionCode"]+"";
			EntityData entity = DAL.EntityDAO.WorkFlowDAO.GetWorkFlowOpinionByCode(OpinionCode);
			if(entity.HasRecord())
			{
				this.UserName.InnerHtml = "�����ˣ�"+BLL.SystemRule.GetUserName(entity.GetString("UserCode"));
				this.OpininoDate.InnerHtml = "����ʱ�䣺"+entity.GetDateTime("OpinionDate").ToString();
				this.OpinionDiv.InnerHtml = entity.GetString("OpinionText").Replace("\n", "<br>");;
				AttachMentList1.MasterCode = entity.GetString("ApplicationCode").ToString();
				
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
