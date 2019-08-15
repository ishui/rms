

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
    using RmsPM.Web.WorkFlowControl;


    public partial class BiddingConditionFileInfo : BiddingControlBase
    {
        #region 私有变量


        private string _BiddingConditionFileCode = "";
        private string _BiddingCode = "";
        private string _Name = "";//技术条件名
        private string _BiddingConditionFileState = "";//状态

        private string _Zbfw = "";//招标范围
        private string _Jsxq = "";//技术要求及指标
        private string _Zlbz = "";//质量标准
        private string _Gq = "";//工期
        private string _Rctj = "";//入场条件及总包管理方式
        private string _Shfw = "";//保修及售后服务

        private string _Remark = "";//备注


        #endregion

        #region 属性



        public string BiddingConditionFileUrl
        {
            get
            {
                return BLL.WorkFlowRule.GetProcedureURLByName("招标技术条件评审");
            }
        }

        public string BiddingConditionFileCode
        {
            get
            {
                if (_BiddingConditionFileCode == "")
                {
                    if (this.ViewState["_BiddingConditionFileCode"] != null)
                        return this.ViewState["_BiddingConditionFileCode"].ToString();
                    return "";
                }
                return _BiddingConditionFileCode;
            }
            set
            {
                _BiddingConditionFileCode = value;
                this.ViewState["_BiddingConditionFileCode"] = value;
            }
        }

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
        /// 技术条件名
        /// </summary>
        public string Name
        {
            get
            {
                if (_Name == "")
                {
                    if (this.ViewState["_Name"] != null)
                        return this.ViewState["_Name"].ToString();
                    return "";
                }
                return _Name;
            }
            set
            {
                _Name = value;
                this.ViewState["_Name"] = value;
            }
        }

        /// <summary>
        /// 状态

        /// </summary>
        public string BiddingConditionFileState
        {
            get
            {
                if (_BiddingConditionFileState == "")
                {
                    if (this.ViewState["_BiddingConditionFileState"] != null)
                        return this.ViewState["_BiddingConditionFileState"].ToString();
                    return "";
                }
                return _BiddingConditionFileState;
            }
            set
            {
                _BiddingConditionFileState = value;
                this.ViewState["_BiddingConditionFileState"] = value;
            }
        }

        /// <summary>
        /// 招标范围
        /// </summary>
        public string Zbfw
        {
            get
            {
                if (_Zbfw == "")
                {
                    if (this.ViewState["_Zbfw"] != null)
                        return this.ViewState["_Zbfw"].ToString();
                    return "";
                }
                return _Zbfw;
            }
            set
            {
                _Zbfw = value;
                this.ViewState["_Zbfw"] = value;
            }
        }


        /// <summary>
        /// 技术要求及指标
        /// </summary>
        public string Jsxq
        {
            get
            {
                if (_Jsxq == "")
                {
                    if (this.ViewState["_Jsxq"] != null)
                        return this.ViewState["_Jsxq"].ToString();
                    return "";
                }
                return _Jsxq;
            }
            set
            {
                _Jsxq = value;
                this.ViewState["_Jsxq"] = value;
            }
        }


        /// <summary>
        /// 质量标准
        /// </summary>
        public string Zlbz
        {
            get
            {
                if (_Zlbz == "")
                {
                    if (this.ViewState["_Zlbz"] != null)
                        return this.ViewState["_Zlbz"].ToString();
                    return "";
                }
                return _Zlbz;
            }
            set
            {
                _Zlbz = value;
                this.ViewState["_Zlbz"] = value;
            }
        }

        /// <summary>
        /// 工期
        /// </summary>
        public string Gq
        {
            get
            {
                if (_Gq == "")
                {
                    if (this.ViewState["_Gq"] != null)
                        return this.ViewState["_Gq"].ToString();
                    return "";
                }
                return _Gq;
            }
            set
            {
                _Gq = value;
                this.ViewState["_Gq"] = value;
            }
        }

        /// <summary>
        /// 入场条件及总包管理方式
        /// </summary>
        public string Rctj
        {
            get
            {
                if (_Rctj == "")
                {
                    if (this.ViewState["_Rctj"] != null)
                        return this.ViewState["_Rctj"].ToString();
                    return "";
                }
                return _Rctj;
            }
            set
            {
                _Rctj = value;
                this.ViewState["_Rctj"] = value;
            }
        }

        /// <summary>
        /// 保修及售后服务

        /// </summary>
        public string Shfw
        {
            get
            {
                if (_Shfw == "")
                {
                    if (this.ViewState["_Shfw"] != null)
                        return this.ViewState["_Shfw"].ToString();
                    return "";
                }
                return _Shfw;
            }
            set
            {
                _Shfw = value;
                this.ViewState["_Shfw"] = value;
            }
        }

        /// <summary>
        /// 备注
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


        #endregion



        protected void Page_Load(object sender, EventArgs e)
        {

            AttachMentList1.AttachMentType = "BiddingConditionFile1";

            AttachMentList2.AttachMentType = "BiddingConditionFile2";

            AttachMentList3.AttachMentType = "BiddingConditionFile3";

            AttachMentList4.AttachMentType = "BiddingConditionFile4";

            AttachMentList5.AttachMentType = "BiddingConditionFile5";
            AttachMentList6.AttachMentType = "BiddingConditionFile6";

            if (this.BiddingConditionFileCode != "")
            {

                AttachMentList1.MasterCode = this.BiddingConditionFileCode;
  
                AttachMentList2.MasterCode = this.BiddingConditionFileCode;
             
                AttachMentList3.MasterCode = this.BiddingConditionFileCode;
               
                AttachMentList4.MasterCode = this.BiddingConditionFileCode;
   
                AttachMentList5.MasterCode = this.BiddingConditionFileCode;
                AttachMentList6.MasterCode = this.BiddingConditionFileCode;
            }
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
                //EyeableDiv.Visible = false;
                //OperableDiv.Visible = true;
            }
            else if (this.State == ModuleState.Eyeable)//可见的

            {
                LoadData(false);
                //OperableDiv.Visible = false;
                //EyeableDiv.Visible = true;
            }
            else if (this.State == ModuleState.Begin)//可见的

            {
                LoadData(false);
                //OperableDiv.Visible = false;
                //EyeableDiv.Visible = true;
            }
            else if (this.State == ModuleState.End)//可见的

            {
                LoadData(false);
                //OperableDiv.Visible = false;
                //EyeableDiv.Visible = true;
            }
            else
            {
                this.Visible = false;
            }
        }
        /// ****************************************************************************
        /// <summary>
        /// 数据加载
        /// </summary>
        /// ****************************************************************************
        private void LoadData(bool Flag)
        {

            RmsPM.BLL.BiddingConditionFile cbiddingconditionfile = new RmsPM.BLL.BiddingConditionFile();

            if (this.BiddingConditionFileCode != "")
                cbiddingconditionfile.BiddingConditionFileCode = this.BiddingConditionFileCode;
            if (this.BiddingCode != "")
                cbiddingconditionfile.BiddingCode = this.BiddingCode;
            if (this.BiddingConditionFileState != "")
                cbiddingconditionfile.State = this.BiddingConditionFileState;

            System.Data.DataTable dtBiddingFile = cbiddingconditionfile.GetBiddings();
            if (dtBiddingFile.Rows.Count != 0)
            {
                this.BiddingConditionFileCode = dtBiddingFile.Rows[0]["BiddingConditionFileCode"].ToString();
                cbiddingconditionfile.BiddingConditionFileCode = this.BiddingConditionFileCode;
                if (Flag)
                {

                    //string LinkUrl = "<a OnClick=\"javascript:OpenLargeWindow('BiddingFileManage.aspx?BiddingCode=" + cbiddingconditionfile.BiddingCode + "&BiddingConditionFileCode=" + this.BiddingConditionFileCode + "&ProjectCode=" + this.ProjectCode + "');\">" + dtBiddingFile.Rows[0]["Remark"].ToString() + "</a>";
                    this.TdBiddingConditionFileName.InnerHtml = cbiddingconditionfile.Name;
                    this.TdNumber.InnerHtml = cbiddingconditionfile.BiddingConditionFileNumber;
                    this.tdBiddingFileState.InnerHtml = RmsPM.BLL.BiddingConditionFile.GetBiddingConditionFileStatusName(cbiddingconditionfile.State);
                    

                }
                else
                {
                    this.TdBiddingConditionFileName.InnerHtml = cbiddingconditionfile.Name;
                    this.TdNumber.InnerHtml = cbiddingconditionfile.BiddingConditionFileNumber;
                    this.tdBiddingFileState.InnerHtml = RmsPM.BLL.BiddingConditionFile.GetBiddingConditionFileStatusName(cbiddingconditionfile.State);
                }
                //.Replace("\n","<br />");
                this.lblZBFW.Text = RmsPM.BLL.StringRule.FormartOutput(cbiddingconditionfile.Zbfw).Replace("\n", "<br />");
                this.lblJSYQ.Text = RmsPM.BLL.StringRule.FormartOutput(cbiddingconditionfile.Jsxq).Replace("\n", "<br />");
                this.lblZLBZ.Text = RmsPM.BLL.StringRule.FormartOutput(cbiddingconditionfile.Zlbz).Replace("\n", "<br />");
                this.lblGQ.Text = RmsPM.BLL.StringRule.FormartOutput(cbiddingconditionfile.Gq).Replace("\n", "<br />");
                this.lblRCTJ.Text = RmsPM.BLL.StringRule.FormartOutput(cbiddingconditionfile.Rctj).Replace("\n", "<br />");
                this.lblSHFW.Text = RmsPM.BLL.StringRule.FormartOutput(cbiddingconditionfile.Shfw).Replace("\n", "<br />");
            }

            if (!((User)Session["User"]).HasRight("210806"))//招标技术条件编辑

            {
                this.btnEdit.Visible = false;
            }
            if (!((User)Session["User"]).HasRight("210805"))//招标技术条件审核

            {
                this.btnWorkflow.Visible = false;
            }


            this.btnEdit.Attributes["OnClick"] = "javascript:OpenLargeWindow('BiddingConditionFileManage.aspx?BiddingCode=" + cbiddingconditionfile.BiddingCode + "&BiddingConditionFileCode=" + cbiddingconditionfile.BiddingConditionFileCode + "&ProjectCode=" + this.ProjectCode + "','BiddingConditionFileInfo');";
            this.btnWorkflow.Attributes["OnClick"] = "javascript:OpenLargeWindow('" + this.BiddingConditionFileUrl + "?BiddingCode=" + cbiddingconditionfile.BiddingCode + "&BiddingConditionFileCode=" + cbiddingconditionfile.BiddingConditionFileCode + "&ProjectCode=" + this.ProjectCode + "','BiddingConditionFileInfoWorkFlow');";

            switch (cbiddingconditionfile.State)
            {
                case "7":
                    this.btnWorkflow.Visible = false;
                    break;
                case "9":
                    this.btnWorkflow.Visible = false;
                    break;
            }


        }


    }
}
