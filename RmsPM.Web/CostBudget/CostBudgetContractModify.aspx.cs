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
	/// CostBudgetContractModify 的摘要说明。
	/// </summary>
	public partial class CostBudgetContractModify : PageBase
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
				this.txtContractCode.Value = Request["ContractCode"];
				this.txtRelationType.Value = Request["RelationType"];
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
				if ((this.txtCostBudgetSetCode.Value == "") || (this.txtCostCode.Value == "") || (this.txtContractCode.Value == "") || (this.txtRelationType.Value == ""))
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "未传入预算设置表编号、费用项编号、合同编号或相关记录类型"));
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

				if (this.txtRelationType.Value.ToLower() == "Contract".ToLower()) 
				{
					//取合同信息
					EntityData entityContract = DAL.EntityDAO.ContractDAO.GetContractByCode(this.txtContractCode.Value);
					if (entityContract.HasRecord()) 
					{
						this.lblContractName.Text = entityContract.GetString("ContractName");
					}
					else 
					{
						Response.Write(Rms.Web.JavaScript.Alert(true, string.Format("合同{0}不存在", this.txtContractCode.Value)));
						Response.Write(Rms.Web.JavaScript.WinClose(true));
						return;
					}
					entityContract.Dispose();
				}
				else if (this.txtRelationType.Value.ToLower() == "Bidding".ToLower()) 
				{
					//取招标计划
					BLL.Bidding bidding = new BLL.Bidding();
					bidding.BiddingCode = this.txtContractCode.Value;
					this.lblContractName.Text = bidding.Title;
				}
				else 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, string.Format("未知的相关记录类型“{0}”", this.txtRelationType.Value)));
					Response.Write(Rms.Web.JavaScript.WinClose(true));
				}

				//合同预算
				decimal BudgetMoney = 0;

				//取合同预算
				entity = DAL.EntityDAO.CostBudgetDAO.GetCostBudgetContractByContractCode(this.txtCostBudgetSetCode.Value, this.txtCostCode.Value, this.txtContractCode.Value, this.txtRelationType.Value);
				if (entity.HasRecord()) 
				{
					BudgetMoney = entity.GetDecimal("BudgetMoney");
					this.txtDescription.Value = entity.GetString("Description");
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
				DataRow dr;
				EntityData entity = DAL.EntityDAO.CostBudgetDAO.GetCostBudgetContractByContractCode(this.txtCostBudgetSetCode.Value, this.txtCostCode.Value, this.txtContractCode.Value, this.txtRelationType.Value);
				if (entity.HasRecord()) 
				{
					dr = entity.CurrentRow;
				}
				else 
				{
					dr = entity.CurrentTable.NewRow();

					dr["CostBudgetContractCode"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("CostBudgetContractCode");
					dr["ProjectCode"] = this.txtProjectCode.Value;
					dr["CostBudgetSetCode"] = this.txtCostBudgetSetCode.Value;
					dr["CostCode"] = this.txtCostCode.Value;
					dr["ContractCode"] = this.txtContractCode.Value;
					dr["RelationType"] = this.txtRelationType.Value;

					entity.CurrentTable.Rows.Add(dr);
				}

				dr["BudgetMoney"] = BLL.ConvertRule.ToDecimal(this.txtMoney.Value);
				dr["Description"] = this.txtDescription.Value;

				dr["ModifyPerson"] = base.user.UserCode;
				dr["ModifyDate"] = DateTime.Now;

				DAL.EntityDAO.CostBudgetDAO.SubmitAllCostBudgetContract(entity);

				entity.Dispose();
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

			Hint = BLL.CostBudgetRule.CheckCostBudgetContractInput(this.txtProjectCode.Value, this.txtCostBudgetSetCode.Value, this.txtCostCode.Value, this.txtContractCode.Value, this.txtRelationType.Value, BLL.ConvertRule.ToDecimal(this.txtMoney.Value));
			if (Hint != "")
			{
				return false;
			}

			return true;
		}

		/// <summary>
		/// 返回
		/// </summary>
		private void GoBack() 
		{
			Response.Write(Rms.Web.JavaScript.ScriptStart);

			Response.Write("try {window.opener.RefreshCostBudgetContract();}");
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
