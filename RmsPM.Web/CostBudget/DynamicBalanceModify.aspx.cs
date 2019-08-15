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
	/// DynamicBalanceModify ��ժҪ˵����
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

				//Ԥ����̬����
				decimal BudgetMoney = 0;

				//ȡ��̬���ñ�
				entity = BLL.CostBudgetRule.GetValidCostBudget(this.txtCostBudgetSetCode.Value, 0);
				if (entity.HasRecord()) 
				{
					//ȡ��̬������ϸ
					EntityData entityDtl = DAL.EntityDAO.CostBudgetDAO.GetCostBudgetDtlByCostBudgetCode(entity.GetString("CostBudgetCode"), this.txtCostCode.Value);
					if (entityDtl.HasRecord()) 
					{
						BudgetMoney = entityDtl.GetDecimal("BudgetMoney");
						this.txtDescription.Value = entityDtl.GetString("Description");
					}
					entityDtl.Dispose();
				}
				entity.Dispose();

                //Ԥ�����ֱ��¼�룬��Ҫ�����Ѷ��ķǺ�ͬ��� xyq 2018.7.24
                this.txtMoney.Value = BLL.CostBudgetPageRule.GetMoneyShowString(BudgetMoney);

                /*
				//Ԥ���� = ��̬���� - �Ѷ���ͬ
				decimal balance = BudgetMoney - BLL.ConvertRule.ToDecimal(this.txtContractMoney.Value);
				this.txtMoney.Value = BLL.CostBudgetPageRule.GetMoneyShowString(balance);
                */

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
				string CostBudgetCode;

				//ȡ��ǰԤ�����ñ�����з�����
				EntityData entityAllCBS = BLL.CostBudgetRule.GetAllCBSBySet(this.txtProjectCode.Value, this.txtCostBudgetSetCode.Value);

				//���涯̬���ñ�
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

				//��̬������ϸ��ʷ��¼
				EntityData entityDtlHis = DAL.EntityDAO.CostBudgetDAO.GetCostBudgetDtlHisByCode("");

				//��̬������ϸ�����У�
				EntityData entityDtl = DAL.EntityDAO.CostBudgetDAO.GetV_CostBudgetDtlByCostBudgetCode(CostBudgetCode);

				//��ǰһ����̬������ϸ
				DataRow[] drs = entityDtl.CurrentTable.Select("CostCode = '" + this.txtCostCode.Value + "'");
				DataRow drDtl;
				if (drs.Length <= 0) 
				{
					drDtl = entityDtl.CurrentTable.NewRow();

					drDtl["CostBudgetDtlCode"] = RmsPM.DAL.EntityDAO.SystemManageDAO.GetNewSysCode("CostBudgetDtlCode");
					drDtl["CostBudgetCode"] = CostBudgetCode;
					drDtl["ProjectCode"] = this.txtProjectCode.Value;
					drDtl["CostCode"] = this.txtCostCode.Value;

					//���������Ϣ
					BLL.CostBudgetPageRule.FillCostBudgetDtlCBSData(drDtl, entityAllCBS.CurrentTable);

					entityDtl.CurrentTable.Rows.Add(drDtl);
				}
				else 
				{
					drDtl = drs[0];

					//���涯̬������ϸ��ʷ��¼
					BLL.CostBudgetRule.AddCostBudgetDtlHis(entityDtlHis.CurrentTable, drDtl, dr);
				}

                //Ԥ�����ֱ��¼�룬��Ҫ�����Ѷ��ķǺ�ͬ��� xyq 2018.7.24
                drDtl["BudgetMoney"] = BLL.ConvertRule.ToDecimal(this.txtMoney.Value);

                /*
				//��̬���� = Ԥ���� + �Ѷ���ͬ
				decimal money = BLL.ConvertRule.ToDecimal(this.txtMoney.Value) + BLL.ConvertRule.ToDecimal(this.txtContractMoney.Value);
				drDtl["BudgetMoney"] = money;
                */

				drDtl["Description"] = this.txtDescription.Value;

				//���¸���Ķ�̬����
				string FullCode = drDtl["FullCode"].ToString();
				string[] arrCostCode = FullCode.Split("-".ToCharArray());
				for(int i=arrCostCode.Length - 2;i>=0;i--) 
				{
					//������������ڵ�ǰԤ�����ñ�
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

						//���������Ϣ
						BLL.CostBudgetPageRule.FillCostBudgetDtlCBSData(drP, entityAllCBS.CurrentTable);

						entityDtl.CurrentTable.Rows.Add(drP);
					}

					//����Ķ�̬���� = �����ۼ�
					DataRow[] drsChild = entityDtl.CurrentTable.Select("ParentCode = '" + drP["CostCode"].ToString() + "'");
					decimal SumBudgetMoney = BLL.MathRule.SumColumn(drsChild, "BudgetMoney");

					//���涯̬������ϸ��ʷ��¼
					BLL.CostBudgetRule.AddCostBudgetDtlHis(entityDtlHis.CurrentTable, drP, dr);

					drP["BudgetMoney"] = SumBudgetMoney;
				}

				//��������
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

				//���������Ԥ���ܶ�
				BLL.CostBudgetRule.SaveCostBudgetTotalBudgetMoney(entity.CurrentTable, entityDtl.CurrentTable);

				//�ύ
				using(StandardEntityDAO dao=new StandardEntityDAO("CostBudget"))
				{
					dao.BeginTrans();
					try
					{
						//�ύ����
						dao.SubmitEntity(entity);

						//�ύ��ϸ
						dao.EntityName = "CostBudgetDtl";
						dao.SubmitEntity(entityDtl);

						//�ύ��ϸ��ʷ
						dao.EntityName = "CostBudgetDtlHis";
						dao.SubmitEntity(entityDtlHis);


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

			Response.Write("try {window.opener.RefreshBalance();}");
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

		/// <summary>
		/// ɾ��Ԥ�����ñ�
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "ɾ��Ԥ�����ñ����" + ex.Message));
			}
		}

	}
}
