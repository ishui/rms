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
using System.Text;

using RmsPM.BLL;
using RmsPM.DAL.EntityDAO;
using RmsPM.DAL.QueryStrategy;
using RmsPM.Web;
using Rms.ORMap;


namespace RmsPM.Web.Supplier
{
    /// <summary>
    /// SupplierInfo 的摘要说明。
    /// </summary>
    public partial class SupplierInfo : PageBase
    {
        protected System.Web.UI.WebControls.Label lblU8Code;
        protected System.Web.UI.HtmlControls.HtmlInputButton btnNew;


        /// <summary>
        /// 单位评分评审页面
        /// </summary>
        public string SupplierGradePage
        {
            get
            {
                return BLL.WorkFlowRule.GetProcedureURLByName("单位评分审核");
            }
        }


        public string SupplierPursveGradePage
        {
            get
            {
                return BLL.WorkFlowRule.GetProcedureURLByName("供应商评分审核");
            }
        }

        public string SupplierSurveyOpinionPage
        {
            get
            {
                return BLL.WorkFlowRule.GetProcedureURLByName("厂商调查意见审核");
            }
        }


        /// <summary>
        /// 获取单位信息页面
        /// </summary>
        public string SupplierGradeInfoPage
        {
            get
            {
                return "../SupplierGrade/SupplierGradeModif.aspx";
            }
        }
        protected void Page_Load(object sender, System.EventArgs e)
        {
            // 在此处放置用户代码以初始化页面
            if (!IsPostBack)
            {

                string supplierCode = Request["SupplierCode"] + "";

                ArrayList ar = user.GetResourceRight(supplierCode, "Supplier");
                if (!ar.Contains("140101"))
                {
                    Response.Redirect("../RejectAccess.aspx");
                    Response.End();
                }

                LoadData();
                // 权限
                if (!ar.Contains("140103"))
                    this.btnModify.Visible = false;
                EntityData entityContract = DAL.EntityDAO.ContractDAO.GetContractBySupplierCode(supplierCode);
                EntityData entitySupplier = DAL.EntityDAO.ProjectDAO.GetStandard_SupplierByCode(supplierCode);
                if (!ar.Contains("140104") || entityContract.HasRecord())
                {
                    this.btnDelete.Visible = false;
                }
                entityContract.Dispose();
                if (!user.HasRight("100102"))
                    this.btnNewDocument.Visible = false;

                if (!user.HasRight("141504"))
                    this.BtnLinkMan.Visible = false;

                if (!user.HasRight("1106"))
                    this.btnModifySubjectSet.Visible = false;

                if (!user.HasRight("141803"))
                    this.btnCompanyTitle.Visible = false;
                switch (this.up_sPMName.ToUpper())
                { 
                    case "SHIDAIPM":
                        if (entitySupplier.GetInt("Status") == 1 || entitySupplier.GetInt("Status") == 2)
                        {
                            this.btnSupplierAuditing.Visible = false;
                            this.btnSingleAuditing.Visible = false;
                            break;
                        }
                        this.btnSupplierAuditing.Visible = true;
                        this.btnSingleAuditing.Visible = true;
                        if (!user.HasRight("140107"))
                        {
                            this.btnSingleAuditing.Visible = false;
                        }
                        break;
                    default:
                        this.btnSupplierAuditing.Visible=false;
                        this.btnSingleAuditing.Visible = false;
                        break;
                }
                ViewState["_SupplierAuditingURL"] = BLL.WorkFlowRule.GetProcedureURLByName("厂商审核流程");
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
            this.dgDocumentList.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgDocumentList_DeleteCommand);

        }
        #endregion

