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
	/// RptFinIOChart ��ժҪ˵����
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

		private void IniPage()
		{
			try 
			{
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];

				int type = BLL.ConvertRule.ToInt(Request.QueryString["Type"]);
				this.txtType.Value = type.ToString();

				/*
				//���
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
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
                    this.lblProjectName.Text = "����";
                }

                if (Source.Trim() == "")
                {
                    Response.Write(Rms.Web.JavaScript.Alert(true, "��ѡ��ͳ������"));
                    return;
                }

                if ((StartY == 0) || (EndY == 0))
                    return;
                
//                int StartY = BLL.ConvertRule.ToInt(Request.QueryString["StartY"]);
//				int EndY = BLL.ConvertRule.ToInt(Request.QueryString["EndY"]);

				//����
                switch (MonthType.ToLower())
                {
                    case "q":
                        this.lblTitle1.Text = "���";
                        this.lblTitle2.Text = BLL.CashFlowRule.GetMonthTypeName(MonthType);
                        break;

                    case "m":
                        goto case "q";

                    case "d":
                        this.lblTitle1.Text = "����";
                        this.lblTitle2.Text = "��";
                        break;
                }

				//���չ��
				string html_title1 = "";
				string html_title2 = "";
				int MonthCount = 0;

				//ȡ����
				DataTable tb;

				switch (Type)
				{
					case 1:
						//����ֵ
                        BLL.CashFlowRule.GenerateYearTitleHtml(MonthType, StartY, EndY, ref html_title1, ref html_title2, ref MonthCount);
                        tb = BLL.CashFlowRule.GetNetCashFlowTotal(ProjectCode, StartY, EndY, MonthType, DiscountRate, IsSum);
						break;

					case 2:
						//�ڲ�����
                        BLL.CashFlowRule.GenerateYearTitleHtml(MonthType, StartY, EndY, ref html_title1, ref html_title2, ref MonthCount);
                        tb = BLL.CashFlowRule.GetIncomeTotal(ProjectCode);
						break;

					default:
						//�ֽ�����
                        BLL.CashFlowRule.GenerateYearTitleHtml(MonthType, StartDate, EndDate, ref html_title1, ref html_title2, ref MonthCount);
                        tb = BLL.CashFlowRule.GetCashFlowTotal(ProjectCode, StartDate, EndDate, MonthType, IsSum, Source);
						break;
				}

                ViewState["html_title1"] = html_title1;
                ViewState["html_title2"] = html_title2;
                ViewState["MonthCount"] = MonthCount;

                //Ԫת�ɰ���
                switch (Type)
                {
                    case 1:
                        //����ֵ
                        BLL.CashFlowRule.YuanToMil(tb, StartY, EndY, MonthType);
                        BLL.CashFlowRule.FillCashFlowMoneyHtml(tb, StartY, EndY, MonthType, "#,##0");
                        break;

                    case 2:
                        //�ڲ�����
                        break;

                    default:
                        //�ֽ�����
                        BLL.CashFlowRule.YuanToMil(tb, StartDate, EndDate, MonthType, 6);
                        BLL.CashFlowRule.FillCashFlowMoneyHtml(tb, StartDate, EndDate, MonthType, "#,##0");
                        break;
                }

				if (tb.Rows.Count == 0) 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "������"));
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
						//ʵ��
						dv = new DataView(tb, "id=" + Source.ToString(), "", DataViewRowState.CurrentRows);
						break;

					case 2:
						//�ƻ���׼
						dv = new DataView(tb, "id=" + Source.ToString(), "", DataViewRowState.CurrentRows);
						break;

					case 3:
						//�ƻ�����
						dv = new DataView(tb, "id=" + Source.ToString(), "", DataViewRowState.CurrentRows);
						break;

					case 12:
						//ʵ��-�ƻ���׼
						dv = new DataView(tb, "id in (1, 2)", "", DataViewRowState.CurrentRows);
						break;

					case 23:
						//�ƻ���׼-����
						dv = new DataView(tb, "id in (2, 3)", "", DataViewRowState.CurrentRows);
						break;

					case 13:
						//ʵ��-�ƻ�����
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
					case "column":  //����ͼ
						this.ucChartColumn.Source = Source;
						this.ucChartColumn.Type = Type;
                        this.ucChartColumn.MonthType = MonthType;
                        this.ucChartColumn.BindChart(dv);
						this.ucChartColumn.Visible = true;

						break;

					default:  //����ͼ
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾͼ�����" + ex.Message));
			}
		}

		/*
		/// <summary>
		/// ��ʾĳ����Χ
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ��ȳ���" + ex.Message));
			}
		}
		*/

	}
}
