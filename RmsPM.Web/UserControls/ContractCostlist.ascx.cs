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

	/// <summary>
	///		ContractCostlist 的摘要说明。
	/// </summary>
	public partial class ContractCostlist : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label hid_Money;


		public Boolean CostEnable 
		{
			set
			{ 
				this.hid_CostEnable.Checked = value;
				this.Visible = this.hid_CostEnable.Checked;
			}
			get { return this.hid_CostEnable.Checked; }
		}
		public string ContractCode
		{
			set	{ this.hid_ContractCode.Text = value;}
			get	{ return this.hid_ContractCode.Text;}	
		}
		public string ContractCostCode
		{
			set	{ this.hid_ContractCostCode.Text = value;}
			get	{ return this.hid_ContractCostCode.Text;}	
		}
		public string Index
		{
			set { this.lblIndex.Text = value; }
			get { return this.lblIndex.Text; }
		}
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
			LoadData();
		}

		public void LoadData()
		{
			try
			{
				if ( !hid_CostEnable.Checked )
				{
					return;
				}

				string action = Request["Act"] + "" ;

				//绑定费用项的ProjectCode
				string projectCode = Request["ProjectCode"] + "" ;
				ucCostBudgetDtl.ProjectCode = projectCode;

				EntityData entity = ReadEntitySession();
				lblIndex.Text = LoadIndex();

				//合同明细
				entity.SetCurrentTable("ContractCost");
				DataRow[] drSelects = entity.CurrentTable.Select( String.Format(" ContractCostCode='{0}'", ContractCostCode),"",DataViewRowState.CurrentRows);
				if ( drSelects.Length > 0 )
				{
					if ( drSelects[0]["ContractCostLabel"].ToString().Trim() != "" )
					{
						ucCostBudgetDtl.Enable = false;
						txtTotalMoney.Enabled = false;
					}
					else
					{
						ucCostBudgetDtl.Enable = true;
						txtTotalMoney.Enabled = true;
					}
					ucCostBudgetDtl.CostBudgetSetCode = drSelects[0]["CostBudgetSetCode"].ToString();
					ucCostBudgetDtl.CostCode = drSelects[0]["CostCode"].ToString();
					txtTotalMoney.Value = drSelects[0]["Money"].ToString();
					txtDescription.Text = drSelects[0]["Description"].ToString();

				}
				else
				{

				}

				//付款计划
				entity.SetCurrentTable("ContractCostPlan");

				BindCostList(entity.CurrentTable,ContractCostCode);

				if ( action == "Bidding")
				{
					txtTotalMoney.Enabled = false;
					ucCostBudgetDtl.Enable = false;
				}
				else
				{
					txtTotalMoney.Enabled = true;
					ucCostBudgetDtl.Enable = true;
				}

				WriteEntitySession(entity);
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
			this.imbDelete.Click += new System.Web.UI.ImageClickEventHandler(this.imbDelete_Click);
			this.dgCostList.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgCostList_DeleteCommand);
			this.dgCostList.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgCostList_ItemDataBound);

		}
		#endregion

		private void AddNewEmptyPlanRow( EntityData entity , string contractCode, string contractCostCode, string tableName, string keyColumnName, int rows  )
		{
			for ( int i=0;i<rows;i++)
			{
				DataRow dr = entity.GetNewRecord(tableName);
				dr[keyColumnName]=DAL.EntityDAO.SystemManageDAO.GetNewSysCode(keyColumnName);
				dr["ContractCode"]=contractCode;
				dr["ContractCostCode"]=contractCostCode;

				entity.AddNewRecord(dr,tableName);
			}
		}

		/// <summary>
		/// 显示合同明细
		/// </summary>
		private void BindCostList(DataTable tb,string contractCostCode) 
		{
			try 
			{
				string sFilter = "ContractCostCode='" + contractCostCode +"'";
				DataView dv = new DataView(tb, "", "", DataViewRowState.CurrentRows);

				dv.RowFilter = sFilter ;
				ViewState["SumMoney"] = BLL.MathRule.SumColumn(tb.Select(sFilter),"Money");


				this.dgCostList.DataSource = dv;
				this.dgCostList.DataBind();

				int iCount = this.dgCostList.Items.Count;

				for (int i=0;i<iCount;i++)
				{
					((WebNumericEdit)this.dgCostList.Items[i].FindControl("txtMoney")).ClientSideEvents.ValueChange 
						= "InfraMoneyValueChange" + ClientID.ToString();
				}

			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示合同明细出错：" + ex.Message));
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
				EntityData entity = ReadEntitySession();
				AddNewEmptyPlanRow(entity,ContractCode,ContractCostCode,"ContractCostPlan","ContractCostPlanCode",5);

				BindCostList(entity.Tables["ContractCostPlan"],ContractCostCode);

				WriteEntitySession(entity);
				entity.Dispose();
				if ( msg != "" )
				{
					Response.Write( Rms.Web.JavaScript.Alert(true,msg));
				}
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "新增明细出错：" + ex.Message));
			}
		}

		public string  SaveToSession()
		{

			string alertMsg = "";
			try
			{
				string contractCode = ContractCode;
				string projectCode = Request["ProjectCode"] + "";
				

				EntityData entity = ReadEntitySession();

				entity.SetCurrentTable("ContractCost");

				foreach ( DataRow dr in entity.Tables["ContractCost"].Select(String.Format( "ContractCostCode='{0}'"  ,ContractCostCode )))
				{
					dr["CostCode"] = ucCostBudgetDtl.CostCode;
					dr["CostBudgetSetCode"] = ucCostBudgetDtl.CostBudgetSetCode;
					dr["PBSType"] = ucCostBudgetDtl.PBSType;
					dr["PBSCode"] = ucCostBudgetDtl.PBSCode;
					dr["Money"] = txtTotalMoney.ValueDecimal;
					dr["Description"] = txtDescription.Text.Trim();
				}

				foreach ( DataGridItem li in this.dgCostList.Items)
				{
					string planningPayDate = ((AspWebControl.Calendar)li.FindControl("dtPlanningPayDate")).Value;
					string payConditionText =((HtmlInputText)li.FindControl("txtPayConditionText")).Value.Trim();
						//((TextBox)li.FindControl("txtPayConditionText")).Text.Trim();

					WebNumericEdit txtMoney = (WebNumericEdit)li.FindControl("txtMoney");


					string contractCostPlanCode = li.Cells[0].Text;
					foreach ( DataRow dr in entity.Tables["ContractCostPlan"].Select(String.Format( "ContractCostPlanCode='{0}'"  ,contractCostPlanCode )))
					{

						dr["Money"] = txtMoney.ValueDecimal;
						dr["PayConditionText"] = payConditionText;
						if ( planningPayDate != "" )
							dr["PlanningPayDate"] = planningPayDate;
						else
							dr["PlanningPayDate"] = System.DBNull.Value;
					}
				}

				WriteEntitySession(entity);
				entity.Dispose();
				
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "明细保存出错：" + ex.Message));
			}
			return alertMsg;

		}


		/// <summary>
		/// 删除付款计划明细
		/// </summary>
		/// <param name="source"></param>
		/// <param name="e"></param>
		private void dgCostList_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try
			{
				string msg = SaveToSession();

				string contractCode = ContractCode;
				string action=Request.QueryString["Act"]+"";
				string projectCode = Request["ProjectCode"] + "";

				string codeTemp = ContractCostCode;
				string codeplanTemp = e.Item.Cells[0].Text;

				EntityData entity = ReadEntitySession();

				//删除相关付款条件
				foreach ( DataRow dr in entity.Tables["ContractCostPlan"].Select( String.Format("ContractCostPlanCode='{0}'" ,codeplanTemp ) ))
					dr.Delete();

				this.dgCostList.DataSource=new DataView( entity.Tables["ContractCostPlan"] ,"" , "",DataViewRowState.CurrentRows) ;
				this.dgCostList.DataBind();

				WriteEntitySession(entity);
				entity.Dispose();

				if ( msg != "" )
				{
					Response.Write( Rms.Web.JavaScript.Alert(true,msg));
				}

			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "删除款项明细出错：" + ex.Message));
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

		private void imbDelete_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			string contractCode = ContractCode;
			string action=Request.QueryString["Act"]+"";
			string projectCode = Request["ProjectCode"] + "";

			string codeTemp = ContractCostCode;

			SaveAllToSession();

