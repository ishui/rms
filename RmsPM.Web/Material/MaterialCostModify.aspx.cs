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
	/// MaterialCostModify 的摘要说明。
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
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

                        if (this.ucGroup.Text.StartsWith("系数含量"))
                        {
                            this.lblPriceTitle.Text = "含量";
                        }

                        //看系统类别的权限
                        ArrayList ar = user.GetResourceRight(this.txtMaterialCostCode.Value, "MaterialCost");
                        if (!ar.Contains("141103"))  //材料价格数据库修改
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
					if (!base.user.HasRight("141102"))  //材料价格新增
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
		/// 有效性检查
		/// </summary>
		/// <param name="Hint"></param>
		/// <returns></returns>
		private bool CheckValid(ref string Hint) 
		{
			Hint = "";

			if (this.ucGroup.Value.Trim() == "") 
			{
				Hint = "请输入分类";
				return false;
			}

            if (this.txtDescription.Text.Trim() == "")
            {
                Hint = "请输入描述";
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

				GoBack();
			}
			catch(Exception ex) 
			{
				Response.Write(JavaScript.Alert(true, "保存失败：" + ex.Message));
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
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
				BLL.MaterialRule.DeleteMaterialCost(this.txtMaterialCostCode.Value);

				GoBack();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "删除材料价格出错：" + ex.Message));
			}
		}

	}
}
