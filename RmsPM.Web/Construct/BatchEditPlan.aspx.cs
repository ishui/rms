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
using RmsPM.DAL;
using RmsPM.DAL.EntityDAO;
using RmsPM.DAL.QueryStrategy;
using Rms.Web;
using RmsPM.BLL;

namespace RmsPM.Web.Construct
{
	/// <summary>
	/// BatchEditPlan 的摘要说明。
	/// </summary>
	public partial class BatchEditPlan: PageBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack )
			{
				IniPage();
			}
		}

		private void IniPage()
		{
			try 
			{
				this.txtAct.Value = Request.QueryString["action"];
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];

				string Year = BLL.ConstructRule.GetConstructPlanCurrYearByProject(this.txtProjectCode.Value);
				if (Year == "") 
				{
					Year = DateTime.Today.Year.ToString();
				}
				this.txtIYear.Value = Year;

				this.spanYear.InnerText = this.txtIYear.Value;

				LoadData();
				LoadDataGrid();

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
				//形象进度字典
				EntityData entityV = DAL.EntityDAO.ConstructDAO.GetAllVisualProgress();
				ViewState["tbVisualProgress"] = entityV.CurrentTable;
				entityV.Dispose();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

		/// <summary>
		/// 显示计划明细
		/// </summary>
		private void LoadDataGrid()
		{
			string ProjectCode =  this.txtProjectCode.Value;

			try
			{
				int IYear = BLL.ConvertRule.ToInt(txtIYear.Value);

				DataTable tb = BLL.ConstructRule.GenerateConstructPlanTable(ProjectCode, IYear);

				BindDataGrid(tb);
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"加载计划失败");
				Response.Write(Rms.Web.JavaScript.Alert(true, "加载计划失败：" + ex.Message));
			}
		}

		/// <summary>
		/// 显示计划明细
		/// </summary>
		private void BindDataGrid(DataTable tb)
		{
			try
			{
				this.txtDetailCount.Value = tb.Rows.Count.ToString();

				string[] arrField = {"TotalBuildArea", "PTotalInvest", "InvestBefore", "LCFArea"};
				decimal[] arrValue = BLL.MathRule.SumColumn(tb, arrField);
				this.txtSumTotalBuildArea.Value = BLL.MathRule.GetDecimalShowString(arrValue[0]);
				this.txtSumPTotalInvest.Value = BLL.MathRule.GetDecimalShowString(arrValue[1]);
				this.txtSumInvestBefore.Value = BLL.MathRule.GetDecimalShowString(arrValue[2]);
				this.txtSumLCFArea.Value = BLL.MathRule.GetDecimalShowString(arrValue[3]);

				this.txtProjectPInvest.Value = BLL.MathRule.GetDecimalShowString(arrValue[1]);

				this.dgList.DataSource = tb;
				this.dgList.DataBind();

			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"加载计划失败");
				Response.Write(Rms.Web.JavaScript.Alert(true, "加载计划失败：" + ex.Message));
			}
		}

		public DataTable GetVisualProgressDataSource() 
		{
			return (DataTable)ViewState["tbVisualProgress"];
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
			this.dgList.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgList_ItemDataBound);

		}
		#endregion

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

				SaveDtl();

				GoBack();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "保存出错：" + ex.Message));
			}
		}

		/// <summary>
		/// 保存计划明细
		/// </summary>
		private void SaveDtl() 
		{
			try 
			{
				DataTable tb = ScreenToTable(true);

				string ProjectCode = this.txtProjectCode.Value;
				int IYear = BLL.ConvertRule.ToInt(this.txtIYear.Value);

				EntityData entityU = DAL.EntityDAO.PBSDAO.GetPBSUnitByProject(ProjectCode);
				EntityData entityP = DAL.EntityDAO.ConstructDAO.GetConstructAnnualPlanByProjectYear(ProjectCode, IYear);

				foreach(DataRow dr in tb.Rows) 
				{
					string PBSUnitCode = dr["PBSUnitCode"].ToString();

					//更新单位工程
					DataRow[] drsU = entityU.CurrentTable.Select("PBSUnitCode='" + PBSUnitCode + "'");
					if (drsU.Length > 0) 
					{
						DataRow drU = drsU[0];
						decimal PTotalInvest = BLL.ConvertRule.ToDecimal(dr["PTotalInvest"]);
						drU["PInvest"] = PTotalInvest;

						//更新年度计划
						DataRow[] drsP = entityP.CurrentTable.Select("PBSUnitCode='" + PBSUnitCode + "'");
						DataRow drP;
						if (drsP.Length > 0) 
						{
							drP = drsP[0];
						}
						else 
						{
							drP = entityP.CurrentTable.NewRow();

							drP["AnnualPlanCode"] = RmsPM.DAL.EntityDAO.SystemManageDAO.GetNewSysCode("AnnualPlanCode");
							drP["ProjectCode"] = ProjectCode;
							drP["PBSUnitCode"] = PBSUnitCode;
							drP["IYear"] = IYear;

							entityP.CurrentTable.Rows.Add(drP);
						}

						drP["VisualProgress"] = BLL.ConvertRule.ToString(dr["VisualProgress"]);
						drP["CurrentFloor"] = BLL.ConvertRule.ToInt(dr["CurrentFloor"]);
						drP["InvestBefore"] = BLL.ConvertRule.ToDecimal(dr["InvestBefore"]);
						drP["LCFArea"] = BLL.ConvertRule.ToDecimal(dr["LCFArea"]);

						//---------本年计划投资自动计算--begin
						int TotalFloorCount = BLL.PBSRule.GetPBSUnitFloorCount(PBSUnitCode);

						//本年末总投资
						decimal CurrTotalInvest = BLL.ConstructRule.CalcInvestByVisualProgress(PTotalInvest, BLL.ConvertRule.ToString(drP["VisualProgress"]), TotalFloorCount, BLL.ConvertRule.ToInt(drP["CurrentFloor"]));

						//本年投资 = 本年末总投资 - 上年末投资
						decimal CurrYearInvest = CurrTotalInvest - BLL.ConvertRule.ToDecimal(drP["InvestBefore"]);
						drP["PInvest"] = CurrYearInvest;

						//---------本年计划投资自动计算--end

						drP["PlanDate"] = DateTime.Now;
						drP["PlanPerson"] = base.user.UserCode;
					}
				}

				DAL.EntityDAO.PBSDAO.SubmitAllPBSUnit(entityU);
				DAL.EntityDAO.ConstructDAO.SubmitAllConstructAnnualPlan(entityP);

				entityP.Dispose();
				entityU.Dispose();
			}
			catch ( Exception ex )
			{
				throw ex;
			}
		}

		private void dgList_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem)) 
			{
				HtmlSelect sltVisualProgress = (HtmlSelect)e.Item.FindControl("sltVisualProgress");

				HtmlInputHidden txtVisualProgress = (HtmlInputHidden)e.Item.FindControl("txtVisualProgress");
				foreach(ListItem item in sltVisualProgress.Items) 
				{
					item.Selected = (item.Value == txtVisualProgress.Value);
				}
			}

			if (e.Item.ItemType == ListItemType.Footer) 
			{
				//显示合计金额
				((Label)e.Item.FindControl("lblSumTotalBuildArea")).Text = this.txtSumTotalBuildArea.Value;
				((Label)e.Item.FindControl("lblSumPTotalInvest")).Text = this.txtSumPTotalInvest.Value;
				((Label)e.Item.FindControl("lblSumInvestBefore")).Text = this.txtSumInvestBefore.Value;
				((Label)e.Item.FindControl("lblSumLCFArea")).Text = this.txtSumLCFArea.Value;
			}
		}

		/// <summary>
		/// 分摊
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnApport_ServerClick(object sender, System.EventArgs e)
		{
			try 
			{
				decimal ProjectPInvest = BLL.ConvertRule.ToDecimal(this.txtProjectPInvest.Value);
				decimal ProjectBuildArea = BLL.ConvertRule.ToDecimal(this.txtSumTotalBuildArea.Value);

				DataTable tb = ScreenToTable(false);

				int iCount = tb.Rows.Count;
				decimal SumPInvest = 0;
				for(int i=0;i<iCount;i++)
				{
					DataRow dr = tb.Rows[i];

					decimal BuildArea = BLL.ConvertRule.ToDecimal(dr["TotalBuildArea"]);
					decimal PInvest = 0;

					if (i < iCount - 1) 
					{
						if (ProjectBuildArea != 0) 
						{
							PInvest = Math.Round(ProjectPInvest * (BuildArea / ProjectBuildArea), 2);
						}
					}
					else 
					{
						//最后一条 = 总金额 - 前（n-1）条金额
						PInvest = ProjectPInvest - SumPInvest;
					}

					SumPInvest = SumPInvest + PInvest;

					dr["PTotalInvest"] = PInvest;
				}

				BindDataGrid(tb);
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "分摊出错：" + ex.Message));
			}
		}

		/// <summary>
		/// 屏幕数据保存到临时表
		/// </summary>
		/// <returns></returns>
		private DataTable ScreenToTable(bool isBindGrid) 
		{
			try 
			{
				string ProjectCode = this.txtProjectCode.Value;
				int IYear = BLL.ConvertRule.ToInt(this.txtIYear.Value);

				DataTable tb = BLL.ConstructRule.CreateConstructPlanTable();

				int i = -1;
				foreach (DataGridItem item in this.dgList.Items)
				{
					i++;

					string PBSUnitCode = this.dgList.DataKeys[i].ToString();
					HtmlInputHidden txtPBSUnitName = (HtmlInputHidden)item.FindControl("txtPBSUnitName");
					HtmlInputHidden txtAnnualPlanCode = (HtmlInputHidden)item.FindControl("txtAnnualPlanCode");
					HtmlInputHidden txtTotalBuildArea = (HtmlInputHidden)item.FindControl("txtTotalBuildArea");
					HtmlInputText txtPTotalInvest = (HtmlInputText)item.FindControl("txtPTotalInvest");
					HtmlSelect sltVisualProgress = (HtmlSelect)item.FindControl("sltVisualProgress");
					HtmlInputText txtCurrentFloor = (HtmlInputText)item.FindControl("txtCurrentFloor");
					HtmlInputText txtInvestBefore = (HtmlInputText)item.FindControl("txtInvestBefore");
					HtmlInputText txtLCFArea = (HtmlInputText)item.FindControl("txtLCFArea");

					DataRow dr = tb.NewRow();

					dr["PBSUnitCode"] = PBSUnitCode;
					dr["PBSUnitName"] = txtPBSUnitName.Value;
					dr["AnnualPlanCode"] = txtAnnualPlanCode.Value;
					dr["ProjectCode"] = ProjectCode;
					dr["IYear"] = IYear;

					dr["TotalBuildArea"] = BLL.ConvertRule.ToDecimal(txtTotalBuildArea.Value);
					dr["PTotalInvest"] = BLL.ConvertRule.ToDecimal(txtPTotalInvest.Value);
					dr["VisualProgress"] = sltVisualProgress.Value;
					dr["CurrentFloor"] = BLL.ConvertRule.ToInt(txtCurrentFloor.Value);
					dr["TotalBuildArea"] = BLL.ConvertRule.ToDecimal(txtTotalBuildArea.Value);
					dr["InvestBefore"] = BLL.ConvertRule.ToDecimal(txtInvestBefore.Value);
					dr["LCFArea"] = BLL.ConvertRule.ToDecimal(txtLCFArea.Value);

					tb.Rows.Add(dr);
				}

				if (isBindGrid) 
				{
					BindDataGrid(tb);
				}

				return tb;
			}
			catch ( Exception ex )
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

			int i = -1;
			foreach (DataGridItem item in this.dgList.Items)
			{
				i++;

				string PBSUnitCode = this.dgList.DataKeys[i].ToString();
				HtmlInputHidden txtPBSUnitName = (HtmlInputHidden)item.FindControl("txtPBSUnitName");
				HtmlInputText txtPTotalInvest = (HtmlInputText)item.FindControl("txtPTotalInvest");
				HtmlSelect sltVisualProgress = (HtmlSelect)item.FindControl("sltVisualProgress");
				HtmlInputText txtCurrentFloor = (HtmlInputText)item.FindControl("txtCurrentFloor");
				HtmlInputText txtInvestBefore = (HtmlInputText)item.FindControl("txtInvestBefore");
				HtmlInputText txtLCFArea = (HtmlInputText)item.FindControl("txtLCFArea");

				string PBSUnitName = txtPBSUnitName.Value;

				if ( txtPTotalInvest.Value != "" )
				{
					if ( ! Rms.Check.StringCheck.IsNumber(txtPTotalInvest.Value))
					{
						Hint = string.Format("“{0}”的计划总投资必须是数值 ！ ", PBSUnitName);
						return false;
					}
				}

				if ( txtCurrentFloor.Value != "" )
				{
					if ( ! Rms.Check.StringCheck.IsInt(txtCurrentFloor.Value))
					{
						Hint = string.Format("“{0}”的计划施工层数必须是整数 ！ ", PBSUnitName);
						return false;
					}
				}

				string tempHint = BLL.PBSRule.CheckPBSUnitFloorCount(PBSUnitCode, txtCurrentFloor.Value, string.Format("“{0}”的计划施工层数", PBSUnitName));
				if (tempHint != "") 
				{
					Hint = tempHint;
					return false;
				}

				if ( txtInvestBefore.Value != "" )
				{
					if ( ! Rms.Check.StringCheck.IsNumber(txtInvestBefore.Value))
					{
						Hint = string.Format("“{0}”的上年末完成投资必须是数值 ！ ", PBSUnitName);
						return false;
					}
				}

				if ( txtLCFArea.Value != "" )
				{
					if ( ! Rms.Check.StringCheck.IsNumber(txtLCFArea.Value))
					{
						Hint = string.Format("“{0}”的上年结转面积必须是数值 ！ ", PBSUnitName);
						return false;
					}
				}

			}

			return true;
		}

	}
}
