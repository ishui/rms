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

namespace RmsPM.Web.PBS
{
	/// <summary>
	/// PBSUnitModify 的摘要说明。
	/// </summary>
	public partial class PBSUnitModify : PageBase
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack) 
			{
				IniPage();
				LoadData();
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
			this.dgList.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgList_DeleteCommand);

		}
		#endregion

		private void IniPage() 
		{
			try 
			{
				this.txtFromUrl.Value = Request.QueryString["FromUrl"];
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];
				this.txtPBSUnitCode.Value = Request.QueryString["PBSUnitCode"];

//				RmsPM.BLL.PageFacade.LoadDictionarySelect(this.sltDevelopUnit,"建设单位","");
				RmsPM.BLL.PageFacade.LoadDictionarySelect(this.sltConstructUnit,"施工单位","");
//				RmsPM.BLL.PageFacade.LoadVisualProgressSelect(this.sltVisualProgress,"");

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
				string PBSUnitCode = this.txtPBSUnitCode.Value.Trim();
				EntityData entityBuilding;
				if (PBSUnitCode != "") 
				{
					EntityData entity = DAL.EntityDAO.PBSDAO.GetPBSUnitByCode(PBSUnitCode);
					if (entity.HasRecord()) 
					{
						DataRow dr = entity.CurrentRow;

						this.txtPBSUnitName.Value = entity.GetString("PBSUnitName");
						this.txtPInvest.Value = BLL.MathRule.GetDecimalShowString(dr["PInvest"]);
						this.txtInvest.Value = BLL.MathRule.GetDecimalShowString(dr["Invest"]);
						this.txtPStartDate.Value = entity.GetDateTimeOnlyDate("PStartDate");
						this.txtPEndDate.Value = entity.GetDateTimeOnlyDate("PEndDate");
						this.txtStartDate.Value = entity.GetDateTimeOnlyDate("StartDate");
						this.txtEndDate.Value = entity.GetDateTimeOnlyDate("EndDate");
//						this.sltVisualProgress.Value = entity.GetString("VisualProgress");
						this.txtRemark.Value = entity.GetString("Remark");
						this.txtUFCode.Value = entity.GetString("UFCode");

						this.ucManager.Value = entity.GetString("Manager");

						this.sltConstructUnit.Value = entity.GetString("ConstructUnit");
//						this.sltDevelopUnit.Value = entity.GetString("DevelopUnit");

						this.txtProjectCode.Value = entity.GetString("ProjectCode");

					}
					entity.Dispose();

					//取楼栋列表
					entityBuilding = DAL.EntityDAO.ProductDAO.GetBuildingByPBSUnitCode(PBSUnitCode);
				}
				else 
				{
					//缺省计划开竣工日期
					EntityData entityP = DAL.EntityDAO.ProjectDAO.GetProjectByCode(this.txtProjectCode.Value);
					if (entityP.HasRecord()) 
					{
						this.txtPStartDate.Value = entityP.GetDateTimeOnlyDate("kgDate");
						this.txtPEndDate.Value = entityP.GetDateTimeOnlyDate("jgDate");
					}
					entityP.Dispose();

					//新增时楼栋列表为空
					entityBuilding = DAL.EntityDAO.ProductDAO.GetBuildingByCode("");
				}

				//显示楼栋列表
				Session["tbBuilding"] = entityBuilding.CurrentTable;
				entityBuilding.Dispose();
				BindDataGrid();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示信息出错：" + ex.Message));
			}
		}

		/// <summary>
		/// 显示楼栋列表
		/// </summary>
		private void BindDataGrid() 
		{
			try 
			{
				DataTable tb = (DataTable)Session["tbBuilding"];

				//记录多条楼栋代码，用“,”分隔
				string codes = "";
				foreach(DataRow dr in tb.Rows) 
				{
					if (codes != "") 
					{
						codes = codes + ",";
					}
					codes = codes + dr["BuildingCode"].ToString();
				}
				this.txtSelectBuildingCode.Value = codes;

				this.dgList.Columns[1].FooterText = BLL.StringRule.BuildShowNumberString(BLL.MathRule.SumColumn(tb,"Area"));
				this.dgList.DataSource = tb;
				this.dgList.DataBind();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示列表出错：" + ex.Message));
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

			if (this.txtPBSUnitName.Value.Trim() == "") 
			{
				Hint = "请输入单位工程名称";
				return false;
			}

//			if(sltVisualProgress.Value=="")
//			{
//				Hint = "请选择形象进度 ！ ";
//				return false;
//			}

			//单位工程名称不能重复
			if (BLL.PBSRule.IsPBSUnitNameExists(this.txtPBSUnitName.Value, this.txtPBSUnitCode.Value, this.txtProjectCode.Value))
			{
				Hint = "相同的单位工程名称已存在 ！ ";
				return false;
			}

			if (this.ucManager.Hint != "") 
			{
				Hint = "负责人输入有误：" + this.ucManager.Hint;
				return false;
			}

			return true;
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

				bool isNew = false;
				string PBSUnitCode = this.txtPBSUnitCode.Value.Trim();
				EntityData entity = DAL.EntityDAO.PBSDAO.GetPBSUnitByCode(PBSUnitCode);
				DataRow dr;

				if (entity.HasRecord()) 
				{
					dr = entity.CurrentRow;
				}
				else 
				{
					isNew = true;
					dr = entity.CurrentTable.NewRow();
					PBSUnitCode = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("PBSUnitCode");
					dr["PBSUnitCode"] = PBSUnitCode;
					dr["ProjectCode"] = txtProjectCode.Value;

					//新增时，形象进度为“未开工”
					dr["VisualProgress"] = BLL.PBSRule.GetFirstVisualProgress();
				}

				dr["PBSUnitName"] = this.txtPBSUnitName.Value.Trim();
				dr["Invest"] = BLL.ConvertRule.ToDecimal(this.txtInvest.Value.Trim());
				dr["PInvest"] = BLL.ConvertRule.ToDecimal(this.txtPInvest.Value.Trim());
				dr["PStartDate"] = BLL.ConvertRule.ToDate(this.txtPStartDate.Value.Trim());
				dr["PEndDate"] = BLL.ConvertRule.ToDate(this.txtPEndDate.Value.Trim());
				dr["StartDate"] = BLL.ConvertRule.ToDate(this.txtStartDate.Value.Trim());
				dr["EndDate"] = BLL.ConvertRule.ToDate(this.txtEndDate.Value.Trim());
				dr["Remark"] = this.txtRemark.Value.Trim();
//				dr["VisualProgress"] = this.sltVisualProgress.Value.Trim();
				dr["UFCode"] = this.txtUFCode.Value.Trim();
				dr["Manager"] = this.ucManager.Value;
				dr["ConstructUnit"] = this.sltConstructUnit.Value;
//				dr["DevelopUnit"] = this.sltDevelopUnit.Value;

				if (entity.HasRecord()) 
				{
					DAL.EntityDAO.PBSDAO.UpdatePBSUnit(entity);
				}
				else 
				{
					entity.CurrentTable.Rows.Add(dr);
					DAL.EntityDAO.PBSDAO.InsertPBSUnit(entity);
				}

				entity.Dispose();

				SaveBuilding(PBSUnitCode);

				//本年计划投资自动计算
				BLL.ConstructRule.UpdateConstructAnnualPlanInvest(PBSUnitCode);

				if (isNew) 
				{
					//生成缺省年度计划
					BLL.ConstructRule.InsertDefaultConstructAnnualPlan(txtProjectCode.Value);
				}

				GoBack();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "保存出错：" + ex.Message));
			}
		}

		/// <summary>
		/// 保存楼栋
		/// </summary>
		private void SaveBuilding(string PBSUnitCode) 
		{
			try 
			{
				DataTable tb = (DataTable)Session["tbBuilding"];

				//删除原先有现在没有的
				EntityData entity = DAL.EntityDAO.ProductDAO.GetBuildingByPBSUnitCode(PBSUnitCode);
				foreach(DataRow dr in entity.CurrentTable.Rows) 
				{
					string code = dr["BuildingCode"].ToString();
					if (tb.Select("BuildingCode='" + code + "'").Length == 0) 
					{
						EntityData entityBuilding = DAL.EntityDAO.ProductDAO.GetBuildingByCode(code);
						if (entityBuilding.HasRecord()) 
						{
							entityBuilding.CurrentTable.Rows[0]["PBSUnitCode"] = DBNull.Value;
							DAL.EntityDAO.ProductDAO.UpdateBuilding(entityBuilding);
						}
						entityBuilding.Dispose();
					}

				}
				entity.Dispose();

				//添加
				foreach(DataRow dr in tb.Rows) 
				{
					string code = dr["BuildingCode"].ToString();
					EntityData entityBuilding = DAL.EntityDAO.ProductDAO.GetBuildingByCode(code);
					if (entityBuilding.HasRecord()) 
					{
						if (entityBuilding.GetString("PBSUnitCode") != PBSUnitCode) 
						{
							entityBuilding.CurrentTable.Rows[0]["PBSUnitCode"] = PBSUnitCode;
							DAL.EntityDAO.ProductDAO.UpdateBuilding(entityBuilding);
						}
					}
					entityBuilding.Dispose();
				}

			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "保存出错：" + ex.Message));
			}
		}

		/// <summary>
		/// 返回
		/// </summary>
		private void GoBack() 
		{
			Response.Write(Rms.Web.JavaScript.ScriptStart);
			Response.Write("window.opener.location = window.opener.location;");
			Response.Write(Rms.Web.JavaScript.WinClose(false));
//			string FromUrl = this.txtFromUrl.Value.Trim();
//			if (FromUrl != "") 
//			{
//				Response.Write(string.Format("window.location = '{0}';", FromUrl));
//			}
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
		}

		/// <summary>
		/// 删除楼栋
		/// </summary>
		/// <param name="source"></param>
		/// <param name="e"></param>
		private void dgList_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try 
			{
				string code = this.dgList.DataKeys[e.Item.ItemIndex].ToString();

				DataTable dt = (DataTable)Session["tbBuilding"];
				DataRow[] drs = dt.Select("BuildingCode='" + code + "'");
				if (drs.Length > 0) 
				{
					dt.Rows.Remove(drs[0]);
				}

//				Session["tbBuilding"] = dt;
				BindDataGrid();
			}
			catch(Exception ex) 
			{
				Response.Write(JavaScript.Alert(true, "删除失败：" + ex.Message));
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

		/// <summary>
		/// 选择楼栋返回
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnSelectBuildingReturn_ServerClick(object sender, System.EventArgs e)
		{
			try 
			{
				string codes = this.txtSelectBuildingCode.Value.Trim();
				string[] arrcode = codes.Split(",".ToCharArray());

				DataTable tb = (DataTable)Session["tbBuilding"];
				tb.Rows.Clear();

				foreach(string code in arrcode) 
				{
					EntityData entity = DAL.EntityDAO.ProductDAO.GetBuildingByCode(code);
					if (entity.HasRecord()) 
					{
						DataRow dr = entity.CurrentTable.Rows[0];
						DataRow drNew = tb.NewRow();

						int iColumnCount = tb.Columns.Count;
						for ( int i =0 ; i<iColumnCount; i++)
						{
							string columnName= tb.Columns[i].ColumnName;
							if ( entity.CurrentTable.Columns.Contains(columnName))
								drNew[columnName] = dr[columnName];
						}
						tb.Rows.Add(drNew);
					}
					entity.Dispose();
				}

				Session["tbBuilding"] = tb;

				BindDataGrid();
			}
			catch(Exception ex) 
			{
				Response.Write(JavaScript.Alert(true, "失败：" + ex.Message));
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

	}
}
