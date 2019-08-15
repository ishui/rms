namespace RmsPM.Web.Construct
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Collections;
	using Rms.ORMap;
	using RmsPM.DAL.QueryStrategy;
	using RmsPM.BLL;
	using Rms.Web;

	/// <summary>
	/// ConstructPlanChart
	/// </summary>
	public partial class ConstructPlanChart : System.Web.UI.UserControl
	{

//		protected decimal MonthPixel = 50;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if (this.Visible) 
			{
				string reload = Rms.Web.JavaScript.ScriptStart;
				reload += @"var ClientID = '" + this.ClientID + "';" + "\n" ;
				reload += Rms.Web.JavaScript.ScriptEnd;
				Response.Write(reload);

				if (!Page.IsPostBack) 
				{
					IniPage();
				}
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
		///		设计器支持所需的方法 - 不要使用代码编辑器
		///		修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion

		private void IniPage() 
		{
			try
			{
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"初始化页面失败");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面失败：" + ex.Message));
			}
		}

		/// <summary>
		/// 设置单位工程、年度，初始化
		/// </summary>
		/// <param name="ProjectCode"></param>
		public void SetKeyValue(string AnnualPlanCode)
		{
			try 
			{
				this.txtAnnualPlanCode.Value = AnnualPlanCode;
				LoadChart(this.txtAnnualPlanCode.Value);
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"初始化页面失败");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面失败：" + ex.Message));
			}
		}

