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
using RmsPM.DAL.EntityDAO;
using RmsPM.DAL.QueryStrategy;
using Rms.Web;
using Infragistics.WebUI.WebDataInput;
using RmsPM.Web;

namespace RmsPM.Web.Finance
{
	/// <summary>
	/// PaymentDetailModify ��ժҪ˵����
	/// </summary>
	public partial class PaymentDetailModify :  PageBase
	{
        protected int up_iItemCashCount = 6;
        protected bool up_bItemCashOther = true;

		protected bool up_bEditExchangeRate = false;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.spanSupplyName.InnerText = this.txtSupplyName.Value;

			if ( !IsPostBack)
			{
				IniPage();
				LoadData();
			}

            this.myAttachMentAdd.AttachMentType = "PaymentAttachMent";
            this.myAttachMentAdd.MasterCode = this.txtPaymentCode.Value;  
		}

        public string Type
        {
            get
            {
                return Request["Type"] + "";
            }
        }
        public string PaymentCode
        {
            get
            {
                return this.txtPaymentCode.Value;
            }
            set
            {
                this.txtPaymentCode.Value = value;
            }

        }
		private void IniPage()
		{
			try
			{
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];
				this.txtPaymentCode.Value = Request.QueryString["PaymentCode"];
				this.txtContractCode.Value = Request.QueryString["ContractCode"];
				//				this.txtIsContract.Value = Request.QueryString["IsContract"];

                switch (this.up_sPMNameLower)
                { 
                    case "shimaopm":
                        this.dgList.Columns[5].Visible = false;
                        this.dgList.Columns[6].Visible = false;
                        this.dgList.Columns[7].Visible = false;
                        this.dgList.Columns[8].Visible = false;
                        this.dgList.Columns[9].Visible = false;
                        this.dgList.Columns[10].Visible = false;
                        break;

                }

			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}

