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
	/// CostBudgetSetModify 的摘要说明。
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
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

						//记录老的值
						this.OldGroupCode = entity.GetString("GroupCode");
//						this.OldCostCode = entity.GetString("CostCode");

						//看系统类别的权限
						ArrayList ar = user.GetResourceRight(this.txtCostBudgetSetCode.Value, "CostBudgetSet");
						if ( ! ar.Contains("041103"))  //预算表修改
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
					if (!base.user.HasRight("041102"))  //预算表新增
					{
						Response.Redirect( "../RejectAccess.aspx?OperationCode=041102" );
						Response.End();
					}

                    //缺省值
                    this.sltSetType.Value = "预算";

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
		/// 有效性检查
		/// </summary>
		/// <param name="Hint"></param>
		/// <returns></returns>
		private bool CheckValid(ref string Hint) 
		{
			Hint = "";

			if (this.txtCostBudgetSetName.Value.Trim() == "") 
			{
				Hint = "请输入预算表名称";
				return false;
			}

			if (this.ucGroup.Value.Trim() == "") 
			{
				Hint = "请输入类别";
				return false;
			}

			if (this.ucUnit.Value.Trim() == "") 
			{
				Hint = "请输入部门";
				return false;
			}

			if (this.ucPBS.PBSType == "")
			{
				Hint = "请输入单位工程";
				return false;
			}

            if (this.sltSetType.Value.Trim() == "")
            {
                Hint = "请输入设置类型";
                return false;
            }
            
            /*
            if (this.ucCost.Value.Trim() == "") 
            {
                Hint = "请输入费用项";
                return false;
            }
            */

			if (this.txtCostBudgetSetCode.Value != "") 
			{
				if (this.ucGroup.Value != this.OldGroupCode) 
				{
					//是否已做预算
					if (BLL.CostBudgetRule.IsCostBudgetSetHasBudget(this.txtCostBudgetSetCode.Value))
					{
						Hint = "已做预算，不能修改预算类别";
						return false;
					}
				}
			}

			/*
			//预算表名称不能重复
			if (BLL.ProductRule.IsModelNameExists(this.txtModelName.Value, this.txtModelCode.Value, this.txtProjectCode.Value))
			{
				Hint = "相同的预算表名称已存在 ！ ";
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
		/// 删除预算设置表
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "删除预算设置表出错：" + ex.Message));
			}
		}

		/// <summary>
		/// 单位工程改变时
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnPBSChange_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				//缺省预算表面积从单位工程读入
				if (this.ucPBS.PBSType == "B") //楼栋
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
				else //项目
				{
					//取楼栋面积之和
					EntityData entityBuilding = DAL.EntityDAO.ProductDAO.GetBuildingByProjectCode(this.txtProjectCode.Value);
					decimal BuildingArea = BLL.MathRule.SumColumn(entityBuilding.CurrentTable, "HouseArea");
					entityBuilding.Dispose();

					this.txtBuildingArea.Value = BuildingArea;
				}
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "删除预算设置表出错：" + ex.Message));
			}
		}

	}
}
