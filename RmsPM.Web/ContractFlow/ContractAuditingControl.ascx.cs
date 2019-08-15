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
	///		ContractAuditingControl ��ժҪ˵����
	/// </summary>
	public partial class ContractAuditingControl : ContractControlBase
	{
		private string _ProjectCode = "";
		private string _ContractCode = "";

		/// <summary>
		/// ��ͬ����
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
		/// ��Ŀ����
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
			// �ڴ˴������û������Գ�ʼ��ҳ��
		}
		public void InitControl()
		{
			if(this.State == ModuleState.Sightless)//���ɼ���
			{
				this.Visible = false;
			}
			else if(this.State == ModuleState.Operable)//�ɲ�����
			{
				LoadData();
			}
			else if(this.State == ModuleState.Eyeable)//�ɼ���
			{
				LoadData();
			}
			else if(this.State == ModuleState.Begin)//�ɼ���
			{
				LoadData();
			}
			else if(this.State == ModuleState.End)//�ɼ���
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

					// ���������еĺ�ͬ����Ҫ����ǰ�ķ���
					if ( status == 4 || status == 1 )
					{
//						bool canPass = true; // �Ƿ�ͨ��
						int budgetPower = BLL.SystemRule.GetBudgetControlPower(projectCode);

						// �ð汾�ĵ�ǰ��Ч�汾���
						string currentContractCode = BLL.ContractRule.GetCurrentContractVersionCode(contractCode);

						// ������
						if ( budgetPower == 1 )
						{
							DataTable dt = new DataTable();
							dt.Columns.Add( "CostCode",System.Type.GetType("System.String") );
							dt.Columns.Add( "CostName",System.Type.GetType("System.String") );
							dt.Columns.Add( "CurrentCost",System.Type.GetType("System.Decimal") );
						
							//����ֽ�
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
							// �����ܶ�
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
				
							// ��̬����
							string budgetCode = "";
							EntityData standardBudget = BLL.CBSRule.GetCurrentDynamicEntity(projectCode,ref budgetCode);
							if ( ! standardBudget.HasRecord())
							{
								Response.Write( Rms.Web.JavaScript.ScriptStart);
								Response.Write(Rms.Web.JavaScript.Alert(false,"�����ƶ�Ԥ�� ��"));
								Response.Write(Rms.Web.JavaScript.WinClose(false));
								Response.Write(Rms.Web.JavaScript.ScriptEnd);
								Response.End();
							}

							//����ֽ�
							entity.SetCurrentTable("ContractAllocation");
							foreach ( DataRow dr in entity.CurrentTable.Rows)
							{
								string costCode = (string)dr["CostCode"];
								string apCostCode = BLL.CBSRule.GetCostAccountPointCode( projectCode,budgetCode,costCode);

								// ����ͬ��������
								decimal thisContractCost = decimal.Zero;
								if ( ! dr.IsNull("Money"))
									thisContractCost=(decimal)dr["Money"];

								DataRow[] drsDT = dt.Select( String.Format( "CostCode='{0}'",apCostCode ) );
								if ( drsDT.Length == 0 )
								{
									string costName = BLL.CBSRule.GetCostName(costCode);
									// ��̬����
									decimal dynamicCost = BLL.MathRule.SumColumn(standardBudget.Tables["BudgetCost"],"BudgetCost", String.Format( "CostCode='{0}'" ,costCode )  );
									// ��ǰ��Ŀ�Ǻ�ͬ��ʵ�ʷ������
									decimal ahCost = BLL.CBSRule.GetAHMoney(costCode,"","","","0");
									// ���к�ͬ�и÷�������ռ�ݵĽ��
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

							// ����ԭ��ͬ��ռ�õĲ���
							// ԭ�汾��ͬ
							EntityData entityOld = DAL.EntityDAO.ContractDAO.GetStandard_ContractByCode( currentContractCode );
							foreach ( DataRow dr in entityOld.Tables["ContractAllocation"].Rows)
							{
								string costCode = (string)dr["CostCode"];
								string apCostCode = BLL.CBSRule.GetCostAccountPointCode( projectCode,budgetCode,costCode);

								decimal thisContractCost = decimal.Zero;
								if ( ! dr.IsNull("Money"))
									thisContractCost=(decimal)dr["Money"];

								// �ҵ���صķ�����Ҳ����Ͳ�����ˡ�
								DataRow [] drsDT = dt.Select( String.Format( "CostCode='{0}' ", apCostCode ) );
								if ( drsDT.Length > 0 )
								{
									drsDT[0]["CurrentContractAllocationCost"] = (decimal)drsDT[0]["CurrentContractAllocationCost"] + thisContractCost;
								}
							}

							// ������ÿռ�
							foreach ( DataRow dr in dt.Rows)
							{
								// ���ÿռ�
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
							// ��ȷ���Ƶ��·ݣ�
							// Ҫ���ҿ��Ƶ� AccountPoint = 1 ��Ԥ��㣻
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
						

							// Ԥ��ļƻ���������
							EntityData planPeriod = DAL.EntityDAO.SystemManageDAO.GetPeriodDefineByProjectCode(projectCode);

							// ��̬����
							string budgetCode = "";
							EntityData standardBudget = BLL.CBSRule.GetCurrentDynamicEntity(projectCode,ref budgetCode);
							if ( ! standardBudget.HasRecord())
							{
								Response.Write( Rms.Web.JavaScript.ScriptStart);
								Response.Write(Rms.Web.JavaScript.Alert(false,"�����ƶ�Ԥ�� ��"));
								Response.Write(Rms.Web.JavaScript.WinClose(false));
								Response.Write(Rms.Web.JavaScript.ScriptEnd);
								Response.End();
							}
							// ȡ��صĲ���
							int iYear = standardBudget.GetInt("IYear");
							int iMonth = standardBudget.GetInt("IMonth");
							int periodMonth = standardBudget.GetInt("PeriodMonth");
							int afterPeriod  = standardBudget.GetInt("AfterPeriod");
							int periodIndex = standardBudget.GetInt("PeriodIndex");

							// ��¼���������ڿ��Ƶ�ڵ��ʱ��εĽ��
							foreach ( DataRow dr in entity.Tables["ContractAllocation"].Rows)
							{
								string costCode = (string)dr["CostCode"];
								string apCostCode = BLL.CBSRule.GetCostAccountPointCode( projectCode,budgetCode,costCode);
								// ����ͬ��������
								decimal thisContractCost = decimal.Zero;
								if ( ! dr.IsNull("Money"))
									thisContractCost=(decimal)dr["Money"];

								DateTime payDate = (DateTime) dr["PlanningPayDate"];

								// ��һ����Ԥ���е���һ��
								// ������һ�ڵ�Ԥ��Ϳ�ʼ������ʱ��
								DateTime budgetStartDate =  DateTime.Parse( String.Format( "{0}-{1}-1",iYear,iMonth ) );
								string startDate = "";			// �����ڵĿ�ʼʱ��
								string endDate = "";			// �����ڵĽ���ʱ��
								int year = 0;					// ����������
								int month = 1;					// �����ڵ�����

								// �ڱ���Ԥ����
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
									// ��̬����
									decimal dynamicCost = decimal.Zero;
									if ( year == 0 )
										dynamicCost = BLL.MathRule.SumColumn(standardBudget.Tables["BudgetMonth"],"Money", String.Format( "CostCode='{0}' and IYear={1} and IMonth={2} " ,apCostCode , year , month )  );
									else
										dynamicCost = BLL.MathRule.SumColumn(standardBudget.Tables["BudgetYear"],"Money", String.Format( "CostCode='{0}' and IYear={1}" ,apCostCode,year )  );

									// ��ǰ��Ŀ�Ǻ�ͬ�ĸ�ʱ��ε�ʵ�ʷ������
									decimal ahCost = BLL.CBSRule.GetAHMoney(apCostCode,startDate,endDate,"","0");
									// ������Ч��ͬ�и÷������ڸ�ʱ�����ռ�ݵĽ��
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

							// ����ԭ��ͬ��ռ�õĲ���

							// ԭ�汾��ͬ
							EntityData entityOld = DAL.EntityDAO.ContractDAO.GetStandard_ContractByCode( currentContractCode );
							foreach ( DataRow dr in entityOld.Tables["ContractAllocation"].Rows)
							{
								string costCode = (string)dr["CostCode"];
								string apCostCode = BLL.CBSRule.GetCostAccountPointCode( projectCode,budgetCode,costCode);

								// ����ͬ��������
								decimal thisContractCost = decimal.Zero;
								if ( ! dr.IsNull("Money"))
									thisContractCost=(decimal)dr["Money"];

								DateTime payDate = (DateTime) dr["PlanningPayDate"];

								// ��һ����Ԥ���е���һ��
								// ������һ�ڵ�Ԥ��Ϳ�ʼ������ʱ��
								DateTime budgetStartDate =  DateTime.Parse( String.Format( "{0}-{1}-1",iYear,iMonth ) );
								string startDate = "";			// �����ڵĿ�ʼʱ��
								string endDate = "";			// �����ڵĽ���ʱ��
								int year = 0;					// ����������
								int month = 1;					// �����ڵ�����

								// �ڱ���Ԥ����
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

								// �ҵ���صķ�����Ҳ����Ͳ�����ˡ�
								DataRow [] drsDT = dt.Select( String.Format( "CostCode='{0}' and year={1} and month={2} ", new object[]{apCostCode,year,month} ) );
								if ( drsDT.Length > 0 )
								{
									drsDT[0]["CurrentContractAllocationCost"] = (decimal)drsDT[0]["CurrentContractAllocationCost"] + thisContractCost;
								}
							}

							// ������ÿռ�
							foreach ( DataRow dr in dt.Rows)
							{
								// ���ÿռ�
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
				ApplicationLog.WriteLog(this.ToString(),ex,"�������ݳ���");
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

		}
		#endregion
	}
}
