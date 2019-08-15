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
	/// CostPlanList ��ժҪ˵����
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

                //ȱʡ��ʾ���µ�
                this.ucCostBudgetSelectMonth.MonthStart = DateTime.Today.ToString("yyyy-MM");
                this.ucCostBudgetSelectMonth.MonthEnd = DateTime.Today.AddMonths(1).ToString("yyyy-MM");
            }
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
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

		}
		#endregion

        /// <summary>
        /// �����ʾ
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
        /// ������ϸ
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
                Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ����ƻ������" + ex.Message));
            }
        }

        /// <summary>
        /// �󶨶�̬������ϸ
        /// </summary>
        private void BindDataGrid(BLL.CostPlan cp)
        {
            try
            {
                DataTable tbDtl = cp.tb;

                //���չ��
                ViewState["html_title1"] = cp.DateTitleHtml1;
                ViewState["html_title2"] = cp.DateTitleHtml2;

                //�۵�ȫ��������
                BLL.CostBudgetPageRule.CollapseAll(tbDtl);

                //ֻ��һ�����ڵ�ʱչ��
                BLL.CostBudgetPageRule.ExpandRoot(tbDtl);

                this.dgList.DataSource = tbDtl;
                this.dgList.DataBind();
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
                Response.Write(Rms.Web.JavaScript.Alert(true, "�󶨸���ƻ������" + ex.Message));
            }
        }

        /// <summary>
        /// ��ʾĳ����Χ
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
                Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ��ȳ���" + ex.Message));
            }
        }

    }
}
