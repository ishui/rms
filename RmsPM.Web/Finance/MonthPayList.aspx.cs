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

namespace RmsPM.Web.Finance
{
	/// <summary>
	/// MonthPayList ��ժҪ˵����
	/// </summary>
	public partial class MonthPayList : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtPaymentCode;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtContractCode;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtIsContract;
		protected System.Web.UI.HtmlControls.HtmlAnchor hrefViewVoucher;
		protected System.Web.UI.HtmlControls.HtmlTableRow trContract;
		protected System.Web.UI.WebControls.Label lblPurpose;
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
		protected System.Web.UI.WebControls.Label lblGroupName;
		protected System.Web.UI.HtmlControls.HtmlTable tbOpiniont;
		protected System.Web.UI.HtmlControls.HtmlTable tbOpinionv;
		protected System.Web.UI.WebControls.Label lblOpinion;
		protected System.Web.UI.WebControls.Label lblVerID;
		protected System.Web.UI.WebControls.Label lblCostSortID;
		protected System.Web.UI.WebControls.Label lblCostName;
		protected System.Web.UI.WebControls.Label lblCheckDate;
		protected System.Web.UI.WebControls.Label lblCreatePersonName;
		protected System.Web.UI.WebControls.Label lblCreateDate;
		protected System.Web.UI.HtmlControls.HtmlGenericControl spanListTitleTargetMoney;
		protected System.Web.UI.HtmlControls.HtmlGenericControl spanListTitleTargetMoneyDesc;
		protected System.Web.UI.WebControls.Label lblTargetCheckDate;
		protected System.Web.UI.HtmlControls.HtmlGenericControl spanTargetVerID;
		protected System.Web.UI.HtmlControls.HtmlAnchor hrefTargetVerID;
	
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

        private void SetDateRange()
        {
            string StartYm = "";
            string EndYm = "";

            if (this.ucCostBudgetSelectMonth.ShowMonth)
            {
                StartYm = this.ucCostBudgetSelectMonth.MonthStart.Replace("-", "");
                EndYm = this.ucCostBudgetSelectMonth.MonthEnd.Replace("-", "");
            }

            this.txtStartYm.Value = StartYm;
            this.txtEndYm.Value = EndYm;
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
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];
				this.txtFromUrl.Value = Request.QueryString["FromUrl"];
                this.txtCostBudgetSetCode.Value = Request.QueryString["CostBudgetSetCode"];

                //��ï ȥ������/�Ѹ�,ֻ��ʾ���� 2007.2.2
                this.sltType.Style["display"] = "none";

                switch (this.up_sPMName.ToUpper())
                {
                    case "SHIMAOPM":
                        //��ï��ֻ��ʾ�����ŵ�Ԥ��� xyq 2007.7.25
                        BLL.PageFacade.LoadCostBudgetSetSelect(this.sltCostBudgetSet, "", this.txtProjectCode.Value, user.m_EntityDataAccessUnit);
                        break;

                    default:
                        BLL.PageFacade.LoadCostBudgetSetSelect(this.sltCostBudgetSet, "", this.txtProjectCode.Value);
                        break;

                }

                if (this.txtCostBudgetSetCode.Value != "")
                {
                    this.sltCostBudgetSet.Value = this.txtCostBudgetSetCode.Value;
                }

                //ȱʡ��ʾ���µ�
                this.ucCostBudgetSelectMonth.MonthStart = DateTime.Today.ToString("yyyy-MM");
                this.ucCostBudgetSelectMonth.MonthEnd = DateTime.Today.ToString("yyyy-MM");
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
				this.lblProjectName.Text = BLL.ProjectRule.GetProjectName(this.txtProjectCode.Value);

