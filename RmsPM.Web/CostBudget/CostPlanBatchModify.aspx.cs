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
using Rms.Web;

namespace RmsPM.Web.CostBudget
{
	/// <summary>
	/// CostPlanBatchModify 的摘要说明。
	/// </summary>
	public partial class CostPlanBatchModify : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlTable TableToolbar;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnUpdate;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
            if (!IsPostBack)
			{
//				IniPage();
//                LoadDataGrid();
            }
		}

		private void IniPage()
		{
			try 
			{
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];
                this.txtCostBudgetSetCode.Value = Request.QueryString["CostBudgetSetCode"];
                this.txtStartYm.Value = Request.QueryString["StartYm"];
                this.txtEndYm.Value = Request.QueryString["EndYm"];

				this.lblProjectName.Text = BLL.ProjectRule.GetProjectName(this.txtProjectCode.Value);

                this.txtMonthCount.Value = BLL.StringRule.GetMonthsBetween(this.txtStartYm.Value, this.txtEndYm.Value).ToString();
            }
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
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

            IniPage();
            LoadDataGrid();
        }
		
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            this.dgList.ItemCreated += new System.Web.UI.WebControls.RepeaterItemEventHandler(this.dgList_ItemCreated);
            this.dgList.ItemDataBound += new System.Web.UI.WebControls.RepeaterItemEventHandler(this.dgList_ItemDataBound);
        }
		#endregion

        /// <summary>
        /// 金额显示
        /// </summary>
        /// <param name="money"></param>
        /// <returns></returns>
        public string GetMoneyShowString(object money)
        {
            try
            {
                return BLL.CostBudgetPageRule.GetMoneyShowString(money, BLL.CostBudgetPageRule.m_MoneyUnit.fen);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 生成明细
        /// </summary>
        private void LoadDataGrid()
        {
            try
            {
                if (this.txtCostBudgetSetCode.Value == "")
                    return;

                BLL.CostPlan cp = new RmsPM.BLL.CostPlan(this.txtProjectCode.Value, this.txtCostBudgetSetCode.Value);
                cp.StartYm = this.txtStartYm.Value;
                cp.EndYm = this.txtEndYm.Value;
                cp.ReadOnly = false;

                cp.Generate();

//                Session["CostPlanBatchModify_tbPlan"] = cp.tbPlan;
                Session["CostPlanBatchModify_tbDtl"] = cp.tb;

                BindDataGrid(cp);
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
                Response.Write(Rms.Web.JavaScript.Alert(true, "显示付款计划表出错：" + ex.Message));
            }
        }

        /// <summary>
        /// 绑定动态费用明细
        /// </summary>
        private void BindDataGrid(BLL.CostPlan cp)
        {
            try
            {
                DataTable tbDtl = cp.tb;

                //年度展开
                ViewState["html_title1"] = cp.DateTitleHtml1;
                ViewState["html_title2"] = cp.DateTitleHtml2;

                //折叠全部费用项
                BLL.CostBudgetPageRule.CollapseAll(tbDtl);

                //展开所有
                BLL.CostBudgetPageRule.ExpandDeep(tbDtl, 99);

                this.dgList.DataSource = tbDtl;
                this.dgList.DataBind();
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
                Response.Write(Rms.Web.JavaScript.Alert(true, "绑定付款计划表出错：" + ex.Message));
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        private void Save()
        {
            try
            {
                string ProjectCode = this.txtProjectCode.Value;
                string CostBudgetSetCode = this.txtCostBudgetSetCode.Value;
                string StartYm = this.txtStartYm.Value;
                string EndYm = this.txtEndYm.Value;
                int MonthCount = BLL.StringRule.GetMonthsBetween(StartYm, EndYm);

                QueryAgent qa = new QueryAgent();
                try
                {
                    //合同付款计划
                    string sql = "select p.*"
                        + ", a.CostCode"
                        + ", convert(varchar(6), p.PlanningPayDate, 112) as PlanYm"
                        + " from ContractCost a"
                        + ", Contract b"
                        + ", ContractCostPlan p"
                        + " where a.ContractCode = b.ContractCode"
                        + " and a.ContractCostCode = p.ContractCostCode"
                        + " and b.ProjectCode = '" + ProjectCode + "'"
                        + " and a.CostBudgetSetCode = '" + CostBudgetSetCode + "'"
                        + " and convert(varchar(6), p.PlanningPayDate, 112) between '" + StartYm + "' and '" + EndYm + "'"
                        ;
                    EntityData entity = qa.FillEntityData("ContractCostPlan", sql);

                    //招标付款计划
                    sql = "select p.*"
                        + ", a.CostCode"
                        + ", convert(varchar(6), p.PlanningPayDate, 112) as PlanYm"
                        + " from BiddingDtl a"
                        + ", Bidding b"
                        + ", BiddingDtlPlan p"
                        + " where a.BiddingCode = b.BiddingCode"
                        + " and a.BiddingDtlCode = p.BiddingDtlCode"
                        + " and b.ProjectCode = '" + ProjectCode + "'"
                        + " and a.CostBudgetSetCode = '" + CostBudgetSetCode + "'"
                        + " and convert(varchar(6), p.PlanningPayDate, 112) between '" + StartYm + "' and '" + EndYm + "'"
                        ;
                    EntityData entityBidding = qa.FillEntityData("BiddingDtlPlan", sql);

                    //预留金额表头
                    DataRow drCostBudget = null;
                    string CostBudgetCode = "";
                    EntityData entityCostBudget = BLL.CostBudgetRule.GetValidCostBudget(CostBudgetSetCode, 0, false);
                    if (entityCostBudget.HasRecord())
                    {
                        CostBudgetCode = entityCostBudget.GetString("CostBudgetCode");
                        drCostBudget = entityCostBudget.CurrentRow;
                    }

                    //预留金额明细
                    EntityData entityCostBudgetDtl = DAL.EntityDAO.CostBudgetDAO.GetCostBudgetDtlByCostBudgetCode(CostBudgetCode);

                    //预留金额付款计划
                    sql = "select b.*"
                        + ", a.CostCode"
                        + ", right('0000' + cast(b.IYear as varchar), 4) + right('00' + cast(b.IMonth as varchar), 2) as PlanYm"
                        + " from CostBudgetDtl a"
                        + ", CostBudgetMonth b"
                        + " where a.CostBudgetDtlCode = b.CostBudgetDtlCode"
                        + " and a.ProjectCode = '" + ProjectCode + "'"
                        + " and a.CostBudgetCode = '" + CostBudgetCode + "'"
                        + " and right('0000' + cast(b.IYear as varchar), 4) + right('00' + cast(b.IMonth as varchar), 2) between '" + StartYm + "' and '" + EndYm + "'"
                        ;
                    EntityData entityCostBudgetMonth = qa.FillEntityData("CostBudgetMonth", sql);

                    foreach (RepeaterItem item in this.dgList.Items)
                    {
                        HtmlInputHidden txtContractCode = (HtmlInputHidden)item.FindControl("txtContractCode");
                        string ContractCode = txtContractCode.Value;

                        if (ContractCode != "")
                        {
                            HtmlInputHidden txtCostCode = (HtmlInputHidden)item.FindControl("txtCostCode");
                            string CostCode = txtCostCode.Value;
                            string RecordType = txtContractCode.Attributes["RecordType"]; 

                            //取该费用项的预留金额明细
                            DataRow drCostBudgetDtl = null;
                            DataRow[] drsCostBudgetDtl = entityCostBudgetDtl.CurrentTable.Select(string.Format("CostCode = '{0}'", CostCode), "", DataViewRowState.CurrentRows);
                            if (drsCostBudgetDtl.Length > 0)
                                drCostBudgetDtl = drsCostBudgetDtl[0];

                            //年月循环
                            for (int i = 0; i < MonthCount; i++)
                            {
                                string ym = BLL.StringRule.YmAddMonths(StartYm, i);

                                HtmlInputText txtPlanMoney = (HtmlInputText)item.FindControl("txtPlanMoney_" + ym);
                                decimal PlanMoney = BLL.ConvertRule.ToDecimal(txtPlanMoney.Value);

                                if (RecordType == "Bidding") //招标-------------------------------
                                {
                                    string BiddingCode = ContractCode.Substring("Bidding_".Length).Split("#"[0])[0];

                                    //取该招标该费用项该年月的计划
                                    DataRow[] drsPlan = entityBidding.CurrentTable.Select(string.Format("BiddingCode = '{0}' and CostCode = '{1}' and PlanYm = '{2}'", BiddingCode, CostCode, ym), "", DataViewRowState.CurrentRows);
                                    if (PlanMoney == 0)
                                    {
                                        //计划金额为0时，删除计划
                                        for (int j = drsPlan.Length - 1; j >= 0; j--)
                                        {
                                            drsPlan[j].Delete();
                                        }
                                    }
                                    else
                                    {
                                        if (drsPlan.Length == 0)
                                        {
                                            //无计划时，新增计划

                                            //取BiddingDtl中的Code
                                            sql = "select top 1 BiddingDtlCode from BiddingDtl"
                                                + " where BiddingCode = '" + BiddingCode + "'"
                                                + " and CostBudgetSetCode = '" + CostBudgetSetCode + "'"
                                                + " and CostCode = '" + CostCode + "'"
                                                ;
                                            string BiddingDtlCode = BLL.ConvertRule.ToString(qa.ExecuteScalar(sql));

                                            if (BiddingDtlCode != "")
                                            {
                                                DataRow drPlan = entityBidding.CurrentTable.NewRow();

                                                drPlan["BiddingDtlPlanCode"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("BiddingDtlPlanCode");
                                                drPlan["BiddingCode"] = BiddingCode;
                                                drPlan["BiddingDtlCode"] = BiddingDtlCode;
                                                drPlan["PlanningPayDate"] = BLL.ConvertRule.ToDate(ym.Substring(0, 4) + "-" + ym.Substring(4, 2) + "-01");
                                                drPlan["Money"] = PlanMoney;

                                                entityBidding.CurrentTable.Rows.Add(drPlan);
                                            }
                                        }
                                        else
                                        {
                                            //总金额改变时才修改
                                            decimal OldMoney = BLL.MathRule.SumColumn(drsPlan, "Money");
                                            if (PlanMoney != OldMoney)
                                            {

                                                //保留计划的第1条，第2条及以后记录删除
                                                if (drsPlan.Length > 1)
                                                {
                                                    for (int j = drsPlan.Length - 1; j >= 1; j--)
                                                    {
                                                        drsPlan[j].Delete();
                                                    }
                                                }

                                                //金额改在第1条计划上
                                                drsPlan[0]["Money"] = PlanMoney;
                                            }

                                        }
                                    }

                                }
                                else if (RecordType == "Balance") //预留金额-------------------------------
                                {
                                    //取该费用项该年月的计划
                                    DataRow[] drsPlan = entityCostBudgetMonth.CurrentTable.Select(string.Format("CostCode = '{0}' and PlanYm = '{1}'", CostCode, ym), "", DataViewRowState.CurrentRows);
                                    
                                    if (PlanMoney == 0)
                                    {
                                        //计划金额为0时，删除计划
                                        for (int j = drsPlan.Length - 1; j >= 0; j--)
                                        {
                                            drsPlan[j].Delete();
                                        }

                                        ////若明细的预留金额为0，删除明细
                                        //if ((drDtl != null) && (BLL.ConvertRule.ToDecimal(drDtl["BudgetMoney"]) == 0) && (BLL.ConvertRule.ToString(drDtl["Description"]) == ""))
                                        //    drDtl.Delete();
                                    }
                                    else
                                    {
                                        //新增预留金额表头
                                        if (drCostBudget == null)
                                        {
                                            drCostBudget = entityCostBudget.CurrentTable.NewRow();
                                            CostBudgetCode = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("CostBudgetCode");
                                            drCostBudget["CostBudgetCode"] = CostBudgetCode;
                                            drCostBudget["ProjectCode"] = ProjectCode;
                                            drCostBudget["CostBudgetSetCode"] = CostBudgetSetCode;
                                            drCostBudget["TargetFlag"] = 0;
                                            drCostBudget["Status"] = 1;
                                            drCostBudget["VerID"] = 0;
                                            drCostBudget["FirstCostBudgetCode"] = drCostBudget["CostBudgetCode"];
                                            drCostBudget["TotalBudgetMoney"] = 0;
                                            drCostBudget["ModifyDate"] = DateTime.Now;
                                            drCostBudget["ModifyPerson"] = user.UserCode;
                                            entityCostBudget.CurrentTable.Rows.Add(drCostBudget);
                                        }

                                        //新增预留金额明细
                                        if (drCostBudgetDtl == null)
                                        {
                                            drCostBudgetDtl = entityCostBudgetDtl.CurrentTable.NewRow();
                                            drCostBudgetDtl["CostBudgetDtlCode"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("CostBudgetDtlCode");
                                            drCostBudgetDtl["CostBudgetCode"] = CostBudgetCode;
                                            drCostBudgetDtl["ProjectCode"] = ProjectCode;
                                            drCostBudgetDtl["CostCode"] = CostCode;
                                            drCostBudgetDtl["BudgetMoney"] = 0;
                                            entityCostBudgetDtl.CurrentTable.Rows.Add(drCostBudgetDtl);
                                        }

                                        DataRow drPlan = null;
                                        if (drsPlan.Length == 0)
                                        {
                                            //无计划时，新增计划
                                            drPlan = entityCostBudgetMonth.CurrentTable.NewRow();

                                            drPlan["CostBudgetMonthCode"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("CostBudgetMonthCode");
                                            drPlan["CostBudgetDtlCode"] = drCostBudgetDtl["CostBudgetDtlCode"];
                                            drPlan["CostBudgetCode"] = CostBudgetCode;
                                            drPlan["ProjectCode"] = ProjectCode;
                                            drPlan["IYear"] = BLL.ConvertRule.ToInt(ym.Substring(0, 4));
                                            drPlan["IMonth"] = BLL.ConvertRule.ToInt(ym.Substring(4, 2));

                                            entityCostBudgetMonth.CurrentTable.Rows.Add(drPlan);
                                        }
                                        else
                                        {
                                            drPlan = drsPlan[0];
                                        }

                                        drPlan["BudgetMoney"] = PlanMoney;
                                    }

                                }
                                else //合同-------------------------------------------------------
                                {
                                    //取该合同该费用项该年月的计划
                                    DataRow[] drsPlan = entity.CurrentTable.Select(string.Format("ContractCode = '{0}' and CostCode = '{1}' and PlanYm = '{2}'", ContractCode, CostCode, ym), "", DataViewRowState.CurrentRows);
                                    if (PlanMoney == 0)
                                    {
                                        //计划金额为0时，删除计划
                                        for (int j = drsPlan.Length - 1; j >= 0; j--)
                                        {
                                            drsPlan[j].Delete();
                                        }
                                    }
                                    else
                                    {
                                        if (drsPlan.Length == 0)
                                        {
                                            //无计划时，新增计划

                                            //取ContractCost中的Code
                                            sql = "select top 1 ContractCostCode from ContractCost"
                                                + " where ContractCode = '" + ContractCode + "'"
                                                + " and CostBudgetSetCode = '" + CostBudgetSetCode + "'"
                                                + " and CostCode = '" + CostCode + "'"
                                                ;
                                            string ContractCostCode = BLL.ConvertRule.ToString(qa.ExecuteScalar(sql));

                                            if (ContractCostCode != "")
                                            {
                                                DataRow drPlan = entity.CurrentTable.NewRow();

                                                drPlan["ContractCostPlanCode"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("ContractCostPlanCode");
                                                drPlan["ContractCode"] = ContractCode;
                                                drPlan["ContractCostCode"] = ContractCostCode;
                                                drPlan["PlanningPayDate"] = BLL.ConvertRule.ToDate(ym.Substring(0, 4) + "-" + ym.Substring(4, 2) + "-01");
                                                drPlan["Money"] = PlanMoney;

                                                entity.CurrentTable.Rows.Add(drPlan);
                                            }
                                        }
                                        else
                                        {
                                            //总金额改变时才修改
                                            decimal OldMoney = BLL.MathRule.SumColumn(drsPlan, "Money");
                                            if (PlanMoney != OldMoney)
                                            {

                                                //保留计划的第1条，第2条及以后记录删除
                                                if (drsPlan.Length > 1)
                                                {
                                                    for (int j = drsPlan.Length - 1; j >= 1; j--)
                                                    {
                                                        drsPlan[j].Delete();
                                                    }
                                                }

                                                //金额改在第1条计划上
                                                drsPlan[0]["Money"] = PlanMoney;
                                            }

                                        }
                                    }
                                }
                            }
                        }
                    }

                    DAL.EntityDAO.ContractDAO.SubmitAllContractCostPlan(entity);
                    DAL.EntityDAO.BiddingDAO.SubmitAllBiddingDtlPlan(entityBidding);
                    DAL.EntityDAO.CostBudgetDAO.SubmitAllCostBudget(entityCostBudget);
                    DAL.EntityDAO.CostBudgetDAO.SubmitAllCostBudgetDtl(entityCostBudgetDtl);
                    DAL.EntityDAO.CostBudgetDAO.SubmitAllCostBudgetMonth(entityCostBudgetMonth);

                    entity.Dispose();
                    entityBidding.Dispose();
                    entityCostBudgetMonth.Dispose();
                    entityCostBudgetDtl.Dispose();
                    entityCostBudget.Dispose();
                }
                finally
                {
                    qa.Dispose();
                }
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
            try
            {
//                Response.Write(Rms.Web.JavaScript.Alert(true, this.dgList.Items[0].FindControl("txtPlanMoney_200609").ID));
//                return;

                string Hint = "";
                /*
                if (!CheckValid(ref Hint, tbDtl))
                {
                    Response.Write(Rms.Web.JavaScript.Alert(true, Hint));
                    return;
                }
                 */

                Save();
            }
            catch (Exception ex)
            {
                Response.Write(JavaScript.Alert(true, "保存失败：" + ex.Message));
                ApplicationLog.WriteLog(this.ToString(), ex, "");
                return;
            }

            GoBack();
        }

        /// <summary>
        /// 返回
        /// </summary>
        private void GoBack()
        {
            Response.Write(Rms.Web.JavaScript.ScriptStart);

            Response.Write("window.opener.Refresh();");
//            Response.Write("window.opener.location = window.opener.location;");
            Response.Write(Rms.Web.JavaScript.WinClose(false));
            Response.Write(Rms.Web.JavaScript.ScriptEnd);

        }

        private void dgList_ItemCreated(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                PlaceHolder phPlan = (PlaceHolder)e.Item.FindControl("phPlan");

                int MonthCount = BLL.ConvertRule.ToInt(this.txtMonthCount.Value);

                for (int i = 0; i < MonthCount; i++)
                {
                    string ym = BLL.StringRule.YmAddMonths(this.txtStartYm.Value, i);

                    //输入金额
                    HtmlInputText txt = new HtmlInputText();
                    txt.ID = "txtPlanMoney_" + ym;
                    txt.Attributes["class"] = "input-nember";
                    txt.Size = 16;
                    txt.Attributes["onblur"] = "MoneyBlur(this, true);";
                    txt.Attributes["onfocus"] = "MoneyFocus(this);";

                    //显示金额
                    HtmlGenericControl span = new HtmlGenericControl();
                    span.ID = "spanPlanMoney_" + ym;

                    //单元格
                    HtmlTableCell cell = new HtmlTableCell();
                    cell.Controls.Add(txt);
                    cell.Controls.Add(span);
                    cell.Align = "right";
                    cell.NoWrap = true;
//                    cell.ID = "YearData_" + ym;

                    phPlan.Controls.Add(cell);
                }
            }
        }

        private void dgList_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                DataTable tbDtl = (DataTable)Session["CostPlanBatchModify_tbDtl"];

                string ContractCode = ((HtmlInputHidden)e.Item.FindControl("txtContractCode")).Value;
                string DtlCode = ((HtmlInputHidden)e.Item.FindControl("txtDtlCode")).Value;

                string CostCode = ((HtmlInputHidden)e.Item.FindControl("txtCostCode")).Value;
                PlaceHolder phPlan = (PlaceHolder)e.Item.FindControl("phPlan");

                int MonthCount = BLL.ConvertRule.ToInt(this.txtMonthCount.Value);

                //明细记录
                DataRow[] drsDtl = tbDtl.Select("DtlCode = '" + DtlCode + "'");
                DataRow drDtl = null;
                if (drsDtl.Length > 0)
                {
                    drDtl = drsDtl[0];
                }

                for (int i = 0; i < MonthCount; i++)
                {
                    string ym = BLL.StringRule.YmAddMonths(this.txtStartYm.Value, i);

                    HtmlInputText txt = (HtmlInputText)e.Item.FindControl("txtPlanMoney_" + ym);
                    HtmlGenericControl span = (HtmlGenericControl)e.Item.FindControl("spanPlanMoney_" + ym);

                    if (ContractCode == "") //费用项
                    {
                        txt.Style["display"] = "none";
                    }
                    else //合同
                    {
                        span.Style["display"] = "none";
                    }

                    if (drDtl != null)
                    {
                        //计划金额
                        string sMoney = GetMoneyShowString(drDtl["PlanMoneyYm_" + ym]);

                        //费用项上不显示计划金额
                        if (ContractCode == "")
                            sMoney = "";

                        txt.Value = sMoney;
                        txt.Attributes["RowIndex"] = e.Item.ItemIndex.ToString();
                        txt.Attributes["ParentCode"] = BLL.ConvertRule.ToString(drDtl["ParentCode"]);

                        span.InnerText = sMoney;
                        span.Attributes["RowIndex"] = e.Item.ItemIndex.ToString();
                    }

                }

            }
        }

    }
}
