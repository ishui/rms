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
	/// Global 的摘要说明。
	/// </summary>
	public class Global : System.Web.HttpApplication
	{
		/// <summary>
		/// 必需的设计器变量。
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
			//记录application 开始时间 by simon
            SetLog4Net();
            LogHelper.WriteLog("系统开始运行");
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

			/********************** 在线用户统计 ***************************/
			Hashtable UserTable = new Hashtable();
            Application.Lock();
            Application["UserTable"] = UserTable;
            Application.UnLock();
            /***************************************************************/

            //单一用户登录
            Application["SingleUserLogin"] = BLL.ConvertRule.ToString(System.Configuration.ConfigurationManager.AppSettings["SingleUserLogin"]);

            //被弹出的用户列表
            Hashtable KilledUserTable = new Hashtable();
            Application.Lock();
            Application["KilledUserTable"] = KilledUserTable;
            Application.UnLock();

			//登录超时时间
			Application["LoginTimeOut"] = BLL.ConvertRule.ToDecimal(System.Configuration.ConfigurationSettings.AppSettings["LoginTimeOut"]);

			//是否使用新的合同结构（合同明细、合同计划分开）
			Application["IsContractNew"] = "1";//System.Configuration.ConfigurationSettings.AppSettings["IsContractNew"];

            //营销系统接口
            Application["SalServiceUrl"] = BLL.ConvertRule.ToString(System.Configuration.ConfigurationSettings.AppSettings["SalServiceUrl"]);

            /********************** 定时器启动 ***************************/
            if (System.Configuration.ConfigurationSettings.AppSettings["SendMailTime"] != null)
            {
                //创建一个新的Timer实例
                if (sysTimer == null)
                {
                    sysTimer = new System.Timers.Timer();
                }
                //将sysTimer_Elapsed指定为计时器的 Elapsed 事件处理程序 
                sysTimer.Elapsed += new System.Timers.ElapsedEventHandler(sysTimer_Elapsed);
                sysTimer.Interval = nInterMin * 60 * 1000;
                sysTimer.AutoReset = true;
                sysTimer.Enabled = true;
                //ApplicationLog.WriteLog("SendMailTime", "定时器已经启动,邮件发送时间为" + System.Configuration.ConfigurationSettings.AppSettings["SendMailTime"].ToString());
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
            ApplicationLog.WriteLog(this.ToString(), "applicatin_error事件触发");
            Exception objExp = HttpContext.Current.Server.GetLastError();
            LogHelper.Warn("Error", objExp.InnerException);
		}

		protected void Session_End(Object sender, EventArgs e)
		{
			DeleteTempFile();
            try
            {
                /********************** 在线用户统计 ***************************/
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
            LogHelper.Info("applicatin_end事件触发"+this.ToString());
            LogHelper.Info("系统结束运行");
		}

		/// <summary>
		/// 删除临时路径（temp\SessionID）
		/// </summary>
		private void DeleteTempFile() 
		{
			try 
			{
                //ToDo:还要清无效的attachment,以---开头的attachmentclass。
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
                LogHelper.Info( "删除临时路径出错",ex);
			}
		}

        /// <summary>
        /// 定时器事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sysTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //为了实现在某时间点开始执行,要做一些判断工作
            LogHelper.Info("SendMailTime"+"定时器事件已经触发");
            try
            {
                sysTimer.Stop();
                if (!(DateTime.Now.DayOfWeek != DayOfWeek.Saturday && DateTime.Now.DayOfWeek == DayOfWeek.Sunday))//排除周末
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
                        // 当天待办邮件发送
                        BLL.EmailTimer.SendEmail(BLL.ConvertRule.ToString(System.Configuration.ConfigurationSettings.AppSettings["SystemUrl"].ToString()), BLL.ConvertRule.ToString(System.Configuration.ConfigurationSettings.AppSettings["SendMailContent"].ToString()));
                        LogHelper.Info("SendMailTime 定时器邮件发送成功!!!!!!!!!!!");
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("定时发送邮件出现错误！",ex );
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
        /// 初始化合同参与人有权限的操作
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
        
        #region Web 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{    
			this.components = new System.ComponentModel.Container();
		}
		#endregion
	}
}
