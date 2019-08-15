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
using RmsPM.DAL.QueryStrategy;
using Infragistics.WebUI.WebDataInput;

namespace RmsPM.Web.Contract
{
	/// <summary>
	/// ContractPaymentPlanModify ��ժҪ˵����
	/// </summary>
	public partial class ContractPaymentPlanModify : PageBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{

			if ( !IsPostBack )
			{
				Page_Init();
				LoadData();
			}
		}

		protected void Page_Init()
		{
			string ud_sContractCode = Request["ContractCode"] + "";
			string ud_sProjectCode = Request["ProjectCode"] + "";
			string ud_sContractCostCode = Request["ContractCostCode"] + "";
			string ud_sAction = Request.QueryString["Act"] + "";

			this.txtContractCode.Value = ud_sContractCode;

			ucCostBudgetDtl.ProjectCode = ud_sProjectCode;

			ucCostBudgetDtl.Visible = false;

			EntityData entity;

			switch ( ud_sAction )
			{
				case "EditPlan":
					entity = DAL.EntityDAO.ContractDAO.GetStandard_ContractByCode(ud_sContractCode);
					break;
				default:
					entity = this.ReadEntitySession();
					break;

			}

			if ( !entity.Tables["ContractCost"].Columns.Contains("CostName") )
			{
				entity.Tables["ContractCost"].Columns.Add("CostName");
			}

			foreach ( DataRow dr in entity.Tables["ContractCost"].Rows)
			{
				ucCostBudgetDtl.CostBudgetSetCode = dr["CostBudgetSetCode"].ToString();
				ucCostBudgetDtl.CostCode = dr["CostCode"].ToString();
				dr["CostName"] = ucCostBudgetDtl.CostName;
			}

			this.WriteEntitySession(entity);

			DataView  ud_dvCost = new DataView(entity.Tables["ContractCost"]);

			this.ddlCost.DataSource = ud_dvCost;
			this.ddlCost.DataTextField = "CostName";
			this.ddlCost.DataValueField = "ContractCostCode";
			this.ddlCost.DataBind();

			if ( ud_sContractCostCode != "" )
			{
				int ud_iIndexOfItem = this.ddlCost.Items.IndexOf(this.ddlCost.Items.FindByValue(ud_sContractCostCode));

				if ( ud_iIndexOfItem >= 0 )
				{
					this.ddlCost.SelectedIndex = ud_iIndexOfItem;
					this.ddlCost.Enabled = false;
				}
			}

			EntityData entityC = DAL.EntityDAO.ClaimsExpressionsDAO.GetAllClaimsExpressions();

			DataView ud_dvClaimsExpressions = new DataView(entityC.Tables["ClaimsExpressions"],"Status = 1","",DataViewRowState.CurrentRows);
            
			this.ddlClaimsExpressions.DataSource = ud_dvClaimsExpressions;
			this.ddlClaimsExpressions.DataTextField = "ClaimsExpressionsName";
			this.ddlClaimsExpressions.DataValueField = "ClaimsExpressionsCode";
			this.ddlClaimsExpressions.DataBind();
		}

		private void WriteEntitySession( EntityData entity)
		{
			string action=Request.QueryString["Act"]+"";

			Session["ContractEntity" + action]=entity;
		}

		private EntityData ReadEntitySession()
		{
			string action = Request.QueryString["Act"]+"";

			return (EntityData)Session["ContractEntity" + action];
		}
	
