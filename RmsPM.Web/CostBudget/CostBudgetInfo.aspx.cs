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
	/// CostBudgetInfo 的摘要说明。
	/// </summary>
	public partial class CostBudgetInfo : CostBudgetPageBase
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
		/// 动态费用实体
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
		/// 是否有修改权限
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}


		private void LoadData()
		{
			try
			{
				EntityData entityBackup = null;

				if (this.txtCostBudgetBackupCode.Value != "")
				{
					//取备份表
					entityBackup = DAL.EntityDAO.CostBudgetDAO.GetV_CostBudgetBackupByCode(this.txtCostBudgetBackupCode.Value);
					if (!entityBackup.HasRecord()) 
					{
						Response.Write(Rms.Web.JavaScript.Alert(true, "项目费用备份表不存在"));
						Response.Write(Rms.Web.JavaScript.WinClose(true));
						return;
					}

					//取备份的预算设置表
					EntityData entitySet = DAL.EntityDAO.CostBudgetDAO.GetCostBudgetBackupSetByBackupCode(this.txtCostBudgetBackupCode.Value, this.txtCostBudgetSetCode.Value, true);
					if (!entitySet.HasRecord()) 
					{
						Response.Write(Rms.Web.JavaScript.Alert(true, "备份中不含该预算表"));
						Response.Write(Rms.Web.JavaScript.WinClose(true));
						return;
					}

					this.txtCostBudgetBackupSetCode.Value = entitySet.GetString("CostBudgetBackupSetCode");
					BindData(entitySet);

					entitySet.Dispose();
				}
				else
				{
					//取预算设置表
					EntityData entitySet = DAL.EntityDAO.CostBudgetDAO.GetV_CostBudgetSetByCode(txtCostBudgetSetCode.Value);
					if ( entitySet.HasRecord())
					{
						BindData(entitySet);
					}
					else 
					{
						Response.Write(Rms.Web.JavaScript.Alert(true, "预算设置表不存在"));
						Response.Write(Rms.Web.JavaScript.WinClose(true));
						return;
					}
					entitySet.Dispose();
				}

				this.lblDynamicDateDesc.InnerText = BLL.CostBudgetRule.GetDynamicDateDesc(this.txtCostBudgetBackupCode.Value, entityBackup);

				if (entityBackup != null) entityBackup.Dispose();

				BLL.PageFacade.LoadCostBudgetBackupSelect(this.sltBackup, this.txtCostBudgetBackupCode.Value, this.txtProjectCode.Value);

				//权限
				this.RightModify = true;

                if (!ShowColBudget)
                {
                    this.btnExcel.Visible = false;
                }

				LoadCostBudgetData();
				LoadDataGrid(false);

				//权限
				/*
				ArrayList ar = user.GetResourceRight(this.txtCostBudgetSetCode.Value,"CostBudgetSet");
				if ( ! ar.Contains("041301"))  //动态预算查看
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示预算表出错：" + ex.Message));
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
				this.lblBuildingArea.Text = BLL.StringRule.AddUnit(BLL.StringRule.BuildShowNumberString(entitySet.GetDecimal("BuildingArea")), "平米");
				this.lblHouseCount.Text = BLL.StringRule.BuildShowNumberString(entitySet.GetDecimal("HouseCount"), "#,##0.##");
				this.lblHouseArea.Text = BLL.StringRule.AddUnit(BLL.StringRule.BuildShowNumberString(entitySet.GetDecimal("HouseArea")), "平米");
			}
			catch (  Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示预算表出错：" + ex.Message));
			}
		}

		/// <summary>
		/// 显示动态调整表头
		/// </summary>
		private void LoadCostBudgetData()
		{
			try 
			{
				if (this.txtCostBudgetBackupCode.Value != "") 
				{
					this.txtCostBudgetCode.Value = "";

					//取备份的预算设置表
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
					//取动态调整表
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
							case "1"://当前
								this.btnModify.Style["display"] = "none";
								this.btnDelete.Style["display"] = "none";
								this.btnCheck.Style["display"] = "none";

								this.btnModifyEx.Style["display"] = "";
								this.btnViewHistory.Style["display"] = "";

								break;

							case "2"://历史
								this.btnModify.Style["display"] = "none";
								this.btnDelete.Style["display"] = "none";
								this.btnCheck.Style["display"] = "none";

								this.btnModifyEx.Style["display"] = "none";
								this.btnViewHistory.Style["display"] = "none";

								break;

							default: //未审、调整
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示动态调整表头出错：" + ex.Message));
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
                dyn.MoneyUnit = this.MoneyUnit;
                dyn.ShowContractAccountMoney = base.ShowContractAccountMoney;

				dyn.Generate();

				return dyn;
			}
			catch(Exception ex) 
			{
				throw ex;
			}
		}

		/// <summary>
		/// 生成动态费用明细
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
				BLL.CostBudgetPageRule.GenerateCostBudgetMoneyTitleHtml(dyn.tb, "BudgetMoney", "已批预算", "当前", ref html_title_target_money1, ref html_title_target_money2, false);

				ViewState["html_title_target_money1"] = html_title_target_money1;
				ViewState["html_title_target_money2"] = html_title_target_money2;
				*/

				//预算计划按年度展开
				string html_title1 = "";
				string html_title2 = "";
				BLL.CostBudgetPageRule.GenerateCostBudgetPlanTitleHtml(dyn.tb, dyn.iStartY, dyn.iEndY, ref html_title1, ref html_title2);

				ViewState["html_title1"] = html_title1;
				ViewState["html_title2"] = html_title2;

				//暂存明细表
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示动态费用明细出错：" + ex.Message));
			}
		}

		/// <summary>
		/// 绑定动态费用明细
		/// </summary>
		private void BindDataGrid(BLL.CostBudgetDynamic dyn, bool IsScreenToTable) 
		{
			try 
			{
				DataTable tbDtl = dyn.tbHtml;

				//已批预算的审核日期
				this.lblTargetCheckDate.Text = dyn.GetTargetCheckDate();
				string VerID = dyn.GetTargetVerID();
                string VerName = dyn.GetTargetVerName();
				this.txtHasTargetHis.Value = (BLL.ConvertRule.ToDecimal(VerID) > 0)?"1":"";

				//版本号链接
				this.spanTargetVerID.InnerText = "";
				this.hrefTargetVerID.InnerText = "";
				if (this.txtHasTargetHis.Value == "1")  //有历史版本
				{
                    this.hrefTargetVerID.InnerText = VerName;
				}
				else 
				{
                    this.spanTargetVerID.InnerText = VerName;
				}

				//历史目标费用
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
					//屏幕数据保存到临时表
					tbDtl = ScreenToTable(false);
				}
				else 
				{
					//折叠全部费用项
					BLL.CostBudgetPageRule.CollapseAll(tbDtl);

					if (dyn.NeedApport) //有分摊时，多了一级总计
					{
						BLL.CostBudgetPageRule.ExpandDeep(tbDtl, 3);
					}
					else 
					{
						BLL.CostBudgetPageRule.ExpandDeep(tbDtl, 2);
					}
				}

                //复制显示要页面的table
                DataTable tbPage = this.GeneratePageHtml(tbDtl);

                this.dgList.DataSource = tbPage;
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "绑定动态费用明细出错：" + ex.Message));
			}
		}

        /// <summary>
        /// 复制显示要页面的table
        /// </summary>
        /// <param name="tbDtl"></param>
        /// <returns></returns>
        private DataTable GeneratePageHtml(DataTable tbDtl)
        {
            try
            {
                object obj1;
                object obj2;
                string text;
                string title;

                DataTable tbPage = tbDtl.Copy();
                tbPage.Columns.Add("PageHtml");
                foreach (DataRow drPage in tbPage.Rows)
                {
                    string html = "";
                    string ClassTd = BLL.ConvertRule.ToString(drPage["ClassTd"]);
                    string ClassTdR = ClassTd + "-r";
                    string CostBudgetDtlCode = BLL.ConvertRule.ToString(drPage["CostBudgetDtlCode"]);

                    //费用项名称
                    html += "<td nowrap title='" + BLL.ConvertRule.ToString(drPage["SortID"]) + "'>"
                        + "<span id='TreeNodeSpan_" + CostBudgetDtlCode + "'></span>"
                        + "<span class='spanTn'><img id='TreeNodeImg_" + CostBudgetDtlCode + "' style='display:none' class='imgTn' onclick='CBTree_ImgExpandClick(this);'></span>"
                        + BLL.ConvertRule.ToString(drPage["CostName"])
                        + "</td>";

                    //合同编号
                    html += BLL.CostBudgetPageRule.GetTd(drPage["ContractIDHtml"], ClassTd);

                    //合同名称
                    html += BLL.CostBudgetPageRule.GetTd(drPage["ContractNameHtml"], ClassTd);

                    //供应商
                    html += BLL.CostBudgetPageRule.GetTd(drPage["SupplierNameHtml"], ClassTd);

                    //描述
                    html += BLL.CostBudgetPageRule.GetTd(drPage["DescriptionHtml"]
                        , BLL.CostBudgetDynamic.GetChangeDescShowHtml(ViewState["HasTargetChange"], drPage["Description"], drPage["BudgetChangeDescription"], drPage["BudgetChangeDescription"])
                        , drPage["ClassTd"]);

                    //年月展开
                    html += BLL.ConvertRule.ToString(drPage["PlanDataHtml"]);

                    //已批预算
                    if (ShowColBudget)
                    {
                        text = GetMoneyShowString(drPage["BudgetMoney"])
                        + BLL.CostBudgetDynamic.GetChangeMoneyShowHtml(ViewState["HasTargetChange"], drPage["BudgetMoney"], drPage["BudgetChangeMoney"], GetMoneyShowString(drPage["BudgetChangeMoney"]), drPage["RecordType"]);
                        title = GetMoneyShowHint(drPage["BudgetMoney"])
                            + BLL.CostBudgetDynamic.GetChangeMoneyShowHtml(ViewState["HasTargetChange"], drPage["BudgetMoney"], drPage["BudgetChangeMoney"], GetMoneyShowHint(drPage["BudgetChangeMoney"]), drPage["RecordType"]);
                        html += BLL.CostBudgetPageRule.GetTd2(text, ClassTdR, title);
                    }

                    //历史预算
                    html += BLL.ConvertRule.ToString(drPage["BudgetMoneyHisHtml"]);

                    //已定合同
                    html += BLL.CostBudgetPageRule.GetTd2(GetMoneyShowString(drPage["ContractMoney"]), ClassTdR, GetMoneyShowHint(drPage["ContractMoney"]));

                    //已定变更
                    html += BLL.CostBudgetPageRule.GetTd2(GetMoneyShowString(drPage["ContractChangeMoney"]), ClassTdR, GetMoneyShowHint(drPage["ContractChangeMoney"]));

                    //待定合同/变更
                    html += BLL.CostBudgetPageRule.GetTd2(GetMoneyShowString(drPage["ContractApplyMoney"]), ClassTdR, GetMoneyShowHint(drPage["ContractApplyMoney"]));

                    //估计最终价
                    html += BLL.CostBudgetPageRule.GetTd2(GetMoneyShowString(drPage["ContractTotalMoney"]), ClassTdR, GetMoneyShowHint(drPage["ContractTotalMoney"]));

                    //已结算
                    if (base.ShowContractAccountMoney)
                    {
                        html += BLL.CostBudgetPageRule.GetTd2(GetMoneyShowString(drPage["ContractAccountMoney"]), ClassTdR, GetMoneyShowHint(drPage["ContractAccountMoney"]));
                    }

                    //差额
                    if (ShowColBudget)
                    {
                        if (base.IsRemindContractBudgetBalance)
                        {
                            string style = BLL.CostBudgetPageRule.GetContractBudgetBalanceRemindStyle(drPage["ContractBudgetBalance"]);
                            if (style != "")
                                style = string.Format("style=\"{0}\"", style);
                            html += BLL.CostBudgetPageRule.GetTd2(GetMoneyShowString(drPage["ContractBudgetBalance"]), "", ClassTdR, GetMoneyShowHint(drPage["ContractBudgetBalance"]), style);
                        }
                        else
                        {
                            html += BLL.CostBudgetPageRule.GetTd2(GetMoneyShowString(drPage["ContractBudgetBalance"]), ClassTdR, GetMoneyShowHint(drPage["ContractBudgetBalance"]));
                        }
                    }

                    if (ShowColBeforeChange)
                    {
                        //预算单方造价
                        html += BLL.CostBudgetPageRule.GetTd2(GetMoneyShowString(drPage["BudgetPrice"], "price"), ClassTdR, GetMoneyShowHint(drPage["BudgetPrice"]));

                        //变更前单方造价
                        html += BLL.CostBudgetPageRule.GetTd2(GetMoneyShowString(drPage["ContractOriginalPrice"], "price"), ClassTdR, GetMoneyShowHint(drPage["ContractOriginalPrice"]));
                    }

                    //单方造价
                    html += BLL.CostBudgetPageRule.GetTd2(GetMoneyShowString(drPage["BuildingPrice"], "price"), ClassTdR, GetMoneyShowHint(drPage["BuildingPrice"]));

                    //单元造价
                    html += BLL.CostBudgetPageRule.GetTd2(GetMoneyShowString(drPage["HousePrice"], "price"), ClassTdR, GetMoneyShowHint(drPage["HousePrice"]));

                    //累计已批
                    html += BLL.CostBudgetPageRule.GetTd2(BLL.CostBudgetPageRule.GetContractPayHref(GetMoneyShowString(drPage["ContractPay"]), drPage["CostCode"], drPage["ContractCode"], ""), ClassTdR, GetMoneyShowHint(drPage["ContractPay"]));

                    //已批%
                    html += BLL.CostBudgetPageRule.GetTd(BLL.StringRule.BuildShowPercentString(drPage["ContractPayPercent"], "####"), ClassTdR);

                    //未批款
                    html += BLL.CostBudgetPageRule.GetTd2(GetMoneyShowString(drPage["ContractPayBalance"]), ClassTdR, GetMoneyShowHint(drPage["ContractPayBalance"]));

                    //累计已付
                    html += BLL.CostBudgetPageRule.GetTd2(BLL.CostBudgetPageRule.GetContractPayRealHref(GetMoneyShowString(drPage["ContractPayReal"]), drPage["CostCode"], drPage["ContractCode"], "", "", ""), ClassTdR, GetMoneyShowHint(drPage["ContractPayReal"]));

                    drPage["PageHtml"] = html;
                }

                return tbPage;
            }
            catch (Exception ex)
            {
                throw ex;
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
		/// 删除
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "删除动态预算出错：" + ex.Message));
			}
		}

		/// <summary>
		/// 审核
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "动态预算审核出错：" + ex.Message));
			}
		}

		/*
		private void dgList_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
		{
			if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
			{
				int iStartY = BLL.ConvertRule.ToInt(ViewState["StartY"]);
				int iEndY = BLL.ConvertRule.ToInt(ViewState["EndY"]);

				//缺省展开当年
				int curr_year = DateTime.Today.Year;

				DataTable tbDtl = m_dyn.tb;

				string CostBudgetDtlCode = ((HtmlInputHidden)e.Item.FindControl("txtCostBudgetDtlCode")).Value;
				string CostCode = ((HtmlInputHidden)e.Item.FindControl("txtCostCode")).Value;
				
				if (!CostBudgetDtlCode.StartsWith("R_"))  //合计行无合同明细
				{
					//预算明细记录
					DataRow[] drsDtl = tbDtl.Select("CostBudgetDtlCode = '" + CostBudgetDtlCode + "'");
					DataRow drDtl = null;
					if (drsDtl.Length > 0) 
					{
						drDtl = drsDtl[0];
					}

					//合同明细
					PlaceHolder phContract = (PlaceHolder)e.Item.FindControl("phContract");
					DataTable tbContract = m_dyn.tbContract;
					DataRow[] drsContract = tbContract.Select("CostCode = '" + CostCode + "'", "ContractID, ContractName", DataViewRowState.CurrentRows);
					this.m_dyn.AddRowContract(e.Item.ItemIndex, drDtl, drsContract, phContract, iStartY, iEndY, curr_year.ToString(), this.MoneyUnit); 

					//无合同金额占一行
					DataTable tbNoContract = m_dyn.tbNoContract;
					DataRow[] drsNoContract = tbNoContract.Select("CostCode = '" + CostCode + "'");
					this.m_dyn.AddRowBalance(e.Item.ItemIndex, drDtl, drsContract, drsNoContract, phContract, iStartY, iEndY, curr_year.ToString(), this.RightModify, this.MoneyUnit);
				}
			}
		}
		*/

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
				Response.Write(Rms.Web.JavaScript.Alert(true, "隐藏历史目标费用出错：" + ex.Message));
			}
		}

		/// <summary>
		/// 屏幕数据保存到临时表
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
					throw new Exception("明细序号" + CostBudgetDtlCode + "在临时表中不存在，不能保存");
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
				LoadCostBudgetData();
				LoadDataGrid(true);
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "刷新出错：" + ex.Message));
			}
		}

		/// <summary>
		/// 刷新目标费用
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "刷新出错：" + ex.Message));
			}
		}

		/// <summary>
		/// 刷新预留金额
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "刷新出错：" + ex.Message));
			}
		}

		/// <summary>
		/// 刷新合同计划
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "刷新金额单位出错：" + ex.Message));
			}
		}

		/// <summary>
		/// 刷新合同预算
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "刷新出错：" + ex.Message));
			}
		}

		/// <summary>
		/// 加合同明细
		/// </summary>
		/// <param name="tbDtl"></param>
		/// <param name="drDtl"></param>
		/// <param name="drsContract"></param>
		/// <param name="drsNoContract"></param>
		private static void AddContract(DataTable tbDtl, DataRow drDtl, DataRow[] drsContract, ref int ReportDtlSno)
		{
			try 
			{
				//合同明细直接显示在费用项上时，合同明细不另外显示
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
		/// 加预留金额
		/// </summary>
		/// <param name="tbDtl"></param>
		/// <param name="drDtl"></param>
		/// <param name="drsContract"></param>
		/// <param name="drsNoContract"></param>
		private static void AddNoContract(DataTable tbDtl, DataRow drDtl, DataRow[] drsContract, DataRow[] drsNoContract, ref int ReportDtlSno)
		{
			try 
			{
				//非叶结点的费用项，又没有合同时，不显示预留金额
				if ((!BLL.ConvertRule.ToBool(drDtl["IsLeafCBS"])) && (drsContract.Length <= 0)) return;

				//预留金额直接显示在费用项上时，预留金额不另外显示
				if (BLL.ConvertRule.ToString(drDtl["ContractCode"]) == "BALANCE") return;

				foreach(DataRow drNoContract in drsNoContract) 
				{
					//预留金额的费用为0时，不显示
					if ((BLL.ConvertRule.ToDecimal(drNoContract["ContractMoney"]) == 0)
						&& (BLL.ConvertRule.ToDecimal(drNoContract["ContractChangeMoney"]) == 0)
						&& (BLL.ConvertRule.ToDecimal(drNoContract["ContractApplyMoney"]) == 0)
						)
						continue;

					DataRow drNew = tbDtl.NewRow();
					BLL.ConvertRule.DataRowCopy(drNoContract, drNew, drNoContract.Table, tbDtl);

					drNew["CostBudgetDtlCode"] = "NoContract_" + BLL.ConvertRule.ToString(drNoContract["ContractCode"]);
					drNew["ContractName"] = "预留金额";

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
		/// 导出Excel
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

					//取1级费用项
					DataView dv;
					if (dyn.NeedApport) //有分摊时，多了一级总计
					{
						dv = new DataView(dyn.tb, "Deep=3", "SortID", DataViewRowState.CurrentRows);
					}
					else 
					{
						dv = new DataView(dyn.tb, "Deep=2", "SortID", DataViewRowState.CurrentRows);
					}

					excel.DataSource = dv;

					//新建工作簿
					excel.TemplateFileName = "项目费用表.xls";
//					excel.TemplateSheetName = "汇总";
					excel.TemplateSheetName = "";
					excel.AddWorkbook();

					//新建若干“分项”页
					for(int k=2;k<=dv.Count;k++) 
					{
						((Excel.Worksheet)excel.Book.Worksheets[2]).Copy(TExcel.m_Opt, excel.Book.Worksheets[2]);
					}

					//转到汇总页
					excel.Sheet = (Excel.Worksheet)excel.Book.Worksheets[1];

					//表头信息
					excel.SetCellValue(1, 1, BLL.ProjectRule.GetProjectName(dyn.ProjectCode));

					//报表日期
					if (this.txtCostBudgetBackupCode.Value != "") //备份
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

					//按第2级费用项分组
					excel.StartRow = 11;
					excel.ColumnHeadRow = 9;

					/*
					//2级费用项临时表头
					DataTable tbLevel2 = dyn.entitySet.CurrentTable.Copy();
					BLL.ConvertRule.DataTableAddColumn(dyn.tb, tbLevel2);
					*/

					//3级费用项临时表（包括合同明细）
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

						//取第2级费用项的子项
						DataView dv2 = new DataView(dyn.tb, "ParentCode='" + ParentCostCode + "'", "SortID", DataViewRowState.CurrentRows);

						//复制到临时表
						tbDtl.Rows.Clear();
						int ReportDtlSno = 0;

						foreach(DataRowView drv2 in dv2) 
						{
							DataRow dr2 = drv2.Row;
							string CostCode = BLL.ConvertRule.ToString(dr2["CostCode"]);

							//3级费用项
							drNew = tbDtl.NewRow();
							BLL.ConvertRule.DataRowCopy(dr2, drNew, dyn.tb, tbDtl);

							ReportDtlSno++;
							drNew["ReportDtlSno"] = ReportDtlSno;

							tbDtl.Rows.Add(drNew);

							//3级费用项的合同明细
							DataRow[] drsContract = dyn.tbContract.Select("CostCode = '" + CostCode + "'", "ContractID, ContractName", DataViewRowState.CurrentRows);
							AddContract(tbDtl, drNew, drsContract, ref ReportDtlSno);

							//3级费用项的预留金额
							DataRow[] drsNoContract = dyn.tbNoContract.Select("CostCode = '" + CostCode + "'");
							AddNoContract(tbDtl, drNew, drsContract, drsNoContract, ref ReportDtlSno);

						}

						//合计行
						drNew = tbDtl.NewRow();
						BLL.ConvertRule.DataRowCopy(drv.Row, drNew, dyn.tb, tbDtl);

						ReportDtlSno++;
						drNew["ReportDtlSno"] = ReportDtlSno;
						drNew["CostName"] = "合计";

						tbDtl.Rows.Add(drNew);

						/*
						//第2个2级费用项开始，每次都要新建工作页
						if (i > 1) 
						{
							excel.TemplateSheetName = "分项";
							excel.AddWorksheet(i, true);
						}
						*/

						//转到分项页
						excel.Sheet = (Excel.Worksheet)excel.Book.Worksheets[i + 1];

						//一个2级费用项打印一页
						excel.Sheet.Name = ParentCostName.Replace("/", "");
						excel.DataToSheetSingle(drv.Row);

						//2级费用项表头信息
//						excel.SetCellValue(1, 1, drv["CostName"]);

						DataView dvDtl = new DataView(tbDtl, "", "ReportDtlSno", DataViewRowState.CurrentRows);
						excel.DataSource = dvDtl;
						excel.DataToSheet();

						/*
						//合同明细只有一条时，隐藏合同明细
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

						//隐藏列IsHide
						((Excel.Range)excel.Sheet.Cells[1, colIsHide]).EntireColumn.Hidden = true;
						*/
					}

					//缺省选中第1页
					((Excel.Worksheet)excel.Book.Worksheets[1]).Select(TExcel.m_Opt);

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
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "导出Excel出错：" + ex.Message));
			}
		}

	}
}
