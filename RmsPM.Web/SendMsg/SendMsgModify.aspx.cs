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

using RmsPM.Web;
using Rms.ORMap;


namespace RmsPM.Web.SendMsg
{
	/// <summary>
	/// SendMsgModfy 的摘要说明。
	/// </summary>
	public partial class SendMsgModify : PageBase
	{

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
                InitPage();


            AttachMentAdd1.AttachMentType = "SendMsg1";

        }

		private void InitPage()
		{
            if (Request["UserCode"] != null)
            {
                this.txtUserCode.Value = Request["UserCode"].ToString();
                this.txtUserName.Value = BLL.SystemRule.GetUserName(Request["UserCode"].ToString());
            }
            else
            {
                if (Request["Type"] == "4")
                {
                    string strNull = "";
                    BLL.SendMsg send = new RmsPM.BLL.SendMsg();
                    send.SendMsgCode = Request.QueryString["SendMsgCode"];
                    send.GetSendMsgs();
                    string sendMsg = send.Msg.Replace("<b>", strNull);
                    sendMsg = sendMsg.Replace("</b>", strNull);
                    this.txtContent.Value = sendMsg;
                    return;
                }
                return;
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

		protected void Button1_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				BLL.SendMsg send = new BLL.SendMsg();
				send.SendMsgCode = "";
				send.SendUsercode = user.UserCode;
				send.Msg = this.txtContent.Value;
				send.Sendtime = DateTime.Now.ToShortDateString();
				send.State = "1";
				send.senddel = "0";
				send.todel = "0";
				send.SendMsgSubmit(this.txtUserCode.Value);

                //生成多条附件
                EntityData entitydata = send.entitydata;
                 string oldSendMsgCode = "";
                if (entitydata.HasRecord())
                {
                    AttachMentAdd1.SaveAttachMent(entitydata.CurrentRow["SendMsgCode"].ToString());
                    oldSendMsgCode=entitydata.CurrentRow["SendMsgCode"].ToString();
                }
               
                for (int i = 1; i < entitydata.CurrentTable.Rows.Count; i++)
                {
                    entitydata.SetCurrentRow(i);
                    RmsPM.BLL.DocumentRule.Instance().CopyAttachment(oldSendMsgCode, entitydata.CurrentRow["SendMsgCode"].ToString(),"SendMsg1");
                }

				Response.Write(Rms.Web.JavaScript.ScriptStart);
				Response.Write(Rms.Web.JavaScript.OpenerReload(false));
				Response.Write(Rms.Web.JavaScript.WinClose(false));
				Response.Write(Rms.Web.JavaScript.ScriptEnd);
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}
	}
}