				LoadDataGrid(false);
			}
			catch (  Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾԤ������" + ex.Message));
			}
		}

		/// <summary>
		/// ������ϸ
		/// </summary>
		private void LoadDataGrid(bool IsScreenToTable)
		{
			try 
			{
                this.txtCostBudgetSetCode.Value = this.sltCostBudgetSet.Value;
                string Type = this.sltType.Value;

                if (this.txtCostBudgetSetCode.Value == "")
                {
                    this.threadList.Visible = false;
                    return;
                }

                this.threadList.Visible = true;

                SetDateRange();
                this.lblCostBudgetSetName.Text = "[" + BLL.CostBudgetRule.GetCostBudgetSetName(this.txtCostBudgetSetCode.Value) + "]";

                BLL.MonthPayList mp = new RmsPM.BLL.MonthPayList(this.txtProjectCode.Value, this.txtCostBudgetSetCode.Value);
                switch (Type.ToLower())
                {
                    case "payout":
                        mp.PayType = RmsPM.BLL.MonthPayList.e_PayType.payout;
                        lblTypeTitle.Text = "��";
                        lblTypeTitle2.Text = "��";
                        break;

                    default:
                        mp.PayType = RmsPM.BLL.MonthPayList.e_PayType.payment;
                        lblTypeTitle.Text = "��";
                        lblTypeTitle2.Text = "��";
                        break;

                }

                mp.StartYm = this.txtStartYm.Value;
                mp.EndYm = this.txtEndYm.Value;

                mp.Generate();

                BindDataGrid(mp, IsScreenToTable);
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ����һ�������" + ex.Message));
			}
		}

		/// <summary>
		/// �󶨶�̬������ϸ
		/// </summary>
        private void BindDataGrid(BLL.MonthPayList mp, bool IsScreenToTable) 
		{
			try 
			{
                DataTable tbDtl = mp.tb;

				//���չ��
				ViewState["html_title1"] = mp.DateTitleHtml1;
                ViewState["html_title2"] = mp.DateTitleHtml2;

				//�۵�ȫ��������
				BLL.CostBudgetPageRule.CollapseAll(tbDtl);

				BLL.CostBudgetPageRule.ExpandRoot(tbDtl);

				this.dgList.DataSource = tbDtl;
				this.dgList.DataBind();
            }
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "�󶨸���һ�������" + ex.Message));
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

		/// <summary>
		/// ��ʾĳ����Χ
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

        /// <summary>
        /// ����Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Excel(bool CloseWindow)
        {
            try
            {
                /*
                string ProjectCode = this.txtProjectCode.Value;
                string StartY = "" + Request.QueryString["StartY"];
                string EndY = "" + Request.QueryString["EndY"];

                BLL.ContractPaySchedule sch = new RmsPM.BLL.ContractPaySchedule(ProjectCode);
                sch.PBSType = this.txtPBSType.Value;
                sch.PBSCode = this.txtPBSCode.Value;
                sch.StartY = StartY;
                sch.EndY = EndY;
                sch.Generate();

                TExcel excel = new TExcel(Response, Request, Server, Session);
                try
                {
                    excel.StartRow = 10;
                    excel.ColumnHeadRow = 9;

                    //�½�������
                    excel.TemplateFileName = "����һ����.xls";
                    excel.TemplateSheetName = "";
                    excel.AddWorkbook();

                    //��ͷ��Ϣ
                    string ProjectName = BLL.ProjectRule.GetProjectName(sch.ProjectCode);
                    if (PBSType != "")
                    {
                        ProjectName += "[" + this.sltPBS.Items[this.sltPBS.SelectedIndex].Text + "]";
                    }
                    excel.SetCellValue(1, 1, ProjectName);

                    //��������
                    excel.SetCellValue(1, 3, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                    //���������������ϸ
                    int colYm = excel.Sheet.UsedRange.Columns.Count - 1;

                    if ((sch.StartY != "") && (sch.EndY != ""))  //��������ϸ
                    {
                        int MonthCount = (sch.iEndY - sch.iStartY + 1) * 12;

                        //�����
                        TExcel.InsertBlankColumn(excel.Sheet, colYm, 1, 2, MonthCount);

                        int col = colYm - 1;
                        for (int y = sch.iStartY; y <= sch.iEndY; y++)
                        {
                            for (int m = 1; m <= 12; m++)
                            {
                                col++;
                                string ym = BLL.ConvertRule.FormatYYYYMM(y, m);

                                //дexcel����ϸ�ֶζ���
                                excel.SetCellValue(excel.ColumnHeadRow, col, "PayoutMoneyYm_" + ym);
                            }
                        }
                    }
                    else  //��������ϸ
                    {
                        //����������У�������ϸ��
                        TExcel.HideColumn(excel.Sheet, colYm);
                        TExcel.HideColumn(excel.Sheet, colYm + 1);
                    }

                    excel.DataSource = sch.tb;
                    excel.DataToSheet();

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
                */
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
                Response.Write(Rms.Web.JavaScript.Alert(true, "����Excel����" + ex.Message));
            }
        }

        /// <summary>
        /// �л�Ԥ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCostBudgetSetChange_ServerClick(object sender, EventArgs e)
        {
            try
            {
                LoadDataGrid(false);
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
                Response.Write(Rms.Web.JavaScript.Alert(true, "����λ������ʾ����" + ex.Message));
            }
        }
    }
}
