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
using Rms.ORMap;

namespace RmsPM.Web.DesignDocumentManage
{   

    /// <summary>
    ///		DesignDocumentModify 的摘要说明。

    /// </summary>
    public partial class DesignDocumentManage_DesignDocumentForFlow : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            AttachMentAdd1.AttachMentType = "DesignDocument";
            AttachMentAdd1.MasterCode = this.ApplicationCode;
            AttachMentList1.AttachMentType = "DesignDocument";
            AttachMentList1.MasterCode = this.ApplicationCode;
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
                EyeableDiv.Visible = false;
                OperableDiv.Visible = true;
            }
            else if (this.State == ModuleState.Eyeable)//可见的

            {
                LoadData(false);
                OperableDiv.Visible = false;
                EyeableDiv.Visible = true;
            }
            else if (this.State == ModuleState.Begin)//可见的

            {
                LoadData(false);
                OperableDiv.Visible = false;
                EyeableDiv.Visible = true;
            }
            else if (this.State == ModuleState.End)//可见的

            {
                LoadData(false);
                OperableDiv.Visible = false;
                EyeableDiv.Visible = true;
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
            if (this.ApplicationCode != "")
            {
                BLL.DesignDocument cDesignDocument = new BLL.DesignDocument();
                cDesignDocument.DesignDocumentCode = this.ApplicationCode;
                this.DocumentState = cDesignDocument.State;

                if (Flag)
                {
                    this.txtTitle.Value = cDesignDocument.Title;
                    this.txtProjectCode.InnerHtml = BLL.ProjectRule.GetProjectName(cDesignDocument.ProjectCode);
                    this.txtUnitCode.Value = cDesignDocument.UnitCode;
                    this.txtContext.Value = cDesignDocument.Context.Replace("\n", "<br>");
                }
                else
                {
                    this.tdTitle.InnerHtml = cDesignDocument.Title;
                    this.tdProjectCode.InnerHtml = BLL.ProjectRule.GetProjectName(cDesignDocument.ProjectCode);
                    this.tdUnitCode.InnerHtml = BLL.SystemRule.GetUnitName(cDesignDocument.UnitCode);
                    this.tdContext.InnerHtml = cDesignDocument.Context;
                }
            }
            else
            {
                this.txtProjectCode.InnerHtml = BLL.ProjectRule.GetProjectName(this.ProjectCode);
                this.tdProjectCode.InnerHtml = BLL.ProjectRule.GetProjectName(this.ProjectCode);
                if (Flag)
                {
                    //this.txtUnitCode.Value = cDesignDocument.UnitCode;

                }
            }
        }

        /// ****************************************************************************
        /// <summary>
        /// 提交数据
        /// </summary>
        /// ****************************************************************************
        public void SubmitData()
        {
            BLL.DesignDocument cDesignDocument = new BLL.DesignDocument();
            cDesignDocument.DesignDocumentCode = this.ApplicationCode;
            cDesignDocument.Title = this.txtTitle.Value;
            cDesignDocument.ProjectCode = this.ProjectCode;
            cDesignDocument.UnitCode = this.txtUnitCode.Value;
            cDesignDocument.Context = this.txtContext.Value;
            cDesignDocument.CreateDate = DateTime.Now.ToString();
            cDesignDocument.CreateUser = this.UserCode;
            cDesignDocument.State = this.DocumentState;
            cDesignDocument.Flag = this.DocumentFlag;
            cDesignDocument.dao = this.dao;
            cDesignDocument.DesignDocumentSubmit();

            if (this.ApplicationCode == "")
            {
                this.ApplicationCode = cDesignDocument.DesignDocumentCode;
                this.AttachMentAdd1.SaveAttachMent(this.ApplicationCode);
            }
        }
        /// ****************************************************************************
        /// <summary>
        /// 删除数据
        /// </summary>
        /// ****************************************************************************
        public void Delete()
        {
            BLL.DesignDocument cDesignDocument = new BLL.DesignDocument();
            cDesignDocument.DesignDocumentCode = this.ApplicationCode;
            cDesignDocument.dao = this.dao;
            cDesignDocument.DesignDocumentDelete();
        }
        /// ****************************************************************************
        /// <summary>
        /// 提交数据
        /// </summary>
        /// ****************************************************************************
        public void ConfirmData(bool flag,string Type)
        {
            BLL.DesignDocument cDesignDocument = new BLL.DesignDocument();
            cDesignDocument.DesignDocumentCode = this.ApplicationCode;
            if (flag)
            {
                cDesignDocument.State = Type + "2";
            }
            else 
            {
                cDesignDocument.State = Type + "3";
            }
            cDesignDocument.dao = this.dao;
            cDesignDocument.DesignDocumentSubmit();
        }
        /// <summary>
        /// 业务代码
        /// </summary>
        public string ApplicationCode
        {
            get
            {
                if (this.ViewState["_ApplicationCode"] != null)
                    return this.ViewState["_ApplicationCode"].ToString();
                return "";
            }
            set
            {
                this.ViewState["_ApplicationCode"] = value;
            }
        }

        /// <summary>
        /// 模块状态

        /// </summary>
        public ModuleState State
        {
            get
            {
                if (this.ViewState["_State"] != null)
                    return (ModuleState)this.ViewState["_State"];
                return ModuleState.Unbeknown;
            }
            set
            {
                this.ViewState["_State"] = value;
            }
        }
        /// <summary>
        /// 用户代码
        /// </summary>
        public string UserCode
        {
            get
            {
                if (this.ViewState["_UserCode"] != null)
                    return this.ViewState["_UserCode"].ToString();
                return "";
            }
            set
            {
                this.ViewState["_UserCode"] = value;
            }
        }
        /// <summary>
        /// 用户代码
        /// </summary>
        public string ProjectCode
        {
            get
            {
                if (this.ViewState["_ProjectCode"] != null)
                    return this.ViewState["_ProjectCode"].ToString();
                return "";
            }
            set
            {
                this.ViewState["_ProjectCode"] = value;
            }
        }
        /// <summary>
        /// 方案状态

        /// </summary>
        public string DocumentState
        {
            get
            {
                if (this.ViewState["_DocumentState"] != null)
                    return this.ViewState["_DocumentState"].ToString();
                return "";
            }
            set
            {
                this.ViewState["_DocumentState"] = value;
            }
        }
        /// <summary>
        /// Flag
        /// </summary>
        public string DocumentFlag
        {
            get
            {
                if (this.ViewState["_DocumentFlag"] != null)
                    return this.ViewState["_DocumentFlag"].ToString();
                return "";
            }
            set
            {
                this.ViewState["_DocumentFlag"] = value;
            }
        }
        /// <summary>
        /// Type
        /// </summary>
        public string DocumentType
        {
            get
            {
                if (this.ViewState["_DocumentType"] != null)
                    return this.ViewState["_DocumentType"].ToString();
                return "";
            }
            set
            {
                this.ViewState["_DocumentType"] = value;
            }
        }

        private StandardEntityDAO _dao = null;
        /// <summary>
        /// 事务对象
        /// </summary>
        public StandardEntityDAO dao
        {
            get
            {
                return this._dao;
            }
            set
            {
                _dao = value;
            }
        }
        public string Title
        {
            get { return this.txtTitle.Value; }
        }
        public string Unit
        {
            get { return this.txtUnitCode.Value; }
        }
    }
}

