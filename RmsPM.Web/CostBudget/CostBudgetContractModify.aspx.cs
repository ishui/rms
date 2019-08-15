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
	/// CostBudgetContractModify ��ժҪ˵����
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}

		private void LoadData()
		{
			try
			{
				if ((this.txtCostBudgetSetCode.Value == "") || (this.txtCostCode.Value == "") || (this.txtContractCode.Value == "") || (this.txtRelationType.Value == ""))
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "δ����Ԥ�����ñ��š��������š���ͬ��Ż���ؼ�¼����"));
					Response.Write(Rms.Web.JavaScript.WinClose(true));
					return;
				}

				//ȡԤ�����ñ�
				EntityData entity = DAL.EntityDAO.CostBudgetDAO.GetCostBudgetSetByCode(this.txtCostBudgetSetCode.Value);
				if (entity.HasRecord())
				{
					this.txtProjectCode.Value = entity.GetString("ProjectCode");
				}
				else 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, string.Format("Ԥ�����ñ�{0}������", this.txtCostBudgetSetCode.Value)));
					Response.Write(Rms.Web.JavaScript.WinClose(true));
					return;
				}
				entity.Dispose();

				//ȡ��������Ϣ
				EntityData entityCBS = DAL.EntityDAO.CBSDAO.GetCBSByCode(this.txtCostCode.Value);
				if (entityCBS.HasRecord()) 
				{
					this.lblSortID.Text = entityCBS.GetString("SortID");
					this.lblCostName.Text = entityCBS.GetString("CostName");
				}
				else 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, string.Format("������{0}������", this.txtCostCode.Value)));
					Response.Write(Rms.Web.JavaScript.WinClose(true));
					return;
				}
				entityCBS.Dispose();

				if (this.txtRelationType.Value.ToLower() == "Contract".ToLower()) 
				{
					//ȡ��ͬ��Ϣ
					EntityData entityContract = DAL.EntityDAO.ContractDAO.GetContractByCode(this.txtContractCode.Value);
					if (entityContract.HasRecord()) 
					{
						this.lblContractName.Text = entityContract.GetString("ContractName");
					}
					else 
					{
						Response.Write(Rms.Web.JavaScript.Alert(true, string.Format("��ͬ{0}������", this.txtContractCode.Value)));
						Response.Write(Rms.Web.JavaScript.WinClose(true));
						return;
					}
					entityContract.Dispose();
				}
				else if (this.txtRelationType.Value.ToLower() == "Bidding".ToLower()) 
				{
					//ȡ�б�ƻ�
					BLL.Bidding bidding = new BLL.Bidding();
					bidding.BiddingCode = this.txtContractCode.Value;
					this.lblContractName.Text = bidding.Title;
				}
				else 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, string.Format("δ֪����ؼ�¼���͡�{0}��", this.txtRelationType.Value)));
					Response.Write(Rms.Web.JavaScript.WinClose(true));
				}

				//��ͬԤ��
				decimal BudgetMoney = 0;

				//ȡ��ͬԤ��
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
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
		/// ����
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
		/// ��Ч�Լ��
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
		/// ����
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
		/// ����
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
				Response.Write(JavaScript.Alert(true, "����ʧ�ܣ�" + ex.Message));
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

	}
}
