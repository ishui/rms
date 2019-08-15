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
	/// CostBudgetPurchaseModify ��ժҪ˵����
	/// </summary>
	public partial class CostBudgetPurchaseModify : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack)
			{
				IniPage();
				LoadData();
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

		private void IniPage()
		{
			try
			{
//				this.txtPurchaseFlowCode.Value = Request.QueryString["PurchaseFlowCode"];
				this.txtPurchaseFlowDetailCode.Value = Request.QueryString["PurchaseFlowDetailCode"];
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];
				this.txtCostBudgetSetCode.Value = Request.QueryString["CostBudgetSetCode"];
				this.txtCostCode.Value = Request.QueryString["CostCode"];

				this.ucCostBudgetDtl.ProjectCode = this.txtProjectCode.Value;
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}

		private void LoadData()
		{
			try
			{
				string PurchaseFlowDetailCode = this.txtPurchaseFlowDetailCode.Value;

				//����ʱ���봫����Ŀ���롢Ԥ�����ñ��š���������
				if (PurchaseFlowDetailCode == "")
				{
					if ((this.txtProjectCode.Value == "") || (this.txtCostBudgetSetCode.Value == "") || (this.txtCostCode.Value == ""))
					Response.Write(Rms.Web.JavaScript.Alert(true, "δ������Ŀ�����Ԥ�����ñ��Ż�������ţ���������"));
					Response.Write(Rms.Web.JavaScript.WinClose(true));
					return;
				}

				if ( PurchaseFlowDetailCode != "")
				{
					//�޸�

					//��ϸ
					EntityData entityDtl = DAL.EntityDAO.PurchaseFlowDAO.GetPurchaseFlowDetailByCode(PurchaseFlowDetailCode);
					if ( entityDtl.HasRecord())
					{
						DataRow drDtl = entityDtl.CurrentRow;

						this.txtPurchaseFlowCode.Value = entityDtl.GetString("PurchaseCode");
						this.txtMoney.Value = BLL.StringRule.BuildShowNumberString(entityDtl.GetDecimal("Money"));
//						this.txtMoney.Value = BLL.StringRule.BuildShowNumberString(entityDtl.GetDecimal("Money"), "#,##0.####");
						this.txtDescription.Value = entityDtl.GetString("Description");

						this.txtCostCode.Value = entityDtl.GetString("CostCode");
						this.txtCostBudgetSetCode.Value = entityDtl.GetString("CostBudgetSetCode");

						//����
						EntityData entity = DAL.EntityDAO.PurchaseFlowDAO.GetPurchaseFlowByCode(this.txtPurchaseFlowCode.Value);
						if ( entity.HasRecord())
						{
							DataRow dr = entity.CurrentRow;

							this.txtPurpose.Value = entity.GetString("Purpose");
							this.txtProjectCode.Value = entity.GetString("ProjectCode");

						}
						else 
						{
							Response.Write(Rms.Web.JavaScript.Alert(true, "��ͬ�ƻ�������"));
							Response.Write(Rms.Web.JavaScript.WinClose(true));
							return;
						}
						entity.Dispose();

					}
					else 
					{
						Response.Write(Rms.Web.JavaScript.Alert(true, "��ͬ�ƻ���ϸ������"));
						Response.Write(Rms.Web.JavaScript.WinClose(true));
						return;
					}
					entityDtl.Dispose();

				}
				else 
				{
					//����

					//ȱʡֵ
//					this.dtPayDate.Value = DateTime.Today.ToString("yyyy-MM-dd");

				}

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
			}
			catch (  Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ���ݳ���" + ex.Message));
			}
		
		}

		/// <summary>
		/// ����
		/// </summary>
		private void SavaData()
		{
			try
			{
				//����
				EntityData entity = DAL.EntityDAO.PurchaseFlowDAO.GetPurchaseFlowByCode(this.txtPurchaseFlowCode.Value);
				DataRow dr = null;
				if (!entity.HasRecord()) //����
				{
					this.txtPurchaseFlowCode.Value = RmsPM.DAL.EntityDAO.SystemManageDAO.GetNewSysCode("PurchaseFlow");

					dr = entity.CurrentTable.NewRow();
					dr["PurchaseFlowCode"] = this.txtPurchaseFlowCode.Value;
					dr["ProjectCode"] = this.txtProjectCode.Value;
					dr["CreateDate"] = DateTime.Today;

					dr["State"] = 0;

					entity.CurrentTable.Rows.Add(dr);
				}
				else
				{
					dr = entity.CurrentRow;
				}

				dr["Purpose"] = this.txtPurpose.Value;

				//��ϸ
				EntityData entityDtl = DAL.EntityDAO.PurchaseFlowDAO.GetPurchaseFlowDetailByCode(this.txtPurchaseFlowDetailCode.Value);
				DataRow drDtl = null;
				if (!entityDtl.HasRecord())
				{
					this.txtPurchaseFlowDetailCode.Value = RmsPM.DAL.EntityDAO.SystemManageDAO.GetNewSysCode("PurchaseFlowDetail");

					drDtl = entityDtl.CurrentTable.NewRow();
					drDtl["PurchaseFlowCode"] = this.txtPurchaseFlowCode.Value;

					entityDtl.CurrentTable.Rows.Add(drDtl);
				}
				else 
				{
					drDtl = entityDtl.CurrentRow;
				}

				drDtl["Money"] = BLL.ConvertRule.ToDecimal(this.txtMoney.Value);
				drDtl["Description"] = this.txtDescription.Value;

				//�ύ
				using(StandardEntityDAO dao=new StandardEntityDAO("PurchaseFlow"))
				{
					dao.BeginTrans();
					try
					{
						//�ύ����
						dao.SubmitEntity(entity);

						//�ύ��ϸ
						dao.EntityName = "PurchaseFlowDetail";
						dao.SubmitEntity(entityDtl);

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
				entityDtl.Dispose();
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

			if (this.txtPurpose.Value.Trim() == "")
			{
				Hint = "�������ͬ����";
				return false;
			}

			if (this.txtMaterialName.Value.Trim() == "")
			{
				Hint = "��������������";
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

			Response.Write("try {window.opener.RefreshPurchase();}");
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
