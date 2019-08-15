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
using Rms.Web;
using RmsPM.DAL.EntityDAO;
using RmsPM.DAL;
using RmsPM.BLL;
using RmsPM.Web;
using RmsPM.DAL.QueryStrategy;


public partial class Remind_noticeupdateinfo : RmsPM.Web.PageBase
{

    private string strAction = "";
    private DataTable dt = new DataTable();
    private string strNoticeCode = ""; 

    protected void Page_Load(object sender, EventArgs e)
    {
       
        try
        {
            this.InitPage();
            if (!this.IsPostBack)
            {
                PageFacade.LoadDictionarySelect(this.DDLNoticeClass, "通知类型", "");//通知类别改成通知类型
                this.LoadDate();
            }
        }
        catch (System.Exception ec)
        {
            // ApplicationLog.WriteLog(this.ToString(), ex, "载入通知失败");
        }
    }


    public void InitPage()
    {
        //对通知标题的个性化判断
        if (this.up_sPMNameLower != "tianyangoa")
        {
            this.trNotice.Visible = false;
        }
        else
        {
            this.trNotice.Visible = true;
            
        }

        strNoticeCode = Request.QueryString["Code"] + "";

        // 载入附件
        this.myAttachMentAdd.AttachMentType = "NoticeAttachMent";
        this.myAttachMentAdd.MasterCode = strNoticeCode;

        FeedBack1.FeedBackType = "Notice";
        FeedBack1.MasterCode = this.strNoticeCode;


        try
        {
            User myUser = new User(user.UserCode);
            QueryAgent qa = new QueryAgent();
            RmsPM.DAL.QueryStrategy.RoleOperation sb = new RmsPM.DAL.QueryStrategy.RoleOperation();
            sb.AddStrategy(new Strategy(RoleOperationName.UserCode, user.UserCode));
            //sb.AddStrategy(new Strategy( RoleOperationName.UserCode,"080102"));
            string sql = sb.BuildMainQueryString();
            DataSet Ds = qa.ExecSqlForDataSet(sql);
            string str_Edit = "";
            string str_Delete = "";
            for (int i = 0; i < Ds.Tables[0].Rows.Count; i++)
            {
                if (Ds.Tables[0].Rows[i][1].ToString() == "080102")
                {
                    str_Edit = Ds.Tables[0].Rows[i][1].ToString();
                }
                if (Ds.Tables[0].Rows[i][1].ToString() == "080103")
                {
                    str_Delete = Ds.Tables[0].Rows[i][1].ToString();
                }

            }

            //判断是否拥有删除权限
            if (!user.HasRight("080103"))
            {
                this.btDelete.Visible = false;
            }
            else
            {
                this.btDelete.Visible = true;
            }



        }
        catch (Exception dd)
        {
            string h = dd.Message.ToString();
            string f = h;
        }
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

            RmsPM.DAL.QueryStrategy.RoleOperation sb = new RmsPM.DAL.QueryStrategy.RoleOperation();
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


    public void LoadDate()
    {
        string strIsAll = "0";
        int strType = 0;
        EntityData entityNotice = RemindDAO.GetNoticeByCode(strNoticeCode);
     
        if (entityNotice.HasRecord())
        {
           
            if (entityNotice.GetInt("status").ToString() != "0")
            {
                strType = entityNotice.GetInt("Type");
                if (strType == 99)
                {
                    this.trNotice.Visible = false;
                }
                this.DDLNoticeClass.Value = Server.HtmlEncode(entityNotice.GetString("NoticeClass"));
                this.txtTitle.Value = Server.HtmlEncode(entityNotice.GetString("Title"));
                this.lbSubmitname.Text = RmsPM.BLL.SystemRule.GetUserName(entityNotice.GetString("SubmitPerson").ToString()).ToString();
                this.lblSubmitDate.Text = entityNotice.GetDateTimeOnlyDate("SubmitDate");
                this.taContent.Value = StringRule.FormartOutput(entityNotice.GetString("Content"));
                strIsAll = entityNotice.GetString("IsAll");
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
        if (strIsAll == "0") //部分人员
        {
            string strUsers = "";
            string strUserNames = "";
            string strStations = "";
            string strStationNames = "";
            RmsPM.BLL.ResourceRule.GetAccessRange(this.strNoticeCode, "0801", "080104", ref strUsers, ref strUserNames, ref strStations, ref strStationNames);
            this.txtUsers.Value = RmsPM.BLL.StringRule.CutRepeat(strUsers);
            this.SelectName.InnerText = RmsPM.BLL.StringRule.CutRepeat(strUserNames);
            this.txtStations.Value = RmsPM.BLL.StringRule.CutRepeat(strStations);
            this.SelectName.InnerText += "，" + RmsPM.BLL.StringRule.CutRepeat(strStationNames);
        }
        else
        {
            this.txtUsers.Value = this.GetAllUser();
            this.SelectName.InnerText = "全体人员";
        }

        entityNotice.Dispose();


        // this.lblTitle.Text = "修改通知";
        //EntityData entityNotice = RemindDAO.GetNoticeByCode(this.strNoticeCode);
        //this.txtTitle.Value = System.Web.HttpUtility.HtmlEncode(entityNotice.GetString("Title"));
        //this.taContent.Value = StringRule.FormartOutput(entityNotice.GetString("Content"));
        //this.LoadUser(entityNotice.GetString("IsAll"));

    }

    private string GetAllUser()
    {
        string strUser = "";
        EntityData entity = SystemManageDAO.GetAllSystemUser();
        DataView dv = new DataView(entity.CurrentTable, "isnull(Status,0)='0'", "", System.Data.DataViewRowState.CurrentRows);
        foreach (DataRowView drv in dv)
        {
            if (strUser != "") strUser += ",";
            strUser += drv["UserCode"].ToString();
        }
        return strUser;
    }


    protected void SaveToolsButton_ServerClick(object sender, EventArgs e)
    {

        try
        {
            if (UpdataData())
            { 
                this.SaveToolsButton.Visible = false;
                this.CancelToolsButton.Value = "取消";

                Response.Write(JavaScript.ScriptStart);
                Response.Write("window.opener.refresh();");
                Response.Write("window.close();");
                Response.Write(JavaScript.ScriptEnd);
            }
            else
            {
                Response.Write(Rms.Web.JavaScript.Alert(true, "类型不能为空，标题不能为空，内容不能问空！"));
            }
        }
        catch (Exception ex)
        {
            ApplicationLog.WriteLog(this.ToString(), ex, "保存通知失败");
        }
    }

    //更新数据
    private bool UpdataData()
    {
        //保存通知
        EntityData entityNotice = RemindDAO.GetNoticeByCode(this.strNoticeCode);
        DataRow dr;
        if (entityNotice.HasRecord())
        {
            dr = entityNotice.CurrentRow;
           
            if (this.txtTitle.Value.Trim() == "")
            {
                return false;
            }
            if (this.taContent.Value.Trim() == string.Empty)
            {
                return false;
            }
             dr["NoticeCode"] = this.strNoticeCode;
             //新加通知类型（测试正确后删除此注释）
             if (this.up_sPMNameLower == "tianyangoa")
             {
                 dr["NoticeClass"] = this.DDLNoticeClass.Value.Trim();
                 dr["Title"] = this.txtTitle.Value.Trim();
                 
                 if (entityNotice.GetInt("Type")!= 99)
                 {
                    if (this.DDLNoticeClass.Value.Trim() == "")
                    {
                     return false;
                     }
                 }                 
             }
            dr["Title"] = this.txtTitle.Value.Trim();
            dr["UpdateDate"] = DateTime.Now;
            dr["UserCode"] = this.user.UserCode;
            //dr["EnableDate"] = this.dtbShowDate.Value;
//            dr["SubmitPerson"] = base.user.UserCode;
//            dr["SubmitDate"] = DateTime.Now.ToShortDateString();
            dr["Content"] = this.taContent.Value.ToString();

          

            // 保存资源，保存权限			
            string strUser = this.txtUsers.Value.Trim();
            string strStation = this.txtStations.Value.Trim();
            // 没有选择人员则向全体发布
            if (strUser.Length < 1 && strStation.Length < 1)
            {
                strUser = this.GetAllUser();
                dr["IsAll"] = "1";
            }
            else
            {
                dr["IsAll"] = "0";
            }
            dr["status"] = "1";
            ArrayList arOperator = new ArrayList();
            this.SaveRS(arOperator, RmsPM.BLL.StringRule.CutRepeat(strUser), RmsPM.BLL.StringRule.CutRepeat(strStation), "080104"); // 一般通知查看权限
            this.SaveRS(arOperator, base.user.UserCode, "", "080102,080103,080104"); // 修改和删除

            if (arOperator.Count > 0)
                RmsPM.BLL.ResourceRule.SetResourceAccessRange(this.strNoticeCode, "0801", "", arOperator, false);

        }

        RemindDAO.UpdateNotice(entityNotice);
        entityNotice.Dispose();

        // 保存附件
        this.myAttachMentAdd.SaveAttachMent(this.strNoticeCode);


        //修改保存后这条信息又成为新信息，就是没有读过的信息

        User u = (User)Session["User"];
        string strDelete = "Delete from UserLookedNotice where  noticecode='" + this.strNoticeCode + "' and usercode='" + u.UserCode + "'";
        QueryAgent qa = new QueryAgent();
        qa.ExecuteSql(strDelete);
        qa.Dispose();
        return true;
    }

    /// <summary>
    /// 添加权限资源
    /// </summary>
    private void SaveRS(ArrayList arOperator, string strUser, string strStation, string strOption)
    {

        if (strUser.Length > 0)
        {
            foreach (string strTUser in strUser.Split(','))
            {
                if (strTUser == "") continue;
                AccessRange acRang = new AccessRange();
                acRang.AccessRangeType = 0;
                acRang.RelationCode = strTUser;
                acRang.Operations = strOption;
                arOperator.Add(acRang);
            }
        }
        if (strStation.Length > 0)
        {
            foreach (string strTStation in strStation.Split(','))
            {
                if (strTStation == "") continue;
                AccessRange acRang = new AccessRange();
                acRang.AccessRangeType = 1;
                acRang.RelationCode = strTStation;
                acRang.Operations = strOption;
                arOperator.Add(acRang);
            }
        }
    }

    protected void btDelete_ServerClick(object sender, EventArgs e)
    {

        //删除通知 改变通知状态

        EntityData entityNotice = RemindDAO.GetNoticeByCode(this.strNoticeCode);
        DataRow dr;
        if (entityNotice.HasRecord())
        {
            dr = entityNotice.CurrentRow;
            //dr["NoticeCode"] = this.strNoticeCode;
            //dr["Title"] = this.txtTitle.Value.Trim();
            ////dr["EnableDate"] = this.dtbShowDate.Value;
            //dr["SubmitPerson"] = base.user.UserCode;
            //dr["SubmitDate"] = DateTime.Now.ToShortDateString();
            //dr["Content"] = this.taContent.Value;

            //// 保存资源，保存权限			
            //string strUser = this.txtUsers.Value.Trim();
            //string strStation = this.txtStations.Value.Trim();
            //// 没有选择人员则向全体发布
            //if (strUser.Length < 1 && strStation.Length < 1)
            //{
            //    //strUser = this.GetAllUser();
            //    dr["IsAll"] = "1";
            //}
            //else
            //{
            //    dr["IsAll"] = "0";
            //}
            dr["status"] = "0";
            ArrayList arOperator = new ArrayList();
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
        //try
        //{
        //    // 删除附件
        //    this.myAttachMentAdd.DelAttachMentByMasterCode(this.strNoticeCode);
        //    // 删除分发范围
        //    EntityData entityUser = RemindDAO.GetNoticeUserByNoticeCode(this.strNoticeCode);
        //    if (entityUser.HasRecord())
        //    {
        //        RemindDAO.DeleteNoticeUser(entityUser);
        //    }
        //    entityUser.Dispose();

        //    // 是否添加入删除资源和权限的操作


        //    EntityData entityNotice = RemindDAO.GetNoticeByCode(strNoticeCode);
        //    RemindDAO.DeleteNotice(entityNotice);

        //    Response.Write(JavaScript.ScriptStart);
        //    Response.Write("window.opener.refresh();");
        //    Response.Write("window.close();");
        //    Response.Write(JavaScript.ScriptEnd);
        //}
        //catch (Exception ex)
        //{
        //    ApplicationLog.WriteLog(this.ToString(), ex, "删除失败");
        //}
    }

}

