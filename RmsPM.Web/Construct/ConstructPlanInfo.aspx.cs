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
using RmsPM.DAL.QueryStrategy;

namespace RmsPM.Web.Construct
{
	/// <summary>
	/// ConstructPlanInfo 的摘要说明。
	/// </summary>
	public partial class ConstructPlanInfo : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlInputButton btnShowPBSUnitCtrl;
		protected System.Web.UI.WebControls.Label lblTotalInvest;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack) 
			{
				IniPage();
				LoadPBSUnit();
				LoadYear();
				LoadData();
				LoadProgress();
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

		private void IniPage() 
		{
			try 
			{
				this.txtFromUrl.Value = Request.QueryString["FromUrl"];
				this.txtPBSUnitCode.Value = Request.QueryString["PBSUnitCode"];

				//权限
				this.btnModify.Visible = user.HasRight("020103");
				this.btnModifyProgressStep.Visible = user.HasRight("020103");
				this.btnDelete.Visible = user.HasRight("020104");
				this.btnAdd.Visible = user.HasRight("020102");

				this.btnAddProgress.Visible = user.HasRight("020202");
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

		private void LoadPBSUnit() 
		{
			try 
			{
				ClearAll();

				string PBSUnitCode = this.txtPBSUnitCode.Value;

				EntityData entityU = DAL.EntityDAO.PBSDAO.GetPBSUnitByCode(PBSUnitCode);
				if (entityU.HasRecord()) 
				{
					DataRow dr = entityU.CurrentRow;

					this.txtProjectCode.Value = entityU.GetString("ProjectCode");
					this.lblPBSUnitName.Text = entityU.GetString("PBSUnitName");
					string VisualProgress = BLL.ConstructRule.GetVisualProgressName(entityU.GetString("VisualProgress"));

					this.lblPBSUnitVisualProgress.Text = VisualProgress;

					if (VisualProgress == "结构")
					{
						//取当前施工层数
						EntityData entityR = BLL.ConstructRule.GetLastConstructProgressReport(PBSUnitCode);
						int CurrentLayer = 0;
						if (entityR.HasRecord()) 
						{
							CurrentLayer = entityR.GetInt("CurrentLayer");
						}
						this.lblPBSUnitVisualProgress.Text = this.lblPBSUnitVisualProgress.Text + string.Format("({0}层)", CurrentLayer);
						entityR.Dispose();
					}

					//取单位工程的楼栋
					EntityData entityB = DAL.EntityDAO.ProductDAO.GetBuildingByPBSUnitCode(PBSUnitCode);
					int BuildingCount = entityB.CurrentTable.Rows.Count;
					string BuildingCode = "";
					string BuildingName = "";
					decimal BuildingArea = 0;
					foreach(DataRow drB in entityB.CurrentTable.Rows)
					{
						if (BuildingCode != "") 
						{
							BuildingCode = BuildingCode + ",";
							BuildingName = BuildingName + ",";
						}

						BuildingCode = BuildingCode + BLL.ConvertRule.ToString(drB["BuildingCode"]);
						BuildingName = BuildingName + BLL.ConvertRule.ToString(drB["BuildingName"]);
						BuildingArea = BuildingArea + BLL.ConvertRule.ToDecimal(drB["Area"]);
					}
					entityB.Dispose();

					this.lblBuildingCount0.Visible = (BuildingCount == 0);
					this.hrefBuilding.Visible = (BuildingCount > 0);

					this.lblBuildingCount.Text = BuildingCount.ToString();
					this.txtBuildingCodes.Value = BuildingCode;
					this.txtBuildingNames.Value = BuildingName;
					this.lblBuildArea.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalShowString(BuildingArea), "平米");
//					this.lblBuildArea.Text = BLL.MathRule.GetDecimalShowString(dr["TotalBuildArea"]);
					this.lblTotalPInvest.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalShowString(dr["PInvest"]), "万元");
				}
				entityU.Dispose();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

		private void LoadYear() 
		{
			try 
			{
				this.sltYear.Items.Clear();
				BLL.PageFacade.LoadConstructPlanYearSelect(this.sltYear, "", this.txtProjectCode.Value, true);

				//缺省选择最新年度
				if (this.sltYear.Items.Count > 0) 
				{
					this.sltYear.Items[this.sltYear.Items.Count - 1].Selected = true;
				}

//				string CurrYear = this.sltYear.Value.Trim();
//				int iCurrYear;
//
//				if (CurrYear == "") 
//				{
//					iCurrYear = DateTime.Today.Year;
//				}
//				else 
//				{
//					iCurrYear = BLL.ConvertRule.ToInt(CurrYear);
//				}
//
//				this.sltYear.Items.Clear();
//
//				//前几年
//				for (int i=-5;i<0;i++) 
//				{
//					int year = iCurrYear + i;
//					this.sltYear.Items.Add(new ListItem(year.ToString(), year.ToString()));
//				}
//
//				//当年、后几年
//				for (int i=0;i<6;i++) 
//				{
//					int year = iCurrYear + i;
//					this.sltYear.Items.Add(new ListItem(year.ToString(), year.ToString()));
//				}
//
//				this.sltYear.Value = iCurrYear.ToString();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化年度出错：" + ex.Message));
			}
		}

		private void ClearAll() 
		{
			try 
			{
				this.txtAnnualPlanCode.Value = "";
				this.lblPBSUnitName.Text = "";
				this.lblPInvest.Text = "";
				this.lblInvestBefore.Text = "";
				this.lblLCFArea.Text = "";
				this.lblBuildArea.Text = "";
				this.lblVisualProgress.Text = "";

				this.tbConstructPlanChart.SetKeyValue(this.txtAnnualPlanCode.Value);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		private void LoadData() 
		{
			try 
			{
				string PBSUnitCode = this.txtPBSUnitCode.Value.Trim();

				if (this.sltYear.Items.Count > 0) 
				{
					int year = BLL.ConvertRule.ToInt(this.sltYear.Value);

					if (PBSUnitCode != "") 
					{
						EntityData entity = DAL.EntityDAO.ConstructDAO.GetConstructAnnualPlanByPBSUnitYear(PBSUnitCode, year);
						if (entity.HasRecord()) 
						{
							DataRow dr = entity.CurrentRow;

							this.txtAnnualPlanCode.Value = entity.GetString("AnnualPlanCode");
							this.lblPInvest.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalShowString(dr["PInvest"]), "万元");
							this.lblInvestBefore.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalShowString(dr["InvestBefore"]), "万元");
							this.lblLCFArea.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalShowString(dr["LCFArea"]), "平米");
							string VisualProgress = BLL.ConstructRule.GetVisualProgressName(entity.GetString("VisualProgress"));
							int CurrentFloor = entity.GetInt("CurrentFloor");

							this.lblVisualProgress.Text = VisualProgress;

							if (VisualProgress == "结构")
							{
								this.lblVisualProgress.Text = this.lblVisualProgress.Text + string.Format("({0}层)", CurrentFloor);
							}

							this.tbConstructPlanChart.SetKeyValue(this.txtAnnualPlanCode.Value);
						}
						entity.Dispose();
					}
				}

				IniToolbar();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

		private void LoadProgress() 
		{
			try 
			{
				string PBSUnitCode = this.txtPBSUnitCode.Value.Trim();
				EntityData entity = BLL.ConstructRule.GetLastConstructProgressReport(this.txtPBSUnitCode.Value);

//				EntityData entity = DAL.EntityDAO.ConstructDAO.GetConstructProgressByPBSUnit(PBSUnitCode);

				this.dgProgress.DataSource = entity;
				this.dgProgress.DataBind();
				entity.Dispose();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

		/// <summary>
		/// 初始化工具栏的按钮
		/// </summary>
		private void IniToolbar() 
		{
			if (this.sltYear.Items.Count > 0) 
			{
				if (this.sltYear.Value == this.sltYear.Items[this.sltYear.Items.Count - 1].Value) 
				{
					//最新年度
					this.btnAdd.Style["display"] = "";
					this.btnModify.Style["display"] = "";
					this.btnDelete.Style["display"] = "";
					this.btnModifyProgressStep.Style["display"] = "";
				}
				else 
				{
					//历史年度
					this.btnAdd.Style["display"] = "none";
					this.btnModify.Style["display"] = "none";
					this.btnDelete.Style["display"] = "none";
//					this.btnModifyProgressStep.Style["display"] = "none";
				}
			}
			else 
			{
				//无年度计划
				this.btnAdd.Style["display"] = "";

				this.btnModify.Style["display"] = "";
				//				this.btnModify.Style["display"] = "none";

				this.btnDelete.Style["display"] = "none";
				this.btnModifyProgressStep.Style["display"] = "none";
			}
		}

		/// <summary>
		/// 返回
		/// </summary>
		private void GoBack() 
		{
			Response.Write(Rms.Web.JavaScript.ScriptStart);
			string FromUrl = this.txtFromUrl.Value.Trim();
			if (FromUrl != "") 
			{
				Response.Write(string.Format("window.location = '{0}';", FromUrl));
			}
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
			Response.End();
		}

		protected void btnHiddenYear_ServerClick(object sender, System.EventArgs e)
		{
			LoadData();
		}

		protected void btnSetPBSUnit_ServerClick(object sender, System.EventArgs e)
		{
			LoadPBSUnit();
			LoadYear();
			LoadData();
			LoadProgress();
		}

	}
}
