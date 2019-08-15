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
using RmsPM.DAL;
using RmsPM.BLL;
using RmsPM.DAL.QueryStrategy;
using AspWebControl;
using Infragistics.WebUI.WebDataInput;
using System.Text;



namespace RmsPM.Web.Contract
{
    /// <summary>
    /// ContractModify 的摘要说明。
    /// </summary>
    public partial class ContractModify : PageBase
    {

        
		
		protected string totalMoney = "";



        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (ViewState["rptCostListCount"] == null)
            {
                ViewState["rptCostListCount"] = "0";
            }
            // 时代需求 印花税
            if (base.up_sPMNameLower != "shidaipm")
            {
                st.Visible = false;

            }
   
            if (!IsPostBack)
            {
				DropDownList1.Attributes.Add("onChange", "CalcStampDuty()");
                IniPage();
                LoadData();
            }
            else
            {
                EventBind();
            }

            this.myAttachMentAdd.AttachMentType = "ContractAttachMent";
            this.myAttachMentAdd.MasterCode = this.txtContractCode.Value;
        }

        private void IniPage()
        {
            try
            {
                string projectCode = Request["ProjectCode"] + "";
                this.inputSystemGroup.ClassCode = "0501";

                this.divPerformingCircs.Visible = false;
                this.sltPerformingCircs.Visible = false;
                this.trAdIssueDate.Visible = false;

                switch (this.up_sPMName.ToLower())
                {
                    case "shimaopm":
                        this.inputSystemGroup.SelectAllLeaf = false;
                        this.tdGroupNameHint.InnerHtml = "";
                        this.txtGroupName.Visible = false;
                        //this.divPaymentDefine.Visible = false;
                        break;

                    case "tianyangoa":
                        this.lblTextAreaHint0.Text = "字数限制：300字";
                        this.lblTextAreaHint1.Text = "字数限制：300字";
                        this.lblTextAreaHint2.Text = "字数限制：300字";
                        this.lblTextAreaHint3.Text = "字数限制：300字";
                        this.lblTextAreaHint4.Text = "字数限制：300字";

                        this.lblMarkSegmentTitle.Text = "期（或标段）";
                        this.lblGroupNameTitle.Text = "组团";

                        break;

                    case "nonggongshangpm":
                        this.divPerformingCircs.Visible = true;
                        this.sltPerformingCircs.Visible = true;
                        break;

                    case "yefengpm":
                        this.lblBaohanTitle.Text = "履约保证金";

                        this.divAdjustMoney.Visible = false;
                        this.divAdjustMoney2.Visible = false;
                        this.txtAdjustMoney.Visible = false;

                        //this.divPaymentDefine.Visible = false;

                        break;

                    case "gaokepm":
                        this.trAdIssueDate.Visible = true;
                        break;

                    default:
                        break;
                }

                BLL.PageFacade.LoadDictionarySelect(this.sltCreateMode, "合同形成方式", "");
                BLL.PageFacade.LoadDictionarySelect(this.sltPerformingCircs, "合同履行情况", "");

            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "初始化页面错误。");
            }
        }

        private void LoadData()
        {
            try
            {
                string contractCode = Request.QueryString["ContractCode"] + "";
                this.txtContractCode.Value = contractCode;
                string action = Request.QueryString["Act"] + "";
                string projectCode = Request["ProjectCode"] + "";
                EntityData entity = null;
                string BiddingContractCode = Request["BiddingContractCode"] + "";

                if (BiddingContractCode != "")
                {
                    entity = new EntityData("Standard_Contract");
                    RmsPM.BLL.TC_OA_BiddingContract cTC_OA_BiddingContract = new RmsPM.BLL.TC_OA_BiddingContract();
                    cTC_OA_BiddingContract.TC_OA_BiddingContractCode = BiddingContractCode;
                    DataTable dt = cTC_OA_BiddingContract.GetTC_OA_BiddingContracts();
                    if (dt != null)
                    {
                        foreach (DataRow dr in dt.Select())
                        {
                            DataRow drAdd = entity.GetNewRecord();
                            contractCode = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("ContractCode");
                            this.txtContractCode.Value = contractCode;
                            drAdd["ContractCode"] = contractCode;
                            drAdd["ProjectCode"] = dr["ProjectCode"].ToString();
                            drAdd["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd");
                            drAdd["CreatePerson"] = base.user.UserCode;
                            drAdd["DevelopUnit"] = BLL.ProjectRule.GetDevelopUnitByProject(dr["ProjectCode"].ToString());
                            drAdd["Status"] = 1;
                            drAdd["ChangeStatus"] = 0;
                            drAdd["ChangeCount"] = 0;

							try
							{
								drAdd["StampDuty"] = dr["StampDuty"].ToString();
								drAdd["StampDutyID"] = dr["StampDutyID"].ToString();
								//印花税
							}
							catch { }
			
                			drAdd["ContractPerson"] = user.UserCode;
                            drAdd["UnitCode"] = SetNewAddedUnit(projectCode);
                            //drAdd["BiddingCode"] = ud_sBiddingCode;

                            //drAdd["Type"] = ud_ContractDefaultValue.ContractType;
                            drAdd["ContractID"] = dr["ContractID"].ToString();
                            drAdd["ContractName"] = dr["WorkName"].ToString();
                            drAdd["SupplierCode"] = dr["SupplierCode"].ToString();
                            // drAdd["TotalMoney"] = ud_ContractDefaultValue.ContractMoney;


                            entity.AddNewRecord(drAdd);
                        }
                    }
                }
                else
                {
                    if (action == "Add")
                    {
                        if (!user.HasRight("050102"))
                        {
                            Response.Redirect("../RejectAccess.aspx");
                            Response.End();
                        }

                        if ((!user.HasRight("050120")) && (!user.HasRight("050121"))) //约定产值显示、实际产值显示
                        {
                            //this.divContractProduction.Visible = false;
                        }

                        entity = new EntityData("Standard_Contract");
                        DataRow dr = entity.GetNewRecord();

                        contractCode = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("ContractCode");
                        this.txtContractCode.Value = contractCode;
                        dr["ContractCode"] = contractCode;
                        dr["ProjectCode"] = projectCode;
                        dr["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd");
                        dr["CreatePerson"] = base.user.UserCode;
                        dr["Status"] = 1;
                        dr["ChangeStatus"] = 0;
                        dr["ChangeCount"] = 0;

						try
						{
							dr["StampDuty"] = 0;
							dr["StampDutyID"] = 0;
							//印花税
						}
						catch { }

                        dr["ContractPerson"] = user.UserCode;

                        dr["UnitCode"] = SetNewAddedUnit(projectCode);

                        dr["DevelopUnit"] = BLL.ProjectRule.GetDevelopUnitByProject(projectCode);

                        entity.AddNewRecord(dr);
                        this.lblTitle.Text = "合同申请";

                    }
                    else if (action == "Edit")
                    {
                        ArrayList ar = user.GetResourceRight(contractCode, "Contract");
                        if (!ar.Contains("050103"))
                        {
                            Response.Redirect("../RejectAccess.aspx");
                            Response.End();
                        }

                        if ((!ar.Contains("050120")) && (!ar.Contains("050121"))) //约定产值显示、实际产值显示
                        {
                            //this.divContractProduction.Visible = false;
                        }

                        entity = DAL.EntityDAO.ContractDAO.GetStandard_ContractByCode(contractCode);
                        this.lblTitle.Text = "合同修改";
                    }
                    else if (action == "Bidding")
                    {
                        if ((!user.HasRight("050120")) && (!user.HasRight("050121"))) //约定产值显示、实际产值显示
                        {
                            //this.divContractProduction.Visible = false;
                        }

                        string ud_sContractDefaultValueCode = Request["ContractDefaultValueCode"] + "";

                        this.txtContractID.Disabled = true;
                        this.txtBudgetMoney.Enabled = false;
                        this.txtAdjustMoney.Enabled = false;

                        BLL.ContractDefaultValue ud_ContractDefaultValue = BLL.BiddingManage.GetContractDefaultValue(Request["ContractDefaultValueCode"] + "");

                        string ud_sBiddingCode = ud_ContractDefaultValue.BiddingCode;

                        entity = new EntityData("Standard_Contract");
                        DataRow drAdd = entity.GetNewRecord();
                        contractCode = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("ContractCode");
                        this.txtContractCode.Value = contractCode;
                        drAdd["ContractCode"] = contractCode;
                        drAdd["ProjectCode"] = ud_ContractDefaultValue.ProjectCode;
                        drAdd["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd");
                        drAdd["CreatePerson"] = base.user.UserCode;
                        drAdd["DevelopUnit"] = BLL.ProjectRule.GetDevelopUnitByProject(ud_ContractDefaultValue.ProjectCode);
                        drAdd["Status"] = 1;
                        drAdd["ChangeStatus"] = 0;
                        drAdd["ChangeCount"] = 0;
                        drAdd["ContractPerson"] = user.UserCode;
                        drAdd["UnitCode"] = ud_ContractDefaultValue.UnitCode;
                        drAdd["BiddingCode"] = ud_sBiddingCode;
                        drAdd["ContractDefaultValueCode"] = ud_sContractDefaultValueCode;
                        drAdd["AdjustMoney"] = ud_ContractDefaultValue.ObligateMoney;
                        drAdd["Type"] = ud_ContractDefaultValue.ContractType;
                        drAdd["ContractID"] = ud_ContractDefaultValue.ContractNumber;
                        drAdd["ContractName"] = ud_ContractDefaultValue.ContractName;
                        drAdd["SupplierCode"] = ud_ContractDefaultValue.SupplierCode;
                        drAdd["TotalMoney"] = ud_ContractDefaultValue.ContractMoney;

						try
						{
							drAdd["StampDuty"] = Rms.Check.StringCheck.IsNumber(this.StampDuty.Value.ToString()) ? this.StampDuty.Value : 0;
							drAdd["StampDutyID"] = DropDownList1.SelectedValue;
							//印花税
						}
						catch { }
                        if (ud_ContractDefaultValue.Mostly)
                        {
                            drAdd["Mostly"] = 1;
                        }
                        else
                        {
                            drAdd["Mostly"] = 0;
                        }

                        entity.AddNewRecord(drAdd);

                        for (int i = 0; i < ud_ContractDefaultValue.BiddingMoneys.Length; i++)
                        {
                            string contractCostCode = AddNewEmptyCostRow(entity);
                            entity.SetCurrentTable("ContractCost");
                            foreach (DataRow drCost in entity.CurrentTable.Select(String.Format("ContractCostCode='{0}'", contractCostCode), "", DataViewRowState.CurrentRows))
                            {
                                decimal ud_deCostMoney = decimal.Zero;

                                drCost["CostCode"] = ud_ContractDefaultValue.BiddingMoneys[i].CostCode;
                                drCost["CostBudgetSetCode"] = ud_ContractDefaultValue.BiddingMoneys[i].CostBudgetSetCode;

                                for (int j = 0; j < ud_ContractDefaultValue.BiddingMoneys[i].CashMoneys.Length; j++)
                                {
                                    string ud_sCashCode = this.AddNewEmptyCostCashRow(entity, contractCostCode);

                                    foreach (DataRow drCash in entity.Tables["ContractCostCash"].Select(String.Format("ContractCostCashCode='{0}'", ud_sCashCode), "", DataViewRowState.CurrentRows))
                                    {
                                        decimal ud_deCash, ud_deExchangeRate, ud_deCashMoney;

                                        ud_deCash = decimal.Parse(ud_ContractDefaultValue.BiddingMoneys[i].CashMoneys[j].MoneyCash);
                                        ud_deExchangeRate = decimal.Parse(ud_ContractDefaultValue.BiddingMoneys[i].CashMoneys[j].ExchangeRate);

                                        ud_deCashMoney = ud_deCash * ud_deExchangeRate;
                                        ud_deCostMoney += ud_deCashMoney;

                                        drCash["Cash"] = ud_deCash;
                                        drCash["MoneyType"] = ud_ContractDefaultValue.BiddingMoneys[i].CashMoneys[j].MoneyType;
                                        drCash["ExchangeRate"] = ud_deExchangeRate;
                                        drCash["Money"] = ud_deCashMoney;
                                    }
                                }
                                drCost["Money"] = ud_deCostMoney;


                            }
                        }

                        this.lblTitle.Text = "合同申请";

                    }
                    else
                    {
                        Response.Write(Rms.Web.JavaScript.PageTo(true, "Contract.aspx?ProjectCode=" + projectCode));
                        Response.End();
                    }
                }

                if (entity.Tables["ContractCost"].Rows.Count == 0)
                {
                    AddNewEmptyCostRow(entity);
                }

                //				AddNewValueEmptyRow(entity,contractCode,"ContractProduction","ContractProductionCode",5,0);
                //				AddNewValueEmptyRow(entity,contractCode,"ContractProduction","ContractProductionCode",5,1);


                // 基本信息
                entity.SetCurrentTable("Contract");
                this.txtRemark.Value = entity.GetString("Remark");

                //合同编号---农工商特殊处理                
                this.txtContractID.Value = entity.GetString("ContractID");
                if ((action == "Add" || action == "Bidding") && this.up_sPMName.ToLower() == "nonggongshangpm")
                {
                    this.txtContractID.Value = "合同编号自动生成";
                }

                this.txtContractName.Value = entity.GetString("ContractName");

                this.txtContractPerson.Value = entity.GetString("ContractPerson");
                this.txtContractPersonName.Value = BLL.SystemRule.GetUserName(entity.GetString("ContractPerson"));

                this.inputSystemGroup.Value = entity.GetString("Type");

                this.DropDownList1.DataTextField = "StampDuty";
                this.DropDownList1.DataValueField = "StampDutyIDName";
                this.DropDownList1.DataSourceID = "StampDutyData";
                this.DropDownList1.DataBind();

                try
                {
                    DropDownList1.SelectedValue = entity.GetInt ("StampDutyID").ToString();
    				this.StampDuty.Value = entity.GetDecimalString ("StampDuty");
            }
                catch { }

                //DropDownList1.Attributes.Add();
//                DropDownList1.SelectedValue = entity.GetString("ContractPerson");


                //				this.txtTypeCode.Value=entity.GetString("Type");
                //				this.txtTypeName.Value=BLL.ContractRule.GetContractTypeName(entity.GetString("Type"));

                this.ucUnit.Value = entity.GetString("UnitCode");
                this.txtSupplierCode.Value = entity.GetString("SupplierCode");
                this.oldSupplier.Value = entity.GetString("SupplierCode");
                this.txtSupplierName.Value = BLL.ProjectRule.GetSupplierName(entity.GetString("SupplierCode"));

                this.txtSupplier2Code.Value = entity.GetString("Supplier2Code");
                this.txtSupplier2Name.Value = BLL.ProjectRule.GetSupplierName(entity.GetString("Supplier2Code"));

                this.txtChangeReason.Value = entity.GetString("ChangeReason");
                this.txtChangeRemark.Value = entity.GetString("ChangeRemark");
                this.txtContractObject.Value = entity.GetString("ContractObject");
                this.txtThirdParty.Value = entity.GetString("ThirdParty");

                this.hBeforeAccountTotalMoney.Value = entity.GetDecimalString("BeforeAccountTotalMoney");

                this.txtDevelopUnit.Value = entity.GetString("DevelopUnit");
                this.txtWorkTime.Value = entity.GetString("WorkTime");
                this.sltCreateMode.Value = entity.GetString("CreateMode");
                this.txtProjectName.Value = BLL.ProjectRule.GetProjectName(entity.GetString("ProjectCode"));
                this.txtMarkSegment.Value = entity.GetString("MarkSegment");
                this.txtGroupName.Value = entity.GetString("GroupName");
                this.txtBuilding.Value = entity.GetString("Building");
                this.txtPayMode.Value = entity.GetString("PayMode");
                this.txtQualityRequire.Value = entity.GetString("QualityRequire");
                this.sltPerformingCircs.Value = entity.GetString("PerformingCircs");

                this.txtBudgetMoney.ValueDecimal = entity.GetDecimal("BudgetMoney");
                this.txtAdjustMoney.ValueDecimal = entity.GetDecimal("AdjustMoney");

                this.txtContractArea.Value = entity.GetString("ContractArea");
                this.dtContractDate.Value = entity.GetDateTimeOnlyDate("ContractDate");

                this.txtBaohan.ValueDecimal = entity.GetDecimal("BaoHan");

                //this.txtPerCash1.Value = entity.GetDecimal("PerCash1");
                //this.txtPerCash2.Value = entity.GetDecimal("PerCash2");
                //this.txtPerCash3.Value = entity.GetDecimal("PerCash3");
                //this.dtWorkStartDate.Value = entity.GetDateTime("WorkStartDate");
                //this.dtWorkEndDate.Value = entity.GetDateTime("WorkEndDate");

                this.dtAdIssueDate.Value = entity.GetDateTimeOnlyDate("AdIssueDate");

                switch (entity.GetInt("Mostly"))
                {
                    case 0:
                        this.chkMostly.Checked = false;
                        break;
                    case 1:
                        this.chkMostly.Checked = true;
                        break;
                    default:
                        this.chkMostly.Checked = false;
                        break;
                }



                totalMoney = entity.GetDecimalString("TotalMoney");

                // 如果是合同变更,且打开开关
                bool isShow = IsOldSumMoney();
                if (action == "Change" && isShow)
                {
                    // 合同变更显示
                    this.lblOldMoney.Attributes.Add("class", "form-item");
                    this.lblOldMoney.InnerText = "原始金额";
                    this.txtOldMoney.Visible = true;
                    this.txtOldMoney.Value = entity.CurrentRow.IsNull("oldSumMoney") ? "" : entity.GetDecimal("oldSumMoney").ToString();

                    if (base.user.HasOperationRight("050109")) // 原始合同总价参照
                        this.txtOldMoney.Enabled = true;
                    else
                        this.txtOldMoney.Enabled = false;
                }

                // 如果不是变更
                int status = entity.GetInt("Status");
                if (status != 4)
                {
                    this.trChange1.Visible = false;
                    this.trChange2.Visible = false;
                }

                //录入合同产值
                //if (this.divContractProduction.Visible)
                //{
                //    entity.SetCurrentTable("ContractProduction");

                //    //约定
                //    BindValueList(entity.CurrentTable, 0);
                //    //实际
                //    BindValueList(entity.CurrentTable, 1);
                //}

                //相关工作
                entity.SetCurrentTable("TaskContract");
                entity.CurrentTable.Columns.Add("TaskName");
                entity.CurrentTable.Columns.Add("UserNames");
                entity.CurrentTable.Columns.Add("CompletePercent");
                int iCount = entity.CurrentTable.Rows.Count;
                for (int i = 0; i < iCount; i++)
                {
                    entity.SetCurrentRow(i);
                    string WBSCode = entity.GetString("WBSCode");

                    EntityData entityTask = DAL.EntityDAO.WBSDAO.GetTaskByCode(WBSCode);
                    if (entityTask.HasRecord())
                    {
                        entity.CurrentRow["TaskName"] = entityTask.CurrentRow["TaskName"];
                        entity.CurrentRow["CompletePercent"] = entityTask.CurrentRow["CompletePercent"];
                    }
                    entityTask.Dispose();

                    DataTable tbGroup = BLL.WBSRule.GetTaskPersonNameGroupByType(WBSCode);
                    entity.CurrentRow["UserNames"] = BLL.WBSRule.GetTaskPersonNameMaster(tbGroup);
                }
                //this.dgTaskList.DataSource = new DataView(entity.CurrentTable, "", "", DataViewRowState.CurrentRows);
                //this.dgTaskList.DataBind();

                WriteEntitySession(entity);

                // 合同款项明细
                BindCostList();

                entity.Dispose();
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "加载合同数据错误");
                Response.Write(Rms.Web.JavaScript.Alert(true, "加载合同数据出错：" + ex.Message));
            }

        }



        /// <summary>
        /// 读取合同款项明细
        /// </summary>
        private void BindCostList()
        {
            string contractCode = this.txtContractCode.Value.ToString();

            EntityData entity = ReadEntitySession();

            entity.SetCurrentTable("ContractCost");

            if (entity.CurrentTable.Rows.Count == 0)
            {
                AddNewEmptyCostRow(entity);
            }

   //         rptCostList.DataSource = entity.CurrentTable;
			//ViewState["vrptCostListCount"]=entity.CurrentTable.Rows.Count.ToString();

   //         rptCostList.DataBind();

        }

        // 取得项目设定: 是否可以查看原合同价格
        private bool IsOldSumMoney()
        {
            bool isSystemProportion = true;// 取得系统设定

            ProjectConfigStrategyBuilder sb = new ProjectConfigStrategyBuilder();
            sb.AddStrategy(new Strategy(ProjectConfigStrategyName.ProjectCode, base.project.ProjectCode));
            string sql = sb.BuildMainQueryString();
            QueryAgent qa = new QueryAgent();
            EntityData projectConfig = qa.FillEntityData("ProjectConfig", sql);
            qa.Dispose();

            DataRow[] drSelects = projectConfig.CurrentTable.Select(String.Format(" ConfigName='ContractOldMoney'"));
            if (drSelects.Length > 0)
            {
                if (BLL.ConvertRule.ToString(drSelects[0]["ConfigData"]) == "1")
                    isSystemProportion = false;
            }
            projectConfig.Dispose();

            return isSystemProportion;
        }

        private void AddNewEmptyCostRow(EntityData entity, int pm_iRows)
        {
            for (int i = 0; i < pm_iRows; i++)
            {
                AddNewEmptyCostRow(entity);
            }
        }

        private string AddNewEmptyCostCashRow(EntityData entity, string pm_sCostCode)
        {
            string contractCode = txtContractCode.Value;


            //币种汇率表
            string ud_sContractCostCashCode = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("ContractCostCashCode");
            DataRow dr1 = entity.GetNewRecord("ContractCostCash");
            dr1["ContractCostCashCode"] = ud_sContractCostCashCode;
            dr1["ContractCode"] = contractCode;
            dr1["ContractCostCode"] = pm_sCostCode;
            dr1["ExchangeRate"] = 1;
            entity.AddNewRecord(dr1, "ContractCostCash");

            return ud_sContractCostCashCode;
        }

        private string AddNewEmptyCostRow(EntityData entity)
        {
            string contractCode = txtContractCode.Value;
            string ud_sAction = Request["act"] + "";

            //款项明细表
            DataRow dr = entity.GetNewRecord("ContractCost");
            string costCode = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("ContractCostCode");

            dr["ContractCostCode"] = costCode;
            dr["ContractCode"] = contractCode;

            if (entity.Tables["ContractCost"].Columns.Contains("PayConditionHtml"))
            {
                //付款条件
                dr["PayConditionHtml"] = BLL.ContractRule.GetContractPayConditionHtml(dr["ContractCostCode"].ToString(), entity.Tables["ContractPayCondition"], true);
            }

            entity.AddNewRecord(dr, "ContractCost");

            switch (ud_sAction)
            {
                case "Bidding":
                    break;
                default:
                    this.AddNewEmptyCostCashRow(entity, costCode);
                    break;

            }

            //付款计划表
            for (int j = 0; j < 8; j++)
            {
                DataRow dr2 = entity.GetNewRecord("ContractCostPlan");
                dr2["ContractCostPlanCode"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("ContractCostPlanCode");
                dr2["ContractCode"] = contractCode;
                dr2["ContractCostCode"] = costCode;

                entity.AddNewRecord(dr2, "ContractCostPlan");
            }

            return costCode;
        }

        private void AddNewValueEmptyRow(EntityData entity, string contractCode, string tableName, string keyColumnName, int rows, int IsFact)
        {
            for (int i = 0; i < rows; i++)
            {
                DataRow dr = entity.GetNewRecord(tableName);
                dr[keyColumnName] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode(keyColumnName);
                dr["ContractCode"] = contractCode;

                if (IsFact == 0)
                {
                    //约定产值
                    dr["IsFact"] = 0;
                }
                else
                {
                    //实际产值
                    dr["IsFact"] = 1;
                }

                entity.AddNewRecord(dr, tableName);
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
            //this.rptCostList.ItemDataBound += new System.Web.UI.WebControls.RepeaterItemEventHandler(this.rptCostList_ItemDataBound);
            //this.dgValueList.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgValueList_DeleteCommand);
            //this.dgValueList.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgValueList_ItemDataBound);
            //this.dgFactValueList.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgFactValueList_DeleteCommand);
            //this.dgFactValueList.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgFactValueList_ItemDataBound);
            //this.dgTaskList.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgTaskList_DeleteCommand);

        }
        #endregion

        /// <summary>
        /// 显示合同产值（约定、实际）
        /// </summary>
        private void BindValueList(DataTable tb, int IsFact)
        {
            try
            {
                DataView dv = new DataView(tb, "", "", DataViewRowState.CurrentRows);

                if (IsFact == 0)
                {
                    dv.RowFilter = "IsFact=0";
                    ViewState["SumValue"] = BLL.MathRule.SumColumn(tb.Select("IsFact=0"), "ProductionValue");
                    //this.dgValueList.DataSource = dv;
                    //this.dgValueList.DataBind();
                }
                else
                {
                    dv.RowFilter = "IsFact=1";
                    ViewState["SumFactValue"] = BLL.MathRule.SumColumn(tb.Select("IsFact=1"), "ProductionValue");
                    //this.dgFactValueList.DataSource = dv;
                    //this.dgFactValueList.DataBind();
                }
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
                if (IsFact == 0)
                {
                    Response.Write(Rms.Web.JavaScript.Alert(true, "显示约定产值出错：" + ex.Message));
                }
                else
                {
                    Response.Write(Rms.Web.JavaScript.Alert(true, "显示实际产值出错：" + ex.Message));
                }
            }
        }

        private string SaveToSession()
        {
            string alertMsg = "";
            try
            {
                string contractCode = this.txtContractCode.Value;
                string projectCode = Request["ProjectCode"] + "";

                EntityData entity = ReadEntitySession();

                //款项明细
                //foreach (RepeaterItem ud_Item in this.rptCostList.Items)
                //{
                //    alertMsg = ((UserControls.ContractCostModify)ud_Item.FindControl("ucCostModify")).SaveToSession();

                //    if (alertMsg != "")
                //    {
                //        break;
                //    }
                //}

                //产值
                if (alertMsg == "")
                {
                    entity = ReadEntitySession();

                    //foreach (DataGridItem li in this.dgValueList.Items)
                    //{
                    //    string ContractProductionCode = li.Cells[0].Text;

                    //    string ProductionDate = ((AspWebControl.Calendar)li.FindControl("dtProductionDate")).Value;
                    //    WebNumericEdit txtValue = (WebNumericEdit)li.FindControl("txtValue");


                    //    foreach (DataRow dr in entity.Tables["ContractProduction"].Select(String.Format("ContractProductionCode='{0}'", ContractProductionCode)))
                    //    {
                    //        dr["ProductionValue"] = txtValue.ValueDecimal;

                    //        if (ProductionDate != "")
                    //            dr["ProductionDate"] = ProductionDate;
                    //        else
                    //            dr["ProductionDate"] = System.DBNull.Value;
                    //    }
                    //}

                    //foreach (DataGridItem li in this.dgFactValueList.Items)
                    //{
                    //    string ContractProductionCode = li.Cells[0].Text;

                    //    string FactProductionDate = ((AspWebControl.Calendar)li.FindControl("dtFactProductionDate")).Value;
                    //    WebNumericEdit txtFactValue = (WebNumericEdit)li.FindControl("txtFactValue");


                    //    foreach (DataRow dr in entity.Tables["ContractProduction"].Select(String.Format("ContractProductionCode='{0}'", ContractProductionCode)))
                    //    {
                    //        dr["ProductionValue"] = txtFactValue.ValueDecimal;

                    //        if (FactProductionDate != "")
                    //            dr["ProductionDate"] = FactProductionDate;
                    //        else
                    //            dr["ProductionDate"] = System.DBNull.Value;
                    //    }
                    //}

                    WriteEntitySession(entity);
                }

                entity.Dispose();

                return alertMsg;
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
                Response.Write(Rms.Web.JavaScript.Alert(true, "保存Session出错：" + ex.Message));
                throw ex;
            }


        }

        /// <summary>
        /// 新增约定产值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNewValueItem_ServerClick(object sender, System.EventArgs e)
        {
            try
            {
                string contractCode = this.txtContractCode.Value;
                string msg = SaveToSession();

                if (msg != "")
                {
                    Response.Write(Rms.Web.JavaScript.Alert(true, msg));
                }
                else
                {
                    EntityData entity = ReadEntitySession();
                    AddNewValueEmptyRow(entity, contractCode, "ContractProduction", "ContractProductionCode", 5, 0);

                    BindValueList(entity.Tables["ContractProduction"], 0);

                    WriteEntitySession(entity);
                    entity.Dispose();
                }
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
                Response.Write(Rms.Web.JavaScript.Alert(true, "新增约定产值出错：" + ex.Message));
            }
        }

        /// <summary>
        /// 新增实际产值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNewFactValueItem_ServerClick(object sender, System.EventArgs e)
        {
            try
            {
                string contractCode = this.txtContractCode.Value;
                string msg = SaveToSession();

                if (msg != "")
                {
                    Response.Write(Rms.Web.JavaScript.Alert(true, msg));
                }
                else
                {

                    EntityData entity = ReadEntitySession();
                    AddNewValueEmptyRow(entity, contractCode, "ContractProduction", "ContractProductionCode", 5, 1);

                    BindValueList(entity.Tables["ContractProduction"], 1);

                    WriteEntitySession(entity);
                    entity.Dispose();
                }

            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
                Response.Write(Rms.Web.JavaScript.Alert(true, "新增实际产值出错：" + ex.Message));
            }
        }

        /// <summary>
        /// 新增合同款项明细
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNewCostItem_ServerClick(object sender, System.EventArgs e)
        {

            try
            {
                string projectCode = Request["ProjectCode"] + "";
                string contractCode = this.txtContractCode.Value;

                string msg = SaveToSession();

                if (msg != "")
                {
                    Response.Write(Rms.Web.JavaScript.Alert(true, msg));
                }
                else
                {
                    EntityData entity = ReadEntitySession();
                    string costCode = AddNewEmptyCostRow(entity);

                    WriteEntitySession(entity);
                    entity.Dispose();

                    BindCostList();
                }

            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
                Response.Write(Rms.Web.JavaScript.Alert(true, "新增合同款项明细出错：" + ex.Message));
            }
        }

        /// <summary>
        /// 清除空记录
        /// </summary>
        private string ClearData()
        {
            try
            {
                string ErrMsg = "";
                string contractCode = this.txtContractCode.Value;
                EntityData entity = ReadEntitySession();

                // 清除产值为空的记录
                foreach (DataRow dr in entity.Tables["ContractProduction"].Select("", "", DataViewRowState.CurrentRows))
                {
                    if (BLL.ConvertRule.ToString(dr["ProductionValue"]) == "0" && BLL.ConvertRule.ToString(dr["ProductionDate"]) == "")
                    {
                        dr.Delete();
                        continue;
                    }

                    if (BLL.ConvertRule.ToString(dr["ProductionValue"]) == "0" || BLL.ConvertRule.ToString(dr["ProductionDate"]) == "")
                    {
                        if (BLL.ConvertRule.ToInt(dr["IsFact"]) == 0)
                        {
                            ErrMsg = "请将约定产值填写完整！";
                        }
                        else
                        {
                            ErrMsg = "请将实际产值填写完整！";
                        }
                        break; ;
                    }
                }

                if (ErrMsg == "")
                {

                    // 清除款项为空的记录
                    foreach (DataRow dr in entity.Tables["ContractCost"].Select("", "", DataViewRowState.CurrentRows))
                    {
                        string contractCostCode = dr["ContractCostCode"].ToString();

                        string ud_sRowFilter = String.Format("ContractCostCode={0}", contractCostCode);

                        //清除空的币种汇率
                        foreach (DataRow drCash in entity.Tables["ContractCostCash"].Select(ud_sRowFilter, "", DataViewRowState.CurrentRows))
                        {
                            if (drCash["Cash"] == DBNull.Value)
                            {
                                drCash.Delete();
                            }
                            else if ((decimal)drCash["Cash"] == Decimal.Zero)
                            {
                                if (entity.Tables["ContractCostCash"].Rows.Count > 1)
                                {
                                    drCash.Delete();
                                }

                            }
                            else
                            {
                                if (drCash["ExchangeRate"] == DBNull.Value || (decimal)drCash["ExchangeRate"] == Decimal.Zero)
                                {
                                    ErrMsg = "请填写币种汇率！";
                                    return ErrMsg;
                                }
                            }
                        }

                        //清除空的付款计划
                        foreach (DataRow drPlan in entity.Tables["ContractCostPlan"].Select(ud_sRowFilter, "", DataViewRowState.CurrentRows))
                        {
                            if (BLL.ConvertRule.ToString(drPlan["PlanningPayDate"]) == "" && BLL.ConvertRule.ToString(drPlan["Money"]) == "0")
                            {
                                drPlan.Delete();
                                continue;
                            }
                            drPlan["PlanningPayDate"] = "2019-4-8";
                            drPlan["Money"] = "100";
                            if (BLL.ConvertRule.ToString(drPlan["PlanningPayDate"]) == "" || BLL.ConvertRule.ToString(drPlan["Money"]) == "0")
                            {
                                ErrMsg = "请将付款计划填写完整！";
                                return ErrMsg;
                            }
                        }

                        //清除空的合同款项明细
                        if (entity.Tables["ContractCostPlan"].Select(ud_sRowFilter, "", DataViewRowState.CurrentRows).Length == 0 &&
                            entity.Tables["ContractCostCash"].Select(ud_sRowFilter, "", DataViewRowState.CurrentRows).Length == 0)
                        {
                            if (BLL.ConvertRule.ToString(dr["CostCode"]) == "")
                            {
                                if (entity.Tables["ContractCost"].Rows.Count > 1)
                                {
                                    dr.Delete();
                                    continue;
                                }
                                else
                                {
                                    ErrMsg = "请至少输入一条合同款项明细！";
                                    return ErrMsg;
                                }
                            }
                        }
                        else
                        {
                            dr["CostCode"] = "101090";
                            if (BLL.ConvertRule.ToString(dr["CostCode"]) == "")
                            {
                                ErrMsg = "请将合同款项明细填写完整3！";//3
                                return ErrMsg;
                            }
                        }
                    }
                }

                WriteEntitySession(entity);

                entity.Dispose();

                return ErrMsg;
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
                Response.Write(Rms.Web.JavaScript.Alert(true, "清除空记录出错：" + ex.Message));
                throw ex;
            }
        }

        private string SaveBaseInfo()
        {

            try
            {
                string action = Request.QueryString["Act"] + "";

                string contractCode = this.txtContractCode.Value;
                string projectCode = Request["ProjectCode"] + "";
                string msg = "";


                //数据检查
                string contractName = this.txtContractName.Value.Trim();
                string type = this.inputSystemGroup.Value;
                if (contractName == "")
                {
                    msg += "请填写合同名称 ！";
                }

                // 合同类型必须填写；新增时候再检查是否能操作该类合同，修改就不用检查了，
                if (type == "")
                {
                    msg += "请填写合同类型 ！";
                }
                else
                {
                    if (action == "Add" || action == "Bidding")
                    {
                        if (!user.HasTypeOperationRight("050102", type))
                            msg += "您不能操作这类合同 ！";
                    }
                }
                this.txtSupplierCode.Value = "100007";
                if (this.txtSupplierCode.Value == "")
                {
                    msg += "请填写供应商 ！";
                }

                if (this.ucUnit.Value == "")
                {
                    msg += "请填写单位 ！";
                }

                if (!BLL.ContractRule.IsContractIDUnique(this.txtContractID.Value.Trim(), contractCode))
                {
                    msg += "已存在相同的合同编号 ！";
                }

                //if ( this.sltCreateMode.Value == "" )
                //{
                //    msg+="请选择生成方式 ！";
                //}

                if (this.txtContractArea.Value.Length > 300)
                {
                    msg += "承包范围字数超过300字！";
                }

                if (this.txtContractObject.Value.Length > 300)
                {
                    msg += "合同概述字数超过300字！";
                }

                if (this.txtPayMode.Value.Length > 300)
                {
                    msg += "付款方式字数超过300字！";
                }

                if (this.txtQualityRequire.Value.Length > 300)
                {
                    msg += "质量要求及保修约定字数超过300字！";
                }

                if (this.txtRemark.Value.Length > 300)
                {
                    msg += "备注字数超过300字！";
                }

                if (this.txtRemark.Value.Length > 300)
                {
                    msg += "备注字数超过300字！";
                }

                if (msg != "")
                    return msg;

                // 如果合同更改供应商，自动同步请款单供应商
                if (action == "Change" && this.txtSupplierCode.Value != this.oldSupplier.Value)
                {
                    // 取得当前所有请款
                    EntityData pentity = DAL.EntityDAO.PaymentDAO.GetPaymentByContractCode(contractCode);
                    for (int i = 0; i < pentity.CurrentTable.Rows.Count; i++)
                    {
                        pentity.CurrentTable.Rows[i]["SupplyCode"] = this.txtSupplierCode.Value;
                        pentity.CurrentTable.Rows[i]["SupplyName"] = BLL.ProjectRule.GetSupplierName(this.txtSupplierCode.Value);
                    }
                    DAL.EntityDAO.PaymentDAO.UpdatePayment(pentity);
                    pentity.Dispose();
                }

                EntityData entity = ReadEntitySession();
                entity.SetCurrentTable("Contract");
                DataRow dr = entity.CurrentRow;

                dr["ContractName"] = contractName;

                //合同编号---农工商特殊处理  
                if ((action == "Add" || action == "Bidding") && this.up_sPMName.ToLower() == "nonggongshangpm")
                {
                    dr["ContractID"] = AutoRunContractID();
                }
                else
                {
                    dr["ContractID"] = this.txtContractID.Value;
                }

                dr["Type"] = type;
                dr["UnitCode"] = this.ucUnit.Value;
                dr["SupplierCode"] = this.txtSupplierCode.Value;
                dr["Supplier2Code"] = this.txtSupplier2Code.Value;
                dr["ContractPerson"] = this.txtContractPerson.Value;
                dr["ThirdParty"] = this.txtThirdParty.Value;

                dr["DevelopUnit"] = this.txtDevelopUnit.Value;
                dr["WorkTime"] = this.txtWorkTime.Value;
                dr["CreateMode"] = this.sltCreateMode.Value;
                dr["MarkSegment"] = this.txtMarkSegment.Value;
                dr["GroupName"] = this.txtGroupName.Value;
                dr["Building"] = this.txtBuilding.Value;
                dr["PayMode"] = this.txtPayMode.Value;
                dr["QualityRequire"] = this.txtQualityRequire.Value;

                dr["BudgetMoney"] = this.txtBudgetMoney.ValueDecimal;
                dr["AdjustMoney"] = this.txtAdjustMoney.ValueDecimal;
                dr["BaoHan"] = this.txtBaohan.ValueDecimal;

                dr["ContractArea"] = this.txtContractArea.Value;
                dr["PerformingCircs"] = this.sltPerformingCircs.Value;

				try
				{
					dr["StampDuty"] = Rms.Check.StringCheck.IsNumber(this.StampDuty.Value.ToString()) ? StampDuty.Value : 0;
					dr["StampDutyID"] = DropDownList1.SelectedValue;
				}
				catch { }
				//印花税

                //if (this.dtWorkStartDate.Value != "")
                //{
                //    dr["WorkStartDate"] = this.dtWorkStartDate.Value;
                //}

                //if (this.dtWorkEndDate.Value != "")
                //{
                //    dr["WorkEndDate"] = this.dtWorkEndDate.Value;
                //}

                //dr["PerCash1"] = this.txtPerCash1.ValueDecimal;
                //dr["PerCash2"] = this.txtPerCash2.ValueDecimal;
                //dr["PerCash3"] = this.txtPerCash3.ValueDecimal;

                if (this.dtContractDate.Value.Trim() != "")
                {
                    dr["ContractDate"] = this.dtContractDate.Value;
                }

                if (this.chkMostly.Checked == true)
                {
                    dr["Mostly"] = 1;
                }
                else
                {
                    dr["Mostly"] = 0;
                }

                dr["Remark"] = this.txtRemark.Value;
                dr["ContractObject"] = this.txtContractObject.Value;

                dr["AdIssueDate"] = BLL.ConvertRule.ToDate(this.dtAdIssueDate.Value);

                dr["LastModifyPerson"] = base.user.UserCode;
                dr["LastModifyDate"] = DateTime.Now.ToString("yyyy-MM-dd");

                decimal totalMoney = BLL.MathRule.SumColumn(entity.Tables["ContractCost"], "Money");

                dr["TotalMoney"] = totalMoney;

                WriteEntitySession(entity);
                entity.Dispose();
                return "";

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_ServerClick(object sender, System.EventArgs e)
        {
            string contractCode = this.txtContractCode.Value;
            string action = Request.QueryString["Act"] + "";
            string projectCode = Request["ProjectCode"] + "";
            try
            {
                string unitCode = "";

                string msg = SaveToSession();
                if (msg != "")
                {
                    Response.Write(Rms.Web.JavaScript.Alert(true, msg));
                    return;
                }

                string ClearMsg = ClearData();
                if (ClearMsg != "")
                {
                    Response.Write(Rms.Web.JavaScript.Alert(true, ClearMsg));
                    return;
                }

                string baseMsg = SaveBaseInfo();
                if (baseMsg != "")
                {
                    Response.Write(Rms.Web.JavaScript.Alert(true, baseMsg));
                    return;
                }

                //				SaveTaskContract();

                if (action == "Add" || action == "Edit" || action == "Bidding" || action == "OA")
                {
                    EntityData entity = ReadEntitySession();
                    entity.SetCurrentTable("Contract");
                    unitCode = entity.GetString("UnitCode");

                    string ud_sRowFilter = String.Format("ContractCode={0}", contractCode);

                    //更新原始金额


                    foreach (DataRow dr in entity.Tables["ContractCost"].Select(ud_sRowFilter, "", DataViewRowState.CurrentRows))
                    {
                        dr["OriginalMoney"] = dr["Money"];
                    }

                    foreach (DataRow dr in entity.Tables["ContractCostCash"].Select(ud_sRowFilter, "", DataViewRowState.CurrentRows))
                    {
                        dr["OriginalCash"] = dr["Cash"];
                    }
                    foreach (DataRow dr in entity.Tables["Contract"].Select(ud_sRowFilter, "", DataViewRowState.CurrentRows))
                    {
                        dr["OriginalMoney"] = dr["TotalMoney"];


                        dr["BudgetMoney"] = txtAdjustMoney.ValueDecimal + BLL.MathRule.SumColumn(entity.Tables["ContractCostCash"].Select(ud_sRowFilter, "", DataViewRowState.CurrentRows), "OriginalCash");
                        //                        dr["BudgetMoney"] = txtAdjustMoney.ValueDecimal + (decimal)dr["OriginalMoney"];

                    }
                    //在这里保存了合同信息
                    DAL.EntityDAO.ContractDAO.SubmitAllStandard_Contract(entity);
                    entity.Dispose();


                }
                string BiddingContractCode = Request["BiddingContractCode"] + "";

                if (BiddingContractCode != "")
                {
                    RmsPM.BLL.TC_OA_BiddingContract cBiddingContract = new RmsPM.BLL.TC_OA_BiddingContract();
                    cBiddingContract.TC_OA_BiddingContractCode = Request["BiddingContractCode"] + "";
                    cBiddingContract.ContractCode = contractCode;
                    cBiddingContract.TC_OA_BiddingContractAdd();
                }

                //BLL.ResourceRule.SetResourceAccessRange(contractCode,"0501",unitCode);

                // 保存附件
                this.myAttachMentAdd.SaveAttachMent(contractCode);

                ClearSession();
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
                Response.Write(Rms.Web.JavaScript.Alert(true, "保存出错：" + ex.Message));
                return;
            }

            GoBack();
        }

        /// <summary>
        /// 返回
        /// </summary>
        private void GoBack()
        {
            string contractCode = txtContractCode.Value;
            string projectCode = Request["ProjectCode"] + "";

            Response.Write(Rms.Web.JavaScript.ScriptStart);

            Response.Write("window.opener.location = window.opener.location;");
            Response.Write("window.location.href='../Contract/ContractInfo.aspx?ContractCode=" + contractCode + "&projectCode=" + projectCode + "';");
            //			Response.Write(Rms.Web.JavaScript.WinClose(false));
            Response.Write(Rms.Web.JavaScript.ScriptEnd);
        }

        /// <summary>
        /// 删除约定产值
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void dgValueList_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            try
            {
                string msg = SaveToSession();

                string contractCode = this.txtContractCode.Value;
                string action = Request.QueryString["Act"] + "";
                string projectCode = Request["ProjectCode"] + "";

                string codeTemp = e.Item.Cells[0].Text;

                EntityData entity = ReadEntitySession();
                foreach (DataRow dr in entity.Tables["ContractProduction"].Select(String.Format("ContractProductionCode='{0}'", codeTemp)))
                {
                    dr.Delete();
                }

                BindValueList(entity.Tables["ContractProduction"], 0);

                WriteEntitySession(entity);
                entity.Dispose();
                if (msg != "")
                {
                    Response.Write(Rms.Web.JavaScript.Alert(true, msg));
                }

            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
                Response.Write(Rms.Web.JavaScript.Alert(true, "删除约定产值出错：" + ex.Message));
            }
        }

        /// <summary>
        /// 删除实际产值
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void dgFactValueList_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            try
            {
                string msg = SaveToSession();

                string contractCode = this.txtContractCode.Value;
                string action = Request.QueryString["Act"] + "";
                string projectCode = Request["ProjectCode"] + "";

                string codeTemp = e.Item.Cells[0].Text;

                EntityData entity = ReadEntitySession();
                foreach (DataRow dr in entity.Tables["ContractProduction"].Select(String.Format("ContractProductionCode='{0}'", codeTemp)))
                {
                    dr.Delete();
                }

                BindValueList(entity.Tables["ContractProduction"], 1);

                WriteEntitySession(entity);
                entity.Dispose();
                if (msg != "")
                {
                    Response.Write(Rms.Web.JavaScript.Alert(true, msg));
                }

            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
                Response.Write(Rms.Web.JavaScript.Alert(true, "删除约定产值出错：" + ex.Message));
            }
        }

        /// <summary>
        /// 修改付款条件后返回，刷新明细
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPayConditionReturn_ServerClick(object sender, System.EventArgs e)
        {
            try
            {
                string msg = SaveToSession();
                EntityData entity = ReadEntitySession();

                //更新付款时间
                string AllocateCode = this.txtConditionAllocateCode.Value;
                string sPayDate = this.txtConditionPayDate.Value;
                if (sPayDate != "")
                {
                    DataRow[] drs = entity.Tables["ContractAllocation"].Select("AllocateCode='" + AllocateCode + "'");
                    if (drs.Length > 0)
                    {
                        drs[0]["PlanningPayDate"] = sPayDate;
                    }
                }


                //this.dgTaskList.DataSource = new DataView(entity.Tables["TaskContract"], "", "", DataViewRowState.CurrentRows);
                //this.dgTaskList.DataBind();

                WriteEntitySession(entity);
                entity.Dispose();

                if (msg != "")
                {
                    Response.Write(Rms.Web.JavaScript.Alert(true, msg));
                }
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
                Response.Write(Rms.Web.JavaScript.Alert(true, "显示款项明细出错：" + ex.Message));
            }
        }

        /// <summary>
        /// 删除相关工作
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void dgTaskList_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            try
            {
                string msg = SaveToSession();

                string contractCode = this.txtContractCode.Value;
                string action = Request.QueryString["Act"] + "";
                string projectCode = Request["ProjectCode"] + "";

                //string codeTemp = this.dgTaskList.DataKeys[e.Item.ItemIndex].ToString();

                EntityData entity = ReadEntitySession();

                // 检查该工作是否已成为合同付款条件
                string WBSCode = e.Item.Cells[0].Text;
                DataTable dtCondition = entity.Tables["ContractPayCondition"];
                if (dtCondition.Select("WBSCode='" + WBSCode + "'").Length > 0)
                {
                    Response.Write(Rms.Web.JavaScript.Alert(true, "该工作已成为合同付款条件，不能删除 ！"));
                    return;
                }

                //foreach (DataRow dr in entity.Tables["TaskContract"].Select(String.Format("TaskContractCode='{0}'", codeTemp)))
                //    dr.Delete();

                //this.dgTaskList.DataSource = new DataView(entity.Tables["TaskContract"], "", "", DataViewRowState.CurrentRows);
                //this.dgTaskList.DataBind();

                WriteEntitySession(entity);
                entity.Dispose();

                if (msg != "")
                {
                    Response.Write(Rms.Web.JavaScript.Alert(true, msg));
                }

            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
                Response.Write(Rms.Web.JavaScript.Alert(true, "删除出错：" + ex.Message));
            }
        }

        /// <summary>
        /// 添加工作项返回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddTaskReturn_ServerClick(object sender, System.EventArgs e)
        {
            try
            {
                string msg = SaveToSession();

                string contractCode = this.txtContractCode.Value;
                string action = Request.QueryString["Act"] + "";
                string projectCode = Request["ProjectCode"] + "";

                string WBSCode = this.txtAddTaskCode.Value;

                EntityData entity = ReadEntitySession();
                DataTable dtTask = entity.Tables["TaskContract"];

                if (dtTask.Select("WBSCode='" + WBSCode + "'").Length == 0)
                {
                    DataRow drNew = dtTask.NewRow();

                    drNew["TaskContractCode"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("TaskContractCode");
                    drNew["ContractCode"] = contractCode;
                    drNew["WBSCode"] = WBSCode;

                    EntityData entityTask = DAL.EntityDAO.WBSDAO.GetTaskByCode(WBSCode);
                    if (entityTask.HasRecord())
                    {
                        drNew["TaskName"] = entityTask.CurrentRow["TaskName"];
                        drNew["CompletePercent"] = entityTask.CurrentRow["CompletePercent"];
                    }
                    entityTask.Dispose();

                    DataTable tbGroup = BLL.WBSRule.GetTaskPersonNameGroupByType(WBSCode);
                    drNew["UserNames"] = BLL.WBSRule.GetTaskPersonNameMaster(tbGroup);

                    dtTask.Rows.Add(drNew);
                }

                //this.dgTaskList.DataSource = new DataView(dtTask, "", "", DataViewRowState.CurrentRows);
                //this.dgTaskList.DataBind();

                WriteEntitySession(entity);
                entity.Dispose();

                if (msg != "")
                {
                    Response.Write(Rms.Web.JavaScript.Alert(true, msg));
                }

            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
                Response.Write(Rms.Web.JavaScript.Alert(true, "添加工作项出错：" + ex.Message));
            }
        }

        private void dgValueList_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                //显示合计约定产值
                ((Label)e.Item.FindControl("lblSumValue")).Text = BLL.MathRule.GetDecimalShowString(ViewState["SumValue"]);
            }
        }

        private void dgFactValueList_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                //显示合计实际产值
                ((Label)e.Item.FindControl("lblSumFactValue")).Text = BLL.MathRule.GetDecimalShowString(ViewState["SumFactValue"]);
            }
        }

        private void rptCostList_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {
            switch (e.Item.ItemType)
            {
                case ListItemType.Item:
                case ListItemType.AlternatingItem:
                    UserControls.ContractCostModify ud_ucCostModify = (UserControls.ContractCostModify)e.Item.FindControl("ucCostModify");
                    ud_ucCostModify.ReadEntitySession += new UserControls.ContractCostModify.ReadEntitySessionEventHandler(this.ReadEntitySession);
                    ud_ucCostModify.WriteEntitySession += new UserControls.ContractCostModify.WriteEntitySessionEventHandler(this.WriteEntitySession);
                    ud_ucCostModify.SaveAllToSession += new RmsPM.Web.UserControls.ContractCostModify.NoParameterStringEventHandler(this.SaveToSession);
                    ud_ucCostModify.BindCostList += new UserControls.ContractCostModify.NoParameterEventHandler(this.BindCostList);
                    if (IsPostBack)
                    {
                        ud_ucCostModify.LoadData();
                    }
                    break;
            }
        }

        private void WriteEntitySession(EntityData entity)
        {
            string action = Request.QueryString["Act"] + "";

            Session["ContractEntity" + action] = entity;
        }

        private EntityData ReadEntitySession()
        {
            string action = Request.QueryString["Act"] + "";

            return (EntityData)Session["ContractEntity" + action];
        }

        private void ClearSession()
        {
            string action = Request.QueryString["Act"] + "";

            Session["ContractEntity" + action] = null;
        }

        private string GetUnitCodeByName()
        {

            string ud_sProjectCode = Request["ProjectCode"] + "";

            string[] ud_saStationCode = BLL.SystemRule.GetStationByUserCode(user.UserCode).Split(',');
            string ud_sUnitCode = BLL.SystemRule.GetUnitByStationCode(ud_saStationCode[0]);



            //EntityData entity = DAL.EntityDAO.OBSDAO.GetUnitByUnitName(ud_sUnitName, ud_sProjectCode);
            ////EntityData entity = DAL.EntityDAO.OBSDAO.GetUnitByUnitName("合约部", ud_sProjectCode);

            //if ( entity.HasRecord() )
            //{
            //    ud_sUnitCode = entity.GetString("UnitCode");
            //}

            return ud_sUnitCode;
        }

        private string GetUnitCodeByName(string name)
        {
            string UnitCode = "";
            EntityData entity = DAL.EntityDAO.OBSDAO.GetUnitByUnitName(name);

            if (entity.HasRecord())
            {
                UnitCode = entity.GetString("UnitCode");
            }

            return UnitCode;
        }

        private void EventBind()
        {
            //foreach (RepeaterItem ud_rptItem in rptCostList.Items)
            //{
            //    switch (ud_rptItem.ItemType)
            //    {
            //        case ListItemType.Item:
            //        case ListItemType.AlternatingItem:
            //            UserControls.ContractCostModify ud_ucCostModify = (UserControls.ContractCostModify)ud_rptItem.FindControl("ucCostModify");
            //            ud_ucCostModify.ReadEntitySession += new UserControls.ContractCostModify.ReadEntitySessionEventHandler(this.ReadEntitySession);
            //            ud_ucCostModify.WriteEntitySession += new UserControls.ContractCostModify.WriteEntitySessionEventHandler(this.WriteEntitySession);
            //            ud_ucCostModify.SaveAllToSession += new RmsPM.Web.UserControls.ContractCostModify.NoParameterStringEventHandler(this.SaveToSession);
            //            ud_ucCostModify.BindCostList += new UserControls.ContractCostModify.NoParameterEventHandler(this.BindCostList);
            //            break;
            //    }

            //}
        }

        protected void btnReload_ServerClick(object sender, System.EventArgs e)
        {
            this.BindCostList();
        }

        /// <summary>
        /// 农工商要求自动生成合同编号，规则20180828-1，20180828-2,20180829-1
        /// </summary>
        private string AutoRunContractID()
        {

            string strNextTemp = "";
            string strNextTemp2 = "";
            int iNextTemp = 0;
            int iNextTemp2 = 0;

            string strYear = System.DateTime.Now.Year.ToString();
            string strMonth = (System.DateTime.Now.Month.ToString().Length == 2) ? (System.DateTime.Now.Month.ToString()) : ("0" + System.DateTime.Now.Month.ToString());
            string strDay = (System.DateTime.Now.Day.ToString().Length == 2) ? (System.DateTime.Now.Day.ToString()) : ("0" + System.DateTime.Now.Day.ToString());
            string strToday = strYear + strMonth + strDay;

            EntityData entity = RmsPM.DAL.EntityDAO.ContractDAO.GetContractIDByToday(strToday);

            if (entity.HasRecord())
            {
                LogHelper.WriteLog("has record");
                DataRow dr = entity.CurrentRow;

                strNextTemp = dr["ContractID"].ToString().Substring(9, dr["ContractID"].ToString().Length - 9); ;
                LogHelper.WriteLog("strnexttemp=" + strNextTemp);
                try
                {
                    iNextTemp = Convert.ToInt32(strNextTemp);
                }
                catch
                {
                    ; //如果是非数字，则滤过 
                }

                for (int k = 1; k < entity.CurrentTable.Rows.Count; k++)
                {
                    entity.SetCurrentRow(k);
                    DataRow drNext = entity.CurrentRow;
                    strNextTemp2 = drNext["ContractID"].ToString().Substring(9, dr["ContractID"].ToString().Length - 9);
                    LogHelper.WriteLog("strnexttemp2=" + strNextTemp2); ;
                    try
                    {
                        iNextTemp2 = Convert.ToInt32(strNextTemp2);
                    }
                    catch
                    {
                        ;
                    }

                    if (iNextTemp <= iNextTemp2)
                    {
                        iNextTemp = iNextTemp2; //如果是非数字，则滤过 
                    }
                }
                strNextTemp = Convert.ToString(iNextTemp + 1);
                LogHelper.WriteLog(strNextTemp);
            }
            else
            {
                strNextTemp = "1";
                LogHelper.WriteLog("have no record");
            }
            // this.txtContractID.Value = strToday + "-" + strNextTemp;
            return strToday + "-" + strNextTemp;
        }

        private string SetNewAddedUnit(string projectCode)
        {
            //得到项目下的部门
            EntityData UnitinProject = BLL.SystemRule.GetUnitUnderProject(projectCode);
            int StationInUnitCount;
            string UnitStrInProject;
            int UnitRowCount = UnitinProject.CurrentTable.Rows.Count;
            string SelectedUnit = "";
            int RoleUnitCount = 0;
            int SelectUnitCount = 0;
            //取到当前用户所属部门
            DataTable dtUnit = RmsPM.BLL.SystemRule.GetUnitListByUserCode(this.user.UserCode);
            if (dtUnit != null)
            {
                RoleUnitCount = dtUnit.Rows.Count;
            }
            //得到项目下当前用户对应的部门
            for (int i = 0; i < UnitRowCount; i++)
            {
                UnitinProject.SetCurrentRow(i);
                UnitStrInProject = UnitinProject.GetString("UnitCode");
                for (int k = 0; k < RoleUnitCount; k++)
                {
                    if (UnitStrInProject == dtUnit.Rows[k]["unitcode"].ToString())
                    {
                        SelectUnitCount++;
                        SelectedUnit = UnitStrInProject;
                    }
                }
            }
            UnitinProject.Dispose();
            //如果这个人在该项目下有多个部门，就不带
            if (SelectUnitCount == 1)
            {
                return SelectedUnit;
            }
            else
            {
                return "";
            }
        }

    }
}
