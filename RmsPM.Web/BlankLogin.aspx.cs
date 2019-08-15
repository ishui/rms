using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Rms.ORMap;
using RmsPM.DAL;
using RmsPM.DAL.QueryStrategy;

namespace RmsPM.Web
{
    public partial class BlankLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                string username = Request["UserName"] + "";
                string password = Request["Password"] + "";
                string action = Request["Act"] + "";
                if (action.ToLower() == "open")
                {
                    if (Login(username, password, true))
                    {
                        Response.Redirect("frame.htm");
                    }
                    else
                    {
                        Response.Redirect("default.aspx");
                    }

                }
                else
                {
                    if (username != string.Empty && password != string.Empty && Session["User"] == null)
                    {
                        if (Login(username, password, true))
                        {
                            //Response.Redirect("blanklogin.aspx?username="+username+"&password="+password);
                            Image1.ImageUrl = "./images/icon_going.gif"; this.lblMessage.Text = "�ѵ�¼";
                        }
                        else { Image1.ImageUrl = "./images/icon_cancel.gif"; this.lblMessage.Text = "�û������������"; }
                    }
                    else { Image1.ImageUrl = "./images/icon_cancel.gif"; this.lblMessage.Text = "�û������������"; }
                }
                //Response.Write("not Postback");
          
          
        }
        private bool Login(string userID, string pwd, bool IsNeedPwd)
        {           
            try
            {
                bool OK = false;
                UserStrategyBuilder sb = new UserStrategyBuilder();
                sb.AddStrategy(new Strategy(UserStrategyName.UserID, userID));
                string sql = sb.BuildMainQueryString();

                Rms.ORMap.QueryAgent qa = new QueryAgent();
                EntityData entity = qa.FillEntityData("SystemUser", sql);
                qa.Dispose();
                string workNO = "";

                
                if (!entity.HasRecord())
                {
                    
                }
                else
                {
                    string RealPwd = entity.GetString("Password");
                    if (RealPwd != pwd)
                    {
                        OK = false;
                    }
                    else
                    {

                        int status = entity.GetInt("Status");
                        // 0��������1 ����
                        if (status == 0)
                        {

                            string userCode = entity.GetString("UserCode");
                            User user = new User(userCode);
                            //						user.ResetUser("P1010");
                            Session["User"] = user;
                            ViewState["UserName"] = userID;
                            ViewState["Password"] = pwd;
                            workNO = user.WorkNO;
                            OK = true;

                            /********************** �����û�ͳ�� ***************************/
                            Application.Lock();
                            Hashtable UserTable = (Hashtable)Application["UserTable"];
                            if (!UserTable.Contains(user.UserCode+","+Session.SessionID))
                            {
                                UserTable.Add(user.UserCode+","+Session.SessionID, user.UserName);
                                Application["UserTable"] = UserTable;
                            }

                            //��һ�û���¼����
                            if (BLL.ConvertRule.ToString(Application["SingleUserLogin"]) == "1")
                            {
                                Hashtable KilledUserTable = (Hashtable)Application["KilledUserTable"];
                                PageBase.KillUser(user, UserTable, KilledUserTable, Session);
                            }

                            Application.UnLock();
                            /***************************************************************/
                        }
                    }
                    
                }
                entity.Dispose();

                if (OK)
                {
                    //��¼������ʱ��
                    Session["LastOperTime"] = DateTime.Now;
                }
                return OK;

            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "�û���¼ʧ��");
                Response.Write(Rms.Web.JavaScript.Alert(true, "�û���¼ʧ�ܣ�" + ex.Message));
                return false;
            }
        }
        protected void RedirectToMainFrame()
        {
            Response.Write(@"<Script language=""javascript"" src=""./Rms.js""></Script>");
            Response.Write(Rms.Web.JavaScript.ScriptStart);
            string usercode="";
            if (Session["User"] != null)
            {
                usercode = ((User)Session["User"]).UserCode;
                Response.Write(@" var win = OpenFullWindow('frame.htm','������Ŀ����ϵͳ" + usercode + "'); ");
            }
            //string strOaPath = System.Configuration.ConfigurationSettings.AppSettings["OAUrl"];         
            else
            {
                Response.Write(@" var win=OpenNormalWindow('default.aspx','������Ŀ����ϵͳ');");
            }
            
            Response.Write(Rms.Web.JavaScript.ScriptEnd);
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            RedirectToMainFrame();
        }
    }
}
