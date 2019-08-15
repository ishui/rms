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
	/// CostBudgetGroupInfo 的摘要说明。
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
		/// 是否显示历史预算金额
		/// </summary>
		public bool IsShowBudgetMoneyHis
		{
			get {return BLL.ConvertRule.ToBool(ViewState["IsShowBudgetMoneyHis"]);}
			set {ViewState["IsShowBudgetMoneyHis"] = value;}
		}

		/// <summary>
		/// 是否显示历史目标费用
		/// </summary>
		public bool IsShowTargetMoneyHis
		{
			get {return BLL.ConvertRule.ToBool(ViewState["IsShowTargetMoneyHis"]);}
			set {ViewState["IsShowTargetMoneyHis"] = value;}
		}

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
		/// 动态费用实体
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}


		private void LoadData()
		{
			string GroupCode = this.txtGroupCode.Value;

			try
			{
				//取预算类别
				EntityData entity = DAL.EntityDAO.SystemManageDAO.GetSystemGroupByCode(GroupCode);
				if ( entity.HasRecord())
				{
					this.lblGroupName.Text = entity.GetString("GroupName");
				}
				else 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "预算类别不存在"));
					return;
				}
				entity.Dispose();

				EntityData entityBackup = null;

				if (this.txtCostBudgetBackupCode.Value != "")
				{
					//取备份表
					entityBackup = DAL.EntityDAO.CostBudgetDAO.GetV_CostBudgetBackupByCode(this.txtCostBudgetBackupCode.Value);
					if (!entityBackup.HasRecord()) 
					{
						Response.Write(Rms.Web.JavaScript.Alert(true, "项目费用备份表不存在"));
						return;
					}
				}

				this.lblDynamicDateDesc.InnerText = BLL.CostBudgetRule.GetDynamicDateDesc(this.txtCostBudgetBackupCode.Value, entityBackup);

				if (entityBackup != null) entityBackup.Dispose();

				BLL.PageFacade.LoadCostBudgetBackupSelect(this.sltBackup, this.txtCostBudgetBackupCode.Value, this.txtProjectCode.Value);

				LoadDataGrid(false);

				/*
				//权限
				ArrayList ar = user.GetResourceRight(this.txtCostBudgetSetCode.Value,"CostBudgetSet");
				if ( ! ar.Contains("041201"))  //动态预算查看
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示预算表出错：" + ex.Message));
			}
		}

		/// <summary>
		/// 显示动态费用明细
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

				//目标费用按版本号展开
				string html_title_target_money1 = "";
				string html_title_target_money2 = "";
//				BLL.CostBudgetPageRule.GenerateCostBudgetMoneyTitleHtml(dyn.tbHtml, "BudgetMoney", "已批预算", "当前", ref html_title_target_money1, ref html_title_target_money2, this.IsShowTargetMoneyHis);

				ViewState["html_title_target_money1"] = html_title_target_money1;
				ViewState["html_title_target_money2"] = html_title_target_money2;

				//预算计划按年度展开
				string html_title1 = "";
				string html_title2 = "";
				BLL.CostBudgetPageRule.GenerateCostBudgetPlanTitleHtml(dyn.tb, dyn.iStartY, dyn.iEndY, ref html_title1, ref html_title2);

				ViewState["html_title1"] = html_title1;
				ViewState["html_title2"] = html_title2;

				//暂存明细表
//				m_dyn = dyn;

				BindDataGrid(dyn, IsScreenToTable);
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示动态费用明细出错：" + ex.Message));
			}
		}

		/// <summary>
		/// 绑定动态费用明细
		/// </summary>
		private void BindDataGrid(BLL.CostBudgetGroupDynamic dyn, bool IsScreenToTable) 
		{
			try 
			{
				DataTable tbDtl = dyn.tbHtml;

				//面积
				if (dyn.tbGroupArea.Rows.Count > 0) 
				{
					this.lblBuildingArea.Text = BLL.StringRule.AddUnit(BLL.StringRule.BuildShowNumberString(dyn.tbGroupArea.Rows[0]["BuildingArea"]), "平米");
					this.lblHouseCount.Text = BLL.StringRule.BuildShowNumberString(dyn.tbGroupArea.Rows[0]["HouseCount"], "#,##0.##");
					this.lblHouseArea.Text = BLL.StringRule.AddUnit(BLL.StringRule.BuildShowNumberString(dyn.tbGroupArea.Rows[0]["HouseArea"]), "平米");
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
					//屏幕数据保存到临时表
					tbDtl = ScreenToTable(dyn, false);
				}
				else 
				{
					//折叠全部费用项
//					BLL.CostBudgetPageRule.CollapseAll(tbDtl);
				}

				this.dgList.DataSource = tbDtl;
				this.dgList.DataBind();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "绑定动态费用明细出错：" + ex.Message));
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
		/// 显示某个范围的年度计划
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示年度计划出错：" + ex.Message));
			}
		}

		/// <summary>
		/// 显示历史预算金额
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示历史预算金额出错：" + ex.Message));
			}
		}

		/// <summary>
		/// 隐藏历史预算金额
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "隐藏历史预算金额出错：" + ex.Message));
			}
		}

		/// <summary>
		/// 显示历史目标费用
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示历史目标费用出错：" + ex.Message));
			}
		}

		/// <summary>
		/// 隐藏历史目标费用
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "隐藏历史目标费用出错：" + ex.Message));
			}
		}

		/// <summary>
		/// 屏幕数据保存到临时表
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
					throw new Exception("明细项" + CostBudgetDtlCode + "在临时表中不存在，不能保存");
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

	}
}
