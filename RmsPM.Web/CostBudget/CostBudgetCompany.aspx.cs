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
using RmsPM.DAL.QueryStrategy;

namespace RmsPM.Web.CostBudget
{
	/// <summary>
	/// CostBudgetCompany 的摘要说明。
	/// </summary>
	public partial class CostBudgetCompany : PageBase
	{
        private string[] arrMoneyField = { "BudgetMoney", "BudgetChangeMoney", "ContractTotalMoney", "ContractMoney", "ContractChangeMoney", "ContractApplyMoney", "ContractAccountMoney", "ContractBudgetBalance", "ContractPay", "ContractPayReal", "ContractPayBalance", "ContractPayPercent", "BudgetPrice", "ContractOriginalPrice", "BuildingPrice" };

        protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				IniPage();
				LoadGrid();
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
            this.dgList.ItemDataBound += new DataGridItemEventHandler(dgList_ItemDataBound);
        }
		#endregion

		private void IniPage()
		{
			try 
			{
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

        private void CalcPercent(DataRow dr)
        {
            try
            {
                decimal ProjectArea = BLL.ConvertRule.ToDecimal(dr["ProjectArea"]);

                if (BLL.ConvertRule.ToDecimal(dr["ContractTotalMoney"]) == 0)
                {
                    dr["ContractPayPercent"] = DBNull.Value;
                }
                else
                {
                    dr["ContractPayPercent"] = BLL.MathRule.Round(BLL.ConvertRule.ToDecimal(dr["ContractPay"]) / BLL.ConvertRule.ToDecimal(dr["ContractTotalMoney"]) * 100, 0);
                }

                if (ProjectArea == 0)
                {
                    dr["BuildingPrice"] = DBNull.Value;
                }
                else
                {
                    dr["BuildingPrice"] = BLL.MathRule.Round(BLL.ConvertRule.ToDecimal(dr["ContractMoney"]) / ProjectArea, 2);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void LoadGrid()
		{
			try 
			{
                DataTable tbList = new DataTable();
                tbList.Columns.Add("ProjectCode");
                tbList.Columns.Add("ProjectName");
                tbList.Columns.Add("ProjectArea", typeof(decimal));
                tbList.Columns.Add("SortID", typeof(int));

                //金额字段
                foreach (string MoneyField in arrMoneyField)
                {
                    tbList.Columns.Add(MoneyField);
                }

                EntityData entityProject = DAL.EntityDAO.ProjectDAO.GetAllProject();
                foreach (DataRow drProject in entityProject.CurrentTable.Rows)
                {
                    string ProjectCode = drProject["ProjectCode"].ToString();
                    DataRow drList = tbList.NewRow();
                    drList["ProjectCode"] = drProject["ProjectCode"];
                    drList["ProjectName"] = drProject["ProjectName"];
                    drList["SortID"] = 0;
                    tbList.Rows.Add(drList);

                    //取项目面积
                    decimal ProjectArea = BLL.CostBudgetRule.GetCostBudgetProjectArea(ProjectCode);
                    drList["ProjectArea"] = ProjectArea;

                    //取预算表
                    CostBudgetStrategyBuilder sb = new CostBudgetStrategyBuilder();
                    sb.AddStrategy(new Strategy(CostBudgetStrategyName.ProjectCode, ProjectCode));

                    //仅预算（不要显示估算）
                    sb.AddStrategy(new Strategy(CostBudgetStrategyName.SetType, BLL.CostBudgetRule.m_BaseSetType));

                    string sql = sb.BuildQueryViewCostDynamicListString();

                    QueryAgent qa = new QueryAgent();
                    try
                    {
                        DataTable tbCostBudget = qa.ExecSqlForDataSet(sql).Tables[0];
                        foreach (DataRow drCostBudget in tbCostBudget.Rows)
                        {
                            string CostBudgetSetCode = drCostBudget["CostBudgetSetCode"].ToString();

                            BLL.CostBudgetDynamic dyn = new BLL.CostBudgetDynamic(ProjectCode, CostBudgetSetCode);

                            dyn.ShowApportion = false;
                            dyn.ShowContractBudget = false;
                            dyn.ShowTargetChange = false;

                            //只显示第1级费用项
                            dyn.MaxCBSDeep = 1;

                            dyn.AutoRefreshHtml = false;

                            try
                            {
                                dyn.Generate();
                            }
                            catch (Exception ex)
                            {
                                continue;
                            }

                            DataRow drR0 = BLL.CostBudgetDynamic.GetR0(dyn.tb);
                            if (drR0 != null)
                            {
                                foreach (string MoneyField in arrMoneyField)
                                {
                                    drList[MoneyField] = BLL.ConvertRule.ToDecimal(drList[MoneyField]) + BLL.ConvertRule.ToDecimal(drR0[MoneyField]);
                                }
                            }
                        }
                    }
                    finally
                    {
                        qa.Dispose();
                    }

                    CalcPercent(drList);
                }
                entityProject.Dispose();

                //合计
                decimal SumProjectArea = BLL.MathRule.SumColumn(tbList, "ProjectArea");
                decimal[] arrSum = BLL.MathRule.SumColumn(tbList, arrMoneyField);
                DataRow drSum = tbList.NewRow();
                drSum["ProjectName"] = " 　　合计";
                drSum["SortID"] = 99;
                drSum["ProjectArea"] = SumProjectArea;
                tbList.Rows.Add(drSum);
                int i = -1;
                foreach (string MoneyField in arrMoneyField)
                {
                    i++;
                    drSum[MoneyField] = arrSum[i];
                }
                CalcPercent(drSum);
//                ViewState["drSum"] = drSum;

//                ViewState["arrSum"] = arrSum;
                
                DataView dvList = new DataView(tbList, "", "SortID, ProjectName", DataViewRowState.CurrentRows);
                this.dgList.DataSource = dvList;
				this.dgList.DataBind();
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示报表出错：" + ex.Message));
			}
		}

        private void dgList_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            /*
            if (e.Item.ItemType == ListItemType.Footer)
            {
                //显示合计金额
                DataRow drSum = (DataRow)ViewState["arrSum"];

                foreach (String MoneyField in arrMoneyField)
                {
                    string lblName = "lblSum" + MoneyField;
                    object lbl = e.Item.FindControl(lblName);
                    if (lbl != null)
                    {
                        ((Label)lbl).Text = BLL.MathRule.GetDecimalShowString(drSum[MoneyField]);
                    }
                }
            }
            */
        }

    }
}
