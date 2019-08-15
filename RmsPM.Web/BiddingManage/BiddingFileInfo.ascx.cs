

namespace RmsPM.Web.BiddingManage
{
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
    using RmsPM.Web.BiddingManage;
    using Rms.ORMap;
    using RmsPM.Web.WorkFlowControl;


    public partial class BiddingFileInfo : BiddingControlBase
    {

        private string _BiddingFileCode="";
        private string _BiddingCode="";
        private string _Remark="";
        private string _BiddingFileState="";

        protected User user = null;

        /// <summary>
        /// 招标文件编号
        /// </summary>
        public string BiddingFileCode
        {
            get
            {
                if (_BiddingFileCode == "")
                {
                    if (this.ViewState["_BiddingFileCode"] != null)
                        return this.ViewState["_BiddingFileCode"].ToString();
                    return "";
                }
                return _BiddingFileCode;
            }
            set
            {
                _BiddingFileCode = value;
                this.ViewState["_BiddingFileCode"] = value;
            }
        }


        /// <summary>
        /// 中标单位评审页面
        /// </summary>
        public string BiddingFileUrl
        {
            get
            {
                return BLL.WorkFlowRule.GetProcedureURLByName("招标文件评审");
            }
        }

        /// <summary>
        /// 招标计划编号
        /// </summary>
        public string BiddingCode
        {
            get
            {
                if (_BiddingCode == "")
                {
                    if (this.ViewState["_BiddingCode"] != null)
                        return this.ViewState["_BiddingCode"].ToString();
                    return "";
                }
                return _BiddingCode;
            }
            set
            {
                _BiddingCode = value;
                this.ViewState["_BiddingCode"] = value;
            }
        }

        /// <summary>
        /// 招标文件摘要说明
        /// </summary>
        public string Remark
        {
            get
            {
                if (_Remark == "")
                {
                    if (this.ViewState["_Remark"] != null)
                        return this.ViewState["_Remark"].ToString();
                    return "";
                }
                return _Remark;
            }
            set
            {
                _Remark = value;
                this.ViewState["_Remark"] = value;
            }
        }


        /// <summary>
        /// 招标文件状态

        /// </summary>
        public string BiddingFileState
        {
            get
            {
                if (_BiddingFileState == "")
                {
                    if (this.ViewState["_BiddingFileState"] != null)
                        return this.ViewState["_BiddingFileState"].ToString();
                    return "";
                }
                return _BiddingFileState;
            }
            set
            {
                _BiddingFileState = value;
                this.ViewState["_BiddingFileState"] = value;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// ****************************************************************************
        /// <summary>
        /// 组件初始化

        /// </summary>
        /// ****************************************************************************
        public void InitControl()
        {

            if (this.State == ModuleState.Sightless)//不可见的
            {
                this.Visible = false;
            }
            else if (this.State == ModuleState.Operable)//可操作的
            {
                LoadData(true);

            }
            else if (this.State == ModuleState.Eyeable)//可见的

            {
                LoadData(false);

            }
            else if (this.State == ModuleState.Begin)//可见的

            {
                LoadData(false);

            }
            else if (this.State == ModuleState.End)//可见的

            {
                LoadData(false);

            }
            else
            {
                this.Visible = false;
            }


        }

        private void LoadData(bool Flag)
        {
            RmsPM.BLL.BiddingFile cBiddingFile = new RmsPM.BLL.BiddingFile();
           
            if (this.BiddingFileCode != "" )
                cBiddingFile.BiddingFileCode = this.BiddingFileCode;
            if (this.BiddingCode != "")
                cBiddingFile.BiddingCode = this.BiddingCode;
            if (this.BiddingFileState != "")
                cBiddingFile.State = this.BiddingFileState;

            System.Data.DataTable dtBiddingFile = cBiddingFile.GetBiddingFiles();
            if (dtBiddingFile.Rows.Count!= 0)
            {
                this.BiddingFileCode = dtBiddingFile.Rows[0]["BiddingFileCode"].ToString();
                cBiddingFile.BiddingFileCode = this.BiddingFileCode;
                if (Flag)
                {

                    string LinkUrl = "<a OnClick=\"javascript:OpenLargeWindow('BiddingFileManage.aspx?BiddingCode=" + cBiddingFile.BiddingCode + "&BiddingFileCode=" + this.BiddingFileCode + "&ProjectCode=" + this.ProjectCode + "');\">" + dtBiddingFile.Rows[0]["Remark"].ToString() + "</a>";
                    this.tdBiddingFileLink.InnerHtml = LinkUrl;

                    this.tdBiddingFileState.InnerHtml = RmsPM.BLL.BiddingFile.GetBiddingFileStatusName(dtBiddingFile.Rows[0]["State"].ToString());


                }
                else
                {
                    this.tdBiddingFileLink.InnerHtml = cBiddingFile.Remark;
                    this.tdBiddingFileState.InnerHtml = RmsPM.BLL.BiddingFile.GetBiddingFileStatusName(cBiddingFile.State);
                }
            }

            //调试状态下用admin
            if ((Session["User"] == null) && (Request["DebugUser"] + "" != ""))
            {
                Session["User"] = new User(Request["DebugUser"] + "");
            }

            if ((Session["User"] == null) && (ConfigurationSettings.AppSettings["IsDebug"] == "1") && (ConfigurationSettings.AppSettings["DebugUser"] != ""))
            {
                Session["User"] = new User(ConfigurationSettings.AppSettings["DebugUser"]);
            }

        

            if (Session["User"] != null)
            {
                this.user = (User)Session["User"];
            }


            if (!this.user.HasRight("210906"))//招标文件编辑
            {
                this.btnEdit.Visible = false;
            }
            if (!this.user.HasRight("210905"))//招标文件审核
            {
                this.btnWorkflow.Visible = false;
            }

            this.btnEdit.Attributes["OnClick"] = "javascript:OpenLargeWindow('BiddingFileManage.aspx?BiddingCode=" + cBiddingFile.BiddingCode + "&BiddingFileCode=" + cBiddingFile.BiddingFileCode + "&ProjectCode=" + this.ProjectCode + "','BiddingFileInfo');";
            this.btnWorkflow.Attributes["OnClick"] = "javascript:OpenLargeWindow('" + this.BiddingFileUrl + "?BiddingCode=" + cBiddingFile.BiddingCode + "&BiddingFileCode=" + cBiddingFile.BiddingFileCode + "&ProjectCode=" + this.ProjectCode + "','BiddingFileInfoWorkflow');";
            switch (cBiddingFile.State)
            {
                case "7":
                case "9":
                case "0":
                    this.btnEdit.Visible = false;
                    this.btnWorkflow.Visible = false;
                    break;
                
                  
                default:
                 
                    break;
            }

           
        }



    }
}