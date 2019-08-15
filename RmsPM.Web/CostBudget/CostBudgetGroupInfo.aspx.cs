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

namespace RmsPM.Web.CostBudget
{
	/// <summary>
	/// CostBudgetGroupInfo ��ժҪ˵����
	/// </summary>
	public partial class CostBudgetGroupInfo : CostBudgetPageBase
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
		protected System.Web.UI.WebControls.Label lblCheckPersonName;
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
		protected System.Web.UI.WebControls.Label lblUnitName;
		protected System.Web.UI.WebControls.Label lblOldMoney;
		protected System.Web.UI.HtmlControls.HtmlGenericControl spanOldMoney;
		protected System.Web.UI.HtmlControls.HtmlGenericControl spanOldMoney2;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtSubjectSetCode;
		protected System.Web.UI.HtmlControls.HtmlTable tbOpiniont;
		protected System.Web.UI.HtmlControls.HtmlTable tbOpinionv;
		protected System.Web.UI.WebControls.Label lblOpinion;
		protected System.Web.UI.WebControls.Label lblVerID;
		protected System.Web.UI.WebControls.Label lblPBSName;
		protected System.Web.UI.WebControls.Label lblCostSortID;
		protected System.Web.UI.WebControls.Label lblCostName;
		protected System.Web.UI.WebControls.Label lblCheckDate;
		protected System.Web.UI.WebControls.Label lblBackupDate;
	
		/// <summary>
		/// �Ƿ���ʾ��ʷԤ����
		/// </summary>
		public bool IsShowBudgetMoneyHis
		{
			get {return BLL.ConvertRule.ToBool(ViewState["IsShowBudgetMoneyHis"]);}
			set {ViewState["IsShowBudgetMoneyHis"] = value;}
		}

		/// <summary>
		/// �Ƿ���ʾ��ʷĿ�����
		/// </summary>
		public bool IsShowTargetMoneyHis
		{
			get {return BLL.ConvertRule.ToBool(ViewState["IsShowTargetMoneyHis"]);}
			set {ViewState["IsShowTargetMoneyHis"] = value;}
		}

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

		/*
		private string m_Guid
		{
			get 
			{
				if (BLL.ConvertRule.ToString(ViewState["m_Guid"]) == "")
				{
					ViewState["m_Guid"] = this.txtGroupCode.Value;
//					ViewState["m_Guid"] = System.Guid.NewGuid().ToString();
				}

				return ViewState["m_Guid"].ToString();
			}
		}

		public string SessionEntityID 
		{
			get
			{
				return "CostBudgetDynamic_" + m_Guid;
			}
		}

		/// <summary>
		/// ��̬����ʵ��
		/// </summary>
		private BLL.CostBudgetGroupDynamic m_dyn
		{
			get
			{
				if (Session[SessionEntityID] == null)
					return null;

				return (BLL.CostBudgetGroupDynamic)Session[SessionEntityID];
			}
			set
			{
				Session[SessionEntityID] = value;
			}
		}
		*/

		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.ucCostBudgetSelectMonth.GotoMonthClick += new System.EventHandler(this.btnGotoMonth_ServerClick);

			if ( !IsPostBack)
			{
				IniPage();
				LoadData();
			}
		}

		private void IniPage()
		{
			try
			{
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];
				this.txtGroupCode.Value = Request.QueryString["GroupCode"];
				this.txtCostBudgetBackupCode.Value = Request.QueryString["CostBudgetBackupCode"];
				this.txtFromUrl.Value = Request.QueryString["FromUrl"];

//				this.txtSessionEntityID.Value = SessionEntityID;
            }
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}


		private void LoadData()
		{
			string GroupCode = this.txtGroupCode.Value;

			try
			{
				//ȡԤ�����
				EntityData entity = DAL.EntityDAO.SystemManageDAO.GetSystemGroupByCode(GroupCode);
				if ( entity.HasRecord())
				{
					this.lblGroupName.Text = entity.GetString("GroupName");
				}
				else 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "Ԥ����𲻴���"));
					return;
				}
				entity.Dispose();

				EntityData entityBackup = null;

				if (this.txtCostBudgetBackupCode.Value != "")
				{
					//ȡ���ݱ�
					entityBackup = DAL.EntityDAO.CostBudgetDAO.GetV_CostBudgetBackupByCode(this.txtCostBudgetBackupCode.Value);
					if (!entityBackup.HasRecord()) 
					{
						Response.Write(Rms.Web.JavaScript.Alert(true, "��Ŀ���ñ��ݱ�����"));
						return;
					}
				}

				this.lblDynamicDateDesc.InnerText = BLL.CostBudgetRule.GetDynamicDateDesc(this.txtCostBudgetBackupCode.Value, entityBackup);

				if (entityBackup != null) entityBackup.Dispose();

				BLL.PageFacade.LoadCostBudgetBackupSelect(this.sltBackup, this.txtCostBudgetBackupCode.Value, this.txtProjectCode.Value);

				LoadDataGrid(false);

				/*
				//Ȩ��
				ArrayList ar = user.GetResourceRight(this.txtCostBudgetSetCode.Value,"CostBudgetSet");
				if ( ! ar.Contains("041201"))  //��̬Ԥ��鿴
				{
					Response.Redirect( "../RejectAccess.aspx?OperationCode=041201" );
					Response.End();
				}

				this.btnModify.Visible = ar.Contains("041202");
				this.btnModifyEx.Visible = ar.Contains("041204");
				this.btnDelete.Visible = ar.Contains("041202");
				this.btnCheck.Visible = ar.Contains("041203");
				this.btnModifySet.Visible = ar.Contains("041103");
				*/
			}
			catch (  Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾԤ������" + ex.Message));
			}
		}

		/// <summary>
		/// ��ʾ��̬������ϸ
		/// </summary>
		private void LoadDataGrid(bool IsScreenToTable)
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

				ViewState["StartY"] = StartY;
				ViewState["EndY"] = EndY;

				BLL.CostBudgetGroupDynamic dyn = new RmsPM.BLL.CostBudgetGroupDynamic(this.txtProjectCode.Value, this.txtGroupCode.Value, this.txtCostBudgetBackupCode.Value);
				dyn.StartY = StartY;
				dyn.EndY = EndY;
                dyn.ShowContractAccountMoney = base.ShowContractAccountMoney;

				dyn.Generate();

				//Ŀ����ð��汾��չ��
				string html_title_target_money1 = "";
				string html_title_target_money2 = "";
