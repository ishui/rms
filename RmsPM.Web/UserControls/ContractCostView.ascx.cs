namespace RmsPM.Web.UserControls
{
	using System;
	using System.Collections;
	using System.ComponentModel;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using Rms.ORMap;
	using RmsPM.DAL;
	using RmsPM.BLL;
	using RmsPM.DAL.QueryStrategy;
	using AspWebControl;
	using Infragistics.WebUI.WebDataInput;

	/// <summary>
	///		ContractCostView 的摘要说明。
	/// </summary>
	public partial class ContractCostView : System.Web.UI.UserControl
	{
	
		public string Index
		{
			set { this.lblIndex.Text = value; }
			get { return this.lblIndex.Text; }
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
		}

		public void LoadData( EntityData entity, string contractCostCode)
		{
			try
			{
				//绑定费用项的ProjectCode
				string projectCode = Request["ProjectCode"] + "" ;
				string contractCode = Request["ContractCode"] + "" ;

				DataRow[] drSelects;

				//合同明细
				entity.SetCurrentTable("ContractCost");
				drSelects = entity.CurrentTable.Select( String.Format(" ContractCostCode='{0}'", contractCostCode),"",DataViewRowState.CurrentRows);
				if ( drSelects.Length > 0 )
				{
					decimal totalMoney = Decimal.Parse(drSelects[0]["Money"].ToString());
					lblTotalMoney.Text =  BLL.StringRule.BuildShowNumberString(totalMoney);
					lblDescription.Text = drSelects[0]["Description"].ToString();

					ucCostBudgetDtl.CostBudgetSetCode = drSelects[0]["CostBudgetSetCode"].ToString();
					ucCostBudgetDtl.CostCode = drSelects[0]["CostCode"].ToString();
					lblCostName.Text = ucCostBudgetDtl.CostName;
					lblPBSName.Text = ucCostBudgetDtl.PBSName;

					// 已付和未付款
					decimal ahMoney = BLL.CBSRule.GetAHMoney("","","",drSelects[0]["ContractCode"].ToString(),"1",contractCostCode);
					this.lblAHMoney.Text = BLL.StringRule.BuildShowNumberString( ahMoney);
					float per = totalMoney == Decimal.Zero ? 0:(float)(ahMoney/totalMoney);
					this.lblAHMoneyPer.Text = per.ToString("#0.00%");
//					this.lblUPMoney.Text = BLL.StringRule.BuildShowNumberString(totalMoney-ahMoney);

					decimal apMoney = BLL.CBSRule.GetAPMoney(contractCode,contractCostCode);
					this.lblAPMoney.Text = BLL.StringRule.BuildShowNumberString(apMoney);
					this.lblUPMoney.Text = BLL.StringRule.BuildShowNumberString(totalMoney-apMoney);




				}

				//付款计划
				entity.SetCurrentTable("ContractCostPlan");

				BindCostList(entity.CurrentTable,contractCostCode);

				entity.Dispose();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog( this.ToString(),ex,"加载合同款项明细数据错误");
				Response.Write(Rms.Web.JavaScript.Alert(true, "加载合同款项明细数据出错：" + ex.Message));
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
			this.dgCostList.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgCostList_ItemDataBound);

		}
		#endregion

		/// <summary>
		/// 显示合同明细
		/// </summary>
		private void BindCostList(DataTable tb,string costCode) 
		{
			try 
			{
				string sFilter = "ContractCostCode='" + costCode +"'";
				DataView dv = new DataView(tb, "", "", DataViewRowState.CurrentRows);

				dv.RowFilter = sFilter ;
				ViewState["SumMoney"] = BLL.MathRule.SumColumn(tb.Select(sFilter),"Money");

				this.dgCostList.DataSource = dv;
				this.dgCostList.DataBind();

//				((Label)dgCostList.Items[dgCostList.Items.Count+1].FindControl("lblSumMoney")).Text
//					= BLL.MathRule.GetDecimalShowString(ViewState["SumMoney"]);

			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示合同明细出错：" + ex.Message));
			}
		}

		private void dgCostList_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			
			if (e.Item.ItemType == ListItemType.Footer) 
			{
				//显示合计金额
				((Label)e.Item.FindControl("lblSumMoney")).Text = BLL.MathRule.GetDecimalShowString(ViewState["SumMoney"]);
			}
		}



	}
}
