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
using RmsPM.BLL;

namespace RmsPM.Web.CostBudget
{
	/// <summary>
	/// DynamicBalanceModify 的摘要说明。
	/// </summary>
	public partial class DynamicBalanceModify : PageBase
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
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
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];
				this.txtCostBudgetSetCode.Value = Request["CostBudgetSetCode"];
				this.txtCostCode.Value = Request["CostCode"];
//				this.txtContractMoney.Value = Request["ContractMoney"];
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
				if ((this.txtCostBudgetSetCode.Value == "") || (this.txtCostCode.Value == ""))
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "未传入预算设置表编号或费用项编号"));
					Response.Write(Rms.Web.JavaScript.WinClose(true));
					return;
				}

				//取预算设置表
				EntityData entity = DAL.EntityDAO.CostBudgetDAO.GetCostBudgetSetByCode(this.txtCostBudgetSetCode.Value);
				if (entity.HasRecord())
				{
					this.txtProjectCode.Value = entity.GetString("ProjectCode");
				}
				else 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, string.Format("预算设置表{0}不存在", this.txtCostBudgetSetCode.Value)));
					Response.Write(Rms.Web.JavaScript.WinClose(true));
					return;
				}
				entity.Dispose();

				//取费用项信息
				EntityData entityCBS = DAL.EntityDAO.CBSDAO.GetCBSByCode(this.txtCostCode.Value);
				if (entityCBS.HasRecord()) 
				{
					this.lblSortID.Text = entityCBS.GetString("SortID");
					this.lblCostName.Text = entityCBS.GetString("CostName");
				}
				else 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, string.Format("费用项{0}不存在", this.txtCostCode.Value)));
					Response.Write(Rms.Web.JavaScript.WinClose(true));
					return;
				}
				entityCBS.Dispose();

				//预留动态费用
				decimal BudgetMoney = 0;

				//取动态费用表
				entity = BLL.CostBudgetRule.GetValidCostBudget(this.txtCostBudgetSetCode.Value, 0);
				if (entity.HasRecord()) 
				{
					//取动态费用明细
					EntityData entityDtl = DAL.EntityDAO.CostBudgetDAO.GetCostBudgetDtlByCostBudgetCode(entity.GetString("CostBudgetCode"), this.txtCostCode.Value);
					if (entityDtl.HasRecord()) 
					{
						BudgetMoney = entityDtl.GetDecimal("BudgetMoney");
						this.txtDescription.Value = entityDtl.GetString("Description");
					}
					entityDtl.Dispose();
				}
				entity.Dispose();

                //预留金额直接录入，不要包括已定的非合同请款 xyq 2018.7.24
                this.txtMoney.Value = BLL.CostBudgetPageRule.GetMoneyShowString(BudgetMoney);

                /*
				//预算金额 = 动态费用 - 已定合同
				decimal balance = BudgetMoney - BLL.ConvertRule.ToDecimal(this.txtContractMoney.Value);
				this.txtMoney.Value = BLL.CostBudgetPageRule.GetMoneyShowString(balance);
                */

			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
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

		/// <summary>
		/// 保存
		/// </summary>
		private void SavaData()
		{
			try
			{
				string CostBudgetSetCode = this.txtCostBudgetSetCode.Value;
				string CostBudgetCode;

				//取当前预算设置表的所有费用项
				EntityData entityAllCBS = BLL.CostBudgetRule.GetAllCBSBySet(this.txtProjectCode.Value, this.txtCostBudgetSetCode.Value);

				//保存动态费用表
				EntityData entity = BLL.CostBudgetRule.GetValidCostBudget(CostBudgetSetCode, 0);
				bool isNew = false;

				DataRow dr = null;
				if (!entity.HasRecord())
				{
					isNew = true;
					CostBudgetCode = RmsPM.DAL.EntityDAO.SystemManageDAO.GetNewSysCode("CostBudgetCode");

					dr = entity.CurrentTable.NewRow();
					dr["CostBudgetCode"] = CostBudgetCode;
					dr["CostBudgetSetCode"] = CostBudgetSetCode;
					dr["ProjectCode"] = this.txtProjectCode.Value;

					dr["Status"] = 1;
					dr["TargetFlag"] = 0;
					dr["FirstCostBudgetCode"] = CostBudgetCode;
					dr["VerID"] = 0;

					entity.CurrentTable.Rows.Add(dr);
				}
				else
				{
					CostBudgetCode = entity.GetString("CostBudgetCode");

					dr = entity.CurrentRow;
				}

				//动态费用明细历史记录
				EntityData entityDtlHis = DAL.EntityDAO.CostBudgetDAO.GetCostBudgetDtlHisByCode("");

				//动态费用明细（所有）
				EntityData entityDtl = DAL.EntityDAO.CostBudgetDAO.GetV_CostBudgetDtlByCostBudgetCode(CostBudgetCode);

				//当前一条动态费用明细
				DataRow[] drs = entityDtl.CurrentTable.Select("CostCode = '" + this.txtCostCode.Value + "'");
				DataRow drDtl;
				if (drs.Length <= 0) 
				{
					drDtl = entityDtl.CurrentTable.NewRow();

					drDtl["CostBudgetDtlCode"] = RmsPM.DAL.EntityDAO.SystemManageDAO.GetNewSysCode("CostBudgetDtlCode");
					drDtl["CostBudgetCode"] = CostBudgetCode;
					drDtl["ProjectCode"] = this.txtProjectCode.Value;
					drDtl["CostCode"] = this.txtCostCode.Value;

					//填费用项信息
					BLL.CostBudgetPageRule.FillCostBudgetDtlCBSData(drDtl, entityAllCBS.CurrentTable);

					entityDtl.CurrentTable.Rows.Add(drDtl);
				}
				else 
				{
					drDtl = drs[0];

					//保存动态费用明细历史记录
					BLL.CostBudgetRule.AddCostBudgetDtlHis(entityDtlHis.CurrentTable, drDtl, dr);
				}

                //预留金额直接录入，不要包括已定的非合同请款 xyq 2018.7.24
                drDtl["BudgetMoney"] = BLL.ConvertRule.ToDecimal(this.txtMoney.Value);

                /*
				//动态费用 = 预算金额 + 已定合同
				decimal money = BLL.ConvertRule.ToDecimal(this.txtMoney.Value) + BLL.ConvertRule.ToDecimal(this.txtContractMoney.Value);
				drDtl["BudgetMoney"] = money;
                */

				drDtl["Description"] = this.txtDescription.Value;

				//更新父项的动态费用
				string FullCode = drDtl["FullCode"].ToString();
				string[] arrCostCode = FullCode.Split("-".ToCharArray());
				for(int i=arrCostCode.Length - 2;i>=0;i--) 
				{
					//费用项必须属于当前预算设置表
					if (entityAllCBS.CurrentTable.Select("CostCode = '" + arrCostCode[i] + "'").Length <= 0)
					{
						break;
					}

					DataRow[] drsP = entityDtl.CurrentTable.Select("CostCode = '" + arrCostCode[i] + "'");
					DataRow drP;
					if (drsP.Length > 0) 
					{
						drP = drsP[0];
					}
					else 
					{
						drP = entityDtl.CurrentTable.NewRow();

						drP["CostBudgetDtlCode"] = RmsPM.DAL.EntityDAO.SystemManageDAO.GetNewSysCode("CostBudgetDtlCode");
						drP["CostBudgetCode"] = CostBudgetCode;
						drP["ProjectCode"] = this.txtProjectCode.Value;
						drP["CostCode"] = arrCostCode[i];

						//填费用项信息
						BLL.CostBudgetPageRule.FillCostBudgetDtlCBSData(drP, entityAllCBS.CurrentTable);

						entityDtl.CurrentTable.Rows.Add(drP);
					}

					//父项的动态费用 = 子项累计
					DataRow[] drsChild = entityDtl.CurrentTable.Select("ParentCode = '" + drP["CostCode"].ToString() + "'");
					decimal SumBudgetMoney = BLL.MathRule.SumColumn(drsChild, "BudgetMoney");

					//保存动态费用明细历史记录
					BLL.CostBudgetRule.AddCostBudgetDtlHis(entityDtlHis.CurrentTable, drP, dr);

					drP["BudgetMoney"] = SumBudgetMoney;
				}

				//保存主表
				if (isNew) 
				{
					dr["ModifyPerson"] = base.user.UserCode;
					dr["ModifyDate"] = DateTime.Now;

//					dr["CreatePerson"] = base.user.UserCode;
//					dr["CreateDate"] = DateTime.Now;
				}
				else 
				{
					dr["ModifyPerson"] = base.user.UserCode;
					dr["ModifyDate"] = DateTime.Now;
				}

				//保存主表的预算总额
				BLL.CostBudgetRule.SaveCostBudgetTotalBudgetMoney(entity.CurrentTable, entityDtl.CurrentTable);

				//提交
				using(StandardEntityDAO dao=new StandardEntityDAO("CostBudget"))
				{
					dao.BeginTrans();
					try
					{
						//提交主表
						dao.SubmitEntity(entity);

						//提交明细
						dao.EntityName = "CostBudgetDtl";
						dao.SubmitEntity(entityDtl);

						//提交明细历史
						dao.EntityName = "CostBudgetDtlHis";
						dao.SubmitEntity(entityDtlHis);


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

				/*
				DAL.EntityDAO.CostBudgetDAO.SubmitAllCostBudget(entity);
				DAL.EntityDAO.CostBudgetDAO.SubmitAllCostBudgetDtl(entityDtl);
				*/

				entity.Dispose();
				entityDtl.Dispose();
				entityDtlHis.Dispose();
				entityAllCBS.Dispose();

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

			return true;
		}

		/// <summary>
		/// 返回
		/// </summary>
		private void GoBack() 
		{
			Response.Write(Rms.Web.JavaScript.ScriptStart);

			Response.Write("try {window.opener.RefreshBalance();}");
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
				string Hint = "";
				if (!CheckValid(ref Hint)) 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, Hint));
					return;
				}

				SavaData();

				GoBack();
			}
			catch(Exception ex) 
			{
				Response.Write(JavaScript.Alert(true, "保存失败：" + ex.Message));
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

		/// <summary>
		/// 删除预算设置表
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnDelete_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "删除预算设置表出错：" + ex.Message));
			}
		}

	}
}