/*
		private string GetGraphLeft(object StartDate, object EndDate, int TotalMonth) 
		{
			string left = "0";

			if (TotalMonth != 0) 
			{
				if ((StartDate != DBNull.Value) && (StartDate != null) && (EndDate != DBNull.Value) && (EndDate != null))
				{
					int month = GetMonthBetween(StartDate, EndDate);
					decimal dMonth = month;

					//绝对像素
					decimal d = month * MonthPixel;
					left = d.ToString() + "px";

					//百分比
//					decimal d = Math.Round(dMonth / TotalMonth, 2) * 100;
//					left = d.ToString() + "%";
				}
			}

			return left;
		}

		private string GetGraphWidth(object StartDate, object EndDate, int TotalMonth) 
		{
			string left = "0";

			if (TotalMonth != 0) 
			{
				if ((StartDate != DBNull.Value) && (StartDate != null) && (EndDate != DBNull.Value) && (EndDate != null))
				{
					int month = GetMonthBetween(StartDate, EndDate);
					month = month + 1;
					decimal dMonth = month;

					//绝对像素
					decimal d = month * MonthPixel;
					left = d.ToString() + "px";

					//百分比
//					decimal d = Math.Round(dMonth / TotalMonth, 2) * 100;
//					left = d.ToString() + "%";
				}
			}

			return left;
		}
*/

		private DataTable GetDataTable(string AnnualPlanCode) 
		{
			string PBSUnitCode = "";
			int year = 0;

			//取当前计划的年份
			EntityData entityMst = DAL.EntityDAO.ConstructDAO.GetConstructAnnualPlanByCode(AnnualPlanCode);
			if (entityMst.HasRecord()) 
			{
				PBSUnitCode = entityMst.GetString("PBSUnitCode");
				year = entityMst.GetInt("IYear");
			}
			entityMst.Dispose();

			//取计划进度表
			DataTable tb = BLL.ConstructRule.GenerateConstructPlanProgressTable(PBSUnitCode, year);

			//填上最后报告日期
			BLL.ConstructRule.AddConstructProgressLastReportDate(PBSUnitCode, tb);

			//格式化开始、结束日期
			BLL.ConstructRule.FormatConstructProgressDate(tb);

			//增加字段：图形位置、长度
//			tb.Columns.Add(new DataColumn("Left", typeof(string)));
//			tb.Columns.Add(new DataColumn("Width", typeof(string)));
//			tb.Columns.Add(new DataColumn("PLeft", typeof(string)));
//			tb.Columns.Add(new DataColumn("PWidth", typeof(string)));

			tb.Columns.Add(new DataColumn("LeftMargin", typeof(int)));
			tb.Columns.Add(new DataColumn("RightMargin", typeof(int)));
			tb.Columns.Add(new DataColumn("Months", typeof(int)));

			tb.Columns.Add(new DataColumn("PLeftMargin", typeof(int)));
			tb.Columns.Add(new DataColumn("PRightMargin", typeof(int)));
			tb.Columns.Add(new DataColumn("PMonths", typeof(int)));

			tb.Columns.Add(new DataColumn("BarDisplay", typeof(string)));
			tb.Columns.Add(new DataColumn("PBarDisplay", typeof(string)));

			tb.Columns.Add(new DataColumn("PointDisplay", typeof(string)));
			tb.Columns.Add(new DataColumn("PPointDisplay", typeof(string)));

			tb.Columns.Add(new DataColumn("Hint", typeof(string)));
			tb.Columns.Add(new DataColumn("PHint", typeof(string)));

			//设置时间点
			object dMin = null;
			object dMax = null;

			//循环取计划、实际日期的最小值、最大值作为日期范围
			foreach(DataRow dr in tb.Rows) 
			{
				//取最小、最大日期
				dMin = BLL.ProgChartRule.GetMinDate(new object[]{dMin, dr["StartDate"], dr["EndDate"], dr["PStartDate"], dr["PEndDate"]});
				dMax = BLL.ProgChartRule.GetMaxDate(new object[]{dMax, dr["StartDate"], dr["EndDate"], dr["PStartDate"], dr["PEndDate"]});
			}

			//计算日期范围内的总月数
			int TotalMonth = BLL.ProgChartRule.GetMonthBetween(dMin, dMax) + 1;

			//计算每条记录的图形位置、长度
			foreach(DataRow dr in tb.Rows) 
			{
//				dr["PLeft"] = GetGraphLeft(dMin, dr["PStartDate"], TotalMonth);
//				dr["Left"] = GetGraphLeft(dMin, dr["StartDate"], TotalMonth);
//
//				dr["PWidth"] = GetGraphWidth(dr["PStartDate"], dr["PEndDate"], TotalMonth);
//				dr["Width"] = GetGraphWidth(dr["StartDate"], dr["EndDate"], TotalMonth);

				dr["LeftMargin"] = BLL.ProgChartRule.ToColSpan(BLL.ProgChartRule.GetMonthBetween(dMin, dr["StartDate"]) + 1);
				dr["Months"] = BLL.ProgChartRule.ToColSpan(BLL.ProgChartRule.GetMonthBetween(dr["StartDate"], dr["EndDate"]) + 1);
				dr["RightMargin"] = BLL.ProgChartRule.ToColSpan(TotalMonth - (int)dr["LeftMargin"] - (int)dr["Months"] + 1);

				dr["PLeftMargin"] = BLL.ProgChartRule.ToColSpan(BLL.ProgChartRule.GetMonthBetween(dMin, dr["PStartDate"]) + 1);
				dr["PMonths"] = BLL.ProgChartRule.ToColSpan(BLL.ProgChartRule.GetMonthBetween(dr["PStartDate"], dr["PEndDate"]) + 1);
				dr["PRightMargin"] = BLL.ProgChartRule.ToColSpan(TotalMonth - (int)dr["PLeftMargin"] - (int)dr["PMonths"] + 1);

				//显示：条形图、点
				if ((dr["StartDate"] == DBNull.Value) || (dr["EndDate"] == DBNull.Value)
					|| (BLL.ConvertRule.ToDateString(dr["StartDate"], "yyyy-MM-dd") == BLL.ConvertRule.ToDateString(dr["EndDate"], "yyyy-MM-dd"))) 
				{
					dr["BarDisplay"] = "none";

					if ((dr["StartDate"] == DBNull.Value) && (dr["EndDate"] == DBNull.Value)) 
					{
						dr["PointDisplay"] = "none";
					}
					else 
					{
						dr["PointDisplay"] = "block";
					}
				}
				else 
				{
					dr["BarDisplay"] = "block";
					dr["PointDisplay"] = "none";
				}

				if ((dr["PStartDate"] == DBNull.Value) || (dr["PEndDate"] == DBNull.Value)
					|| (BLL.ConvertRule.ToDateString(dr["PStartDate"], "yyyy-MM-dd") == BLL.ConvertRule.ToDateString(dr["PEndDate"], "yyyy-MM-dd"))) 
				{
					dr["PBarDisplay"] = "none";

					if ((dr["PStartDate"] == DBNull.Value) && (dr["PEndDate"] == DBNull.Value)) 
					{
						dr["PPointDisplay"] = "none";
					}
					else 
					{
						dr["PPointDisplay"] = "block";
					}
				}
				else 
				{
					dr["PBarDisplay"] = "block";
					dr["PPointDisplay"] = "none";
				}

				//提示信息
				string hint = "形象进度：" + dr["VisualProgressName"].ToString() + "<br>"
					+ "计划开始日期：" + BLL.ConvertRule.ToDateString(dr["PStartDate"], "yyyy-MM-dd") + "<br>"
					+ "计划结束日期：" + BLL.ConvertRule.ToDateString(dr["PEndDate"], "yyyy-MM-dd") + "<br>";
				dr["PHint"] = hint;

				hint = "形象进度：" + dr["VisualProgressName"].ToString() + "<br>"
					+ "实际开始日期：" + BLL.ConvertRule.ToDateString(dr["StartDate"], "yyyy-MM-dd") + "<br>"
					+ "实际结束日期：" + BLL.ConvertRule.ToDateString(dr["EndDate"], "yyyy-MM-dd") + "<br>";
				dr["Hint"] = hint;

	
			}

			//图表宽度 = 总月数宽度
//			if (TotalMonth > 0) 
//			{
//				decimal TotalWidth = TotalMonth * MonthPixel + 300;
//				string s = Rms.Web.JavaScript.ScriptStart;
//				s += "document.all.tbList.width = '" + TotalWidth.ToString() + "px';";
//				s += Rms.Web.JavaScript.ScriptEnd;
//				Page.RegisterStartupScript("start", s);
//			}

			//x轴
			DataTable tbX = new DataTable("X");
			tbX.Columns.Add(new DataColumn("sno"));
			tbX.Columns.Add(new DataColumn("year"));
			tbX.Columns.Add(new DataColumn("month"));

			if (TotalMonth > 0) 
			{
				DateTime dateMin = DateTime.Parse(dMin.ToString());
				DateTime dateMax = DateTime.Parse(dMax.ToString());

				for(int i=0;i<TotalMonth;i++) 
				{
					DataRow dr = tbX.NewRow();
					dr["sno"] = i + 1;

					DateTime dCurr = dateMin.AddMonths(i);
					dr["year"] = dCurr.ToString("yyyy");
					dr["month"] = dCurr.Month.ToString();

					tbX.Rows.Add(dr);
				}
			}

			this.dgX.DataSource = tbX;
			this.dgX.DataBind();

			this.dgX2.DataSource = tbX;
			this.dgX2.DataBind();

			//x轴显示年份
			this.dgXYear.DataSource = BLL.ProgChartRule.GroupByYear(dMin, dMax);
			this.dgXYear.DataBind();

			return tb;
		}

		private void LoadChart(string AnnualPlanCode) 
		{
			try 
			{
				if (AnnualPlanCode != "") 
				{
					DataTable tb = GetDataTable(AnnualPlanCode);
					this.dgList.DataSource = tb;
					this.dgList.DataBind();
				}
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"显示图表失败");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示图表失败：" + ex.Message));
			}
		}
	}
}
