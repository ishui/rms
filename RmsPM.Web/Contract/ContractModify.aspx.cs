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
    /// ContractModify ��ժҪ˵����
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
            // ʱ������ ӡ��˰
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
                        this.lblTextAreaHint0.Text = "�������ƣ�300��";
                        this.lblTextAreaHint1.Text = "�������ƣ�300��";
                        this.lblTextAreaHint2.Text = "�������ƣ�300��";
                        this.lblTextAreaHint3.Text = "�������ƣ�300��";
                        this.lblTextAreaHint4.Text = "�������ƣ�300��";

                        this.lblMarkSegmentTitle.Text = "�ڣ����Σ�";
                        this.lblGroupNameTitle.Text = "����";

                        break;

                    case "nonggongshangpm":
                        this.divPerformingCircs.Visible = true;
                        this.sltPerformingCircs.Visible = true;
                        break;

                    case "yefengpm":
                        this.lblBaohanTitle.Text = "��Լ��֤��";

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

                BLL.PageFacade.LoadDictionarySelect(this.sltCreateMode, "��ͬ�γɷ�ʽ", "");
                BLL.PageFacade.LoadDictionarySelect(this.sltPerformingCircs, "��ͬ�������", "");

            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "��ʼ��ҳ�����");
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
								//ӡ��˰
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

                        if ((!user.HasRight("050120")) && (!user.HasRight("050121"))) //Լ����ֵ��ʾ��ʵ�ʲ�ֵ��ʾ
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
							//ӡ��˰
						}
						catch { }

                        dr["ContractPerson"] = user.UserCode;

                        dr["UnitCode"] = SetNewAddedUnit(projectCode);

                        dr["DevelopUnit"] = BLL.ProjectRule.GetDevelopUnitByProject(projectCode);

                        entity.AddNewRecord(dr);
                        this.lblTitle.Text = "��ͬ����";

                    }
                    else if (action == "Edit")
                    {
                        ArrayList ar = user.GetResourceRight(contractCode, "Contract");
                        if (!ar.Contains("050103"))
                        {
                            Response.Redirect("../RejectAccess.aspx");
                            Response.End();
                        }

                        if ((!ar.Contains("050120")) && (!ar.Contains("050121"))) //Լ����ֵ��ʾ��ʵ�ʲ�ֵ��ʾ
                        {
                            //this.divContractProduction.Visible = false;
                        }

                        entity = DAL.EntityDAO.ContractDAO.GetStandard_ContractByCode(contractCode);
                        this.lblTitle.Text = "��ͬ�޸�";
                    }
                    else if (action == "Bidding")
                    {
                        if ((!user.HasRight("050120")) && (!user.HasRight("050121"))) //Լ����ֵ��ʾ��ʵ�ʲ�ֵ��ʾ
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
							//ӡ��˰
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

                        this.lblTitle.Text = "��ͬ����";

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


                // ������Ϣ
                entity.SetCurrentTable("Contract");
                this.txtRemark.Value = entity.GetString("Remark");

                //��ͬ���---ũ�������⴦��                
                this.txtContractID.Value = entity.GetString("ContractID");
                if ((action == "Add" || action == "Bidding") && this.up_sPMName.ToLower() == "nonggongshangpm")
                {
                    this.txtContractID.Value = "��ͬ����Զ�����";
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

                // ����Ǻ�ͬ���,�Ҵ򿪿���
                bool isShow = IsOldSumMoney();
                if (action == "Change" && isShow)
                {
                    // ��ͬ�����ʾ
                    this.lblOldMoney.Attributes.Add("class", "form-item");
                    this.lblOldMoney.InnerText = "ԭʼ���";
                    this.txtOldMoney.Visible = true;
                    this.txtOldMoney.Value = entity.CurrentRow.IsNull("oldSumMoney") ? "" : entity.GetDecimal("oldSumMoney").ToString();

                    if (base.user.HasOperationRight("050109")) // ԭʼ��ͬ�ܼ۲���
                        this.txtOldMoney.Enabled = true;
                    else
                        this.txtOldMoney.Enabled = false;
                }

                // ������Ǳ��
                int status = entity.GetInt("Status");
                if (status != 4)
                {
                    this.trChange1.Visible = false;
                    this.trChange2.Visible = false;
                }

                //¼���ͬ��ֵ
                //if (this.divContractProduction.Visible)
                //{
                //    entity.SetCurrentTable("ContractProduction");

                //    //Լ��
                //    BindValueList(entity.CurrentTable, 0);
                //    //ʵ��
                //    BindValueList(entity.CurrentTable, 1);
                //}

                //��ع���
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

                // ��ͬ������ϸ
                BindCostList();

                entity.Dispose();
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "���غ�ͬ���ݴ���");
                Response.Write(Rms.Web.JavaScript.Alert(true, "���غ�ͬ���ݳ���" + ex.Message));
            }

        }



        /// <summary>
        /// ��ȡ��ͬ������ϸ
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

        // ȡ����Ŀ�趨: �Ƿ���Բ鿴ԭ��ͬ�۸�
        private bool IsOldSumMoney()
        {
            bool isSystemProportion = true;// ȡ��ϵͳ�趨

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


            //���ֻ��ʱ�
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

            //������ϸ��
            DataRow dr = entity.GetNewRecord("ContractCost");
            string costCode = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("ContractCostCode");

            dr["ContractCostCode"] = costCode;
            dr["ContractCode"] = contractCode;

            if (entity.Tables["ContractCost"].Columns.Contains("PayConditionHtml"))
            {
                //��������
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

            //����ƻ���
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
                    //Լ����ֵ
                    dr["IsFact"] = 0;
                }
                else
                {
                    //ʵ�ʲ�ֵ
                    dr["IsFact"] = 1;
                }

                entity.AddNewRecord(dr, tableName);
            }
        }

        #region Web ������������ɵĴ���
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: �õ����� ASP.NET Web ���������������ġ�
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
        /// �˷��������ݡ�
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
        /// ��ʾ��ͬ��ֵ��Լ����ʵ�ʣ�
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
                    Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾԼ����ֵ����" + ex.Message));
                }
                else
                {
                    Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾʵ�ʲ�ֵ����" + ex.Message));
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

                //������ϸ
                //foreach (RepeaterItem ud_Item in this.rptCostList.Items)
                //{
                //    alertMsg = ((UserControls.ContractCostModify)ud_Item.FindControl("ucCostModify")).SaveToSession();

                //    if (alertMsg != "")
                //    {
                //        break;
                //    }
                //}

                //��ֵ
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
                Response.Write(Rms.Web.JavaScript.Alert(true, "����Session����" + ex.Message));
                throw ex;
            }


        }

        /// <summary>
        /// ����Լ����ֵ
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
                Response.Write(Rms.Web.JavaScript.Alert(true, "����Լ����ֵ����" + ex.Message));
            }
        }

        /// <summary>
        /// ����ʵ�ʲ�ֵ
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
                Response.Write(Rms.Web.JavaScript.Alert(true, "����ʵ�ʲ�ֵ����" + ex.Message));
            }
        }

        /// <summary>
        /// ������ͬ������ϸ
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
                Response.Write(Rms.Web.JavaScript.Alert(true, "������ͬ������ϸ����" + ex.Message));
            }
        }

        /// <summary>
        /// ����ռ�¼
        /// </summary>
        private string ClearData()
        {
            try
            {
                string ErrMsg = "";
                string contractCode = this.txtContractCode.Value;
                EntityData entity = ReadEntitySession();

                // �����ֵΪ�յļ�¼
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
                            ErrMsg = "�뽫Լ����ֵ��д������";
                        }
                        else
                        {
                            ErrMsg = "�뽫ʵ�ʲ�ֵ��д������";
                        }
                        break; ;
                    }
                }

                if (ErrMsg == "")
                {

                    // �������Ϊ�յļ�¼
                    foreach (DataRow dr in entity.Tables["ContractCost"].Select("", "", DataViewRowState.CurrentRows))
                    {
                        string contractCostCode = dr["ContractCostCode"].ToString();

                        string ud_sRowFilter = String.Format("ContractCostCode={0}", contractCostCode);

                        //����յı��ֻ���
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
                                    ErrMsg = "����д���ֻ��ʣ�";
                                    return ErrMsg;
                                }
                            }
                        }

                        //����յĸ���ƻ�
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
                                ErrMsg = "�뽫����ƻ���д������";
                                return ErrMsg;
                            }
                        }

                        //����յĺ�ͬ������ϸ
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
                                    ErrMsg = "����������һ����ͬ������ϸ��";
                                    return ErrMsg;
                                }
                            }
                        }
                        else
                        {
                            dr["CostCode"] = "101090";
                            if (BLL.ConvertRule.ToString(dr["CostCode"]) == "")
                            {
                                ErrMsg = "�뽫��ͬ������ϸ��д����3��";//3
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
                Response.Write(Rms.Web.JavaScript.Alert(true, "����ռ�¼����" + ex.Message));
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


                //���ݼ��
                string contractName = this.txtContractName.Value.Trim();
                string type = this.inputSystemGroup.Value;
                if (contractName == "")
                {
                    msg += "����д��ͬ���� ��";
                }

                // ��ͬ���ͱ�����д������ʱ���ټ���Ƿ��ܲ��������ͬ���޸ľͲ��ü���ˣ�
                if (type == "")
                {
                    msg += "����д��ͬ���� ��";
                }
                else
                {
                    if (action == "Add" || action == "Bidding")
                    {
                        if (!user.HasTypeOperationRight("050102", type))
                            msg += "�����ܲ��������ͬ ��";
                    }
                }
                this.txtSupplierCode.Value = "100007";
                if (this.txtSupplierCode.Value == "")
                {
                    msg += "����д��Ӧ�� ��";
                }

                if (this.ucUnit.Value == "")
                {
                    msg += "����д��λ ��";
                }

                if (!BLL.ContractRule.IsContractIDUnique(this.txtContractID.Value.Trim(), contractCode))
                {
                    msg += "�Ѵ�����ͬ�ĺ�ͬ��� ��";
                }

                //if ( this.sltCreateMode.Value == "" )
                //{
                //    msg+="��ѡ�����ɷ�ʽ ��";
                //}

                if (this.txtContractArea.Value.Length > 300)
                {
                    msg += "�а���Χ��������300�֣�";
                }

                if (this.txtContractObject.Value.Length > 300)
                {
                    msg += "��ͬ������������300�֣�";
                }

                if (this.txtPayMode.Value.Length > 300)
                {
                    msg += "���ʽ��������300�֣�";
                }

                if (this.txtQualityRequire.Value.Length > 300)
                {
                    msg += "����Ҫ�󼰱���Լ����������300�֣�";
                }

                if (this.txtRemark.Value.Length > 300)
                {
                    msg += "��ע��������300�֣�";
                }

                if (this.txtRemark.Value.Length > 300)
                {
                    msg += "��ע��������300�֣�";
                }

                if (msg != "")
                    return msg;

                // �����ͬ���Ĺ�Ӧ�̣��Զ�ͬ������Ӧ��
                if (action == "Change" && this.txtSupplierCode.Value != this.oldSupplier.Value)
                {
                    // ȡ�õ�ǰ�������
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

                //��ͬ���---ũ�������⴦��  
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
				//ӡ��˰

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
        /// ����
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

                    //����ԭʼ���


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
                    //�����ﱣ���˺�ͬ��Ϣ
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

                // ���渽��
                this.myAttachMentAdd.SaveAttachMent(contractCode);

                ClearSession();
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
                Response.Write(Rms.Web.JavaScript.Alert(true, "�������" + ex.Message));
                return;
            }

            GoBack();
        }

        /// <summary>
        /// ����
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
        /// ɾ��Լ����ֵ
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
                Response.Write(Rms.Web.JavaScript.Alert(true, "ɾ��Լ����ֵ����" + ex.Message));
            }
        }

        /// <summary>
        /// ɾ��ʵ�ʲ�ֵ
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
                Response.Write(Rms.Web.JavaScript.Alert(true, "ɾ��Լ����ֵ����" + ex.Message));
            }
        }

        /// <summary>
        /// �޸ĸ��������󷵻أ�ˢ����ϸ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPayConditionReturn_ServerClick(object sender, System.EventArgs e)
        {
            try
            {
                string msg = SaveToSession();
                EntityData entity = ReadEntitySession();

                //���¸���ʱ��
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
                Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ������ϸ����" + ex.Message));
            }
        }

        /// <summary>
        /// ɾ����ع���
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

                // ���ù����Ƿ��ѳ�Ϊ��ͬ��������
                string WBSCode = e.Item.Cells[0].Text;
                DataTable dtCondition = entity.Tables["ContractPayCondition"];
                if (dtCondition.Select("WBSCode='" + WBSCode + "'").Length > 0)
                {
                    Response.Write(Rms.Web.JavaScript.Alert(true, "�ù����ѳ�Ϊ��ͬ��������������ɾ�� ��"));
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
                Response.Write(Rms.Web.JavaScript.Alert(true, "ɾ������" + ex.Message));
            }
        }

        /// <summary>
        /// ��ӹ������
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
                Response.Write(Rms.Web.JavaScript.Alert(true, "��ӹ��������" + ex.Message));
            }
        }

        private void dgValueList_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                //��ʾ�ϼ�Լ����ֵ
                ((Label)e.Item.FindControl("lblSumValue")).Text = BLL.MathRule.GetDecimalShowString(ViewState["SumValue"]);
            }
        }

        private void dgFactValueList_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                //��ʾ�ϼ�ʵ�ʲ�ֵ
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
            ////EntityData entity = DAL.EntityDAO.OBSDAO.GetUnitByUnitName("��Լ��", ud_sProjectCode);

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
        /// ũ����Ҫ���Զ����ɺ�ͬ��ţ�����20180828-1��20180828-2,20180829-1
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
                    ; //����Ƿ����֣����˹� 
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
                        iNextTemp = iNextTemp2; //����Ƿ����֣����˹� 
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
            //�õ���Ŀ�µĲ���
            EntityData UnitinProject = BLL.SystemRule.GetUnitUnderProject(projectCode);
            int StationInUnitCount;
            string UnitStrInProject;
            int UnitRowCount = UnitinProject.CurrentTable.Rows.Count;
            string SelectedUnit = "";
            int RoleUnitCount = 0;
            int SelectUnitCount = 0;
            //ȡ����ǰ�û���������
            DataTable dtUnit = RmsPM.BLL.SystemRule.GetUnitListByUserCode(this.user.UserCode);
            if (dtUnit != null)
            {
                RoleUnitCount = dtUnit.Rows.Count;
            }
            //�õ���Ŀ�µ�ǰ�û���Ӧ�Ĳ���
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
            //���������ڸ���Ŀ���ж�����ţ��Ͳ���
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
