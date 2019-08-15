//===========================================================================
// 此文件是作为 ASP.NET 2.0 Web 项目转换的一部分修改的。
// 类名已更改，且类已修改为从文件“App_Code\Migrated\workflowcontrol\Stub_workflowformopinion_ascx_cs.cs”的抽象基类 
// 继承。
// 在运行时，此项允许您的 Web 应用程序中的其他类使用该抽象基类绑定和访问 
// 代码隐藏页。
// 关联的内容页“workflowcontrol\workflowformopinion.ascx”也已修改，以引用新的类名。
// 有关此代码模式的更多信息，请参考 http://go.microsoft.com/fwlink/?LinkId=46995 
//===========================================================================
namespace RmsPM.Web.WorkFlowControl
{
    using System;
    using System.Data;
    using System.Drawing;
    using System.Web;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;

    using Rms.ORMap;
    using RmsPM.Web.WorkFlowControl;

    /// <summary>
    ///		WorkFlowFormOpinion 的摘要说明。
    /// </summary>
    public partial class Migrated_WorkFlowFormOpinion : WorkFlowFormOpinion
    {
        private string _OpinionType = null;
        private string _ControlType = null;
        private string _OpinionUserCode = null;
        private ModuleState _StateConfirm = ModuleState.Unbeknown;

