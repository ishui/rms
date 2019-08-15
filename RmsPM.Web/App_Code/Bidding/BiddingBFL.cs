using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Rms.DBUtility;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Xml;
using Rms.ORMap;
using RmsPM.Web;
using RmsPM.BLL;
//using MailHelper;
namespace RmsPM.BFL
{
    /// <summary>
    /// BiddingBFL 的摘要说明



    /// </summary>
    public class BiddingBFL
    {
        public BiddingBFL()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        public static List<string[]> ListBiddingOpener(string BiddingEmitCode)
        {
            string ConnStr = SqlHelper.DBConnString;
            List<string[]> ReturnValue = new List<string[]>();
            string SqlStr = string.Format("select OpenerCode,OpenTime from BiddingOpen where BiddingEmitCode='{0}'",BiddingEmitCode);

            using (SqlDataReader sdr = Rms.DBUtility.SqlHelper.ExecuteReader(ConnStr, CommandType.Text, SqlStr))
            {
                while (sdr.Read())
                {
                    string[] tmp = new string[2];
                    tmp[0] = sdr["OpenerCode"].ToString();
                    tmp[1] = sdr["OpenTime"].ToString();
                    ReturnValue.Add(tmp);
                }
            }
            return ReturnValue;
        }

        public static List<string> ListAvaiableBiddingOpener()
        {
            string ConnStr = SqlHelper.DBConnString;
            List<string> ReturnValue = new List<string>();
            string SqlStr = "select distinct ur.UserCode from UserRole ur,Station st,RoleOperation ro ";
            SqlStr += "where ro.RoleCode=st.RoleCode and st.StationCode=ur.StationCode and ro.OperationCode='210302'";

            using (SqlDataReader sdr = Rms.DBUtility.SqlHelper.ExecuteReader(ConnStr, CommandType.Text, SqlStr))
            {
                
                while (sdr.Read())
                {
                    ReturnValue.Add(sdr["UserCode"].ToString());
                }
            }
            return ReturnValue;
        }

