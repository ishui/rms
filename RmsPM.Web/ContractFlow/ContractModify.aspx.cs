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
	/// ContractModify 的摘要说明。
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
				ApplicationLog.WriteLog( this.ToString(),ex,"初始化页面错误。");
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
						this.SpanScript.InnerHtml = "<script language='javascript'> OpenMiddleWindow('../SelectBox/SelectProjectCanAccess.aspx' ,'选择项目');</script>";
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
					this.lblTitle.Text="合同申请";

					//付款条件加字段
//					entity.Tables["ContractPayCondition"].Columns.Add("TaskName");
//					entity.Tables["ContractPayCondition"].Columns.Add("DelayTypeName");
				}
				else if(action=="Edit")
				{
					entity=DAL.EntityDAO.ContractDAO.GetStandard_ContractByCode(contractCode);
					this.lblTitle.Text="合同修改";
				}
				else if(action=="Change")
				{
					entity=DAL.EntityDAO.ContractDAO.GetStandard_ContractByCode(contractCode);
					entity.CurrentRow["Status"]=4;
					this.lblTitle.Text="合同变更";

				}
				else
				{
					Response.Write(Rms.Web.JavaScript.PageTo(true,"Contract.aspx?ProjectCode=" + projectCode ));
					Response.End();
				}

				AddNewEmptyRow(entity,contractCode,"ContractAllocation","AllocateCode",5);
				AddNewEmptyRow(entity,contractCode,"ContractExecutePlan","ContractExecutePlanCode",5);


				// 基本信息
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

				/************************ 采购单 *************************/
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

				// 如果是合同变更,且打开开关
				bool isShow = IsOldSumMoney();
				if(action=="Change"&&isShow)
				{
					// 合同变更显示
					this.lblOldMoney.Attributes.Add("class","form-item");
					this.lblOldMoney.InnerText = "原始金额";
					this.oldMoney.Visible = true;
					this.oldMoney.Value = RmsPM.BLL.StringRule.BuildShowNumberString(entity.GetDecimal("oldSumMoney"));
					
					if(base.user.HasOperationRight("190121")) // 原始合同总价参照
						this.oldMoney.Disabled = false;
					else
						this.oldMoney.Disabled = true;
				}
				
				// 如果不是变更
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
					this.txtAllocateName.Value = "项目";
				else
				{
					this.txtAllocateCodes.Value = buildingCodes;
					this.txtAllocateName.Value = buildingNames;
				}

				// 执行计划
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

				//款项分解
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

					//付款条件
					entity.CurrentRow["PayConditionHtml"] = BLL.ContractRule.GetContractPayConditionHtml(allocateCode, entity.Tables["ContractPayCondition"], true);
				}
				this.dgCostList.DataSource=new DataView( entity.CurrentTable ,"" , "",DataViewRowState.CurrentRows) ;
				this.dgCostList.DataBind();

				//相关工作
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
				ApplicationLog.WriteLog( this.ToString(),ex,"加载合同数据错误");
				Response.Write(Rms.Web.JavaScript.Alert(true, "加载合同数据出错：" + ex.Message));
			}

		}

		// 取得项目设定: 是否可以查看原合同价格
		private bool IsOldSumMoney()
		{
			bool isSystemProportion = true;// 取得系统设定
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
					//付款条件
					dr["PayConditionHtml"] = BLL.ContractRule.GetContractPayConditionHtml(dr[keyColumnName].ToString(), entity.Tables["ContractPayCondition"], true);
				}

				entity.AddNewRecord(dr,tableName);
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
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "新增明细出错：" + ex.Message));
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "新增计划出错：" + ex.Message));
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
//							alertMsg += itemName + "：费用项必须指定到明细费用项 ！" + "\n";
//						}
//					}

					if ( money != "" )
					{
						if ( ! Rms.Check.StringCheck.IsNumber(money))
						{
							money="";
							alertMsg += itemName + "：金额格式不正确 ！" + "\n";
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

						// 费用分摊项的标签
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "保存出错：" + ex.Message));
			}
			return alertMsg;

		}

		private void ClearData()
		{
			try
			{
				string contractCode = this.txtContractCode.Value;
				EntityData entity = (EntityData)Session["ContractEntity"];

				// 清除款项
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

				// 清除执行计划
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "清除出错：" + ex.Message));
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
				// 单位工程
				entity.SetCurrentTable("Contract");
				if ( entity.HasRecord())
				{
					entity.CurrentRow["AlloType"] = alloType;
				}

				// 合同分摊的楼栋
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "保存分摊出错：" + ex.Message));
			}
		}

		private string SaveBaseInfo()
		{

			try
			{
				string action=(string)Session["ContractAct"];

				string contractCode = this.txtContractCode.Value;
				string msg="";

				
				//数据检查
				string contractName = this.txtContractName.Value.Trim();
				string type = this.inputSystemGroup.Value;
				if ( contractName == "")
				{
					msg+="请填写合同名称 ！" ;
				}

				// 合同类型必须填写；新增时候再检查是否能操作该类合同，修改就不用检查了，
				if ( type == "" )
				{
					msg+="请填写合同类型 ！" ;
				}
				else
				{
					if ( action == "Add" )
					{
						if ( ! user.HasTypeOperationRight("050102",type ))
							msg+="您不能操作这类合同 ！" ;
					}
				}

				if ( this.txtSupplierCode.Value == "" )
				{ 
					msg+="请填写供应商 ！"  ;
				}

				if ( this.txthUnit.Value == "" )
				{
					msg+="请填写单位 ！";
				}

				if ( msg != "" )
					return msg;

				// 如果合同更改供应商，自动同步请款单供应商
				if(action=="Change"&&this.txtSupplierCode.Value!=this.oldSupplier.Value)
				{
					// 取得当前所有请款
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

					if(base.user.HasOperationRight("190121")) // 原始合同总价参照
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
					// 存的是第一次合同总额，当合同变更时，取出上一次存的原始金额
					tmp = this.hBeforeAccountTotalMoney.Value.Replace(",","");
					dr["BeforeAccountTotalMoney"]=(tmp.Length>0)?double.Parse(tmp):0.0;
				}
				else
				{
					// 新增时插入合同总额
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
		/// 保存合同相关工作
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
				
					//复制合同基本信息
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

				// 保存附件
				this.myAttachMentAdd.SaveAttachMent(contractCode);

				Session["ContractEntity"]=null;
				Session["ContractAct"]=null;

				/************************ 流程控制增加 **************************/
				string ActCode = "";
				if(action == "Add")
				{
					if(projectCode == "")
					{
						projectCode = this.project.ProjectCode;
					}
                    WorkFlowControl.Migrated_WorkFlowToolbar WorkFlowToolbar1 = new WorkFlowControl.Migrated_WorkFlowToolbar();
					WorkFlowToolbar1.ApplicationCode = contractCode;
					WorkFlowToolbar1.ActCode = "";//工具栏设置
					WorkFlowToolbar1.FlowName = "合同申请审核";
					WorkFlowToolbar1.SystemUserCode = this.user.UserCode;
					WorkFlowToolbar1.SourceUrl = "../WorkFlowControl/";
					WorkFlowToolbar1.Transactor = this.user.UserCode;
					WorkFlowToolbar1.TransactUnit = txthUnit.Value;
					//WorkFlowToolbar1.ToolbarDataBind();
					WorkFlowToolbar1.Save();
					WorkFlowToolbar1.SaveCasePropertyValue("申请人",this.user.UserCode);
					WorkFlowToolbar1.SaveCasePropertyValue("申请项目",BLL.ProjectRule.GetUnitByProject(projectCode));
					WorkFlowToolbar1.SaveCasePropertyValue("申请部门",this.txthUnit.Value);
					WorkFlowToolbar1.SaveCasePropertyValue("主题",this.txtContractName.Value);
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "保存出错：" + ex.Message));
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

				// 检查是否有关该费用项的请款付款
				// 请款单明细, 统计费用分解中各个款项的付款请款
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
					Response.Write( Rms.Web.JavaScript.Alert(true,"该款项已经处于申请中或已经支付过，不能删除 ！") );
					return;
				}

				EntityData entity = (EntityData)Session["ContractEntity"];
				foreach ( DataRow dr in entity.Tables["ContractAllocation"].Select(String.Format( "AllocateCode='{0}'" ,codeTemp)))
					dr.Delete();

				//删除相关付款条件
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "删除出错：" + ex.Message));
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "删除出错：" + ex.Message));
			}
		}

		/// <summary>
		/// 修改付款条件后返回，刷新明细
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnPayConditionReturn_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				string msg = SaveToSession();
				EntityData entity = (EntityData)Session["ContractEntity"];

				//更新付款时间
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示款项明细出错：" + ex.Message));
			}
		}

		/// <summary>
		/// 删除相关工作
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

				// 检查该工作是否已成为合同付款条件
				string WBSCode = e.Item.Cells[0].Text;
				DataTable dtCondition = entity.Tables["ContractPayCondition"];
				if (dtCondition.Select("WBSCode='" + WBSCode + "'").Length > 0)
				{
					Response.Write( Rms.Web.JavaScript.Alert(true,"该工作已成为合同付款条件，不能删除 ！") );
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "删除出错：" + ex.Message));
			}
		}

		/// <summary>
		/// 添加工作项返回
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "添加工作项出错：" + ex.Message));
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

			//2004.12.29 无opener时不要报错
			Response.Write("if (window.opener) window.opener.location = window.opener.location;");
			//			Response.Write( Rms.Web.JavaScript.OpenerReload(false) );

			Response.Write( Rms.Web.JavaScript.ScriptEnd );
	
		}


	}
}
