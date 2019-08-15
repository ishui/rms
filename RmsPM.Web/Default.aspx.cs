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
	/// _Default ��ժҪ˵����
	/// </summary>
	public partial class _Default :  System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, System.EventArgs e)
		{
            //string ud_sPMName = System.Configuration.ConfigurationManager.AppSettings["PMName"].ToString();
            //string userID="";
            #region  ע�͵�������
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
                this.lblMessage.Text = "�������û���";

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
                UserStrategyBuilder sb = new UserStrategyBuilder(); //��ȡSQL��ѯ���;
                if (System.Configuration.ConfigurationSettings.AppSettings["UserNameLogin"] == "1")
                {
                    sb.AddStrategy(new Strategy(UserStrategyName.UserIdorUserName, userID));  //���û�IDֵ���뵽Strategy��
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

                Rms.ORMap.QueryAgent qa = new QueryAgent();                  //�����ݿ�;
                EntityData entity = qa.FillEntityData("SystemUser", sql);   //��ȡ�������ݣ�
                qa.Dispose();
                string workNO = "";

                bool OK = false;
                if (!entity.HasRecord())
                {
                    this.lblMessage.Text = "�û������������";
                }
                else
                {
                    string RealPwd = entity.GetString("Password");
                    if (pwd != RealPwd && IsNeedPwd)
                    {
                        this.lblMessage.Text = "�û������������";
                    }
                    else
                    {
                        int status = entity.GetInt("Status");
                        // 0��������1 ����
                        if (status == 1)
                        {
                            this.lblMessage.Text = "���û��ѱ�����";
                        }
                        else
                        {
                            string userCode = entity.GetString("UserCode");
                            User user = new User(userCode);
                            //user.ResetUser("P1010");
                            Session["User"] = user;
                            workNO = user.WorkNO;

                            //�Ƿ�ȱʡ��ʾ���һ�η��ʵ���Ŀ
                            string UseLastProject = System.Configuration.ConfigurationSettings.AppSettings["UseLastProject"];
                            if (UseLastProject == "1")
                            {
                                // ȡ�û����ʹ�õ���Ŀ�����û��ȡ�û��ܽ������Ŀ�� ���û�Ȩ������
                                string projectCode = entity.GetString("LastProjectCode");

                                if (projectCode == "")
                                {
                                    // ����Ҫ�޸ģ� ȡ�û��ܽ������Ŀ
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

                            /********************** �����û�ͳ�� ***************************/
                            Application.Lock();
                            Hashtable UserTable = (Hashtable)Application["UserTable"];
                            if (!UserTable.Contains(user.UserCode + "," + Session.SessionID))
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

                    string strOaPath = System.Configuration.ConfigurationSettings.AppSettings["OAUrl"];
                    Response.Write(@"<Script language=""javascript"" src=""./Rms.js""></Script>");
                    Response.Write(Rms.Web.JavaScript.ScriptStart);
                    Response.Write("window.opener=null;");
                    string usercode=((User)Session["User"]).UserCode;
                    Response.Write(@" var win = OpenFullWindow('frame.htm','������Ŀ����ϵͳ" + (usercode=="0"?(DateTime.Now.Minute.ToString()+DateTime.Now.Second.ToString()):usercode) + "'); ");
                    Response.Write(@" if ( win != this ) { win.opener = null;window.open('', '_parent', ''); window.close(); }");
                    Response.Write(Rms.Web.JavaScript.ScriptEnd);
                }

            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "�û���¼ʧ��");
                Response.Write(Rms.Web.JavaScript.Alert(true, "�û���¼ʧ�ܣ�" + ex.Message));
            }
        }
		#region ȡ������
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
				this.lblMessage.Text = "�������û���";
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
					this.lblMessage.Text = "�û�������";
				}
				else
				{
					DataTable tb = entity.Tables[0];					
					foreach(DataRow dr in tb.Rows)
					{
						if(dr["MailBox"].ToString()=="")
						{
							this.lblMessage.Text = "��û��ϵͳ����";
							throw new Exception("��û������");
						}
						BLL.MailRule mymail= new RmsPM.BLL.MailRule();
						mymail.Title = userID+" Pmϵͳ����";
						mymail.ToMail=dr["MailBox"].ToString().Split(new char[] { ';' })[0];
						mymail.Addressee=userID;
						mymail.ShowMode="1";
						mymail.SendName="ϵͳ��������";
						mymail.Body=" PMϵͳ:�û���:"+dr["userID"].ToString()+" ����: "+dr["UserName"].ToString()+" ����: "+dr["PassWord"].ToString();
						mymail.sendMail();
						this.lblMessage.Text="�����ѷ��͵���������";
						//mymail.Body = "�װ���:"entity.Tables[0].Rows[""]"";
					}
				}
			}
			catch( Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"ȡ������ʧ��");
				Response.Write(Rms.Web.JavaScript.Alert(true, "ȡ������ʧ�ܣ�" + ex.Message));
			}
		}*/
		#endregion

    }

}
