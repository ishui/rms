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
	/// CostTargetModifyItem 的摘要说明。
	/// </summary>
	public partial class CostTargetModifyItem : PageBase
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

				//目标费用
				decimal BudgetMoney = 0;

				//取最新的目标费用表头
				entity = BLL.CostBudgetRule.GetCurrentCostBudget(this.txtCostBudgetSetCode.Value, 1, false);
				if (entity.HasRecord()) 
				{
					//取目标费用明细
					EntityData entityDtl = DAL.EntityDAO.CostBudgetDAO.GetCostBudgetDtlByCostBudgetCode(entity.GetString("CostBudgetCode"), this.txtCostCode.Value);
					if (entityDtl.HasRecord()) 
					{
						BudgetMoney = entityDtl.GetDecimal("BudgetMoney");
						this.txtDescription.Value = entityDtl.GetString("Description");
					}
					entityDtl.Dispose();
				}
				entity.Dispose();

				this.txtMoney.Value = BLL.CostBudgetPageRule.GetMoneyShowString(BudgetMoney);

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
				string CostBudgetCode = "";
				int status = 0;
				bool IsNeedCheck = true;
				string ChangingCostBudgetCode = "";

				//屏幕数据保存到临时表
				EntityData entityScreen = DAL.EntityDAO.CostBudgetDAO.GetCostBudgetDtlByCode("");
				DataTable tbScreen = entityScreen.CurrentTable;

				DataRow drScreen = tbScreen.NewRow();
				drScreen["CostBudgetDtlCode"] = -1;
				drScreen["CostCode"] = this.txtCostCode.Value;
				drScreen["BudgetMoney"] = BLL.ConvertRule.ToDecimal(this.txtMoney.Value);
				drScreen["Description"] = this.txtDescription.Value;
				tbScreen.Rows.Add(drScreen);

				DataTable tbDtl = BLL.CostBudgetRule.BuildTempTargetDtl(this.txtProjectCode.Value, this.txtCostBudgetSetCode.Value, tbScreen, ref status);
				entityScreen.Dispose();
//				DataTable tbDtl = BLL.CostBudgetRule.BuildTempTargetDtl(this.txtProjectCode.Value, this.txtCostBudgetSetCode.Value, this.txtCostCode.Value, BLL.ConvertRule.ToDecimal(this.txtMoney.Value), this.txtDescription.Value, ref status);

				//取申请或调整中的目标费用表
				EntityData entity = DAL.EntityDAO.CostBudgetDAO.GetCostBudgetByStatus(CostBudgetSetCode, 1, "0,3", false);
				if (entity.HasRecord()) 
				{
					CostBudgetCode = entity.GetString("CostBudgetCode");
					ChangingCostBudgetCode = entity.GetString("CostBudgetCode");
				}
				entity.Dispose();

				//取当前有效的目标费用
				EntityData entityValid = BLL.CostBudgetRule.GetValidCostBudget(this.txtCostBudgetSetCode.Value, 1);

				if (status == 3)  //调整
				{
					IsNeedCheck = BLL.CostBudgetRule.IsCostTargetNeedCheck(this.txtCostBudgetSetCode.Value, tbDtl);
				}

				if (!IsNeedCheck)  //无需审核时，直接更新当前有效的目标费用
				{
					CostBudgetCode = entityValid.GetString("CostBudgetCode");
				}

				//要保存的目标费用
				entity = RmsPM.DAL.EntityDAO.CostBudgetDAO.GetStandard_CostBudgetByCode(CostBudgetCode);

				//保存预算主表
				BLL.CostBudgetRule.SaveTempTarget(entity, entityValid, this.txtProjectCode.Value, this.txtCostBudgetSetCode.Value, status, base.user.UserCode, "");

				//保存预算明细
				BLL.CostBudgetRule.SaveCostBudgetDtl(entity, tbDtl, "", "");

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

			return true;
		}

		/// <summary>
		/// 返回
		/// </summary>
		private void GoBack() 
		{
			Response.Write(Rms.Web.JavaScript.ScriptStart);

			Response.Write("try {window.opener.RefreshTarget();}");
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

	}
}
