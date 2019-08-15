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

namespace RmsPM.Web.ConstructProg
{
	/// <summary>
	/// ProjectProgressChartEx
	/// </summary>
	public partial class ProjectProgressChartEx : PageBase
	{

//		protected decimal MonthPixel = 50;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack) 
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
		///		�����֧������ķ��� - ��Ҫʹ�ô���༭��
		///		�޸Ĵ˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion

		private void IniPage() 
		{
			try
			{
				this.txtWBSCode.Value = Request.QueryString["WBSCode"];	
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"��ʼ��ҳ��ʧ��");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ��ʧ�ܣ�" + ex.Message));
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

					//��������
					decimal d = month * MonthPixel;
					left = d.ToString() + "px";

					//�ٷֱ�
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

					//��������
					decimal d = month * MonthPixel;
					left = d.ToString() + "px";

					//�ٷֱ�
//					decimal d = Math.Round(dMonth / TotalMonth, 2) * 100;
//					left = d.ToString() + "%";
				}
			}

			return left;
		}
*/

		private DataTable GetDataTable(string WBSCode) 
		{
			string StartField = "ActualStartDate";
			string EndField = "ActualFinishDate";
			string PStartField = "PlannedStartDate";
			string PEndField = "PlannedFinishDate";
			string TempEndField = "TempEndDate";

			//ȡ�������ȱ�
			DataTable tb = BLL.ConstructProgRule.GenerateProjectProgressChartTable(WBSCode);

			//��ʽ����ʼ����������
			BLL.ConstructProgRule.FormatProjectProgressDate(tb, StartField, EndField);

			//�ѿ�ʼ��δ����ʱ�������������
			BLL.ConstructProgRule.FormatProjectProgressEndDate(tb, StartField, EndField, TempEndField);

			//�����ֶΣ�ͼ��λ�á�����
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

			//����ʱ���
			object dMin = null;
			object dMax = null;

			//ѭ��ȡ�ƻ���ʵ�����ڵ���Сֵ�����ֵ��Ϊ���ڷ�Χ
			foreach(DataRow dr in tb.Rows) 
			{
				//ȡ��С���������
				dMin = BLL.ProgChartRule.GetMinDate(new object[]{dMin, dr[StartField], dr[TempEndField], dr[PStartField], dr[PEndField]});
				dMax = BLL.ProgChartRule.GetMaxDate(new object[]{dMax, dr[StartField], dr[TempEndField], dr[PStartField], dr[PEndField]});
			}

			//�������ڷ�Χ�ڵ�������
			int TotalMonth = BLL.ProgChartRule.GetMonthBetween(dMin, dMax) + 1;

			//����ÿ����¼��ͼ��λ�á�����
			foreach(DataRow dr in tb.Rows) 
			{
//				dr["PLeft"] = GetGraphLeft(dMin, dr[PStartField], TotalMonth);
//				dr["Left"] = GetGraphLeft(dMin, dr[PStartField], TotalMonth);
//
//				dr["PWidth"] = GetGraphWidth(dr[PStartField], dr[PEndField], TotalMonth);
//				dr["Width"] = GetGraphWidth(dr[PStartField], dr[TempEndField], TotalMonth);

				dr["LeftMargin"] = BLL.ProgChartRule.ToColSpan(BLL.ProgChartRule.GetMonthBetween(dMin, dr[StartField]) + 1);
				dr["Months"] = BLL.ProgChartRule.ToColSpan(BLL.ProgChartRule.GetMonthBetween(dr[StartField], dr[TempEndField]) + 1);
				dr["RightMargin"] = BLL.ProgChartRule.ToColSpan(TotalMonth - (int)dr["LeftMargin"] - (int)dr["Months"] + 1);

				dr["PLeftMargin"] = BLL.ProgChartRule.ToColSpan(BLL.ProgChartRule.GetMonthBetween(dMin, dr[PStartField]) + 1);
				dr["PMonths"] = BLL.ProgChartRule.ToColSpan(BLL.ProgChartRule.GetMonthBetween(dr[PStartField], dr[PEndField]) + 1);
				dr["PRightMargin"] = BLL.ProgChartRule.ToColSpan(TotalMonth - (int)dr["PLeftMargin"] - (int)dr["PMonths"] + 1);

				//��ʾ������ͼ����
				if ((dr[StartField] == DBNull.Value) || (dr[TempEndField] == DBNull.Value)
					|| (BLL.ConvertRule.ToDateString(dr[StartField], "yyyy-MM-dd") == BLL.ConvertRule.ToDateString(dr[TempEndField], "yyyy-MM-dd"))) 
				{
					dr["BarDisplay"] = "none";

					if ((dr[StartField] == DBNull.Value) && (dr[TempEndField] == DBNull.Value)) 
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

				if ((dr[PStartField] == DBNull.Value) || (dr[PEndField] == DBNull.Value)
					|| (BLL.ConvertRule.ToDateString(dr[PStartField], "yyyy-MM-dd") == BLL.ConvertRule.ToDateString(dr[PEndField], "yyyy-MM-dd"))) 
				{
					dr["PBarDisplay"] = "none";

					if ((dr[PStartField] == DBNull.Value) && (dr[PEndField] == DBNull.Value)) 
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

				//��ʾ��Ϣ
				string hint = "�������ƣ�" + dr["TaskName"].ToString()
					+ "<br>" + "״����̬��" + ComSource.GetTaskStatusName(BLL.ConvertRule.ToInt(dr["Status"]).ToString())
					+ "<br>" + "�ƻ���ʼ���ڣ�" + BLL.ConvertRule.ToDateString(dr[PStartField], "yyyy-MM-dd")
					+ "<br>" + "�ƻ��������ڣ�" + BLL.ConvertRule.ToDateString(dr[PEndField], "yyyy-MM-dd")
					+ "<br>" + "��ǰ���ȣ�" + BLL.ConvertRule.ToInt(dr["CompletePercent"]).ToString() + "%"
					;
				dr["PHint"] = hint;

				hint = "�������ƣ�" + dr["TaskName"].ToString()
					+ "<br>" + "״����̬��" + ComSource.GetTaskStatusName(BLL.ConvertRule.ToInt(dr["Status"]).ToString())
					+ "<br>" + "��ǰ���ȣ�" + BLL.ConvertRule.ToInt(dr["CompletePercent"]).ToString() + "%"
					+ "<br>" + "ʵ�ʿ�ʼ���ڣ�" + BLL.ConvertRule.ToDateString(dr[StartField], "yyyy-MM-dd")
					+ "<br>" + "ʵ�ʽ������ڣ�" + BLL.ConvertRule.ToDateString(dr[EndField], "yyyy-MM-dd")
					;
				dr["Hint"] = hint;

	
			}

			//ͼ���� = ���������
//			if (TotalMonth > 0) 
//			{
//				decimal TotalWidth = TotalMonth * MonthPixel + 300;
//				string s = Rms.Web.JavaScript.ScriptStart;
//				s += "document.all.tbList.width = '" + TotalWidth.ToString() + "px';";
//				s += Rms.Web.JavaScript.ScriptEnd;
//				Page.RegisterStartupScript("start", s);
//			}

			//x��
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

			//x����ʾ���
			this.dgXYear.DataSource = BLL.ProgChartRule.GroupByYear(dMin, dMax);
			this.dgXYear.DataBind();

			return tb;
		}

		private void LoadChart() 
		{
			try 
			{
				string WBSCode = this.txtWBSCode.Value;

				if (WBSCode != "") 
				{
					DataTable tb = GetDataTable(WBSCode);
					this.dgList.DataSource = tb;
					this.dgList.DataBind();
				}
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"��ʾͼ��ʧ��");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾͼ��ʧ�ܣ�" + ex.Message));
			}
		}
	}
}
