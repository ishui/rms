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
using RmsReport;

namespace RmsPM.Web.CostBudget
{
	/// <summary>
	/// CostBudgetInfoB ��ժҪ˵����
	/// </summary>
	public partial class CostBudgetInfoB : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtPaymentCode;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtContractCode;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtIsContract;
		protected System.Web.UI.HtmlControls.HtmlAnchor hrefViewVoucher;
		protected System.Web.UI.HtmlControls.HtmlTableRow trContract;
		protected System.Web.UI.WebControls.Label lblPurpose;
		protected System.Web.UI.HtmlControls.HtmlGenericControl spanTitle;
		protected System.Web.UI.WebControls.Label lblPaymentID;
		protected System.Web.UI.WebControls.Label lblRecieptCount;
		protected System.Web.UI.WebControls.Label lblContractID;
		protected System.Web.UI.WebControls.Label lblContractName;
		protected System.Web.UI.WebControls.Label lblPayer;
		protected System.Web.UI.WebControls.Label lblPayDate;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnAddDtl;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnAddFromCost;
		protected System.Web.UI.WebControls.Label lblMoney;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnPayout;
		protected System.Web.UI.WebControls.Label lblIsContractName;
		protected System.Web.UI.WebControls.DataGrid dgContractAllocation;
		protected System.Web.UI.HtmlControls.HtmlTable trContract2;
		protected System.Web.UI.WebControls.DataGrid dgPayoutItem;
		protected System.Web.UI.HtmlControls.HtmlTable trContract3;
		protected System.Web.UI.WebControls.DataGrid dgContractPaymentPlan;
		protected System.Web.UI.HtmlControls.HtmlTable trContract4;
		protected System.Web.UI.HtmlControls.HtmlTable trContract5;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnAccount;
		protected System.Web.UI.WebControls.Label lblRemark;
		protected System.Web.UI.WebControls.Label lblSupplyName;
		protected System.Web.UI.WebControls.Label lblOldMoney;
		protected System.Web.UI.HtmlControls.HtmlGenericControl spanOldMoney;
		protected System.Web.UI.HtmlControls.HtmlGenericControl spanOldMoney2;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtSubjectSetCode;
		protected System.Web.UI.HtmlControls.HtmlTable tbOpiniont;
		protected System.Web.UI.HtmlControls.HtmlTable tbOpinionv;
		protected System.Web.UI.WebControls.Label lblOpinion;
		protected System.Web.UI.WebControls.Label lblBackupDate;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label lblBackupPersonName;
		protected System.Web.UI.WebControls.Label lblBackupDescription;
		protected System.Web.UI.WebControls.Label lblBackupName;
	
		/// <summary>
		/// ��λ
		/// </summary>
		public BLL.CostBudgetPageRule.m_MoneyUnit MoneyUnit
		{
			get {return BLL.CostBudgetPageRule.GetMoneyUnit(this.sltMoneyUnit);}
		}

		/// <summary>
		/// �����ʾ
		/// </summary>
		/// <param name="money"></param>
		/// <returns></returns>
		public string GetMoneyShowString(object money)
		{
			try 
			{
				return BLL.CostBudgetPageRule.GetMoneyShowString(money, MoneyUnit);
			}
			catch(Exception ex) 
			{
				throw ex;
			}
		}

		/// <summary>
		/// �����ʾ
		/// </summary>
		/// <param name="money"></param>
		/// <returns></returns>
		public string GetMoneyShowString(object money, string MoneyType)
		{
			try 
			{
				return BLL.CostBudgetPageRule.GetMoneyShowString(money, MoneyUnit, MoneyType);
			}
			catch(Exception ex) 
			{
				throw ex;
			}
		}

		/// <summary>
		/// ԭʼ������ʾ��Ԫ��
		/// </summary>
		/// <param name="money"></param>
		/// <returns></returns>
		public string GetMoneyShowHint(object money)
		{
			try 
			{
				string hint = BLL.CostBudgetPageRule.GetWanDecimalShowHint(money);

				return hint;
			}
			catch(Exception ex) 
			{
				throw ex;
			}
		}

		private string m_Guid
		{
			get 
			{
				if (BLL.ConvertRule.ToString(ViewState["m_Guid"]) == "")
				{
					ViewState["m_Guid"] = this.txtCostBudgetSetCode.Value;
				}

				return ViewState["m_Guid"].ToString();
			}
		}

		public string SessionEntityID 
		{
			get
			{
				if (this.txtCostBudgetBackupCode.Value != "")
				{
//					return "";
					return "CostBudgetDynamic_backup";
				}
				else 
				{
					return "CostBudgetDynamic_" + m_Guid;
				}
			}
		}