//				BLL.CostBudgetPageRule.GenerateCostBudgetMoneyTitleHtml(dyn.tbHtml, "BudgetMoney", "����Ԥ��", "��ǰ", ref html_title_target_money1, ref html_title_target_money2, this.IsShowTargetMoneyHis);

				ViewState["html_title_target_money1"] = html_title_target_money1;
				ViewState["html_title_target_money2"] = html_title_target_money2;

				//Ԥ��ƻ������չ��
				string html_title1 = "";
				string html_title2 = "";
				BLL.CostBudgetPageRule.GenerateCostBudgetPlanTitleHtml(dyn.tb, dyn.iStartY, dyn.iEndY, ref html_title1, ref html_title2);

				ViewState["html_title1"] = html_title1;
				ViewState["html_title2"] = html_title2;

				//�ݴ���ϸ��
//				m_dyn = dyn;

				BindDataGrid(dyn, IsScreenToTable);
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
		private void BindDataGrid(BLL.CostBudgetGroupDynamic dyn, bool IsScreenToTable) 
		{
			try 
			{
				DataTable tbDtl = dyn.tbHtml;

				//���
				if (dyn.tbGroupArea.Rows.Count > 0) 
				{
					this.lblBuildingArea.Text = BLL.StringRule.AddUnit(BLL.StringRule.BuildShowNumberString(dyn.tbGroupArea.Rows[0]["BuildingArea"]), "ƽ��");
					this.lblHouseCount.Text = BLL.StringRule.BuildShowNumberString(dyn.tbGroupArea.Rows[0]["HouseCount"], "#,##0.##");
					this.lblHouseArea.Text = BLL.StringRule.AddUnit(BLL.StringRule.BuildShowNumberString(dyn.tbGroupArea.Rows[0]["HouseArea"]), "ƽ��");
				}
				else 
				{
					this.lblBuildingArea.Text = "";
					this.lblHouseCount.Text = "";
					this.lblHouseArea.Text = "";
				}

				ViewState["HasTargetChange"] = dyn.HasTargetChange;
				if (dyn.HasTargetChange)
				{
					this.spanListTitleTargetMoneyDesc.InnerText = dyn.TargetChangeDesc;
					this.spanListTitleTargetMoney.Style["display"] = "";
				}

				if (IsScreenToTable)
				{
					//��Ļ���ݱ��浽��ʱ��
					tbDtl = ScreenToTable(dyn, false);
				}
				else 
				{
					//�۵�ȫ��������
//					BLL.CostBudgetPageRule.CollapseAll(tbDtl);
				}

				this.dgList.DataSource = tbDtl;
				this.dgList.DataBind();
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
				this.IsShowBudgetMoneyHis = true;
				LoadDataGrid(true);
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
				this.IsShowBudgetMoneyHis = false;
				LoadDataGrid(true);
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
				this.IsShowTargetMoneyHis = true;
				LoadDataGrid(true);
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
				this.IsShowTargetMoneyHis = false;
				LoadDataGrid(true);
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
		private DataTable ScreenToTable(BLL.CostBudgetGroupDynamic dyn, bool isBindGrid) 
		{
			DataTable tb = dyn.tbHtml;

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
					throw new Exception("��ϸ��" + CostBudgetDtlCode + "����ʱ���в����ڣ����ܱ���");
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
				LoadDataGrid(true);
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
				LoadDataGrid(true);
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "ˢ�½�λ����" + ex.Message));
			}
		}

	}
}
