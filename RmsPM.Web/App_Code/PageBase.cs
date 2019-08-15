using System;
using System.Configuration;
using System.Web;
using RmsPM;
using System.Web.UI;
using System.Collections;

namespace RmsPM.Web
{
	/// <summary>
	/// PageBase ��ժҪ˵����
	/// </summary>
	public class PageBase : System.Web.UI.Page
	{
		protected string ProjectCode="";
		protected string SubjectSetCode = "1";
		protected User user = null;
        protected ProjectInfo project = null;
        protected string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["RmsPM.Data.ConnectionString"].ConnectionString;

		/// <summary>
		/// �Ƿ�ʹ���µĺ�ͬ�ṹ����ͬ��ϸ����ͬ�ƻ��ֿ���
		/// </summary>
		public bool IsContractNew 
		{
			get {return BLL.ConvertRule.ToString(Application["IsContractNew"])=="1";}
		}

        public string up_sPMName
        {
            get 
            {
                return System.Configuration.ConfigurationManager.AppSettings["PMName"] == null ? "" : System.Configuration.ConfigurationManager.AppSettings["PMName"].ToString();
            }
        }

        public string up_sPMNameLower
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["PMName"] == null ? "" : System.Configuration.ConfigurationManager.AppSettings["PMName"].ToString().ToLower();
            }
        }

        public string up_sConnectionString
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["PMName"] == null ? "" : System.Configuration.ConfigurationManager.ConnectionStrings["RmsPM.Data.ConnectionString"].ConnectionString .ToLower();
            }
        }

        #region ��һ�û���¼����

        /// <summary>
        /// ͬ���û��ٴε�¼ʱ������ǰ���û�
        /// </summary>
        /// <param name="user"></param>
        /// <param name="UserTable"></param>
        /// <param name="KilledUserTable"></param>
        /// <param name="Session"></param>
        public static void KillUser(User user, Hashtable UserTable, Hashtable KilledUserTable, System.Web.SessionState.HttpSessionState Session)
        {
            try
            {
                //ϵͳ����Ա���û�����Ϊ��0����������
                if (user.UserCode == "0") return;

                ArrayList al = new ArrayList();
                foreach(object objKey in UserTable.Keys)
                {
                    if (objKey.ToString().StartsWith(user.UserCode + ",") && (objKey.ToString() != user.UserCode + "," + Session.SessionID))
                    {
                        if (!KilledUserTable.Contains(objKey.ToString()))
                        {
                            KilledUserTable.Add(objKey.ToString(), user.UserName);
                        }

                        al.Add(objKey.ToString());
//                        UserTable.Remove(objKey);
                    }
                }

                foreach (string key in al)
                {
                    UserTable.Remove(key);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
        
        public PageBase()
		{
		}

		override protected void OnInit(EventArgs e)
		{
			Page.Response.Expires=-1;
			base.OnInit(e);
			this.InitEventHandler();
			//����״̬����admin
            if ((Session["User"] == null) && (Request["DebugUser"]+"" != ""))
            {
                Session["User"] = new User(Request["DebugUser"] + "");
                this.user = (User)Session["User"];

                /********************** �����û�ͳ�� ***************************/
                Application.Lock();
                Hashtable UserTable = (Hashtable)Application["UserTable"];
                if (UserTable != null)
                {
                    if (!UserTable.Contains(user.UserCode + "," + Session.SessionID))
                    {
                        UserTable.Add(user.UserCode + "," + Session.SessionID, user.UserName);
                        Application["UserTable"] = UserTable;
                    }

                    //��һ�û���¼����
                    if (BLL.ConvertRule.ToString(Application["SingleUserLogin"]) == "1")
                    {
                        Hashtable KilledUserTable = (Hashtable)Application["KilledUserTable"];
                        KillUser(user, UserTable, KilledUserTable, Session);
                    }
                }

                Application.UnLock();
                /***************************************************************/
            }

			if ((Session["User"] == null) && (ConfigurationSettings.AppSettings["IsDebug"] == "1") && (ConfigurationSettings.AppSettings["DebugUser"] != ""))
			{
				Session["User"] = new User(ConfigurationSettings.AppSettings["DebugUser"]);
                this.user = (User)Session["User"];

                /********************** �����û�ͳ�� ***************************/
                Application.Lock();
                Hashtable UserTable = (Hashtable)Application["UserTable"];
                if (UserTable != null)
                {
                    if (!UserTable.Contains(user.UserCode + "," + Session.SessionID))
                    {
                        UserTable.Add(user.UserCode + "," + Session.SessionID, user.UserName);
                        Application["UserTable"] = UserTable;
                    }

                    //��һ�û���¼����
                    if (BLL.ConvertRule.ToString(Application["SingleUserLogin"]) == "1")
                    {
                        Hashtable KilledUserTable = (Hashtable)Application["KilledUserTable"];
                        KillUser(user, UserTable, KilledUserTable, Session);
                    }
                }

                Application.UnLock();
                /***************************************************************/
            }

			if ( Session["User"] != null )
			{
				this.user = (User)Session["User"];

				//��������ʱ��ʱ��Ҫ���µ�¼
				if (!Page.IsPostBack) //ҳ��ط�ʱ���жϳ�ʱ
				{
                    if (!user.IsWindowsAuthenticated) //windows�û��Զ���¼ʱ���жϳ�ʱ
                    {
                        decimal LoginTimeOut = BLL.ConvertRule.ToDecimal(Application["LoginTimeOut"]);
                        if (LoginTimeOut > 0)
                        {
                            //������ʱ��
                            if (Session["LastOperTime"] != null)
                            {
                                TimeSpan ts = DateTime.Now.Subtract((DateTime)Session["LastOperTime"]);
                                if (ts.Minutes > LoginTimeOut) //��ʱ
                                {
                                    Session["User"] = null;
                                }
                            }
                        }
                    }
				}
			}

            //��ֹ������¼ʱ��ȡwindows�û��Զ���¼ 2007.2.24
            if ((Session["User"] == null) && Page.User.Identity.IsAuthenticated)
			{
                string UserID = Page.User.Identity.Name;
                if (UserID.IndexOf("\\") > 0)
                {
                    int num1 = UserID.IndexOf(@"\") + 1;
                    UserID = UserID.Substring(num1, UserID.Length - num1);
                }
                this.user = new User();
                if (user.LoadUserByUserID(UserID))
                {
                    user.IsWindowsAuthenticated = true;
                    Session["User"] = user;

                    /********************** �����û�ͳ�� ***************************/
                    Application.Lock();
                    Hashtable UserTable = (Hashtable)Application["UserTable"];
                    if (!UserTable.Contains(user.UserCode + "," + Session.SessionID))
                    {
                        UserTable.Add(user.UserCode + "," + Session.SessionID, user.UserName);
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

            if (Session["User"] == null) //��Ҫ���µ�½
			{
                //��¼��Ҫ���µ�¼�¼� by Simon
                LogHelper.Info("���µ�¼ IP:" + Request.UserHostAddress + " URL: " + Request.Url);
                
				//��¼ҳ��
                string url=ResolveClientUrl("~/Default.aspx");
				Response.Write( Rms.Web.JavaScript.ScriptStart);
				Response.Write (String.Format( @"  if ( window.parent == null ) window.open('{0}','a'); else  window.parent.open('{0}','a');  ", url) );
				//				Response.Write ( @"  if ( window.parent == null ) window.open('.\\Default.aspx'); else  window.parent.open('.\\Default.aspx');  " );
				//				Response.Write( Rms.Web.JavaScript.WinOpenMax(false,@"\Default.aspx",""));
				//				Response.Write( Rms.Web.JavaScript.WinClose(false));
				Response.Write ( @"  if ( window.parent == null ) { window.opener=null;  window.close() ; } else  { window.parent.opener = null; window.parent.close(); } " );
				Response.Write( Rms.Web.JavaScript.ScriptEnd);
				Response.End();
			}

            //��һ�û���¼����
            if (BLL.ConvertRule.ToString(Application["SingleUserLogin"]) == "1")
            {
                Hashtable KilledUserTable = (Hashtable)Application["KilledUserTable"];

                if (KilledUserTable != null)
                {
                    if (KilledUserTable.Contains(user.UserCode + "," + Session.SessionID))
                    {
                        //��¼�������¼�
                        LogHelper.Info("�û������� IP:" + Request.UserHostAddress + " URL: " + Request.Url);

                        Session.Abandon();

                        Response.Write(Rms.Web.JavaScript.Alert(true, "��������"));

                        //��¼ҳ��
                        string url = ResolveClientUrl("~/Default.aspx");
                        Response.Write(Rms.Web.JavaScript.ScriptStart);
                        Response.Write(String.Format(@"  if ( window.parent == null ) window.open('{0}','a'); else  window.parent.open('{0}','a');  ", url));
                        Response.Write(@"  if ( window.parent == null ) { window.opener=null;  window.close() ; } else  { window.parent.opener = null; window.parent.close(); } ");
                        Response.Write(Rms.Web.JavaScript.ScriptEnd);

                        //�嵯���û��б�
                        Application.Lock();
                        KilledUserTable.Remove(user.UserCode + "," + Session.SessionID);
                        Application["KilledUserTable"] = KilledUserTable;
                        Application.UnLock();

                        Response.End();
                    }
                }
            }


//			if (Session["ProjectCode"] != null) 
//			{
//				ProjectCode = Session["ProjectCode"].ToString();
//			}

			if (Session["Project"] != null) 
			{
				project = (ProjectInfo)Session["Project"];
			}

			//��¼������ʱ��
			Session["LastOperTime"] = DateTime.Now;
		}

		virtual protected void InitEventHandler()
		{
		}
	}
}