        private void LoadData()
        {
            try
            {
                //基本信息

                string SupplierCode = Request["SupplierCode"] + "";
                string projectCode = Request["projectCode"] + "";

                EntityData entity = ProjectDAO.GetStandard_SupplierByCode(SupplierCode);
                if (entity.HasRecord())
                {
                    this.lblSupplierName.Text = entity.GetString("SupplierName");
                    this.lblAbbreviation.Text = entity.GetString("Abbreviation");
                    this.lblAreaCode.Text = entity.GetString("AreaCode");

                    this.lblProduct.Text = entity.GetString("Product").Replace("\n", "<br>");
                    this.lblQuality.Text = entity.GetString("Quality").Replace("\n", "<br>");
                    this.lblAchievement.Text = entity.GetString("Achievement").Replace("\n", "<br>");
                    this.lblCheckOpinion.Text = entity.GetString("CheckOpinion").Replace("\n", "<br>");

                    this.lblContractPerson.Text = entity.GetString("ContractPerson");
                    this.lblCreditLevel.Text = entity.GetString("CreditLevel");
                    this.lblIndustrySort.Text = entity.GetString("IndustrySort");
                    this.lblIndustryType.Text = entity.GetString("IndustryType");
                    this.lblLicenseID.Text = entity.GetString("LicenseID");
                    
                    this.lblOfficePhone.Text = entity.GetString("OfficePhone");
                    this.lblMobile.Text = entity.GetString("Mobile");
                    this.lblFax.Text = entity.GetString("Fax");
                    this.lblPostCode.Text = entity.GetString("PostCode");
                    this.lblEmail.Text = entity.GetString("EMail");
                    this.lblWebAddress.Text = entity.GetString("WebAddress");

                    this.lblRegisteredAddress.Text = entity.GetString("RegisteredAddress");
                    this.lblRegisteredCapital.Text = entity.GetString("RegisteredCapital");
                    this.lblSJHG.Text = entity.GetString("SJHG");
                    this.lblTaxID.Text = entity.GetString("TaxID");
                    this.lblTaxNo.Text = entity.GetString("TaxNo");
                    this.lblWorkAddress.Text = entity.GetString("WorkAddress");
                    this.lblWorkTimeLimit.Text = entity.GetString("WorkTimeLimit");
                    this.lblArtificialPerson.Text = entity.GetString("ArtificialPerson");
                    this.lblWorkScope.Text = entity.GetString("WorkScope");
                    this.lblStructure.Text = entity.GetString("Structure");
                    this.lblRemark.Text = entity.GetString("Remark");
                    this.lblTypeName.Text = BLL.ProjectRule.GetSupplierTypeName(entity.GetString("SupplierTypeCode"));

                    this.lblSaleType.Text = entity.GetString("saleType");
                    this.lblCharacterType.Text = entity.GetString("characterType");
                    this.lblCCC.Text = RmsPM.BLL.SupplierRule.GetTypeName(entity.GetString("IsCCC"));
                    this.lblISO.Text = RmsPM.BLL.SupplierRule.GetTypeName(entity.GetString("IsISO"));
                    this.lblQualityGrade.Text = entity.GetString("QualityGrade") == "" ? "未定" : entity.GetString("QualityGrade");
                    this.lblOpenBank.Text = entity.GetString("OpenBank");
                    this.lblReciver.Text = entity.GetString("Reciver");
                    this.lblAccount.Text = entity.GetString("Account");
                }

                switch (this.up_sPMName.ToLower())
                {
                    case "shidaipm":
                        this.isAuditted.Visible = true;
                        this.TdisAuditted.Visible = true;
                        this.isAuditted.Text = RmsPM.BLL.SupplierRule.GetIsAuditted(entity.GetInt("Status"));
                        this.PreWorkFlowPoint.ColSpan = 4;
                        break;


                    case "shimaopm":
                        this.btnGradeAdd.Visible = this.user.HasRight("2701");
                        this.btnPursveWorkflow.Visible = this.user.HasRight("2709");
                        this.DataGrid_supplierRecord.Visible = false;
                        this.DataGrid_supplierGrade.Visible = true;
                        RmsPM.BLL.GradeMessage cgradeMessage = new GradeMessage();
                        cgradeMessage.SupplierCode = SupplierCode;
                        //cgradeMessage.State = "0";
                        System.Data.DataTable dtGradeMessage = cgradeMessage.GetGradeMessages();
                        RmsPM.DAL.QueryStrategy.WorkFlowHistory sbGradeMessage = new RmsPM.DAL.QueryStrategy.WorkFlowHistory();
                        sbGradeMessage.AddStrategy(new Strategy(WorkFlowHistoryStrategyName.ProcedureNameAndApplicationCodein, this.GetWorkFlowListString(dtGradeMessage)));



                        if (!((User)Session["User"]).HasOperationRight("090102"))
                        {
                            sbGradeMessage.AddStrategy(new Strategy(WorkFlowHistoryStrategyName.ActUserCode, ((User)Session["User"]).UserCode));
                        }
                        sbGradeMessage.AddOrder("CreateDate", false);



                        string sqlGradeMessage = sbGradeMessage.BuildMainQueryString();

                        QueryAgent qaGradeMessage = new QueryAgent();
                        DataSet dsGradeMessage = qaGradeMessage.ExecSqlForDataSet(sqlGradeMessage);
                        qaGradeMessage.Dispose();
                        if (dsGradeMessage != null)
                        {
                            DataTable dttempgradeMessage = dsGradeMessage.Tables[0];
                            dttempgradeMessage.Columns.Add("ProjectManage", System.Type.GetType("System.String"));
                            dttempgradeMessage.Columns.Add("State", System.Type.GetType("System.String"));
                            dttempgradeMessage.Columns.Add("SupplierCode", System.Type.GetType("System.String"));

                            foreach (DataRow dr in dttempgradeMessage.Select())
                            {
                                if (dtGradeMessage.Select("GradeMessageCode='" + dr["ApplicationCode"] + "'").Length != 0)
                                {
                                    dr["ProjectManage"] = dtGradeMessage.Select("GradeMessageCode='" + dr["ApplicationCode"] + "'")[0]["ProjectManage"].ToString();
                                    dr["State"] = dtGradeMessage.Select("GradeMessageCode='" + dr["ApplicationCode"] + "'")[0]["State"].ToString();
                                    dr["SupplierCode"] = dtGradeMessage.Select("GradeMessageCode='" + dr["ApplicationCode"] + "'")[0]["SupplierCode"].ToString();
                                }
                            }

                            this.DataGrid_supplierGrade.DataSource = dttempgradeMessage;

                            this.WorkFlowPoint.Visible = true;
                            this.lblGradePoint.Visible = true;
                            if (dtGradeMessage.Select("state='0'").Length != 0)
                                this.lblGradePoint.Text = RmsPM.BLL.GradeList.GetSumGradePoint(dtGradeMessage.Select("state='0'")[0]["GradeMessageCode"].ToString());
                            this.DataGrid_supplierGrade.DataBind();

                        }
                        this.PreAuditted.ColSpan = 4;
                        break;


                    default:
                        this.btnPG.Visible = true;
                        this.DataGrid_supplierRecord.Visible = true;
                        this.DataGrid_supplierGrade.Visible = false;
                        this.isAuditted.Visible = false;
                        this.TdisAuditted.Visible = false;
                        this.PreAuditted.ColSpan = 4;
                        this.PreWorkFlowPoint.ColSpan = 4;
                        DataGrid_supplierRecord.DataSource = new DataView(entity.Tables["SupplierOpinion"], "", "OpinionDate DESC", DataViewRowState.CurrentRows);
                        DataGrid_supplierRecord.DataBind();
                        break;
                }

                ContractStrategyBuilder sb = new ContractStrategyBuilder();
                sb.AddStrategy(new Strategy(ContractStrategyName.SupplierCode, SupplierCode));
                sb.AddStrategy(new Strategy(ContractStrategyName.Status, "0,1,2"));
                if (projectCode != "")
                    sb.AddStrategy(new Strategy(ContractStrategyName.ProjectCode, projectCode));
                ArrayList arAccess = new ArrayList();
                arAccess.Add("050101");
                arAccess.Add(user.UserCode);
                arAccess.Add(user.BuildStationCodes());
                sb.AddStrategy(new Strategy(ContractStrategyName.AccessRange, arAccess));
                string sql = sb.BuildMainQueryString();
                QueryAgent qa = new QueryAgent();
                EntityData contract = qa.FillEntityData("Contract", sql);
                qa.Dispose();

                contract.CurrentTable.Columns.Add("TypeName");
                contract.CurrentTable.Columns.Add("StatusName");
                contract.CurrentTable.Columns.Add("CheckDisplay");			//是否有权限审核,控制按钮的显示隐藏
                contract.CurrentTable.Columns.Add("AHMoney", System.Type.GetType("System.Decimal"));
                contract.CurrentTable.Columns.Add("PHMoney", System.Type.GetType("System.Decimal"));
                int iCount = contract.CurrentTable.Rows.Count;
                for (int i = 0; i < iCount; i++)
                {
                    contract.SetCurrentRow(i);
                    contract.CurrentRow["TypeName"] = BLL.ContractRule.GetContractTypeName(contract.GetString("Type"));
                    contract.CurrentRow["StatusName"] = BLL.ContractRule.GetContractStatusName(contract.GetInt("Status").ToString());
                    decimal ah = BLL.ContractRule.GetContractPayment(contract.GetString("ContractCode"));
                    decimal totalMoney = contract.GetDecimal("TotalMoney");
                    contract.CurrentRow["AHMoney"] = ah;
                    contract.CurrentRow["PHMoney"] = totalMoney - ah;
                }
                this.dgContract.DataSource = contract;
                this.dgContract.DataBind();
                contract.Dispose();

                LoadDocument();

                // 当前厂商询价记录
                LoadEnquiry();

                //当前厂商联系人
                LoadLinkman();

                //厂商财务编码
                LoadSupplierSubjectSet(entity);

                //添加调查意见
                LoadSurvey();

                //加载公司主题
                LoadCompanyTitle();



                entity.Dispose();
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "载入页面出错");
                Response.Write(Rms.Web.JavaScript.Alert(true, "载入页面出错"));
            }
        }


