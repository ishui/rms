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
                            Image1.ImageUrl = "./images/icon_going.gif"; this.lblMessage.Text = "已登录";
                        }
                        else { Image1.ImageUrl = "./images/icon_cancel.gif"; this.lblMessage.Text = "用户名或密码错误"; }
                    }
                    else { Image1.ImageUrl = "./images/icon_cancel.gif"; this.lblMessage.Text = "用户名或密码错误"; }
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
                        // 0－正常，1 禁用
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

                            /********************** 在线用户统计 ***************************/
                            Application.Lock();
                            Hashtable UserTable = (Hashtable)Application["UserTable"];
                            if (!UserTable.Contains(user.UserCode+","+Session.SessionID))
                            {
                                UserTable.Add(user.UserCode+","+Session.SessionID, user.UserName);
                                Application["UserTable"] = UserTable;
                            }

                            //单一用户登录控制
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
                    //记录最后操作时间
                    Session["LastOperTime"] = DateTime.Now;
                }
                return OK;

            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "用户登录失败");
                Response.Write(Rms.Web.JavaScript.Alert(true, "用户登录失败：" + ex.Message));
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
                Response.Write(@" var win = OpenFullWindow('frame.htm','房产项目管理系统" + usercode + "'); ");
            }
            //string strOaPath = System.Configuration.ConfigurationSettings.AppSettings["OAUrl"];         
            else
            {
                Response.Write(@" var win=OpenNormalWindow('default.aspx','房产项目管理系统');");
            }
            
            Response.Write(Rms.Web.JavaScript.ScriptEnd);
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            RedirectToMainFrame();
        }
    }
}
