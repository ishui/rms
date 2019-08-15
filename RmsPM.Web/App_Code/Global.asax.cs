using System;
using System.Collections;
using System.ComponentModel;
using System.Web;
using System.Web.SessionState;
using Rms.ORMap;
using System.Data;
using Rms.LogHelper;


namespace RmsPM.Web
{
	/// <summary>
	/// Global ��ժҪ˵����
	/// </summary>
	public class Global : System.Web.HttpApplication
	{
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.IContainer components = null;
        static System.Timers.Timer sysTimer;
        private int nInterMin = 10;

//        public static ArrayList ContractActorOperationList = null;

		public Global()
		{
			InitializeComponent();
		}	
		
		protected void Application_Start(Object sender, EventArgs e)
		{
			//��¼application ��ʼʱ�� by simon
            SetLog4Net();
            LogHelper.WriteLog("ϵͳ��ʼ����");
            IAppConfigSetter setter=new AppConfigSetter();
			ApplicationConfiguration.SetAppConfiguration(setter);
			ClassBuilderManager.LoadClassBuilderDefine ();

			ApplicationConfigSetup acs = new ApplicationConfigSetup();
			acs.LoadLogPath();
			acs.Dispose();


			BLL.WorkFlowSystemCode wfs = new RmsPM.BLL.WorkFlowSystemCode();
			Rms.WorkFlow.InterfaceManager.iSystemCode = wfs;

			BLL.WorkFlowDefine wfd = new RmsPM.BLL.WorkFlowDefine();
			Rms.WorkFlow.InterfaceManager.iDefinition = wfd;

			BLL.WorkFlowCaseIO wfci = new BLL.WorkFlowCaseIO();
			Rms.WorkFlow.InterfaceManager.iWorkCase = wfci;

			RmsPM.DAL.QueryStrategy.SystemClassDescription.LoadItem();

			string vPath = Server.MapPath(System.Configuration.ConfigurationSettings.AppSettings["VirtualDirectory"]);
			Application["VirtualMapPath"] = vPath;

			AvailableFunction.LoadAvailableFunction();

			/********************** �����û�ͳ�� ***************************/
			Hashtable UserTable = new Hashtable();
            Application.Lock();
            Application["UserTable"] = UserTable;
            Application.UnLock();
            /***************************************************************/

            //��һ�û���¼
            Application["SingleUserLogin"] = BLL.ConvertRule.ToString(System.Configuration.ConfigurationManager.AppSettings["SingleUserLogin"]);

            //���������û��б�
            Hashtable KilledUserTable = new Hashtable();
            Application.Lock();
            Application["KilledUserTable"] = KilledUserTable;
            Application.UnLock();

			//��¼��ʱʱ��
			Application["LoginTimeOut"] = BLL.ConvertRule.ToDecimal(System.Configuration.ConfigurationSettings.AppSettings["LoginTimeOut"]);

			//�Ƿ�ʹ���µĺ�ͬ�ṹ����ͬ��ϸ����ͬ�ƻ��ֿ���
			Application["IsContractNew"] = "1";//System.Configuration.ConfigurationSettings.AppSettings["IsContractNew"];

            //Ӫ��ϵͳ�ӿ�
            Application["SalServiceUrl"] = BLL.ConvertRule.ToString(System.Configuration.ConfigurationSettings.AppSettings["SalServiceUrl"]);

            /********************** ��ʱ������ ***************************/
            if (System.Configuration.ConfigurationSettings.AppSettings["SendMailTime"] != null)
            {
                //����һ���µ�Timerʵ��
                if (sysTimer == null)
                {
                    sysTimer = new System.Timers.Timer();
                }
                //��sysTimer_Elapsedָ��Ϊ��ʱ���� Elapsed �¼�������� 
                sysTimer.Elapsed += new System.Timers.ElapsedEventHandler(sysTimer_Elapsed);
                sysTimer.Interval = nInterMin * 60 * 1000;
                sysTimer.AutoReset = true;
                sysTimer.Enabled = true;
                //ApplicationLog.WriteLog("SendMailTime", "��ʱ���Ѿ�����,�ʼ�����ʱ��Ϊ" + System.Configuration.ConfigurationSettings.AppSettings["SendMailTime"].ToString());
            }
            /***************************************************************/
            Application["GridPageSize"] = 14;

            InitContractActorOperationList();
		}
 
		protected void Session_Start(Object sender, EventArgs e)
		{
			Session["Project"] = new ProjectInfo();
		}

		protected void Application_BeginRequest(Object sender, EventArgs e)
		{

		}

		protected void Application_EndRequest(Object sender, EventArgs e)
		{

		}

		protected void Application_AuthenticateRequest(Object sender, EventArgs e)
		{

		}

		protected void Application_Error(Object sender, EventArgs e)
		{
            ApplicationLog.WriteLog(this.ToString(), "applicatin_error�¼�����");
            Exception objExp = HttpContext.Current.Server.GetLastError();
            LogHelper.Warn("Error", objExp.InnerException);
		}

		protected void Session_End(Object sender, EventArgs e)
		{
			DeleteTempFile();
            try
            {
                /********************** �����û�ͳ�� ***************************/
                Application.Lock();
                Hashtable UserTable = (Hashtable)Application["UserTable"];
                if (UserTable.Contains(((User)Session["user"]).UserCode+","+Session.SessionID))
                {
                    UserTable.Remove(((User)Session["user"]).UserCode + "," + Session.SessionID);
                    Application["UserTable"] = UserTable;
                }
                Application.UnLock();
                /***************************************************************/
            }
            catch (Exception ex)
            {
                LogHelper.Warn(this.ToString(), ex);
            }
		}