        private string GetSurveyListString(DataTable dtSurveyOpinion)
        {
            string ListString = "";

            for (int i = 0; i < dtSurveyOpinion.Rows.Count; i++)
            {
                if (i != dtSurveyOpinion.Rows.Count - 1)
                    ListString += "'厂商调查意见审核" + dtSurveyOpinion.Rows[i]["SupplierSurveyOpinionCode"].ToString() + "',";
                else
                    ListString += "'厂商调查意见审核" + dtSurveyOpinion.Rows[i]["SupplierSurveyOpinionCode"].ToString() + "'";
            }

            if (ListString == "")
            {
                ListString += "'厂商调查意见审核" + "" + "'";
            }


            return ListString;
        }

        private string GetWorkFlowListString(DataTable dtGradeMessage)
        {
            string ListString = "";

            for (int i = 0; i < dtGradeMessage.Rows.Count; i++)
            {
                if (i != dtGradeMessage.Rows.Count - 1)
                    ListString += "'单位评分审核" + dtGradeMessage.Rows[i]["GradeMessageCode"].ToString() + "',";
                else
                    ListString += "'单位评分审核" + dtGradeMessage.Rows[i]["GradeMessageCode"].ToString() + "'";
            }

            if (ListString == "")
            {
                ListString += "'单位评分审核" + "" + "'";
            }


            return ListString;
        }

