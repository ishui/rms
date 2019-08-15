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
	///		ContractInfoControl ��ժҪ˵������ͬ������Ϣ��ʾ���
	/// </summary>
	/// *******************************************************************************************
	public partial class ContractInfoControl : ContractControlBase
	{

		private string _ProjectCode = "";
		private string _ContractCode = "";
		private string _UserCode = "";
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
		/// <summary>
		/// ��ǰ�û�
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
		/// ҳ�����
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
		/// ��ʼ��
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

		/// <summary>
		/// ���ݼ���
		/// </summary>
		private void LoadData()
		{
			// ��ǰ�汾�ĺ�ͬ��
			string curContractCode = BLL.ContractRule.GetCurrentContractVersionCode(ContractCode);
			this.ViewState.Add("CurrentContractCode",curContractCode);
			try
			{

				EntityData entity = DAL.EntityDAO.ContractDAO.GetStandard_ContractByCode(ContractCode);
				decimal totalMoney = decimal.Zero;
				if ( entity.HasRecord())
				{
					/************************ �ɹ��� *************************/
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
						this.PurchaseLink.InnerHtml = "<a href=\"javascript:OpenFullWindow('../Purchase/PurchaseManage.aspx?frameType=List&action=View&applicationCode="+PurchaseCode+"','�ɹ���');return false;\">"+PurchaseName+"</a>";
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
					// �趨��ͬ������
					if(status==4)
					{
				
						RmsPM.DAL.QueryStrategy.ContractStrategyBuilder CSB=new RmsPM.DAL.QueryStrategy.ContractStrategyBuilder();

						CSB.AddStrategy( new Strategy( DAL.QueryStrategy.ContractStrategyName.ContractCode,curContractCode));
						string sql1 = CSB.BuildMainQueryString();
						QueryAgent qa1 = new QueryAgent();
						qa1.SetTopNumber(1);
						EntityData entityTemp = qa1.FillEntityData("Contract",sql1);
						qa1.Dispose();

						// �Ա�
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


					// ��ͬ�Ѹ���δ����
					decimal ahMoney = BLL.CBSRule.GetAHMoney("","","",ContractCode,"1");
					this.lblAPMoney.Text = BLL.StringRule.BuildShowNumberString( ahMoney);
					this.lblUPMoney.Text = BLL.StringRule.BuildShowNumberString(totalMoney-ahMoney);

					// ��ͬ״̬�� 0: ������ 1 ����ˣ���ǰ��ͬ�� 2 ��ͬ������ϣ����� �� 
					//            3 ���벻ͨ�� �� 4 ������룻5 ������벻ͨ�������ϣ� 6 ��ʷ��¼
					this.btnModifyPaymentPlan.Visible = false;
					
					this.lblStatus.Text = BLL.ContractRule.GetContractStatusName(status.ToString());

					// ���ԭ��ͬ״̬Ϊ������� Ȼ���ǵ�ǰ��ͬ
					string tmp = BLL.ContractRule.GetContractVersionCode(ContractCode,"4");
					if(tmp.Length>0)
					{
						this.lblStatus.Text += "   �������";
					}

					if(status==0 || status==4 || status == 6)
					{
						this.tdBefore.InnerText = "ԭ��ͬ���(���)��";
						// ȡ�õ�һ�ʺ�ͬ�ܽ��
				
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
							this.tdBefore.InnerText = "ԭ��ͬ��";
							this.lblBeforeAccountTotalMoney.Text = BLL.StringRule.BuildShowNumberString(entity.GetDecimal("BeforeAccountTotalMoney"));
							break;


					}

					//��̯��¥������
					entity.SetCurrentTable("ContractBuilding");
					int iBCount = entity.CurrentTable.Rows.Count;
					string buildingName = "";
					for ( int i=0; i<iBCount; i++)
					{
						entity.SetCurrentRow(i);
						if ( alloType == "P" )
							buildingName = "��Ŀ";
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
				

				// ����ϸ, ͳ�Ʒ��÷ֽ��и�������ĸ������
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

					// �Ѹ����
					dr["TotalPayoutMoney"] = tp;

					//��������
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
				ApplicationLog.WriteLog(this.ToString(),ex,"���غ�ͬ����ʧ�ܡ�");
				Response.Write(Rms.Web.JavaScript.Alert(true, "���غ�ͬ����ʧ�ܣ�" + ex.Message));
			}
		}
		private void ScriptResponse()
		{
			this.ScriptSpan.InnerHtml = "<script language=\"javascript\">"
				+"function DoModifyPaymentPlan()"
				+"{"
				+"	OpenLargeWindow('ContractPaymentPlanModify.aspx?ProjectCode="+this.ProjectCode+"&ContractCode="+this.ContractCode+"','�޸ĸ���ƻ�');"
				+"}"
				+"function doViewSupplier(SupplierCode)"
				+"{"
				+"	OpenFullWindow(\"../Supplier/SupplierInfo.aspx?ProjectCode="+this.ProjectCode+"&SupplierCode=\" + SupplierCode,'��Ӧ����Ϣ');"
				+"}"
				+"</script>";
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
