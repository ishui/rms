namespace RmsPM.Web.UserControls
{
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
	using RmsPM.DAL;
	using RmsPM.BLL;
	using RmsPM.DAL.QueryStrategy;
	using AspWebControl;
	using Infragistics.WebUI.WebDataInput;
	using System.Text;

	/// <summary>
	///		ContractCostlist 的摘要说明。
	/// </summary>
	public partial class ContractCostModify : ControlBase
	{
		protected System.Web.UI.WebControls.Label lblCostMoney;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnCostMoney;




		public string ProjectCode
		{
			get
			{
				return Request["ProjectCode"] + "";
			}
		}

		public string ContractCode
		{
			set
			{ 
				hid_ContractCode.Text = value;
			}
			get
			{
				if ( hid_ContractCode.Text == "" )
				{
					EntityData entity = ReadEntitySession(); 

					entity.SetCurrentTable("Contract");

					hid_ContractCode.Text = entity.GetString("ContractCode");
				}

				return hid_ContractCode.Text;
			}
		}

		public string ContractCostCode
		{
			set
			{ 
				hid_ContractCostCode.Text = value;
			}
			get
			{
				return hid_ContractCostCode.Text;
			}
		}

		public int Index
		{
			set
			{ 
				lblIndex.Text = value.ToString();
			}
			get
			{
				if ( lblIndex.Text != "" )
				{
					return int.Parse(lblIndex.Text);
				}
				else
				{
					return 0;
				}
			}		
		}
	
		public bool CostReadOnly
		{
			set
			{ 
				hid_CostReadOnly.Checked = value;
			}
			get
			{
				return hid_CostReadOnly.Checked;
			}	
		}


		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack )
			{
				LoadData();
			}
			this.lblJS.Text = "";
		}

		private void InitControls()
		{
			try
			{
				string action = Request["Act"] + "" ;

				ucCostBudgetDtl.ProjectCode = this.ProjectCode;
				ucCostBudgetDtl.Enable = !this.CostReadOnly;

//				if ( action == "Bidding")
//				{
//					txtCash.Enabled = false;
//					ucCostBudgetDtl.Enable = false;
//				}
//				else
//				{
//					txtCash.Enabled = true;
//					ucCostBudgetDtl.Enable = true;
//				}
			}
			catch( Exception ex)
			{
				ApplicationLog.WriteLog( this.ToString(),ex,"初始化合同款项明细控件错误");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化合同款项明细控件错误：" + ex.Message));
				throw ex;
			}
		}


		public void LoadData()
		{
			try
			{
				this.InitControls();

				EntityData entity = ReadEntitySession();

				foreach(DataRow dr in entity.Tables["ContractCost"].Select(string.Format("ContractCostCode='{0}'",this.ContractCostCode),"",DataViewRowState.CurrentRows))
				{
					ucCostBudgetDtl.CostBudgetSetCode = dr["CostBudgetSetCode"].ToString();
					ucCostBudgetDtl.CostCode = dr["CostCode"].ToString();

					txtDescription.Text = dr["Description"].ToString();
				}

				this.BindCostCash();
				this.BindCostPlan();

			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog( this.ToString(),ex,"加载合同款项明细数据错误");
				Response.Write(Rms.Web.JavaScript.Alert(true, "加载合同款项明细数据出错：" + ex.Message));
			}

		}

		public void LoadCostMoney()
		{
			try
			{
				string msg = SaveToSession();

				if ( msg != "" )
				{
					Response.Write( Rms.Web.JavaScript.Alert(true,msg));
				}
				else
				{
					EntityData entity = ReadEntitySession();

					decimal ud_deMoney = BLL.MathRule.SumColumn(entity.Tables["ContractCostCash"].Select(string.Format("ContractCostCode = '{0}'",this.ContractCostCode),"",DataViewRowState.CurrentRows),"Money");

					lblCostMoney.Text = ud_deMoney.ToString("N");

					entity.Dispose();
				}
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "估计金额出错：" + ex.Message));
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

		override protected void InitEventHandler()
		{
			base.InitEventHandler();

			this.dgCostCash.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgCostCash_ItemDataBound);
			this.dgCostCash.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgCostCash_DeleteCommand);
			this.dgCostPlan.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgCostPlan_DeleteCommand);
		}

		private void BindCostCash()
		{
			EntityData entity = ReadEntitySession();

			string ud_sRowFilter = string.Format("ContractCostCode='{0}'",this.ContractCostCode);
			string ud_sRowSort = "";

			if ( entity.Tables["ContractCostCash"].Select(ud_sRowFilter,"",System.Data.DataViewRowState.CurrentRows).Length == 0 )
			{
				AddNewEmptyCostCashRow(entity,1);
				WriteEntitySession(entity);
			}

			ViewState["_" + this.ContractCostCode + "SumCashMoney"] = BLL.MathRule.SumColumn(entity.Tables["ContractCostCash"],"Money");

			DataView ud_dvCostCash = new DataView(entity.Tables["ContractCostCash"],ud_sRowFilter,ud_sRowSort,DataViewRowState.CurrentRows);

			dgCostCash.DataSource = ud_dvCostCash;
			dgCostCash.DataBind();
		}

		private void BindCostPlan()
		{

			EntityData entity = ReadEntitySession();

			string ud_sRowFilter = string.Format("ContractCostCode='{0}'",this.ContractCostCode);
			string ud_sRowSort = "";

			if ( entity.Tables["ContractCostPlan"].Select(ud_sRowFilter,"",System.Data.DataViewRowState.CurrentRows).Length == 0 )
			{
				AddNewEmptyCostPlanRow(entity,1);
				WriteEntitySession(entity);
			}

			DataView ud_dvCostPlan = new DataView(entity.Tables["ContractCostPlan"],ud_sRowFilter,ud_sRowSort,System.Data.DataViewRowState.CurrentRows);

			dgCostPlan.DataSource = ud_dvCostPlan;
			dgCostPlan.DataBind();
		}

		private void AddNewEmptyCostCashRow( EntityData entity ,int pm_iRows)
		{
			for ( int i=0;i<pm_iRows;i++ )
			{
				DataRow dr = entity.GetNewRecord("ContractCostCash");
				string code = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("ContractCostCashCode");

				dr["ContractCostCashCode"] = code;
				dr["ContractCode"] = this.ContractCode;
				dr["ContractCostCode"] = this.ContractCostCode;

				entity.AddNewRecord(dr,"ContractCostCash");
			}
		}

		private void AddNewEmptyCostPlanRow( EntityData entity ,int pm_iRows)
		{
			for ( int i=0;i<pm_iRows;i++ )
			{
				DataRow dr = entity.GetNewRecord("ContractCostPlan");
				string code = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("ContractCostPlanCode");

				dr["ContractCostPlanCode"] = code;
				dr["ContractCode"] = this.ContractCode;
				dr["ContractCostCode"] = this.ContractCostCode;

				entity.AddNewRecord(dr,"ContractCostPlan");
			}
		}



		/// <summary>
		/// 新增付款计划
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnNewCostItem_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				string msg = SaveToSession();

				if ( msg != "" )
				{
					Response.Write( Rms.Web.JavaScript.Alert(true,msg));
				}
				else
				{
					EntityData entity = ReadEntitySession();

                    //只新增一行 世茂 2007.1.29
                    AddNewEmptyCostPlanRow(entity, 1);

					WriteEntitySession(entity);

					entity.Dispose();
					BindCostPlan();
				}

			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "新增明细出错：" + ex.Message));
			}
		}

		/// <summary>
		/// 新增币种
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnNewCostCash_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				string msg = SaveToSession();

				if ( msg != "" )
				{
					Response.Write( Rms.Web.JavaScript.Alert(true,msg));
				}
				else
				{
					EntityData entity = ReadEntitySession();

                    //只新增一行 世茂 2007.1.29
					AddNewEmptyCostCashRow(entity,1);

					WriteEntitySession(entity);

					entity.Dispose();

					BindCostCash();
				}

			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "新增币种出错：" + ex.Message));
			}		
		}

		public string  SaveToSession()
		{

			string alertMsg = "";
			try
			{
				string projectCode = Request["ProjectCode"] + "";

				EntityData entity = ReadEntitySession();

				decimal ud_deCostMoney = Decimal.Zero;

				//保存币种金额
				foreach(DataGridItem ud_dgItem in dgCostCash.Items )
				{
					string contractCostCashCode = ud_dgItem.Cells[1].Text;

					foreach ( DataRow dr in entity.Tables["ContractCostCash"].Select(String.Format( "ContractCostCashCode='{0}'" ,contractCostCashCode )))
					{
						UserControls.ExchangeRateControl ud_ucExchangeRate = (UserControls.ExchangeRateControl)ud_dgItem.FindControl("ucExchangeRate");

						decimal ud_deCash = ud_ucExchangeRate.Cash;
						decimal ud_deMoney = ud_deCash * ud_ucExchangeRate.ExchangeRate;

						try
						{
							dr["amount"] = ud_ucExchangeRate.Amount;
						}
						catch { }
						try
						{
							dr["UnitPrise"] = ud_ucExchangeRate.UnitPrise;
						}
						catch { }

	
						dr["Cash"] = ud_deCash;
						dr["MoneyType"] = ud_ucExchangeRate.MoneyType;
						dr["ExchangeRate"] = ud_ucExchangeRate.ExchangeRate;
						dr["Money"] = ud_deMoney;

						ud_deCostMoney += ud_deMoney;
					}

				}

				//保存款项明细
				foreach ( DataRow dr in entity.Tables["ContractCost"].Select(String.Format( "ContractCostCode='{0}'" ,this.ContractCostCode )))
				{
					//try
					//{
					//    dr["amount"] = ucCostBudgetDtl.Amount;
					//}
					//catch { }
					//try
					//{
					//    dr["UnitPrise"] = ucCostBudgetDtl.UnitPrise;
					//}
					//catch { }
				
					dr["CostCode"] = ucCostBudgetDtl.CostCode;
					dr["CostBudgetSetCode"] = ucCostBudgetDtl.CostBudgetSetCode;
					dr["PBSType"] = ucCostBudgetDtl.PBSType;
					dr["PBSCode"] = ucCostBudgetDtl.PBSCode;
					dr["Description"] = txtDescription.Text.Trim();
					dr["Money"] = ud_deCostMoney;
				}

				//保存付款计划
				foreach(DataGridItem ud_dgItem in dgCostPlan.Items )
				{
					string contractCostPlanCode =  ud_dgItem.Cells[1].Text;
				
					string planningPayDate = ((AspWebControl.Calendar)ud_dgItem.FindControl("dtPlanningPayDate")).Value;
					string payConditionText =((HtmlInputText)ud_dgItem.FindControl("txtPayConditionText")).Value.Trim();
					WebNumericEdit txtMoney = (WebNumericEdit)ud_dgItem.FindControl("txtMoney");

					foreach ( DataRow dr in entity.Tables["ContractCostPlan"].Select(String.Format( "ContractCostPlanCode='{0}'"  ,contractCostPlanCode )))
					{

						dr["Money"] = txtMoney.ValueDecimal;
						dr["PayConditionText"] = payConditionText;
						if ( planningPayDate != "" )
						{
							dr["PlanningPayDate"] = planningPayDate;
						}
						else
						{
							dr["PlanningPayDate"] = System.DBNull.Value;
						}
					}

				}

				WriteEntitySession(entity);
				entity.Dispose();

				return alertMsg;
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "明细保存出错：" + ex.Message));
				throw ex;
			}
		}

		private void imbDelete_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			string contractCode = ContractCode;
			string action=Request.QueryString["Act"]+"";
			string projectCode = Request["ProjectCode"] + "";

			string codeTemp = ContractCostCode;

			SaveAllToSession();

			EntityData entity = ReadEntitySession();

			// 清除付款计划的记录
			foreach ( DataRow dr in entity.Tables["ContractCostPlan"].Select(String.Format("ContractCostCode={0}",ContractCostCode),"",DataViewRowState.CurrentRows  ))
			{
				dr.Delete();
			}

			// 清除付款计划的记录
			foreach ( DataRow dr in entity.Tables["ContractCostCash"].Select(String.Format("ContractCostCode={0}",ContractCostCode),"",DataViewRowState.CurrentRows  ))
			{
				dr.Delete();
			}

			// 清除合同款项明细的记录
			foreach ( DataRow dr in entity.Tables["ContractCost"].Select(String.Format("ContractCostCode={0}",ContractCostCode),"",DataViewRowState.CurrentRows  ))
			{
				dr.Delete();
			}

			WriteEntitySession(entity);

			BindCostList();
		}


		/// <summary>
		/// 删除币种明细
		/// </summary>
		/// <param name="source"></param>
		/// <param name="e"></param>
		private void dgCostCash_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try
			{
				string msg = SaveToSession();

				if ( msg != "" )
				{
					Response.Write( Rms.Web.JavaScript.Alert(true,msg));
				}
				else
				{
					string action=Request.QueryString["Act"]+"";
					string projectCode = Request["ProjectCode"] + "";

					string ud_sCashCode = e.Item.Cells[1].Text;

					EntityData entity = ReadEntitySession();

					//删除相关币种
					foreach ( DataRow dr in entity.Tables["ContractCostCash"].Select( String.Format("ContractCostCashCode='{0}'" ,ud_sCashCode ) ))
					{
						dr.Delete();
					}

					WriteEntitySession(entity);
					entity.Dispose();

					BindCostCash();
				}

			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "删除币种汇率出错：" + ex.Message));
			}
		}

		/// <summary>
		/// 删除付款计划明细
		/// </summary>
		/// <param name="source"></param>
		/// <param name="e"></param>
		private void dgCostPlan_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try
			{
				string msg = SaveToSession();

				if ( msg != "" )
				{
					Response.Write( Rms.Web.JavaScript.Alert(true,msg));
				}
				else
				{
					string action=Request.QueryString["Act"]+"";
					string projectCode = Request["ProjectCode"] + "";

					string ud_sPlanCode = e.Item.Cells[1].Text;

					EntityData entity = ReadEntitySession();

					//删除相关付款计划
					foreach ( DataRow dr in entity.Tables["ContractCostPlan"].Select( String.Format("ContractCostPlanCode='{0}'" ,ud_sPlanCode ) ))
					{
						dr.Delete();
					}

					WriteEntitySession(entity);
					entity.Dispose();

					BindCostPlan();
				}

			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "删除付款计划出错：" + ex.Message));
			}
		}


		private void dgCostCash_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:
					UserControls.ExchangeRateControl ud_ucExchangeRate = (UserControls.ExchangeRateControl)e.Item.FindControl("ucExchangeRate");

					DataRowView ud_drvItem = (DataRowView)e.Item.DataItem;
					if ( ud_drvItem["MoneyType"].ToString() != "" )
					{
						ud_ucExchangeRate.Amount = ud_drvItem["Amount"].ToString();
						ud_ucExchangeRate.UnitPrise = ud_drvItem["UnitPrise"].ToString();
						ud_ucExchangeRate.MoneyType = ud_drvItem["MoneyType"].ToString();
						ud_ucExchangeRate.ExchangeRate = ud_drvItem["ExchangeRate"] == DBNull.Value ? Decimal.Zero:Decimal.Parse(ud_drvItem["ExchangeRate"].ToString());
					}
					//try
					//{
					//    ud_ucExchangeRate.Amount = ud_drvItem["Amount"].ToString();
					//    ud_ucExchangeRate.UnitPrise = ud_drvItem["UnitPrise"].ToString();
					//}
					//catch { }
					
					ud_ucExchangeRate.Cash = ud_drvItem["Cash"] == DBNull.Value ? Decimal.Zero : Decimal.Parse(ud_drvItem["Cash"].ToString());
					ud_ucExchangeRate.IsShowTitle = false;
					ud_ucExchangeRate.EditMode = true;
					ud_ucExchangeRate.BindControl();

					break;
				case ListItemType.Footer:
					((Label)e.Item.FindControl("lblSumCashMoney")).Text = ((decimal)ViewState["_" + this.ContractCostCode + "SumCashMoney"]).ToString("N");
					break;
			}
		}

		protected void btnBuildPlan_ServerClick(object sender, System.EventArgs e)
		{
			this.SaveAllToSession();

			string ud_sScript;

			StringBuilder txtScript = new StringBuilder();

			ud_sScript  = Rms.Web.JavaScript.ScriptStart;
			ud_sScript += "BuildPlan('" + this.ContractCostCode + "');";
			ud_sScript += Rms.Web.JavaScript.ScriptEnd;

			txtScript.Append("\n");
			txtScript.Append(ud_sScript);

			this.Page.RegisterStartupScript( "",txtScript.ToString() );

		}


		public delegate void NoParameterEventHandler();
		public delegate string NoParameterStringEventHandler();
		public delegate int NoParameterIntEventHandler();
		public delegate void WriteEntitySessionEventHandler(EntityData entity);
		public delegate EntityData ReadEntitySessionEventHandler();

		public event NoParameterEventHandler BindCostList;
		public event NoParameterStringEventHandler SaveAllToSession;
		public event WriteEntitySessionEventHandler WriteEntitySession;
		public event ReadEntitySessionEventHandler ReadEntitySession;







	}
}
