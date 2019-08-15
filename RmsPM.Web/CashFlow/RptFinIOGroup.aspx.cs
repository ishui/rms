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

namespace RmsPM.Web.CashFlow
{
	/// <summary>
	/// RptFinIOGroup 的摘要说明。
	/// </summary>
	public partial class RptFinIOGroup : PageBase
	{
        private DataSet ds;

        private DataTable tbProject
        {
            get { return ds.Tables["Project"]; }
        }

        private DataTable tbDtl
        {
            get { return ds.Tables["Dtl"]; }
        }

        private DataTable tbHtml
        {
            get { return ds.Tables["Html"]; }
        }

        private int BeginY;
        private int EndY;
        private string BeginYm;
        private string EndYm;

        private string MonthType;
        private string Source;
        private string ProjectCodes;

        protected void Page_Load(object sender, System.EventArgs e)
		{
			this.ucCostBudgetSelectMonth.GotoMonthClick += new System.EventHandler(this.btnGotoMonth_ServerClick);

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

		}
		#endregion

		private void IniPage()
		{
			try 
			{
				//年度
                this.ucCostBudgetSelectMonth.MonthStart = DateTime.Today.Year.ToString() + "-01";
                this.ucCostBudgetSelectMonth.MonthEnd = DateTime.Today.Year.ToString() + "-12";
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

		public static string FormatMoneyHtml(object objVal)
		{
			string r = "";

			try 
			{
				decimal d = BLL.ConvertRule.ToDecimal(objVal);
				if (d != 0)
				{
					r = d.ToString();
				}

//				r = BLL.MathRule.GetDecimalShowString(objVal);
			}
			catch
			{
			}

			if (r == "")
			{
				r = "&nbsp;";
			}

			return r;
		}

        private void CreateDataSet()
        {
            try
            {
                ds = new DataSet();

                //项目列表
                DataTable tbProject = new DataTable("Project");
                tbProject.Columns.Add("ProjectCode");
                tbProject.Columns.Add("ProjectName");
                ds.Tables.Add(tbProject);

                //明细表
                DataTable tbDtl = new DataTable("Dtl");
                tbDtl.Columns.Add("IOType");
                tbDtl.Columns.Add("ProjectCode");
                tbDtl.Columns.Add("Year", typeof(int));
                tbDtl.Columns.Add("Month", typeof(int));
                tbDtl.Columns.Add("Money");
                ds.Tables.Add(tbDtl);

                //输出到Html
                DataTable tbHtml = new DataTable("Html");
                tbHtml.Columns.Add("Type");
                tbHtml.Columns.Add("ProjectCode");
                tbHtml.Columns.Add("ProjectName");
                tbHtml.Columns.Add("MoneyHtmlI");
                tbHtml.Columns.Add("MoneyHtmlO");
                tbHtml.Columns.Add("MoneyHtml");
                ds.Tables.Add(tbHtml);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string GetProjectCodeInSql(string FieldName)
        {
            if (ProjectCodes != "")
                return string.Format(" and {1} in ({0})", "'" + ProjectCodes.Replace(",", "','") + "'", FieldName);
            else
                return " and 1 = 2";
        }

        private void GenerateIn()
        {
            try
            {
                QueryAgent qa = new QueryAgent();
                try
                {
                    string sql;
                    DataTable tbTemp = null;

                    switch (Source)
                    {
                        case "Plan": //计划
                            break;

                        case "Fact": //实际已批
                            sql = "select a.ProjectCode"
                                + ", Year(a.PayDate) as IYear"
                                + ", Month(a.PayDate) as IMonth"
                                + ", sum(isnull(a.PayMoney, 0)) as Money"
                                + " from SalPay a"
                                + " where a.PayDate is not null"
                                ;
                            if (BeginYm != "") sql += string.Format(" and a.PayDate >= convert(datetime, '{0}01', 112)", BeginYm);
                            if (EndYm != "") sql += string.Format(" and a.PayDate < dateadd(month, 1, convert(datetime, '{0}01', 112))", EndYm);

                            sql += GetProjectCodeInSql("a.ProjectCode");

                            sql += " group by a.ProjectCode, Year(a.PayDate), Month(a.PayDate)"
                                ;
                            tbTemp = qa.ExecSqlForDataSet(sql).Tables[0];
                            break;

                        case "FactPay": //实际已付
                            goto case "Fact";
                    }

                    if (tbTemp != null)
                    {
                        foreach (DataRow drTemp in tbTemp.Rows)
                        {
                            DataRow drDtl = tbDtl.NewRow();
                            drDtl["IOType"] = "I";
                            drDtl["ProjectCode"] = drTemp["ProjectCode"];
                            drDtl["Year"] = drTemp["IYear"];
                            drDtl["Month"] = drTemp["IMonth"];
                            drDtl["Money"] = drTemp["Money"];
                            tbDtl.Rows.Add(drDtl);
                        }
                    }
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

        private void GenerateOut()
        {
            try
            {
                QueryAgent qa = new QueryAgent();
                try
                {
                    string sql = "";
                    DataTable tbTemp = null;

                    switch (Source)
                    {
                        case "Plan": //计划
                            //合同付款计划
                            sql = "select a.ProjectCode"
                                + ", Year(c.PlanningPayDate) as IYear"
                                + ", Month(c.PlanningPayDate) as IMonth"
                                + ", b.CostCode"
                                + ", sum(isnull(c.Money, 0)) as Money"
                                + " from Contract a"
                                + ", ContractCost b"
                                + ", ContractCostPlan c"
                                + " where a.ContractCode = b.ContractCode"
                                + " and b.ContractCostCode = c.ContractCostCode"
                                + " and a.Status in (0, 2)" //已审、已结
                                + " and a.ContractDate is not null"
                                ;
                            if (BeginYm != "") sql += string.Format(" and c.PlanningPayDate >= convert(datetime, '{0}01', 112)", BeginYm);
                            if (EndYm != "") sql += string.Format(" and c.PlanningPayDate < dateadd(month, 1, convert(datetime, '{0}01', 112))", EndYm);

                            sql += GetProjectCodeInSql("a.ProjectCode");

                            sql += " group by a.ProjectCode, Year(c.PlanningPayDate), Month(c.PlanningPayDate), b.CostCode"
                                ;
                            break;

                        case "Fact": //实际已批
                            //已审的请款
                            sql = "select a.ProjectCode"
                                + ", Year(a.PayDate) as IYear"
                                + ", Month(a.PayDate) as IMonth"
                                + ", b.CostCode"
                                + ", sum(isnull(b.ItemMoney, 0)) as Money"
                                + " from Payment a"
                                + ", PaymentItem b"
                                + " where a.PaymentCode = b.PaymentCode"
                                + " and a.Status in (1, 2)" //已审、已结
                                + " and a.PayDate is not null"
                                ;
                            if (BeginYm != "") sql += string.Format(" and a.PayDate >= convert(datetime, '{0}01', 112)", BeginYm);
                            if (EndYm != "") sql += string.Format(" and a.PayDate < dateadd(month, 1, convert(datetime, '{0}01', 112))", EndYm);

                            sql += GetProjectCodeInSql("a.ProjectCode");

                            sql += " group by a.ProjectCode, Year(a.PayDate), Month(a.PayDate), b.CostCode"
                                ;
                            tbTemp = qa.ExecSqlForDataSet(sql).Tables[0];
                            break;

                        case "FactPay": //实际已付
                            //已审的付款
                            sql = "select a.ProjectCode"
                                + ", Year(a.PayoutDate) as IYear"
                                + ", Month(a.PayoutDate) as IMonth"
                                + ", pi.CostCode"
                                + ", sum(isnull(b.PayoutMoney, 0)) as Money"
                                + " from Payout a"
                                + ", PayoutItem b"
                                + ", PaymentItem pi"
                                + " where a.PayoutCode = b.PayoutCode"
                                + " and b.PaymentItemCode = pi.PaymentItemCode"
                                + " and a.PayoutDate is not null"
                                + " and a.Status > 0"
                                ;
                            if (BeginYm != "") sql += string.Format(" and a.PayoutDate >= convert(datetime, '{0}01', 112)", BeginYm);
                            if (EndYm != "") sql += string.Format(" and a.PayoutDate < dateadd(month, 1, convert(datetime, '{0}01', 112))", EndYm);

                            sql += GetProjectCodeInSql("a.ProjectCode");

                            sql += " group by a.ProjectCode, Year(a.PayoutDate), Month(a.PayoutDate), pi.CostCode"
                                ;
                            tbTemp = qa.ExecSqlForDataSet(sql).Tables[0];
                            break;
                    }

                    if (tbTemp != null)
                    {
                        foreach (DataRow drTemp in tbTemp.Rows)
                        {
                            DataRow drDtl = tbDtl.NewRow();
                            drDtl["IOType"] = "O";
                            drDtl["ProjectCode"] = drTemp["ProjectCode"];
                            drDtl["Year"] = drTemp["IYear"];
                            drDtl["Month"] = drTemp["IMonth"];
                            drDtl["Money"] = drTemp["Money"];
                            tbDtl.Rows.Add(drDtl);
                        }
                    }
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
        /// 取当前年度的月份范围
        /// </summary>
        /// <param name="CurrentY"></param>
        /// <param name="iBeginM"></param>
        /// <param name="iEndM"></param>
        private void GetCurrentYearMonthRange(int CurrentY, ref int iBeginM, ref int iEndM)
        {
            try
            {
                iBeginM = 1;
                iEndM = 12;

                if (CurrentY == BeginY)
                {
                    iBeginM = BLL.ConvertRule.ToInt(BeginYm.Substring(4, 2));
                }

                if (CurrentY == EndY)
                {
                    iEndM = BLL.ConvertRule.ToInt(EndYm.Substring(4, 2));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 取当前年度的季度范围
        /// </summary>
        /// <param name="CurrentY"></param>
        /// <param name="iBeginM"></param>
        /// <param name="iEndM"></param>
        private void GetCurrentYearQuarterRange(int CurrentY, ref int iBeginQ, ref int iEndQ)
        {
            try
            {
                iBeginQ = 1;
                iEndQ = 4;

                if (CurrentY == BeginY)
                {
                    iBeginQ = BLL.CashFlowRule.GetQuarterByMonth(BLL.ConvertRule.ToInt(BeginYm.Substring(4, 2)));
                }

                if (CurrentY == EndY)
                {
                    iEndQ = BLL.CashFlowRule.GetQuarterByMonth(BLL.ConvertRule.ToInt(EndYm.Substring(4, 2)));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string GetMoneyHtml(string ProjectCode, string IOType)
        {
            try
            {
                string Html = "";

                string c_filter = "1=1";
                if (ProjectCode != "")
                    c_filter += " and ProjectCode = '" + ProjectCode + "'";

                decimal Money;

                //合计列
                Money = 0;
                if (IOType == "") //净现金流
                {
                    Money = BLL.MathRule.SumColumn(tbDtl, "Money", c_filter + string.Format("and IOType = '{0}'", "I"))
                        - BLL.MathRule.SumColumn(tbDtl, "Money", c_filter + string.Format("and IOType = '{0}'", "O"));
                }
                else
                {
                    Money = BLL.MathRule.SumColumn(tbDtl, "Money", c_filter + string.Format("and IOType = '{0}'", IOType));
                }
                Html += string.Format("<td align=\"right\" nowrap class=\"sum\">{0}</td>", BLL.MathRule.GetDecimalShowString(Money));

                //季度、月度循环
                for (int y = BeginY; y <= EndY; y++)
                {

                    switch (MonthType.ToLower())
                    {
                        case "q":  //季度
                            int iBeginQ = 0;
                            int iEndQ = 0;
                            GetCurrentYearQuarterRange(y, ref iBeginQ, ref iEndQ);

                            for (int q = iBeginQ; q <= iEndQ; q++)
                            {
                                string MonthIn = BLL.CashFlowRule.GetMonthInQuoater(q);
                                Money = 0;

                                string filter = c_filter + string.Format("and Year = {0} and Month in ({1})", y, MonthIn);

                                //本季度
                                if (IOType == "") //净现金流
                                {
                                    Money = BLL.MathRule.SumColumn(tbDtl, "Money", filter + string.Format("and IOType = '{0}'", "I"))
                                        - BLL.MathRule.SumColumn(tbDtl, "Money", filter + string.Format("and IOType = '{0}'", "O"));
                                }
                                else
                                {
                                    Money = BLL.MathRule.SumColumn(tbDtl, "Money", filter + string.Format("and IOType = '{0}'", IOType));
                                }

                                Html += string.Format("<td align=\"right\" nowrap>{0}</td>", BLL.MathRule.GetDecimalShowString(Money));
                            }

                            break;

                        case "m":  //月度
                            int iBeginM = 0;
                            int iEndM = 0;
                            GetCurrentYearMonthRange(y, ref iBeginM, ref iEndM);

                            for (int m = iBeginM; m <= iEndM; m++)
                            {
                                Money = 0;

                                string filter = c_filter + string.Format("and Year = {0} and Month = {1}", y, m);

                                //本月
                                if (IOType == "") //净现金流
                                {
                                    Money = BLL.MathRule.SumColumn(tbDtl, "Money", filter + string.Format("and IOType = '{0}'", "I"))
                                        - BLL.MathRule.SumColumn(tbDtl, "Money", filter + string.Format("and IOType = '{0}'", "O"));
                                }
                                else
                                {
                                    Money = BLL.MathRule.SumColumn(tbDtl, "Money", filter + string.Format("and IOType = '{0}'", IOType));
                                }

                                Html += string.Format("<td align=\"right\" nowrap>{0}</td>", BLL.MathRule.GetDecimalShowString(Money));
                            }

                            break;
                    }
                }

                return Html;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GenerateHtml()
        {
            try
            {
                DataRow drHtml;

                foreach (DataRow drProject in tbProject.Rows)
                {
                    string ProjectCode = BLL.ConvertRule.ToString(drProject["ProjectCode"]);

                    drHtml = tbHtml.NewRow();
                    drHtml["Type"] = "";
                    drHtml["ProjectCode"] = drProject["ProjectCode"];
                    drHtml["ProjectName"] = drProject["ProjectName"];
                    tbHtml.Rows.Add(drHtml);

                    //流入
                    drHtml["MoneyHtmlI"] = GetMoneyHtml(ProjectCode, "I");

                    //流出
                    drHtml["MoneyHtmlO"] = GetMoneyHtml(ProjectCode, "O");

                    //净现金流
                    drHtml["MoneyHtml"] = GetMoneyHtml(ProjectCode, "");
                }

                //合计
                drHtml = tbHtml.NewRow();
                drHtml["Type"] = "SUM";
                drHtml["ProjectCode"] = "";
                drHtml["ProjectName"] = "";
                tbHtml.Rows.Add(drHtml);

                //流入
                drHtml["MoneyHtmlI"] = GetMoneyHtml("", "I");

                //流出
                drHtml["MoneyHtmlO"] = GetMoneyHtml("", "O");

                //净现金流
                drHtml["MoneyHtml"] = GetMoneyHtml("", "");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 生成集团的现金流表
        /// </summary>
        /// <returns></returns>
        private void GenerateCashFlow()
        {
            try
            {
                CreateDataSet();

                //项目列表
                string[] arrProjectCode = ProjectCodes.Split(","[0]);
                foreach (string ProjectCode in arrProjectCode)
                {
                    DataRow drNew = tbProject.NewRow();
                    drNew["ProjectCode"] = ProjectCode;
                    drNew["ProjectName"] = BLL.ProjectRule.GetProjectName(ProjectCode);
                    tbProject.Rows.Add(drNew);
                }

                //流入
                GenerateIn();

                //流入
                GenerateOut();

                GenerateHtml();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 生成年度、月或季度的标题
        /// </summary>
        /// <param name="MonthType"></param>
        /// <param name="StartY"></param>
        /// <param name="EndY"></param>
        /// <param name="html_title1"></param>
        /// <param name="html_title2"></param>
        /// <param name="MonthCount"></param>
        private void GenerateYearTitleHtml(ref string html_title1, ref string html_title2, ref int MonthCount)
        {
            try
            {
                switch (MonthType.ToLower())
                {
                    case "q":
                        GenerateYearQuarterTitleHtml(ref html_title1, ref html_title2, ref MonthCount);
                        break;

                    case "m":
                        GenerateYearMonthTitleHtml(ref html_title1, ref html_title2, ref MonthCount);
                        break;
                }

                //合计列
//                html_title1 += "<th align=center nowrap rowspan=2>合计</th>";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 生成年度、季度的标题Html
        /// </summary>
        private void GenerateYearQuarterTitleHtml(ref string html_title1, ref string html_title2, ref int MonthCount)
        {
            try
            {
                html_title1 = "";
                html_title2 = "";
                MonthCount = 0;

                for (int y = BeginY; y <= EndY; y++)
                {
                    int iBeginQ = 0;
                    int iEndQ = 0;
                    GetCurrentYearQuarterRange(y, ref iBeginQ, ref iEndQ);

                    html_title1 += string.Format("<th colspan={1} align=center nowrap>{0}年</th>", y, iEndQ - iBeginQ + 1);

                    for (int q = iBeginQ; q <= iEndQ; q++)
                    {
                        MonthCount++;
                        html_title2 += string.Format("<th align=center nowrap>{0}季度</th>", q);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 生成年度、月度的标题Html
        /// </summary>
        private void GenerateYearMonthTitleHtml(ref string html_title1, ref string html_title2, ref int MonthCount)
        {
            try
            {
                html_title1 = "";
                html_title2 = "";
                MonthCount = 0;

                for (int y = BeginY; y <= EndY; y++)
                {
                    int iBeginM = 0;
                    int iEndM = 0;
                    GetCurrentYearMonthRange(y, ref iBeginM, ref iEndM);

                    html_title1 += string.Format("<th colspan={1} align=center nowrap>{0}年</th>", y, iEndM - iBeginM + 1);

                    for (int m = iBeginM; m <= iEndM; m++)
                    {
                        MonthCount++;
                        html_title2 += string.Format("<th align=center nowrap>{0}月</th>", m);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadGrid()
		{
			try 
			{
                Source = this.sltSource.Value;
                MonthType = this.sltMonthType.Value;

                //Source = BLL.ConvertRule.ToString(Request.QueryString["Source"]);
                //MonthType = BLL.ConvertRule.ToString(Request.QueryString["MonthType"]);

                //项目
                ProjectCodes = this.ucSelectProjectMulti.Value;
                if (ProjectCodes == "")
                {
                    foreach (DataRow dr in user.m_EntityDataAccessProject.CurrentTable.Rows)
                        ProjectCodes += BLL.ConvertRule.ToString(dr["ProjectCode"]) + ",";

                    if (ProjectCodes.Length > 0)
                        ProjectCodes = ProjectCodes.Substring(0, ProjectCodes.Length - 1);
                }

//                Response.Write(Rms.Web.JavaScript.Alert(true, "user:" + user.UserName + "," + user.m_EntityDataAccessProject.CurrentTable.Rows.Count.ToString()));

				BLL.CashFlowSource source = BLL.CashFlowRule.GetCashFlowSourceById(Source);
				string SourceDesc = (source != null)?source.Desc:"";
				this.lblSourceName.Text = SourceDesc;

                BeginYm = this.ucCostBudgetSelectMonth.MonthStart.Replace("-", "");
                EndYm = this.ucCostBudgetSelectMonth.MonthEnd.Replace("-", "");
                
                BeginY = BLL.ConvertRule.ToInt(BeginYm.Substring(0, 4));
                EndY = BLL.ConvertRule.ToInt(EndYm.Substring(0, 4));

				//年度展开
				string html_title1 = "";
				string html_title2 = "";
				int MonthCount = 0;
				GenerateYearTitleHtml(ref html_title1, ref html_title2, ref MonthCount);

				ViewState["html_title1"] = html_title1;
				ViewState["html_title2"] = html_title2;
				ViewState["MonthCount"] = MonthCount;

				//生成现金流表
				GenerateCashFlow();

				DataView dvDtl = new DataView(tbHtml, "Type = ''", "ProjectName", DataViewRowState.CurrentRows);
				this.dgList.DataSource = dvDtl;
				this.dgList.DataBind();

				//总计
                DataView dvSum = new DataView(tbHtml, "Type = 'SUM'", "ProjectName", DataViewRowState.CurrentRows);
                this.dgListTotal.DataSource = dvSum;
				this.dgListTotal.DataBind();
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示报表出错：" + ex.Message));
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
				LoadGrid();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示年度出错：" + ex.Message));
			}
		}

        /// <summary>
        /// 统计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDoReport_ServerClick(object sender, EventArgs e)
        {
            try
            {
                LoadGrid();
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
                Response.Write(Rms.Web.JavaScript.Alert(true, "统计出错：" + ex.Message));
            }
        }

    }
}
