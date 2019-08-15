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
	/// CostTargetModifyItem ��ժҪ˵����
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}

		private void LoadData()
		{
			try
			{
				if ((this.txtCostBudgetSetCode.Value == "") || (this.txtCostCode.Value == ""))
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "δ����Ԥ�����ñ��Ż��������"));
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

				//Ŀ�����
				decimal BudgetMoney = 0;

				//ȡ���µ�Ŀ����ñ�ͷ
				entity = BLL.CostBudgetRule.GetCurrentCostBudget(this.txtCostBudgetSetCode.Value, 1, false);
				if (entity.HasRecord()) 
				{
					//ȡĿ�������ϸ
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
				string CostBudgetSetCode = this.txtCostBudgetSetCode.Value;
				string CostBudgetCode = "";
				int status = 0;
				bool IsNeedCheck = true;
				string ChangingCostBudgetCode = "";

				//��Ļ���ݱ��浽��ʱ��
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

				//ȡ���������е�Ŀ����ñ�
				EntityData entity = DAL.EntityDAO.CostBudgetDAO.GetCostBudgetByStatus(CostBudgetSetCode, 1, "0,3", false);
				if (entity.HasRecord()) 
				{
					CostBudgetCode = entity.GetString("CostBudgetCode");
					ChangingCostBudgetCode = entity.GetString("CostBudgetCode");
				}
				entity.Dispose();

				//ȡ��ǰ��Ч��Ŀ�����
				EntityData entityValid = BLL.CostBudgetRule.GetValidCostBudget(this.txtCostBudgetSetCode.Value, 1);

				if (status == 3)  //����
				{
					IsNeedCheck = BLL.CostBudgetRule.IsCostTargetNeedCheck(this.txtCostBudgetSetCode.Value, tbDtl);
				}

				if (!IsNeedCheck)  //�������ʱ��ֱ�Ӹ��µ�ǰ��Ч��Ŀ�����
				{
					CostBudgetCode = entityValid.GetString("CostBudgetCode");
				}

				//Ҫ�����Ŀ�����
				entity = RmsPM.DAL.EntityDAO.CostBudgetDAO.GetStandard_CostBudgetByCode(CostBudgetCode);

				//����Ԥ������
				BLL.CostBudgetRule.SaveTempTarget(entity, entityValid, this.txtProjectCode.Value, this.txtCostBudgetSetCode.Value, status, base.user.UserCode, "");

				//����Ԥ����ϸ
				BLL.CostBudgetRule.SaveCostBudgetDtl(entity, tbDtl, "", "");

				//���������Ԥ���ܶ�
				BLL.CostBudgetRule.SaveCostBudgetTotalBudgetMoney(entity.Tables["CostBudget"], entity.Tables["CostBudgetDtl"]);

				//�ύ
				using(StandardEntityDAO dao=new StandardEntityDAO("CostBudget"))
				{
					dao.BeginTrans();
					try
					{
						dao.SubmitEntity(entity);

						//ɾ�������е�Ŀ�����
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
							//RollBackTrans�ᱨ���� SqlTransaction ����ɣ�����Ҳ�޷�ʹ��
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
		/// ��Ч�Լ��
		/// </summary>
		/// <param name="Hint"></param>
		/// <returns></returns>
		private bool CheckValid(ref string Hint) 
		{
			Hint = "";

			return true;
		}

		/// <summary>
		/// ����
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