		/// <summary>
		/// ��̬����ʵ��
		/// </summary>
		private BLL.CostBudgetDynamic m_dyn
		{
			get
			{
				if ((SessionEntityID == "") || (Session[SessionEntityID] == null))
					return null;

				return (BLL.CostBudgetDynamic)Session[SessionEntityID];
			}
			set
			{
				if (SessionEntityID != "") 
				{
					Session[SessionEntityID] = value;
				}
			}
		}

		/// <summary>
		/// �Ƿ����޸�Ȩ��
		/// </summary>
		public bool RightModify 
		{
			get {return BLL.ConvertRule.ToBool(ViewState["RightModify"]);}
			set {ViewState["RightModify"] = value;}
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.ucCostBudgetSelectMonth.GotoMonthClick += new System.EventHandler(this.btnGotoMonth_ServerClick);

			if ( !IsPostBack)
			{
				IniPage();

                string Act = "" + Request.QueryString["Act"];
                switch (Act.ToLower())
                {
                    case "excel":
                        Excel(true);
                        return;
                }

                LoadData();
			}
		}

		private void IniPage()
		{
			try
			{
				this.txtCostBudgetSetCode.Value = Request.QueryString["CostBudgetSetCode"];
				this.txtCostBudgetBackupCode.Value = Request.QueryString["CostBudgetBackupCode"];
				this.txtFromUrl.Value = Request.QueryString["FromUrl"];

				this.txtSessionEntityID.Value = SessionEntityID;
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
				EntityData entityBackup = null;

				if (this.txtCostBudgetBackupCode.Value != "")
				{
					//ȡ���ݱ�
					entityBackup = DAL.EntityDAO.CostBudgetDAO.GetV_CostBudgetBackupByCode(this.txtCostBudgetBackupCode.Value);
					if (!entityBackup.HasRecord()) 
					{
						Response.Write(Rms.Web.JavaScript.Alert(true, "��Ŀ���ñ��ݱ�����"));
						Response.Write(Rms.Web.JavaScript.WinClose(true));
						return;
					}

					//ȡ���ݵ�Ԥ�����ñ�
					EntityData entitySet = DAL.EntityDAO.CostBudgetDAO.GetCostBudgetBackupSetByBackupCode(this.txtCostBudgetBackupCode.Value, this.txtCostBudgetSetCode.Value, true);
					if (!entitySet.HasRecord()) 
					{
						Response.Write(Rms.Web.JavaScript.Alert(true, "�����в�����Ԥ���"));
						Response.Write(Rms.Web.JavaScript.WinClose(true));
						return;
					}

					this.txtCostBudgetBackupSetCode.Value = entitySet.GetString("CostBudgetBackupSetCode");
					BindData(entitySet);

					entitySet.Dispose();
				}
				else
				{
					//ȡԤ�����ñ�
					EntityData entitySet = DAL.EntityDAO.CostBudgetDAO.GetV_CostBudgetSetByCode(txtCostBudgetSetCode.Value);
					if ( entitySet.HasRecord())
					{
						BindData(entitySet);
					}
					else 
					{
						Response.Write(Rms.Web.JavaScript.Alert(true, "Ԥ�����ñ�����"));
						Response.Write(Rms.Web.JavaScript.WinClose(true));
						return;
					}
					entitySet.Dispose();
				}

				this.lblDynamicDateDesc.InnerText = BLL.CostBudgetRule.GetDynamicDateDesc(this.txtCostBudgetBackupCode.Value, entityBackup);

				if (entityBackup != null) entityBackup.Dispose();

				BLL.PageFacade.LoadCostBudgetBackupSelect(this.sltBackup, this.txtCostBudgetBackupCode.Value, this.txtProjectCode.Value);

				//Ȩ��
				this.RightModify = true;

				LoadCostBudgetData();
				LoadDataGrid(false);

				//Ȩ��
				/*
				ArrayList ar = user.GetResourceRight(this.txtCostBudgetSetCode.Value,"CostBudgetSet");
				if ( ! ar.Contains("041301"))  //��̬Ԥ��鿴
				{
					Response.Redirect( "../RejectAccess.aspx?OperationCode=041301" );
					Response.End();
				}

				this.btnModify.Visible = ar.Contains("041302");
				this.btnModifyEx.Visible = ar.Contains("041304");
				this.btnDelete.Visible = ar.Contains("041302");
				this.btnCheck.Visible = ar.Contains("041303");
				this.btnModifySet.Visible = ar.Contains("041103");
				*/
			}
			catch (  Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾԤ������" + ex.Message));
			}
		}