//			// 检查是否有关该费用项的请款付款
//			// 请款单明细, 统计费用分解中各个款项的付款请款
//			PaymentItemStrategyBuilder sb = new PaymentItemStrategyBuilder();
//			sb.AddStrategy( new Strategy( PaymentItemStrategyName.ContractCode,contractCode  ) );
//			sb.AddStrategy( new Strategy( PaymentItemStrategyName.AllocateCode,codeTemp  ) );
//			string sql = sb.BuildQueryViewString();
//			QueryAgent qa = new QueryAgent();
//			EntityData paymentItem = qa.FillEntityData("V_PaymentItem",sql);
//			qa.Dispose();
//			bool isHas =( paymentItem.HasRecord());
//			paymentItem.Dispose();
//			if ( isHas )
//			{
//				Response.Write( Rms.Web.JavaScript.Alert(true,"该款项已经处于申请中或已经支付过，不能删除 ！") );
//				return;
//			}

			EntityData entity = ReadEntitySession();

			// 检查款项明细是否为变更状态

			DataRow[] drSelects =  entity.Tables["ContractCost"].Select(String.Format("ContractCostCode={0}",ContractCostCode));

			string contractCostLabel = drSelects[0]["ContractCostLabel"].ToString().Trim();
			
			if ( ContractCostCode == contractCostLabel )
			{
				// 款项明细为新增状态

				// 清除付款计划的记录
				foreach ( DataRow dr in entity.Tables["ContractCostPlan"].Select(String.Format("ContractCostCode={0}",ContractCostCode),"",DataViewRowState.CurrentRows  ))
				{
					dr.Delete();
				}

				// 清除合同款项明细的记录（并不清除，只是做一个标记）
				foreach ( DataRow dr in entity.Tables["ContractCost"].Select(String.Format("ContractCostCode={0}",ContractCostCode),"",DataViewRowState.CurrentRows  ))
				{
					dr["CostEnable"] = "false";
	//				dr.Delete();
				}
				this.CostEnable = false;
			}
			else
			{
				// 款项明细为变更状态

				// 清除付款计划的记录
				foreach ( DataRow dr in entity.Tables["ContractCostPlan"].Select(String.Format("ContractCostCode={0}",ContractCostCode),"",DataViewRowState.CurrentRows  ))
				{
					dr.Delete();
				}

				// 清除合同款项明细的记录（并不清除，只是将金额改为 0）
				foreach ( DataRow dr in entity.Tables["ContractCost"].Select(String.Format("ContractCostCode={0}",ContractCostCode),"",DataViewRowState.CurrentRows  ))
				{
					dr["Money"] = 0m;
					txtTotalMoney.Value = "0";
				}				
			}

			WriteEntitySession(entity);

			LoadAllIndex();
		}

		public delegate void NoParameterEventHandler();
		public delegate string NoParameterStringEventHandler();
		public delegate int NoParameterIntEventHandler();
		public delegate void WriteEntitySessionEventHandler(EntityData entity);
		public delegate EntityData ReadEntitySessionEventHandler();

		public event NoParameterStringEventHandler LoadIndex;
		public event NoParameterEventHandler LoadAllIndex;
		public event NoParameterStringEventHandler SaveAllToSession;
		public event WriteEntitySessionEventHandler WriteEntitySession;
		public event ReadEntitySessionEventHandler ReadEntitySession;







	}
}
