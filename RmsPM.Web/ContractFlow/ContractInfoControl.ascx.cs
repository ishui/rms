namespace RmsPM.Web.ContractFlow
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Collections;

	using Rms.ORMap;
	using RmsPM.DAL;
	using RmsPM.BLL;
	using RmsPM.DAL.QueryStrategy;
	using RmsPM.Web.WorkFlowControl;

	/// *******************************************************************************************
	/// <summary>
	///		ContractInfoControl 的摘要说明。合同基本信息显示组件
	/// </summary>
	/// *******************************************************************************************
	public partial class ContractInfoControl : ContractControlBase
	{

		private string _ProjectCode = "";
		private string _ContractCode = "";
		private string _UserCode = "";
		/// <summary>
		/// 合同代码
		/// </summary>
		public string ContractCode
		{
			get
			{
				if ( _ContractCode == "" )
				{
					if(this.ViewState["_ContractCode"] != null)
						return this.ViewState["_ContractCode"].ToString();
					return "";
				}
				return _ContractCode;
			}
			set
			{
				_ContractCode = value;
				this.ViewState["_ContractCode"] = value;
			}
		}
		
		/// <summary>
		/// 项目代码
		/// </summary>
		public string ProjectCode
		{
			get
			{
				if ( _ProjectCode == "" )
				{
					if(this.ViewState["_ProjectCode"] != null)
						return this.ViewState["_ProjectCode"].ToString();
					return "";
				}
				return _ProjectCode;
			}
			set
			{
				_ProjectCode = value;
				this.ViewState["_ProjectCode"] = value;
			}
		}
		/// <summary>
		/// 当前用户
		/// </summary>
		public string UserCode
		{
			get
			{
				if ( _UserCode == null )
				{
					if(this.ViewState["_UserCode"] != null)
						return this.ViewState["_UserCode"].ToString();
					return "";
				}
				return _UserCode;
			}
			set
			{
				_UserCode = value;
				this.ViewState["_UserCode"] = value;
			}
		}


		/// ****************************************************************************
		/// <summary>
		/// 页面加载
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// ****************************************************************************
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.myAttachMentList.AttachMentType = "ContractAttachMent";
			this.myAttachMentList.MasterCode = ContractCode;

		}


		/// ****************************************************************************
		/// <summary>
		/// 初始化
		/// </summary>
		/// ****************************************************************************
		public void InitControl()
		{

			/*User user = new User(UserCode);

			ArrayList ar = user.GetResourceRight(ContractCode,"Contract");
			if ( ! ar.Contains("050101"))
			{
				this.Page.Response.Redirect( "../RejectAccess.aspx" );
				Response.End();
			}
			if ( !ar.Contains("050103"))
			{
				this.btnModifyPaymentPlan.Visible = false;
			}*/

			if(this.State == ModuleState.Sightless)//不可见的
			{
				this.Visible = false;
			}
			else if(this.State == ModuleState.Operable)//可操作的
			{
				LoadData();
			}
			else if(this.State == ModuleState.Eyeable)//可见的
			{
				LoadData();
			}
			else if(this.State == ModuleState.Begin)//可见的
			{
				LoadData();
			}
			else if(this.State == ModuleState.End)//可见的
			{
				LoadData();
			}
			else
			{
				this.Visible = false;
			}
		}

		/// <summary>
		/// 数据加载
		/// </summary>
		private void LoadData()
		{
			// 当前版本的合同号
			string curContractCode = BLL.ContractRule.GetCurrentContractVersionCode(ContractCode);
			this.ViewState.Add("CurrentContractCode",curContractCode);
			try
			{

				EntityData entity = DAL.EntityDAO.ContractDAO.GetStandard_ContractByCode(ContractCode);
				decimal totalMoney = decimal.Zero;
				if ( entity.HasRecord())
				{
					/************************ 采购单 *************************/
					DAL.QueryStrategy.PurchaseFlowStrategyBuilder sbp = new DAL.QueryStrategy.PurchaseFlowStrategyBuilder();
					sbp.AddStrategy( new Strategy( DAL.QueryStrategy.PurchaseFlowStrategyName.ContractCode,entity.GetString("ContractCode")));
	
					string sqlp = sbp.BuildMainQueryString();
				
					QueryAgent QAp = new QueryAgent();
					EntityData entityp = QAp.FillEntityData("PurchaseFlow",sqlp);
					string PurchaseName = "";
					string PurchaseCode = "";
					if(entityp.HasRecord())
					{
						PurchaseName = entityp.CurrentRow["Purpose"].ToString();
						PurchaseCode = entityp.CurrentRow["PurchaseFlowCode"].ToString();
					}
					QAp.Dispose();
					entityp.Dispose();
					if(PurchaseCode!="")
					{
						this.PurchaseTr.Visible = true;
						this.PurchaseLink.InnerHtml = "<a href=\"javascript:OpenFullWindow('../Purchase/PurchaseManage.aspx?frameType=List&action=View&applicationCode="+PurchaseCode+"','采购单');return false;\">"+PurchaseName+"</a>";
					}
					else
					{
						this.PurchaseTr.Visible = false;
					}
					/*********************************************************/
					this.ProjectCode = entity.GetString("ProjectCode");
					this.LabelContractID.Text = entity.GetString("ContractID");
					this.LabelType.Text = BLL.ContractRule.GetContractTypeName(entity.GetString("Type"));
					this.LabelRemark.Text = entity.GetString("Remark");
					this.LabelContractName.Text = entity.GetString("ContractName");

					this.LabelContractDate.Text = entity.GetDateTimeOnlyDate("ContractDate");
					this.LabelContractPerson.Text = BLL.SystemRule.GetUserName(entity.GetString("ContractPerson"));

					this.SupplierSpan.InnerHtml="<A href=\"javascript:doViewSupplier('"+entity.GetString("SupplierCode")+"');\">"+BLL.ProjectRule.GetSupplierName(entity.GetString("SupplierCode"))+"</A>";
					totalMoney = entity.GetDecimal("TotalMoney");
					this.LabelTotalMoney.Text = BLL.StringRule.BuildShowNumberString(totalMoney);

					this.lblThirdParty.Text = entity.GetString("ThirdParty");
					this.lblUnitName.Text = BLL.SystemRule.GetUnitName(entity.GetString("UnitCode"));
					this.lblContractObject.Text = entity.GetString("ContractObject");

					string WBSCode = entity.GetString("WBSCode");
					if (WBSCode != "") 
					{
						EntityData entityTask = DAL.EntityDAO.WBSDAO.GetTaskByCode(WBSCode);
						if (entityTask.HasRecord()) 
						{
							this.lblTaskName.Text = entityTask.GetString("TaskName");
							this.hrefTaskName.Attributes["hint"] = BLL.ConstructProgRule.GetTaskHintHtml(entityTask.CurrentRow);
							this.hrefTaskName.HRef = "javascript:OpenTask('"+WBSCode+"');";
						}
						entityTask.Dispose();
					}

					int status = entity.GetInt("Status");
					// 设定合同变更标记
					if(status==4)
					{
				
						RmsPM.DAL.QueryStrategy.ContractStrategyBuilder CSB=new RmsPM.DAL.QueryStrategy.ContractStrategyBuilder();

						CSB.AddStrategy( new Strategy( DAL.QueryStrategy.ContractStrategyName.ContractCode,curContractCode));
						string sql1 = CSB.BuildMainQueryString();
						QueryAgent qa1 = new QueryAgent();
						qa1.SetTopNumber(1);
						EntityData entityTemp = qa1.FillEntityData("Contract",sql1);
						qa1.Dispose();

						// 对比
						if(entityTemp.HasRecord())
						{
							if(entity.GetString("Type")!=entityTemp.GetString("Type")) this.LabelType.BackColor = Color.Yellow;
							if(entity.GetString("Remark")!=entityTemp.GetString("Remark")) this.LabelRemark.BackColor = Color.Yellow;
							if(entity.GetString("ContractName")!=entityTemp.GetString("ContractName")) this.LabelContractName.BackColor = Color.Yellow;
							if(entity.GetDateTimeOnlyDate("ContractDate")!=entityTemp.GetDateTimeOnlyDate("ContractDate")) this.LabelContractDate.BackColor = Color.Yellow;
							if(entity.GetString("ContractPerson")!=entityTemp.GetString("ContractPerson")) this.LabelContractPerson.BackColor = Color.Yellow;
							if(entity.GetString("SupplierCode")!=entityTemp.GetString("SupplierCode")) this.SupplierSpan.Style["background-color"] = "Yellow";
							if(entity.GetDecimal("TotalMoney")!=entityTemp.GetDecimal("TotalMoney")) this.LabelTotalMoney.BackColor = Color.Yellow;
							if(entity.GetString("ThirdParty")!=entityTemp.GetString("ThirdParty")) this.lblThirdParty.BackColor = Color.Yellow;
							if(entity.GetString("UnitCode")!=entityTemp.GetString("UnitCode")) this.lblUnitName.BackColor = Color.Yellow;
							if(entity.GetString("ContractObject")!=entityTemp.GetString("ContractObject")) this.lblContractObject.BackColor = Color.Yellow;
							if(entity.GetString("WBSCode")!=entityTemp.GetString("WBSCode")) this.lblTaskName.BackColor = Color.Yellow;
							if(entity.GetString("AlloType")!=entityTemp.GetString("AlloType")) this.tdAllocateRelation.InnerHtml = "<span style='background-color: #FFFF00'>"+this.tdAllocateRelation.InnerHtml+"</span>";
						}
						entityTemp.Dispose();						
					}

					string alloType = entity.GetString("AlloType");


					// 合同已付和未付款
					decimal ahMoney = BLL.CBSRule.GetAHMoney("","","",ContractCode,"1");
					this.lblAPMoney.Text = BLL.StringRule.BuildShowNumberString( ahMoney);
					this.lblUPMoney.Text = BLL.StringRule.BuildShowNumberString(totalMoney-ahMoney);

					// 合同状态： 0: 正常； 1 待审核，当前合同； 2 合同结算完毕，结束 ； 
					//            3 申请不通过 ； 4 变更申请；5 变更申请不通过，作废； 6 历史记录
					this.btnModifyPaymentPlan.Visible = false;
					
					this.lblStatus.Text = BLL.ContractRule.GetContractStatusName(status.ToString());

					// 标记原合同状态为变更申请 然后标记当前合同
					string tmp = BLL.ContractRule.GetContractVersionCode(ContractCode,"4");
					if(tmp.Length>0)
					{
						this.lblStatus.Text += "   变更申请";
					}

					if(status==0 || status==4 || status == 6)
					{
						this.tdBefore.InnerText = "原合同金额(差额)：";
						// 取得第一笔合同总金额
				
						RmsPM.DAL.QueryStrategy.ContractStrategyBuilder CSB1=new RmsPM.DAL.QueryStrategy.ContractStrategyBuilder();
						CSB1.AddStrategy( new Strategy( DAL.QueryStrategy.ContractStrategyName.Status,"0,4,6"));
						CSB1.AddStrategy( new Strategy( DAL.QueryStrategy.ContractStrategyName.ContractLabel,entity.GetString("ContractLabel")));
						CSB1.AddOrder("ChangeDate",true);
						string sql2 = CSB1.BuildMainQueryString();
						QueryAgent qa2 = new QueryAgent();
						qa2.SetTopNumber(1);
						EntityData entityfirst = qa2.FillEntityData("Contract",sql2);
						qa2.Dispose();

						decimal dtmp = 0.0m;
						if(entityfirst.HasRecord())
						{
							dtmp = entity.GetDecimal("TotalMoney")-entityfirst.GetDecimal("TotalMoney");
							this.lblBeforeAccountTotalMoney.Text = BLL.StringRule.BuildShowNumberString(entityfirst.GetDecimal("TotalMoney"))+"("+BLL.StringRule.BuildShowNumberString(dtmp)+")";
						}							
					}

					switch ( status )
					{
						case 0:
							this.btnModifyPaymentPlan.Visible = true;
							string changingCode = BLL.ContractRule.GetChangingContractVersionCode( ContractCode);
							break;
						case 2:
							this.tdBefore.InnerText = "原合同金额：";
							this.lblBeforeAccountTotalMoney.Text = BLL.StringRule.BuildShowNumberString(entity.GetDecimal("BeforeAccountTotalMoney"));
							break;


					}

					//分摊的楼栋名称
					entity.SetCurrentTable("ContractBuilding");
					int iBCount = entity.CurrentTable.Rows.Count;
					string buildingName = "";
					for ( int i=0; i<iBCount; i++)
					{
						entity.SetCurrentRow(i);
						if ( alloType == "P" )
							buildingName = "项目";
						else if ( alloType == "U" )
						{
							string pCode = entity.GetString("PBSUnitCode");
							buildingName += "<a href=## onclick='showPBS(code,alloType)' alloType='U' code='" + pCode + "'>"+BLL.PBSRule.GetPBSUnitName(pCode)+"</a>&nbsp; ";
						}
						else if ( alloType == "B" )
						{
							string bCode = entity.GetString("BuildingCode");
							buildingName += "<a href=## onclick='showPBS(code,alloType)' alloType='B' code='" + bCode + "'>"+BLL.ProductRule.GetBuildingName(bCode)+"</a>&nbsp; ";
						}
					}
					this.tdAllocateRelation.InnerHtml = buildingName;

				}

				entity.SetCurrentTable("ContractExecutePlan");
				

				// 请款单明细, 统计费用分解中各个款项的付款请款
				PaymentItemStrategyBuilder sbPaymentItem = new PaymentItemStrategyBuilder();
				sbPaymentItem.AddStrategy( new Strategy( PaymentItemStrategyName.ContractCode,ContractCode  ) );
				sbPaymentItem.AddStrategy( new Strategy( PaymentItemStrategyName.Status,"1,2"  ) );
				string sql = sbPaymentItem.BuildQueryViewString();
				QueryAgent qa = new QueryAgent();
				EntityData paymentItem = qa.FillEntityData("V_PaymentItem",sql);

				entity.SetCurrentTable("ContractAllocation");
				entity.Tables["ContractAllocation"].Columns.Add("TotalPayoutMoney");
				entity.Tables["ContractAllocation"].Columns.Add("PayConditionHtml");
				foreach ( DataRow dr in entity.Tables["ContractAllocation"].Rows)
				{
					string allocateCode = (string)dr["AllocateCode"];
					decimal tp = BLL.MathRule.SumColumn(paymentItem.CurrentTable,"ItemMoney",String.Format("AllocateCode='{0}'",allocateCode));

					// 已付金额
					dr["TotalPayoutMoney"] = tp;

					//付款条件
					dr["PayConditionHtml"] = BLL.ContractRule.GetContractPayConditionHtml(allocateCode, entity.Tables["ContractPayCondition"], false);
				}
				this.dgPaymentPlanList.DataSource = new DataView ( entity.Tables["ContractAllocation"],"","PlanningPayDate",DataViewRowState.CurrentRows);
				this.dgPaymentPlanList.DataBind();

				entity.Dispose();
				paymentItem.Dispose();

				ScriptResponse();


			}
			catch( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"加载合同数据失败。");
				Response.Write(Rms.Web.JavaScript.Alert(true, "加载合同数据失败：" + ex.Message));
			}
		}
		private void ScriptResponse()
		{
			this.ScriptSpan.InnerHtml = "<script language=\"javascript\">"
				+"function DoModifyPaymentPlan()"
				+"{"
				+"	OpenLargeWindow('ContractPaymentPlanModify.aspx?ProjectCode="+this.ProjectCode+"&ContractCode="+this.ContractCode+"','修改付款计划');"
				+"}"
				+"function doViewSupplier(SupplierCode)"
				+"{"
				+"	OpenFullWindow(\"../Supplier/SupplierInfo.aspx?ProjectCode="+this.ProjectCode+"&SupplierCode=\" + SupplierCode,'供应商信息');"
				+"}"
				+"</script>";
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
	}
}