		private void BindData(EntityData entitySet)
		{
			try 
			{
				DataRow dr = entitySet.CurrentRow;

				this.lblCostBudgetSetName.Text = entitySet.GetString("CostBudgetSetName");
				this.txtProjectCode.Value = entitySet.GetString("ProjectCode");
				this.lblGroupName.Text = entitySet.GetString("GroupName");
				this.lblUnitName.Text = entitySet.GetString("UnitName");
				this.lblPBSName.Text = entitySet.GetString("PBSName");
				this.txtPBSType.Value = entitySet.GetString("PBSType");
				this.txtPBSCode.Value = entitySet.GetString("PBSCode");
				this.lblBuildingArea.Text = BLL.StringRule.AddUnit(BLL.StringRule.BuildShowNumberString(entitySet.GetDecimal("BuildingArea")), "ƽ��");
				this.lblHouseCount.Text = BLL.StringRule.BuildShowNumberString(entitySet.GetDecimal("HouseCount"), "#,##0.##");
				this.lblHouseArea.Text = BLL.StringRule.AddUnit(BLL.StringRule.BuildShowNumberString(entitySet.GetDecimal("HouseArea")), "ƽ��");
			}
			catch (  Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾԤ������" + ex.Message));
			}
		}

		/// <summary>
		/// ��ʾ��̬������ͷ
		/// </summary>
		private void LoadCostBudgetData()
		{
			try 
			{
				if (this.txtCostBudgetBackupCode.Value != "") 
				{
					this.txtCostBudgetCode.Value = "";

					//ȡ���ݵ�Ԥ�����ñ�
					EntityData entity = DAL.EntityDAO.CostBudgetDAO.GetV_CostBudgetBackupSetByCode(this.txtCostBudgetBackupSetCode.Value);
					if (entity.HasRecord())
					{
						this.lblModifyDate.Text = entity.GetDateTimeOnlyDate("DynamicModifyDate");
						this.lblModifyPersonName.Text = entity.GetString("DynamicModifyPersonName");
					}
					entity.Dispose();
				}
				else 
				{
					//ȡ��̬������
					EntityData entity = BLL.CostBudgetRule.GetValidCostBudget(this.txtCostBudgetSetCode.Value, 0, true);
					if (entity.HasRecord()) 
					{
						this.txtCostBudgetCode.Value = entity.GetString("CostBudgetCode");

						this.lblModifyDate.Text = entity.GetDateTimeOnlyDate("ModifyDate");
						this.lblModifyPersonName.Text = entity.GetString("ModifyPersonName");
					}
					entity.Dispose();
				}

				/*
						switch (this.txtStatus.Value)
						{
							case "1"://��ǰ
								this.btnModify.Style["display"] = "none";
								this.btnDelete.Style["display"] = "none";
								this.btnCheck.Style["display"] = "none";

								this.btnModifyEx.Style["display"] = "";
								this.btnViewHistory.Style["display"] = "";

								break;

							case "2"://��ʷ
								this.btnModify.Style["display"] = "none";
								this.btnDelete.Style["display"] = "none";
								this.btnCheck.Style["display"] = "none";

								this.btnModifyEx.Style["display"] = "none";
								this.btnViewHistory.Style["display"] = "none";

								break;

							default: //δ�󡢵���
								this.btnModify.Style["display"] = "";
								this.btnDelete.Style["display"] = "";
								this.btnCheck.Style["display"] = "";

								this.btnModifyEx.Style["display"] = "none";
								this.btnViewHistory.Style["display"] = "none";

								break;
						}
						*/

			}
			catch (  Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ��̬������ͷ����" + ex.Message));
			}
		}

		private BLL.CostBudgetDynamic GenerateDynamic()
		{
			try 
			{
				string StartY = "";
				string EndY = "";

				if (this.ucCostBudgetSelectMonth.ShowMonth) 
				{
					StartY = this.ucCostBudgetSelectMonth.MonthStart;
					EndY = this.ucCostBudgetSelectMonth.MonthEnd;
				}

				BLL.CostBudgetDynamic dyn = new RmsPM.BLL.CostBudgetDynamic(this.txtProjectCode.Value, this.txtCostBudgetSetCode.Value, this.txtCostBudgetBackupCode.Value);
				dyn.StartY = StartY;
				dyn.EndY = EndY;
				dyn.IsModify = this.RightModify;

				dyn.Generate();

				return dyn;
			}
			catch(Exception ex) 
			{
				throw ex;
			}
		}

