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
	/// CostBudgetSetModify ��ժҪ˵����
	/// </summary>
	public partial class CostBudgetSetModify : PageBase
	{

		private string OldCostCode 
		{
			get {return BLL.ConvertRule.ToString(ViewState["OldCostCode"]);}
			set {ViewState["OldCostCode"] = value;}
		}

		private string OldGroupCode 
		{
			get {return BLL.ConvertRule.ToString(ViewState["OldGroupCode"]);}
			set {ViewState["OldGroupCode"] = value;}
		}

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

//				this.ucCost.ProjectCode = this.txtProjectCode.Value;
				this.ucPBS.ProjectCode = this.txtProjectCode.Value;
                PageFacade.LoadPBSTypeSelectAll(this.sltPBSTypeCode, "", this.txtProjectCode.Value);
                BLL.PageFacade.LoadCostBudgetSetTypeSelect(this.sltSetType, "");
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
				bool isNew = true;

				if (this.txtCostBudgetSetCode.Value != "") 
				{
					EntityData entity = RmsPM.DAL.EntityDAO.CostBudgetDAO.GetCostBudgetSetByCode(this.txtCostBudgetSetCode.Value);
					if (entity.HasRecord())
					{
						isNew = false;

						this.txtProjectCode.Value = entity.GetString("ProjectCode");
						this.txtCostBudgetSetName.Value = entity.GetString("CostBudgetSetName");

//						this.ucCost.ProjectCode = this.txtProjectCode.Value;
//						this.ucCost.Value = entity.GetString("CostCode");

						this.ucGroup.Value = entity.GetString("GroupCode");
						this.ucUnit.Value = entity.GetString("UnitCode");

						this.ucPBS.ProjectCode = entity.GetString("ProjectCode");
						this.ucPBS.SetCode(entity.GetString("PBSType"), entity.GetString("PBSCode"));

						this.txtBuildingArea.Value = entity.GetDecimal("BuildingArea");
						this.txtHouseCount.Value = entity.GetDecimal("HouseCount");

                        this.sltPBSTypeCode.Value = entity.GetString("PBSTypeCode");
                        this.sltSetType.Value = entity.GetString("SetType");

						//��¼�ϵ�ֵ
						this.OldGroupCode = entity.GetString("GroupCode");
//						this.OldCostCode = entity.GetString("CostCode");

						//��ϵͳ����Ȩ��
						ArrayList ar = user.GetResourceRight(this.txtCostBudgetSetCode.Value, "CostBudgetSet");
						if ( ! ar.Contains("041103"))  //Ԥ����޸�
						{
							Response.Redirect( "../RejectAccess.aspx?OperationCode=041103" );
							Response.End();
						}

						this.btnDelete.Visible = ar.Contains("041104");
					}
					entity.Dispose();
				}

				if (isNew) 
				{
					if (!base.user.HasRight("041102"))  //Ԥ�������
					{
						Response.Redirect( "../RejectAccess.aspx?OperationCode=041102" );
						Response.End();
					}

                    //ȱʡֵ
                    this.sltSetType.Value = "Ԥ��";

					btnDelete.Visible = false;
				}
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

				EntityData entity = RmsPM.DAL.EntityDAO.CostBudgetDAO.GetCostBudgetSetByCode(CostBudgetSetCode);

				DataRow dr = null;
				if (!entity.HasRecord())
				{
					CostBudgetSetCode = RmsPM.DAL.EntityDAO.SystemManageDAO.GetNewSysCode("CostBudgetSetCode");
					this.txtCostBudgetSetCode.Value = CostBudgetSetCode;

					dr = entity.CurrentTable.NewRow();
					dr["CostBudgetSetCode"] = CostBudgetSetCode;
					dr["ProjectCode"] = this.txtProjectCode.Value;
					entity.CurrentTable.Rows.Add(dr);

					dr["CreatePerson"] = base.user.UserCode;
					dr["CreateDate"] = DateTime.Now;
				}
				else
				{
					dr = entity.CurrentRow;

					dr["ModifyPerson"] = base.user.UserCode;
					dr["ModifyDate"] = DateTime.Now;
				}

				dr["CostBudgetSetName"] = this.txtCostBudgetSetName.Value;

				dr["GroupCode"] = this.ucGroup.Value;
				dr["UnitCode"] = this.ucUnit.Value;
				dr["PBSType"] = this.ucPBS.PBSType;
				dr["PBSCode"] = this.ucPBS.Value;
				dr["BuildingArea"] = this.txtBuildingArea.ValueDecimal;
				dr["HouseCount"] = this.txtHouseCount.ValueDecimal;
				//				dr["CostCode"] = this.ucCost.Value;

                dr["PBSTypeCode"] = this.sltPBSTypeCode.Value;
                dr["SetType"] = this.sltSetType.Value;

				DAL.EntityDAO.CostBudgetDAO.SubmitAllCostBudgetSet(entity);

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

			if (this.txtCostBudgetSetName.Value.Trim() == "") 
			{
				Hint = "������Ԥ�������";
				return false;
			}

			if (this.ucGroup.Value.Trim() == "") 
			{
				Hint = "���������";
				return false;
			}

			if (this.ucUnit.Value.Trim() == "") 
			{
				Hint = "�����벿��";
				return false;
			}

			if (this.ucPBS.PBSType == "")
			{
				Hint = "�����뵥λ����";
				return false;
			}

            if (this.sltSetType.Value.Trim() == "")
            {
                Hint = "��������������";
                return false;
            }
            
            /*
            if (this.ucCost.Value.Trim() == "") 
            {
                Hint = "�����������";
                return false;
            }
            */

			if (this.txtCostBudgetSetCode.Value != "") 
			{
				if (this.ucGroup.Value != this.OldGroupCode) 
				{
					//�Ƿ�����Ԥ��
					if (BLL.CostBudgetRule.IsCostBudgetSetHasBudget(this.txtCostBudgetSetCode.Value))
					{
						Hint = "����Ԥ�㣬�����޸�Ԥ�����";
						return false;
					}
				}
			}

			/*
			//Ԥ������Ʋ����ظ�
			if (BLL.ProductRule.IsModelNameExists(this.txtModelName.Value, this.txtModelCode.Value, this.txtProjectCode.Value))
			{
				Hint = "��ͬ��Ԥ��������Ѵ��� �� ";
				return false;
			}
			*/

			return true;
		}

		/// <summary>
		/// ����
		/// </summary>
		private void GoBack() 
		{
			Response.Write(Rms.Web.JavaScript.ScriptStart);
			Response.Write("window.opener.location = window.opener.location;");
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
				BLL.CostBudgetRule.DeleteCostBudgetSet(this.txtCostBudgetSetCode.Value);

				GoBack();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "ɾ��Ԥ�����ñ����" + ex.Message));
			}
		}

		/// <summary>
		/// ��λ���̸ı�ʱ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnPBSChange_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				//ȱʡԤ�������ӵ�λ���̶���
				if (this.ucPBS.PBSType == "B") //¥��
				{
					if (this.ucPBS.Value != "") 
					{
						EntityData entityBuilding = DAL.EntityDAO.ProductDAO.GetBuildingByCode(this.ucPBS.Value);
						if (entityBuilding.HasRecord()) 
						{
							this.txtBuildingArea.Value = entityBuilding.GetDecimal("HouseArea");
						}
						entityBuilding.Dispose();
					}
				}
				else //��Ŀ
				{
					//ȡ¥�����֮��
					EntityData entityBuilding = DAL.EntityDAO.ProductDAO.GetBuildingByProjectCode(this.txtProjectCode.Value);
					decimal BuildingArea = BLL.MathRule.SumColumn(entityBuilding.CurrentTable, "HouseArea");
					entityBuilding.Dispose();

					this.txtBuildingArea.Value = BuildingArea;
				}
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "ɾ��Ԥ�����ñ����" + ex.Message));
			}
		}

	}
}
