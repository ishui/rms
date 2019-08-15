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
	/// SupplierMaterialModify 的摘要说明。
	/// </summary>
	public partial class SupplierMaterialModify : PageBase
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
            this.spanSupplierName.InnerText = this.txtSupplierName.Value;
            
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
				this.txtSupplierMaterialCode.Value = Request["SupplierMaterialCode"];
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
				bool isNew = true;

				if (this.txtSupplierMaterialCode.Value != "") 
				{
					EntityData entity = RmsPM.DAL.EntityDAO.MaterialDAO.GetSupplierMaterialByCode(this.txtSupplierMaterialCode.Value);
                    if (entity.HasRecord())
                    {
                        isNew = false;

                        this.txtSupplierCode.Value = entity.GetString("SupplierCode");
                        this.txtSupplierName.Value = BLL.ProjectRule.GetSupplierName(entity.GetString("SupplierCode"));
                        this.spanSupplierName.InnerText = this.txtSupplierName.Value;

                        this.txtModel.Value = entity.GetString("model");
                        this.txtSpec.Value = entity.GetString("spec");
                        this.txtSampleID.Value = entity.GetString("SampleID");

                        this.txtUnit.Value = entity.GetString("Unit");
                        this.txtPrice.Value = BLL.MathRule.GetDecimalShowString(entity.GetDecimal("Price"), "0.##");
                        this.txtBrand.Value = entity.GetString("Brand");
                        this.txtNation.Value = entity.GetString("nation");
                        this.txtAreaCode.Value = entity.GetString("AreaCode");

                        this.ucGroup.Value = entity.GetString("GroupCode");

                        //看系统类别的权限
                        ArrayList ar = user.GetResourceRight(this.txtSupplierMaterialCode.Value, "SupplierMaterial");
                        if (!ar.Contains("141303"))  //厂商材料库修改
                        {
                            Response.Redirect("../RejectAccess.aspx?OperationCode=141303");
                            Response.End();
                        }

                        this.btnDelete.Visible = ar.Contains("141304");
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
                    if (!base.user.HasRight("141302"))  //厂商材料新增
					{
						Response.Redirect( "../RejectAccess.aspx?OperationCode=141302" );
						Response.End();
					}

					btnDelete.Visible = false;
				}
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
				string SupplierMaterialCode = this.txtSupplierMaterialCode.Value;

                EntityData entity = RmsPM.DAL.EntityDAO.MaterialDAO.GetSupplierMaterialByCode(SupplierMaterialCode);

                DataRow dr = null;
				if (!entity.HasRecord())
				{
					SupplierMaterialCode = RmsPM.DAL.EntityDAO.SystemManageDAO.GetNewSysCode("SupplierMaterialCode");
					this.txtSupplierMaterialCode.Value = SupplierMaterialCode;

					dr = entity.CurrentTable.NewRow();
					dr["SupplierMaterialCode"] = SupplierMaterialCode;
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
                dr["SupplierCode"] = this.txtSupplierCode.Value;
                dr["Brand"] = this.txtBrand.Value;
                dr["Model"] = this.txtModel.Value;
                dr["Spec"] = this.txtSpec.Value;
                dr["Nation"] = this.txtNation.Value;
                dr["AreaCode"] = this.txtAreaCode.Value;
                dr["SampleID"] = this.txtSampleID.Value;
                dr["GroupCode"] = this.ucGroup.Value;

				DAL.EntityDAO.MaterialDAO.SubmitAllSupplierMaterial(entity);

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

			if (this.ucGroup.Value.Trim() == "") 
			{
				Hint = "请输入材料类型";
				return false;
			}

            /*
            //材料价格名称不能重复
            if (BLL.ProductRule.IsModelNameExists(this.txtModelName.Value, this.txtModelCode.Value, this.txtProjectCode.Value))
            {
                Hint = "相同的材料价格名称已存在 ！ ";
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
			Response.Write("window.opener.location = window.opener.location;");
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

			}
			catch(Exception ex) 
			{
				Response.Write(JavaScript.Alert(true, "保存失败：" + ex.Message));
				ApplicationLog.WriteLog(this.ToString(),ex,"");
                return;
			}

            GoBack();
        }

		/// <summary>
		/// 删除材料价格
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnDelete_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				BLL.MaterialRule.DeleteSupplierMaterial(this.txtSupplierMaterialCode.Value);

			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "删除材料价格出错：" + ex.Message));
                return;
			}
        
            GoBack();
        }

	}
}
