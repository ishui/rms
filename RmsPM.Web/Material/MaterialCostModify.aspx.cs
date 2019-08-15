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

namespace RmsPM.Web.Material
{
	/// <summary>
	/// MaterialCostModify ��ժҪ˵����
	/// </summary>
	public partial class MaterialCostModify : PageBase
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
				this.txtMaterialCostCode.Value = Request["MaterialCostCode"];
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

				if (this.txtMaterialCostCode.Value != "") 
				{
					EntityData entity = RmsPM.DAL.EntityDAO.MaterialDAO.GetMaterialCostByCode(this.txtMaterialCostCode.Value);
                    if (entity.HasRecord())
                    {
                        isNew = false;

                        this.txtUnit.Value = entity.GetString("Unit");
                        this.txtPrice.Value = BLL.MathRule.GetDecimalShowString(entity.GetDecimal("Price"), "0.##");
                        this.dtBiddingDate.Value = entity.GetDateTimeOnlyDate("BiddingDate");
                        this.txtProject.Value = entity.GetString("Project");
                        this.txtAreaCode.Value = entity.GetString("AreaCode");
                        this.txtCategory.Value = entity.GetString("Category");

                        this.txtDescription.Text = entity.GetString("Description");
                        this.txtDescriptionEn.Text = entity.GetString("DescriptionEn");

                        this.ucGroup.Value = entity.GetString("GroupCode");

                        if (this.ucGroup.Text.StartsWith("ϵ������"))
                        {
                            this.lblPriceTitle.Text = "����";
                        }

                        //��ϵͳ����Ȩ��
                        ArrayList ar = user.GetResourceRight(this.txtMaterialCostCode.Value, "MaterialCost");
                        if (!ar.Contains("141103"))  //���ϼ۸����ݿ��޸�
                        {
                            Response.Redirect("../RejectAccess.aspx?OperationCode=141103");
                            Response.End();
                        }

                        this.btnDelete.Visible = ar.Contains("141104");
                    }
                    else
                    {
                        string MaterialTypeCode = Request["MaterialTypeCode"] + "";
                        this.ucGroup.Value = MaterialTypeCode;
                    }
					entity.Dispose();
				}

				if (isNew) 
				{
					if (!base.user.HasRight("141102"))  //���ϼ۸�����
					{
						Response.Redirect( "../RejectAccess.aspx?OperationCode=141102" );
						Response.End();
					}

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
				string MaterialCostCode = this.txtMaterialCostCode.Value;

				EntityData entity = RmsPM.DAL.EntityDAO.MaterialDAO.GetMaterialCostByCode(MaterialCostCode);

				DataRow dr = null;
				if (!entity.HasRecord())
				{
					MaterialCostCode = RmsPM.DAL.EntityDAO.SystemManageDAO.GetNewSysCode("MaterialCostCode");
					this.txtMaterialCostCode.Value = MaterialCostCode;

					dr = entity.CurrentTable.NewRow();
					dr["MaterialCostCode"] = MaterialCostCode;
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

				dr["Unit"] = this.txtUnit.Value;
                dr["Price"] = BLL.ConvertRule.ToDecimalObj(this.txtPrice.Value);
                dr["Project"] = this.txtProject.Value;
                dr["BiddingDate"] = BLL.ConvertRule.ToDate(this.dtBiddingDate.Value);
                dr["AreaCode"] = this.txtAreaCode.Value;
                dr["Category"] = this.txtCategory.Value;
                dr["GroupCode"] = this.ucGroup.Value;

                dr["Description"] = this.txtDescription.Text;
                dr["DescriptionEn"] = this.txtDescriptionEn.Text;

				DAL.EntityDAO.MaterialDAO.SubmitAllMaterialCost(entity);

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

			if (this.ucGroup.Value.Trim() == "") 
			{
				Hint = "���������";
				return false;
			}

            if (this.txtDescription.Text.Trim() == "")
            {
                Hint = "����������";
                return false;
            }

            /*
            //���ϼ۸����Ʋ����ظ�
            if (BLL.ProductRule.IsModelNameExists(this.txtModelName.Value, this.txtModelCode.Value, this.txtProjectCode.Value))
            {
                Hint = "��ͬ�Ĳ��ϼ۸������Ѵ��� �� ";
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
		/// ɾ�����ϼ۸�
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnDelete_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				BLL.MaterialRule.DeleteMaterialCost(this.txtMaterialCostCode.Value);

				GoBack();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "ɾ�����ϼ۸����" + ex.Message));
			}
		}

	}
}
