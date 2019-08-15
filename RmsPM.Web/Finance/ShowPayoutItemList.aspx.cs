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

namespace RmsPM.Web.Finance
{
	/// <summary>
	/// ShowPayoutItemList 的摘要说明。
	/// </summary>
	public partial class ShowPayoutItemList : PageBase
	{
		protected AspWebControl.Calendar dtbCheckDate1;
		protected AspWebControl.Calendar dtbCheckDate0;
		protected System.Web.UI.HtmlControls.HtmlSelect sltAccountant;
		protected System.Web.UI.HtmlControls.HtmlTable TableToolbar;
		protected AspWebControl.Calendar dtbPayoutDate0;
		protected AspWebControl.Calendar dtbPayoutDate1;
		protected AspWebControl.Calendar dtbInputDate0;
		protected AspWebControl.Calendar dtbInputDate1;
		protected System.Web.UI.HtmlControls.HtmlTable divAdvSearch;
		protected RmsPM.Web.UserControls.InputUser ucInputPerson;
		protected RmsPM.Web.UserControls.InputUser ucCheckPerson;
		protected RmsPM.Web.UserControls.InputSystemGroup inputSystemGroup ;

		public string ParamCostBudgetSetCode
		{
			get {return BLL.ConvertRule.ToString(Request.QueryString["CostBudgetSetCode"]);}
		}

		public string ParamCostCode
		{
			get {return BLL.ConvertRule.ToString(Request.QueryString["CostCode"]);}
		}

        public string ParamSubjectCode
        {
            get { return BLL.ConvertRule.ToString(Request.QueryString["SubjectCode"]); }
        }

        public string ParamContractCode
		{
			get {return BLL.ConvertRule.ToString(Request.QueryString["ContractCode"]);}
		}

		public string ParamPaymentCode
		{
			get {return BLL.ConvertRule.ToString(Request.QueryString["PaymentCode"]);}
		}

		public string ParamIsContract
		{
			get {return BLL.ConvertRule.ToString(Request.QueryString["IsContract"]);}
		}

        public string ParamPBSType
        {
            get { return BLL.ConvertRule.ToString(Request.QueryString["PBSType"]); }
        }

        public string ParamPBSCode
        {
            get { return BLL.ConvertRule.ToString(Request.QueryString["PBSCode"]); }
        }

        public string ParamPayoutDateBegin
        {
            get { return BLL.ConvertRule.ToString(Request.QueryString["PayoutDateBegin"]); }
        }

        public string ParamPayoutDateEnd
        {
            get { return BLL.ConvertRule.ToString(Request.QueryString["PayoutDateEnd"]); }
        }

        protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack)
			{
				IniPage();
				LoadDataGrid();
			}
		}

		private void IniPage()
		{
			try 
			{
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];

				LoadParamDesc();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

		/// <summary>
		/// 初始化参数描述
		/// </summary>
		private void LoadParamDesc()
		{
			try 
			{
				const string Sep = "<span style='width:20px'></span>";
				string desc = "";

				//预算表
				if (this.ParamCostBudgetSetCode != "")
				{
					desc += Sep + "预算表：" + BLL.CostBudgetRule.GetCostBudgetSetName(this.ParamCostBudgetSetCode);
				}

				//费用项
				if (this.ParamCostCode != "")
				{
					desc += Sep + "费用项：" + BLL.CBSRule.GetCostName(this.ParamCostCode);
				}

                //科目
                if (this.ParamSubjectCode != "")
                {
                    string SubjectSetCode = BLL.ProjectRule.GetSubjectSetCodeByProject(this.txtProjectCode.Value);
                    desc += Sep + "科目：" + BLL.SubjectRule.GetSubjectName(this.ParamSubjectCode, SubjectSetCode);
                }

                //合同
				if (this.ParamContractCode != "")
				{
					EntityData entity = DAL.EntityDAO.ContractDAO.GetContractByCode(ParamContractCode);
					if (entity.HasRecord()) 
					{
						desc += Sep + "合同名称：" + entity.GetString("ContractName");
					}
					entity.Dispose();
				}

				//非合同请款单
				if (this.ParamPaymentCode != "")
				{
					desc += Sep + "非合同请款单：" + ParamPaymentCode;
				}

				//非合同
				if (this.ParamIsContract == "0")
				{
					desc += Sep + "非合同";
				}

                //单位工程
                if ((this.ParamPBSType != "") || (this.ParamPBSCode != ""))
                {
                    desc += Sep + "单位工程：" + BLL.CostBudgetRule.GetPBSName(ParamPBSType, ParamPBSCode);
                }

                //付款日期
                if ((this.ParamPayoutDateBegin != "") || (this.ParamPayoutDateEnd != ""))
                {
                    desc += Sep + "付款日期：" + BLL.StringRule.GetDateRangeDesc(this.ParamPayoutDateBegin, this.ParamPayoutDateEnd);
                }

                this.lblParamDesc.Text = desc;
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化参数描述出错：" + ex.Message));
			}
		}

		private void LoadDataGrid()
		{
			try
			{
				PayoutItemStrategyBuilder sb = new PayoutItemStrategyBuilder("V_PayoutItem");
				sb.AddStrategy( new Strategy( PayoutItemStrategyName.ProjectCode,txtProjectCode.Value));

				if (this.ParamCostBudgetSetCode != "")
					sb.AddStrategy( new Strategy( PayoutItemStrategyName.CostBudgetSetCode, this.ParamCostBudgetSetCode));

				if (this.ParamCostCode != "")
					sb.AddStrategy( new Strategy( PayoutItemStrategyName.CostCodeIncludeAllChild, this.ParamCostCode));

                if (this.ParamSubjectCode != "")
                    sb.AddStrategy(new Strategy(PayoutItemStrategyName.SubjectCodeIncludeAllChild, this.ParamSubjectCode));

                if (this.ParamContractCode != "")
					sb.AddStrategy( new Strategy( PayoutItemStrategyName.ContractCode, this.ParamContractCode));

				if (this.ParamPaymentCode != "")
					sb.AddStrategy( new Strategy( PayoutItemStrategyName.PaymentCode, this.ParamPaymentCode));

				if (this.ParamIsContract != "")
					sb.AddStrategy( new Strategy( PayoutItemStrategyName.IsContract, this.ParamIsContract));

                if ((this.ParamPBSType != "") || (this.ParamPBSCode != ""))
                {
                    sb.AddStrategy(new Strategy(PayoutItemStrategyName.PBSTypeAndCode, this.ParamPBSType, this.ParamPBSCode));
                }

                if (this.ParamPayoutDateBegin != "" || this.ParamPayoutDateEnd != "")
                {
                    ArrayList ar = new ArrayList();
                    ar.Add(this.ParamPayoutDateBegin);
                    ar.Add(this.ParamPayoutDateEnd);
                    sb.AddStrategy(new Strategy(PayoutItemStrategyName.PayoutDateRange, ar));
                }

                //已审
                if (BLL.PaymentRule.IsPayoutMoneyIncludeNotCheck == 0)
                    sb.AddStrategy(new Strategy(PayoutItemStrategyName.Status, "1,2"));

                //排序
				string sortsql = BLL.GridSort.GetSortSQL(ViewState);
				if (sortsql == "")
				{
					//缺省排序
					sb.AddOrder( "PayoutDate" ,true);
					sb.AddOrder( "PayoutCode" ,true);
				}

				string sql = sb.BuildMainQueryString();

				if (sortsql != "")
				{
					//点列标题排序
					sql = sql + " order by " + sortsql;
				}

				QueryAgent qa = new QueryAgent();
				EntityData entity = qa.FillEntityData( "PayoutItem",sql );
				qa.Dispose();

				string[] arrField = {"PayoutMoney"};
				decimal[] arrSum = BLL.MathRule.SumColumn(entity.CurrentTable, arrField);
				ViewState["SumMoney"] = arrSum[0].ToString("N");
				this.dgList.DataSource = entity.CurrentTable;
				this.dgList.DataBind();

				this.GridPagination1.RowsCount = entity.CurrentTable.Rows.Count.ToString();

				entity.Dispose();

			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示列表出错：" + ex.Message));
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
			this.dgList.ItemCreated += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgList_ItemCreated);
			this.dgList.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.dgList_SortCommand);
			this.dgList.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgList_ItemDataBound);

		}
		#endregion

		private void dgList_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			try
			{
				BLL.GridSort.SortCommand((DataGrid)source, ViewState, source, e);
				((DataGrid)source).CurrentPageIndex = 0;
				LoadDataGrid();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

		private void dgList_ItemCreated(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			try
			{
				BLL.GridSort.ItemCreate((DataGrid)sender, ViewState, sender, e);
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}	

		protected void GridPagination1_PageIndexChange(object sender, System.EventArgs e)
		{
			try
			{
				this.LoadDataGrid();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

		private void btnSearch_ServerClick(object sender, System.EventArgs e)
		{
			this.dgList.CurrentPageIndex = 0;
			LoadDataGrid();
		}

		private void dgList_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Footer) 
			{
				//显示合计金额
				((Label)e.Item.FindControl("lblSumMoney")).Text = BLL.ConvertRule.ToString(ViewState["SumMoney"]);
			}
		}
	}
}
