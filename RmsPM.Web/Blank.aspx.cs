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
	/// Blank ��ժҪ˵����
	/// </summary>
	public partial class Blank : PageBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(Request["Flag"] == null)
			{
				string MsgScript = "";
				string InLineUser = " <a href=\"#\" onclick=\"javascript:goInLinePage();return false;\">����������"+((Hashtable)Application["UserTable"]).Count.ToString()+"</a>";

				BLL.SendMsg msg = new BLL.SendMsg();
				msg.ToUsercode = ((User)Session["User"]).UserCode;
				msg.State = "1";
				msg.todel = "0";

				if(msg.GetSendMsgs().Rows.Count>0)
					MsgScript = "<a href=\"#\" onclick=\"javascript:goMsgManage();return false;\"><IMG runat=\"server\" id=\"MsgImg\" border=\"0\" alt=\"��Ϣ\" src=\"Images/ShowMsg.gif\"></a>";
				else
					MsgScript = "<a href=\"#\" onclick=\"javascript:goMsgManage();return false;\"><IMG runat=\"server\" id=\"MsgImg\" border=\"0\" alt=\"��Ϣ\" src=\"Images/Msg.gif\"></a>";


				this.scriptspan.InnerHtml = "<script language=\"javascript\">"
					+"window.parent.tdInLineUser.innerHTML = '"+InLineUser+"';"
					+"window.parent.TdMsgImg.innerHTML = '"+MsgScript+"';"
					+"</script>";
			}
			else
			{
				/********************** �����û�ͳ�� ***************************/
                Session.Abandon();
				/***************************************************************/
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
