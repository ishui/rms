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
	/// RptFinIOList ��ժҪ˵����
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

				//���
				int StartY = 0;
				int EndY = 0;
				BLL.CashFlowRule.GetCashFlowStartEnd(this.txtProjectCode.Value, ref StartY, ref EndY);
				this.ucCostBudgetSelectMonth.MonthStart = StartY.ToString();
				this.ucCostBudgetSelectMonth.MonthEnd = EndY.ToString();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
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
				//ȡ����
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

				//����
				this.lblMonthTypeName.Text = BLL.CashFlowRule.GetMonthTypeName(MonthType);

				//���չ��
				string html_title1 = "";
				string html_title2 = "";
				int MonthCount = 0;
				BLL.CashFlowRule.GenerateYearTitleHtml(MonthType, StartY, EndY, ref html_title1, ref html_title2, ref MonthCount);

				ViewState["html_title1"] = html_title1;
				ViewState["html_title2"] = html_title2;
				ViewState["MonthCount"] = MonthCount;

				//�����ֽ�����
				DataSet ds = BLL.CashFlowRule.GenerateCashFlowList(ProjectCode, StartY, EndY, MonthType, Source);

				//����
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

				//����
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

				//�ܼ�
				DataTable tbTotal = ds.Tables["CashFlowTotal"];
				this.dgListTotal.DataSource = tbTotal;
				this.dgListTotal.DataBind();
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ�������" + ex.Message));
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ��ȳ���" + ex.Message));
			}
		}

	}
}