		/// <summary>
		/// ���ɶ�̬������ϸ
		/// </summary>
		private void LoadDataGrid(bool IsScreenToTable)
		{
			try 
			{
				BLL.CostBudgetDynamic dyn = this.GenerateDynamic();
				ViewState["StartY"] = dyn.StartY;
				ViewState["EndY"] = dyn.EndY;

				/*
				string html_title_target_money1 = "";
				string html_title_target_money2 = "";
				BLL.CostBudgetPageRule.GenerateCostBudgetMoneyTitleHtml(dyn.tb, "BudgetMoney", "����Ԥ��", "��ǰ", ref html_title_target_money1, ref html_title_target_money2, false);

				ViewState["html_title_target_money1"] = html_title_target_money1;
				ViewState["html_title_target_money2"] = html_title_target_money2;
				*/

				//Ԥ��ƻ������չ��
				string html_title1 = "";
				string html_title2 = "";
				BLL.CostBudgetPageRule.GenerateCostBudgetPlanTitleHtml(dyn.tb, dyn.iStartY, dyn.iEndY, ref html_title1, ref html_title2);

				ViewState["html_title1"] = html_title1;
				ViewState["html_title2"] = html_title2;

				//�ݴ���ϸ��
				m_dyn = dyn;

				BindDataGrid(dyn, IsScreenToTable);

				if (this.txtCostBudgetBackupCode.Value != "")
				{
					m_dyn = null;
				}
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ��̬������ϸ����" + ex.Message));
			}
		}

		/// <summary>
		/// �󶨶�̬������ϸ
		/// </summary>
		private void BindDataGrid(BLL.CostBudgetDynamic dyn, bool IsScreenToTable) 
		{
			try 
			{
				DataTable tbDtl = dyn.tbHtml;

				//����Ԥ����������
				this.lblTargetCheckDate.Text = dyn.GetTargetCheckDate();
				string VerID = dyn.GetTargetVerID();
				this.txtHasTargetHis.Value = (BLL.ConvertRule.ToDecimal(VerID) > 0)?"1":"";

				if (VerID != "") VerID = "�汾" + VerID;

				//�汾������
				this.spanTargetVerID.InnerText = "";
				this.hrefTargetVerID.InnerText = "";
				if (this.txtHasTargetHis.Value == "1")  //����ʷ�汾
				{
					this.hrefTargetVerID.InnerText = VerID;
				}
				else 
				{
					this.spanTargetVerID.InnerText = VerID;
				}

				//��ʷĿ�����
				string TargetHisHead1 = "";
				string TargetHisHead2 = "";
				dyn.GenerateTargetHisHead(ref TargetHisHead1, ref TargetHisHead2);

				ViewState["TargetHisHead1"] = TargetHisHead1;
				ViewState["TargetHisHead2"] = TargetHisHead2;

				ViewState["HasTargetChange"] = dyn.HasTargetChange;
				if (dyn.HasTargetChange)
				{
					this.spanListTitleTargetMoneyDesc.InnerText = dyn.TargetChangeDesc;
					this.spanListTitleTargetMoney.Style["display"] = "";
				}
				else 
				{
					this.spanListTitleTargetMoneyDesc.InnerText = "";
					this.spanListTitleTargetMoney.Style["display"] = "none";
				}

				if (IsScreenToTable)
				{
					//��Ļ���ݱ��浽��ʱ��
					tbDtl = ScreenToTable(false);
				}
				else 
				{
					//�۵�ȫ��������
					BLL.CostBudgetPageRule.CollapseAll(tbDtl);

					if (dyn.NeedApport) //�з�̯ʱ������һ���ܼ�
					{
						BLL.CostBudgetPageRule.ExpandDeep(tbDtl, 3);
					}
					else 
					{
						BLL.CostBudgetPageRule.ExpandDeep(tbDtl, 2);
					}
				}

				this.dgList.DataSource = tbDtl;
				this.dgList.DataBind();

				/*
				string[] arrField = {"ItemMoney", "OldItemMoney", "TotalPayoutMoney", "RemainItemMoney"};
				decimal[] arrSum = BLL.MathRule.SumColumn(tb, arrField);
				this.dgList.Columns[2].FooterText = arrSum[0].ToString("N");
				this.dgList.Columns[3].FooterText = arrSum[1].ToString("N");
				this.dgList.Columns[6].FooterText = arrSum[2].ToString("N");
				this.dgList.Columns[7].FooterText = arrSum[3].ToString("N");

				this.dgList.DataSource = tb;
				this.dgList.DataBind();
				*/
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "�󶨶�̬������ϸ����" + ex.Message));
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

		private void GoBack() 
		{
			Response.Write(Rms.Web.JavaScript.ScriptStart);

			Response.Write("try {window.opener.Refresh();}");
			Response.Write("catch(e){window.opener.location = window.opener.location;}");

			//			Response.Write("window.opener.location = window.opener.location;");
			Response.Write(Rms.Web.JavaScript.WinClose(false));
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
		}

		/// <summary>
		/// ɾ��
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnDelete_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				BLL.CostBudgetRule.DeleteCostBudget(this.txtCostBudgetCode.Value);

