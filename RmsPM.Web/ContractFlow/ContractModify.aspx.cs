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

namespace RmsPM.Web.ContractFlow
{
	/// <summary>
	/// ContractModify ��ժҪ˵����
	/// </summary>
	public partial class ContractModify : PageBase
	{
		protected string totalMoney = "";
		private string projectCode = "";


	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.spanTaskName.InnerText = this.txtTaskName.Value;

			if ( !IsPostBack )
			{
				IniPage();
				LoadData();
			}
			this.myAttachMentAdd.AttachMentType = "ContractAttachMent"; 
			this.myAttachMentAdd.MasterCode = this.txtContractCode.Value;  
		}
		

		private void IniPage()
		{
			try
			{
				if( Request["ProjectCode"]+"" != "")
				{
					projectCode = Request["ProjectCode"]+"";
				}
				else
				{
					projectCode = this.project.ProjectCode;
				}
				this.hdProjectCode.Value = projectCode;
				this.inputSystemGroup.ClassCode = "0501";
//				BLL.PageFacade.LoadUnitSelect( this.sltUnit,"",projectCode);
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog( this.ToString(),ex,"��ʼ��ҳ�����");
			}
		}

		private void LoadData()
		{
			try
			{
				string contractCode=Request.QueryString["ContractCode"]+"";
				this.txtContractCode.Value = contractCode;
				string action=Request.QueryString["Act"]+"";
				if(action == "Add")
				{
					if(projectCode != "")
					{
						this.SpanScript.InnerHtml = "";
					}
					else
					{
						this.SpanScript.InnerHtml = "<script language='javascript'> OpenMiddleWindow('../SelectBox/SelectProjectCanAccess.aspx' ,'ѡ����Ŀ');</script>";
						//Response.End();
					}
				}
				EntityData entity=null;

				if(action=="Add")
				{
					
					entity=new EntityData("Standard_Contract");
					DataRow dr = entity.GetNewRecord();
					dr = entity.GetNewRecord();
					contractCode = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("ContractCode");
					this.txtContractCode.Value = contractCode;
					dr["ContractCode"] = contractCode;
					dr["ProjectCode"] = projectCode;
					dr["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd");
					dr["CreatePerson"] = base.user.UserCode;
					dr["Status"]=1;
					dr["ContractLabel"]=DAL.EntityDAO.SystemManageDAO.GetNewSysCode("ContractLabel");
					dr["ContractPerson"]=user.UserCode;
					dr["AlloType"]="P";
					entity.AddNewRecord(dr);
					this.lblTitle.Text="��ͬ����";

					//�����������ֶ�
//					entity.Tables["ContractPayCondition"].Columns.Add("TaskName");
//					entity.Tables["ContractPayCondition"].Columns.Add("DelayTypeName");
				}
				else if(action=="Edit")
				{
					entity=DAL.EntityDAO.ContractDAO.GetStandard_ContractByCode(contractCode);
					this.lblTitle.Text="��ͬ�޸�";
				}
				else if(action=="Change")
				{
					entity=DAL.EntityDAO.ContractDAO.GetStandard_ContractByCode(contractCode);
					entity.CurrentRow["Status"]=4;
					this.lblTitle.Text="��ͬ���";

				}
				else
				{
					Response.Write(Rms.Web.JavaScript.PageTo(true,"Contract.aspx?ProjectCode=" + projectCode ));
					Response.End();
				}

				AddNewEmptyRow(entity,contractCode,"ContractAllocation","AllocateCode",5);
				AddNewEmptyRow(entity,contractCode,"ContractExecutePlan","ContractExecutePlanCode",5);


				// ������Ϣ
				entity.SetCurrentTable("Contract");
				this.txtRemark.Value = entity.GetString("Remark");
				this.txtContractID.Value = entity.GetString("ContractID");
				this.txtContractName.Value = entity.GetString("ContractName");

				this.txtContractPerson.Value = entity.GetString("ContractPerson");
				this.txtContractPersonName.Value = BLL.SystemRule.GetUserName(entity.GetString("ContractPerson"));

				this.inputSystemGroup.Value = entity.GetString("Type");
//				this.txtTypeCode.Value=entity.GetString("Type");
//				this.txtTypeName.Value=BLL.ContractRule.GetContractTypeName(entity.GetString("Type"));

				string unitCode = entity.GetString("UnitCode");
				this.txthUnit.Value = unitCode;
				this.txtUnitName.Value = BLL.SystemRule.GetUnitName(unitCode);
				this.txtSupplierCode.Value = entity.GetString("SupplierCode");
				this.oldSupplier.Value = entity.GetString("SupplierCode");
				this.txtSupplierName.Value = BLL.ProjectRule.GetSupplierName( entity.GetString("SupplierCode"));

				this.txtChangeReason.Value = entity.GetString("ChangeReason");
				this.txtChangeRemark.Value = entity.GetString("ChangeRemark");
				string alloType = entity.GetString("AlloType");
				this.txtAlloType.Value = alloType;
				this.txtContractObject.Value = entity.GetString("ContractObject");
				this.txtThirdParty.Value = entity.GetString("ThirdParty");

				this.txtWBSCode.Value = entity.GetString("WBSCode");
				this.txtTaskName.Value = BLL.WBSRule.GetWBSName(entity.GetString("WBSCode"));
				this.spanTaskName.InnerText = this.txtTaskName.Value;
				this.hBeforeAccountTotalMoney.Value = entity.GetDecimalString("BeforeAccountTotalMoney");

				totalMoney = entity.GetDecimalString("TotalMoney");

				/************************ �ɹ��� *************************/
				 DAL.QueryStrategy.PurchaseFlowStrategyBuilder sbp = new DAL.QueryStrategy.PurchaseFlowStrategyBuilder();
				sbp.AddStrategy( new Strategy( DAL.QueryStrategy.PurchaseFlowStrategyName.ProjectCode,this.projectCode));
				sbp.AddStrategy( new Strategy( DAL.QueryStrategy.PurchaseFlowStrategyName.ContractCode,""));
	
				string sqlp = sbp.BuildMainQueryString();
				
				QueryAgent QAp = new QueryAgent();
				EntityData entityp = QAp.FillEntityData("PurchaseFlow",sqlp);
				this.PurchaseSelect.DataSource = entityp;
				this.PurchaseSelect.DataTextField = "Purpose";
				this.PurchaseSelect.DataValueField = "PurchaseFlowCode";
				this.PurchaseSelect.DataBind();
				ListItem PSItem = new ListItem("","");
				this.PurchaseSelect.Items.Add(PSItem);
				QAp.Dispose();
				entityp.Dispose();
				if(Request["DocCode"]+"" != "")
				{
					this.PurchaseSelect.Value = Request["DocCode"].ToString();
				}
				else
				{
					this.PurchaseSelect.Value = "";
				}
				/*********************************************************/

				// ����Ǻ�ͬ���,�Ҵ򿪿���
				bool isShow = IsOldSumMoney();
				if(action=="Change"&&isShow)
				{
					// ��ͬ�����ʾ
					this.lblOldMoney.Attributes.Add("class","form-item");
					this.lblOldMoney.InnerText = "ԭʼ���";
					this.oldMoney.Visible = true;
					this.oldMoney.Value = RmsPM.BLL.StringRule.BuildShowNumberString(entity.GetDecimal("oldSumMoney"));
					
					if(base.user.HasOperationRight("190121")) // ԭʼ��ͬ�ܼ۲���
						this.oldMoney.Disabled = false;
					else
						this.oldMoney.Disabled = true;
				}
				
				// ������Ǳ��
				int status = entity.GetInt("Status");
				if ( status != 4 )
				{
					this.trChange1.Visible = false;
					this.trChange2.Visible = false;
				}

				string buildingCodes = "";
				string buildingNames = "";
				entity.SetCurrentTable("ContractBuilding");
				foreach ( DataRow dr in entity.Tables["ContractBuilding"].Select("","",DataViewRowState.CurrentRows))
				{
					if ( buildingCodes != "" )
					{
						buildingCodes += ",";
						buildingNames += ",";
					}
					if ( alloType == "B" )
					{
						string bCode = (string)dr["BuildingCode"];
						buildingCodes += bCode ;
						buildingNames += BLL.ProductRule.GetBuildingName(bCode);
					}
					else if ( alloType == "U" )
					{
						string pCode = (string)dr["PBSUnitCode"];
						buildingCodes += pCode ;
						buildingNames += BLL.ProductRule.GetBuildingName(pCode);
					}

				}
				if ( alloType == "P" )
					this.txtAllocateName.Value = "��Ŀ";
				else
				{
					this.txtAllocateCodes.Value = buildingCodes;
					this.txtAllocateName.Value = buildingNames;
				}

				// ִ�мƻ�
				entity.SetCurrentTable("ContractExecutePlan");
				entity.CurrentTable.Columns.Add( "ExecutePersonName" );
				int iCount = entity.CurrentTable.Rows.Count;
				for ( int i=0;i<iCount;i++)
				{
					entity.SetCurrentRow(i);
					string personCode = entity.GetString("Executor");
					entity.CurrentRow["ExecutePersonName"]=BLL.SystemRule.GetUserName(personCode);
				}
				this.dgExecuteList.DataSource = new DataView(entity.Tables["ContractExecutePlan"],"","",DataViewRowState.CurrentRows) ;
				this.dgExecuteList.DataBind();

				//����ֽ�
				entity.SetCurrentTable("ContractAllocation");
				entity.CurrentTable.Columns.Add("CostName");
				entity.CurrentTable.Columns.Add("PayConditionHtml");
				iCount = entity.CurrentTable.Rows.Count;
				for ( int i=0;i<iCount;i++)
				{
					entity.SetCurrentRow(i);
					string allocateCode = entity.GetString("AllocateCode");
					string costCode = entity.GetString("CostCode");
					entity.CurrentRow["CostName"]=BLL.CBSRule.GetCostName(costCode);

					//��������
					entity.CurrentRow["PayConditionHtml"] = BLL.ContractRule.GetContractPayConditionHtml(allocateCode, entity.Tables["ContractPayCondition"], true);
				}
				this.dgCostList.DataSource=new DataView( entity.CurrentTable ,"" , "",DataViewRowState.CurrentRows) ;
				this.dgCostList.DataBind();

				//��ع���
				entity.SetCurrentTable("TaskContract");
				entity.CurrentTable.Columns.Add("TaskName");
				entity.CurrentTable.Columns.Add("UserNames");
				entity.CurrentTable.Columns.Add("CompletePercent");
				iCount = entity.CurrentTable.Rows.Count;
				for ( int i=0;i<iCount;i++)
				{
					entity.SetCurrentRow(i);
					string WBSCode = entity.GetString("WBSCode");

					EntityData entityTask = DAL.EntityDAO.WBSDAO.GetTaskByCode(WBSCode);
					if (entityTask.HasRecord())
					{
						entity.CurrentRow["TaskName"] = entityTask.CurrentRow["TaskName"];
						entity.CurrentRow["CompletePercent"] = entityTask.CurrentRow["CompletePercent"];
					}
					entityTask.Dispose();

					DataTable tbGroup = BLL.WBSRule.GetTaskPersonNameGroupByType(WBSCode);
					entity.CurrentRow["UserNames"] = BLL.WBSRule.GetTaskPersonNameMaster(tbGroup);
				}
				this.dgTaskList.DataSource=new DataView( entity.CurrentTable ,"" , "",DataViewRowState.CurrentRows) ;
				this.dgTaskList.DataBind();

				Session["ContractAct"]=action;
				Session["ContractEntity"]=entity;
				entity.Dispose();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog( this.ToString(),ex,"���غ�ͬ���ݴ���");
				Response.Write(Rms.Web.JavaScript.Alert(true, "���غ�ͬ���ݳ���" + ex.Message));
			}

		}

