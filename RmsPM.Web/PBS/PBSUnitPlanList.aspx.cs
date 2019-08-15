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
using RmsPM.DAL.QueryStrategy;
using RmsPM.BLL;
using Rms.Web;

namespace RmsPM.Web.PBS
{
	/// <summary>
	/// PBSUnitPlanList 的摘要说明。
	/// </summary>
	public partial class PBSUnitPlanList : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlTable TableToolbar;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack) 
			{
				IniPage();
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
			this.dgPlanList.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgPlanList_PageIndexChanged);
			this.dgPlanList.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgPlanList_ItemDataBound);

		}
		#endregion

		private void IniPage() 
		{
			try 
			{
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];

				//权限
				this.btnBatchEditPlan.Visible = user.HasRight("020105");
				this.btnDeleteYearPlan.Visible = user.HasRight("020104");
				this.btnNewYearPlan.Visible = user.HasRight("020102");

				LoadPlan();
				LoadDataGridPlan();

				((PBSUnitHintEmpty)this.ucPBSUnitHintEmpty).SetProject(this.txtProjectCode.Value);
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

		private void LoadPlan() 
		{
			try 
			{
				//当前年度
				DataTable tbYear = DAL.EntityDAO.ConstructDAO.GetConstructAnnualPlanYearByProject(this.txtProjectCode.Value);
				if (tbYear.Rows.Count > 0) 
				{
					DataRow dr = tbYear.Rows[tbYear.Rows.Count-1];
					this.txtIYear.Value = BLL.ConvertRule.ToString(dr["IYear"]);
				}
				//						this.txtIYear.Value = BLL.ConstructRule.GetConstructPlanCurrYearByProject(this.txtProjectCode.Value);
				this.lblIYear.Text = this.txtIYear.Value;

				if (this.txtIYear.Value == "") 
				{
					this.btnNewYearPlan.Style["display"] = "none";
				}
				else 
				{
					this.btnNewYearPlan.Style["display"] = "";
				}

				//有两条以上年度时，可以删除当前年度
				if (tbYear.Rows.Count <= 1) 
				{
					this.btnDeleteYearPlan.Style["display"] = "none";
				}
				else 
				{
					this.btnDeleteYearPlan.Style["display"] = "";
				}

				//缺省为当年
				if (this.lblIYear.Text == "") 
				{
					this.lblIYear.Text = DateTime.Today.Year.ToString();
				}
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

		private void LoadDataGridPlan() 
		{
			try 
			{
				ConstructAnnualPlanStrategyBuilder sb = new ConstructAnnualPlanStrategyBuilder("V_ConstructAnnualPlan");

				string ProjectCode = this.txtProjectCode.Value;
				if (ProjectCode != "")
					sb.AddStrategy( new Strategy( ConstructAnnualPlanStrategyName.ProjectCode, ProjectCode));

				int IYear = BLL.ConvertRule.ToInt(this.txtIYear.Value);
				sb.AddStrategy(new Strategy(ConstructAnnualPlanStrategyName.IYear, IYear.ToString()));

				//				string PBSUnitCode = this.txtSearchPBSUnitCode.Value.Trim();
				//				if (PBSUnitCode != "")
				//					sb.AddStrategy(new Strategy(PBSUnitStrategyName.PBSUnitCode, PBSUnitCode));

				string PBSUnitName = this.txtSearchPBSUnitName.Value.Trim();
				if (PBSUnitName != "")
					sb.AddStrategy(new Strategy(ConstructAnnualPlanStrategyName.PBSUnitName, PBSUnitName));

				sb.AddOrder("PBSUnitName", true);

				string sql = sb.BuildMainQueryString();

				QueryAgent qa = new QueryAgent();
				EntityData entity = qa.FillEntityData( "V_ConstructAnnualPlan",sql );
				qa.Dispose();

				string[] arrField = {"TotalBuildArea", "PTotalInvest", "InvestBefore", "PInvest", "LCFArea"};
				decimal[] arrValue = BLL.MathRule.SumColumn(entity.CurrentTable, arrField);
				this.txtSumTotalBuildArea.Value = BLL.MathRule.GetDecimalShowString(arrValue[0]);
				this.txtSumPTotalInvest.Value = BLL.MathRule.GetDecimalShowString(arrValue[1]);
				this.txtSumInvestBefore.Value = BLL.MathRule.GetDecimalShowString(arrValue[2]);
				this.txtSumPInvest.Value = BLL.MathRule.GetDecimalShowString(arrValue[3]);
				this.txtSumLCFArea.Value = BLL.MathRule.GetDecimalShowString(arrValue[4]);

				dgPlanList.DataSource = entity;
				dgPlanList.DataBind();
				entity.Dispose();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

		protected void btnSearch_ServerClick(object sender, System.EventArgs e)
		{
			this.dgPlanList.CurrentPageIndex = 0;
			LoadDataGridPlan();
		}

		/// <summary>
		/// 删除年度计划
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnDeleteYearPlan_ServerClick(object sender, System.EventArgs e)
		{
			try 
			{
				int IYear = BLL.ConvertRule.ToInt(this.txtIYear.Value);
				BLL.ConstructRule.DeleteConstructAnnualPlan(this.txtProjectCode.Value, IYear);
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "删除出错：" + ex.Message));
				return;
			}

			LoadPlan();
			LoadDataGridPlan();
		}

		/// <summary>
		/// 结转年度计划
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnNewYearPlan_ServerClick(object sender, System.EventArgs e)
		{
			try 
			{
				int IYear = BLL.ConvertRule.ToInt(this.txtIYear.Value);
				BLL.ConstructRule.NewYearConstructAnnualPlan(this.txtProjectCode.Value, IYear, base.user.UserCode);
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "删除出错：" + ex.Message));
				return;
			}

			LoadPlan();
			LoadDataGridPlan();
		}

		private void dgPlanList_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.dgPlanList.CurrentPageIndex = e.NewPageIndex;
			LoadDataGridPlan();
		}

		private void dgPlanList_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Footer) 
			{
				//显示合计金额
				((Label)e.Item.FindControl("lblSumTotalBuildArea")).Text = this.txtSumTotalBuildArea.Value;
				((Label)e.Item.FindControl("lblSumPTotalInvest")).Text = this.txtSumPTotalInvest.Value;
				((Label)e.Item.FindControl("lblSumInvestBefore")).Text = this.txtSumInvestBefore.Value;
				((Label)e.Item.FindControl("lblSumPInvest")).Text = this.txtSumPInvest.Value;
				((Label)e.Item.FindControl("lblSumLCFArea")).Text = this.txtSumLCFArea.Value;
			}
		}
	}
}