        public static void InsertBiddingOpener(string BiddingEmitCode, string OpenerCode)
        {
            string ConnStr = SqlHelper.DBConnString;
            string SqlStr = string.Format("insert into BiddingOpen(BiddingEmitCode,OpenerCode) values('{0}','{1}')", BiddingEmitCode, OpenerCode);
            SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, SqlStr);
        }
        public static void UpdateEmit_AllowAfterClose(string BiddingEmitCode, int value)
        {
            string ConnStr = SqlHelper.DBConnString;
            string SqlStr = string.Format("update BiddingEmit set AllowAfterClose={0} where BiddingEmitCode={1}",value,BiddingEmitCode);
            SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, SqlStr);
        }
        public static void UpdateEmit_IsWsZTB(string BiddingEmitCode, int value)
        {
            string ConnStr = SqlHelper.DBConnString;
            string SqlStr = string.Format("update BiddingEmit set IsWsZTB={0} where BiddingEmitCode={1}", value, BiddingEmitCode);
            SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, SqlStr);
        }
        public static void SetEmit_State(string BiddingEmitCode, int value)
        {
            string ConnStr = SqlHelper.DBConnString;
            string SqlStr = string.Format("update BiddingEmit set State={0} where BiddingEmitCode={1}", value, BiddingEmitCode);
            SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, SqlStr);
          
        }
        public static int GetEmit_State(string BiddingEmitCode)
        {
            string ConnStr = SqlHelper.DBConnString;
            string SqlStr = string.Format("select State from BiddingEmit  where BiddingEmitCode={0}",  BiddingEmitCode);
            object obj = SqlHelper.ExecuteScalar(ConnStr, CommandType.Text, SqlStr);
            if (obj != DBNull.Value)
            {
                return Convert.ToInt32(obj);
            }
            else
            {
                return 0;
            }
        }
        public static void SetEmitOrderCode(string BiddingEmitCode)
        {
            string ConnStr = SqlHelper.DBConnString;
            string SqlStr = string.Format("update BiddingReturn set State='{0}' where BiddingEmitCode={1}", "", BiddingEmitCode);
            SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, SqlStr);

            string TempBiddingDtlCode = "";
            string TempMoney = "0";
            Int32 TempState = 1;
            //State字段中存放商务标排名，规则为：如果回标金额为0或者为空时不参与商务标排名；金额一样的单位，名次一样。

            SqlStr = string.Format("select * from BiddingReturn where BiddingEmitCode={0} and isnull(Money,0)<>0 order by BiddingDtlCode,isnull(Money,0)", BiddingEmitCode);
            using (SqlDataReader reader = SqlHelper.ExecuteReader(ConnStr, CommandType.Text, SqlStr))
            {
                while (reader.Read())
                {
                    if (TempBiddingDtlCode != reader["BiddingDtlCode"].ToString())
                    {
                        TempState = 1;
                        TempMoney = "0";
                        TempBiddingDtlCode = reader["BiddingDtlCode"].ToString();
                    }
                    SqlStr = string.Format("update BiddingReturn set State={0} where BiddingReturnCode={1}", TempState.ToString(), reader["BiddingReturnCode"].ToString());
                    SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, SqlStr);
                    if (!reader["Money"].ToString().Equals(TempMoney))
                    {
                        TempState++;
                        TempMoney = reader["Money"].ToString();
                    }                    
                }
            }
        }
        public static void SetBiddingState(string BiddingCode, int value)
        {
            string ConnStr = SqlHelper.DBConnString;
            string SqlStr = string.Format("update Bidding set State={0} where BiddingCode={1}", value, BiddingCode);
            SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, SqlStr);            
        }
        public static int GetBiddingState(string BiddingCode)
        {
            string ConnStr = SqlHelper.DBConnString;
            string SqlStr = string.Format("select State from Bidding  where BiddingCode={0}", BiddingCode);
            object obj = SqlHelper.ExecuteScalar(ConnStr, CommandType.Text, SqlStr);
            if (obj != DBNull.Value)
            {
                return Convert.ToInt32(obj);
            }
            else
            {
                return 0;
            }        
        }
        /// <summary>
        /// 产生全数字XXXX-XXXX序列号，并到BiddingEmitTo.SerialNum查重，返回非重号
        /// </summary>
        /// <returns></returns>
        public static string NewBiddingSN()
        {
            string ReturnValue;
            Random objRam = new Random();
            ReturnValue = objRam.Next(1000, 9999).ToString() + "-" + objRam.Next(1000, 9999).ToString();
            
            string ConnStr = SqlHelper.DBConnString;
            string SqlStr =string.Format( "Select count(*) from BiddingEmitTo where SerialNum='{0}'",ReturnValue);
            if (Convert.ToInt32(SqlHelper.ExecuteScalar(ConnStr, CommandType.Text, SqlStr)) ==0 )
            {
                return ReturnValue;
            }
            else
            {
                return NewBiddingSN();
            }
        }

        /// <summary>
        /// 重新生成全数字XXXX-XXXX序列号，并到BiddingEmitTo.SerialNum查重，返回非重号
        /// </summary>
        /// <returns></returns>
        private string ReBiddingSN()
        {
            string ReturnValue;
            Random objRam = new Random();
            ReturnValue = objRam.Next(1000, 9999).ToString() + "-" + objRam.Next(1000, 9999).ToString();

            string ConnStr = SqlHelper.DBConnString;
            string SqlStr = string.Format("Select count(*) from BiddingEmitTo where SerialNum='{0}'", ReturnValue);
            if (Convert.ToInt32(SqlHelper.ExecuteScalar(ConnStr, CommandType.Text, SqlStr)) == 0)
            {
                return ReturnValue;
            }
            else
            {
                return ReBiddingSN();
            }            
        }

        /// <summary>
        /// 产生6位随机数字密码


        /// </summary>
        /// <returns></returns>
        public static string NewPassword()
        {
            Random objRam = new Random();
            return objRam.Next(100000, 999999).ToString();            
        }

        /// <summary>
        /// 
        /// 重新生成6位随机数字密码

        /// </summary>
        /// <returns></returns>
        public string RePassword()
        {
            Random objRam = new Random();
            return objRam.Next(100000, 999999).ToString();                
        }

        /// <summary>
        /// 判断某人是否可开某个标


        /// </summary>
        /// <param name="BiddingEmitCode"></param>
        /// <param name="UserCode"></param>
        /// <returns></returns>
        public static bool CanOpenNow(string BiddingEmitCode, string UserCode)
        {
            string ConnStr = SqlHelper.DBConnString;
            string SqlStr = string.Format("select * from BiddingOpen where BiddingEmitCode='{0}' and OpenerCode='{1}'", BiddingEmitCode,UserCode );
            using (SqlDataReader reader = SqlHelper.ExecuteReader(ConnStr, CommandType.Text, SqlStr))
            {
                if (reader.Read())
                {
                    if (reader["OpenTime"] == DBNull.Value)
                    {
                        SqlStr = string.Format("select * from BiddingEmit where State=0 and BiddingEmitCode='{0}'", BiddingEmitCode);
                        using (SqlDataReader readEmit = SqlHelper.ExecuteReader(ConnStr, CommandType.Text,SqlStr))
                        {
                            if (readEmit.Read())
                            {
                                if (Convert.ToDateTime(readEmit["PrejudicationDate"]) > DateTime.Now)
                                {
                                    return false;
                                }
                                else
                                {
                                    return true;
                                }
                            }
                            else
                            { //没找到发标纪录，不太可能
                                return false;
                            }
                        }
                    }
                    else
                    {//该开标人已开过标
                        return false;
                    }
                }
                else
                { // 非开标人
                    return false;
                }
            }
        }

        public static void BiddingOpen(string BiddingEmitCode,string UserCode)
        {
            string ConnStr = SqlHelper.DBConnString;
            string SqlStr = string.Format("select * from BiddingEmit where State=0 and BiddingEmitCode='{0}'", BiddingEmitCode);
            using (SqlDataReader readEmit = SqlHelper.ExecuteReader(ConnStr, CommandType.Text, SqlStr))
            {
                if (readEmit.Read())
                {
                    if (Convert.ToDateTime(readEmit["PrejudicationDate"]) <= DateTime.Now)
                    {
                        SqlStr = "update BiddingOpen set OpenTime='{0}' where OpenTime is null and BiddingEmitCode='{1}' and OpenerCode='{2}'";
                        SqlStr = string.Format(SqlStr, DateTime.Now.ToString(), BiddingEmitCode, UserCode);
                        int ret = SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, SqlStr);
                        if (ret == 1)
                        {
                            SqlStr = string.Format("select Count(*) from BiddingOpen where BiddingEmitCode='{0}' and OpenTime is null",BiddingEmitCode);
                            ret = Convert.ToInt32(SqlHelper.ExecuteScalar(ConnStr, CommandType.Text, SqlStr));
                            if (ret == 0)
                            {
                                SetEmit_State(BiddingEmitCode, 1);
                                SetEmitOrderCode(BiddingEmitCode);
                                SetBiddingState(readEmit["BiddingCode"].ToString(), 3);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 重新生成序列号、密码并邮件通知供应商

        /// </summary>
        /// <param name="BiddingEmitCode">发标编号</param>
        /// <param name="EmailTemplateFileName">邮件模板XML文件名</param>
        /// <returns></returns>
        public static void Emit_LastSend(string BiddingEmitCode,string EmailTemplateFileName)
        {
            QueryAgent qa = new QueryAgent();
            string SqlStr = "";
            SqlStr = string.Format("select BiddingEmitToCode from BiddingEmitTo where BiddingEmitCode={0}",BiddingEmitCode);
            DataTable dt = qa.ExecSqlForDataSet(SqlStr).Tables[0];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string pwd = NewPassword();
                    string sn = NewBiddingSN();
                    string sql = string.Format("Update BiddingEmitTo set SerialNum='" + sn + "',SuppPwd='" + pwd + "' where BiddingEmitToCode={0}", dt.Rows[i]["BiddingEmitToCode"].ToString());
                    qa.ExecuteSql(sql);
                }
            }
            dt.Dispose();
            Emit_SendMail(BiddingEmitCode, EmailTemplateFileName);
        }

        /// <summary>
        /// 网上招投标发送Email通知供应商用户名密码
        /// </summary>
        /// <param name="BiddingEmitCode">发标编号</param>
        /// <param name="EmailTemplateFileName">邮件模板XML文件名</param>
        /// <returns></returns>
        public static void Emit_SendMail(string BiddingEmitCode, string EmailTemplateFileName)
        {
            QueryAgent qa = new QueryAgent();
            string SqlStr = "";
            SqlStr = string.Format("select BiddingEmitToCode,SupplierCode,SerialNum,SuppPwd from BiddingEmitTo where BiddingEmitCode={0}", BiddingEmitCode);
            DataTable dt = qa.ExecSqlForDataSet(SqlStr).Tables[0];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    SendEmail(dt.Rows[i]["SupplierCode"].ToString(), dt.Rows[i]["BiddingEmitToCode"].ToString(), dt.Rows[i]["SerialNum"].ToString(), dt.Rows[i]["SuppPwd"].ToString(), EmailTemplateFileName);
                }
            }
            dt.Dispose();
        }
        

        /// <summary>
        /// 网上招投标发送Email通知供应商用户名密码，单个供应商
        /// </summary>
        /// <param name="SupplierCode">供应商编号</param>
        /// <param name="EmitToCode">投标编号</param>
        /// <param name="pwd">密码</param>
        /// <param name="gid">用户名</param>
        /// <param name="EmailTemplateFileName">邮件模板XML文件名</param>
        private static void SendEmail(string SupplierCode, string EmitToCode, string pwd, string gid, string EmailTemplateFileName)
        {
            try
            {
                string Title = "";
                string MailBody = "";
                string UserMail = "";

                string UserName = "";
                string BiddingTitle = "";

                string MailUser = ConfigurationManager.AppSettings["MailUser"].ToString();
                string MailPwd = ConfigurationManager.AppSettings["MailPwd"].ToString();

                EntityData entity = new EntityData();
                entity = SupplierRule.GetSupplierByCode(SupplierCode);
                if (entity.HasRecord())
                {
                    UserName = entity.CurrentTable.Rows[0]["SupplierName"].ToString();
                    UserMail = entity.CurrentTable.Rows[0]["EMail"].ToString();
                    entity.Dispose();

                    if (UserMail != "")
                    {
                        Bidding cBidding = new Bidding();
                        BiddingEmit cBiddingEmit = new BiddingEmit();
                        StandardEntityDAO dao = new StandardEntityDAO("BiddingEmitTo");
                        string BiddingCode = cBiddingEmit.GetBiddingEmitByEmitToCode(dao, EmitToCode).CurrentRow["BiddingCode"].ToString();


                        BiddingTitle = cBidding.GetBiddingName(BiddingCode);

                        XMLTreeViewManager vm = new XMLTreeViewManager(EmailTemplateFileName);
                        XmlDocument doc = new XmlDocument();
                        doc.Load(vm.m_XMLFileName);
                        XmlNode EmailTypeNode = doc.DocumentElement.SelectSingleNode("BiddingEmitTo");
                        if (EmailTypeNode != null)
                        {
                            Title = EmailTypeNode.SelectSingleNode("Title").InnerText;
                            MailBody = EmailTypeNode.SelectSingleNode("MailBody").InnerText;
                        }

                        Title = Title.Replace("#BiddingTitle#", BiddingTitle);
                        Title = Title.Replace("#Password#", pwd);
                        Title = Title.Replace("#GID#", gid);
                        MailBody = MailBody.Replace("#BiddingTitle#", BiddingTitle);
                        MailBody = MailBody.Replace("#Password#", pwd);
                        MailBody = MailBody.Replace("#GID#", gid);
                        BLL.MailRule mail = new BLL.MailRule();
                        mail.Title = Title;
                        mail.Body = MailBody;
                        mail.ToMail = UserMail;
                        mail.sendMail();

                        EmailHistoryInsert("BiddingEmitTo", EmitToCode, Title, UserMail, MailUser, MailBody, DateTime.Now);
                    }
                }
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog("BiddingBFL", ex, ex.Message);
            }
        }

        /// <summary>
        /// 保存邮件发送历史记录

        /// </summary>
        /// <param name="EmailType">邮件类型</param>
        /// <param name="MasterCode">相关主键</param>
        /// <param name="EmailTitle">邮件主题</param>
        /// <param name="Receiver">接受者</param>
        /// <param name="Sender">发送者</param>
        /// <param name="EmailContent">邮件内容</param>
        /// <param name="SendDate">发送日期</param>
        private static void EmailHistoryInsert(string EmailType, string MasterCode, string EmailTitle, string Receiver, string Sender, string EmailContent, DateTime SendDate)
        {
            string SqlStr = "";
            SqlStr = "Insert into EmailHistory values (";
            SqlStr += "'" + DAL.EntityDAO.SystemManageDAO.GetNewSysCode("EmailHistory") + "'";
            SqlStr += ",'" + EmailType + "','" + MasterCode + "','" + EmailTitle + "'";
            SqlStr += ",'" + Receiver + "','" + Sender + "','" + EmailContent + "','" + DateTime.Now + "'";
            SqlStr += ")";
            QueryAgent qa = new QueryAgent();
            qa.ExecuteSql(SqlStr);
            qa.Dispose();
        }
    }
}
