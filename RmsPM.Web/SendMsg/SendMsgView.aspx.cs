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
	/// SendMsgView ��ժҪ˵����
	/// </summary>
	public partial class SendMsgView : PageBase
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
				InitPage();

            AttachMentList1.AttachMentType = "SendMsg1";
            if (Request["MsgCode"]+"" != "")
            {
                AttachMentList1.MasterCode = Request["MsgCode"] + "";
            }
            
		}
		private void InitPage()
		{
			if(Request["MsgCode"] != null)
			{
				BLL.SendMsg send = new BLL.SendMsg();
				send.SendMsgCode = Request["MsgCode"].ToString();
				this.txtContent.InnerHtml = send.Msg.Replace("\n", "<br>");
				this.tdsendtime.InnerHtml = "����ʱ�䣺"+send.Sendtime;
				if(send.SendUsercode == user.UserCode)
				{
					this.tdusername.InnerHtml = "�����ˣ�"+BLL.SystemRule.GetUserName(send.ToUsercode);
				}
				if(send.ToUsercode == user.UserCode)
				{
					this.tdusername.InnerHtml = "�����ˣ�"+BLL.SystemRule.GetUserName(send.SendUsercode);
					BLL.SendMsg msg = new BLL.SendMsg();
					msg.SendMsgCode = Request["MsgCode"].ToString();
					msg.State = "2";
					msg.SendMsgSubmit();
				}
				if(send.SendUsercode == user.UserCode && send.ToUsercode == user.UserCode)
				{
					this.tdusername.InnerHtml = "�����ˣ�"+BLL.SystemRule.GetUserName(send.SendUsercode);
					this.tdusername.InnerHtml += " �����ˣ�"+BLL.SystemRule.GetUserName(send.ToUsercode);
				}
				if(Request["Re"]+"" == "true")
				{
					this.btnRevert.Visible = true;
					this.spanscript.InnerHtml = "<script language=\"javascript\">"
						+"function Revert()"
						+"{"
						+"		window.location=\"../SendMsg/SendMsgModify.aspx?UserCode="+send.SendUsercode+"\""
						+"}"
						+"</script>";
				}
				else
				{
					this.btnRevert.Visible = false;
				}

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

		protected void Button1_ServerClick(object sender, System.EventArgs e)
		{
			if(Request["MsgCode"] != null)
			{
				BLL.SendMsg send = new BLL.SendMsg();
				send.SendMsgCode = Request["MsgCode"].ToString();

				if(send.SendUsercode == user.UserCode)
				{
					send.senddel = "1";
				}
				if(send.ToUsercode == user.UserCode)
				{
					send.todel = "1";
				}
				if(send.SendUsercode == user.UserCode && send.ToUsercode == user.UserCode)
				{
					send.senddel = "1";
					send.todel = "1";
				}
				send.SendMsgSubmit();

				Response.Write(Rms.Web.JavaScript.ScriptStart);
				Response.Write(Rms.Web.JavaScript.OpenerReload(false));
				Response.Write(Rms.Web.JavaScript.WinClose(false));
				Response.Write(Rms.Web.JavaScript.ScriptEnd);
			}
			
		}
        protected void btnTransmit_ServerClick(object sender, EventArgs e)
        {
            if (Request["MsgCode"] != null)
            { 
                BLL.SendMsg send = new RmsPM.BLL.SendMsg();
                send.SendMsgCode = Request["MsgCode"].ToString();
               Response.Redirect("../SendMsg/SendMsgModify.aspx?SendMsgCode=" + send.SendMsgCode + "&Type=4");//��4�������ж�ת����
            } 
        }
}
}
