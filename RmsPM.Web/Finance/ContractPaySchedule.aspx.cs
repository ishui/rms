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
	/// ContractPaySchedule 的摘要说明。
	/// </summary>
	public partial class ContractPaySchedule : PageBase
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
		/// 金额单位
		/// </summary>
		public BLL.CostBudgetPageRule.m_MoneyUnit MoneyUnit
		{
			get {return BLL.CostBudgetPageRule.GetMoneyUnit(this.sltMoneyUnit);}
		}

		/// <summary>
		/// 金额显示
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
		/// 金额显示
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
		/// 原始金额的提示（元）
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
				return "RptContractPayList";
			}
		}

        /*
		/// <summary>
		/// 一览表实体
		/// </summary>
		private BLL.ContractPaySchedule m_sch
		{
			get
			{
				if (Session[m_Guid] == null)
					return null;

				return (BLL.ContractPaySchedule)Session[m_Guid];
			}
			set
			{
				Session[m_Guid] = value;
			}
		}
        */

		private void SetDateRange()
		{
			string StartYm = "";
			string EndYm = "";

			if (this.ucCostBudgetSelectMonth.ShowMonth) 
			{
				StartYm = this.ucCostBudgetSelectMonth.MonthStart.Replace("-", "");
                EndYm = this.ucCostBudgetSelectMonth.MonthEnd.Replace("-", "");
			}

            ViewState["StartYm"] = StartYm;
            ViewState["EndYm"] = EndYm;

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
                this.txtPBSType.Value = Request.QueryString["PBSType"];
                this.txtPBSCode.Value = Request.QueryString["PBSCode"];

                BLL.PageFacade.LoadCostByPBSSelect(this.sltPBS, "", this.txtProjectCode.Value);

                if (this.txtPBSType.Value != "")
                {
                    if (this.txtPBSType.Value.ToUpper() == "P")
                    {
                        this.sltPBS.Value = this.txtPBSType.Value;
                    }
                    else
                    {
                        this.sltPBS.Value = this.txtPBSCode.Value;
                    }
                }
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示预算表出错：" + ex.Message));
			}
		}

		/// <summary>
		/// 生成明细
		/// </summary>
		private void LoadDataGrid(bool IsScreenToTable)
		{
			try 
			{
                //按单位工程
                this.txtPBSType.Value = "";
                this.txtPBSCode.Value = "";
                this.lblPBSName.Text = "";
                if (this.sltPBS.Value.Trim() != "")
                {
                    this.lblPBSName.Text = "[" + this.sltPBS.Items[this.sltPBS.SelectedIndex].Text + "]";

                    if (this.sltPBS.Value.ToUpper() == "P") //项目
                    {
                        this.txtPBSType.Value = "P";
                    }
                    else //楼栋
                    {
                        this.txtPBSType.Value = "B";
                        this.txtPBSCode.Value = this.sltPBS.Value;
                    }
                }

				BLL.ContractPaySchedule sch = new RmsPM.BLL.ContractPaySchedule(this.txtProjectCode.Value);
                sch.PBSType = this.txtPBSType.Value;
                sch.PBSCode = this.txtPBSCode.Value;
				sch.StartYm = BLL.ConvertRule.ToString(ViewState["StartYm"]);
				sch.EndYm = BLL.ConvertRule.ToString(ViewState["EndYm"]);

				sch.Generate();

				//暂存一览表
//				m_sch = sch;

				BindDataGrid(sch, IsScreenToTable);
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示付款一览表出错：" + ex.Message));
			}
		}

		/// <summary>
		/// 绑定动态费用明细
		/// </summary>
        private void BindDataGrid(BLL.ContractPaySchedule sch, bool IsScreenToTable) 
		{
			try 
			{
				DataTable tbDtl = sch.tb;

				//年度展开
				ViewState["html_title1"] = sch.DateTitleHtml1;
				ViewState["html_title2"] = sch.DateTitleHtml2;

                //if (IsScreenToTable)
                //{
                //    //屏幕数据保存到临时表
                //    tbDtl = ScreenToTable(false);
                //}
                //else 
                //{
					//折叠全部费用项
					BLL.CostBudgetPageRule.CollapseAll(tbDtl);

					BLL.CostBudgetPageRule.ExpandDeep(tbDtl, 2);
				//}

				this.dgList.DataSource = tbDtl;
				this.dgList.DataBind();
            }
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "绑定付款一览表出错：" + ex.Message));
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
		/// 显示某个范围
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnGotoMonth_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				//				DateTime t1 = DateTime.Now;

				SetDateRange();

                LoadDataGrid(true);
                
                //if (m_sch == null)
                //{
                //    LoadDataGrid(true);
                //}
                //else 
                //{
                //    m_sch.StartY = BLL.ConvertRule.ToString(ViewState["StartY"]);
                //    m_sch.EndY = BLL.ConvertRule.ToString(ViewState["EndY"]);
                //    m_sch.RefreshDateRange();

                //    BindDataGrid(true);
                //}

				/*
				DateTime t2 = DateTime.Now;
				TimeSpan t = t2.Subtract(t1);
				Response.Write(Rms.Web.JavaScript.Alert(true, t.Duration().ToString()));
				*/
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示年度计划出错：" + ex.Message));
			}
		}

        /*
		/// <summary>
		/// 屏幕数据保存到临时表
		/// </summary>
		/// <returns></returns>
		private DataTable ScreenToTable(bool isBindGrid) 
		{
			DataTable tb = m_sch.tb;

			foreach (RepeaterItem item in this.dgList.Items)
			{
				HtmlInputHidden txtDtlCode = (HtmlInputHidden)item.FindControl("txtDtlCode");
				HtmlInputHidden txtIsExpand = (HtmlInputHidden)item.FindControl("txtIsExpand");

				string DtlCode = txtDtlCode.Value;

				DataRow dr = null;
				DataRow[] drs = tb.Select("DtlCode='" + DtlCode + "'");
				if (drs.Length > 0) 
				{
					dr = drs[0];
				}
				else
				{
					throw new Exception("明细序号" + DtlCode + "在临时表中不存在，不能保存");
				}

				dr["IsExpand"] = BLL.ConvertRule.ToInt(txtIsExpand.Value);
			}

			if (isBindGrid) 
			{
				this.dgList.DataSource = tb;
				this.dgList.DataBind();
			}

			return tb;
		}
        */

		/// <summary>
		/// 刷新全部
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "刷新出错：" + ex.Message));
			}
		}

		/// <summary>
		/// 改变金额单位
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "刷新金额单位出错：" + ex.Message));
			}
		}

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Excel(bool CloseWindow)
        {
            try
            {
                string ProjectCode = this.txtProjectCode.Value;
                string StartYm = "" + Request.QueryString["StartYm"];
                string EndYm = "" + Request.QueryString["EndYm"];
                string PBSType = "" + Request.QueryString["PBSType"];
                string PBSCode = "" + Request.QueryString["PBSCode"];

                BLL.ContractPaySchedule sch = new RmsPM.BLL.ContractPaySchedule(ProjectCode);
                sch.PBSType = this.txtPBSType.Value;
                sch.PBSCode = this.txtPBSCode.Value;
                sch.StartYm = StartYm;
                sch.EndYm = EndYm;
                sch.Generate();

                TExcel excel = new TExcel(Response, Request, Server, Session);
                try
                {
                    excel.StartRow = 10;
                    excel.ColumnHeadRow = 9;

                    //去掉1级合计
                    /*
                    DataView dv;
                    if (dyn.NeedApport) //有分摊时，多了一级总计
                    {
                        dv = new DataView(dyn.tb, "Deep=3", "SortID", DataViewRowState.CurrentRows);
                    }
                    else
                    {
                        dv = new DataView(dyn.tb, "Deep=2", "SortID", DataViewRowState.CurrentRows);
                    }
                    */

                    //新建工作簿
                    excel.TemplateFileName = "付款一览表.xls";
                    excel.TemplateSheetName = "";
                    excel.AddWorkbook();

                    //表头信息
                    string ProjectName = BLL.ProjectRule.GetProjectName(sch.ProjectCode);
                    if (PBSType != "")
                    {
                        ProjectName += "[" + this.sltPBS.Items[this.sltPBS.SelectedIndex].Text + "]";
                    }
                    excel.SetCellValue(1, 1, ProjectName);

                    //报表日期
                    excel.SetCellValue(1, 3, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                    //最后两列是年月明细
                    int colYm = excel.Sheet.UsedRange.Columns.Count - 1;

                    if ((StartYm != "") && (EndYm != ""))  //有年月明细
                    {
                        int MonthCount = BLL.StringRule.GetMonthsBetween(StartYm, EndYm);

                        //插空列
                        TExcel.InsertBlankColumn(excel.Sheet, colYm, 1, 2, MonthCount);

                        int col = colYm - 1;
                        for (int i = 0; i < MonthCount; i++)
						{
                            col++;
                            string ym = BLL.StringRule.YmAddMonths(StartYm, i);

                            //写excel的明细字段定义
                            excel.SetCellValue(excel.ColumnHeadRow, col, "PayoutMoneyYm_" + ym);
                        }
                    }
                    else  //无年月明细
                    {
                        //隐藏最后两列（年月明细）
                        TExcel.HideColumn(excel.Sheet, colYm);
                        TExcel.HideColumn(excel.Sheet, colYm + 1);
                    }

                    excel.DataSource = sch.tb;
                    excel.DataToSheet();

                    //保存
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
                ApplicationLog.WriteLog(this.ToString(), ex, "");
                Response.Write(Rms.Web.JavaScript.Alert(true, "导出Excel出错：" + ex.Message));
            }
        }

        /// <summary>
        /// 切换单位工程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPBSChange_ServerClick(object sender, EventArgs e)
        {
            try
            {
                LoadDataGrid(false);
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
                Response.Write(Rms.Web.JavaScript.Alert(true, "按单位工程显示出错：" + ex.Message));
            }
        }
    }
}