		private void LoadData()
		{
			try
			{
				string ud_sContractCode = Request.QueryString["ContractCode"] + "";
				string ud_sProjectCode = Request["ProjectCode"] + "";
				string ud_sAction = Request.QueryString["Act"] + "";

				string ud_sContractCostCode = this.ddlCost.SelectedValue;

				this.txtContractCode.Value = ud_sContractCode;

				EntityData entity;

				switch ( ud_sAction )
				{
					case "EditPlan":
						entity = DAL.EntityDAO.ContractDAO.GetStandard_ContractByCode(ud_sContractCode);
						this.WriteEntitySession(entity);
						break;
					default:
						entity = this.ReadEntitySession();
						break;

				}

				if ( entity.Tables["ContractCostPlan"].Rows.Count == 0 )
				{
					AddNewPlanEmptyRow(entity,this.ddlCost.SelectedValue,5);
                    this.WriteEntitySession(entity);
				}

				// ������Ϣ
				entity.SetCurrentTable("Contract");
				this.lblContractID.Text = entity.GetString("ContractID");
				this.lblContractName.Text = entity.GetString("ContractName");

				this.LabelType.Text = BLL.SystemGroupRule.GetSystemGroupFullName( entity.GetString("Type"));
				string unitCode = entity.GetString("UnitCode");
				this.lblUnitName.Text = BLL.SystemRule.GetUnitName(unitCode);

				foreach ( DataRow dr in entity.Tables["ContractCost"].Select( string.Format("ContractCostCode='{0}'",ud_sContractCostCode)))
				{
					this.ccStartDay.Value = dr["PlanStartDate"].ToString();
					this.ccEndDay.Value = dr["PlanEndDate"].ToString();
				}


				this.BindCostPlanDataGrid( entity);
/*
				//��ع���
				entity.SetCurrentTable("TaskContract");
				entity.CurrentTable.Columns.Add("TaskName");
				entity.CurrentTable.Columns.Add("UserNames");
				entity.CurrentTable.Columns.Add("CompletePercent");

				//����ֽ�
				entity.SetCurrentTable("ContractAllocation");
				entity.CurrentTable.Columns.Add("CostName");
				entity.CurrentTable.Columns.Add("PayConditionHtml");
				int  iCount = entity.CurrentTable.Rows.Count;
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

				Session["ContractEntity"]=entity;
				entity.Dispose();
*/

			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog( this.ToString(),ex,"���غ�ͬ���ݴ���");
				Response.Write(Rms.Web.JavaScript.Alert(true, "���غ�ͬ���ݳ���" + ex.Message));
			}

		}

		private void BindCostPlanDataGrid( EntityData entity )
		{

			string ud_sRowsFilter = string.Format("ContractCostCode='{0}'",this.ddlCost.SelectedValue);
			DataView ud_dvCostPlan = new DataView(entity.Tables["ContractCostPlan"],ud_sRowsFilter,"",DataViewRowState.CurrentRows);

			this.dgCostList.DataSource = ud_dvCostPlan;
			this.dgCostList.DataBind();
		}

