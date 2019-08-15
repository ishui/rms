namespace RmsPM.Web.UserControls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using Rms.ORMap;

	/// <summary>
	///		ContractCostPlanView ��ժҪ˵����
	/// </summary>
	public partial class ContractCostPlanView : ControlBase
	{

		public bool ModifyPaymentPlan
		{
			get	{ return this.btnModifyPaymentPlan.Visible; }
			set { this.btnModifyPaymentPlan.Visible = value; }
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��

			if (!IsPostBack)
			{
				this.InitControl();
			}
			
		}

		public void InitControl()
		{
			this.LoadData();
		}

		private void LoadData()
		{
			string ud_sProjectCode = Request["ProjectCode"] + "";
			string ud_sContractCode = Request["ContractCode"] + "";

			EntityData entity = DAL.EntityDAO.ContractDAO.GetContractCostPlanIncludeCostCodeByCode(ud_sContractCode);

			this.BindCostPlanDataGrid(entity);
		}

		private void BindCostPlanDataGrid( EntityData entity )
		{
			DataView ud_dvCostPlan = new DataView(entity.Tables["ContractCostPlan"],"","CostCode",DataViewRowState.CurrentRows);

			ViewState["_SumMoney"] = BLL.MathRule.SumColumn(entity.Tables["ContractCostPlan"],"Money");

			dgCostPlanList.DataSource = ud_dvCostPlan;
			dgCostPlanList.DataBind();
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

		override protected void InitEventHandler()
		{
			base.InitEventHandler();
			this.dgCostPlanList.ItemDataBound += new DataGridItemEventHandler(this.dgCostPlanList_ItemDataBound);

		}

		private void dgCostPlanList_ItemDataBound( object sender,DataGridItemEventArgs e )
		{
			switch ( e.Item.ItemType )
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:
					UserControls.InputCostBudgetDtl ud_ucCostBudgetDtl = (UserControls.InputCostBudgetDtl)e.Item.FindControl("ucCostBudgetDtl");
					Label ud_lblPBSName = (Label)e.Item.FindControl("lblPBSName");
					Label ud_lblCostName = (Label)e.Item.FindControl("lblCostName");

					DataRowView ud_drvItem = (DataRowView)e.Item.DataItem;

					ud_ucCostBudgetDtl.CostBudgetSetCode = ud_drvItem["CostBudgetSetCode"].ToString();
					ud_ucCostBudgetDtl.CostCode = ud_drvItem["CostCode"].ToString();

					ud_lblCostName.Text = ud_ucCostBudgetDtl.CostName;
					ud_lblPBSName.Text = ud_ucCostBudgetDtl.PBSName;
					break;
				case ListItemType.Footer:
					Label ud_lblSumMoney = (Label)e.Item.FindControl("lblSumMoney");

					decimal ud_deSumMoney = ViewState["_SumMoney"] == null ? Decimal.Zero : (decimal)ViewState["_SumMoney"];

					ud_lblSumMoney.Text = ud_deSumMoney.ToString("N");
					break;
			}
		}
	}
}
