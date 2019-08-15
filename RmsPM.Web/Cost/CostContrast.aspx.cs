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

using Rms.ORMap;
using RmsPM.DAL;
using RmsPM.BLL;
using RmsPM.DAL.QueryStrategy;

namespace RmsPM.Web.Cost
{
	/// <summary>
	/// CostContrast 的摘要说明。
	/// </summary>
	public partial class CostContrast : PageBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack)
			{
				LoadData();
			}
		}

		// costCode == ""时， 是总成本
		private void LoadData()
		{
			string costCode = Request["CostCode"] + "";
			string projectCode = Request["ProjectCode"] + "";

			try
			{

				if ( costCode == "")
					this.lblCostName.Text = "总成本";
				else
					this.lblCostName.Text = BLL.CBSRule.GetCostName(costCode);


				DateTime tStart = DateTime.Today;		//整个图表的起始时间
				DateTime tEnd = DateTime.Today;			//整个图标的结束时间， 用来确定图表的时间轴
				decimal maxValue = decimal.Zero;		//整个图表中的最高数值， 用来确定图表的高度

				EntityData estimateCheck = RmsPM.DAL.EntityDAO.CBSDAO.GetCostEstimateCheckByCode(projectCode);
				
				if ( ! estimateCheck.HasRecord())
				{
					Response.Write(Rms.Web.JavaScript.ScriptStart);
					Response.Write(Rms.Web.JavaScript.Alert(false,"还没有做好估算，请先做好估算 ！"));
					Response.Write(Rms.Web.JavaScript.WinClose(false));
					Response.Write(Rms.Web.JavaScript.ScriptEnd);
					return;
				}

				tStart = DateTime.Parse(estimateCheck.GetDateTimeOnlyDate("CheckDate"));
				estimateCheck.Dispose();
				this.lblDate.Text = "时间： 从 " + tStart.ToString("yyyy-MM-dd") + " 到 " + tEnd.ToString("yyyy-MM-dd");

				// 估算费用
				decimal ce = BLL.CBSRule.SumTotalEstimateCost(costCode,projectCode);

				TimeSpan ts = tEnd - tStart ;
				int Length = Math.Abs(ts.Days)+1;

				string [] CEs = new string[ Length ];			//估算成本数组
				string [] Bs = new string[ Length ];			//预算成本数组
				string [] Ds = new string[ Length ];			//动态成本数组

				//设置初值
				for ( int i =0 ; i< Length ; i++)
				{
					CEs[i] = ce.ToString();
					Bs[i]=ce.ToString();
					Ds[i]=ce.ToString();
				}

				string sql = "";
				int iCount = 0;
				TimeSpan tSpan  ;
				int iS = 0;
				decimal dTemp = decimal.Zero;
				DateTime checkDate ;
				QueryAgent qa = new QueryAgent();
				// 费用项在BudgetCost中找
				if ( costCode != "" )
				{
					// 预算 , 以及必然存在的动态
					V_BudgetCostStrategyBuilder sb = new V_BudgetCostStrategyBuilder();
					sb.AddStrategy( new Strategy(V_BudgetCostStrategyName.ProjectCode,projectCode) );
					sb.AddStrategy( new Strategy(V_BudgetCostStrategyName.IsDynamic,"0") );
					sb.AddStrategy( new Strategy(V_BudgetCostStrategyName.Flag,"0,2") );
					sb.AddStrategy( new Strategy(V_BudgetCostStrategyName.CostCode,costCode) );
					sb.AddOrder("CheckDate",true);
					sql = sb.BuildMainQueryString();
					EntityData yuBudgetCost = qa.FillEntityData("V_BudgetCost",sql);
					iCount = yuBudgetCost.CurrentTable.Rows.Count;
					for ( int i=0;i<iCount;i++)
					{
						yuBudgetCost.SetCurrentRow(i);
						dTemp = yuBudgetCost.GetDecimal("BudgetCost");
						if ( dTemp > maxValue )
							maxValue = dTemp;
						checkDate = DateTime.Parse(yuBudgetCost.GetDateTimeOnlyDate("CheckDate"));
						tSpan = checkDate - tStart;
						iS = tSpan.Days ;
						for ( int j = iS;j<Length;j++)
						{
							Bs[j] = dTemp.ToString();
							Ds[j] = dTemp.ToString();
						}
						
					}

					// 动态修改申请
					sb = new V_BudgetCostStrategyBuilder();
					sb.AddStrategy( new Strategy(V_BudgetCostStrategyName.ProjectCode,projectCode) );
					sb.AddStrategy( new Strategy(V_BudgetCostStrategyName.IsDynamic,"2") );
					sb.AddStrategy( new Strategy(V_BudgetCostStrategyName.Flag,"2") );
					sb.AddStrategy( new Strategy(V_BudgetCostStrategyName.CostCode,costCode) );
					sb.AddOrder("CheckDate",true);

					sql = sb.BuildMainQueryString();
					yuBudgetCost = qa.FillEntityData("V_BudgetCost",sql);
					iCount = yuBudgetCost.CurrentTable.Rows.Count;
					for ( int i=0;i<iCount;i++)
					{
						yuBudgetCost.SetCurrentRow(i);
						dTemp = yuBudgetCost.GetDecimal("BudgetCost");
						if ( dTemp > maxValue )
							maxValue = dTemp;
						checkDate = DateTime.Parse(yuBudgetCost.GetDateTimeOnlyDate("CheckDate"));
						tSpan = checkDate - tStart;
						iS = tSpan.Days ;
						for ( int j = iS;j<Length;j++)
						{
							Ds[j] = dTemp.ToString();
						}
					}
					yuBudgetCost.Dispose();

				}
					// 总费用
				else
				{
					// 预算 , 以及必然存在的动态
					BudgetStrategyBuilder sb = new BudgetStrategyBuilder();
					sb.AddStrategy( new Strategy(BudgetStrategyName.ProjectCode,projectCode) );
					sb.AddStrategy( new Strategy(BudgetStrategyName.IsDynamic,"0") );
					sb.AddStrategy( new Strategy(BudgetStrategyName.Flag,"0,2") );
					sb.AddOrder("CheckDate",true);
					sql = sb.BuildMainQueryString();
					EntityData budget = qa.FillEntityData("Budget",sql);
					iCount = budget.CurrentTable.Rows.Count;
					for ( int i=0;i<iCount;i++)
					{
						budget.SetCurrentRow(i);
						dTemp = budget.GetDecimal("TotalMoney");
						if ( dTemp > maxValue )
							maxValue = dTemp;
						checkDate = DateTime.Parse(budget.GetDateTimeOnlyDate("CheckDate"));
						tSpan = checkDate - tStart;
						iS = tSpan.Days ;
						for ( int j = iS;j<Length;j++)
						{
							Bs[j] = dTemp.ToString();
							Ds[j] = dTemp.ToString();
						}
					}

					// 动态修改申请
					sb = new BudgetStrategyBuilder();
					sb.AddStrategy( new Strategy(BudgetStrategyName.ProjectCode,projectCode) );
					sb.AddStrategy( new Strategy(BudgetStrategyName.IsDynamic,"2") );
					sb.AddStrategy( new Strategy(BudgetStrategyName.Flag,"2") );
					sb.AddOrder("CheckDate",true);

					sql = sb.BuildMainQueryString();
					budget = qa.FillEntityData("Budget",sql);
					iCount = budget.CurrentTable.Rows.Count;
					for ( int i=0;i<iCount;i++)
					{
						budget.SetCurrentRow(i);
						dTemp = budget.GetDecimal("TotalMoney");
						if ( dTemp > maxValue )
							maxValue = dTemp;
						checkDate = DateTime.Parse(budget.GetDateTimeOnlyDate("CheckDate"));
						tSpan = checkDate - tStart;
						iS = tSpan.Days ;
						for ( int j = iS;j<Length;j++)
						{
							Ds[j] = dTemp.ToString();
						}
					}
					budget.Dispose();
				}

				qa.Dispose();

//
//				//生成字符串
				string cString ="";
				string bString ="";
				string dString ="";
				for ( int i =0 ; i<Length; i++)
				{
					if ( i != 0 )
					{
						cString += ",";
						bString += ",";
						dString += ",";
					}

					cString += CEs[i];
					bString += Bs[i];
					dString += Ds[i];

				}

				ShowChar( cString,bString,dString,maxValue , this.tdGraphy );

			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"加载成本数据错误。");
			}

		}


		

		private void ShowChar( string cString , string bString , string dString , decimal maxValue , HtmlTableCell cell )
		{


			string url = Request.Url.AbsoluteUri;
			int i = url.LastIndexOf("/");
			string path = url.Substring(0,i+1);
			decimal dRange = (maxValue*1.2m);
			string sRange = dRange.ToString();


			StringBuilder sb = new StringBuilder();
			
			sb.Append ( @"<APPLET height=300 width=500 archive=""chart.jar"" codebase=""" + path  +  @"""  code=""com.objectplanet.chart.LineChartApplet""   VIEWASTEXT>");

			sb.Append ( @"<param name=seriesCount value=3>"  );
			sb.Append ( @"<param name=sampleValues_0 value=""" + cString +  @""">"  );
			sb.Append ( @"<param name=sampleValues_1 value=""" + bString +  @""">"  );
			sb.Append ( @"<param name=sampleValues_2 value=""" + dString +  @""">"  );
			sb.Append ( @"<param name=sampleColors value=""black, green, red"">"  );
			sb.Append ( @"<param name=range value="""+ sRange +@""">"  );
			sb.Append ( @"</applet> ");
			cell.InnerHtml=sb.ToString();

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
	}
}
