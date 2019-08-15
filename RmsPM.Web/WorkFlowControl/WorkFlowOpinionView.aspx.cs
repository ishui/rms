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
	/// WorkFlowRouterCopy 的摘要说明。
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
				this.UserName.InnerHtml = "处理人："+BLL.SystemRule.GetUserName(entity.GetString("UserCode"));
				this.OpininoDate.InnerHtml = "处理时间："+entity.GetDateTime("OpinionDate").ToString();
				this.OpinionDiv.InnerHtml = entity.GetString("OpinionText").Replace("\n", "<br>");;
				AttachMentList1.MasterCode = entity.GetString("ApplicationCode").ToString();
				
			}
			
		}

		#region Web 窗体设计器生成的代码
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion
	}
}
