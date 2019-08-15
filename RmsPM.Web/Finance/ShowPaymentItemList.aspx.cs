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
	/// ShowPaymentItemList 的摘要说明。
	/// </summary>
	public partial class ShowPaymentItemList : PageBase
	{
		protected AspWebControl.Calendar dtbPayDate1;
		protected AspWebControl.Calendar dtbPayDate0;
		protected AspWebControl.Calendar dtbCheckDate1;
		protected AspWebControl.Calendar dtbCheckDate0;
		protected AspWebControl.Calendar dtbApplyDate1;
		protected AspWebControl.Calendar dtbApplyDate0;
		protected System.Web.UI.HtmlControls.HtmlInputText txtVoucherID;
		protected System.Web.UI.HtmlControls.HtmlSelect sltAccountant;
		protected System.Web.UI.HtmlControls.HtmlGenericControl spanTitle;
		protected System.Web.UI.HtmlControls.HtmlTable TableToolbar;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnAdd;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnPayout;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtSumPayoutMoney;
		protected System.Web.UI.HtmlControls.HtmlTable divAdvSearch1;
		protected RmsPM.Web.UserControls.InputUser ucApplyPerson;
		protected RmsPM.Web.UserControls.InputUser ucCheckPerson;
		protected RmsPM.Web.UserControls.InputUnit ucUnit;
		protected RmsPM.Web.UserControls.InputSystemGroup inputSystemGroupPayment;
	
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

		public string ParamIsPayout
		{
			get {return BLL.ConvertRule.ToString(Request.QueryString["IsPayout"]);}
		}

		public string ParamPBSType
		{
			get {return BLL.ConvertRule.ToString(Request.QueryString["PBSType"]);}
		}

		public string ParamPBSCode
		{
			get {return BLL.ConvertRule.ToString(Request.QueryString["PBSCode"]);}
		}

        public string ParamPayDateBegin
        {
            get { return BLL.ConvertRule.ToString(Request.QueryString["PayDateBegin"]); }
        }

        public string ParamPayDateEnd
        {
            get { return BLL.ConvertRule.ToString(Request.QueryString["PayDateEnd"]); }
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

				//是否已付
				switch (this.ParamIsPayout)
				{
					case "0":
						desc += Sep + "未付";
						break;

					case "1":
						desc += Sep + "部分已付";
						break;

					case "2":
						desc += Sep + "已付清";
						break;

					case "1,2":
						desc += Sep + "已付";
						break;

					case "0,1":
						desc += Sep + "未付清";
						break;
				}

				//单位工程
				if ((this.ParamPBSType != "") || (this.ParamPBSCode != ""))
				{
					desc += Sep + "单位工程：" + BLL.CostBudgetRule.GetPBSName(ParamPBSType, ParamPBSCode);
				}

                //请款日期
                if ((this.ParamPayDateBegin != "") || (this.ParamPayDateEnd != ""))
                {
                    desc += Sep + "请款日期：" + BLL.StringRule.GetDateRangeDesc(this.ParamPayDateBegin, this.ParamPayDateEnd);
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
				PaymentItemStrategyBuilder sb = new PaymentItemStrategyBuilder();
				sb.AddStrategy( new Strategy( PaymentItemStrategyName.ProjectCode,txtProjectCode.Value));

				if (this.ParamCostBudgetSetCode != "")
					sb.AddStrategy( new Strategy( PaymentItemStrategyName.CostBudgetSetCode, this.ParamCostBudgetSetCode));

				if (this.ParamCostCode != "")
					sb.AddStrategy( new Strategy( PaymentItemStrategyName.CostCodeIncludeAllChild, this.ParamCostCode));

                if (this.ParamSubjectCode != "")
                    sb.AddStrategy(new Strategy(PaymentItemStrategyName.SubjectCodeIncludeAllChild, this.ParamSubjectCode));

                if (this.ParamContractCode != "")
					sb.AddStrategy( new Strategy( PaymentItemStrategyName.ContractCode, this.ParamContractCode));

				if (this.ParamPaymentCode != "")
					sb.AddStrategy( new Strategy( PaymentItemStrategyName.PaymentCode, this.ParamPaymentCode));

				if (this.ParamIsContract != "")
					sb.AddStrategy( new Strategy( PaymentItemStrategyName.IsContract, this.ParamIsContract));

				if (this.ParamIsPayout != "")
					sb.AddStrategy( new Strategy( PaymentItemStrategyName.IsPayout, this.ParamIsPayout));

				if ((this.ParamPBSType != "") || (this.ParamPBSCode != ""))
				{
					sb.AddStrategy( new Strategy( PaymentItemStrategyName.PBSType, this.ParamPBSType));
					sb.AddStrategy( new Strategy( PaymentItemStrategyName.PBSCode, this.ParamPBSCode));
				}

                if (this.ParamPayDateBegin != "" || this.ParamPayDateEnd != "")
                {
                    ArrayList ar = new ArrayList();
                    ar.Add(this.ParamPayDateBegin);
                    ar.Add(this.ParamPayDateEnd);
                    sb.AddStrategy(new Strategy(PaymentItemStrategyName.PayDate, ar));
                }
                
                //已审
				sb.AddStrategy( new Strategy( PaymentItemStrategyName.Status, "1,2"));

				//排序
				string sortsql = BLL.GridSort.GetSortSQL(ViewState);
				if (sortsql == "")
				{
					//缺省排序
					sb.AddOrder( "CheckDate" ,true);
					sb.AddOrder( "PaymentCode" ,true);
				}

				string sql = sb.BuildQueryViewString();

				if (sortsql != "")
				{
					//点列标题排序
					sql = sql + " order by " + sortsql;
				}

				QueryAgent qa = new QueryAgent();
				EntityData entity = qa.FillEntityData( "PaymentItem",sql );
				qa.Dispose();

				//加已付金额
				if (!entity.CurrentTable.Columns.Contains("ItemPayoutMoney"))
				{
                    entity.CurrentTable.Columns.Add("ItemPayoutMoney", typeof(decimal));
					foreach(DataRow dr in entity.CurrentTable.Rows) 
					{
                        dr["ItemPayoutMoney"] = BLL.PaymentRule.GetPayoutMoneyByPaymentItem(dr["PaymentItemCode"]);
					}
				}

                string[] arrField = { "ItemMoney", "ItemPayoutMoney" };
				decimal[] arrSum = BLL.MathRule.SumColumn(entity.CurrentTable, arrField);
				ViewState["SumMoney"] = arrSum[0].ToString("N");
				ViewState["SumTotalPayoutMoney"] = arrSum[1].ToString("N");
				ViewState["SumTotalPayoutBalance"] = (arrSum[0] - arrSum[1]).ToString("N");
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
				((Label)e.Item.FindControl("lblSumTotalPayoutMoney")).Text = BLL.ConvertRule.ToString(ViewState["SumTotalPayoutMoney"]);
				((Label)e.Item.FindControl("lblSumTotalPayoutBalance")).Text = BLL.ConvertRule.ToString(ViewState["SumTotalPayoutBalance"]);
			}
		}
	}
}
