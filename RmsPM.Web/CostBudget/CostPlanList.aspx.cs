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
	/// CostPlanList 的摘要说明。
	/// </summary>
	public partial class CostPlanList : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlTable TableToolbar;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnUpdate;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
            this.ucCostBudgetSelectMonth.GotoMonthClick += new System.EventHandler(this.btnGotoMonth_ServerClick);
            if (!IsPostBack)
			{
				IniPage();
                btnGotoMonth_ServerClick(sender, e);
            }
		}

		private void IniPage()
		{
			try 
			{
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];
                this.txtCostBudgetSetCode.Value = Request.QueryString["CostBudgetSetCode"];

				this.lblProjectName.Text = BLL.ProjectRule.GetProjectName(this.txtProjectCode.Value);

                //缺省显示两月的
                this.ucCostBudgetSelectMonth.MonthStart = DateTime.Today.ToString("yyyy-MM");
                this.ucCostBudgetSelectMonth.MonthEnd = DateTime.Today.AddMonths(1).ToString("yyyy-MM");
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
		}
		
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{    

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

        private void SetDateRange()
        {
            string StartYm = "";
            string EndYm = "";

            if (this.ucCostBudgetSelectMonth.ShowMonth)
            {
                StartYm = this.ucCostBudgetSelectMonth.MonthStart.Replace("-", "");
                EndYm = this.ucCostBudgetSelectMonth.MonthEnd.Replace("-", "");
            }

            this.txtStartYm.Value = StartYm;
            this.txtEndYm.Value = EndYm;
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

                cp.Generate();

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

                //只有一个根节点时展开
                BLL.CostBudgetPageRule.ExpandRoot(tbDtl);

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
        /// 显示某个范围
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGotoMonth_ServerClick(object sender, System.EventArgs e)
        {
            try
            {
                SetDateRange();
                LoadDataGrid();
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
                Response.Write(Rms.Web.JavaScript.Alert(true, "显示年度出错：" + ex.Message));
            }
        }

    }
}
