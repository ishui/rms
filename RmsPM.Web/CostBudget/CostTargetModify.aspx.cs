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
using Rms.Web;
using RmsPM.DAL.EntityDAO;
using RmsPM.DAL.QueryStrategy;
using RmsPM.BLL;
using Infragistics.WebUI.WebDataInput;

namespace RmsPM.Web.CostBudget
{
	/// <summary>
	/// CostTargetModify 的摘要说明。
	/// </summary>
	public partial class CostTargetModify : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlGenericControl spantest;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.ucCostBudgetSelectMonth.GotoMonthClick += new System.EventHandler(this.btnGotoMonth_ServerClick);

			if (!IsPostBack)
			{
				IniPage();
				LoadData();
			}
		}

		private void IniPage() 
		{
			try 
			{
				this.txtCostBudgetSetCode.Value = Request["CostBudgetSetCode"];
				this.txtShowCostCode.Value = Request["CostCode"];
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

		private void LoadData()
		{
			try
			{
				//取申请或调整中的目标费用表
				EntityData entityChange = DAL.EntityDAO.CostBudgetDAO.GetCostBudgetByStatus(this.txtCostBudgetSetCode.Value, 1, "0,3", false);
				if (entityChange.HasRecord()) 
				{
					this.txtCostBudgetCode.Value = entityChange.GetString("CostBudgetCode");
				}
				entityChange.Dispose();

				//取当前有效的目标费用
				EntityData entityValid = BLL.CostBudgetRule.GetValidCostBudget(this.txtCostBudgetSetCode.Value, 1);
				if (entityValid.HasRecord())
				{
					this.txtValidCostBudgetCode.Value = entityValid.GetString("CostBudgetCode");
				}
				entityValid.Dispose();

				string CostBudgetCode = this.txtCostBudgetCode.Value;
				string CostBudgetSetCode = this.txtCostBudgetSetCode.Value;
				string ValidCostBudgetCode = this.txtValidCostBudgetCode.Value;

				//预算设置表信息
				EntityData entitySet = DAL.EntityDAO.CostBudgetDAO.GetV_CostBudgetSetByCode(CostBudgetSetCode);
				if ( entitySet.HasRecord())
				{
					/*
							//权限
							ArrayList ar = user.GetResourceRight(paymentCode,"Payment");
							if ( ! ar.Contains("060101"))
							{
								Response.Redirect( "../RejectAccess.aspx?OperationCode=060101" );
								Response.End();
							}

							this.btnModify.Visible = ar.Contains("060103");
							this.btnModifyEx.Visible = ar.Contains("060106");
							this.btnDelete.Visible = ar.Contains("060104");
							this.btnCheck.Visible = ar.Contains("060105");
							this.btnAccount.Visible = ar.Contains("060107");
							this.btnPayout.Visible = base.user.HasRight("060202");
							*/

					this.lblCostBudgetSetName.Text = entitySet.GetString("CostBudgetSetName");
					this.txtProjectCode.Value = entitySet.GetString("ProjectCode");

					this.txtGroupCode.Value = entitySet.GetString("GroupCode");
					this.lblGroupName.Text = entitySet.GetString("GroupName");
						
					this.lblUnitName.Text = entitySet.GetString("UnitName");
					this.lblPBSName.Text = entitySet.GetString("PBSName");
				}
				else 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "预算设置表不存在"));
					return;
				}
				entitySet.Dispose();

				if (CostBudgetCode == "")
				{
					if  (ValidCostBudgetCode == "")  //--------------------------新建申请---------------------------
					{
						this.lblTitle.Text = "申请";
						this.txtStatus.Value = "0";

						this.lblVerID.Text = "(保存时生成)";
						BindDataGrid(false);
					}
					else  //--------------------------新建调整---------------------------
					{
						this.lblTitle.Text = "调整";
						this.txtStatus.Value = "3";

						this.lblVerID.Text = "(保存时生成)";
						this.txtFirstCostBudgetCode.Value = entityValid.GetString("FirstCostBudgetCode");
                        this.txtCostBudgetName.Value = entityValid.GetString("CostBudgetName");

						BindDataGrid(false);
					}
				}
				else //--------------------------修改(申请、调整)---------------------------
				{
					//预算表
					EntityData entity = DAL.EntityDAO.CostBudgetDAO.GetV_CostBudgetByCode(CostBudgetCode);
					if ( entity.HasRecord())
					{
						DataRow dr = entity.CurrentRow;

						this.lblVerID.Text = entity.GetDecimal("VerID").ToString();
                        this.txtCostBudgetName.Value = entity.GetString("CostBudgetName");
						this.txtStatus.Value = entity.GetInt("Status").ToString();
						this.txtFirstCostBudgetCode.Value = entity.GetString("FirstCostBudgetCode");

						BindDataGrid(false);
					}
					else 
					{
						Response.Write(Rms.Web.JavaScript.Alert(true, "申请或调整中的预算表不存在"));
						return;
					}
					entity.Dispose();

					switch (this.txtStatus.Value) 
					{
						case "0":
							this.lblTitle.Text = "申请";
							break;

						case "3":
							this.lblTitle.Text = "调整";
							break;

					}
				}

			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示预算表出错：" + ex.Message));
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
			this.dgList.ItemCreated += new System.Web.UI.WebControls.RepeaterItemEventHandler(this.dgList_ItemCreated);
			this.dgList.ItemDataBound += new System.Web.UI.WebControls.RepeaterItemEventHandler(this.dgList_ItemDataBound);

		}
		#endregion

		/// <summary>
		/// 显示预算明细
		/// </summary>
		private void BindDataGrid(bool IsScreenToTable) 
		{
			try 
			{
				string CostBudgetCode = this.txtCostBudgetCode.Value;
				if (CostBudgetCode == "") //新建调整时，数据从当前生效带入
				{
					CostBudgetCode = this.txtValidCostBudgetCode.Value;
				}

				string StartY = "";
				string EndY = "";

				if (this.ucCostBudgetSelectMonth.ShowMonth) 
				{
					StartY = this.ucCostBudgetSelectMonth.MonthStart;
					EndY = this.ucCostBudgetSelectMonth.MonthEnd;
				}

				ViewState["StartY"] = StartY;
				ViewState["EndY"] = EndY;

				BLL.CostBudgetTarget target = new RmsPM.BLL.CostBudgetTarget(this.txtProjectCode.Value, CostBudgetCode, this.txtCostBudgetSetCode.Value);
				target.ShowCostCode = txtShowCostCode.Value;
				target.StartY = StartY;
				target.EndY = EndY;
				target.IsModify = true;

				DataTable tbDtl = target.GenerateCurrent();

				if (tbDtl == null) return;

				//预算计划按年度展开
				string html_title1 = "";
				string html_title2 = "";
				BLL.CostBudgetPageRule.GenerateCostBudgetPlanTitleHtml(tbDtl, target.iStartY, target.iEndY, ref html_title1, ref html_title2);

				ViewState["html_title1"] = html_title1;
				ViewState["html_title2"] = html_title2;

				//暂存明细表
				ViewState["tbDtl"] = tbDtl;

				if (IsScreenToTable)
				{
					//屏幕数据保存到临时表
					tbDtl = ScreenToTable(false);
				}
				else 
				{
					//折叠全部费用项
					BLL.CostBudgetPageRule.CollapseAll(tbDtl);

					//展开根结点
					if (this.txtShowCostCode.Value == "") 
					{
						BLL.CostBudgetPageRule.ExpandRoot(tbDtl);
					}
				}

				this.dgList.DataSource = tbDtl;
				this.dgList.DataBind();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示列表出错：" + ex.Message));
			}
		}

		/// <summary>
		/// 保存
		/// </summary>
		private void SaveData(DataTable tbDtl, ref bool IsNeedCheck)
		{
			try
			{
				string CostBudgetCode = this.txtCostBudgetCode.Value;
				IsNeedCheck = true;
				string ChangingCostBudgetCode = "";

				//取申请或调整中的目标费用表
				EntityData entity = DAL.EntityDAO.CostBudgetDAO.GetCostBudgetByStatus(this.txtCostBudgetSetCode.Value, 1, "0,3", false);
				if (entity.HasRecord()) 
				{
					ChangingCostBudgetCode = entity.GetString("CostBudgetCode");
				}
				entity.Dispose();

				//取当前有效的目标费用
				EntityData entityValid = BLL.CostBudgetRule.GetValidCostBudget(this.txtCostBudgetSetCode.Value, 1);

				if (this.txtStatus.Value == "3")  //调整
				{
					IsNeedCheck = BLL.CostBudgetRule.IsCostTargetNeedCheck(this.txtCostBudgetSetCode.Value, tbDtl);
				}

				if (!IsNeedCheck)  //无需审核时，直接更新当前有效的目标费用
				{
					this.txtCostBudgetCode.Value = "";
					CostBudgetCode = entityValid.GetString("CostBudgetCode");
				}

				//要保存的目标费用
				entity = RmsPM.DAL.EntityDAO.CostBudgetDAO.GetStandard_CostBudgetByCode(CostBudgetCode);

				//保存预算主表
				BLL.CostBudgetRule.SaveTempTarget(entity, entityValid, this.txtProjectCode.Value, this.txtCostBudgetSetCode.Value, BLL.ConvertRule.ToInt(this.txtStatus.Value), base.user.UserCode, this.txtCostBudgetName.Value);
				this.txtCostBudgetCode.Value = entity.GetString("CostBudgetCode");

				//保存预算明细
				BLL.CostBudgetRule.SaveCostBudgetDtl(entity, tbDtl, BLL.ConvertRule.ToString(ViewState["StartY"]), BLL.ConvertRule.ToString(ViewState["EndY"]));

				//保存主表的预算总额
				BLL.CostBudgetRule.SaveCostBudgetTotalBudgetMoney(entity.Tables["CostBudget"], entity.Tables["CostBudgetDtl"]);

				//提交
				using(StandardEntityDAO dao=new StandardEntityDAO("CostBudget"))
				{
					dao.BeginTrans();
					try
					{
						dao.SubmitEntity(entity);

						//删除调整中的目标费用
						if (!IsNeedCheck)
						{
							BLL.CostBudgetRule.DeleteChangingTarget(ChangingCostBudgetCode, dao);
						}

						dao.CommitTrans();
					}
					catch(Exception ex)
					{
						try 
						{
							//RollBackTrans会报错：此 SqlTransaction 已完成；它再也无法使用
							dao.RollBackTrans();
						}
						catch 
						{
						}

						throw ex;
					}
				}

//				DAL.EntityDAO.CostBudgetDAO.SubmitAllStandard_CostBudget(entity);

				entity.Dispose();
				entityValid.Dispose();

			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// 有效性检查
		/// </summary>
		/// <param name="Hint"></param>
		/// <returns></returns>
		private bool CheckValid(ref string Hint) 
		{
			Hint = "";

			/*
			if (this.ucCost.Value.Trim() == "") 
			{
				Hint = "请输入费用项";
				return false;
			}
			*/

			return true;
		}

		/// <summary>
		/// 返回
		/// </summary>
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
		/// 保存
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnSave_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				//屏幕数据保存到临时表
				DataTable tbDtl = ScreenToTable(false);
				if (tbDtl == null) return;

				string Hint = "";
				if (!CheckValid(ref Hint)) 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, Hint));
					return;
				}

				bool IsNeedCheck = true;
				SaveData(tbDtl, ref IsNeedCheck);

				if (IsNeedCheck)
				{
					Response.Write(JavaScript.Alert(true, "目标费用总额已变，需要审核"));
				}
				else 
				{
					Response.Write(JavaScript.Alert(true, "目标费用总额未变，无需审核，立即生效"));
				}

				GoBack();
			}
			catch(Exception ex) 
			{
				Response.Write(JavaScript.Alert(true, "保存失败：" + ex.Message));
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

		/// <summary>
		/// 屏幕数据保存到临时表
		/// </summary>
		/// <returns></returns>
		private DataTable ScreenToTable(bool isBindGrid) 
		{
			DataTable tb = (DataTable)ViewState["tbDtl"];

			foreach (RepeaterItem item in this.dgList.Items)
			{
//				HtmlInputText txtPrice = (HtmlInputText)item.FindControl("txtPrice");
//				HtmlInputText txtQty = (HtmlInputText)item.FindControl("txtQty");

				HtmlInputText txtBudgetMoney = (HtmlInputText)item.FindControl("txtBudgetMoney");
				HtmlInputHidden txtCostCode = (HtmlInputHidden)item.FindControl("txtCostCode");
				HtmlInputHidden txtIsExpand = (HtmlInputHidden)item.FindControl("txtIsExpand");
				HtmlInputText txtDescription = (HtmlInputText)item.FindControl("txtDescription");

				string CostCode = txtCostCode.Value;

				DataRow dr = null;
				DataRow[] drs = tb.Select("CostCode='" + CostCode + "'");
				if (drs.Length > 0) 
				{
					dr = drs[0];
				}
				else
				{
					throw new Exception("费用项" + CostCode + "在临时表中不存在，不能保存");
				}

//				dr["Price"] = BLL.ConvertRule.ToDecimal(txtPrice.Value);
//				dr["Qty"] = BLL.ConvertRule.ToDecimal(txtQty.Value);

				dr["BudgetMoney"] = BLL.ConvertRule.ToDecimal(txtBudgetMoney.Value);
				dr["Description"] = txtDescription.Value;

				dr["IsExpand"] = BLL.ConvertRule.ToInt(txtIsExpand.Value);

				//预算计划
				foreach(DataColumn col in tb.Columns) 
				{
					if (col.ColumnName.StartsWith("BudgetMoney_"))
					{
						string ym = col.ColumnName.Replace("BudgetMoney_", "");
						string y = ym.Substring(0, 4);
						string m = ym.Substring(4, 2);

						HtmlInputText txtM = (HtmlInputText)item.FindControl("txtBudgetMoney_" + ym);
						if (txtM != null) 
						{
							dr[col.ColumnName] = BLL.ConvertRule.ToDecimal(txtM.Value);
						}
					}
				}
			}

			if (isBindGrid) 
			{
				this.dgList.DataSource = tb;
				this.dgList.DataBind();
			}

			return tb;
		}

		private void dgList_ItemCreated(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
		{
			if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
			{
				PlaceHolder phPlan = (PlaceHolder)e.Item.FindControl("phPlan");

				DataTable tbDtl = (DataTable)ViewState["tbDtl"];

				//缺省展开当年
//				int curr_year = DateTime.Today.Year;

				foreach(DataColumn col in tbDtl.Columns) 
				{
					if (col.ColumnName.StartsWith("BudgetMoney_"))
					{
						string ym = col.ColumnName.Replace("BudgetMoney_", "");
						string y = ym.Substring(0, 4);
						string m = ym.Substring(4, 2);

						//只展开第1年
						int expand = BLL.CostBudgetPageRule.GetExpandByYear(y.ToString(), BLL.ConvertRule.ToString(ViewState["StartY"]));

						string month_display;

						if (expand == 0)  //折叠状态
						{
							month_display = "none";
						}
						else  //展开状态
						{
							month_display = "";
						}

						//输入金额
						HtmlInputText txt = new HtmlInputText();
						txt.ID = "txtBudgetMoney_" + ym;
						txt.Attributes["class"] = "input-nember";
						txt.Size = 8;
						txt.Attributes["onblur"] = "MoneyBlur(this, true);";
						txt.Attributes["onfocus"] = "MoneyFocus(this);";

						/*
						WebNumericEdit txt = new WebNumericEdit();
						txt.ID = "txtBudgetMoney_" + ym;
						txt.CssClass = "infra-input-nember";
						txt.ImageDirectory = "../images/infragistics/images/";
						txt.JavaScriptFileName = "../images/infragistics/20051/scripts/ig_edit.js";
						txt.JavaScriptFileNameCommon = "../images/infragistics/20051/scripts/ig_shared.js";
						txt.Width = Unit.Pixel(50);
						*/

						//显示金额
						HtmlGenericControl span = new HtmlGenericControl();
						span.ID = "spanBudgetMoney_" + ym;
						span.Style["display"] = "none";

						//单元格
						HtmlTableCell cell = new HtmlTableCell();
						cell.Controls.Add(txt);
						cell.Controls.Add(span);
						cell.Align = "right";
						cell.NoWrap = true;
						cell.ID = "YearData_" + ym;

						if (m == "00")  //年度
						{
						}
						else  //月度
						{
							cell.Style["display"] = month_display;
						}

						phPlan.Controls.Add(cell);

					}
				}
			}
		}

		private void dgList_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
		{
			if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
			{
				int iStartY = BLL.ConvertRule.ToInt(ViewState["StartY"]);
				int iEndY = BLL.ConvertRule.ToInt(ViewState["EndY"]);

				DataTable tbDtl = (DataTable)ViewState["tbDtl"];

				string CostBudgetDtlCode = ((HtmlInputHidden)e.Item.FindControl("txtCostBudgetDtlCode")).Value;
				PlaceHolder phPlan = (PlaceHolder)e.Item.FindControl("phPlan");

				//预算明细记录
				DataRow[] drsDtl = tbDtl.Select("CostBudgetDtlCode = '" + CostBudgetDtlCode + "'");
				DataRow drDtl = null;
				if (drsDtl.Length > 0) 
				{
					drDtl = drsDtl[0];
				}

				//预算计划
				if (iStartY > 0)
				{
					for(int iy=iStartY;iy<=iEndY;iy++)
					{
						for(int im=0;im<=12;im++)
						{
							string ym = BLL.ConvertRule.FormatYYYYMM(iy, im);

							//计划金额
							if (drDtl != null) 
							{
								string sMoney = BLL.CostBudgetPageRule.GetMoneyShowString(drDtl["BudgetMoney_" + ym]);

								HtmlInputText txt = (HtmlInputText)e.Item.FindControl("txtBudgetMoney_" + ym);
								HtmlGenericControl span = (HtmlGenericControl)e.Item.FindControl("spanBudgetMoney_" + ym);

								txt.Value = sMoney;
								txt.Attributes["RowIndex"] = e.Item.ItemIndex.ToString();
								txt.Attributes["ParentCode"] = BLL.ConvertRule.ToString(drDtl["ParentCode"]);

								span.InnerText = sMoney;
								span.Attributes["RowIndex"] = e.Item.ItemIndex.ToString();

								//							((HtmlInputText)phPlan.Controls[1].Controls[0]).Value = BLL.CostBudgetPageRule.GetMoneyShowString(drsM[0]["BudgetMoney_" + ym]);
								//							((HtmlInputText)phPlan.Controls[1].FindControl("txtBudgetMoney_" + ym)).Value = BLL.CostBudgetPageRule.GetMoneyShowString(drsM[0]["BudgetMoney_" + ym]);
							}
						}
					}
				}
			}
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
				BindDataGrid(true);
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示年度计划出错：" + ex.Message));
			}
		}

	}
}