		// ȡ����Ŀ�趨: �Ƿ���Բ鿴ԭ��ͬ�۸�
		private bool IsOldSumMoney()
		{
			bool isSystemProportion = true;// ȡ��ϵͳ�趨
			ProjectConfigStrategyBuilder sb = new ProjectConfigStrategyBuilder();
			sb.AddStrategy( new Strategy( ProjectConfigStrategyName.ProjectCode , base.project.ProjectCode) );
			string sql = sb.BuildMainQueryString();
			QueryAgent qa = new QueryAgent();
			EntityData projectConfig = qa.FillEntityData( "ProjectConfig",sql );
			qa.Dispose();
				
			DataRow[] drSelects = projectConfig.CurrentTable.Select( String.Format(  " ConfigName='ContractOldMoney'" ));
			if ( drSelects.Length>0)
			{
				if ( !drSelects[0].IsNull("ConfigData"))
					if((string)drSelects[0]["ConfigData"]=="1")
						isSystemProportion = false;
			}
			projectConfig.Dispose();

			return isSystemProportion;
		}

		private void AddNewEmptyRow( EntityData entity , string contractCode, string tableName, string keyColumnName, int rows  )
		{
			for ( int i=0;i<rows;i++)
			{
				DataRow dr = entity.GetNewRecord(tableName);
				dr[keyColumnName]=DAL.EntityDAO.SystemManageDAO.GetNewSysCode(keyColumnName);
				dr["ContractCode"]=contractCode;

				if (entity.Tables[tableName].Columns.Contains("PayConditionHtml")) 
				{
					//��������
					dr["PayConditionHtml"] = BLL.ContractRule.GetContractPayConditionHtml(dr[keyColumnName].ToString(), entity.Tables["ContractPayCondition"], true);
				}

				entity.AddNewRecord(dr,tableName);
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
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{    
			this.dgCostList.ItemCreated += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgCostList_ItemCreated);
			this.dgCostList.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgCostList_DeleteCommand);
			this.dgTaskList.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgTaskList_DeleteCommand);
			this.dgExecuteList.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgExecuteList_DeleteCommand);

		}
		#endregion



		protected void btnNewItem_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				string contractCode = this.txtContractCode.Value;
				string msg = SaveToSession();
				EntityData entity = (EntityData)Session["ContractEntity"];
				AddNewEmptyRow(entity,contractCode,"ContractAllocation","AllocateCode",5);

				this.dgCostList.DataSource=new DataView( entity.Tables["ContractAllocation"] ,"" ,"",DataViewRowState.CurrentRows) ;
				this.dgCostList.DataBind();
				Session["ContractEntity"]=entity;
				entity.Dispose();
				if ( msg != "" )
				{
					Response.Write( Rms.Web.JavaScript.Alert(true,msg));
				}
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "������ϸ����" + ex.Message));
			}
		}

		protected void btnNewPlan_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				string contractCode = this.txtContractCode.Value;
				string msg = SaveToSession();
				EntityData entity = (EntityData)Session["ContractEntity"];
				AddNewEmptyRow(entity,contractCode,"ContractExecutePlan","ContractExecutePlanCode",5);
				this.dgExecuteList.DataSource = new DataView(entity.Tables["ContractExecutePlan"],"","",DataViewRowState.CurrentRows) ;
				this.dgExecuteList.DataBind();
				Session["ContractEntity"]=entity;
				entity.Dispose();
				if ( msg != "" )
				{
					Response.Write( Rms.Web.JavaScript.Alert(true,msg));
				}
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "�����ƻ�����" + ex.Message));
			}
		}

		private string  SaveToSession()
		{

			string alertMsg = "";
			try
			{
				string contractCode = this.txtContractCode.Value;

				EntityData entity = (EntityData)Session["ContractEntity"];
				foreach ( DataGridItem li in this.dgExecuteList.Items)
				{
					string executePersonCode = ((HtmlInputHidden)li.FindControl("txtExecutePersonCode")).Value;
					string executeDate = ((AspWebControl.Calendar)li.FindControl("dtExecuteDate")).Value;
					string executeDetail = ((HtmlInputText)li.FindControl("txtExecuteDetail")).Value;
					string contractExecutePlanCode = li.Cells[0].Text;
					string executePersonName = (( HtmlInputHidden ) li.FindControl("txtExecutePersonName")).Value;

					foreach ( DataRow dr in entity.Tables["ContractExecutePlan"].Select(String.Format( "ContractExecutePlanCode='{0}'"  ,contractExecutePlanCode )))
					{
						dr["Executor"]=executePersonCode;
						dr["ExecutePersonName"]=executePersonName;
						if ( executeDate != "" )
							dr["ExecuteDate"]=executeDate;
						else
							dr["ExecuteDate"]=System.DBNull.Value;

						dr["ExecuteDetail"]=executeDetail;
					}
				}

				foreach ( DataGridItem li in this.dgCostList.Items)
				{
					string itemName = ((HtmlInputText)li.FindControl("txtItemName")).Value.Trim();
					string planningPayDate = ((AspWebControl.Calendar)li.FindControl("dtPlanningPayDate")).Value;
					RmsPM.Web.UserControls.InputCost ucCost = (RmsPM.Web.UserControls.InputCost)li.FindControl("ucCost");
					string payConditionText = ((HtmlInputText)li.FindControl("txtPayConditionText")).Value.Trim();

					string costCode = ucCost.Value.Trim();
					string costName = BLL.CBSRule.GetCostName(costCode);
					string money =((HtmlInputText)li.FindControl("txtMoney")).Value.Trim();
//
//					if ( costCode != "" )
//					{
//						if ( ! CBSRule.CheckCBSLeafNode(costCode))
//						{
//							costCode="";
//							alertMsg += itemName + "�����������ָ������ϸ������ ��" + "\n";
//						}
//					}

					if ( money != "" )
					{
						if ( ! Rms.Check.StringCheck.IsNumber(money))
						{
							money="";
							alertMsg += itemName + "������ʽ����ȷ ��" + "\n";
						}
					}

					string allocateCode = li.Cells[0].Text;
					foreach ( DataRow dr in entity.Tables["ContractAllocation"].Select(String.Format( "AllocateCode='{0}'"  ,allocateCode )))
					{
						dr["CostCode"]=costCode;
						dr["CostName"]=costName;
						dr["ItemName"]=itemName;
						dr["PayConditionText"]=payConditionText;
						if ( money !="" )
							dr["Money"]=decimal.Parse(money);
						else
							dr["Money"]=decimal.Zero;

						// ���÷�̯��ı�ǩ
						if ( dr.IsNull("AllocateLabel"))
							dr["AllocateLabel"]=DAL.EntityDAO.SystemManageDAO.GetNewSysCode("AllocateLabel");

						if ( planningPayDate != "" )
							dr["PlanningPayDate"] = planningPayDate;
						else
							dr["PlanningPayDate"] = System.DBNull.Value;
					}
				}

				Session["ContractEntity"]=entity;
				entity.Dispose();
				
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "�������" + ex.Message));
			}
			return alertMsg;

		}

		private void ClearData()
		{
			try
			{
				string contractCode = this.txtContractCode.Value;
				EntityData entity = (EntityData)Session["ContractEntity"];

				// �������
				foreach ( DataRow dr in entity.Tables["ContractAllocation"].Select("","",DataViewRowState.CurrentRows  ))
				{
					if ( dr.IsNull("ItemName"))
					{
						dr.Delete();
						continue;
					}
					else if ( (string)dr["ItemName"] == "" )
					{
						dr.Delete();
						continue;
					}

//					if ( dr.IsNull("Money"))
//					{
//						dr.Delete();
//						continue;
//					}
//					else if ( (decimal)dr["Money"] == decimal.Zero )
//					{
//						dr.Delete();
//						continue;
//					}

//					if ( dr.IsNull("PlanningPayDate"))
//					{
//						dr.Delete();
//						continue;
//					}

				}

				// ���ִ�мƻ�
				foreach ( DataRow dr in entity.Tables["ContractExecutePlan"].Select("","",DataViewRowState.CurrentRows  ))
				{
					if ( dr.IsNull("Executor"))
					{
						dr.Delete();
						continue;
					}
					else if ( (string)dr["Executor"] == "" )
					{
						dr.Delete();
						continue;
					}

					if ( dr.IsNull("ExecuteDate"))
					{
						dr.Delete();
						continue;
					}
				}

				Session["ContractEntity"]=entity;
				entity.Dispose();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "�������" + ex.Message));
			}
		}

		private void CopyTable ( DataTable dtSource, DataTable dtTarget, string keyCode, string contractCode )
		{

			foreach ( DataRow drSource in dtSource.Select( "","",DataViewRowState.CurrentRows ))
			{

				DataRow drNew = dtTarget.NewRow();
				drNew["ContractCode"] = contractCode;
				if ( keyCode != "" )
					drNew[keyCode] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode(keyCode);
				dtTarget.Rows.Add(drNew);

				foreach ( DataColumn dc in dtSource.Columns)
				{
					if ( dtTarget.Columns.Contains(dc.ColumnName) && dc.ColumnName!="ContractCode" && dc.ColumnName != keyCode )
					{
						drNew[dc.ColumnName] = drSource[dc.ColumnName];
					}
				}
			}
		}


		private void SaveBuilding()
		{
			try
			{
				string contractCode = this.txtContractCode.Value;
				string alloType = this.txtAlloType.Value;
				string codes = this.txtAllocateCodes.Value;
				EntityData entity=(EntityData)Session["ContractEntity"];
				// ��λ����
				entity.SetCurrentTable("Contract");
				if ( entity.HasRecord())
				{
					entity.CurrentRow["AlloType"] = alloType;
				}

				// ��ͬ��̯��¥��
				entity.SetCurrentTable("ContractBuilding");
				entity.DeleteAllTableRow( "ContractBuilding" );

				foreach ( string code in codes.Split(new char[]{','}))
				{
					if ( code != "" )
					{
						DataRow drNew = entity.GetNewRecord();
						drNew["ContractBuildingCode"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("ContractBuildingCode");
						drNew["ContractCode"] = contractCode;
						if ( alloType == "B" )
						{
							drNew["BuildingCode"] = code;
						}
						else
						{
							drNew["PBSUnitCode"] = code;
						}
						entity.AddNewRecord(drNew);
					}
				}
				entity.Dispose();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog( this.ToString(),ex,"" );
				Response.Write(Rms.Web.JavaScript.Alert(true, "�����̯����" + ex.Message));
			}
		}

		private string SaveBaseInfo()
		{

			try
			{
				string action=(string)Session["ContractAct"];

				string contractCode = this.txtContractCode.Value;
				string msg="";

				
				//���ݼ��
				string contractName = this.txtContractName.Value.Trim();
				string type = this.inputSystemGroup.Value;
				if ( contractName == "")
				{
					msg+="����д��ͬ���� ��" ;
				}

				// ��ͬ���ͱ�����д������ʱ���ټ���Ƿ��ܲ��������ͬ���޸ľͲ��ü���ˣ�
				if ( type == "" )
				{
					msg+="����д��ͬ���� ��" ;
				}
				else
				{
					if ( action == "Add" )
					{
						if ( ! user.HasTypeOperationRight("050102",type ))
							msg+="�����ܲ��������ͬ ��" ;
					}
				}

				if ( this.txtSupplierCode.Value == "" )
				{ 
					msg+="����д��Ӧ�� ��"  ;
				}

				if ( this.txthUnit.Value == "" )
				{
					msg+="����д��λ ��";
				}

				if ( msg != "" )
					return msg;

				// �����ͬ���Ĺ�Ӧ�̣��Զ�ͬ������Ӧ��
				if(action=="Change"&&this.txtSupplierCode.Value!=this.oldSupplier.Value)
				{
					// ȡ�õ�ǰ�������
					EntityData pentity = DAL.EntityDAO.PaymentDAO.GetPaymentByContractCode(contractCode);
					for(int i=0;i<pentity.CurrentTable.Rows.Count; i++)
					{
						pentity.CurrentTable.Rows[i]["SupplyCode"] = this.txtSupplierCode.Value;
						pentity.CurrentTable.Rows[i]["SupplyName"] = BLL.ProjectRule.GetSupplierName(this.txtSupplierCode.Value);
					}
					DAL.EntityDAO.PaymentDAO.UpdatePayment(pentity);
					pentity.Dispose();
				}

				EntityData entity = (EntityData)Session["ContractEntity"];
				entity.SetCurrentTable("Contract");
				DataRow dr = entity.CurrentRow;

				dr["ContractName"] = contractName;
				dr["ContractID"] = this.txtContractID.Value;
				dr["Type"] = type;
				dr["UnitCode"] = this.txthUnit.Value;
				dr["SupplierCode"] = this.txtSupplierCode.Value;
				dr["ContractPerson"] = this.txtContractPerson.Value;
				dr["ThirdParty"]=this.txtThirdParty.Value;

				dr["WBSCode"] = this.txtWBSCode.Value;

				string tmp = "";
				if(action=="Change")
				{
					dr["ChangeReason"]=this.txtChangeReason.Value;
					dr["ChangeRemark"]=this.txtChangeRemark.Value;
					dr["ChangePerson"]=user.UserCode;
					dr["ChangeDate"]=DateTime.Now.ToString("yyyy-MM-dd");

					if(base.user.HasOperationRight("190121")) // ԭʼ��ͬ�ܼ۲���
					{
						tmp = this.oldMoney.Value.Replace(",","");
						dr["oldSumMoney"] = (tmp.Length>0)?double.Parse(tmp):0.0;
					}

				}				

				dr["Remark"] = this.txtRemark.Value;
				dr["ContractObject"] = this.txtContractObject.Value;

				dr["LastModifyPerson"] = base.user.UserCode;
				dr["LastModifyDate"] = DateTime.Now.ToString("yyyy-MM-dd");

				decimal totalMoney = BLL.MathRule.SumColumn(entity.Tables["ContractAllocation"],"Money");
				dr["TotalMoney"]=totalMoney;
				
				if(action=="Change")
				{
					// ����ǵ�һ�κ�ͬ�ܶ����ͬ���ʱ��ȡ����һ�δ��ԭʼ���
					tmp = this.hBeforeAccountTotalMoney.Value.Replace(",","");
					dr["BeforeAccountTotalMoney"]=(tmp.Length>0)?double.Parse(tmp):0.0;
				}
				else
				{
					// ����ʱ�����ͬ�ܶ�
					dr["BeforeAccountTotalMoney"]=totalMoney;
				}
				Session["ContractEntity"]=entity;
				entity.Dispose();
				return "";

			}
			catch( Exception ex )
			{
				throw ex;
			}
		}

		/*
		/// <summary>
		/// �����ͬ��ع���
		/// </summary>
		private void SaveTaskContract() 
		{
			try 
			{
				string contractCode = this.txtContractCode.Value;

				EntityData entity = (EntityData)Session["ContractEntity"];
				entity.SetCurrentTable("TaskContract");

				DataTable dtCondition = entity.Tables["ContractPayCondition"];

				int icount = entity.CurrentTable.Rows.Count;
				for(int i=icount-1;i>=0;i--)
				{
					DataRow dr = entity.CurrentTable.Rows[i];

					string WBSCode = BLL.ConvertRule.ToString(dr["WBSCode"]);
					if (dtCondition.Select("WBSCode='" + WBSCode + "'").Length == 0) 
					{
						dr.Delete();
					}
				}

				DataView dv = new DataView(dtCondition, "", "", DataViewRowState.CurrentRows);

				foreach(DataRowView drCondition in dv) 
				{
					string WBSCode = BLL.ConvertRule.ToString(drCondition["WBSCode"]);

					if (entity.CurrentTable.Select("WBSCode='" + WBSCode + "'").Length == 0) 
					{
						DataRow drNew = entity.CurrentTable.NewRow();

						drNew["TaskContractCode"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("TaskContractCode");
						drNew["ContractCode"] = contractCode;
						drNew["WBSCode"] = WBSCode;

						entity.CurrentTable.Rows.Add(drNew);
					}
				}

				Session["ContractEntity"]=entity;
			}
			catch( Exception ex )
			{
				throw ex;
			}
		}
*/

		protected void btnSave_ServerClick(object sender, System.EventArgs e)
		{
			string contractCode = this.txtContractCode.Value;
			string action=(string)Session["ContractAct"];
			try
			{
				string unitCode ="";
				

				string msg = SaveToSession();
				if ( msg != "" )
				{
					Response.Write( Rms.Web.JavaScript.Alert(true,msg));
					return;
				}
				ClearData();

				SaveBuilding();
				string baseMsg = SaveBaseInfo();
				if ( baseMsg != "" )
				{
					Response.Write( Rms.Web.JavaScript.Alert(true,baseMsg));
					return;
				}

//				SaveTaskContract();

				if(action=="Add" || action=="Edit")
				{
					EntityData entity = (EntityData)Session["ContractEntity"];
					entity.SetCurrentTable("Contract");
					unitCode = entity.GetString("UnitCode");
					DAL.EntityDAO.ContractDAO.SubmitAllStandard_Contract(entity);
					entity.Dispose();
				}

				if(action=="Change")
				{
					EntityData entityTemp=(EntityData)Session["ContractEntity"];
					EntityData entity=new EntityData("Standard_Contract");
					contractCode=DAL.EntityDAO.SystemManageDAO.GetNewSysCode("ContractCode");
				
					//���ƺ�ͬ������Ϣ
					CopyTable(entityTemp.Tables["Contract"],entity.Tables["Contract"],"",contractCode);
					CopyTable(entityTemp.Tables["ContractBuilding"],entity.Tables["ContractBuilding"],"ContractBuildingCode",contractCode);
					//CopyTable(entityTemp.Tables["ContractPaymentPlan"],entity.Tables["ContractPaymentPlan"],"ContractPaymentPlanCode",contractCode);
					CopyTable(entityTemp.Tables["ContractAllocation"],entity.Tables["ContractAllocation"],"AllocateCode",contractCode);
					CopyTable(entityTemp.Tables["ContractExecutePlan"],entity.Tables["ContractExecutePlan"],"ContractExecutePlanCode",contractCode);

					entity.SetCurrentTable("Contract");
					entity.CurrentRow["Status"]=4;

					DAL.EntityDAO.ContractDAO.SubmitAllStandard_Contract(entity);
					entity.Dispose();
					entityTemp.Dispose();

				}

				//BLL.ResourceRule.SetResourceAccessRange(contractCode,"0501",unitCode);

				// ���渽��
				this.myAttachMentAdd.SaveAttachMent(contractCode);

				Session["ContractEntity"]=null;
				Session["ContractAct"]=null;

				/************************ ���̿������� **************************/
				string ActCode = "";
				if(action == "Add")
				{
					if(projectCode == "")
					{
						projectCode = this.project.ProjectCode;
					}
                    WorkFlowControl.Migrated_WorkFlowToolbar WorkFlowToolbar1 = new WorkFlowControl.Migrated_WorkFlowToolbar();
					WorkFlowToolbar1.ApplicationCode = contractCode;
					WorkFlowToolbar1.ActCode = "";//����������
					WorkFlowToolbar1.FlowName = "��ͬ�������";
					WorkFlowToolbar1.SystemUserCode = this.user.UserCode;
					WorkFlowToolbar1.SourceUrl = "../WorkFlowControl/";
					WorkFlowToolbar1.Transactor = this.user.UserCode;
					WorkFlowToolbar1.TransactUnit = txthUnit.Value;
					//WorkFlowToolbar1.ToolbarDataBind();
					WorkFlowToolbar1.Save();
					WorkFlowToolbar1.SaveCasePropertyValue("������",this.user.UserCode);
					WorkFlowToolbar1.SaveCasePropertyValue("������Ŀ",BLL.ProjectRule.GetUnitByProject(projectCode));
					WorkFlowToolbar1.SaveCasePropertyValue("���벿��",this.txthUnit.Value);
					WorkFlowToolbar1.SaveCasePropertyValue("����",this.txtContractName.Value);
					ActCode = WorkFlowToolbar1.ActCode;
				}
				/***************************************************************/

				if(this.PurchaseSelect.Value != "")
				{
					EntityData entityp = DAL.EntityDAO.PurchaseFlowDAO.GetPurchaseFlowByCode(this.PurchaseSelect.Value);
					DataRow dr = entityp.CurrentRow;
					dr["ContractCode"] = contractCode;
					DAL.EntityDAO.PurchaseFlowDAO.UpdatePurchaseFlow(entityp);
					entityp.Dispose();
				}
				if(action == "Edit")
				{
					ActCode = Request["ActCode"] + "";
				}


				Response.Write(Rms.Web.JavaScript.ScriptStart);
				//Response.Write( "window.opener.location.reload();" );
				//Response.Write(Rms.Web.JavaScript.WinClose(false));
				Response.Write("window.parent.location.href='../ContractFlow/ContractFlowManage.aspx?ActCode="+ActCode+"';");
				Response.Write(Rms.Web.JavaScript.ScriptEnd);
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "�������" + ex.Message));
			}
		}

		private void dgCostList_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try
			{
				string msg = SaveToSession();

				string contractCode = this.txtContractCode.Value;
				string action=Request.QueryString["Act"]+"";

				string codeTemp = e.Item.Cells[0].Text;

				// ����Ƿ��йظ÷����������
				// ����ϸ, ͳ�Ʒ��÷ֽ��и�������ĸ������
				PaymentItemStrategyBuilder sb = new PaymentItemStrategyBuilder();
				sb.AddStrategy( new Strategy( PaymentItemStrategyName.ContractCode,contractCode  ) );
				sb.AddStrategy( new Strategy( PaymentItemStrategyName.AllocateCode,codeTemp  ) );
				string sql = sb.BuildQueryViewString();
				QueryAgent qa = new QueryAgent();
				EntityData paymentItem = qa.FillEntityData("V_PaymentItem",sql);
				qa.Dispose();
				bool isHas =( paymentItem.HasRecord());
				paymentItem.Dispose();
				if ( isHas )
				{
					Response.Write( Rms.Web.JavaScript.Alert(true,"�ÿ����Ѿ����������л��Ѿ�֧����������ɾ�� ��") );
					return;
				}

				EntityData entity = (EntityData)Session["ContractEntity"];
				foreach ( DataRow dr in entity.Tables["ContractAllocation"].Select(String.Format( "AllocateCode='{0}'" ,codeTemp)))
					dr.Delete();

				//ɾ����ظ�������
				foreach ( DataRow dr in entity.Tables["ContractPayCondition"].Select( String.Format("AllocateCode='{0}'" ,codeTemp ) ))
					dr.Delete();

				this.dgCostList.DataSource=new DataView( entity.Tables["ContractAllocation"] ,"" , "",DataViewRowState.CurrentRows) ;
				this.dgCostList.DataBind();

				Session["ContractEntity"]=entity;
				entity.Dispose();
				if ( msg != "" )
				{
					Response.Write( Rms.Web.JavaScript.Alert(true,msg));
				}

			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "ɾ������" + ex.Message));
			}
		}

		private void dgExecuteList_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try
			{
				string msg = SaveToSession();
				string codeTemp = e.Item.Cells[0].Text;
				EntityData entity = (EntityData)Session["ContractEntity"];

				foreach ( DataRow dr in entity.Tables["ContractExecutePlan"].Select( String.Format("ContractExecutePlanCode='{0}'" ,codeTemp ) ))
					dr.Delete();

				this.dgExecuteList.DataSource = new DataView(entity.Tables["ContractExecutePlan"],"","",DataViewRowState.CurrentRows);
				this.dgExecuteList.DataBind();
				Session["ContractEntity"]=entity;
				entity.Dispose();
				if ( msg != "" )
				{
					Response.Write( Rms.Web.JavaScript.Alert(true,msg));
				}
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "ɾ������" + ex.Message));
			}
		}

		/// <summary>
		/// �޸ĸ��������󷵻أ�ˢ����ϸ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnPayConditionReturn_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				string msg = SaveToSession();
				EntityData entity = (EntityData)Session["ContractEntity"];

				//���¸���ʱ��
				string AllocateCode = this.txtConditionAllocateCode.Value;
				string sPayDate = this.txtConditionPayDate.Value;
				if (sPayDate != "") 
				{
					DataRow[] drs = entity.Tables["ContractAllocation"].Select("AllocateCode='" + AllocateCode + "'");
					if (drs.Length > 0) 
					{
						drs[0]["PlanningPayDate"] = sPayDate;
					}
				}

				this.dgCostList.DataSource=new DataView( entity.Tables["ContractAllocation"] ,"" ,"",DataViewRowState.CurrentRows) ;
				this.dgCostList.DataBind();

				this.dgTaskList.DataSource=new DataView( entity.Tables["TaskContract"] ,"" ,"",DataViewRowState.CurrentRows) ;
				this.dgTaskList.DataBind();

				Session["ContractEntity"]=entity;
				entity.Dispose();
				if ( msg != "" )
				{
					Response.Write( Rms.Web.JavaScript.Alert(true,msg));
				}
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ������ϸ����" + ex.Message));
			}
		}

		/// <summary>
		/// ɾ����ع���
		/// </summary>
		/// <param name="source"></param>
		/// <param name="e"></param>
		private void dgTaskList_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try
			{
				string msg = SaveToSession();

				string contractCode = this.txtContractCode.Value;
				string action=Request.QueryString["Act"]+"";

				string codeTemp = this.dgTaskList.DataKeys[e.Item.ItemIndex].ToString();

				EntityData entity = (EntityData)Session["ContractEntity"];

				// ���ù����Ƿ��ѳ�Ϊ��ͬ��������
				string WBSCode = e.Item.Cells[0].Text;
				DataTable dtCondition = entity.Tables["ContractPayCondition"];
				if (dtCondition.Select("WBSCode='" + WBSCode + "'").Length > 0)
				{
					Response.Write( Rms.Web.JavaScript.Alert(true,"�ù����ѳ�Ϊ��ͬ��������������ɾ�� ��") );
					return;
				}

				foreach ( DataRow dr in entity.Tables["TaskContract"].Select(String.Format( "TaskContractCode='{0}'" ,codeTemp)))
					dr.Delete();

				this.dgTaskList.DataSource=new DataView( entity.Tables["TaskContract"] ,"" , "",DataViewRowState.CurrentRows) ;
				this.dgTaskList.DataBind();

				Session["ContractEntity"]=entity;
				entity.Dispose();
				if ( msg != "" )
				{
					Response.Write( Rms.Web.JavaScript.Alert(true,msg));
				}

			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "ɾ������" + ex.Message));
			}
		}

		/// <summary>
		/// ��ӹ������
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnAddTaskReturn_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				string msg = SaveToSession();

				string contractCode = this.txtContractCode.Value;
				string action=Request.QueryString["Act"]+"";

				string WBSCode = this.txtAddTaskCode.Value;

				EntityData entity = (EntityData)Session["ContractEntity"];
				DataTable dtTask = entity.Tables["TaskContract"];

				if (dtTask.Select("WBSCode='" + WBSCode + "'").Length == 0) 
				{
					DataRow drNew = dtTask.NewRow();

					drNew["TaskContractCode"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("TaskContractCode");
					drNew["ContractCode"] = contractCode;
					drNew["WBSCode"] = WBSCode;

					EntityData entityTask = DAL.EntityDAO.WBSDAO.GetTaskByCode(WBSCode);
					if (entityTask.HasRecord())
					{
						drNew["TaskName"] = entityTask.CurrentRow["TaskName"];
						drNew["CompletePercent"] = entityTask.CurrentRow["CompletePercent"];
					}
					entityTask.Dispose();

					DataTable tbGroup = BLL.WBSRule.GetTaskPersonNameGroupByType(WBSCode);
					drNew["UserNames"] = BLL.WBSRule.GetTaskPersonNameMaster(tbGroup);

					dtTask.Rows.Add(drNew);
				}

				this.dgTaskList.DataSource=new DataView( dtTask ,"" , "",DataViewRowState.CurrentRows) ;
				this.dgTaskList.DataBind();

				Session["ContractEntity"]=entity;
				entity.Dispose();
				if ( msg != "" )
				{
					Response.Write( Rms.Web.JavaScript.Alert(true,msg));
				}

			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ӹ��������" + ex.Message));
			}
		}

		private void dgCostList_ItemCreated(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			
			if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
			{
				RmsPM.Web.UserControls.InputCost ucCost = (RmsPM.Web.UserControls.InputCost)e.Item.FindControl("ucCost");
				ucCost.ProjectCode = projectCode;
			}
		}
		protected void btnSelectProject_ServerClick(object sender, System.EventArgs e)
		{
			projectCode = this.txtProjectCode.Value;

			//			base.ProjectCode = projectCode;
			base.project.Reset( projectCode);
			Session["ProjectCode"] = projectCode;
			Session["Project"] = base.project;

			EntityData entity = DAL.EntityDAO.SystemManageDAO.GetSystemUserByCode( base.user.UserCode);
			entity.CurrentRow["LastProjectCode"] = projectCode;
			DAL.EntityDAO.SystemManageDAO.UpdateSystemUser(entity);
			entity.Dispose();
			
			


			IniPage();
			LoadData();

			Response.Write( Rms.Web.JavaScript.ScriptStart );
			//Response.Write( "window.open('ProjectLeftBar.aspx?ProjectCode=" + projectCode + "','contents');" );

			//2004.12.29 ��openerʱ��Ҫ����
			Response.Write("if (window.opener) window.opener.location = window.opener.location;");
			//			Response.Write( Rms.Web.JavaScript.OpenerReload(false) );

			Response.Write( Rms.Web.JavaScript.ScriptEnd );
	
		}


	}
}