        private void LoadCompanyTitle()
        {
            try
            {
                string SupplierCode = Request["SupplierCode"] + "";
                RmsPM.BLL.SupplierTitle cSupplierTitle = new SupplierTitle();
                cSupplierTitle.SupplierCode = SupplierCode;
                DataTable dtSupplier = cSupplierTitle.GetSupplierTitles();
                StringBuilder SB = new StringBuilder();

                SB.Append("<tr>");
                foreach (DataRow dr in dtSupplier.Select())
                {
                    SB.Append("<td class=\"form-item\" width='12%' noWrap>公司标题：</td>");
                    SB.AppendFormat("<td noWrap><a onclick='OpenCompanyInfo({0});return false'>{1}</a></td>", dr["SupplierTitleCode"].ToString(), dr["Title"].ToString());
                }
                SB.Append("</tr>");
                SB.Append("<tr>");
                foreach (DataRow dr in dtSupplier.Select())
                {
                    SB.Append("<td class=\"form-item\"  width='12%' noWrap> 银行帐号：</td>");
                    SB.AppendFormat("<td noWrap >{0}</td>", dr["BankAccount"].ToString());
                }
                SB.Append("</tr>");

                this.DivCompanyTitle.InnerHtml = SB.ToString();
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "载入公司标题出错");
                Response.Write(Rms.Web.JavaScript.Alert(true, "载入公司标题出错"));
            }
        }

        private void LoadSurvey()
        {
            try
            {
                string SupplierCode = Request["SupplierCode"] + "";

                RmsPM.BLL.SupplierSurveyOpinion cSupplierSurveyOpinion = new SupplierSurveyOpinion();
                cSupplierSurveyOpinion.SupplierCode = SupplierCode;
                //cSupplierSurveyOpinion.State = "0";
                System.Data.DataTable dtSupplierSurveyOpinion = cSupplierSurveyOpinion.GetSupplierSurveyOpinions();
                RmsPM.DAL.QueryStrategy.WorkFlowHistory sbSupplierSurveyOpinion = new RmsPM.DAL.QueryStrategy.WorkFlowHistory();
                sbSupplierSurveyOpinion.AddStrategy(new Strategy(WorkFlowHistoryStrategyName.ProcedureNameAndApplicationCodein, this.GetSurveyListString(dtSupplierSurveyOpinion)));

                if (!((User)Session["User"]).HasOperationRight("090102"))
                {
                    sbSupplierSurveyOpinion.AddStrategy(new Strategy(WorkFlowHistoryStrategyName.ActUserCode, ((User)Session["User"]).UserCode));
                }
                sbSupplierSurveyOpinion.AddOrder("CreateDate", false);



                string sqlSupplierSurveyOpinion = sbSupplierSurveyOpinion.BuildMainQueryString();

                QueryAgent qaSupplierSurveyOpinion = new QueryAgent();
                DataSet dsSupplierSurveyOpinion = qaSupplierSurveyOpinion.ExecSqlForDataSet(sqlSupplierSurveyOpinion);
                qaSupplierSurveyOpinion.Dispose();
                if (dsSupplierSurveyOpinion != null)
                {
                    DataTable dttempSupplierSurveyOpinion = dsSupplierSurveyOpinion.Tables[0];
                    dttempSupplierSurveyOpinion.Columns.Add("ZYName", System.Type.GetType("System.String"));
                    dttempSupplierSurveyOpinion.Columns.Add("State", System.Type.GetType("System.String"));
                    dttempSupplierSurveyOpinion.Columns.Add("SupplierCode", System.Type.GetType("System.String"));
                    dttempSupplierSurveyOpinion.Columns.Add("Grade", System.Type.GetType("System.String"));
                    dttempSupplierSurveyOpinion.Columns.Add("AdviceGrade", System.Type.GetType("System.String"));
                    dttempSupplierSurveyOpinion.Columns.Add("SurveyDate", System.Type.GetType("System.String"));

                    foreach (DataRow dr in dttempSupplierSurveyOpinion.Select())
                    {
                        if (dtSupplierSurveyOpinion.Select("SupplierSurveyOpinionCode='" + dr["ApplicationCode"] + "'").Length != 0)
                        {
                            dr["ZYName"] = dtSupplierSurveyOpinion.Select("SupplierSurveyOpinionCode='" + dr["ApplicationCode"] + "'")[0]["ZYName"].ToString();
                            dr["State"] = dtSupplierSurveyOpinion.Select("SupplierSurveyOpinionCode='" + dr["ApplicationCode"] + "'")[0]["State"].ToString();
                            dr["SupplierCode"] = dtSupplierSurveyOpinion.Select("SupplierSurveyOpinionCode='" + dr["ApplicationCode"] + "'")[0]["SupplierCode"].ToString();
                            dr["Grade"] = dtSupplierSurveyOpinion.Select("SupplierSurveyOpinionCode='" + dr["ApplicationCode"] + "'")[0]["Grade"].ToString();
                            dr["AdviceGrade"] = dtSupplierSurveyOpinion.Select("SupplierSurveyOpinionCode='" + dr["ApplicationCode"] + "'")[0]["AdviceGrade"].ToString();
                            dr["SurveyDate"] = dtSupplierSurveyOpinion.Select("SupplierSurveyOpinionCode='" + dr["ApplicationCode"] + "'")[0]["SurveyDate"].ToString();
                        }
                    }
                    dgsurvey.DataSource = dttempSupplierSurveyOpinion;
                    dgsurvey.DataBind();
                }




                //RmsPM.BLL.SupplierSurveyOpinion cSupplierSurveytemp = new SupplierSurveyOpinion();
                //cSupplierSurveytemp.SupplierCode = SupplierCode;
                //cSupplierSurveytemp.State = "0";
                //DataTable dtSupplierSurveytemp = cSupplierSurveytemp.GetSupplierSurveyOpinions();
                //if (dtSupplierSurveytemp.Rows.Count != 0)
                //{
                //    this.lblQualityGrade.Text = dtSupplierSurveytemp.Rows[0]["Grade"].ToString();
                //    // 权限
                //    if (!this.user.HasRight("140106"))
                //        this.btnModify.Visible = false;

                //}
                //else
                //{
                //    this.lblQualityGrade.Text = "未定";
                //}
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "载入调查意见出错");
                Response.Write(Rms.Web.JavaScript.Alert(true, "载入调查意见出错"));
            }
        }

        private void LoadLinkman()
        {
            try
            {
                string SupplierCode = Request["SupplierCode"] + "";

                this.btnZiZhi.Visible = this.user.HasRight("141704");

                RmsPM.BLL.SupplierLinkman csupplierLinkman = new SupplierLinkman();
                csupplierLinkman.SupplierCode = SupplierCode;
                DataTable dtlinkmanList = csupplierLinkman.GetSupplierLinkmans();
                DtLinkmanList.DataSource = dtlinkmanList;
                DtLinkmanList.DataBind();

            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "载入厂商联系人出错");
                Response.Write(Rms.Web.JavaScript.Alert(true, "载入厂商联系人出错"));
            }
        }

        //载入厂商评估记录
        private void LoadSupplierOpinion(EntityData entity)
        {
            try
            {
                entity.SetCurrentTable("SupplierOpinion");
                DataGrid_supplierRecord.DataSource = entity.CurrentTable;
                DataGrid_supplierRecord.DataBind();
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "载入厂商评估记录出错");
                Response.Write(Rms.Web.JavaScript.Alert(true, "载入厂商评估记录出错"));
            }
        }

        //载入厂商财务编码
        private void LoadSupplierSubjectSet(EntityData entity)
        {
            try
            {
                entity.SetCurrentTable("SupplierSubjectSet");
                this.dgSupplierSubjectSet.DataSource = entity.CurrentTable;
                this.dgSupplierSubjectSet.DataBind();
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "载入厂商财务编码出错");
                Response.Write(Rms.Web.JavaScript.Alert(true, "载入厂商财务编码出错"));
            }
        }

        //加载相关文档
        private void LoadDocument()
        {
            try
            {
                string Code = Request["SupplierCode"] + "";
                ArrayList ar = new ArrayList();
                ar.Add("000006");
                ar.Add(Code);

                DAL.QueryStrategy.DocumentStrategyBuilder DSB = new DAL.QueryStrategy.DocumentStrategyBuilder();
                DSB.AddStrategy(new Strategy(DAL.QueryStrategy.DocumentStrategyName.Code, ar));


                //权限
                ArrayList arA = new ArrayList();
                arA.Add("100101");
                arA.Add(user.UserCode);
                arA.Add(user.BuildStationCodes());
                DSB.AddStrategy(new Strategy(DAL.QueryStrategy.DocumentStrategyName.AccessRange, arA));

                string Sql = DSB.BuildMainQueryString();
                QueryAgent QA = new QueryAgent();
                EntityData entityDocument = QA.FillEntityData("Document", Sql);
                QA.Dispose();
                if (entityDocument.HasRecord())
                {
                    if (entityDocument.CurrentRow["MainText"].ToString().Length >= 50)
                    {
                        entityDocument.CurrentRow["MainText"] = entityDocument.CurrentRow["MainText"].ToString().Substring(0, 50) + "...";
                    }
                }
                this.dgDocumentList.DataSource = entityDocument;
                this.dgDocumentList.DataBind();
                entityDocument.Dispose();
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "载入厂商评估记录出错");
                Response.Write(Rms.Web.JavaScript.Alert(true, "载入厂商评估记录出错"));
            }
        }


        private void dgDocumentList_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            string supplierCode = Request["SupplierCode"] + "";
            BLL.DocumentRule.Instance().DeleteDocumentConfig(e.Item.Cells[0].Text, "000006", supplierCode);

            Response.Write(Rms.Web.JavaScript.ScriptStart);
            Response.Write("window.navigate('SupplierInfo.aspx?Page=1&SupplierCode=" + supplierCode + "');");
            Response.Write(Rms.Web.JavaScript.ScriptEnd);
        }

        private void LoadEnquiry()
        {
            EnquiryStrategyBuilder esb = new EnquiryStrategyBuilder();
            ArrayList arA = new ArrayList();
            arA.Add("130202");
            arA.Add(user.UserCode);
            arA.Add(user.BuildStationCodes());
            esb.AddStrategy(new Strategy(DAL.QueryStrategy.EnquiryStrategyName.AccessRange, arA));
            esb.AddStrategy(new Strategy(DAL.QueryStrategy.EnquiryStrategyName.SupplierCode, (string)Request["SupplierCode"]));
            string sql = esb.BuildMainQueryString();
            QueryAgent QA = new QueryAgent();
            DataSet ds = QA.ExecSqlForDataSet(sql);
            QA.Dispose();

            this.dgEnquiry.DataSource = Process(ds.Tables[0]);
            this.dgEnquiry.DataBind();
        }

        private DataTable Process(DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["Remark"] = (dt.Rows[i]["Remark"].ToString().Length > 8) ? dt.Rows[i]["Remark"].ToString().Substring(0, 8) + "..." : dt.Rows[i]["Remark"];
            }
            return dt;
        }

        protected void btnDelete_ServerClick(object sender, System.EventArgs e)
        {
            string supplierCode = Request["SupplierCode"] + "";
            if (supplierCode == "")
                return;
            try
            {
                EntityData entity = DAL.EntityDAO.ProjectDAO.GetStandard_SupplierByCode(supplierCode);
                DAL.EntityDAO.ProjectDAO.DeleteStandard_Supplier(entity);
                entity.Dispose();

                Response.Write(Rms.Web.JavaScript.ScriptStart);
                Response.Write(Rms.Web.JavaScript.OpenerReload(false));
                Response.Write(Rms.Web.JavaScript.WinClose(false));
                Response.Write(Rms.Web.JavaScript.ScriptEnd);

            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
            }
        }

        private void btnRefreshDocument_ServerClick(object sender, System.EventArgs e)
        {
            LoadDocument();
        }

        protected void btnSingle_Click(object sender, System.EventArgs e)
        {
            string supplierCode = Request["SupplierCode"] + "";
            EntityData entitySupplier = DAL.EntityDAO.ProjectDAO.GetStandard_SupplierByCode(supplierCode);
            entitySupplier.SetCurrentTable("Supplier");
            entitySupplier.CurrentTable.Rows[0]["Status"] = 1;

            DAL.EntityDAO.ProjectDAO.UpdateStandard_Supplier(entitySupplier);
            Response.Write("<script>window.location=window.location;</script>");
           
        }

        //protected void dgDocumentList_ItemDataBound1(object sender, DataGridItemEventArgs e)
        //{
        //    if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        //    {

        //        System.Web.UI.HtmlControls.HtmlGenericControl divMainHtml = (System.Web.UI.HtmlControls.HtmlGenericControl)e.Item.FindControl("divMainText");
        //        divMainHtml.InnerHtml = HttpUtility.HtmlDecode(divMainHtml.InnerText);
        //    }
        //}
        protected void DataGrid_supplierRecord_DeleteCommand(object source, DataGridCommandEventArgs e)
        {
            EntityData entity = null;
            try
            {
                entity = new EntityData("SupplierOpinion");
               
                string supplierOpinionCode = DataGrid_supplierRecord.DataKeys[e.Item.ItemIndex].ToString();
                entity=ProjectDAO.GetSupplierOpinionByCode(supplierOpinionCode);
                DAL.EntityDAO.ProjectDAO.DeleteSupplierOpinion(entity);
                FreshData();
            }

            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
            }
					
					
        }

        private void FreshData()
        {
            string supplierCode = Request["SupplierCode"] + "";
            Response.Write(Rms.Web.JavaScript.ScriptStart);
            Response.Write("window.navigate('SupplierInfo.aspx?Page=1&SupplierCode=" + supplierCode + "');");
            Response.Write(Rms.Web.JavaScript.ScriptEnd);
        }
        protected void DtLinkmanList_DeleteCommand(object source, DataGridCommandEventArgs e)
        {
            try
            {
                RmsPM.BLL.SupplierLinkman csupplierLinkman = new RmsPM.BLL.SupplierLinkman();
                csupplierLinkman.SupplierLinkmanCode = DtLinkmanList.DataKeys[e.Item.ItemIndex].ToString();
                csupplierLinkman.SupplierLinkmanDelete();
                FreshData();
            }

            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
            }
        }
}
}
