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
using RmsPM.DAL.EntityDAO;
using RmsPM.DAL.QueryStrategy;

namespace RmsPM.Web.Systems
{
    /// <summary>
    /// UserModify 的摘要说明。
    /// </summary>
    public partial class UserModify : PageBase
    {
        protected string strAttachMentType = "ImageSign";
        protected string strMasterCode = "";

        protected void Page_Load(object sender, System.EventArgs e)
        {


            if (!IsPostBack)
            {
                IniPage();
                LoadData();
            }
            if (this.up_sPMNameLower == "shidaipm")
            {
                RequiredFieldValidator2.Enabled = true;
                RequiredFieldValidator1.Enabled = true;
                RegularExpressionValidator1.Enabled = true;
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

        private void IniPage()
        {
            this.txtRefreshScript.Value = Request["RefreshScript"] + "";
            this.txtRoleCode.Value = Request["RoleCode"] + "";
            this.txtAct.Value = Request.QueryString["act"] + "";

            string userCode = Request["UserCode"] + "";

            if (userCode == "")
            {
                userCode = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("UserCode");
            }

            this.txtUserCode.Value = userCode;

            this.strMasterCode = userCode;

            this.imgSign.Visible = false;
        }

        private void LoadData()
        {
            string userCode = Request["UserCode"] + "";

            try
            {
                EntityData ds = SystemManageDAO.GetStandard_SystemUserByCode(userCode);
                if (ds.HasRecord())
                {
                    UserID.Text = ds.GetString("UserID");
                    UserName.Text = ds.GetString("UserName");
                    this.txtOldUserID.Value = UserID.Text;

                    MailBox.Text = ds.GetString("MailBox");
                    Phone.Text = ds.GetString("Phone");
                    Mobile.Text = ds.GetString("Mobile");
                    Address.Text = ds.GetString("Address");
                    Fax.Text = ds.GetString("Fax");


                    //					this.trPassword.Visible = false;

                    this.txtSortID.Text = ds.GetString("SortID");
                    this.txtPhoneHome.Text = ds.GetString("PhoneHome");
                    this.txtShortName.Text = ds.GetString("ShortUserName");

                    Birthday.Value = ds.GetDateTimeOnlyDate("BirthDay");

                    if (ds.GetString("Sex") != "")
                    {
                        if (ds.GetString("Sex").ToString() == "男")
                        {
                            male.Checked = true;
                        }
                        else
                        {
                            female.Checked = true;
                        }
                    }

                    int status = ds.GetInt("Status");
                    this.rdoStatus0.Checked = (status != 1);
                    this.rdoStatus1.Checked = (status == 1);

                    string ud_sAttachMentCode = BLL.WBSRule.GetAttachMentCodeByUserCode(this.txtUserCode.Value);

                    if ( ud_sAttachMentCode != "" )
                    {
                        this.imgSign.Visible = true;
                        this.btnDeleteSign.Visible = true;
                        this.imgSign.ImageUrl = "../Project/WBSAttachMentView.aspx?Action=View&AttachMent=0&AttachMentCode=" + ud_sAttachMentCode;
                    }
                    else 
                    {
                        this.imgSign.Visible = false;
                        this.btnDeleteSign.Visible = false;
                    }


                }
                else
                {
                    this.rdoStatus0.Checked = true;
                    this.male.Checked = true;
                }

                //显示财务编码
                this.ucInputSubjectSet.LoadData(ds.Tables["SystemUserSubjectSet"]);

                ds.Dispose();

            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
                Response.Write(Rms.Web.JavaScript.Alert(true, "加载页面错误。"));
            }
        }

        private void WriteRefreshScript(string userCode)
        {
            Response.Write(Rms.Web.JavaScript.ScriptStart);
            if (this.txtRefreshScript.Value.Trim() != "")
            {
                Response.Write("window.opener." + this.txtRefreshScript.Value.Trim());
                Response.Write(Rms.Web.JavaScript.WinClose(false));
            }
            else
            {
                Response.Write(Rms.Web.JavaScript.OpenerReload(false));
                Response.Write(Rms.Web.JavaScript.WinClose(false));
                //				Response.Write( Rms.Web.JavaScript.PageTo(false, "UserInfo.aspx?UseCode=" + userCode ));
            }
            Response.Write(Rms.Web.JavaScript.ScriptEnd);
        }


        protected void btnSave_ServerClick(object sender, System.EventArgs e)
        {
            if (this.UserID.Text.Trim() == "")
            {
                Response.Write(Rms.Web.JavaScript.Alert(true, "请输入登录名。"));
                return;
            }

            if (this.UserName.Text.Trim() == "")
            {
                Response.Write(Rms.Web.JavaScript.Alert(true, "请输入姓名。"));
                return;
            }

            /*
            if (this.txtSortID.Text.Trim() == "") 
            {
                Response.Write(Rms.Web.JavaScript.Alert( true,"请输入工号。" ));
                return;
            }
            */

            if (this.PassWord.Value != this.ConfirmPassWord.Value)
            {
                Response.Write(Rms.Web.JavaScript.Alert(true, "两次输入的密码不一致。"));
                return;
            }
            if (this.OwnName.Value != this.ConfirmOwnName.Value)
            {
                Response.Write(Rms.Web.JavaScript.Alert(true, "两次输入的辅助密码不一致。"));
                return;
            }

            string userCode = Request["UserCode"] + "";
            string roleCode = Request["RoleCode"] + "";
            string accountName = this.UserID.Text.Trim();

            if (!BLL.SystemRule.CheckUserAccountName(userCode, accountName))
            {
                Response.Write(Rms.Web.JavaScript.Alert(true, "该登录名已经被他人使用，请换一个登录名。"));

                return;
            }

            bool isNew = (userCode == "");
            EntityData user = null;
            DataRow dr = null;

            if (isNew)
            {
                user = new EntityData("Standard_SystemUser");
                dr = user.GetNewRecord();
//                userCode = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("UserCode");
                userCode = txtUserCode.Value;
                dr["UserCode"] = userCode;
                user.AddNewRecord(dr);

                // 同步新增用户信息
                if (this.hrYes.Checked)
                    this.AddHrUserInfo();
            }
            else
            {
                user = DAL.EntityDAO.SystemManageDAO.GetStandard_SystemUserByCode(userCode);
                dr = user.CurrentRow;

                // 更新人事用户信息基本资料
                if (this.hrYes.Checked)
                    this.UpdateHRUserInfo(userCode);
            }

            dr["UserID"] = accountName;
            dr["UserName"] = UserName.Text;
            dr["MailBox"] = MailBox.Text;
            dr["SortID"] = this.txtSortID.Text;
            dr["PhoneHome"] = this.txtPhoneHome.Text;

            if (PassWord.Value != "1*#06#")
            {
                dr["PassWord"] = PassWord.Value;
            }
            if (OwnName.Value != "1*#06#")
            {
                dr["OwnName"] = OwnName.Value;
            }

            if (Birthday.Value == "")
                dr["BirthDay"] = DBNull.Value;
            else
                dr["BirthDay"] = Birthday.Value;

            dr["ShortUserName"] = this.txtShortName.Text.Trim();

            dr["Address"] = Address.Text;
            dr["Phone"] = Phone.Text;
            dr["Mobile"] = Mobile.Text;
            dr["Fax"] = Fax.Text;
            dr["status"] = 0;
            if (male.Checked)
            {
                dr["Sex"] = "男";
            }
            else
            {
                dr["Sex"] = "女";
            }

            if (this.rdoStatus1.Checked)
                dr["Status"] = 1;
            else
                dr["Status"] = 0;

            // 从组织结构过来的，带来了rolecode新增
            if (isNew && roleCode != "")
            {
                user.SetCurrentTable("UserRole");
                DataRow drRole = user.GetNewRecord();
                drRole["UserCode"] = userCode;
                drRole["RoleCode"] = roleCode;
                user.AddNewRecord(drRole);
            }

            //保存财务编码
            this.ucInputSubjectSet.SaveData(user.Tables["SystemUserSubjectSet"], userCode);

            DAL.EntityDAO.SystemManageDAO.SubmitAllStandard_SystemUser(user);
            user.Dispose();

            WriteRefreshScript(userCode);

        }

        private void UpdateHRUserInfo(string userCode)
        {
            EntityData entity = DAL.EntityDAO.OADAO.GetOAPersonByCode(userCode);
            if (entity.HasRecord())
            {
                DataRow dr = entity.CurrentRow;
                dr["workNo"] = txtSortID.Text.Trim();
                dr["cname"] = UserName.Text;
                dr["homeplace"] = Address.Text.Trim();
                if (Birthday.Value.Length > 0)
                    dr["birthday"] = Birthday.Value;
                else
                    dr["birthday"] = System.DBNull.Value;
                dr["phone"] = Phone.Text.Trim();
                dr["mobile"] = Mobile.Text.Trim();
                if (male.Checked)
                {
                    dr["sex"] = "DI69127";		// 男
                }
                else
                {
                    dr["sex"] = "DI69128";		// 女
                }
                if (rdoStatus0.Checked) dr["Status"] = "1"; // 1 在职
                if (rdoStatus1.Checked) dr["Status"] = "2"; // 2 禁止
                DAL.EntityDAO.OADAO.SubmitAllOAPerson(entity);
            }
            entity.Dispose();
        }
        private void AddHrUserInfo()
        {
            EntityData entity = new EntityData("OAPerson");
            DataRow dr = entity.GetNewRecord();

            string code = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("OAPerson");
            dr["OAPersonCode"] = code;
            dr["workNo"] = txtSortID.Text.Trim();
            dr["cname"] = UserName.Text;
            dr["homeplace"] = Address.Text.Trim();
            if (Birthday.Value.Length > 0)//
                dr["birthday"] = Birthday.Value;
            else
                dr["birthday"] = System.DBNull.Value;
            dr["phone"] = Phone.Text.Trim();
            dr["mobile"] = Mobile.Text.Trim();
            if (male.Checked)
            {
                dr["sex"] = "DI69127";		// 男
            }
            else
            {
                dr["sex"] = "DI69128";		// 女
            }
            dr["Status"] = "1"; // 新增状态为在职

            entity.AddNewRecord(dr);
            DAL.EntityDAO.OADAO.SubmitAllOAPerson(entity);
            entity.Dispose();

        }
        protected void btnDeleteSign_ServerClick(object sender, EventArgs e)
        {
            string ud_sUserCode = this.txtUserCode.Value;
            string ud_sAttachmentCode = BLL.WBSRule.GetAttachMentCodeByUserCode(ud_sUserCode);

            EntityData entityAttachMent = DAL.EntityDAO.DAOFactory.GetAttachmentDAO().GetAttachMentByCode(ud_sAttachmentCode);

            if (entityAttachMent.HasRecord())
            {
                BLL.DocumentRule.Instance().DeleteAttachment(entityAttachMent.CurrentRow["AttachmentCode"].ToString());// WBSDAO.DeleteAttachMent(entityAttachMent);
            }

            this.imgSign.Visible = false;
            this.btnDeleteSign.Visible = false;
        }
}
}

