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
	///		ContractCostView ��ժҪ˵����
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
			// �ڴ˴������û������Գ�ʼ��ҳ��
		}

		public void LoadData( EntityData entity, string contractCostCode)
		{
			try
			{
				//�󶨷������ProjectCode
				string projectCode = Request["ProjectCode"] + "" ;
				string contractCode = Request["ContractCode"] + "" ;

				DataRow[] drSelects;

				//��ͬ��ϸ
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

					// �Ѹ���δ����
					decimal ahMoney = BLL.CBSRule.GetAHMoney("","","",drSelects[0]["ContractCode"].ToString(),"1",contractCostCode);
					this.lblAHMoney.Text = BLL.StringRule.BuildShowNumberString( ahMoney);
					float per = totalMoney == Decimal.Zero ? 0:(float)(ahMoney/totalMoney);
					this.lblAHMoneyPer.Text = per.ToString("#0.00%");
//					this.lblUPMoney.Text = BLL.StringRule.BuildShowNumberString(totalMoney-ahMoney);

					decimal apMoney = BLL.CBSRule.GetAPMoney(contractCode,contractCostCode);
					this.lblAPMoney.Text = BLL.StringRule.BuildShowNumberString(apMoney);
					this.lblUPMoney.Text = BLL.StringRule.BuildShowNumberString(totalMoney-apMoney);




				}

				//����ƻ�
				entity.SetCurrentTable("ContractCostPlan");

				BindCostList(entity.CurrentTable,contractCostCode);

				entity.Dispose();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog( this.ToString(),ex,"���غ�ͬ������ϸ���ݴ���");
				Response.Write(Rms.Web.JavaScript.Alert(true, "���غ�ͬ������ϸ���ݳ���" + ex.Message));
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
			this.dgCostList.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgCostList_ItemDataBound);

		}
		#endregion

		/// <summary>
		/// ��ʾ��ͬ��ϸ
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ��ͬ��ϸ����" + ex.Message));
			}
		}

		private void dgCostList_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			
			if (e.Item.ItemType == ListItemType.Footer) 
			{
				//��ʾ�ϼƽ��
				((Label)e.Item.FindControl("lblSumMoney")).Text = BLL.MathRule.GetDecimalShowString(ViewState["SumMoney"]);
			}
		}



	}
}