        /// <summary>
        /// 
        /// </summary>
        override public ModuleState StateConfirm
        {
            get
            {
                if (_StateConfirm == ModuleState.Unbeknown)
                {
                    if (this.ViewState["_StateConfirm"] != null)
                        return (ModuleState)this.ViewState["_StateConfirm"];
                    return ModuleState.Unbeknown;
                }
                return _StateConfirm;
            }
            set
            {
                _StateConfirm = value;
                this.ViewState["_StateConfirm"] = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        override public string CaseCode
        {
            get
            {
                if (this.ViewState["_CaseCode"] == null)
                    return "";
                return this.ViewState["_CaseCode"].ToString();
            }
            set
            {
                this.ViewState["_CaseCode"] = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        override public string TextOpinion
        {
            get
            {
                return this.OpinionTextArea.Value;
            }
            set
            {
                this.OpinionTextArea.Value = value;
            }
        }


        /// <summary>
        /// 标题
        /// </summary>
        override public string Title
        {
            get
            {
                return this.OpinionTitle.Text;
            }
            set
            {
                this.OpinionTitle.Text = value;
            }
        }
        /// <summary>
        /// 意见类型(Text：基本输入框；TextArea：多行输入框；TextAreaEsay：简单多行输入框；TextNum：数值类型输入框；)
        /// </summary>
        override public string ControlType
        {
            get
            {
                if (_ControlType == null)
                {
                    if (this.ViewState["_ControlType"] != null)
                        return this.ViewState["_ControlType"].ToString();
                    return "TextArea";
                }
                return _ControlType;
            }
            set
            {
                _ControlType = value;
                this.ViewState["_ControlType"] = value;
            }
        }

        /// <summary>
        /// 意见类型
        /// </summary>
        override public string OpinionType
        {
            get
            {
                if (_OpinionType == null)
                {
                    if (this.ViewState["_OpinionType"] != null)
                        return this.ViewState["_OpinionType"].ToString();
                    return "TextArea";
                }
                return _OpinionType;
            }
            set
            {
                _OpinionType = value;
                this.ViewState["_OpinionType"] = value;
            }
        }

        /// <summary>
        /// 关联用户代码
        /// </summary>
        override public string OpinionUserCode
        {
            get
            {
                if (_OpinionUserCode == null)
                {
                    if (this.ViewState["_OpinionUserCode"] != null)
                        return this.ViewState["_OpinionUserCode"].ToString();
                    return "";
                }
                return _OpinionUserCode;
            }
            set
            {
                _OpinionUserCode = value;
                this.ViewState["_OpinionUserCode"] = value;
            }
        }

        /// <summary>		
        /// 确认值（Approve/Reject ）
        /// </summary>
        override public string OpinionConfirm
        {
            get
            {
                return this.rdoCheck.SelectedValue;
            }
            set
            {
                this.rdoCheck.SelectedIndex = this.rdoCheck.Items.IndexOf(this.rdoCheck.Items.FindByValue(value));
            }
        }
        /// <summary>
        /// 发送窗口传回的同意否决结果
        /// </summary>
        override public string AuditValue
        {
            get
            {
                if (this.ViewState["_AuditValue"] == null)
                    return "";
                return this.ViewState["_AuditValue"].ToString();
            }
            set
            {
                this.ViewState["_AuditValue"] = value;
            }
        }



        /// <summary>
        /// 修改人:阚少明 修改日期:2006-11-19 修改结果:允许针对当前textarea进行操作;  修改原因:针对使用formopinion为业务控件使用的例子
        /// </summary>
        override public bool IsUseTextArea
        {
            get
            {
                return this.OpinionTextArea.Disabled;
            }
            set
            {
                this.OpinionTextArea.Disabled = value;
            }
        }

        /// <summary>
        /// rdocheck的Enable
        /// </summary>
        override public bool IsRdoCheck
        {
            get
            {
                return this.rdoCheck.Enabled;
            }
            set
            {
                this.rdoCheck.Enabled = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        override public bool IsUseTemplateOpinion
        {
            get
            {
                return this.sltTemplateOpinion.Disabled;
            }
            set
            {
                this.sltTemplateOpinion.Disabled = value;
            }
        }

        /// <summary>
        /// 项目代码
        /// </summary>
        override public string ProjectCode
        {
            get
            {
                if (this.ViewState["_ProjectCode"] == null)
                    return "";
                return this.ViewState["_ProjectCode"].ToString();
            }
            set
            {
                this.ViewState["_ProjectCode"] = value;
            }
        }




        /// <summary>
        /// 初始化页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, System.EventArgs e)
        {
        }

        /// <summary>
        /// 组件初始化
        /// </summary>
        override public void InitControl()
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


            if (this.StateConfirm == ModuleState.Sightless)//不可见的
            {
                this.rdoCheck.Visible = false;
                this.CheckSpan.Visible = false;
            }
            else if (this.StateConfirm == ModuleState.Operable)//可操作的
            {
                this.rdoCheck.Visible = true;
                //this.rdoCheck.Enabled = false;
                this.CheckSpan.Visible = false;
                LoadCheckData(true);
            }
            else if (this.StateConfirm == ModuleState.Eyeable)//可见的
            {
                this.rdoCheck.Visible = false;
                this.CheckSpan.Visible = true;
                LoadCheckData(false);

            }
            else if (this.StateConfirm == ModuleState.Begin)//不可见的
            {
                this.rdoCheck.Visible = false;
                this.CheckSpan.Visible = false;
                LoadCheckData(false);
            }
            else if (this.StateConfirm == ModuleState.End)//可见的
            {
                this.rdoCheck.Visible = false;
                this.CheckSpan.Visible = true;
                LoadCheckData(false);
            }
            else
            {
                this.rdoCheck.Visible = false;
                this.CheckSpan.Visible = true;
                LoadCheckData(false);
            }

            if (this.State != ModuleState.Operable)
            {

                wfsImageSign.OpinionType = this.OpinionType;
                wfsImageSign.ApplicationCode = this.ApplicationCode;
                wfsImageSign.CaseCode = this.CaseCode;
                wfsImageSign.State = this.State;
                wfsImageSign.InitControl();
                wfsImageSign.Visible = true;
            }
            else
            {
                wfsImageSign.Visible = false;
            }

        }

        /// ****************************************************************************
        /// <summary>
        /// 数据加载
        /// </summary>
        /// ****************************************************************************
        private void LoadCheckData(bool Flag)
        {
            if (this.ApplicationCode != "")
            {
                EntityData entity = this.GetData();

                //修改日期：2006-9-11 修改类型：增加以下else逻辑 修改人：clm
                //修改前代码：string ud_sOpinionConfirm = "";
                string ud_sOpinionConfirm = BLL.ConvertRule.ToString(System.Configuration.ConfigurationManager.AppSettings["WorkFlowRadioDefaultValue"]);
                if (entity.HasRecord())
                {
                    ud_sOpinionConfirm = entity.CurrentRow["OpinionConfirm"].ToString();
                    switch (ud_sOpinionConfirm)
                    {
                        case "Approve":
                            this.CheckSpan.InnerHtml = "审核：" + "同意";
                            break;
                        case "Reject":
                            this.CheckSpan.InnerHtml = "审核：" + "否决";
                            break;
                        case "":
                            this.CheckSpan.InnerHtml = "";
                            break;
                    }
                }

                this.rdoCheck.SelectedIndex = this.rdoCheck.Items.IndexOf(this.rdoCheck.Items.FindByValue(ud_sOpinionConfirm));



                if (Flag && (this.OpinionUserCode != "" && this.OpinionUserCode != ((User)Session["User"]).UserCode))
                {
                    this.rdoCheck.Visible = false;
                    this.CheckSpan.Visible = true;
                }
            }
            else//修改日期：2006-9-11 修改类型：增加以下else逻辑 修改人：clm
            {
                this.rdoCheck.SelectedIndex = this.rdoCheck.Items.IndexOf(this.rdoCheck.Items.FindByValue(BLL.ConvertRule.ToString(System.Configuration.ConfigurationManager.AppSettings["WorkFlowRadioDefaultValue"])));
            }
        }
        /// ****************************************************************************
        /// <summary>
        /// 数据加载
        /// </summary>
        /// ****************************************************************************
        private void LoadData(bool Flag)
        {
            if (this.CaseCode != "")
            {
                string OpinionTextValue = "";
                string OpinionUserValue = "";
                string OpinionDateValue = "";

                EntityData entity = this.GetData();
                if (entity.HasRecord())
                {
                    OpinionTextValue = entity.CurrentRow["OpinionText"].ToString();
                    OpinionUserValue = entity.CurrentRow["OpinionUser"].ToString();
                    OpinionDateValue = entity.GetDateTimeOnlyDate("OpinionDate");
                }

                entity.Dispose();

                if (Flag)
                {
                    if (this.OpinionUserCode == "" || (this.OpinionUserCode != "" && this.OpinionUserCode == ((User)Session["User"]).UserCode))
                    {
                        //if (System.Configuration.ConfigurationSettings.AppSettings["PMName"] == "ShiMaoPM")
                        //{
                        //    if (OpinionTextValue == "" && this.ControlType == "TextArea")
                        //        OpinionTextValue = "同意";
                        //}
                        EditBound(OpinionTextValue, OpinionUserValue, OpinionDateValue);
                    }
                    else
                    {
                        ViewBound(OpinionTextValue, OpinionUserValue, OpinionDateValue);
                    }
                }
                else
                {
                    ViewBound(OpinionTextValue, OpinionUserValue, OpinionDateValue);
                }
            }
            else
            {
                if (Flag)
                {
                    if (this.ControlType == "TextArea")
                    {
                        string UserName = RmsPM.BLL.SystemRule.GetUserNameByProjectCode(this.ProjectCode, ((User)Session["User"]).UserName, ((User)Session["User"]).UserShortName, null);
                        EditBound("", UserName, DateTime.Now.ToShortDateString());
                    }
                    else
                    {
                        string UserName = RmsPM.BLL.SystemRule.GetUserNameByProjectCode(this.ProjectCode, ((User)Session["User"]).UserName, ((User)Session["User"]).UserShortName, null);
                        EditBound("", UserName, DateTime.Now.ToShortDateString());
                    }
                }
                else
                {
                    ViewBound("", "", "");
                }
            }
        }

        public EntityData GetData()
        {
            DAL.QueryStrategy.PurchaseFlowOpinionStrategyBuilder sb = new RmsPM.DAL.QueryStrategy.PurchaseFlowOpinionStrategyBuilder();
            //sb.AddStrategy( new Strategy( DAL.QueryStrategy.PurchaseFlowOpinionStrategyName.ObjectCode,this.ApplicationCode));
            sb.AddStrategy(new Strategy(DAL.QueryStrategy.PurchaseFlowOpinionStrategyName.OpinionType, this.OpinionType));
            sb.AddStrategy(new Strategy(DAL.QueryStrategy.PurchaseFlowOpinionStrategyName.CaseCode, this.CaseCode));
            if (this.OpinionUserCode != "")
                sb.AddStrategy(new Strategy(DAL.QueryStrategy.PurchaseFlowOpinionStrategyName.OpinionUserCode, this.OpinionUserCode));
            sb.AddStrategy(new Strategy(DAL.QueryStrategy.PurchaseFlowOpinionStrategyName.StateIn, ((User)Session["User"]).UserCode));
            sb.AddOrder("OpinionDate", false);

            string sql = sb.BuildMainQueryString();
            QueryAgent QA = new QueryAgent();
            EntityData entity = QA.FillEntityData("PurchaseFlowOpinion", sql);
            QA.Dispose();
            return entity;
        }
        /// <summary>
        /// 编辑状态加载
        /// </summary>
        /// <param name="OpinionText"></param>
        /// <param name="OpinionUser"></param>
        /// <param name="OpinionDate"></param>
        private void EditBound(string OpinionTextValue, string OpinionUserValue, string OpinionDateValue)
        {
            this.OpinionLabel.Visible = false;
            this.OpinionTextAreaDiv.Visible = false;
            if (this.ControlType == "Text")
            {
                this.OpinionText.Value = OpinionTextValue;
                this.OpinionText.Visible = true;
                this.OpinionNum.Visible = false;
                this.OpinionDiv.Visible = false;
                this.OpinionTextArea.Visible = false;
                this.OpinionUserAndDate.Visible = false;
            }
            else if (this.ControlType == "TextArea")
            {
                this.OpinionTextArea.Value = OpinionTextValue;
                if (OpinionUserValue == "")
                {

                    string UserName = RmsPM.BLL.SystemRule.GetUserNameByProjectCode(this.ProjectCode, ((User)Session["User"]).UserName, ((User)Session["User"]).UserShortName, null);
                    this.OpinionUser.InnerHtml = UserName;

                    this.OpinionDate.InnerHtml = DateTime.Now.ToShortDateString();
                }
                else
                {
                    this.OpinionUser.InnerHtml = OpinionUserValue;
                    this.OpinionDate.InnerHtml = OpinionDateValue;
                }
                this.OpinionText.Visible = false;
                this.OpinionNum.Visible = false;
                this.OpinionDiv.Visible = true;
                this.OpinionTextArea.Visible = true;
                this.OpinionUserAndDate.Visible = true;

                BLL.TemplateOpinion to = new BLL.TemplateOpinion();
                to.UserCode = ((User)Session["User"]).UserCode;
                DataTable dt = to.GetTemplateOpinions();
                if (dt.Rows.Count == 0)
                {
                    this.sltTemplateOpinion.Visible = false;
                }
                else
                {
                    this.sltTemplateOpinion.Visible = true;
                    this.sltTemplateOpinion.DataSource = dt;
                    this.sltTemplateOpinion.DataTextField = "Name";
                    this.sltTemplateOpinion.DataValueField = "Center";
                    this.sltTemplateOpinion.DataBind();
                    ListItem li = new ListItem();
                    li.Text = "--常用意见--";
                    li.Value = "";
                    li.Selected = true;
                    this.sltTemplateOpinion.Items.Add(li);
                    this.sltTemplateOpinion.Attributes["onchange"] = "javascript:document.all('" + this.ClientID + "_OpinionTextArea').value = this.value;";
                }
            }
            else if (this.ControlType == "TextAreaEsay")
            {
                this.OpinionTextArea.Value = OpinionTextValue;
                this.OpinionText.Visible = false;
                this.OpinionNum.Visible = false;
                this.OpinionDiv.Visible = true;
                this.OpinionTextArea.Visible = true;
                this.OpinionUserAndDate.Visible = false;
            }
            else if (this.ControlType == "TextNum")
            {
                this.OpinionNum.Value = OpinionTextValue;
                this.OpinionText.Visible = false;
                this.OpinionNum.Visible = true;
                this.OpinionDiv.Visible = false;
                this.OpinionTextArea.Visible = false;
                this.OpinionUserAndDate.Visible = false;
            }
        }
        /// <summary>
        /// 查看状态加载
        /// </summary>
        /// <param name="OpinionText"></param>
        /// <param name="OpinionUser"></param>
        /// <param name="OpinionDate"></param>
        private void ViewBound(string OpinionTextValue, string OpinionUserValue, string OpinionDateValue)
        {
            this.OpinionText.Visible = false;
            this.OpinionTextArea.Visible = false;

            if (this.ControlType == "Text")
            {
                this.OpinionLabel.Text = OpinionTextValue;
                this.OpinionLabel.Visible = true;
                this.OpinionTextAreaDiv.Visible = false;
                this.OpinionNum.Visible = false;
                this.OpinionDiv.Visible = false;
                this.OpinionUserAndDate.Visible = false;
            }
            else if (this.ControlType == "TextArea")
            {
                this.OpinionTextAreaDiv.InnerHtml = OpinionTextValue.Replace("\n", "<br>");
                this.OpinionUser.InnerHtml = OpinionUserValue;
                this.OpinionDate.InnerHtml = OpinionDateValue;
                this.OpinionLabel.Visible = false;
                this.OpinionTextAreaDiv.Visible = true;
                this.OpinionNum.Visible = false;
                this.OpinionDiv.Visible = true;
                this.OpinionUserAndDate.Visible = true;
            }
            else if (this.ControlType == "TextAreaEsay")
            {
                this.OpinionTextAreaDiv.InnerHtml = OpinionTextValue.Replace("\n", "<br>");
                this.OpinionLabel.Visible = false;
                this.OpinionTextAreaDiv.Visible = true;
                this.OpinionNum.Visible = false;
                this.OpinionDiv.Visible = true;
                this.OpinionUserAndDate.Visible = false;
            }
            else if (this.ControlType == "TextNum")
            {
                this.OpinionNum.Value = OpinionTextValue;
                this.OpinionNum.ReadOnly = true;
                this.OpinionLabel.Visible = false;
                this.OpinionTextAreaDiv.Visible = false;
                this.OpinionNum.Visible = true;
                this.OpinionDiv.Visible = false;
                this.OpinionUserAndDate.Visible = false;
            }
        }

        /// ****************************************************************************
        /// <summary>
        /// 组织业务实体数据
        /// </summary>
        /// <returns>业务数据实体对象</returns>
        /// ****************************************************************************
        private EntityData BuildData(bool flag, bool CheckFlag, bool Stateflag)
        {
            string PurchaseFlowOpinionCode = "";
            DAL.QueryStrategy.PurchaseFlowOpinionStrategyBuilder sb = new RmsPM.DAL.QueryStrategy.PurchaseFlowOpinionStrategyBuilder();
            //sb.AddStrategy( new Strategy( DAL.QueryStrategy.PurchaseFlowOpinionStrategyName.ObjectCode,this.ApplicationCode));
            sb.AddStrategy(new Strategy(DAL.QueryStrategy.PurchaseFlowOpinionStrategyName.OpinionType, this.OpinionType));
            sb.AddStrategy(new Strategy(DAL.QueryStrategy.PurchaseFlowOpinionStrategyName.CaseCode, this.CaseCode));

            if (this.OpinionUserCode != "")
            {
                sb.AddStrategy(new Strategy(DAL.QueryStrategy.PurchaseFlowOpinionStrategyName.OpinionUserCode, this.OpinionUserCode));
            }

            sb.AddOrder("OpinionDate", false);

            string sql = sb.BuildMainQueryString();

            EntityData entityopinion = new EntityData("PurchaseFlowOpinion");
            dao.FillEntity(sql, entityopinion);

            if (entityopinion.Tables[0].Rows.Count > 0)
                PurchaseFlowOpinionCode = entityopinion.CurrentRow["PurchaseFlowOpinionCode"].ToString();

            entityopinion.Dispose();

            bool NewRecordFlag = false;
            EntityData entity = DAL.EntityDAO.PurchaseFlowDAO.GetPurchaseFlowOpinionByCode(dao, PurchaseFlowOpinionCode);
            DataRow dr;
            if (PurchaseFlowOpinionCode == "")
            {
                NewRecordFlag = true;
                PurchaseFlowOpinionCode = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("PurchaseFlowOpinion");
                dr = entity.GetNewRecord();
            }
            else
            {
                dr = entity.CurrentRow;
            }

            if (flag)
            {
                dr["PurchaseFlowOpinionCode"] = PurchaseFlowOpinionCode;
                dr["ObjectCode"] = this.ApplicationCode;
                dr["CaseCode"] = this.CaseCode;
                dr["OpinionType"] = this.OpinionType;
                dr["OpinionUserCode"] = this.OpinionUserCode;
                if (Stateflag)
                    dr["State"] = ((User)Session["User"]).UserCode;
                else
                    dr["State"] = "";

                if (this.ControlType == "Text")
                {
                    dr["OpinionText"] = this.OpinionText.Value.Trim();
                    string UserName = RmsPM.BLL.SystemRule.GetUserNameByProjectCode(this.ProjectCode, ((User)Session["User"]).UserName, ((User)Session["User"]).UserShortName, null);
                    dr["OpinionUser"] = UserName;
                    dr["OpinionDate"] = DateTime.Now.ToString();
                }
                else if (this.ControlType == "TextArea")
                {
                    dr["OpinionText"] = this.OpinionTextArea.Value;
                    string UserName = RmsPM.BLL.SystemRule.GetUserNameByProjectCode(this.ProjectCode, ((User)Session["User"]).UserName, ((User)Session["User"]).UserShortName, null);
                    dr["OpinionUser"] = UserName;
                    dr["OpinionDate"] = DateTime.Now.ToString();
                }
                else if (this.ControlType == "TextAreaEsay")
                {
                    dr["OpinionText"] = this.OpinionTextArea.Value;
                    string UserName = RmsPM.BLL.SystemRule.GetUserNameByProjectCode(this.ProjectCode, ((User)Session["User"]).UserName, ((User)Session["User"]).UserShortName, null);
                    dr["OpinionUser"] = UserName;
                    dr["OpinionDate"] = DateTime.Now.ToString();
                }
                else if (this.ControlType == "TextNum")
                {
                    dr["OpinionText"] = this.OpinionNum.Value;
                    string UserName = RmsPM.BLL.SystemRule.GetUserNameByProjectCode(this.ProjectCode, ((User)Session["User"]).UserName, ((User)Session["User"]).UserShortName, null);
                    dr["OpinionUser"] = UserName;
                    dr["OpinionDate"] = DateTime.Now.ToString();
                }

                if (CheckFlag)
                {
                    //dr["OpinionConfirm"] = this.rdoCheck.SelectedItem.Value == null ? "" : this.rdoCheck.SelectedItem.Value;
                    dr["OpinionConfirm"] = this.AuditValue;
                }

                if (NewRecordFlag)
                {
                    entity.AddNewRecord(dr);
                }
            }
            return entity;
        }
        /// ****************************************************************************
        /// <summary>
        /// 提交数据
        /// </summary>
        /// ****************************************************************************
        override public void SubmitData()
        {
            if (this.ApplicationCode != "")
            {
                if (this.OpinionUserCode == "" || (this.OpinionUserCode != "" && this.OpinionUserCode == ((User)(Session["User"])).UserCode))
                {
                    if (this.dao == null)
                    {
                        DAL.EntityDAO.PurchaseFlowDAO.SubmitAllPurchaseFlowOpinion(this.BuildData(true, this.rdoCheck.Visible, false));
                    }
                    else
                    {
                        dao.EntityName = "PurchaseFlowOpinion";
                        dao.SubmitEntity(this.BuildData(true, this.rdoCheck.Visible, false));
                    }
                }
            }
            else
            {
                Rms.Web.JavaScript.Alert(true, "没有需要填写意见的单据！");
            }
        }
        /// ****************************************************************************
        /// <summary>
        /// 提交数据
        /// </summary>
        /// ****************************************************************************
        public void SubmitData(bool flag)
        {
            if (this.ApplicationCode != "")
            {
                if (this.OpinionUserCode == "" || (this.OpinionUserCode != "" && this.OpinionUserCode == ((User)(Session["User"])).UserCode))
                {
                    if (this.dao == null)
                    {
                        DAL.EntityDAO.PurchaseFlowDAO.SubmitAllPurchaseFlowOpinion(this.BuildData(true, this.rdoCheck.Visible, flag));
                    }
                    else
                    {
                        dao.EntityName = "PurchaseFlowOpinion";
                        dao.SubmitEntity(this.BuildData(true, this.rdoCheck.Visible, flag));
                    }
                }
            }
            else
            {
                Rms.Web.JavaScript.Alert(true, "没有需要填写意见的单据！");
            }
        }

        /// ****************************************************************************
        /// <summary>
        /// 删除
        /// </summary>
        /// ****************************************************************************
        //		public void DeleteData()
        override public void DeleteData()
        {
            if (this.ApplicationCode != "")
            {
                if (this.dao == null)
                {
                    DAL.EntityDAO.PurchaseFlowDAO.DeletePurchaseFlowOpinion(this.BuildData(false, false, false));
                }
                else
                {
                    dao.EntityName = "PurchaseFlowOpinion";
                    dao.SubmitEntity(this.BuildData(false, false, false));
                }
            }
            else
            {
                Rms.Web.JavaScript.Alert(true, "没有需要填写意见的单据！");
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
        ///		设计器支持所需的方法 - 不要使用代码编辑器
        ///		修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {

        }
        #endregion

    }
}