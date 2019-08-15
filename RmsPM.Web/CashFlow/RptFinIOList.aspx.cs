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
using RmsDM.BLL;

namespace RmsPM.Web.CashFlow
{
	/// <summary>
	/// RptFinIOList 的摘要说明。
	/// </summary>
	public partial class RptFinIOList : System.Web.UI.Page
	{
	
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
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];

				//年度
				int StartY = 0;
				int EndY = 0;
				BLL.CashFlowRule.GetCashFlowStartEnd(this.txtProjectCode.Value, ref StartY, ref EndY);
				this.ucCostBudgetSelectMonth.MonthStart = StartY.ToString();
				this.ucCostBudgetSelectMonth.MonthEnd = EndY.ToString();
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

		/*
		public static DataTable GenerateRtpFinI(string ProjectCode)
		{
			try 
			{
				DataTable tb = BLL.SalBudgetRule.GetSalPBSType(false);

				tb.Columns.Add("PBSTypeNameHtml");

				foreach(DataRow dr in tb.Rows)
				{
					int Deep = BLL.ConvertRule.ToInt(dr["Deep"]);
					string PBSTypeName = BLL.ConvertRule.ToString(dr["PBSTypeName"]);

					string space = "";
					for(int i=1;i<Deep;i++)
					{
						space = space + "&nbsp;&nbsp;&nbsp;";
					}

					dr["PBSTypeNameHtml"] = space + PBSTypeName;
				}

				return tb;
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}
		*/

		/*
		public static DataTable GenerateRtpFinIO(string ProjectCode)
		{
			try 
			{
				//取数据
				QueryAgent qa = new Rms.ORMap.QueryAgent();
				try 
				{
					string sql = string.Format("select * from cbs where ProjectCode = '{0}' order by FullCode", ProjectCode);
					DataSet ds = qa.ExecSqlForDataSet(sql);
					DataTable tb = ds.Tables[0];

					tb.Columns.Add("CostNameHtml");

					foreach(DataRow dr in tb.Rows)
					{
						int Deep = BLL.ConvertRule.ToInt(dr["Deep"]);
						string CostName = BLL.ConvertRule.ToString(dr["CostName"]);

						string space = "";
						for(int i=1;i<Deep;i++)
						{
							space = space + "&nbsp;&nbsp;&nbsp;";
						}

						dr["CostNameHtml"] = space + CostName;
					}

					return tb;
				}
				finally 
				{
					qa.Dispose();
				}
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}
		*/

		private void LoadGrid()
		{
			try 
			{
				string ProjectCode = this.txtProjectCode.Value;
				string Source = BLL.ConvertRule.ToString(Request.QueryString["Source"]);
				string MonthType = BLL.ConvertRule.ToString(Request.QueryString["MonthType"]);

				BLL.CashFlowSource source = BLL.CashFlowRule.GetCashFlowSourceById(Source);
				string SourceDesc = (source != null)?source.Desc:"";
				this.lblSourceName.Text = SourceDesc;

				int StartY = BLL.ConvertRule.ToInt(this.ucCostBudgetSelectMonth.MonthStart);
				int EndY = BLL.ConvertRule.ToInt(this.ucCostBudgetSelectMonth.MonthEnd);

				//标题
				this.lblMonthTypeName.Text = BLL.CashFlowRule.GetMonthTypeName(MonthType);

				//年度展开
				string html_title1 = "";
				string html_title2 = "";
				int MonthCount = 0;
				BLL.CashFlowRule.GenerateYearTitleHtml(MonthType, StartY, EndY, ref html_title1, ref html_title2, ref MonthCount);

				ViewState["html_title1"] = html_title1;
				ViewState["html_title2"] = html_title2;
				ViewState["MonthCount"] = MonthCount;

				//生成现金流表
				DataSet ds = BLL.CashFlowRule.GenerateCashFlowList(ProjectCode, StartY, EndY, MonthType, Source);

				//流入
				DataTable tbI = ds.Tables["CashFlowI"];

				tbI.Columns.Add("PBSTypeNameHtml");

				foreach(DataRow dr in tbI.Rows)
				{
					int Deep = BLL.ConvertRule.ToInt(dr["Deep"]);
					string PBSTypeName = BLL.ConvertRule.ToString(dr["PBSTypeName"]);

					string space = "";
					for(int i=1;i<Deep;i++)
					{
						space = space + "&nbsp;&nbsp;&nbsp;";
					}

					dr["PBSTypeNameHtml"] = space + PBSTypeName;
				}

				DataView dvI = new DataView(tbI, "Deep > 0", "PBSTypeFullID", DataViewRowState.CurrentRows);
				this.dgListI.DataSource = dvI;
				this.dgListI.DataBind();

				DataView dvITotal = new DataView(tbI, "Deep = 0", "PBSTypeFullID", DataViewRowState.CurrentRows);
				this.dgListITotal.DataSource = dvITotal;
				this.dgListITotal.DataBind();

				//流出
				DataTable tbO = ds.Tables["CashFlowO"];

				tbO.Columns.Add("CostNameHtml");

				foreach(DataRow dr in tbO.Rows)
				{
					int Deep = BLL.ConvertRule.ToInt(dr["Deep"]);
					string CostName = BLL.ConvertRule.ToString(dr["CostName"]);

					string space = "";
					for(int i=1;i<Deep;i++)
					{
						space = space + "&nbsp;&nbsp;&nbsp;";
					}

					dr["CostNameHtml"] = space + CostName;
				}

				DataView dvO = new DataView(tbO, "Deep > 0", "CostFullID", DataViewRowState.CurrentRows);
				this.dgListO.DataSource = dvO;
				this.dgListO.DataBind();

				DataView dvOTotal = new DataView(tbO, "Deep = 0", "CostFullID", DataViewRowState.CurrentRows);
				this.dgListOTotal.DataSource = dvOTotal;
				this.dgListOTotal.DataBind();

				//总计
				DataTable tbTotal = ds.Tables["CashFlowTotal"];
				this.dgListTotal.DataSource = tbTotal;
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
				//				DateTime t1 = DateTime.Now;

				LoadGrid();

				/*
				DateTime t2 = DateTime.Now;
				TimeSpan t = t2.Subtract(t1);
				Response.Write(Rms.Web.JavaScript.Alert(true, t.Duration().ToString()));
				*/
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示年度出错：" + ex.Message));
			}
		}

	}
}