		protected void Application_End(Object sender, EventArgs e)
		{
            if (sysTimer != null)
                sysTimer.Dispose();
            LogHelper.Info("applicatin_end�¼�����"+this.ToString());
            LogHelper.Info("ϵͳ��������");
		}

		/// <summary>
		/// ɾ����ʱ·����temp\SessionID��
		/// </summary>
		private void DeleteTempFile() 
		{
			try 
			{
                //ToDo:��Ҫ����Ч��attachment,��---��ͷ��attachmentclass��
				//				string vPath = Server.MapPath(System.Configuration.ConfigurationSettings.AppSettings["VirtualDirectory"]);
				string vPath = Application["VirtualMapPath"].ToString();
				if (vPath != "") 
				{
                    if (!vPath.EndsWith("\\"))
                        vPath += "\\";

//                    string TempPath = System.Configuration.ConfigurationSettings.AppSettings["ApplicationTempPath"];
                    string TempPath = "Temp\\";
					if (TempPath != "") 
					{
						TempPath = vPath + TempPath + Session.SessionID.ToString();
						if (System.IO.Directory.Exists(TempPath)) 
						{
							System.IO.Directory.Delete(TempPath, true);
						}
					}
				}
			}
			catch (Exception ex)
			{
                LogHelper.Info( "ɾ����ʱ·������",ex);
			}
		}

        /// <summary>
        /// ��ʱ���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sysTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //Ϊ��ʵ����ĳʱ��㿪ʼִ��,Ҫ��һЩ�жϹ���
            LogHelper.Info("SendMailTime"+"��ʱ���¼��Ѿ�����");
            try
            {
                sysTimer.Stop();
                if (!(DateTime.Now.DayOfWeek != DayOfWeek.Saturday && DateTime.Now.DayOfWeek == DayOfWeek.Sunday))//�ų���ĩ
                {
                    string strTime = System.Configuration.ConfigurationSettings.AppSettings["SendMailTime"].ToString();
                    int nIndex = strTime.IndexOf(':');
                    string strHour, strMin;
                    if (nIndex > 0)
                    {
                        int nLength = strTime.Length;
                        strHour = strTime.Substring(0, nIndex);
                        strMin = strTime.Substring(nIndex + 1, nLength - nIndex - 1);
                    }
                    else
                    {
                        strHour = strTime;
                        strMin = "0";
                    }

                    int nHour = int.Parse(strHour);
                    int nMin = int.Parse(strMin);
                    int nNowHour = DateTime.Now.Hour;
                    int nNowMin = DateTime.Now.Minute;

                    if (nMin < nInterMin)
                    {
                        nHour = nHour - 1;
                        if (nHour == 0)
                        {
                            nHour = 23;
                        }
                        nMin = nMin + 60 - nInterMin;
                    }

                    string strNowHour = nNowHour.ToString();
                    string strNowMin = nNowMin.ToString();
                    strHour = nHour.ToString();
                    strMin = nMin.ToString();

                    strNowHour = strNowHour.PadLeft(2, '0');
                    strNowMin = strNowMin.PadLeft(2, '0');
                    strHour = strHour.PadLeft(2, '0');
                    strMin = strMin.PadLeft(2, '0');

                    //LogHelper.Info("NowTime = " + strNowHour + ":" + strNowMin + "<br>" + "SetTime = " + strHour + ":" + strMin);

                    if ((nNowHour == nHour) && ((nMin - nNowMin) < nInterMin) && ((nMin - nNowMin) >= 0))
                    {                        
                        // ��������ʼ�����
                        BLL.EmailTimer.SendEmail(BLL.ConvertRule.ToString(System.Configuration.ConfigurationSettings.AppSettings["SystemUrl"].ToString()), BLL.ConvertRule.ToString(System.Configuration.ConfigurationSettings.AppSettings["SendMailContent"].ToString()));
                        LogHelper.Info("SendMailTime ��ʱ���ʼ����ͳɹ�!!!!!!!!!!!");
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("��ʱ�����ʼ����ִ���",ex );
            }
            finally
            {
                sysTimer.Start();
            }
        }

        void SetLog4Net()
        {
            string configFileName = System.Configuration.ConfigurationSettings.AppSettings["logFileName"];
            if (configFileName == string.Empty || configFileName == null)
            {
                configFileName = "log.log4net";
            }
            configFileName = Server.MapPath("~") + "\\" + configFileName;
            System.IO.FileInfo f = new System.IO.FileInfo(configFileName);
            LogHelper.SetConfig(f, System.Configuration.ConfigurationManager.ConnectionStrings["RmsPM.Data.ConnectionString"].ConnectionString );

        }

        /// <summary>
        /// ��ʼ����ͬ��������Ȩ�޵Ĳ���
        /// </summary>
        private void InitContractActorOperationList()
        {
            try
            {
                ArrayList ar = new ArrayList();

                string ops = BLL.ConvertRule.ToString(System.Configuration.ConfigurationManager.AppSettings["ContractActorOperation"]);
                foreach (string op in ops.Split(","[0]))
                {
                    ar.Add(op);
                }

                /*
                QueryAgent qa = new QueryAgent();
                string sql = "select FunctionStructureCode from FunctionStructure where FunctionStructureCode like '0501%' and IsAvailable = 0";
                DataTable tb = qa.ExecSqlForDataSet(sql).Tables[0];
                foreach (DataRow dr in tb.Rows)
                {
                    ar.Add(dr["FunctionStructureCode"].ToString());
                }
                tb.Dispose();
                qa.Dispose();
                */

		Application["ContractActorOperationList"] = ar;

//                ContractActorOperationList = ar;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        #region Web ������������ɵĴ���
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{    
			this.components = new System.ComponentModel.Container();
		}
		#endregion
	}
}