		private void AddNewPlanEmptyRow( EntityData entity ,string pm_sContractCostCode, int rows  )
		{

			string ud_sContractCode = txtContractCode.Value;

			for ( int i=0;i<rows;i++)
			{
				DataRow dr = entity.GetNewRecord("ContractCostPlan");
				dr["ContractCostPlanCode"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("ContractCostPlanCode");
				dr["ContractCode"] = ud_sContractCode;
				dr["ContractCostCode"] = pm_sContractCostCode;

//				if (entity.Tables["ContractCostPlan"].Columns.Contains("PayConditionHtml")) 
//				{
//					//��������
//					dr["PayConditionHtml"] = BLL.ContractRule.GetContractPayConditionHtml(dr[keyColumnName].ToString(), entity.Tables["ContractPayCondition"], true);
//				}

				entity.AddNewRecord(dr,"ContractCostPlan");
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

		}
		#endregion

		override protected void InitEventHandler()
		{
			base.InitEventHandler();
			this.dgCostList.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgCostList_DeleteCommand);

		}

		protected void btnNewItem_ServerClick(object sender, System.EventArgs e)
		{
			
			try
			{
				string ud_sContractCode = this.txtContractCode.Value;
				string msg = SaveToSession();

				EntityData entity = this.ReadEntitySession();

				AddNewPlanEmptyRow(entity,this.ddlCost.SelectedValue,5);

				this.BindCostPlanDataGrid(entity);
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


		private string  SaveToSession()
		{

			string alertMsg = "";
			try
			{
				string ud_sContractCode = this.txtContractCode.Value;
				string ud_sProjectCode = Request["ProjectCode"] + "";

				string ud_sContractCostCode = this.ddlCost.SelectedValue;

				EntityData entity = this.ReadEntitySession();

				foreach( DataRow dr in entity.Tables["ContractCost"].Select(string.Format("ContractCostCode='{0}'",ud_sContractCostCode)))
				{

					if ( this.ccStartDay.Value == "" )
					{
						dr["PlanStartDate"] =  DBNull.Value;
					}
					else
					{
						dr["PlanStartDate"] = this.ccStartDay.Value;
					}

					if ( this.ccEndDay.Value == "" )
					{
						dr["PlanEndDate"] =  DBNull.Value;
					}
					else
					{
						dr["PlanEndDate"] = this.ccEndDay.Value;
					}

				}


				foreach ( DataGridItem ud_Item in this.dgCostList.Items)
				{
					AspWebControl.Calendar ud_dtPlanningPayDate = (AspWebControl.Calendar)ud_Item.FindControl("dtPlanningPayDate");
					WebNumericEdit ud_txtMoney = (WebNumericEdit)ud_Item.FindControl("txtMoney");
					HtmlInputText ud_txtPayConditionText = (HtmlInputText)ud_Item.FindControl("txtPayConditionText");

                    string ud_sContractCostPlanCode = ud_Item.Cells[1].Text;

                    bool ud_bModify = false;

					foreach ( DataRow dr in entity.Tables["ContractCostPlan"].Select(String.Format( "ContractCostPlanCode='{0}'" ,ud_sContractCostPlanCode )))
					{
						if (ud_dtPlanningPayDate.Value == "" )
						{
							dr["PlanningPayDate"] =  DBNull.Value;
						}
						else
						{
							dr["PlanningPayDate"] = ud_dtPlanningPayDate.Value;
						}

						dr["Money"] = ud_txtMoney.ValueDecimal;
						dr["PayConditionText"] = ud_txtPayConditionText.Value;

                        ud_bModify = true;
					}

                    if (!ud_bModify)
                    {
                        DataRow ud_drNew = entity.Tables["ContractCostPlan"].NewRow();

                        ud_drNew["ContractCode"] = ud_sContractCode;
                        ud_drNew["ContractCostCode"] = ud_sContractCostCode;
                        ud_drNew["ContractCostPlanCode"] = ud_sContractCostPlanCode;

                        if (ud_dtPlanningPayDate.Value == "")
                        {
                            ud_drNew["PlanningPayDate"] = DBNull.Value;
                        }
                        else
                        {
                            ud_drNew["PlanningPayDate"] = ud_dtPlanningPayDate.Value;
                        }

                        ud_drNew["Money"] = ud_txtMoney.ValueDecimal;
                        ud_drNew["PayConditionText"] = ud_txtPayConditionText.Value;

                        entity.Tables["ContractCostPlan"].Rows.Add(ud_drNew);

                    }
				}

				this.WriteEntitySession(entity);
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
				string ud_sContractCode = this.txtContractCode.Value;
				EntityData entity = this.ReadEntitySession();
				
				string ud_sRowFilter = String.Format("ContractCostCode={0}",this.ddlCost.SelectedValue);

				//����յĸ���ƻ�
				foreach ( DataRow drPlan in entity.Tables["ContractCostPlan"].Select(ud_sRowFilter,"",DataViewRowState.CurrentRows  ))
				{
					if ( BLL.ConvertRule.ToString(drPlan["PlanningPayDate"]) == "" && BLL.ConvertRule.ToString(drPlan["Money"]) == "0" )
					{
						drPlan.Delete();
						continue;
					}

					if ( BLL.ConvertRule.ToString(drPlan["PlanningPayDate"]) == "" || BLL.ConvertRule.ToString(drPlan["Money"]) == "0" )
					{
						Response.Write(Rms.Web.JavaScript.Alert(true, "�뽫����ƻ���д����"));

						return;
					}
				}
				this.WriteEntitySession( entity );
				entity.Dispose();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "�������" + ex.Message));
			}
		}

		protected void btnSave_ServerClick(object sender, System.EventArgs e)
		{
			string ud_sContractCode = Request.QueryString["ContractCode"] + "";
			string ud_sProjectCode = Request["ProjectCode"] + "";
			string ud_sAction = Request.QueryString["Act"] + "";

			string ud_sContractCostCode = this.ddlCost.SelectedValue;

			this.txtContractCode.Value = ud_sContractCode;

			try
			{
				string msg = SaveToSession();
				if ( msg != "" )
				{
					Response.Write( Rms.Web.JavaScript.Alert(true,msg));
					return;
				}
				this.ClearData();


				if ( ud_sAction == "EditPlan" )
				{
					EntityData entity = this.ReadEntitySession();

					DAL.EntityDAO.ContractDAO.SubmitAllStandard_Contract(entity);

				}
				else
				{
					Response.Write(Rms.Web.JavaScript.ScriptStart);
					Response.Write(string.Format("window.opener.{0}();", "Page_Reload"));
					Response.Write(Rms.Web.JavaScript.ScriptEnd);			
				}

			

				Response.Write(Rms.Web.JavaScript.ScriptStart);
				Response.Write(Rms.Web.JavaScript.WinClose(false));
				Response.Write(Rms.Web.JavaScript.ScriptEnd);
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "�������" + ex.Message));
			}
		}

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
		/// ɾ������ƻ���ϸ
		/// </summary>
		/// <param name="source"></param>
		/// <param name="e"></param>
		private void dgCostList_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
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

					//ɾ����ظ���ƻ�
					foreach ( DataRow dr in entity.Tables["ContractCostPlan"].Select( String.Format("ContractCostPlanCode='{0}'" ,ud_sPlanCode ) ))
					{
						dr.Delete();
					}

					WriteEntitySession(entity);
					BindCostPlanDataGrid(entity);
					entity.Dispose();


				}

			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "ɾ������ƻ�����" + ex.Message));
			}
		}

		protected void btnBuildPlan_Click(object sender, System.EventArgs e)
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

					if ( ccStartDay.Value == "" || ccEndDay.Value == "" )
					{
						Response.Write( Rms.Web.JavaScript.Alert( true,"����д��ʼ�ͽ���ʱ��") );
						return;
					}

					if ( DateTime.Compare( DateTime.Parse(ccStartDay.Value),DateTime.Parse(ccEndDay.Value)) > 0 )
					{
						Response.Write( Rms.Web.JavaScript.Alert( true,"��ʼʱ��ӦС�ڵ��ڽ���ʱ��") );
						return;
					}

					EntityData entityC = DAL.EntityDAO.ClaimsExpressionsDAO.GetStandard_ClaimsExpressionsByCode(this.ddlClaimsExpressions.SelectedValue);
 
					EntityData entity = this.ReadEntitySession();

					string ud_sRowsFilter = string.Format ( "ContractCostCode = '{0}'",this.ddlCost.SelectedValue);
					string ud_sSort = "PlanningPayDate";

					DateTime ud_dtStart = DateTime.Parse(ccStartDay.Value);
					DateTime ud_dtEnd = DateTime.Parse(ccEndDay.Value);

					decimal ud_deTotalMoney = Decimal.Zero;
					decimal ud_deTotalPayMoney = Decimal.Zero;

					foreach ( DataRow dr in entity.Tables["ContractCost"].Select(ud_sRowsFilter))
					{
						if ( dr["Money"] == DBNull.Value )
						{
							Response.Write( Rms.Web.JavaScript.Alert(true,"����д������ϸ���") );
							return;
						}

						ud_deTotalMoney = (decimal)dr["Money"];
					}


					foreach(DataRow drPlan in entity.Tables["ContractCostPlan"].Select(ud_sRowsFilter,ud_sSort,DataViewRowState.CurrentRows ))
					{
						if ( drPlan["PlanningPayDate"] != DBNull.Value )
						{
							DateTime ud_dtPayDate = (DateTime)drPlan["PlanningPayDate"];

							float ud_fPeriodPer =  (float)(ud_dtPayDate.ToFileTime() - ud_dtStart.ToFileTime() ) / (float)(ud_dtEnd.ToFileTime() - ud_dtStart.ToFileTime());
					
							float ud_fClaimsPer;

							float[] ud_fPeriod = new float[2];
							float[] ud_fClaims = new float[2];

							ud_fPeriod[0] = 0;
							ud_fClaims[0] = 0;

							ud_fPeriod[1] = 1;
							ud_fClaims[1] = 1;

							foreach ( DataRow dr in  entityC.Tables["ClaimsExpressionsDetail"].Select(string.Format("Period <= {0}",ud_fPeriodPer.ToString()),"Period DESC",DataViewRowState.CurrentRows))
							{
								ud_fPeriod[0] = float.Parse(dr["Period"].ToString());
								ud_fClaims[0] = float.Parse(dr["Claims"].ToString());
								break;
							}

							foreach ( DataRow dr in  entityC.Tables["ClaimsExpressionsDetail"].Select(string.Format("Period >= {0}",ud_fPeriodPer.ToString()),"Period",DataViewRowState.CurrentRows))
							{
								ud_fPeriod[1] = float.Parse(dr["Period"].ToString());
								ud_fClaims[1] = float.Parse(dr["Claims"].ToString());
								break;
							}

							if ( ud_fPeriod[1] != ud_fPeriod[0] )
							{
								ud_fClaimsPer = ( ud_fClaims[1] - ud_fClaims[0] ) / ( ud_fPeriod[1] - ud_fPeriod[0]) * ( ud_fPeriodPer - ud_fPeriod[0] )  + ud_fClaims[0];
							}
							else
							{
								ud_fClaimsPer = ud_fClaims[0];
							}

							decimal ud_deMoney = ud_deTotalMoney * (decimal)ud_fClaimsPer - ud_deTotalPayMoney;

							ud_deTotalPayMoney += ud_deMoney;

							drPlan["Money"] = ud_deMoney;

						}
					}


					BindCostPlanDataGrid(entity);
				}
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "���ɸ���ƻ�����" + ex.Message));
			}
		}




	}
}