		private void LoadData()
		{

			try
			{
                EntityData entity;
                DataTable tbDtl = new DataTable();

				//				string WBSCode = "";

				//����ʱ���봫����Ŀ����
                if ((PaymentCode == "") && (this.txtProjectCode.Value == ""))
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "����Ŀ���룬��������"));
					Response.Write(Rms.Web.JavaScript.WinClose(true));
					return;
				}				
                if ( PaymentCode != "")
				{
					
                    //��Զ �޸�(060228)
                    //***************************************************************************************
                    if (PaymentCode != "")
                    {
                        this.txtIsNew.Value = "0";

                        entity = DAL.EntityDAO.PaymentDAO.GetStandard_PaymentByCode(PaymentCode);
                        SetControlMessage(entity);
                        tbDtl = BLL.PaymentRule.GeneratePaymentItemCashTable(entity.Tables["PaymentItem"]);
                    }                   
				}
                else if (this.Type != "")
                {
                    //new EntityData("Standard_Payment");
                    entity = SetDefaultValue();
                    this.dtPayDate.Value = DateTime.Today.ToString("yyyy-MM-dd");
                    this.txtIsNew.Value = "1";
                  //  this.txtPaymentID.Value = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("PaymentCode");
                    SetControlMessage(entity);
                    tbDtl = BLL.PaymentRule.GeneratePaymentItemCashTable(entity.Tables["PaymentItem"]);
                }
                else
                {
                    //����
                    this.txtIsNew.Value = "1";

                    //ȱʡֵ
                    this.dtPayDate.Value = DateTime.Today.ToString("yyyy-MM-dd");

                    this.txtApplyPerson.Value = user.UserCode;
                    this.txtApplyPersonName.Value = BLL.SystemRule.GetUserName(user.UserCode);

                    //����ʱ���ö�Ӧ���� Lys 070216
                    SetNewAddedUnit();

                    tbDtl = BLL.PaymentRule.GeneratePaymentItemCashTable();
                   
                    //����ʱ��ȱʡ��ʾ5����ϸ
                    for (int i = 0; i < 5; i++)
                    {
                        AddDtl(tbDtl);
                    }

                    //�Ƿ��ͬ���
                    if (this.txtContractCode.Value == "")
                    {
                        this.txtIsContract.Value = "0";

                    }
                    else
                    {
                        this.txtIsContract.Value = "1";

                        int Issue = BLL.PaymentRule.GetIssueByContractCode(this.txtContractCode.Value);
                        this.txtIssue.Value = Issue.ToString();

                        this.hidAHMoney.Value = BLL.ContractRule.GetAHMoney(this.txtContractCode.Value).ToString();

                        //ȱʡ�����ͬ��Ϣ
                        EntityData entityC = DAL.EntityDAO.ContractDAO.GetContractByCode(this.txtContractCode.Value);
                        //						EntityData entityC = DAL.EntityDAO.ContractDAO.GetStandard_ContractByCode(this.txtContractCode.Value);
                        if (!entityC.HasRecord())
                        {
                            Response.Write(Rms.Web.JavaScript.Alert(true, "��ͬ������"));
                            Response.Write(Rms.Web.JavaScript.WinClose(true));
                            return;
                        }

                        this.txtSupplyCode.Value = entityC.GetString("SupplierCode");
                        this.txtSupplyName.Value = BLL.ProjectRule.GetSupplierName(entityC.GetString("SupplierCode"));
                        this.spanSupplyName.InnerText = this.txtSupplyName.Value;
                      
                       
                        if(this.up_sPMNameLower!="shidaipm")//ʱ��Ҫ���ú�ͬ�Ĳ��Ŷ����÷����˲���
                        this.ucUnit.Value = entityC.GetString("UnitCode");

                        decimal ud_dePaymentPercent =  1m;

                        if (System.Configuration.ConfigurationManager.AppSettings["PaymentPercent"] != null
                            && decimal.Parse(System.Configuration.ConfigurationManager.AppSettings["PaymentPercent"].ToString()) <= 1m)
                        {
                            ud_dePaymentPercent = decimal.Parse(System.Configuration.ConfigurationManager.AppSettings["PaymentPercent"].ToString());
                        }


                        this.hidUpperMoney.Value = (entityC.GetDecimal("TotalMoney") * ud_dePaymentPercent).ToString();

                        string ud_sClassCode = "0601";
                        string ud_sSortID = RmsPM.BLL.SystemGroupRule.GetSystemGroupSortIDByGroupCode(entityC.GetString("Type"));

                        switch (up_sPMName)
                        {
                            case "ShiMaoPM":
                                ud_sSortID += "01";
                                this.ucGroup.Value = RmsPM.BLL.SystemGroupRule.GetSystemGroupCodeBySortID(ud_sSortID, ud_sClassCode);
                                this.ucGroup.SelectAllLeaf = false;
                                break;
                            default:
                                break;
                        }
                    }

                    //����ʱ������������ţ�PaymentID = PaymentCode
                    PaymentCode = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("PaymentCode");
                    this.txtPaymentID.Value = PaymentCode;
                    this.txtPaymentCode.Value = PaymentCode;
                }

				if (this.txtIsContract.Value == "0")
				{
					this.trContract.Visible = false;
					this.trContract2.Visible = false;
					this.trContract3.Visible = false;
					this.trContract4.Visible = false;
					this.trContract5.Visible = false;
					this.trContract6.Visible = false;
				}
				else 
				{
                    this.TrPayment.Visible = false;

					EntityData entityCon = DAL.EntityDAO.ContractDAO.GetStandard_ContractByCode(this.txtContractCode.Value);
					if (entityCon.HasRecord()) 
					{
						this.lblContractID.Text = entityCon.GetString("ContractID");
						this.lblContractName.Text = entityCon.GetString("ContractName");
						this.txtContractType.Value = entityCon.GetString("Type");
						this.lblContractTypeName.Text = BLL.SystemGroupRule.GetSystemGroupFullName(this.txtContractType.Value);
					}

					//��ͬ���ܿλ���ɸ�
					this.hrefSelectSupply.Style["display"] = "none";

                    //����Ҫ���ܿλ�ɸ�
                    if (this.up_sPMNameLower == "tangchenpm" && this.txtIsNew.Value != "1")
                        this.hrefSelectSupply.Style["display"] = "";
					//					BLL.PageFacade.LoadPaymentItemSelectContractAllocation(this.sltSummaryEg, "", this.txtContractCode.Value);

					//��ͬ������ϸ
					DataTable tbAllo;
					if (base.IsContractNew) //�º�ͬ�ṹ
					{
						tbAllo = entityCon.Tables["ContractCost"];

						//����ͬ��ϸ���
						tbAllo.Columns.Add("AllocateCode");
						foreach(DataRow dr in tbAllo.Rows) 
						{
							dr["AllocateCode"] = dr["ContractCostCode"];
						}

						//����ͬ��ϸ������Ϊ����������
						if (!tbAllo.Columns.Contains("ItemName")) 
						{
							tbAllo.Columns.Add("ItemName");

							foreach(DataRow dr in tbAllo.Rows)
							{
                                //��ʾԤ���������
								dr["ItemName"] = BLL.CostBudgetRule.GetCostBudgetSetName(BLL.ConvertRule.ToString(dr["CostBudgetSetCode"])) + " -> " + BLL.CBSRule.GetCostName(BLL.ConvertRule.ToString(dr["CostCode"])); 
							}
						}


						//					tbAllo.Columns.Add("TotalPaymentMoney", typeof(System.Decimal));
						//					foreach(DataRow drAllo in tbAllo.Rows) 
						//					{
						//						drAllo["TotalPaymentMoney"] = BLL.PaymentRule.GetPaymentMoneyByContractAllocate(BLL.ConvertRule.ToString(drAllo["AllocateCode"]), 0);
						//					}
						//
						//					BindContractAllocationDataGrid(tbAllo);
						//					BindContractPlanDataGrid(entityCon.Tables["ContractPaymentPlan"]);

						//��ͬ���������ӡ���ѡ��
						DataRow drAllo = tbAllo.NewRow();
						drAllo["ContractCostCode"] = "-1";
						drAllo["AllocateCode"] = drAllo["ContractCostCode"];
						drAllo["ItemName"] = "--��ѡ��--";
						tbAllo.Rows.InsertAt(drAllo, 0);
					}
					else  //�Ϻ�ͬ�ṹ
					{
						tbAllo = entityCon.Tables["ContractAllocation"];
						//					tbAllo.Columns.Add("TotalPaymentMoney", typeof(System.Decimal));
						//					foreach(DataRow drAllo in tbAllo.Rows) 
						//					{
						//						drAllo["TotalPaymentMoney"] = BLL.PaymentRule.GetPaymentMoneyByContractAllocate(BLL.ConvertRule.ToString(drAllo["AllocateCode"]), 0);
						//					}
						//
						//					BindContractAllocationDataGrid(tbAllo);
						//					BindContractPlanDataGrid(entityCon.Tables["ContractPaymentPlan"]);

						//��ͬ���������ӡ���ѡ��
						DataRow drAllo = tbAllo.NewRow();
						drAllo["AllocateCode"] = "-1";
						drAllo["ItemName"] = "--��ѡ��--";
						tbAllo.Rows.InsertAt(drAllo, 0);
					}

					ViewState["tbContractAllocate"] = tbAllo;
					entityCon.Dispose();
				}

				BindDataGrid(tbDtl);
			}
			catch (  Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ���ݳ���" + ex.Message));
			}
		
		}

		public DataTable GetSelectSummaryDataSource() 
		{
			return (DataTable)ViewState["tbContractAllocate"];
		}

		/*
		/// <summary>
		/// ��ʾ��ͬ������ϸ
		/// </summary>
		private void BindContractAllocationDataGrid(DataTable tbAllocation) 
		{
			try 
			{
				this.dgContractAllocation.Columns[3].FooterText = BLL.MathRule.SumColumn(tbAllocation, "Money").ToString("N");
				this.dgContractAllocation.Columns[4].FooterText = BLL.MathRule.SumColumn(tbAllocation, "TotalPaymentMoney").ToString("N");
				this.dgContractAllocation.DataSource = tbAllocation;
				this.dgContractAllocation.DataBind();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ�б����" + ex.Message));
			}
		}

		/// <summary>
		/// ��ʾ��ͬ����ƻ���ϸ
		/// </summary>
		private void BindContractPlanDataGrid(DataTable tbPlan) 
		{
			try 
			{
				this.dgContractPaymentPlan.Columns[4].FooterText = BLL.MathRule.SumColumn(tbPlan, "Money").ToString("N");
				this.dgContractPaymentPlan.DataSource = tbPlan;
				this.dgContractPaymentPlan.DataBind();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ�б����" + ex.Message));
			}
		}
		*/

		/// <summary>
		/// ��ʾ����ϸ
		/// </summary>
		private void BindDataGrid(DataTable tb) 
		{
			try 
			{
				//this.txtMoney.Value = BLL.MathRule.SumColumn(tb, "ItemMoney").ToString("N");

				this.txtMoney.Value = Decimal.Zero.ToString("N");

                ViewState["_SumItemMoney0"] = Decimal.Zero.ToString("N");
                ViewState["_SumItemMoney1"] = Decimal.Zero.ToString("N");
                ViewState["_SumItemMoney2"] = Decimal.Zero.ToString("N");
                ViewState["_SumItemMoney3"] = Decimal.Zero.ToString("N");
                ViewState["_SumItemMoney4"] = Decimal.Zero.ToString("N");
                ViewState["_SumItemMoney5"] = Decimal.Zero.ToString("N");
                ViewState["_SumItemMoney6"] = Decimal.Zero.ToString("N");
                ViewState["_SumItemMoney7"] = Decimal.Zero.ToString("N");
                ViewState["_SumItemMoney8"] = Decimal.Zero.ToString("N");
                ViewState["_SumItemMoney9"] = Decimal.Zero.ToString("N");

				this.dgList.DataSource = tb;
				this.dgList.DataBind();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ�б����" + ex.Message));
			}
		}

		override protected void InitEventHandler()
		{
			base.InitEventHandler();
			this.dgList.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgList_DeleteCommand);
			this.dgList.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgList_ItemDataBound);
		}

		/// <summary>
		/// ��Ч�Լ��
		/// </summary>
		/// <param name="Hint"></param>
		/// <returns></returns>
		private bool CheckValid(ref string Hint, DataTable tbDtl) 
		{
			Hint = "";

			if ((this.txtSupplyCode.Value == "") && (this.txtPayer.Value.Trim() == "") )
			{
				Hint = "�������ܿλ���ܿ���";
				return false;
			}

            //payer=���ɱ���������ϵͳ���� 
            if (this.txtPayer.Value == "�ɱ��������")
            {
                Hint = "��ͨ�����տ��˲����ǡ��ɱ�������";
                return false;
            }
            if ("shidaipm" != this.up_sPMNameLower)
            {
                if (this.dtPayDate.Value.Trim() == "")
                {
                    Hint = "��������󸶿���";
                    return false;
                }
            }

			//			if (this.txtContractCode.Value == "") 
			//			{
			if (this.ucGroup.Value == "") 
			{
				Hint = "�������������";
				return false;
			}
			//			}

           
			/*
						if (this.txtPBSTypeName.Value.Trim() == "") 
						{
							Hint = "����������";
							return false;
						}

						if ( dtPay != "" )
						{
							if ( !Rms.Check.StringCheck.IsDateTime(dtPay))
							{
								Response.Write(Rms.Web.JavaScript.Alert(true,"����ʱ����д���� ��"));
								return;
							}
						}

						string rc = this.txtRecieptCount.Text.Trim();
						if ( rc != "" )
						{
							if ( !Rms.Check.StringCheck.IsInt(rc))
							{
								Response.Write(Rms.Web.JavaScript.Alert(true,"����������д���� ��"));
								return;
							}
						}

						string money = this.txtMoney.Text.Trim();
						if ( money != "" )
						{
							if ( !Rms.Check.StringCheck.IsNumber(money) )
							{
								Response.Write(Rms.Web.JavaScript.Alert(true,"�ܽ����д���� ��"));
								return;
							}
						}
			*/
            if (tbDtl.Rows.Count <= 0)
            {
                Hint = "�����������ϸ";
                return false;
            }
			foreach(DataRow dr in tbDtl.Rows) 
			{
				if (this.txtContractCode.Value == "") 
				{
					//�Ǻ�ͬ���
					if (BLL.ConvertRule.ToString(dr["Summary"]) == "") 
					{
						Hint = "���������";
						return false;
					}
                    if (this.TxtPaymentName.Value.Trim() == "")
                    {
                        Hint = "�������������";
                        return false;
                    }

				}
				else 
				{
					//��ͬ���
					if (BLL.ConvertRule.ToInt(dr["AllocateCode"]) < 0) 
					{
						Hint = "��ѡ�����";
						return false;
					}
				}

				if (BLL.ConvertRule.ToDecimal(dr["ItemCash"]) == 0) 
				{
					Hint = "�����������";
					return false;
				}

				if (BLL.ConvertRule.ToString(dr["CostCode"]) == "") 
				{
					Hint = "�����������";
					return false;
				}

                /*
				if (!BLL.CBSRule.CheckCBSLeafNode(BLL.ConvertRule.ToString(dr["CostCode"])))
				{
					Hint = string.Format("��{0}������ĩ�������� ��", BLL.ConvertRule.ToString(dr["CostName"]));
					return false;
				}
                */
			}

			if (this.txtContractCode.Value != "") 
			{
                if (this.txtIssue.Value == "")
                {
                    Hint = "�����븶������";
                    return false;
                }

                if (this.txtIssue.Value != "")
                {
                    if (!Rms.Check.StringCheck.IsInt(this.txtIssue.Value))
                    {
                        Hint = "������������������";
                        return false;
                    }

                    Hint = BLL.PaymentRule.CheckIssueBeforeSave(this.txtIssue.Value, this.txtContractCode.Value, this.txtPaymentCode.Value);
                    if (Hint != "")
                        return false;
                }


                //�����ϸ���ܳ�����ͬ����������
                DataTable tbResult = BLL.PaymentRule.CreatePaymentCheckResultTable();
                Hint = BLL.PaymentRule.CheckPaymentCostLimit(this.txtPaymentCode.Value, this.txtContractCode.Value, tbDtl, ref tbResult);
                if (Hint.Length > 0) return false;        
				
			}
           
            //�������ʱ����Ƿ��ܲ���������

            if (this.txtIsNew.Value == "1")
            {
                if (!user.HasTypeOperationRight("060102", this.ucGroup.Value))
                {
                    Hint = "�����ܲ����������";
                    return false;
                }
            }
            //ʱ��Ҫ����Ϊ��ѡ��
            if (this.up_sPMNameLower == "shidaipm" && this.ucUnit.Value == string.Empty)
            {
                Hint = "��ѡ������";
                return false;
            }

			return true;
		}

		/// <summary>
		/// ����
		/// </summary>
		private void Save(DataTable tbDtl)
		{
			string isContract = this.txtIsContract.Value;
			string paymentCode = this.txtPaymentCode.Value;
			string contractCode = this.txtContractCode.Value;
			string projectCode = this.txtProjectCode.Value;

			bool isNew = (this.txtIsNew.Value.Trim() == "1" );
			
			try
			{
				EntityData entity = null;
				DataRow dr = null;

				if ( isNew )
				{
					entity = new EntityData("Standard_Payment");
					dr = entity.GetNewRecord();

					//					paymentCode = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("PaymentCode");
					//					this.txtPaymentCode.Value = paymentCode;

					dr["PaymentCode"] = paymentCode;
					dr["PaymentID"] = paymentCode;
					dr["ProjectCode"] = projectCode;
					dr["ApplyPerson"] = this.txtApplyPerson.Value;
					dr["ApplyDate"] = DateTime.Now.ToString("yyyy-MM-dd");
					dr["Status"] = 0;
					entity.AddNewRecord(dr);
				}
				else
				{
					entity = DAL.EntityDAO.PaymentDAO.GetStandard_PaymentByCode(paymentCode);
					dr = entity.CurrentRow;
				}

				if ( contractCode == "" )
				{
					//�Ǻ�ͬ���
					dr["IsContract"] = 0;
					dr["ContractCode"] = "";
				}
				else
				{
					//��ͬ���
					dr["IsContract"] = 1;
					dr["ContractCode"] = contractCode;
				}

				dr["GroupCode"] = this.ucGroup.Value;

				//				dr["PaymentID"] = this.txtPaymentID.Value;
				dr["RecieptCount"] = BLL.ConvertRule.ToInt(this.txtRecieptCount.Value);

				dr["Payer"] = this.txtPayer.Value;
                dr["PaymentName"] = this.TxtPaymentName.Value;
				string SupplyCode = this.txtSupplyCode.Value;
				dr["SupplyName"] = this.txtSupplyName.Value;
				dr["SupplyCode"] = SupplyCode;

				//				dr["WBSCode"] = this.sltTask.Value;
				dr["UnitCode"] = this.ucUnit.Value;
				dr["PayDate"] = BLL.ConvertRule.ToDate(this.dtPayDate.Value);
				dr["Purpose"] = this.txtPurpose.Value;
				dr["IsApportion"] = 0;
				dr["Issue"] = BLL.ConvertRule.ToIntObj(txtIssue.Value.Trim());

                dr["PaymentTitle"] = this.txtPaymentTitle.Value;

                dr["BankName"] = this.txtBankName.Value;
                dr["BankAccount"] = this.txtBankAccount.Value;

				dr["Remark"] = this.txtRemark.Value;

				//��ϸ�ܽ��
                decimal ud_deMoney = BLL.MathRule.SumColumn(tbDtl, "ItemMoney");
				dr["Money"] = ud_deMoney;

                string ud_sAction = Request["Act"] + "";

                switch (ud_sAction)
                {
                    case "AuditModify":
                        break;
                    default:
                        //�ۼ�����
                        dr["AHMoney"] = BLL.ContractRule.GetAHMoney(contractCode);
                        dr["AHCash"] = BLL.ContractRule.GetAHCash(contractCode);

                        for (int i = 0; i <= 9; i++)
                        {
                            dr["AHCash"+i.ToString()] = BLL.ContractRule.GetAHCash(contractCode, i);
                        }

                        //�ۼ��Ѹ�
                        dr["APMoney"] = BLL.ContractRule.GetAPMoney(contractCode);
                        dr["APCash"] = BLL.ContractRule.GetAPCash(contractCode);

                        //�ۼ�Ӧ����
                        dr["TotalPayMoney"] = (decimal)dr["AHMoney"] + (decimal)dr["Money"];
                        break;
                }

				SaveDetail(entity, tbDtl);

				DAL.EntityDAO.PaymentDAO.SubmitAllStandard_Payment(entity);
				entity.Dispose();
			}
			catch ( Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// ��������ϸ
		/// </summary>
		private void SaveDetail(EntityData entity, DataTable tb) 
		{
			try 
			{
				entity.SetCurrentTable("Payment");
				string PaymentCode = entity.GetString("PaymentCode");
				string ProjectCode = entity.GetString("ProjectCode");

				//�ɵ���ϸ
				entity.SetCurrentTable("PaymentItem");

				//ɾ��ԭ��������û�е�
				foreach(DataRow dr in entity.CurrentTable.Rows) 
				{
					string PaymentItemCode = dr["PaymentItemCode"].ToString();
					if (tb.Select("PaymentItemCode='" + PaymentItemCode + "'").Length == 0) 
					{
						dr.Delete();

						//ɾ�������ϸ��̯��¥��
						DataRow[] drs = entity.Tables["PaymentItemBuilding"].Select("PaymentItemCode='" + PaymentItemCode + "'");
						foreach(DataRow drI in drs) 
						{
							drI.Delete();
						}
					}
				}

				//�������޸�
				foreach(DataRow dr in tb.Rows) 
				{
					string PaymentItemCode = dr["PaymentItemCode"].ToString();
					string BuildingCodeAll = BLL.ConvertRule.ToString(dr["BuildingCodeAll"]);
					DataRow drNew;
					DataRow[] drs;

					//�����ϸ
					entity.SetCurrentTable("PaymentItem");
					drs = entity.CurrentTable.Select("PaymentItemCode='" + PaymentItemCode + "'");

					if (drs.Length == 0) 
					{
						drNew = entity.CurrentTable.NewRow();

						PaymentItemCode = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("PaymentItemCode");
						drNew["PaymentItemCode"] = PaymentItemCode;
						drNew["PaymentCode"] = PaymentCode;

						entity.CurrentTable.Rows.Add(drNew);
					}
					else 
					{
						drNew = drs[0];
					}

					drNew["Summary"] = BLL.ConvertRule.ToString(dr["Summary"]);
					drNew["CostCode"] = BLL.ConvertRule.ToString(dr["CostCode"]);
					drNew["AllocateCode"] = BLL.ConvertRule.ToString(dr["AllocateCode"]);

					drNew["CostBudgetSetCode"] = dr["CostBudgetSetCode"];
					drNew["PBSType"] = dr["PBSType"];
					drNew["PBSCode"] = dr["PBSCode"];

					drNew["ContractCostCashCode"] = dr["ContractCostCashCode"];
                    drNew["ItemCash0"] = BLL.ConvertRule.ToDecimalObj(dr["ItemCash0"]);
                    drNew["ItemCash1"] = BLL.ConvertRule.ToDecimalObj(dr["ItemCash1"]);
                    drNew["ItemCash2"] = BLL.ConvertRule.ToDecimalObj(dr["ItemCash2"]);
                    drNew["ItemCash3"] = BLL.ConvertRule.ToDecimalObj(dr["ItemCash3"]);
                    drNew["ItemCash4"] = BLL.ConvertRule.ToDecimalObj(dr["ItemCash4"]);
                    drNew["ItemCash5"] = BLL.ConvertRule.ToDecimalObj(dr["ItemCash5"]);
                    drNew["ItemCash6"] = BLL.ConvertRule.ToDecimalObj(dr["ItemCash6"]);
                    drNew["ItemCash7"] = BLL.ConvertRule.ToDecimalObj(dr["ItemCash7"]);
                    drNew["ItemCash8"] = BLL.ConvertRule.ToDecimalObj(dr["ItemCash8"]);
                    drNew["ItemCash9"] = BLL.ConvertRule.ToDecimalObj(dr["ItemCash9"]);

                    drNew["ItemCash"] = BLL.ConvertRule.ToDecimalObj(dr["ItemCash"]);

					drNew["MoneyType"] = dr["MoneyType"];
					drNew["ExchangeRate"] = BLL.ConvertRule.ToDecimalObj(dr["ExchangeRate"]);
					drNew["ItemMoney"] = BLL.ConvertRule.ToDecimal(dr["ItemCash"]) * BLL.ConvertRule.ToDecimal(dr["ExchangeRate"]);

					string AlloType = BLL.ConvertRule.ToString(dr["AlloType"]);
					drNew["AlloType"] = AlloType;

					//�����ϸ��̯��¥��  begin-----------------------------------
					entity.SetCurrentTable("PaymentItemBuilding");

					//ɾ��ԭ��������û�е�
					string[] arrBuildingCode = BuildingCodeAll.Split(",".ToCharArray());
					drs = entity.CurrentTable.Select("PaymentItemCode='" + PaymentItemCode + "'");
					foreach(DataRow drI in drs) 
					{
						switch (AlloType.ToUpper()) 
						{
							case "B":
								string BuildingCode = BLL.ConvertRule.ToString(drI["BuildingCode"]);

								if (!IsArrayInclude(arrBuildingCode, BuildingCode))
								{
									drI.Delete();
								}

								break;

							case "U":
								string PBSUnitCode = BLL.ConvertRule.ToString(drI["PBSUnitCode"]);

								if (!IsArrayInclude(arrBuildingCode, PBSUnitCode))
								{
									drI.Delete();
								}

								break;

							default:
								drI.Delete();
								break;
						}

					}

					foreach(string BuildingCode in arrBuildingCode) 
					{
						if (BuildingCode != "") 
						{
							switch (AlloType.ToUpper()) 
							{
								case "B":
									drs = entity.CurrentTable.Select("PaymentItemCode='" + PaymentItemCode + "' and BuildingCode='" + BuildingCode + "'");

									if (drs.Length == 0) 
									{
										drNew = entity.CurrentTable.NewRow();

										drNew["SystemID"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("PaymentItemBuildingSystemID");
										drNew["PaymentItemCode"] = PaymentItemCode;
										drNew["PaymentCode"] = PaymentCode;
										drNew["BuildingCode"] = BuildingCode;

										entity.CurrentTable.Rows.Add(drNew);
									}

									break;

								case "U":
									drs = entity.CurrentTable.Select("PaymentItemCode='" + PaymentItemCode + "' and PBSUnitCode='" + BuildingCode + "'");

									if (drs.Length == 0) 
									{
										drNew = entity.CurrentTable.NewRow();

										drNew["SystemID"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("PaymentItemBuildingSystemID");
										drNew["PaymentItemCode"] = PaymentItemCode;
										drNew["PaymentCode"] = PaymentCode;
										drNew["PBSUnitCode"] = BuildingCode;

										entity.CurrentTable.Rows.Add(drNew);
									}

									break;

								default:
									break;
							}

						}
					}

					//�����ϸ��̯��¥��  end-----------------------------------
				}
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				throw ex;
			}
		}

		private bool IsArrayInclude(object[] arr, object val) 
		{
			try 
			{
				bool ret = false;

				foreach(object item in arr) 
				{
					if (BLL.ConvertRule.ToString(item) == BLL.ConvertRule.ToString(val)) 
					{
						ret = true;
						break;
					}
				}

				return ret;
			}
			catch (Exception ex) 
			{
				throw ex;
			}
		}

		/// <summary>
		/// ����
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnSave_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				//�µ���ϸ��
				DataTable tbDtl = ScreenToTable(true);
				if (tbDtl == null) return;

				DeleteNullDtl(tbDtl);

				string Hint = "";
				if (!CheckValid(ref Hint, tbDtl)) 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, Hint));
					return;
				}

				Save(tbDtl);
			}
			catch(Exception ex) 
			{
				Response.Write(JavaScript.Alert(true, "����ʧ�ܣ�" + ex.Message));
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				return;
			}

			GoBack();
		}

		/// <summary>
		/// ����
		/// </summary>
		private void GoBack() 
		{
			string paymentCode = this.txtPaymentCode.Value;
			string projectCode = Request["ProjectCode"]+"";

			Response.Write(Rms.Web.JavaScript.ScriptStart);

			Response.Write("window.opener.location = window.opener.location;");
			Response.Write("window.location.href='..//Finance/PaymentInfo.aspx?ProjectCode=" + projectCode + "&PaymentCode=" + paymentCode + "';");
			//			Response.Write(Rms.Web.JavaScript.WinClose(false));
			Response.Write(Rms.Web.JavaScript.ScriptEnd);

		}

		/// <summary>
		/// ɾ���յ������ϸ
		/// </summary>
		/// <param name="tb"></param>
		private void DeleteNullDtl(DataTable tb) 
		{
			try 
			{
				//ɾ���յ���ϸ
				int c = tb.Rows.Count;
				for(int i=c-1;i>=0;i--)
				{
					DataRow dr = tb.Rows[i];
					bool isnull = false;

                    if ((BLL.ConvertRule.ToDecimal(dr["ItemCash"]) == 0) && (BLL.ConvertRule.ToDecimal(dr["ItemCash0"]) == 0)  && (BLL.ConvertRule.ToString(dr["CostCode"]) == "") && (BLL.ConvertRule.ToString(dr["BuildingCodeAll"]) == ""))
					{
						if (this.txtContractCode.Value == "") 
						{
							//�Ǻ�ͬ���
							if (BLL.ConvertRule.ToString(dr["Summary"]) == "") 
							{
								isnull = true;
							}
						}
						else 
						{
							//��ͬ���
							if (BLL.ConvertRule.ToInt(dr["AllocateCode"]) < 0) 
							{
								isnull = true;
							}
						}
					}

					if (isnull) 
					{
						tb.Rows.Remove(dr);
					}
				}

			}
			catch (Exception ex) 
			{
				throw ex;
			}
		}

		/// <summary>
		/// ��Ļ���ݱ��浽��ʱ��
		/// </summary>
		/// <returns></returns>
		private DataTable ScreenToTable(bool isBindGrid) 
		{
			DataTable tb = BLL.PaymentRule.GeneratePaymentItemCashTable();

			foreach (DataGridItem item in this.dgList.Items)
			{
				UserControls.ExchangeRateControl ud_ucExchangeRate = (UserControls.ExchangeRateControl)item.FindControl("ucExchangeRate");

				HtmlInputHidden txtPaymentItemCode = (HtmlInputHidden)item.FindControl("txtPaymentItemCode");
				HtmlInputHidden txtPaymentItemCashCode = (HtmlInputHidden)item.FindControl("txtPaymentItemCashCode");
				HtmlInputHidden txtContractCostCashCode = (HtmlInputHidden)item.FindControl("txtContractCostCashCode");

				HtmlInputText txtSummary = (HtmlInputText)item.FindControl("txtSummary");
				HtmlSelect sltSummary = (HtmlSelect)item.FindControl("sltSummary");

				DropDownList ddlMoneyType = (DropDownList)item.FindControl("ddlMoneyType");
				WebNumericEdit txtCash = (WebNumericEdit)item.FindControl("txtCash");

				WebNumericEdit txtItemMoney = (WebNumericEdit)item.FindControl("txtItemMoney");




				//				HtmlInputText txtItemMoney = (HtmlInputText)item.FindControl("txtItemMoney");

				RmsPM.Web.UserControls.InputCostBudgetDtl ucCostBudgetDtl = (RmsPM.Web.UserControls.InputCostBudgetDtl)item.FindControl("ucCostBudgetDtl");

				HtmlInputHidden txtAlloType = (HtmlInputHidden)item.FindControl("txtAlloType");
				HtmlInputHidden txtBuildingCodeAll = (HtmlInputHidden)item.FindControl("txtBuildingCodeAll");
				HtmlInputHidden txtBuildingNameAll = (HtmlInputHidden)item.FindControl("txtBuildingNameAll");

				HtmlInputHidden txtAllocateCode = (HtmlInputHidden)item.FindControl("txtAllocateCode");

				string PaymentItemCode = txtPaymentItemCode.Value;

				/*
				if ( txtItemMoney.Value != "")
				{
					if ( !Rms.Check.StringCheck.IsNumber(txtItemMoney.Value))
					{
						Response.Write(Rms.Web.JavaScript.Alert(true, "������ָ�ʽ����ȷ !"));
						return null;
					}
				}
				*/

				DataRow dr = tb.NewRow();


				dr["PaymentItemCode"] = PaymentItemCode;

				if (this.txtIsContract.Value == "1") 
				{
					dr["Summary"] = sltSummary.Items[sltSummary.SelectedIndex].Text;
					dr["AllocateCode"] = sltSummary.Value;
				}
				else 
				{
					dr["Summary"] = txtSummary.Value;
				}

				//				dr["AllocateCode"] = txtAllocateCode.Value;

				//				dr["ItemMoney"] = txtItemMoney.ValueDecimal;
				//				dr["ItemMoney"] = BLL.ConvertRule.ToDecimalObj(txtItemMoney.Value);

				dr["CostCode"] = ucCostBudgetDtl.CostCode;
				dr["CostName"] = ucCostBudgetDtl.CostName;

				dr["CostBudgetSetCode"] = ucCostBudgetDtl.CostBudgetSetCode;
				dr["PBSType"] = ucCostBudgetDtl.PBSType;
				dr["PBSCode"] = ucCostBudgetDtl.PBSCode;

				dr["AlloType"] = txtAlloType.Value;
				dr["BuildingCodeAll"] = txtBuildingCodeAll.Value;
				dr["BuildingNameAll"] = txtBuildingNameAll.Value;

				//				dr["ItemMoney"] = txtItemMoney.ValueDecimal;

				dr["ContractCostCashCode"] = txtContractCostCashCode.Value;


                //������
                decimal ud_deItemCash,ud_deItemMoney,ud_deExchangeRate;
                decimal[] ud_deaItemCash = new decimal[up_iItemCashCount];
                
                ud_deExchangeRate = ud_ucExchangeRate.ExchangeRate;

                ud_deaItemCash[0] = ud_ucExchangeRate.Cash;
                ud_deItemCash = ud_deaItemCash[0];
                dr["ItemCash0"] = ud_deaItemCash[0];

                for (int i = 1; i < up_iItemCashCount; i++)
                {
                    WebNumericEdit ud_txtItemCash = (WebNumericEdit)item.FindControl("txtItemCash" + i.ToString());

                    ud_deaItemCash[i] = ud_txtItemCash.ValueDecimal;
                    ud_deItemCash += ud_deaItemCash[i];
                    dr["ItemCash" + i.ToString()] = ud_deaItemCash[i];

                }

                if (this.up_bItemCashOther)
                {
                    WebNumericEdit ud_txtItemCash = (WebNumericEdit)item.FindControl("txtItemCash9");
                    ud_deItemCash += ud_txtItemCash.ValueDecimal;
                    dr["ItemCash9"] = ud_txtItemCash.ValueDecimal;
                }


                dr["MoneyType"] = ud_ucExchangeRate.MoneyType;
                dr["ExchangeRate"] = ud_deExchangeRate;
                dr["ItemCash"] = ud_deItemCash;
                dr["ItemMoney"] = ud_deItemCash * ud_deExchangeRate;

				tb.Rows.Add(dr);
			}

			if (isBindGrid) 
			{
                BindDataGrid(tb);

			}

			return tb;
		}

		/// <summary>
		/// ����һ����ϸ
		/// </summary>
		/// <param name="tb"></param>
		private void AddDtl(DataTable tb) 
		{
			try 
			{
				DataRow dr = tb.NewRow();

				int sno = BLL.ConvertRule.ToInt(this.txtDetailSno.Value.Trim()) + 1;
				this.txtDetailSno.Value = sno.ToString();

				dr["PaymentItemCode"] = -sno;

				//ȱʡ��̯����Ŀ
				dr["AlloType"] = "P";
				dr["BuildingNameAll"] = "��Ŀ";
				tb.Rows.Add(dr);

				if (this.txtContractCode.Value != "") 
				{
					DataTable tbAllo = GetSelectSummaryDataSource();
					if ((tbAllo != null) && (tbAllo.Rows.Count > 0) )
					{
						string AllocateCode = BLL.ConvertRule.ToString(tbAllo.Rows[0]["AllocateCode"]);
						dr["AllocateCode"] = AllocateCode;
						BLL.PaymentRule.SetPaymentItemDefaultFromContractAllocate(dr, AllocateCode, this.txtContractCode.Value, base.IsContractNew);
					}
				}
			}
			catch (Exception ex) 
			{
				throw ex;
			}
		}

		/// <summary>
		/// ������ϸ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnAddDtl_ServerClick(object sender, System.EventArgs e)
		{
			try 
			{
				DataTable tb = ScreenToTable(false);
				if (tb == null) return;

				AddDtl(tb);
				BindDataGrid(tb);
			}
			catch(Exception ex) 
			{
				Response.Write(JavaScript.Alert(true, "������ϸʧ�ܣ�" + ex.Message));
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

		/// <summary>
		/// ɾ����ϸ
		/// </summary>
		/// <param name="source"></param>
		/// <param name="e"></param>
		private void dgList_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try 
			{
				string code = this.dgList.DataKeys[e.Item.ItemIndex].ToString();

				DataTable tb = ScreenToTable(false);
				if (tb == null) return;

				DataRow[] drs = tb.Select("PaymentItemCode='" + code + "'");
				if (drs.Length > 0) 
				{
					tb.Rows.Remove(drs[0]);
				}

				BindDataGrid(tb);
			}
			catch(Exception ex) 
			{
				Response.Write(JavaScript.Alert(true, "ɾ����ϸʧ�ܣ�" + ex.Message));
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}
 
		private void dgList_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:

                    RmsPM.Web.UserControls.InputCostBudgetDtl ucCostBudgetDtl = (RmsPM.Web.UserControls.InputCostBudgetDtl)e.Item.FindControl("ucCostBudgetDtl");
                    ucCostBudgetDtl.ProjectCode = this.txtProjectCode.Value;
                    if ("shimaopm" == this.up_sPMNameLower &&  "1" == this.txtIsContract.Value)
                    {
                        ucCostBudgetDtl.Enable = false;
                    }
                    
                    if (this.txtIsContract.Value == "1") 
					{
						HtmlSelect sltSummary = (HtmlSelect)e.Item.FindControl("sltSummary");
                        HtmlInputText txtSummary = (HtmlInputText)e.Item.FindControl("txtSummary");
						HtmlInputHidden txtAllocateCode = (HtmlInputHidden)e.Item.FindControl("txtAllocateCode");

                        sltSummary.Style["display"] = "";
                        txtSummary.Style["display"] = "none";

                        foreach (ListItem item in sltSummary.Items) 
						{
							item.Selected = (item.Value == txtAllocateCode.Value);
						}

						Init_MoneyTypeDropDownList(txtAllocateCode.Value,((DataRowView)e.Item.DataItem).Row["ContractCostCashCode"].ToString(),e);
					}
					else
					{
						Init_MoneyTypeDropDownList(((DataRowView)e.Item.DataItem).Row["MoneyType"].ToString(),e);
					}

					UserControls.ExchangeRateControl ud_ucExchangeRate = (UserControls.ExchangeRateControl)e.Item.FindControl("ucExchangeRate");

                    WebNumericEdit ud_txtItemCash1 = (WebNumericEdit)e.Item.FindControl("txtItemCash1");
                    WebNumericEdit ud_txtItemCash2 = (WebNumericEdit)e.Item.FindControl("txtItemCash2");
                    WebNumericEdit ud_txtItemCash3 = (WebNumericEdit)e.Item.FindControl("txtItemCash3");
                    WebNumericEdit ud_txtItemCash4 = (WebNumericEdit)e.Item.FindControl("txtItemCash4");
                    WebNumericEdit ud_txtItemCash5 = (WebNumericEdit)e.Item.FindControl("txtItemCash5");
                    WebNumericEdit ud_txtItemCash9 = (WebNumericEdit)e.Item.FindControl("txtItemCash9");

                    Label ud_lblItemCash = (Label)e.Item.FindControl("lblItemCash");

                    DataRowView ud_drvItem = (DataRowView)e.Item.DataItem;

                    decimal ud_deItemMoney = BLL.ConvertRule.ToDecimal(txtMoney.Value);
                    decimal ud_deItemCash = BLL.ConvertRule.ToDecimal(ud_drvItem["ItemCash"]);
                    ud_deItemMoney += ud_deItemCash * ud_ucExchangeRate.ExchangeRate;
                    txtMoney.Value = ud_deItemMoney.ToString("N");

                    for (int i = 0; i < up_iItemCashCount; i++)
                    {
                        decimal ud_deSumItemMoney = BLL.ConvertRule.ToDecimal(ViewState["_SumItemMoney" + i.ToString()].ToString());
                        decimal ud_deItemCashI = BLL.ConvertRule.ToDecimal(ud_drvItem["ItemCash" + i.ToString()]);
                        ud_deSumItemMoney += ud_deItemCashI * ud_ucExchangeRate.ExchangeRate;
                        ViewState["_SumItemMoney" + i.ToString()] = ud_deSumItemMoney.ToString("N");
                    }


                    if (this.Type != "")
                    {
                        ud_ucExchangeRate.IsAllowModify = false;
                    }

                    ud_txtItemCash1.ValueDecimal = ud_drvItem["ItemCash1"] == DBNull.Value ? decimal.Zero : decimal.Parse(ud_drvItem["ItemCash1"].ToString());
                    ud_txtItemCash2.ValueDecimal = ud_drvItem["ItemCash2"] == DBNull.Value ? decimal.Zero : decimal.Parse(ud_drvItem["ItemCash2"].ToString());
                    ud_txtItemCash3.ValueDecimal = ud_drvItem["ItemCash3"] == DBNull.Value ? decimal.Zero : decimal.Parse(ud_drvItem["ItemCash3"].ToString());
                    ud_txtItemCash4.ValueDecimal = ud_drvItem["ItemCash4"] == DBNull.Value ? decimal.Zero : decimal.Parse(ud_drvItem["ItemCash4"].ToString());
                    ud_txtItemCash5.ValueDecimal = ud_drvItem["ItemCash5"] == DBNull.Value ? decimal.Zero : decimal.Parse(ud_drvItem["ItemCash5"].ToString());
                    ud_txtItemCash9.ValueDecimal = ud_drvItem["ItemCash9"] == DBNull.Value ? decimal.Zero : decimal.Parse(ud_drvItem["ItemCash9"].ToString());

                    if ( ud_drvItem["ItemCash"] == null )
                    {
                        ud_lblItemCash.Text =  decimal.Zero.ToString("N") ;
                    }
                    else
                    {
                        ud_lblItemCash.Text = BLL.ConvertRule.ToDecimal(ud_drvItem["ItemCash"]).ToString("N");
                    }
                    break;
				case ListItemType.Footer:
					//��ʾ�ϼƽ��
					Label lblSumItemMoney = (Label)e.Item.FindControl("lblSumItemMoney");

					lblSumItemMoney.Text = this.txtMoney.Value;

                    for (int i = 0; i < up_iItemCashCount; i++)
                    {
                        Label ud_lblSumItemMoney = (Label)e.Item.FindControl("lblSumItemMoney" + i.ToString());

                        ud_lblSumItemMoney.Text = ViewState["_SumItemMoney" + i.ToString()].ToString();


                    }

					break;
			}
		}

		private void Init_MoneyTypeDropDownList(string pm_sMoneyType,System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			UserControls.ExchangeRateControl ud_ucExchangeRate = (UserControls.ExchangeRateControl)e.Item.FindControl("ucExchangeRate");

			DataRowView ud_drvItem = (DataRowView)e.Item.DataItem;
			ud_ucExchangeRate.MoneyType = pm_sMoneyType;
			if ( ud_drvItem["ExchangeRate"] != DBNull.Value )
			{
				ud_ucExchangeRate.ExchangeRate = Decimal.Parse(ud_drvItem["ExchangeRate"].ToString());
			}
			ud_ucExchangeRate.Cash = ud_drvItem["ItemCash0"] == DBNull.Value ? Decimal.Zero:(decimal)ud_drvItem["ItemCash0"];
			ud_ucExchangeRate.IsShowTitle = false;
			ud_ucExchangeRate.EditMode = true;
			ud_ucExchangeRate.IsShowExchange = up_bEditExchangeRate;
			ud_ucExchangeRate.BindControl();
		}

		private void Init_MoneyTypeDropDownList(string pm_sContractCostCode,string pm_sMoneyTypeCode,System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			UserControls.ExchangeRateControl ud_ucExchangeRate = (UserControls.ExchangeRateControl)e.Item.FindControl("ucExchangeRate");
			HtmlInputHidden txtContractCostCashCode = (HtmlInputHidden)e.Item.FindControl("txtContractCostCashCode");

			if (this.txtIsContract.Value == "1") 
			{
				if ( pm_sContractCostCode != "" && pm_sContractCostCode != "-1")
				{
					string ud_sContractCode = this.txtContractCode.Value;

					EntityData entity = DAL.EntityDAO.ContractDAO.GetStandard_ContractByCode(ud_sContractCode);
					DataRowView ud_drvItem = (DataRowView)e.Item.DataItem;

					DataView dv = new DataView(entity.Tables["ContractCostCash"],string.Format("ContractCostCode='{0}'",pm_sContractCostCode),"",DataViewRowState.CurrentRows);

					ud_ucExchangeRate.MoneyTypeDataSource = dv;
					ud_ucExchangeRate.MoneyTypeDataTextField = "MoneyType";
					ud_ucExchangeRate.MoneyTypeDataValueField = "ContractCostCashCode";
					ud_ucExchangeRate.MoneyType = ud_drvItem["MoneyType"].ToString();
					ud_ucExchangeRate.ExchangeRate = ud_drvItem["ExchangeRate"] == DBNull.Value ? Decimal.Zero:Decimal.Parse(ud_drvItem["ExchangeRate"].ToString());
                    ud_ucExchangeRate.Cash = ud_drvItem["ItemCash0"] == DBNull.Value ? Decimal.Zero : (decimal)ud_drvItem["ItemCash0"];
                    
					ud_ucExchangeRate.IsShowExchange = up_bEditExchangeRate;
					ud_ucExchangeRate.IsShowTitle = false;

					ud_ucExchangeRate.BindControl();


					if ( txtContractCostCashCode.Value != ud_ucExchangeRate.MoneyTypeValue )
					{
						txtContractCostCashCode.Value = ud_ucExchangeRate.MoneyTypeValue;
						ud_ucExchangeRate.Cash = BLL.PaymentRule.GetPaymentUHCashByContractCostCashCode(txtContractCostCashCode.Value);
					}

				}
				else
				{
					Init_MoneyTypeDropDownList("",e);
					txtContractCostCashCode.Value = "";
				}
			}
		}

		/// <summary>
		/// ѡ���ͬ���������ȱʡ����
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnHidSummaryChange_ServerClick(object sender, System.EventArgs e)
		{
			try 
			{
				DataTable tb = ScreenToTable(false);
				if (tb == null) return;

				string sIndex = this.txtSelectDetailItemIndex.Value;
				if (sIndex != "") 
				{
					int i = int.Parse(sIndex);
					string ud_sPaymentItemCode = this.dgList.DataKeys[i].ToString();

					DataRow[] drs = tb.Select("PaymentItemCode='" + ud_sPaymentItemCode + "'");
					if (drs.Length > 0) 
					{
						DataRow dr = drs[0];
						string AllocateCode = BLL.ConvertRule.ToString(dr["AllocateCode"]);
						string ud_sContractCostCashCode = dr["ContractCostCashCode"].ToString();

                        BLL.PaymentRule.SetPaymentItemDefaultFromContractAllocate(dr, AllocateCode, this.txtContractCode.Value, true, ud_sContractCostCashCode);
					}
				}

				BindDataGrid(tb);
			}
			catch(Exception ex) 
			{
				Response.Write(JavaScript.Alert(true, "��ʾ��ͬ����ȱʡ����ʧ�ܣ�" + ex.Message));
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
        }

        private void SetControlMessage(EntityData entity)
        {
            //�޸�
            if (entity.HasRecord())
            {
                //�ɱ��������ʱ��ת��ɱ����������޸�ҳ�� xyq 2018.7.21 add
                if (entity.GetString("Payer") == "�ɱ��������")
                {
                    Response.Redirect("CostBatchPaymentModify.aspx?PaymentCode=" + this.txtPaymentCode.Value);
                    Response.End();
                }

                DataRow dr = entity.CurrentRow;
                this.txtPaymentCode.Value = entity.GetString("PaymentCode");
                this.txtProjectCode.Value = entity.GetString("ProjectCode");
                this.txtIsContract.Value = entity.GetInt("IsContract").ToString();
                this.txtContractCode.Value = entity.GetString("ContractCode");
                this.txtStatus.Value = entity.GetInt("Status").ToString();
                this.TxtPaymentName.Value = entity.GetString("PaymentName");

                this.ucUnit.Value = entity.GetString("UnitCode");
                this.ucGroup.Value = entity.GetString("GroupCode");
                this.dtPayDate.Value = entity.GetDateTimeOnlyDate("PayDate");
                //						this.lblMoney.Text = BLL.MathRule.GetDecimalShowString(dr["Money"]);

                this.txtPaymentID.Value = this.txtPaymentCode.Value;
                this.txtRecieptCount.Value = entity.GetInt("RecieptCount").ToString();
                //						this.txtMoney.Text = BLL.StringRule.BuildGeneralNumberString(entity.GetDecimal("Money"));
                this.txtPurpose.Value = entity.GetString("Purpose");
                //						WBSCode = entity.GetString("WBSCode");
                this.txtPayer.Value = entity.GetString("Payer");
                this.txtSupplyCode.Value = entity.GetString("SupplyCode");
                this.txtSupplyName.Value = entity.GetString("SupplyName");
                this.spanSupplyName.InnerText = this.txtSupplyName.Value;

                this.txtPaymentTitle.Value = entity.GetString("PaymentTitle");

                this.txtBankName.Value = entity.GetString("BankName");
                this.txtBankAccount.Value = entity.GetString("BankAccount");

                this.txtRemark.Value = entity.GetString("Remark");

                this.txtIssue.Value = entity.GetInt("Issue").ToString();
                this.txtApplyPerson.Value = entity.GetString("ApplyPerson");
                this.txtApplyPersonName.Value = BLL.SystemRule.GetUserName(entity.GetString("ApplyPerson"));


                //Ȩ��
                string OperationCode = "";
                if (entity.GetInt("Status") == 0)
                {
                    //�޸�Ȩ��
                    OperationCode = "060103";
                }
                else
                {
                    //��˺��޸�Ȩ��
                    OperationCode = "060106";
                }
                /*
                ArrayList ar = user.GetResourceRight(PaymentCode, "Payment");
                if (!ar.Contains(OperationCode))
                {
                    Response.Redirect("../RejectAccess.aspx?OperationCode=" + OperationCode);
                    Response.End();
                }*/

            }
            else
            {
                //if()
                Response.Write(Rms.Web.JavaScript.Alert(true, "��������"));
                Response.Write(Rms.Web.JavaScript.WinClose(true));
                return;
            }
            entity.Dispose();
        }


        private void SetNewAddedUnit()
        {
            string userstation = user.BuildStationCodes();
            string[] station = userstation.Split(new char[] { ',' });

            if (station.Length == 1)
            {
                this.ucUnit.Value = BLL.SystemRule.GetUnitByStationCode(station[0]);
            }
            //����ʱ�������ϸ�λʱȡ��ǰ��Ŀ��Ӧ���� Lys070216
            else if (station.Length > 1)
            {
                int StationCount = station.Length;
                //�õ���Ŀ�µĲ���
                EntityData UnitinProject = BLL.SystemRule.GetUnitUnderProject(this.txtProjectCode.Value);
                int StationInUnitCount;
                EntityData StationInUnit;
                string UnitInProject;
                int UnitRowCount = UnitinProject.CurrentTable.Rows.Count;
                int SelectStationCount = 0;
                //�õ���Ŀ�µ�ǰ�û���Ӧ�Ĳ���
                for (int i = 0; i < UnitRowCount; i++)
                {
                    UnitinProject.SetCurrentRow(i);
                    //������Ŀ�õ����ţ����ݲ��ŵõ���λ
                    UnitInProject = UnitinProject.GetString("UnitCode");
                    StationInUnit = RmsPM.DAL.EntityDAO.OBSDAO.GetStationByUnitCode(UnitInProject);

                    StationInUnitCount = StationInUnit.CurrentTable.Rows.Count;
                    for (int k = 0; k < StationInUnitCount; k++)
                    {
                        StationInUnit.SetCurrentRow(k);
                        string SingleStationInUnit = StationInUnit.GetString("StationCode");
                        for (int j = 0; j < StationCount; j++)
                        {
                            if (SingleStationInUnit == station[j])
                            {
                                SelectStationCount++;
                                this.ucUnit.Value = BLL.SystemRule.GetUnitByStationCode(station[j]);
                            }
                        }
                    }
                    StationInUnit.Dispose();
                }
                //���������ڸ���Ŀ���ж�����ţ��Ͳ���
                if (SelectStationCount > 1)
                    this.ucUnit.Value = "";
                UnitinProject.Dispose();
            }
        }

        #region ������ȡ��Ϣ
        private EntityData SetDefaultValue()
        {
            BLL.PayRule pr = new RmsPM.BLL.PayRule();
            pr.PayType=Request["Type"]+"";
            pr.ApplicationCode = Request["ApplicationCode"] + "";
            return pr.Entity;
        }
        #endregion

    }
}
