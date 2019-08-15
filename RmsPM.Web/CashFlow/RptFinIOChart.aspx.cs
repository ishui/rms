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
	/// RptFinIOChart 的摘要说明。
	/// </summary>
	public partial class RptFinIOChart : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
//			this.ucCostBudgetSelectMonth.GotoMonthClick += new System.EventHandler(this.btnGotoMonth_ServerClick);

			if (!IsPostBack)
			{
				IniPage();
				LoadChart();
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
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];

				int type = BLL.ConvertRule.ToInt(Request.QueryString["Type"]);
				this.txtType.Value = type.ToString();

				/*
				//年度
				int StartY = 0;
				int EndY = 0;
				BLL.CashFlowRule.GetCashFlowStartEnd(this.txtProjectCode.Value, ref StartY, ref EndY);
				this.ucCostBudgetSelectMonth.MonthStart = StartY.ToString();
				this.ucCostBudgetSelectMonth.MonthEnd = EndY.ToString();
				*/
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

		private void LoadChart()
		{
			try 
			{
				string ProjectCode = this.txtProjectCode.Value;
				string ChartType = BLL.ConvertRule.ToString(Request.QueryString["ChartType"]);
				string MonthType = BLL.ConvertRule.ToString(Request.QueryString["MonthType"]);
				int Type = BLL.ConvertRule.ToInt(this.txtType.Value);
				string Source = BLL.ConvertRule.ToString(Request.QueryString["Source"]);
				int IsSum = BLL.ConvertRule.ToInt(Request.QueryString["IsSum"]);
				decimal DiscountRate = BLL.ConvertRule.ToDecimal(Request.QueryString["DiscountRate"]);

                string StartDate = BLL.ConvertRule.ToString(Request.QueryString["StartY"]);
                string EndDate = BLL.ConvertRule.ToString(Request.QueryString["EndY"]);

                int StartY = BLL.ConvertRule.ToInt(StartDate.Substring(0, 4));
                int EndY = BLL.ConvertRule.ToInt(EndDate.Substring(0, 4));

                if (ProjectCode != "")
                {
                    this.lblProjectName.Text = BLL.ProjectRule.GetProjectName(ProjectCode);
                }
                else
                {
                    this.lblProjectName.Text = "集团";
                }

                if (Source.Trim() == "")
                {
                    Response.Write(Rms.Web.JavaScript.Alert(true, "请选择统计类型"));
                    return;
                }

                if ((StartY == 0) || (EndY == 0))
                    return;
                
//                int StartY = BLL.ConvertRule.ToInt(Request.QueryString["StartY"]);
//				int EndY = BLL.ConvertRule.ToInt(Request.QueryString["EndY"]);

				//标题
                switch (MonthType.ToLower())
                {
                    case "q":
                        this.lblTitle1.Text = "年度";
                        this.lblTitle2.Text = BLL.CashFlowRule.GetMonthTypeName(MonthType);
                        break;

                    case "m":
                        goto case "q";

                    case "d":
                        this.lblTitle1.Text = "年月";
                        this.lblTitle2.Text = "日";
                        break;
                }

				//年度展开
				string html_title1 = "";
				string html_title2 = "";
				int MonthCount = 0;

				//取数据
				DataTable tb;

				switch (Type)
				{
					case 1:
						//净现值
                        BLL.CashFlowRule.GenerateYearTitleHtml(MonthType, StartY, EndY, ref html_title1, ref html_title2, ref MonthCount);
                        tb = BLL.CashFlowRule.GetNetCashFlowTotal(ProjectCode, StartY, EndY, MonthType, DiscountRate, IsSum);
						break;

					case 2:
						//内部收益
                        BLL.CashFlowRule.GenerateYearTitleHtml(MonthType, StartY, EndY, ref html_title1, ref html_title2, ref MonthCount);
                        tb = BLL.CashFlowRule.GetIncomeTotal(ProjectCode);
						break;

					default:
						//现金流量
                        BLL.CashFlowRule.GenerateYearTitleHtml(MonthType, StartDate, EndDate, ref html_title1, ref html_title2, ref MonthCount);
                        tb = BLL.CashFlowRule.GetCashFlowTotal(ProjectCode, StartDate, EndDate, MonthType, IsSum, Source);
						break;
				}

                ViewState["html_title1"] = html_title1;
                ViewState["html_title2"] = html_title2;
                ViewState["MonthCount"] = MonthCount;

                //元转成百万
                switch (Type)
                {
                    case 1:
                        //净现值
                        BLL.CashFlowRule.YuanToMil(tb, StartY, EndY, MonthType);
                        BLL.CashFlowRule.FillCashFlowMoneyHtml(tb, StartY, EndY, MonthType, "#,##0");
                        break;

                    case 2:
                        //内部收益
                        break;

                    default:
                        //现金流量
                        BLL.CashFlowRule.YuanToMil(tb, StartDate, EndDate, MonthType, 6);
                        BLL.CashFlowRule.FillCashFlowMoneyHtml(tb, StartDate, EndDate, MonthType, "#,##0");
                        break;
                }

				if (tb.Rows.Count == 0) 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "无数据"));
					return;
				}

				DataView dv;

				string filter = "";
				if (Source != "") 
				{
					string StrIn = "'" + Source.Replace(",", "','") + "'";
					filter = string.Format("id in ({0})", StrIn);
				}
				dv = new DataView(tb, filter, "", DataViewRowState.CurrentRows);

				/*
				switch (Source)
				{
					case 1:
						//实际
						dv = new DataView(tb, "id=" + Source.ToString(), "", DataViewRowState.CurrentRows);
						break;

					case 2:
						//计划基准
						dv = new DataView(tb, "id=" + Source.ToString(), "", DataViewRowState.CurrentRows);
						break;

					case 3:
						//计划调整
						dv = new DataView(tb, "id=" + Source.ToString(), "", DataViewRowState.CurrentRows);
						break;

					case 12:
						//实际-计划基准
						dv = new DataView(tb, "id in (1, 2)", "", DataViewRowState.CurrentRows);
						break;

					case 23:
						//计划基准-调整
						dv = new DataView(tb, "id in (2, 3)", "", DataViewRowState.CurrentRows);
						break;

					case 13:
						//实际-计划调整
						dv = new DataView(tb, "id in (1, 3)", "", DataViewRowState.CurrentRows);
						break;

					default:
						dv = new DataView(tb);
						break;
				}
				*/

				if (Type == 2)
				{
					this.dgList.Visible = false;
					this.dgListPercent.Visible = true;

					this.dgListPercent.DataSource = dv;
					this.dgListPercent.DataBind();
				}
				else 
				{
					this.dgList.DataSource = dv;
					this.dgList.DataBind();
				}

				switch (ChartType.ToLower()) 
				{
					case "column":  //体量图
						this.ucChartColumn.Source = Source;
						this.ucChartColumn.Type = Type;
                        this.ucChartColumn.MonthType = MonthType;
                        this.ucChartColumn.BindChart(dv);
						this.ucChartColumn.Visible = true;

						break;

					default:  //线性图
						this.ucChartLine.Source = Source;
						this.ucChartLine.Type = Type;
                        this.ucChartLine.MonthType = MonthType;
                        this.ucChartLine.BindChart(dv);
						this.ucChartLine.Visible = true;

						break;
				}

			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示图表出错：" + ex.Message));
			}
		}

		/*
		/// <summary>
		/// 显示某个范围
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnGotoMonth_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				LoadChart();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示年度出错：" + ex.Message));
			}
		}
		*/

	}
}
