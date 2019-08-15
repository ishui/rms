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
using System.Data.SqlClient;


namespace RmsPM.Web.Contract
{
	/// <summary>
	/// ContractChange ��ժҪ˵����
	/// </summary>
	public partial class ContractChange : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlInputText txtContractID;
		protected System.Web.UI.HtmlControls.HtmlInputText txtContractName;
		protected System.Web.UI.HtmlControls.HtmlInputText txtSupplierName;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtSupplierCode;


		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if ( !IsPostBack )
			{
				IniPage();
				LoadData();
			}
			this.myAttachMentAdd.AttachMentType = "ContractChangeAttachMent"; 
			this.myAttachMentAdd.MasterCode = this.txtContractChangeCode.Value;  
		}

		private void IniPage()
		{
			try
			{
				string projectCode = Request["ProjectCode"]+"";
				string contractCode = Request["ContractCode"]+"";

				this.txtContractCode.Value = contractCode;

                BLL.PageFacade.LoadDictionarySelect(this.ddlChangeType, "��ͬ�������", "");

			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog( this.ToString(),ex,"��ʼ��ҳ�����");
				Rms.Web.JavaScript.Alert(true,"��ʼ��ҳ�����" + ex.Message );
			}
		}

		private void LoadData()
		{
			try
			{
				string projectCode = Request["ProjectCode"]+"";
				string contractCode = Request["ContractCode"]+"";
				string contractChangeCode = Request["ContractChangeCode"]+"";
				string action=Request.QueryString["Act"]+"";

				ArrayList ar = user.GetResourceRight(contractCode,"Contract");
				if ( ! ar.Contains("050104") )
				{
					Response.Redirect( "../RejectAccess.aspx" );
					Response.End();
				}

                if (this.up_sPMName.ToUpper() == "YefengPM".ToUpper())
                {
                    this.lblHintChangeReason.Visible = true;
                }

                EntityData entity = DAL.EntityDAO.ContractDAO.GetStandard_ContractByCode(contractCode);

				if (action=="Change")
				{
					contractChangeCode = AddNewEmptyChangeRow(entity,contractCode);

					entity.Tables["Contract"].Rows[0]["ContractLabel"] = contractChangeCode;
//                    entity.Tables["Contract"].Rows[0]["ChangeCount"] = (int)entity.Tables["Contract"].Rows[0]["ChangeCount"] + 1;
                    entity.Tables["Contract"].Rows[0]["ChangeStatus"] = 1;

//					entity.Tables["Contract"].Rows[0]["Status"] = 4;

					this.CopyCostTableToChangeTable(entity,contractCode,contractChangeCode);

				}
				else if (action=="Edit")
				{
					if ( contractChangeCode == "" )
					{
						Rms.Web.JavaScript.Alert(true,"��Ч�ĺ�ͬ�����" );
						return;
					}
				}

				this.txtContractChangeCode.Value = contractChangeCode;

				// ��ͬ������Ϣ
				entity.SetCurrentTable("Contract");

				lblProjectName.Text =  BLL.ProjectRule.GetProjectName(projectCode);
				lblContractID.Text = entity.GetString("ContractID");
				lblContractName.Text = entity.GetString("ContractName");
				lblSupplierName.Text = BLL.ProjectRule.GetSupplierName( entity.GetString("SupplierCode"));

				// ��ʾ��ͬ���
				this.ShowContractMoney(entity,contractChangeCode);

                // 2007.1.25 ��ͷ����ʾ��ұ���
                string ForeignMoneyType = BLL.ContractRule.GetForeignMoneyType(entity);
                if (ForeignMoneyType != "")
                {
                    this.lblForeignMoneyType.Text = ForeignMoneyType;
                    this.lblForeignMoneyType2.Text = ForeignMoneyType;
                    this.lblForeignMoneyType3.Text = ForeignMoneyType;
                    this.lblForeignMoneyType4.Text = ForeignMoneyType;
                    this.lblForeignMoneyType5.Text = ForeignMoneyType;
                    this.lblForeignMoneyType6.Text = ForeignMoneyType;
                    this.lblForeignMoneyType7.Text = ForeignMoneyType;
                    this.lblForeignMoneyType8.Text = ForeignMoneyType;
                    this.lblForeignMoneyType9.Text = ForeignMoneyType;
                }
                
                //��ͬ���������Ϣ
				entity.SetCurrentTable("ContractChange");

            
				foreach ( DataRow dr in entity.CurrentTable.Select(String.Format("ContractChangeCode='{0}'",contractChangeCode)))
				{
		            if( dr["changetype"].ToString()=="����"){
                        Server.Transfer("ContracStrikeModify.aspx");
                    }
                    txtVoucher.Value = dr["Voucher"].ToString();
					txtChangeId.Value =dr["ContractChangeId"].ToString();
					txtChangeReason.Value = dr["ChangeReason"].ToString();

					txtSupplierChangeMoney.Value = dr["SupplierChangeMoney"].ToString();
					txtConsultantAuditMoney.Value = dr["ConsultantAuditMoney"].ToString();
					txtProjectAuditMoney.Value = dr["ProjectAuditMoney"].ToString();
                    this.ddlChangeType.SelectedIndex = this.ddlChangeType.Items.IndexOf(this.ddlChangeType.Items.FindByValue(dr["ChangeType"].ToString()));


				}

				//������ϸ
				entity.SetCurrentTable("ContractCostChange");

				this.dgCostListBind(entity.CurrentTable,contractChangeCode);

                this.gvNexusBind(entity.Tables["ContractNexus"]);

				WriteEntitySession(entity);
				entity.Dispose();
			}
			catch( Exception ex )
			{
				ApplicationLog.WriteLog( this.ToString(),ex,"���غ�ͬ���ݴ���");
				Response.Write(Rms.Web.JavaScript.Alert(true, "���غ�ͬ���ݳ���" + ex.Message));
			}
		}

		protected override void InitEventHandler()
		{
			base.InitEventHandler ();
			this.dgCostList.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgCostList_DeleteCommand);
			this.dgCostList.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgCostList_ItemDataBound);
            this.gvNexusList.RowDataBound += new GridViewRowEventHandler(this.gvNexusList_RowDataBound);
		}


		private string AddNewEmptyChangeRow( EntityData entity , string contractCode)
		{
			string tableName = "ContractChange";
			
			DataRow drChange = entity.GetNewRecord(tableName);
			string contractChangeCode = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("ContractChangeCode");

			decimal TotalMoney,OriginalMoney,TotalChangeMoney;
			decimal ChangeMoney,NewMoney;

            TotalMoney = (decimal)entity.Tables["Contract"].Rows[0]["TotalMoney"];
			OriginalMoney = (decimal)entity.Tables["Contract"].Rows[0]["OriginalMoney"];
			TotalChangeMoney = TotalMoney - OriginalMoney;

			ChangeMoney = Decimal.Zero;
			NewMoney = TotalMoney + ChangeMoney;

			drChange["ContractChangeCode"]=contractChangeCode;
			drChange["ContractCode"] = contractCode;
			drChange["Money"] = TotalMoney;
			drChange["OriginalMoney"] = OriginalMoney;
			drChange["TotalChangeMoney"] = TotalChangeMoney;
			drChange["ChangeMoney"] = ChangeMoney;
			drChange["NewMoney"] = NewMoney;
			drChange["Status"] = 1;

			entity.AddNewRecord(drChange,tableName);

			return contractChangeCode;
		}

		private void CopyCostTableToChangeTable( EntityData entity,string pm_sContractCode,string pm_sContractChangeCode )
		{
			foreach ( DataRow drCost in entity.Tables["ContractCost"].Rows )
			{
				string ud_sContractCostCode = drCost["ContractCostCode"].ToString();

				foreach ( DataRow drCash in entity.Tables["ContractCostCash"].Select(string.Format("ContractCostCode = '{0}'",ud_sContractCostCode)) )
				{
					string ud_sContractCostChangeCode = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("ContractCostChangeCode");

					DataRow drCostChange = entity.GetNewRecord("ContractCostChange");

					drCostChange["ContractCostChangeCode"] = ud_sContractCostChangeCode;
					drCostChange["ContractCode"] = pm_sContractCode;
					drCostChange["ContractCostCode"] = drCost["ContractCostCode"];
					drCostChange["ContractChangeCode"] = pm_sContractChangeCode;
					drCostChange["CostCode"] = drCost["CostCode"];
					drCostChange["CostBudgetDtlCode"] = drCost["CostBudgetDtlCode"];
					drCostChange["CostBudgetSetCode"] = drCost["CostBudgetSetCode"];
					drCostChange["PBSType"] = drCost["PBSType"];
					drCostChange["PBSCode"] = drCost["PBSCode"];
					drCostChange["Description"] = drCost["Description"];
					drCostChange["Status"] = 1;

					drCostChange["ContractCostCashCode"] = drCash["ContractCostCashCode"];

					decimal ud_deCostMoney,ud_deCostOriginalMoney,ud_deCostTotalChangeMoney;
					decimal ud_deCostCash,ud_deCostOriginalCash,ud_deCostTotalChangeCash,ud_deExchangeRate;

					ud_deCostCash = BLL.ConvertRule.ToDecimal(drCash["Cash"]);
					ud_deCostOriginalCash = BLL.ConvertRule.ToDecimal(drCash["OriginalCash"]);
					ud_deCostMoney = BLL.ConvertRule.ToDecimal(drCash["Money"]);
					ud_deExchangeRate = BLL.ConvertRule.ToDecimal(drCash["ExchangeRate"]);

					ud_deCostOriginalMoney = ud_deCostOriginalCash * ud_deExchangeRate;
					ud_deCostTotalChangeCash = ud_deCostCash - ud_deCostOriginalCash;
					ud_deCostTotalChangeMoney = ud_deCostMoney - ud_deCostOriginalMoney;

					drCostChange["Cash"] = ud_deCostCash;
					drCostChange["OriginalCash"] = ud_deCostOriginalCash;
					drCostChange["TotalChangeCash"] = ud_deCostTotalChangeCash;
					drCostChange["NewCash"] = ud_deCostCash;
					drCostChange["ChangeCash"] = Decimal.Zero;

					drCostChange["Money"] = ud_deCostMoney;
					drCostChange["OriginalMoney"] = ud_deCostOriginalMoney;
					drCostChange["TotalChangeMoney"] = ud_deCostTotalChangeMoney;
					drCostChange["NewMoney"] = ud_deCostMoney;
					drCostChange["ChangeMoney"] = Decimal.Zero;

                    drCostChange["ExchangeRate"] = ud_deExchangeRate;
					drCostChange["MoneyType"] = drCash["MoneyType"];

					entity.Tables["ContractCostChange"].Rows.Add(drCostChange);
				}
			}

		}

		private void WriteEntitySession( EntityData entity)
		{
			string action=Request.QueryString["Act"]+"";

			Session["ContractEntity" + action]=entity;
		}

		private EntityData ReadEntitySession()
		{
			string action=Request.QueryString["Act"]+"";

			return (EntityData)Session["ContractEntity" + action];
		}

		private void ClearSession()
		{
			string action=Request.QueryString["Act"]+"";

			Session["ContractEntity" + action]=null;
		}


		private void ShowContractMoney( EntityData entity,string contractChangeCode )
		{
			entity.SetCurrentTable("Contract");

			decimal TotalMoney,TotalChangeMoney,OriginalMoney,NewTotalMoney,ChangeMoney,BudgetMoney,AdjustMoney;;

			OriginalMoney = entity.GetDecimal("OriginalMoney");
			BudgetMoney = entity.GetDecimal("BudgetMoney");
			AdjustMoney = entity.GetDecimal("AdjustMoney");

			TotalMoney = Decimal.Zero;
			TotalChangeMoney = Decimal.Zero;

			foreach ( DataRow dr in entity.Tables["ContractChange"].Select( string.Format("ContractChangeCode={0}",contractChangeCode),"",System.Data.DataViewRowState.CurrentRows) )
			{
				TotalMoney = dr["Money"] != DBNull.Value ? (decimal)dr["Money"] : Decimal.Zero;
				TotalChangeMoney = dr["TotalChangeMoney"] != DBNull.Value ? (decimal)dr["TotalChangeMoney"] : Decimal.Zero;
			}

            //��ͬԭ�ҽ��
            decimal OriginalCash, NewTotalCash, ChangeCash, TotalChangeCash;

            string[] arrField = { "OriginalCash" };
            decimal[] arrValue = RmsPM.BLL.MathRule.SumColumn(entity.Tables["ContractCostCash"], arrField);
            OriginalCash = arrValue[0];

            string[] arrField2 = { "NewMoney", "NewCash", "TotalChangeCash", "ChangeCash" };
            decimal[] arrValue2 = RmsPM.BLL.MathRule.SumColumn(entity.Tables["ContractCostChange"].Select(String.Format("ContractChangeCode='{0}' and Status in (0,1)", contractChangeCode)), arrField2);
            NewTotalMoney = arrValue2[0];
            NewTotalCash = arrValue2[1];
            TotalChangeCash = arrValue2[2];
            ChangeCash = arrValue2[3];

            ChangeMoney = NewTotalMoney - TotalMoney;
            
            hidOriginalMoney.Value = OriginalMoney.ToString();
			hidTotalChangeMoney.Value = TotalChangeMoney.ToString();

			txtBudgetMoney.Value = BudgetMoney.ToString("N");
			txtAdjustMoney.Value = AdjustMoney.ToString("N");
            txtOriginalMoney.Value = OriginalCash.ToString("N");
			txtTotalChangeMoney.Value = TotalChangeCash.ToString("N");
			txtChangeMoney.Value = ChangeCash.ToString("N");
			txtNewTotalMoney.Value = NewTotalCash.ToString("N");
		}

		private void dgCostList_DeleteCommand(object sender, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string contractCode = txtContractCode.Value;
			string contractChangeCode = txtContractChangeCode.Value;

			string msg = SaveToSession();

			EntityData entity = ReadEntitySession();

			string contractCostChangeCode = e.Item.Cells[0].Text.Trim();

			// ��������ϸ�Ƿ�Ϊ���״̬

			if( HttpUtility.HtmlDecode(e.Item.Cells[1].Text).Trim() == "" )
			{

				// ������ϸΪ����״̬

				// �������ƻ��ļ�¼
				foreach ( DataRow dr in entity.Tables["ContractCostChange"].Select(String.Format("ContractCostChangeCode={0}",contractCostChangeCode),"",DataViewRowState.CurrentRows  ))
				{
					dr.Delete();
				}
			}
			else
			{
				// ������ϸΪ���״̬

				// �����ͬ������ϸ�ļ�¼�����������ֻ�ǽ�����Ϊ 0��
				foreach ( DataRow dr in entity.Tables["ContractCostChange"].Select(String.Format("ContractCostChangeCode={0}",contractCostChangeCode),"",DataViewRowState.CurrentRows  ))
				{
					decimal CostMoney = dr["Money"] != DBNull.Value ? (decimal)dr["Money"] : Decimal.Zero;
					
					dr["ChangeMoney"] = Decimal.Zero;
					dr["NewMoney"] = CostMoney;

					((WebNumericEdit)e.Item.FindControl("txtCostChangeMoney")).ValueDecimal = Decimal.Zero;
					((HtmlInputText)e.Item.FindControl("txtCostNewMoney")).Value = CostMoney.ToString("N");
				}				
			}

			this.dgCostListBind(entity.Tables["ContractCostChange"],contractChangeCode);

			WriteEntitySession(entity);
			ShowContractMoney(entity,contractChangeCode);
			entity.Dispose();
		}

		private void dgCostList_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			switch ( e.Item.ItemType )
			{
				case ListItemType.Header:
					break;

				case ListItemType.Item:
				case ListItemType.AlternatingItem:
					string ud_sProjectCode = Request["ProjectCode"] + "";

					UserControls.ExchangeRateControl ud_ucExchangeRate = (UserControls.ExchangeRateControl)e.Item.FindControl("ucExchangeRate");
					UserControls.InputCostBudgetDtl ud_ucCostBudgetDtl = (UserControls.InputCostBudgetDtl)e.Item.FindControl("ucCostBudgetDtl");

					WebNumericEdit ud_txtCostTotalChangeCash = (WebNumericEdit)e.Item.FindControl("txtCostTotalChangeCash");

					DataRowView ud_drvItem = (DataRowView)e.Item.DataItem;


					ud_ucCostBudgetDtl.ProjectCode = ud_sProjectCode;
					ud_txtCostTotalChangeCash.Enabled = false;

					ud_ucExchangeRate.IsShowTitle = false;
					ud_ucExchangeRate.IsAllowCashChange = false;
					ud_ucExchangeRate.Cash = BLL.ConvertRule.ToDecimal(ud_drvItem["Cash"]);

					if ( ud_drvItem["MoneyType"].ToString() != "" )
					{
						ud_ucExchangeRate.ExchangeRate = BLL.ConvertRule.ToDecimal(ud_drvItem["ExchangeRate"]);
						ud_ucExchangeRate.MoneyType = ud_drvItem["MoneyType"].ToString();
					}

					if ( HttpUtility.HtmlDecode(e.Item.Cells[1].Text).Trim() != "" )
					{
						((UserControls.InputCostBudgetDtl)e.Item.FindControl("ucCostBudgetDtl")).Enable = false;
						ud_ucExchangeRate.EditMode = false;
					}
					else
					{
						((UserControls.InputCostBudgetDtl)e.Item.FindControl("ucCostBudgetDtl")).Enable = true;
						ud_ucExchangeRate.EditMode = true;

					}

					ud_ucExchangeRate.BindControl();

					break;
				case ListItemType.Footer:
					//��ʾ�ϼƽ��
					((Label)e.Item.FindControl("lblSumCostOriginalMoney")).Text = BLL.MathRule.GetDecimalShowString(ViewState["SumCostOriginalMoney"]);
					((Label)e.Item.FindControl("lblSumCostTotalChangeMoney")).Text = BLL.MathRule.GetDecimalShowString(ViewState["SumCostTotalChangeMoney"]);
					((Label)e.Item.FindControl("lblSumCostChangeMoney")).Text = BLL.MathRule.GetDecimalShowString(ViewState["SumCostChangeMoney"]);
					((Label)e.Item.FindControl("lblSumCostNewMoney")).Text = BLL.MathRule.GetDecimalShowString(ViewState["SumCostNewMoney"]);
					break;
				default:
					break;

			}
		}


		private void dgCostListBind( DataTable dt,string contractChangeCode)
		{
			DataView dv = new DataView(dt,String.Format("ContractChangeCode='{0}' and Status in (0,1)",contractChangeCode),"",DataViewRowState.CurrentRows);

			ViewState["SumCostOriginalMoney"] = BLL.MathRule.SumColumn(dt.Select(String.Format("ContractChangeCode='{0}' and Status in (0,1)",contractChangeCode)),"OriginalMoney");
			ViewState["SumCostTotalChangeMoney"] = BLL.MathRule.SumColumn(dt.Select(String.Format("ContractChangeCode='{0}' and Status in (0,1)",contractChangeCode)),"TotalChangeMoney");
			ViewState["SumCostChangeMoney"] = BLL.MathRule.SumColumn(dt.Select(String.Format("ContractChangeCode='{0}' and Status in (0,1)",contractChangeCode)),"ChangeMoney");
			ViewState["SumCostNewMoney"] = BLL.MathRule.SumColumn(dt.Select(String.Format("ContractChangeCode='{0}' and Status in (0,1)",contractChangeCode)),"NewMoney");

			dgCostList.DataSource = dv;
			dgCostList.DataBind();

		}

		private string  SaveToSession()
		{
			string alertMsg = "";
			try
			{
				string contractCode = this.txtContractCode.Value;
				string projectCode = Request["ProjectCode"] + "";
			
				EntityData entity = ReadEntitySession();

				string contractLabel = txtContractChangeCode.Value;


				//��ͬ���������Ϣ
				entity.SetCurrentTable("ContractChange");
				foreach ( DataRow dr in entity.CurrentTable.Select(String.Format("ContractChangeCode='{0}'",contractLabel),"",DataViewRowState.CurrentRows))
				{
					dr["Voucher"] = txtVoucher.Value;
					dr["ContractChangeId"] = txtChangeId.Value;
					dr["ChangeReason"] = txtChangeReason.Value;

					dr["SupplierChangeMoney"] = txtSupplierChangeMoney.ValueDecimal;
					dr["ConsultantAuditMoney"] = txtConsultantAuditMoney.ValueDecimal;
					dr["ProjectAuditMoney"] = txtProjectAuditMoney.ValueDecimal;

                    dr["ChangeType"] = this.ddlChangeType.SelectedItem.Value;
				}

				//������ϸ
				entity.SetCurrentTable("ContractCostChange");

				foreach ( DataGridItem li in this.dgCostList.Items)
				{
					string contractCostChangeCode = li.Cells[0].Text;
					string contractCostCode = HttpUtility.HtmlDecode(li.Cells[1].Text).Trim();

					UserControls.InputCostBudgetDtl ucCostBudgetDtl = (UserControls.InputCostBudgetDtl)li.FindControl("ucCostBudgetDtl");
					UserControls.ExchangeRateControl ud_ucExchangeRate = (UserControls.ExchangeRateControl)li.FindControl("ucExchangeRate");

					WebNumericEdit txtCostChangeCash = (WebNumericEdit)li.FindControl("txtCostChangeCash");
					HtmlInputText txtDescription = (HtmlInputText)li.FindControl("txtDescription");

					foreach ( DataRow dr in entity.CurrentTable.Select(String.Format( "ContractCostChangeCode='{0}'"  ,contractCostChangeCode),"",DataViewRowState.CurrentRows))
					{
						decimal ud_deCostCash,ud_deCostOriginalCash,ud_deCostTotalChangeCash,ud_deCostChangeCash,ud_deCostNewCash,ud_deExchangeRate;
						decimal ud_deCostMoney,ud_deCostOriginalMoney,ud_deCostTotalChangeMoney,ud_deCostChangeMoney,ud_deCostNewMoney;

						ud_deCostCash = (decimal)dr["Cash"];
						ud_deCostOriginalCash = (decimal)dr["OriginalCash"];
						ud_deCostTotalChangeCash = (decimal)dr["TotalChangeCash"];


						ud_deCostMoney = (decimal)dr["Money"];
						ud_deCostOriginalMoney = (decimal)dr["OriginalMoney"];
						ud_deCostTotalChangeMoney = (decimal)dr["TotalChangeMoney"];


						ud_deCostChangeCash = txtCostChangeCash.ValueDecimal;
						ud_deExchangeRate = ud_ucExchangeRate.ExchangeRate;

						ud_deCostNewCash = ud_deCostOriginalCash + ud_deCostTotalChangeCash + ud_deCostChangeCash;

						ud_deCostNewMoney = ud_deCostNewCash * ud_deExchangeRate;
						ud_deCostChangeMoney = ud_deCostChangeCash * ud_deExchangeRate;

						dr["ChangeCash"] = ud_deCostChangeCash;
						dr["NewCash"] = ud_deCostNewCash;

						dr["ChangeMoney"] = ud_deCostChangeMoney;
						dr["NewMoney"] = ud_deCostNewMoney;
						dr["Description"] = txtDescription.Value;

						if ( contractCostCode == "" )
						{
							dr["CostCode"] = ucCostBudgetDtl.CostCode;
							dr["CostBudgetSetCode"] = ucCostBudgetDtl.CostBudgetSetCode;
							dr["PBSType"] = ucCostBudgetDtl.PBSType;
							dr["PBSCode"] = ucCostBudgetDtl.PBSCode;

							dr["MoneyType"] = ud_ucExchangeRate.MoneyType;
							dr["ExchangeRate"] = ud_ucExchangeRate.ExchangeRate;
						}

					}
				}

				WriteEntitySession(entity);
				entity.Dispose();
				
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "����Session����" + ex.Message));
			}
			return alertMsg;

		}

		private void AddNewEmptyCostChangeRow( EntityData entity , string contractCode, string contractChangeCode, int rows )
		{
			string contractCostChangeCode = "";

			for ( int i=0;i<rows;i++)
			{
				DataRow drCostChange = entity.GetNewRecord("ContractCostChange");
				contractCostChangeCode = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("ContractCostChangeCode");

				drCostChange["ContractCostChangeCode"]=contractCostChangeCode;
				drCostChange["ContractCode"]=contractCode;
				drCostChange["ContractChangeCode"]=contractChangeCode;
				drCostChange["Money"] = Decimal.Zero;
				drCostChange["OriginalMoney"] = Decimal.Zero;
				drCostChange["TotalChangeMoney"] = Decimal.Zero;
				drCostChange["NewMoney"] = Decimal.Zero;
				drCostChange["ChangeMoney"] = Decimal.Zero;

				drCostChange["Cash"] = Decimal.Zero;
				drCostChange["OriginalCash"] = Decimal.Zero;
				drCostChange["TotalChangeCash"] = Decimal.Zero;
				drCostChange["NewCash"] = Decimal.Zero;
				drCostChange["ChangeCash"] = Decimal.Zero;

				drCostChange["Status"] = 1;

				entity.AddNewRecord(drCostChange,"ContractCostChange");
			}
		}

		protected void btnNewCostItem_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				string contractCode = this.txtContractCode.Value;
				string msg = SaveToSession();

				EntityData entity = ReadEntitySession();

				string contractChangeCode = txtContractChangeCode.Value;

				AddNewEmptyCostChangeRow(entity,contractCode,contractChangeCode,5);

				dgCostListBind(entity.Tables["ContractCostChange"],contractChangeCode);

				entity.Dispose();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "����������ϸ����" + ex.Message));
			}
			
		}

		/// <summary>
		/// ����ռ�¼
		/// </summary>
		private string ClearData()
		{
			try
			{
				string ErrMsg = "";
				string contractCode = this.txtContractCode.Value;
				EntityData entity = ReadEntitySession();

                if ((this.up_sPMName.ToUpper() == "YefengPM".ToUpper()) && (this.txtChangeReason.Value.Trim() == ""))
                {
                    ErrMsg = "��������ԭ��ժҪ";
                    return ErrMsg;
                }
                
                // �����ֵΪ�յļ�¼
				foreach ( DataRow dr in entity.Tables["ContractCostChange"].Select("","",DataViewRowState.CurrentRows))
				{
					if ( (decimal)dr["ChangeCash"] != Decimal.Zero && dr["CostCode"].ToString().Trim() == "" )
					{
						ErrMsg = "�뽫��ͬ������ϸ��д����1��";//1
						return ErrMsg;
					}

					if ( (decimal)dr["ChangeCash"] == Decimal.Zero && dr["CostCode"].ToString().Trim() == "") 
					{
						dr.Delete();
					}
				}

				WriteEntitySession(entity);
				entity.Dispose();
				return ErrMsg;
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "����ռ�¼����" + ex.Message));
				return "����ռ�¼����" + ex.Message;
			}
		}

		/// <summary>
		/// ����
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnSave_ServerClick(object sender, System.EventArgs e)
		{
			string contractCode = this.txtContractCode.Value;
			string action=Request.QueryString["Act"]+"";
			string projectCode = Request["ProjectCode"] + "";


			try
			{
				string msg = SaveToSession();
				if ( msg != "" )
				{
					Response.Write( Rms.Web.JavaScript.Alert(true,msg));
					return;
				}
                
                string ClearMsg = ClearData();
				if ( ClearMsg != "" )
				{
					Response.Write( Rms.Web.JavaScript.Alert(true,ClearMsg));
					return;
				}

				EntityData entity = ReadEntitySession();

				string contractChangeCode = txtContractChangeCode.Value;

				entity.SetCurrentTable("Contract");

				// ͳ�ƺ�ͬ���

				decimal TotalMoney,TotalChangeMoney,OriginalMoney,NewTotalMoney,ChangeMoney;

				TotalMoney = entity.GetDecimal("TotalMoney");
				OriginalMoney = entity.GetDecimal("OriginalMoney");
				TotalChangeMoney = TotalMoney - OriginalMoney;
				ChangeMoney = BLL.MathRule.SumColumn(entity.Tables["ContractCostChange"].Select(String.Format("ContractChangeCode='{0}' and Status in (0,1)",contractChangeCode)),"ChangeMoney");
//				NewTotalMoney = BLL.MathRule.SumColumn(entity.Tables["ContractCostChange"].Select(String.Format("ContractChangeCode='{0}' and Status in (0,1)",contractChangeCode)),"NewMoney");
				NewTotalMoney = TotalMoney + ChangeMoney;

				foreach ( DataRow dr in entity.Tables["ContractChange"].Select(String.Format("ContractChangeCode='{0}'",contractChangeCode)))
				{
					dr["ChangeMoney"] = ChangeMoney;
					dr["NewMoney"] = NewTotalMoney;

					// ��¼����ˡ����ʱ��
					dr["ChangePerson"] = user.UserCode;
					dr["ChangeDate"] = DateTime.Now.ToString("yyyy-MM-dd");
				}
                

				DAL.EntityDAO.ContractDAO.SubmitAllStandard_Contract(entity);

                //����ص��ݸ�Ϊ������״̬

                DataTable ud_dtNexusCodeType = GetNexusCodeTypeTable();

                SqlConnection ud_SqlConn = new SqlConnection(this.up_sConnectionString);
                ud_SqlConn.Open();
                SqlTransaction ud_SqlTran = ud_SqlConn.BeginTransaction();

                BLL.ContractRule.BatchWaitDealwith(ud_dtNexusCodeType, ud_SqlTran);

                ud_SqlTran.Commit();
                ud_SqlTran.Dispose();

				entity.Dispose();


				// ���渽��
				this.myAttachMentAdd.SaveAttachMent(contractChangeCode);

				ClearSession();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "�������" + ex.Message));
				return;
			}

			GoBack( );
		}

		/// <summary>
		/// ����
		/// </summary>
		private void GoBack() 
		{
			string contractCode = txtContractCode.Value;
			string projectCode = Request["ProjectCode"]+"";
			string contractChangeCode = txtContractChangeCode.Value;

			Response.Write(Rms.Web.JavaScript.ScriptStart);

//			Response.Write("window.opener.location = window.opener.location;");
			Response.Write("window.location.href='../Contract/ContractChangeInfo.aspx?ProjectCode=" + projectCode + "&ContractCode=" + contractCode + "&ContractChangeCode=" + contractChangeCode + "';");
//			Response.Write(Rms.Web.JavaScript.WinClose(false));
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
		}

        private string GetNexusCodes(DataTable pm_dtNexusList)
        { 
            string ud_sFilter = string.Format("ContractChangeCode='{0}'", txtContractChangeCode.Value);

            string ud_sNexusCodes = "" ;

            foreach (DataRow dr in pm_dtNexusList.Select(ud_sFilter, "", DataViewRowState.CurrentRows))
            {
                ud_sNexusCodes += dr["Code"].ToString() + "," + dr["Type"].ToString() + ";";
            }

            return ud_sNexusCodes;
        }

        /// <summary>
        /// ��ʾ��ص���
        /// </summary>
        private void gvNexusBind( DataTable pm_dtNexusList )
        {

            string ud_sFilter = string.Format("ContractChangeCode='{0}'", txtContractChangeCode.Value);

            decimal ud_deSumMoney = BLL.MathRule.SumColumn(pm_dtNexusList, "Money",ud_sFilter);

            ViewState["_SumMoney"] = ud_deSumMoney;

            DataView ud_dvNexusList = new DataView(pm_dtNexusList, ud_sFilter, "", DataViewRowState.CurrentRows);

            gvNexusList.DataSource = ud_dvNexusList;
            gvNexusList.DataBind();

        }

        private void ShowNexusListByViewState()
        {
            SqlConnection ud_SqlConnection = new SqlConnection(this.up_sConnectionString);
            ud_SqlConnection.Open();
            DataTable ud_dtCodeType = GetNexusCodeTypeTable();

            DataTable ud_dtNexusList = BLL.ContractRule.GetNexusList(ud_dtCodeType, ud_SqlConnection);

            ud_dtNexusList.Columns.Add("ContractCode");
            ud_dtNexusList.Columns.Add("ContractChangeCode");

            string ud_sContractCode = txtContractCode.Value;
            string ud_sContractChangeCode = txtContractChangeCode.Value;

            foreach (DataRow dr in ud_dtNexusList.Rows)
            {
                dr["ContractCode"] = ud_sContractCode;
                dr["ContractChangeCode"] = ud_sContractChangeCode;
            }

            gvNexusBind(ud_dtNexusList);

        }

        private void gvNexusList_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            switch (e.Row.RowType)
            { 
                case DataControlRowType.DataRow:

                    Label ud_lblNexusType = (Label)e.Row.FindControl("lblNexusType");
                    Label ud_lblPersonName = (Label)e.Row.FindControl("lblPersonName");

                    DataRowView ud_drvItem = (DataRowView)e.Row.DataItem;

                    switch (ud_drvItem["Type"].ToString())
                    { 
                        case "Vise":
                            ud_lblNexusType.Text = "�ֳ�ǩ֤";
                            break;
                    }

                    ud_lblPersonName.Text = BLL.SystemRule.GetUserName(ud_drvItem["Person"].ToString());
                    break;

                case DataControlRowType.Footer:
                    Label ud_lblSumMoney = (Label)e.Row.FindControl("lblSumMoney");

                    ud_lblSumMoney.Text = decimal.Parse(ViewState["_SumMoney"].ToString()).ToString("N");
                    
                    break;
            }
        }

        /// <summary>
        /// �����ص����б�
        /// </summary>
        private DataTable GetNexusCodeTypeTable()
        {
            DataTable ud_dtNexusCode = new DataTable();

            ud_dtNexusCode.Columns.Add("Code");
            ud_dtNexusCode.Columns.Add("Type");

            string ud_sNexusCodes = hidNexusCodes.Value;

            foreach (string ud_sTmp in ud_sNexusCodes.Split(';'))
            { 
                
                string[] ud_saCodeType = ud_sTmp.Split(',');

                if (ud_saCodeType.Length == 2)
                {
                    DataRow ud_drNew = ud_dtNexusCode.NewRow();

                    ud_drNew["Code"] = ud_saCodeType[0];
                    ud_drNew["Type"] = ud_saCodeType[1];

                    ud_dtNexusCode.Rows.Add(ud_drNew);
                }
            }

            return ud_dtNexusCode;

        }

        protected void btnLoadNexus_ServerClick(object sender, EventArgs e)
        {
            this.ShowNexusListByViewState();

            string ud_sContractCode = txtContractCode.Value;
            string ud_sContractChangeCode = txtContractChangeCode.Value;

            DataTable ud_dtNexusCodeType = GetNexusCodeTypeTable();


            SqlConnection ud_SqlConn = new SqlConnection(this.up_sConnectionString);
            ud_SqlConn.Open();
            SqlTransaction ud_SqlTran = ud_SqlConn.BeginTransaction();

            EntityData entity = ReadEntitySession();

            BLL.ContractRule.BuildContractChangeByNexusCodeType(ud_sContractCode, ud_sContractChangeCode, entity, ud_dtNexusCodeType, ud_SqlTran,user.UserCode);

            this.WriteEntitySession(entity);
            this.dgCostListBind(entity.Tables["ContractCostChange"], ud_sContractChangeCode);

            ud_SqlTran.Dispose();
            

        }
}
}
