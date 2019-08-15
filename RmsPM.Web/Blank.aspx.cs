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

namespace RmsPM.Web
{
	/// <summary>
	/// Blank 的摘要说明。
	/// </summary>
	public partial class Blank : PageBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(Request["Flag"] == null)
			{
				string MsgScript = "";
				string InLineUser = " <a href=\"#\" onclick=\"javascript:goInLinePage();return false;\">在线人数："+((Hashtable)Application["UserTable"]).Count.ToString()+"</a>";

				BLL.SendMsg msg = new BLL.SendMsg();
				msg.ToUsercode = ((User)Session["User"]).UserCode;
				msg.State = "1";
				msg.todel = "0";

				if(msg.GetSendMsgs().Rows.Count>0)
					MsgScript = "<a href=\"#\" onclick=\"javascript:goMsgManage();return false;\"><IMG runat=\"server\" id=\"MsgImg\" border=\"0\" alt=\"消息\" src=\"Images/ShowMsg.gif\"></a>";
				else
					MsgScript = "<a href=\"#\" onclick=\"javascript:goMsgManage();return false;\"><IMG runat=\"server\" id=\"MsgImg\" border=\"0\" alt=\"消息\" src=\"Images/Msg.gif\"></a>";


				this.scriptspan.InnerHtml = "<script language=\"javascript\">"
					+"window.parent.tdInLineUser.innerHTML = '"+InLineUser+"';"
					+"window.parent.TdMsgImg.innerHTML = '"+MsgScript+"';"
					+"</script>";
			}
			else
			{
				/********************** 在线用户统计 ***************************/
                Session.Abandon();
				/***************************************************************/
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
