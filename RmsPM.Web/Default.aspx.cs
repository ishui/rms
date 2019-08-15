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
using RmsPM.DAL;
using RmsPM.DAL.QueryStrategy;

namespace RmsPM.Web
{
	/// <summary>
	/// _Default 的摘要说明。
	/// </summary>
	public partial class _Default :  System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, System.EventArgs e)
		{
            //string ud_sPMName = System.Configuration.ConfigurationManager.AppSettings["PMName"].ToString();
            //string userID="";
            #region  注释掉的内容
            //switch (ud_sPMName.ToLower())
            //{ 
            //    case "yuhongpm":
            //        //Bt_GetPassword.Visible = false;
            //        break;
            //}
            
            // HttpClientCertificate hcc = Request.ClientCertificate;
             
            // if (hcc.IsPresent&&hcc.IsValid)
            // {                
            //     string returnStr = "";
            //     string comma = ",";
            //     char[] commaStr = comma.ToCharArray();
            //     string amount = "=";
            //     char[] amountStr = amount.ToCharArray();
            //     string s = hcc.Subject.ToString();
            //     string[] sArray = s.Split(commaStr);
            //     for (int i = 0; i < sArray.Length; i++)
            //     {
            //         if (sArray[i].IndexOf("CN=")>0) 
            //         {
            //             string[] nArray = sArray[i].Split(amountStr);
            //             //Response.Write(nArray[1].ToString());
            //             userID=nArray[1].ToString();
            //         }  
            //     }
            //     Login(userID, "", false);
            // }
            #endregion

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
			this.imgLogin.ServerClick += new System.Web.UI.ImageClickEventHandler(this.imgLogin_ServerClick);
			//this.Bt_GetPassword.ServerClick += new System.Web.UI.ImageClickEventHandler(this.Bt_GetPassword_ServerClick);

		}
		#endregion


		private void imgLogin_ServerClick(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			this.lblMessage.Text = "";
			string userID = this.TextBoxUsername.Value;
			string pwd = this.TextBoxPassword.Value;

            if (userID == "")
            {
                this.lblMessage.Text = "请输入用户名";

            }
            else
            {
                Login(userID, pwd, true);
            }
			
		}
        private void Login(string userID, string pwd,bool IsNeedPwd) 
        {
            try
            {
                UserStrategyBuilder sb = new UserStrategyBuilder(); //获取SQL查询语句;
                if (System.Configuration.ConfigurationSettings.AppSettings["UserNameLogin"] == "1")
                {
                    sb.AddStrategy(new Strategy(UserStrategyName.UserIdorUserName, userID));  //将用户ID值加入到Strategy；
                    if (IsNeedPwd)
                    {
                        sb.AddStrategy(new Strategy(UserStrategyName.PassWord, pwd));  
                    }
                }
                else
                {
                    sb.AddStrategy(new Strategy(UserStrategyName.UserID, userID));
                }
                string sql = sb.BuildMainQueryString(); 

                Rms.ORMap.QueryAgent qa = new QueryAgent();                  //打开数据库;
                EntityData entity = qa.FillEntityData("SystemUser", sql);   //读取数据数据；
                qa.Dispose();
                string workNO = "";

                bool OK = false;
                if (!entity.HasRecord())
                {
                    this.lblMessage.Text = "用户名或密码错误";
                }
                else
                {
                    string RealPwd = entity.GetString("Password");
                    if (pwd != RealPwd && IsNeedPwd)
                    {
                        this.lblMessage.Text = "用户名或密码错误";
                    }
                    else
                    {
                        int status = entity.GetInt("Status");
                        // 0－正常，1 禁用
                        if (status == 1)
                        {
                            this.lblMessage.Text = "该用户已被禁用";
                        }
                        else
                        {
                            string userCode = entity.GetString("UserCode");
                            User user = new User(userCode);
                            //user.ResetUser("P1010");
                            Session["User"] = user;
                            workNO = user.WorkNO;

                            //是否缺省显示最后一次访问的项目
                            string UseLastProject = System.Configuration.ConfigurationSettings.AppSettings["UseLastProject"];
                            if (UseLastProject == "1")
                            {
                                // 取用户最后使用的项目，如果没有取用户能进入的项目， 和用户权限相结合
                                string projectCode = entity.GetString("LastProjectCode");

                                if (projectCode == "")
                                {
                                    // 这里要修改， 取用户能进入的项目
                                    EntityData projects = DAL.EntityDAO.ProjectDAO.GetAllProject();
                                    if (projects.HasRecord())
                                    {
                                        projectCode = projects.GetString("ProjectCode");
                                    }
                                    projects.Dispose();
                                }

                                Session["ProjectCode"] = projectCode;
                                if (projectCode != "")
                                {
                                    ((ProjectInfo)Session["project"]).Reset(projectCode);
                                }
                            }

                            OK = true;

                            /********************** 在线用户统计 ***************************/
                            Application.Lock();
                            Hashtable UserTable = (Hashtable)Application["UserTable"];
                            if (!UserTable.Contains(user.UserCode + "," + Session.SessionID))
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

                    string strOaPath = System.Configuration.ConfigurationSettings.AppSettings["OAUrl"];
                    Response.Write(@"<Script language=""javascript"" src=""./Rms.js""></Script>");
                    Response.Write(Rms.Web.JavaScript.ScriptStart);
                    Response.Write("window.opener=null;");
                    string usercode=((User)Session["User"]).UserCode;
                    Response.Write(@" var win = OpenFullWindow('frame.htm','房产项目管理系统" + (usercode=="0"?(DateTime.Now.Minute.ToString()+DateTime.Now.Second.ToString()):usercode) + "'); ");
                    Response.Write(@" if ( win != this ) { win.opener = null;window.open('', '_parent', ''); window.close(); }");
                    Response.Write(Rms.Web.JavaScript.ScriptEnd);
                }

            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "用户登录失败");
                Response.Write(Rms.Web.JavaScript.Alert(true, "用户登录失败：" + ex.Message));
            }
        }
		#region 取回密码
		/*private void Bt_GetPassword_ServerClick(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			 GetPassWord();
		}

		private void GetPassWord()
		{
			this.lblMessage.Text = "";
			string userID = this.TextBoxUsername.Value;
			string pwd = this.TextBoxPassword.Value;

			if (userID == "") 
			{
				this.lblMessage.Text = "请输入用户名";
				return;
			}

			try
			{
				UserStrategyBuilder sb = new UserStrategyBuilder();
				///sb.AddStrategy( new Strategy( UserStrategyName.UserName,userID));
				//				sb.AddStrategy( new Strategy( UserStrategyName.PassWord,pwd));
				//sb
				sb.AddStrategy(new Strategy(UserStrategyName.UserIdorUserName,userID));
				string sql = sb.BuildMainQueryString();
				Rms.ORMap.QueryAgent qa = new QueryAgent();
				EntityData entity = qa.FillEntityData("SystemUser",sql);
				qa.Dispose();
				if ( !entity.HasRecord() )
				{
					this.lblMessage.Text = "用户名错误";
				}
				else
				{
					DataTable tb = entity.Tables[0];					
					foreach(DataRow dr in tb.Rows)
					{
						if(dr["MailBox"].ToString()=="")
						{
							this.lblMessage.Text = "你没有系统邮箱";
							throw new Exception("你没有邮箱");
						}
						BLL.MailRule mymail= new RmsPM.BLL.MailRule();
						mymail.Title = userID+" Pm系统密码";
						mymail.ToMail=dr["MailBox"].ToString().Split(new char[] { ';' })[0];
						mymail.Addressee=userID;
						mymail.ShowMode="1";
						mymail.SendName="系统管理中心";
						mymail.Body=" PM系统:用户名:"+dr["userID"].ToString()+" 名字: "+dr["UserName"].ToString()+" 密码: "+dr["PassWord"].ToString();
						mymail.sendMail();
						this.lblMessage.Text="密码已发送到您的邮箱";
						//mymail.Body = "亲爱的:"entity.Tables[0].Rows[""]"";
					}
				}
			}
			catch( Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"取回密码失败");
				Response.Write(Rms.Web.JavaScript.Alert(true, "取回密码失败：" + ex.Message));
			}
		}*/
		#endregion

    }

}
