using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Rms.ORMap;
namespace RmsPM.Web.SendMsg
{
	/// <summary>
	/// SendMsgList 的摘要说明。
	/// </summary>
	public partial class SendMsgList : PageBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{
				InitPage();
			}
		}
		private void InitPage()
		{
            BLL.SendMsg send2 = new BLL.SendMsg();
            send2.ToUsercode = user.UserCode;
            send2.State = "1";
            send2.todel = "0";
            if (send2.GetSendMsgs().Rows.Count > 0)
            {
                string code = send2.GetSendMsgs().Rows[0]["SendMsgCode"].ToString();
                Response.Redirect("sendmsgview.aspx?MsgCode=" + code + "&Re=true");
            }
            else
            {
                BLL.SendMsg send = new BLL.SendMsg();
                send.SendUsercode = user.UserCode;
                send.senddel = "0";
                dgList.DataSource = send.GetSendMsgs();
                dgList.DataBind();
                this.GridPagination1.RowsCount = send.GetSendMsgs().Rows.Count.ToString();

                BLL.SendMsg send1 = new BLL.SendMsg();
                send1.ToUsercode = user.UserCode;
                send1.todel = "0";
                Datagrid1.DataSource = send1.GetSendMsgs();
                Datagrid1.DataBind();
                this.GridPagination2.RowsCount = send1.GetSendMsgs().Rows.Count.ToString();
            }
        }
		public static string GetMsgState(string StateCode)
		{
			string StateName = "";
			if(StateCode == "1")
			{
				StateName = "未读";
			}
			else if(StateCode == "2")
			{
				StateName = "已读";
			}
			else if(StateCode == "3")
			{
				StateName = "归档";
			}
			return StateName;
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

		protected void GridPagination2_PageIndexChange(object sender, System.EventArgs e)
		{
			this.InitPage();
		
		}

		protected void GridPagination1_PageIndexChange(object sender, System.EventArgs e)
		{
			this.InitPage();
		}
        protected void Button3_ServerClick(object sender, EventArgs e)
        {
            BLL.SendMsg send = new RmsPM.BLL.SendMsg();
            BLL.SendMsg send1 = new RmsPM.BLL.SendMsg(); ;
           
            
            for (int i = 0; i < this.dgList.Items.Count; i++)
            {
                CheckBox checkAll = (CheckBox)dgList.Items[i].Cells[5].FindControl("ck1");
                if (checkAll.Checked == true)
                {
                    
                    string tblName="SendMsg";
                    string code = dgList.Items[i].Cells[0].Text.Trim().ToString();
				    
                    send.SendMsgCode = code;

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
                   
                }
            
            }

            if ((send1.State) != "1")
            {
                for (int i = 0; i < this.Datagrid1.Items.Count; i++)
                {
                    CheckBox checkAll = (CheckBox)Datagrid1.Items[i].Cells[5].FindControl("ck2");
                    if (checkAll.Checked == true)
                    {
                        send1 = new BLL.SendMsg();
                        string tblName = "SendMsg";
                        string code = Datagrid1.Items[i].Cells[0].Text.Trim().ToString();

                        send1.SendMsgCode = code;

                        if (send1.SendUsercode == user.UserCode)
                        {
                            send1.senddel = "1";
                        }
                        if (send1.ToUsercode == user.UserCode)
                        {
                            send1.todel = "1";
                        }
                        if (send1.SendUsercode == user.UserCode && send1.ToUsercode == user.UserCode)
                        {
                            send1.senddel = "1";
                            send1.todel = "1";
                        }
                        send1.SendMsgSubmit();

                    }

                }
            }
            InitPage();
        }
    }
}