				GoBack();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "ɾ����̬Ԥ�����" + ex.Message));
			}
		}

		/// <summary>
		/// ���
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnCheck_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				BLL.CostBudgetRule.CheckCostBudget(this.txtCostBudgetCode.Value, user.UserCode);

				GoBack();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��̬Ԥ����˳���" + ex.Message));
			}
		}

		/*
		private void dgList_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
		{
			if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
			{
				int iStartY = BLL.ConvertRule.ToInt(ViewState["StartY"]);
				int iEndY = BLL.ConvertRule.ToInt(ViewState["EndY"]);

				//ȱʡչ������
				int curr_year = DateTime.Today.Year;

				DataTable tbDtl = m_dyn.tb;

				string CostBudgetDtlCode = ((HtmlInputHidden)e.Item.FindControl("txtCostBudgetDtlCode")).Value;
				string CostCode = ((HtmlInputHidden)e.Item.FindControl("txtCostCode")).Value;
				
				if (!CostBudgetDtlCode.StartsWith("R_"))  //�ϼ����޺�ͬ��ϸ
				{
					//Ԥ����ϸ��¼
					DataRow[] drsDtl = tbDtl.Select("CostBudgetDtlCode = '" + CostBudgetDtlCode + "'");
					DataRow drDtl = null;
					if (drsDtl.Length > 0) 
					{
						drDtl = drsDtl[0];
					}

					//��ͬ��ϸ
					PlaceHolder phContract = (PlaceHolder)e.Item.FindControl("phContract");
					DataTable tbContract = m_dyn.tbContract;
					DataRow[] drsContract = tbContract.Select("CostCode = '" + CostCode + "'", "ContractID, ContractName", DataViewRowState.CurrentRows);
					this.m_dyn.AddRowContract(e.Item.ItemIndex, drDtl, drsContract, phContract, iStartY, iEndY, curr_year.ToString(), this.MoneyUnit); 

					//�޺�ͬ���ռһ��
					DataTable tbNoContract = m_dyn.tbNoContract;
					DataRow[] drsNoContract = tbNoContract.Select("CostCode = '" + CostCode + "'");
					this.m_dyn.AddRowBalance(e.Item.ItemIndex, drDtl, drsContract, drsNoContract, phContract, iStartY, iEndY, curr_year.ToString(), this.RightModify, this.MoneyUnit);
				}
			}
		}
		*/

		/// <summary>
		/// ��ʾĳ����Χ����ȼƻ�
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnGotoMonth_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
//				DateTime t1 = DateTime.Now;

				LoadDataGrid(true);

				/*
				DateTime t2 = DateTime.Now;
				TimeSpan t = t2.Subtract(t1);
				Response.Write(Rms.Web.JavaScript.Alert(true, t.Duration().ToString()));
				*/
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ��ȼƻ�����" + ex.Message));
			}
		}

		/// <summary>
		/// ��ʾ��ʷԤ����
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnShowBudgetMoneyHis_ServerClick(object sender, System.EventArgs e)
		{
			try 
			{
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ��ʷԤ�������" + ex.Message));
			}
		}

		/// <summary>
		/// ������ʷԤ����
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnHideBudgetMoneyHis_ServerClick(object sender, System.EventArgs e)
		{
			try 
			{
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "������ʷԤ�������" + ex.Message));
			}
		}

		/// <summary>
		/// ��ʾ��ʷĿ�����
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnShowTargetMoneyHis_ServerClick(object sender, System.EventArgs e)
		{
			try 
			{
				if (m_dyn == null)
				{
					LoadDataGrid(true);
				}
				else 
				{
//					m_dyn.ShowTargetHisVerID = this.txtShowTargetMoneyHisVerID.Value;
					m_dyn.ShowTargetHis = true;
					m_dyn.RefreshTargetHis();
					BindDataGrid(m_dyn, true);

					this.txtShowTargetHis.Value = "1";
				}
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ��ʷĿ����ó���" + ex.Message));
			}
		}

		/// <summary>
		/// ������ʷĿ�����
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnHideTargetMoneyHis_ServerClick(object sender, System.EventArgs e)
		{
			try 
			{
				if (m_dyn == null)
				{
					LoadDataGrid(true);
				}
				else 
				{
//					m_dyn.ShowTargetHisVerID = "";
					m_dyn.ShowTargetHis = false;
					m_dyn.RefreshTargetHis();
					BindDataGrid(m_dyn, true);

					this.txtShowTargetHis.Value = "";
				}
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "������ʷĿ����ó���" + ex.Message));
			}
		}

		/// <summary>
		/// ��Ļ���ݱ��浽��ʱ��
		/// </summary>
		/// <returns></returns>
		private DataTable ScreenToTable(bool isBindGrid) 
		{
			DataTable tb = m_dyn.tbHtml;

			string[] arrExpandNode = this.txtExpandNode.Value.Split(","[0]);

			foreach(DataRow dr in tb.Rows) 
			{
				string CostBudgetDtlCode = BLL.ConvertRule.ToString(dr["CostBudgetDtlCode"]);
				int expand = (BLL.ConvertRule.FindArray(arrExpandNode, CostBudgetDtlCode, true) >= 0)?1:0;
				dr["IsExpand"] = expand;
			}

			/*
			foreach (RepeaterItem item in this.dgList.Items)
			{
				HtmlInputHidden txtCostBudgetDtlCode = (HtmlInputHidden)item.FindControl("txtCostBudgetDtlCode");
				HtmlInputHidden txtIsExpand = (HtmlInputHidden)item.FindControl("txtIsExpand");

				string CostBudgetDtlCode = txtCostBudgetDtlCode.Value;

				DataRow dr = null;
				DataRow[] drs = tb.Select("CostBudgetDtlCode='" + CostBudgetDtlCode + "'");
				if (drs.Length > 0) 
				{
					dr = drs[0];
				}
				else
				{
					throw new Exception("��ϸ���" + CostBudgetDtlCode + "����ʱ���в����ڣ����ܱ���");
				}

				dr["IsExpand"] = BLL.ConvertRule.ToInt(txtIsExpand.Value);
			}
			*/

			if (isBindGrid) 
			{
				this.dgList.DataSource = tb;
				this.dgList.DataBind();
			}

			return tb;
		}

		/// <summary>
		/// ˢ��ȫ��
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnRefresh_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				LoadCostBudgetData();
				LoadDataGrid(true);
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "ˢ�³���" + ex.Message));
			}
		}

		/// <summary>
		/// ˢ��Ŀ�����
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnRefreshTarget_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
//				DateTime t1 = DateTime.Now;

				if (m_dyn == null)
				{
					LoadDataGrid(true);
				}
				else 
				{
					m_dyn.RefreshChangeTarget();
					BindDataGrid(m_dyn, true);
				}

				/*
				DateTime t2 = DateTime.Now;
				TimeSpan t = t2.Subtract(t1);
				Response.Write(Rms.Web.JavaScript.Alert(true, t.Duration().ToString()));
				*/
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "ˢ�³���" + ex.Message));
			}
		}

		/// <summary>
		/// ˢ��Ԥ�����
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnRefreshBalance_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
//				DateTime t1 = DateTime.Now;

				LoadCostBudgetData();

				if (m_dyn == null)
				{
					LoadDataGrid(true);
				}
				else 
				{
					m_dyn.CostBudgetCode = this.txtCostBudgetCode.Value;
					m_dyn.RefreshBalance();
					BindDataGrid(m_dyn, true);
				}

				/*
				DateTime t2 = DateTime.Now;
				TimeSpan t = t2.Subtract(t1);
				Response.Write(Rms.Web.JavaScript.Alert(true, t.Duration().ToString()));
				*/
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "ˢ�³���" + ex.Message));
			}
		}

		/// <summary>
		/// ˢ�º�ͬ�ƻ�
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnRefreshPurchase_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				if (m_dyn == null)
				{
					LoadDataGrid(true);
				}
				else 
				{
					m_dyn.RefreshBidding();
					BindDataGrid(m_dyn, true);
				}
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "ˢ�³���" + ex.Message));
			}
		}

		/// <summary>
		/// �ı��λ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnChangeMoneyUnit_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				LoadCostBudgetData();

				if (m_dyn == null)
				{
					LoadDataGrid(true);
				}

				m_dyn.MoneyUnit = this.MoneyUnit;
				m_dyn.RefreshMoneyUnit();
				BindDataGrid(m_dyn, true);
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "ˢ�½�λ����" + ex.Message));
			}
		}

		/// <summary>
		/// ˢ�º�ͬԤ��
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnRefreshCostBudgetContract_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				if (m_dyn == null)
				{
					LoadDataGrid(true);
				}
				else 
				{
					m_dyn.RefreshCostBudgetContract();
					BindDataGrid(m_dyn, true);
				}
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "ˢ�³���" + ex.Message));
			}
		}

		/// <summary>
		/// �Ӻ�ͬ��ϸ
		/// </summary>
		/// <param name="tbDtl"></param>
		/// <param name="drDtl"></param>
		/// <param name="drsContract"></param>
		/// <param name="drsNoContract"></param>
		private static void AddContract(DataTable tbDtl, DataRow drDtl, DataRow[] drsContract, ref int ReportDtlSno)
		{
			try 
			{
				//��ͬ��ϸֱ����ʾ�ڷ�������ʱ����ͬ��ϸ��������ʾ
				if (BLL.ConvertRule.ToString(drDtl["ContractCode"]) != "")
					return;

				foreach(DataRow drContract in drsContract) 
				{
					DataRow drNew = tbDtl.NewRow();
					BLL.ConvertRule.DataRowCopy(drContract, drNew, drContract.Table, tbDtl);

					drNew["CostBudgetDtlCode"] = "Contract_" + BLL.ConvertRule.ToString(drContract["ContractCode"]);

					ReportDtlSno++;
					drNew["ReportDtlSno"] = ReportDtlSno;

					tbDtl.Rows.Add(drNew);
				}
			}
			catch(Exception ex) 
			{
				throw ex;
			}
		}

		/// <summary>
		/// ��Ԥ�����
		/// </summary>
		/// <param name="tbDtl"></param>
		/// <param name="drDtl"></param>
		/// <param name="drsContract"></param>
		/// <param name="drsNoContract"></param>
		private static void AddNoContract(DataTable tbDtl, DataRow drDtl, DataRow[] drsContract, DataRow[] drsNoContract, ref int ReportDtlSno)
		{
			try 
			{
				//��Ҷ���ķ������û�к�ͬʱ������ʾԤ�����
				if ((!BLL.ConvertRule.ToBool(drDtl["IsLeafCBS"])) && (drsContract.Length <= 0)) return;

				//Ԥ�����ֱ����ʾ�ڷ�������ʱ��Ԥ����������ʾ
				if (BLL.ConvertRule.ToString(drDtl["ContractCode"]) == "BALANCE") return;

				foreach(DataRow drNoContract in drsNoContract) 
				{
					//Ԥ�����ķ���Ϊ0ʱ������ʾ
					if ((BLL.ConvertRule.ToDecimal(drNoContract["ContractMoney"]) == 0)
						&& (BLL.ConvertRule.ToDecimal(drNoContract["ContractChangeMoney"]) == 0)
						&& (BLL.ConvertRule.ToDecimal(drNoContract["ContractApplyMoney"]) == 0)
						)
						continue;

					DataRow drNew = tbDtl.NewRow();
					BLL.ConvertRule.DataRowCopy(drNoContract, drNew, drNoContract.Table, tbDtl);

					drNew["CostBudgetDtlCode"] = "NoContract_" + BLL.ConvertRule.ToString(drNoContract["ContractCode"]);
					drNew["ContractName"] = "Ԥ�����";

					ReportDtlSno++;
					drNew["ReportDtlSno"] = ReportDtlSno;

					tbDtl.Rows.Add(drNew);
				}
			}
			catch(Exception ex) 
			{
				throw ex;
			}
		}

		/// <summary>
		/// ����Excel
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Excel(bool CloseWindow)
		{
			try 
			{
				BLL.CostBudgetDynamic dyn = m_dyn;
				if (dyn == null)
				{
					dyn = GenerateDynamic();
				}

				TExcel excel = new TExcel(Response, Request, Server, Session);
				try 
				{
					excel.StartRow = 10;
					excel.ColumnHeadRow = 9;

					//ȡ1��������
					DataView dv;
					if (dyn.NeedApport) //�з�̯ʱ������һ���ܼ�
					{
						dv = new DataView(dyn.tb, "Deep=3", "SortID", DataViewRowState.CurrentRows);
					}
					else 
					{
						dv = new DataView(dyn.tb, "Deep=2", "SortID", DataViewRowState.CurrentRows);
					}

					excel.DataSource = dv;

					//�½�������
					excel.TemplateFileName = "��Ŀ���ñ�.xls";
//					excel.TemplateSheetName = "����";
					excel.TemplateSheetName = "";
					excel.AddWorkbook();

					//�½����ɡ����ҳ
					for(int k=2;k<=dv.Count;k++) 
					{
						((Excel.Worksheet)excel.Book.Worksheets[2]).Copy(TExcel.m_Opt, excel.Book.Worksheets[2]);
					}

					//ת������ҳ
					excel.Sheet = (Excel.Worksheet)excel.Book.Worksheets[1];

					//��ͷ��Ϣ
					excel.SetCellValue(1, 1, BLL.ProjectRule.GetProjectName(dyn.ProjectCode));

					//��������
					if (this.txtCostBudgetBackupCode.Value != "") //����
					{
						excel.SetCellValue(1, 3, dyn.entityBackup.GetDateTime("BackupDate", "yyyy-MM-dd HH:mm:ss"));
					}
					else 
					{
						excel.SetCellValue(1, 3, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
					}

					excel.SetCellValue(1, 5, dyn.GetTargetCheckDate());
					excel.SetCellValue(1, 6, dyn.entitySet.GetDecimal("HouseCount"));
					excel.SetCellValue(1, 7, dyn.entitySet.GetDecimal("BuildingArea"));
					excel.SetCellValue(1, 8, dyn.entitySet.GetDecimal("HouseArea"));

					excel.DataSource = dv;
					excel.DataToSheet();

					//����2�����������
					excel.StartRow = 11;
					excel.ColumnHeadRow = 9;

					/*
					//2����������ʱ��ͷ
					DataTable tbLevel2 = dyn.entitySet.CurrentTable.Copy();
					BLL.ConvertRule.DataTableAddColumn(dyn.tb, tbLevel2);
					*/

					//3����������ʱ��������ͬ��ϸ��
					DataTable tbDtl = dyn.tb.Clone();
					tbDtl.Columns.Add("ReportDtlSno", typeof(int));

					DataColumn[] pk = new DataColumn[1];
					pk[0] = tbDtl.Columns["ReportDtlSno"];
					tbDtl.PrimaryKey = pk;

					DataRow drNew;

					int i=0;
					foreach(DataRowView drv in dv) 
					{
						i++;

//						BLL.ConvertRule.DataRowCopy(drv.Row, tbLevel2.Rows[0], dyn.tb, tbLevel2);

						string ParentCostCode = BLL.ConvertRule.ToString(drv["CostCode"]);
						string ParentCostName = BLL.ConvertRule.ToString(drv["CostName"]);

						//ȡ��2�������������
						DataView dv2 = new DataView(dyn.tb, "ParentCode='" + ParentCostCode + "'", "SortID", DataViewRowState.CurrentRows);

						//���Ƶ���ʱ��
						tbDtl.Rows.Clear();
						int ReportDtlSno = 0;

						foreach(DataRowView drv2 in dv2) 
						{
							DataRow dr2 = drv2.Row;
							string CostCode = BLL.ConvertRule.ToString(dr2["CostCode"]);

							//3��������
							drNew = tbDtl.NewRow();
							BLL.ConvertRule.DataRowCopy(dr2, drNew, dyn.tb, tbDtl);

							ReportDtlSno++;
							drNew["ReportDtlSno"] = ReportDtlSno;

							tbDtl.Rows.Add(drNew);

							//3��������ĺ�ͬ��ϸ
							DataRow[] drsContract = dyn.tbContract.Select("CostCode = '" + CostCode + "'", "ContractID, ContractName", DataViewRowState.CurrentRows);
							AddContract(tbDtl, drNew, drsContract, ref ReportDtlSno);

							//3���������Ԥ�����
							DataRow[] drsNoContract = dyn.tbNoContract.Select("CostCode = '" + CostCode + "'");
							AddNoContract(tbDtl, drNew, drsContract, drsNoContract, ref ReportDtlSno);

						}

						//�ϼ���
						drNew = tbDtl.NewRow();
						BLL.ConvertRule.DataRowCopy(drv.Row, drNew, dyn.tb, tbDtl);

						ReportDtlSno++;
						drNew["ReportDtlSno"] = ReportDtlSno;
						drNew["CostName"] = "�ϼ�";

						tbDtl.Rows.Add(drNew);

						/*
						//��2��2�������ʼ��ÿ�ζ�Ҫ�½�����ҳ
						if (i > 1) 
						{
							excel.TemplateSheetName = "����";
							excel.AddWorksheet(i, true);
						}
						*/

						//ת������ҳ
						excel.Sheet = (Excel.Worksheet)excel.Book.Worksheets[i + 1];

						//һ��2���������ӡһҳ
						excel.Sheet.Name = ParentCostName.Replace("/", "");
						excel.DataToSheetSingle(drv.Row);

						//2���������ͷ��Ϣ
//						excel.SetCellValue(1, 1, drv["CostName"]);

						DataView dvDtl = new DataView(tbDtl, "", "ReportDtlSno", DataViewRowState.CurrentRows);
						excel.DataSource = dvDtl;
						excel.DataToSheet();

						/*
						//��ͬ��ϸֻ��һ��ʱ�����غ�ͬ��ϸ
						int colIsHide = 15;
						for(int k=0;k<dv2.Count;k++) 
						{
							int row = excel.StartRow + k;
							string IsHide =  TExcel.GetCellValue(excel.Sheet, row, colIsHide);
							if (IsHide == "1")
							{
								((Excel.Range)excel.Sheet.Cells[row, 1]).EntireRow.Hidden = true;
							}
						}

						//������IsHide
						((Excel.Range)excel.Sheet.Cells[1, colIsHide]).EntireColumn.Hidden = true;
						*/
					}

					//ȱʡѡ�е�1ҳ
					((Excel.Worksheet)excel.Book.Worksheets[1]).Select(TExcel.m_Opt);

					//����
					excel.SaveWorkbook();
					excel.ShowClient();
				}
				finally 
				{
					excel.Dispose();
				}

				if (CloseWindow)
				{
					Response.Write(Rms.Web.JavaScript.WinClose(true));
				}
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "����Excel����" + ex.Message));
			}
		}

	}
}
