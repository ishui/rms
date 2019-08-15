namespace RmsPM.Web.ContractFlow
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using Rms.ORMap;
	using RmsPM.DAL;
	using RmsPM.DAL.QueryStrategy;
	using RmsPM.BLL;
	using RmsPM.Web.WorkFlowControl;

	/// <summary>
	///		ContractAuditingControl 的摘要说明。
	/// </summary>
	public partial class ContractAuditingControl : ContractControlBase
	{
		private string _ProjectCode = "";
		private string _ContractCode = "";

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

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
		}
		public void InitControl()
		{
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
		private void LoadData()
		{

			string contractCode = this.ContractCode;
			string projectCode = this.ProjectCode;

			try
			{
				int status = -1;

				EntityData entity=RmsPM.DAL.EntityDAO.ContractDAO.GetStandard_ContractByCode(contractCode); 
				if(entity.HasRecord())
				{
					status = entity.GetInt("Status");

					// 如果是审核中的合同，需要看当前的费用
					if ( status == 4 || status == 1 )
					{
//						bool canPass = true; // 是否通过
						int budgetPower = BLL.SystemRule.GetBudgetControlPower(projectCode);

						// 该版本的当前生效版本标号
						string currentContractCode = BLL.ContractRule.GetCurrentContractVersionCode(contractCode);

						// 不控制
						if ( budgetPower == 1 )
						{
							DataTable dt = new DataTable();
							dt.Columns.Add( "CostCode",System.Type.GetType("System.String") );
							dt.Columns.Add( "CostName",System.Type.GetType("System.String") );
							dt.Columns.Add( "CurrentCost",System.Type.GetType("System.Decimal") );
						
							//款项分解
							entity.SetCurrentTable("ContractAllocation");
							foreach ( DataRow dr in entity.CurrentTable.Rows)
							{
								string costCode = (string)dr["CostCode"];
								if ( dt.Select( String.Format( "CostCode='{0}'",costCode ) ).Length == 0 )
								{
									decimal thisContractCost = BLL.MathRule.SumColumn( entity.Tables["ContractAllocation"],"Money", String.Format( "CostCode='{0}'",costCode ) );
									DataRow drNew = dt.NewRow();
									drNew["CostCode"] = costCode;
									string costName = BLL.CBSRule.GetCostName(costCode);
									drNew["CostName"] = costName;
									drNew["CurrentCost"] = thisContractCost;
									dt.Rows.Add(drNew);
								}
							}

							this.dgList1.DataSource = dt;
							this.dgList1.DataBind();
							this.dgList2.Visible = false;
							this.dgList3.Visible = false;
							dt.Dispose();
						}
							// 控制总额
						else if ( budgetPower == 2 )
						{
							DataTable dt = new DataTable();
							dt.Columns.Add( "CostCode",System.Type.GetType("System.String") );
							dt.Columns.Add( "CostName",System.Type.GetType("System.String") );
							dt.Columns.Add( "DynamicCost",System.Type.GetType("System.Decimal") );
							dt.Columns.Add( "AHCost",System.Type.GetType("System.Decimal") );
							dt.Columns.Add( "CostSpace",System.Type.GetType("System.Decimal") );
							dt.Columns.Add( "AllocationCost",System.Type.GetType("System.Decimal") );
							dt.Columns.Add( "CurrentCost",System.Type.GetType("System.Decimal") );
							dt.Columns.Add( "CurrentContractAllocationCost",System.Type.GetType("System.Decimal") );
							dt.Columns.Add( "AlertClass",System.Type.GetType("System.String") );
				
							// 动态费用
							string budgetCode = "";
							EntityData standardBudget = BLL.CBSRule.GetCurrentDynamicEntity(projectCode,ref budgetCode);
							if ( ! standardBudget.HasRecord())
							{
								Response.Write( Rms.Web.JavaScript.ScriptStart);
								Response.Write(Rms.Web.JavaScript.Alert(false,"必须制定预算 ！"));
								Response.Write(Rms.Web.JavaScript.WinClose(false));
								Response.Write(Rms.Web.JavaScript.ScriptEnd);
								Response.End();
							}

							//款项分解
							entity.SetCurrentTable("ContractAllocation");
							foreach ( DataRow dr in entity.CurrentTable.Rows)
							{
								string costCode = (string)dr["CostCode"];
								string apCostCode = BLL.CBSRule.GetCostAccountPointCode( projectCode,budgetCode,costCode);

								// 本合同的申请金额
								decimal thisContractCost = decimal.Zero;
								if ( ! dr.IsNull("Money"))
									thisContractCost=(decimal)dr["Money"];

								DataRow[] drsDT = dt.Select( String.Format( "CostCode='{0}'",apCostCode ) );
								if ( drsDT.Length == 0 )
								{
									string costName = BLL.CBSRule.GetCostName(costCode);
									// 动态费用
									decimal dynamicCost = BLL.MathRule.SumColumn(standardBudget.Tables["BudgetCost"],"BudgetCost", String.Format( "CostCode='{0}'" ,costCode )  );
									// 当前项目非合同的实际发生金额
									decimal ahCost = BLL.CBSRule.GetAHMoney(costCode,"","","","0");
									// 所有合同中该费用项所占据的金额
									decimal allocationCost = BLL.CBSRule.GetContractAllocationCost( costCode ,"" ,"", "","" );

									DataRow drNew = dt.NewRow();
									drNew["CostCode"] = costCode;
									drNew["CostName"] = costName;
									drNew["AHCost"] = ahCost;
									drNew["DynamicCost"] = dynamicCost;
									drNew["AllocationCost"] = allocationCost;
									drNew["CurrentContractAllocationCost"] = decimal.Zero;
									drNew["CurrentCost"] = thisContractCost;
									dt.Rows.Add(drNew);

								}
								else
								{
									DataRow drT = drsDT[0];
									drT["CurrentCost"] = (decimal)drT["CurrentCost"] + thisContractCost;
								}
							}

							// 计算原合同中占用的部分
							// 原版本合同
							EntityData entityOld = DAL.EntityDAO.ContractDAO.GetStandard_ContractByCode( currentContractCode );
							foreach ( DataRow dr in entityOld.Tables["ContractAllocation"].Rows)
							{
								string costCode = (string)dr["CostCode"];
								string apCostCode = BLL.CBSRule.GetCostAccountPointCode( projectCode,budgetCode,costCode);

								decimal thisContractCost = decimal.Zero;
								if ( ! dr.IsNull("Money"))
									thisContractCost=(decimal)dr["Money"];

								// 找到相关的费用项，找不到就不理会了。
								DataRow [] drsDT = dt.Select( String.Format( "CostCode='{0}' ", apCostCode ) );
								if ( drsDT.Length > 0 )
								{
									drsDT[0]["CurrentContractAllocationCost"] = (decimal)drsDT[0]["CurrentContractAllocationCost"] + thisContractCost;
								}
							}

							// 计算费用空间
							foreach ( DataRow dr in dt.Rows)
							{
								// 费用空间
								decimal costSpace = (decimal)dr["dynamicCost"] - (decimal)dr["ahCost"] 
									- (decimal)dr["allocationCost"] + (decimal)dr["currentContractAllocationCost"];

								dr["CostSpace"] = costSpace;
								if ( (decimal)dr["CurrentCost"] > costSpace )
								{
									dr["AlertClass"] = "tdAlert";
//									canPass = false;
								}
							}


							this.dgList2.DataSource = dt;
							this.dgList2.DataBind();
							this.dgList1.Visible = false;
							this.dgList3.Visible = false;
							dt.Dispose();
							entityOld.Dispose();
							standardBudget.Dispose();
						}
							// 精确控制到月份，
							// 要查找控制点 AccountPoint = 1 的预算点；
						else if ( budgetPower == 3 )
						{
							DataTable dt = new DataTable();
							dt.Columns.Add( "CostCode",System.Type.GetType("System.String") );
							dt.Columns.Add( "CostName",System.Type.GetType("System.String") );
							dt.Columns.Add("Year",System.Type.GetType("System.Int32") );
							dt.Columns.Add("Month",System.Type.GetType("System.Int32") );
							dt.Columns.Add("YearMonth",System.Type.GetType("System.String") );
							dt.Columns.Add( "DynamicCost",System.Type.GetType("System.Decimal") );
							dt.Columns.Add( "AHCost",System.Type.GetType("System.Decimal") );
							dt.Columns.Add( "CostSpace",System.Type.GetType("System.Decimal") );
							dt.Columns.Add( "AllocationCost",System.Type.GetType("System.Decimal") );
							dt.Columns.Add( "CurrentCost",System.Type.GetType("System.Decimal") );
							dt.Columns.Add( "CurrentContractAllocationCost",System.Type.GetType("System.Decimal") );
							dt.Columns.Add( "AlertClass",System.Type.GetType("System.String") );
						

							// 预算的计划周期名称
							EntityData planPeriod = DAL.EntityDAO.SystemManageDAO.GetPeriodDefineByProjectCode(projectCode);

							// 动态费用
							string budgetCode = "";
							EntityData standardBudget = BLL.CBSRule.GetCurrentDynamicEntity(projectCode,ref budgetCode);
							if ( ! standardBudget.HasRecord())
							{
								Response.Write( Rms.Web.JavaScript.ScriptStart);
								Response.Write(Rms.Web.JavaScript.Alert(false,"必须制定预算 ！"));
								Response.Write(Rms.Web.JavaScript.WinClose(false));
								Response.Write(Rms.Web.JavaScript.ScriptEnd);
								Response.End();
							}
							// 取相关的参数
							int iYear = standardBudget.GetInt("IYear");
							int iMonth = standardBudget.GetInt("IMonth");
							int periodMonth = standardBudget.GetInt("PeriodMonth");
							int afterPeriod  = standardBudget.GetInt("AfterPeriod");
							int periodIndex = standardBudget.GetInt("PeriodIndex");

							// 记录各个款项在控制点节点的时间段的金额
							foreach ( DataRow dr in entity.Tables["ContractAllocation"].Rows)
							{
								string costCode = (string)dr["CostCode"];
								string apCostCode = BLL.CBSRule.GetCostAccountPointCode( projectCode,budgetCode,costCode);
								// 本合同的申请金额
								decimal thisContractCost = decimal.Zero;
								if ( ! dr.IsNull("Money"))
									thisContractCost=(decimal)dr["Money"];

								DateTime payDate = (DateTime) dr["PlanningPayDate"];

								// 这一天是预算中的哪一期
								// 计算这一期的预算和开始结束的时间
								DateTime budgetStartDate =  DateTime.Parse( String.Format( "{0}-{1}-1",iYear,iMonth ) );
								string startDate = "";			// 所在期的开始时间
								string endDate = "";			// 所在期的结束时间
								int year = 0;					// 所在期期数
								int month = 1;					// 所在期的月数

								// 在本期预算中
								if ( budgetStartDate.AddMonths(periodMonth) > payDate )
								{
									year = 0;
									month = (payDate.Year-iYear) * 12 + (payDate.Month - iMonth)+1;
									startDate = payDate.ToString("yyyy-MM-1");
									endDate = DateTime.Parse(startDate).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");
								}
								else
								{
									int r ;
									year = Math.DivRem( (payDate.Year-iYear) * 12 + (payDate.Month - iMonth) , periodMonth , out r );
									startDate = budgetStartDate.AddMonths( year * periodMonth ).ToString("yyyy-MM-dd");
									endDate = budgetStartDate.AddMonths( (year+1) * periodMonth ).AddDays(-1).ToString("yyyy-MM-dd");
								}

								DataRow [] drsDT = dt.Select( String.Format( "CostCode='{0}' and year={1} and month={2} ", new object[]{apCostCode,year,month} ) );
								if ( drsDT.Length == 0 )
								{
									string costName = BLL.CBSRule.GetCostName(apCostCode);
									// 动态费用
									decimal dynamicCost = decimal.Zero;
									if ( year == 0 )
										dynamicCost = BLL.MathRule.SumColumn(standardBudget.Tables["BudgetMonth"],"Money", String.Format( "CostCode='{0}' and IYear={1} and IMonth={2} " ,apCostCode , year , month )  );
									else
										dynamicCost = BLL.MathRule.SumColumn(standardBudget.Tables["BudgetYear"],"Money", String.Format( "CostCode='{0}' and IYear={1}" ,apCostCode,year )  );

									// 当前项目非合同的该时间段的实际发生金额
									decimal ahCost = BLL.CBSRule.GetAHMoney(apCostCode,startDate,endDate,"","0");
									// 所有生效合同中该费用项在该时间段所占据的金额
									decimal allocationCost = BLL.CBSRule.GetContractAllocationCost( apCostCode ,"" ,"", startDate,endDate );

									DataRow drNew = dt.NewRow();
									drNew["CostCode"] = costCode;
									drNew["CostName"] = costName;
									drNew["Year"] = year;
									drNew["Month"] = month;
									if ( year == 0 )
										drNew["YearMonth"] = BLL.CBSRule.GetBudgetMonthString(iYear,iMonth,month) ;
									else
									{
										string name = "";
										DataRow[] drsYearName = planPeriod.CurrentTable.Select( String.Format( "PeriodIndex={0}",year+periodIndex ) );
										if ( drsYearName.Length > 0 )
											name = (string) drsYearName[0]["PeriodName"];
										drNew["YearMonth"] = name ;
									}
									drNew["DynamicCost"] = dynamicCost;
									drNew["AllocationCost"] = allocationCost;
									drNew["AHCost"] = ahCost;
									drNew["CurrentCost"] = thisContractCost;
									drNew["CurrentContractAllocationCost"] = decimal.Zero;
									dt.Rows.Add(drNew);

								}
								else
								{
									DataRow drT = drsDT[0];
									drT["CurrentCost"] = (decimal)drT["CurrentCost"] + thisContractCost;
								}
							}

							// 计算原合同中占用的部分

							// 原版本合同
							EntityData entityOld = DAL.EntityDAO.ContractDAO.GetStandard_ContractByCode( currentContractCode );
							foreach ( DataRow dr in entityOld.Tables["ContractAllocation"].Rows)
							{
								string costCode = (string)dr["CostCode"];
								string apCostCode = BLL.CBSRule.GetCostAccountPointCode( projectCode,budgetCode,costCode);

								// 本合同的申请金额
								decimal thisContractCost = decimal.Zero;
								if ( ! dr.IsNull("Money"))
									thisContractCost=(decimal)dr["Money"];

								DateTime payDate = (DateTime) dr["PlanningPayDate"];

								// 这一天是预算中的哪一期
								// 计算这一期的预算和开始结束的时间
								DateTime budgetStartDate =  DateTime.Parse( String.Format( "{0}-{1}-1",iYear,iMonth ) );
								string startDate = "";			// 所在期的开始时间
								string endDate = "";			// 所在期的结束时间
								int year = 0;					// 所在期期数
								int month = 1;					// 所在期的月数

								// 在本期预算中
								if ( budgetStartDate.AddMonths(periodMonth) > payDate )
								{
									year = 0;
									month = (payDate.Year-iYear) * 12 + (payDate.Month - iMonth)+1;
									startDate = payDate.ToString("yyyy-MM-1");
									endDate = DateTime.Parse(startDate).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");
								}
								else
								{
									int r ;
									year = Math.DivRem( (payDate.Year-iYear) * 12 + (payDate.Month - iMonth) , periodMonth , out r );
									startDate = budgetStartDate.AddMonths( year * periodMonth ).ToString("yyyy-MM-dd");
									endDate = budgetStartDate.AddMonths( (year+1) * periodMonth ).AddDays(-1).ToString("yyyy-MM-dd");
								}

								// 找到相关的费用项，找不到就不理会了。
								DataRow [] drsDT = dt.Select( String.Format( "CostCode='{0}' and year={1} and month={2} ", new object[]{apCostCode,year,month} ) );
								if ( drsDT.Length > 0 )
								{
									drsDT[0]["CurrentContractAllocationCost"] = (decimal)drsDT[0]["CurrentContractAllocationCost"] + thisContractCost;
								}
							}

							// 计算费用空间
							foreach ( DataRow dr in dt.Rows)
							{
								// 费用空间
								decimal costSpace = (decimal)dr["dynamicCost"] - (decimal)dr["ahCost"] 
									- (decimal)dr["allocationCost"] + (decimal)dr["currentContractAllocationCost"];
								dr["CostSpace"] = costSpace;
								if ( (decimal)dr["CurrentCost"] > costSpace )
								{
									dr["AlertClass"] = "tdAlert";
//									canPass = false;
								}
							}

							this.dgList3.DataSource = dt;
							this.dgList3.DataBind();
							this.dgList1.Visible = false;
							this.dgList2.Visible = false;
							dt.Dispose();
							entityOld.Dispose();
							planPeriod.Dispose();
							standardBudget.Dispose();
						}

						
					}
					else
					{
						this.dgList1.Visible = false;
						this.dgList2.Visible = false;
						this.dgList3.Visible = false;
					}
				}
				entity.Dispose();
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"加载数据出错！");
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
	}
}
