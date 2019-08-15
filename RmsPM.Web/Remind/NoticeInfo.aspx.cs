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
using Rms.Web;
using RmsPM.DAL.EntityDAO;
using RmsPM.BLL;
using RmsPM.Web;
using RmsPM.DAL.QueryStrategy;

namespace RmsPM.Web.Remind
{
    /// <summary>
    /// 查看通知信息
    /// </summary>
    public partial class NoticeInfo : RmsPM.Web.PageBase
    {
        protected string strNoticeCode = "";

        protected void Page_Load(object sender, System.EventArgs e)
        {
            try
            {
                this.InitPage();
                if (!this.IsPostBack)
                {
                    // 在此处放置用户代码以初始化页面

                    this.LoadData();
                }
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "载入通知失败");
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

        private void InitPage()
        {
            strNoticeCode = Request.QueryString["Code"] + "";
            string strAction = Request.QueryString["Action"] + "";
            User myUser = new User(user.UserCode);
            //if(!myUser.HasResourceRight(this.strNoticeCode,"080102"))
            //Server.Transfer("../Remind/NoticeInfo.aspx?&Code="+this.strNoticeCode);

            //this.btDelete.Visible = myUser.HasOperationRight("080103");// 080103为通知删除权限
            try
            {
                //QueryAgent qa = new QueryAgent();
                //DAL.QueryStrategy.RoleOperation sb = new RmsPM.DAL.QueryStrategy.RoleOperation();
                //sb.AddStrategy(new Strategy(RoleOperationName.UserCode, user.UserCode));
                ////sb.AddStrategy(new Strategy( RoleOperationName.UserCode,"080102"));
                //string sql = sb.BuildMainQueryString();
                //DataSet Ds = qa.ExecSqlForDataSet(sql);
                //string str_Edit = "";
                //string str_Delete = "";
                //for (int i = 0; i < Ds.Tables[0].Rows.Count; i++)
                //{
                //    if (Ds.Tables[0].Rows[i][1].ToString() == "080102")
                //    {
                //        str_Edit = Ds.Tables[0].Rows[i][1].ToString();
                //    }
                //    if (Ds.Tables[0].Rows[i][1].ToString() == "080103")
                //    {
                //        str_Delete = Ds.Tables[0].Rows[i][1].ToString();
                //    }

                //}

                ////判断是否拥有修改权限
                //if (str_Edit == "" && !myUser.HasResourceRight(this.strNoticeCode, "080102"))
                //{
                //    this.SaveToolsButton.Visible = false;
                //}
                //else
                //{
                //    this.SaveToolsButton.Visible = true;
                //}

                //if (str_Delete == "" && !myUser.HasResourceRight(this.strNoticeCode, "080103"))
                //{

                //    this.btDelete.Visible = false;
                //}
                //else
                //{
                //    this.btDelete.Visible = true;
                //}



                //发件人、修改、删除、监控
                bool isSender = false;
                bool canModify = false;
                bool canDelete = false;
                bool isMonitor = false;
                EntityData entityNotice = RemindDAO.GetNoticeByCode(strNoticeCode);
                string l_submitPerson = entityNotice.GetString("SubmitPerson").ToString();
                if (l_submitPerson == user.UserCode)
                {
                    isSender = true;
                }
                if (user.HasRight("080102"))
                {
                    canModify = true;
                }
                if (user.HasRight("080103"))
                {
                    canDelete = true;
                }
                if (user.HasRight("080105"))
                {
                    isMonitor = true;
                }
                if ( isSender && canModify)
                {
                    this.SaveToolsButton.Visible = true;                  
                }
                else
                {
                    this.SaveToolsButton.Visible = false;
                }
                if (isMonitor || (isSender && canDelete))
                {
                    this.btDelete.Visible = true;
                }
                else
                {
                    this.btDelete.Visible = false;
                }
            }
            catch (Exception dd)
            {
                string h = dd.Message.ToString();
                string f = h;
            }
            // 载入附件
            this.myAttachMentList.AttachMentType = "NoticeAttachMent";
            this.myAttachMentList.MasterCode = strNoticeCode;

            FeedBack1.FeedBackType = "Notice";
            FeedBack1.MasterCode = this.strNoticeCode;
        }

        /// <summary>
        /// 验证用户是否拥有删除权限
        /// </summary>
        /// <returns></returns>
        public bool CheckUserDtl(string NoticCode)
        {
            //string stationcode      = "";
            //string RoleCode         = "";
            string OperationCode = "";
            try
            {
                QueryAgent qa = new QueryAgent();

                DAL.QueryStrategy.RoleOperation sb = new RmsPM.DAL.QueryStrategy.RoleOperation();
                sb.AddStrategy(new Strategy(RoleOperationName.UserCode, user.UserCode));
                //sb.AddStrategy(new Strategy( RoleOperationName.UserCode,"080102"));
                string sql = sb.BuildMainQueryString();
                DataSet Ds = qa.ExecSqlForDataSet(sql);
                for (int i = 0; i < Ds.Tables[0].Rows.Count; i++)
                {
                    if (Ds.Tables[0].Rows[i]["OperationCode"].ToString() == "080103")
                    {
                        OperationCode += Ds.Tables[0].Rows[i]["OperationCode"].ToString();
                        return true;
                    }

                }
                qa.Dispose();
                return false;

            }
            catch (System.Exception EC)
            {
                //System.Console.Write(EC.Message);
                string h = EC.Message;
                return false;
            }

        }

        /// <summary>
        /// 载入数据
        /// </summary>
        private void LoadData()
        {
            string strIsAll = "0";
            EntityData entityNotice = RemindDAO.GetNoticeByCode(strNoticeCode);
            if (entityNotice.HasRecord())
            {
                if (entityNotice.GetInt("status").ToString() != "0")
                {
                    if (entityNotice.GetInt("Type") == 99)
                    {
                        trNotice.Visible = false;

                    }
                    this.lblNoticeClass.Text = Server.HtmlEncode(entityNotice.GetString("NoticeClass"));//新加通知类型（测试正确删除此注释）
                    this.lblTitle.Text = Server.HtmlEncode(entityNotice.GetString("Title"));
                    this.lbSubmitname.Text = RmsPM.BLL.SystemRule.GetUserName(entityNotice.GetString("SubmitPerson").ToString()).ToString();
                    this.lblSubmitDate.Text = entityNotice.GetDateTimeOnlyDate("SubmitDate");
                    this.divContent.InnerHtml = entityNotice.GetString("Content").Replace("\n", "<br>").Replace("\r", "&nbsp;");
                    strIsAll = entityNotice.GetString("IsAll");
                    //待修改
                    this.lbLastUpdate.Text = RmsPM.BLL.SystemRule.GetUserName(entityNotice.GetString("UserCode").ToString()).ToString() + " 于 " + entityNotice.GetDateTimeOnlyDate("UpdateDate") + "修改";
                }
                else
                {
                    Response.Write(JavaScript.ScriptStart);
                    Response.Write("window.alert('通知已删除');");
                    Response.Write("window.close();");
                    Response.Write(JavaScript.ScriptEnd);
                }
            }
            if (strIsAll == "0")
            {
                string strUsers = "";
                string strUserNames = "";
                string strStations = "";
                string strStationNames = "";
                BLL.ResourceRule.GetAccessRange(this.strNoticeCode, "0801", "080104", ref strUsers, ref strUserNames, ref strStations, ref strStationNames);
                this.lblUser.Text = strUserNames + "，" + strStationNames;
                this.lblUser.Text = BLL.StringRule.CutRepeat(this.lblUser.Text);
            }
            else
                this.lblUser.Text = "全体人员";

            //记录该用户已经读了该信息
            User u = (User)Session["User"];
            string strSelect = "select * from UserLookedNotice where noticecode='" + strNoticeCode + "' and usercode='" + u.UserCode + "'";
            string strInsert = "insert into UserLookedNotice (noticecode,usercode) values ('" + strNoticeCode + "','" + u.UserCode + "')";
            string strDelete = "Delete from UserLookedNotice where  noticecode='" + strNoticeCode + "' and usercode='" + u.UserCode + "'";
            QueryAgent qa = new QueryAgent();
            DataSet ds = qa.ExecSqlForDataSet(strSelect);
            if (ds.Tables[0].Rows.Count <= 0)
            {
                qa.ExecuteSql(strInsert);
            }
            qa.Dispose();
            entityNotice.Dispose();

        }

        protected void btDelete_ServerClick(object sender, System.EventArgs e)
        {
            //删除通知
            EntityData entityNotice = RemindDAO.GetNoticeByCode(this.strNoticeCode);
            DataRow dr;
            if (entityNotice.HasRecord())
            {
                dr = entityNotice.CurrentRow;
                // dr["NoticeCode"] = this.strNoticeCode;
                //dr["Title"] = this.lblTitle.Text.Trim();
                //dr["EnableDate"] = this.dtbShowDate.Value;
                //dr["SubmitPerson"] = base.user.UserCode;
                // dr["SubmitDate"] = DateTime.Now.ToShortDateString();
                //dr["Content"] = this.lblContent.Text;

                // 保存资源，保存权限			
                //string strUser = this.lblUser.Text.Trim();
                //string strStation = this.lb.Value.Trim();
                // 没有选择人员则向全体发布
                //if (strUser.Trim() == "全体人员")
                //{
                //    //strUser = this.GetAllUser();
                //    dr["IsAll"] = "1";
                //}
                //else
                //{
                //    dr["IsAll"] = "0";
                //}
                dr["status"] = "0";
                //ArrayList arOperator = new ArrayList();
                //this.SaveRS(arOperator, RmsPM.BLL.StringRule.CutRepeat(strUser), RmsPM.BLL.StringRule.CutRepeat(strStation), "080104"); // 一般通知查看权限
                //this.SaveRS(arOperator, base.user.UserCode, "", "080102,080103,080104"); // 修改和删除
                //if (arOperator.Count > 0)
                //    RmsPM.BLL.ResourceRule.SetResourceAccessRange(this.strNoticeCode, "0801", "", arOperator, false);

            }

            RemindDAO.UpdateNotice(entityNotice);
            entityNotice.Dispose();
            Response.Write(JavaScript.ScriptStart);
            Response.Write("window.opener.refresh();");
            Response.Write("window.close();");
            Response.Write(JavaScript.ScriptEnd);

        }
        protected void SaveToolsButton_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("noticeupdateinfo.aspx?&Code=" + this.strNoticeCode);
            //Response.Write(JavaScript.ScriptStart);
            //Response.Write("<script>window.open('noticeupdateinfo.aspx?&Code=" + this.strNoticeCode);
            //Response.Write("window.alert('asdfasdf')");
            //Response.Write("window.close();");
            //Response.Write("</script>");
            //Response.Write(JavaScript.ScriptEnd);
        }

    }
}
