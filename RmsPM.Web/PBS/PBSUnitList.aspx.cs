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
	/// PBSUnitList 的摘要说明。
	/// </summary>
	public partial class PBSUnitList : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlGenericControl spanTitle;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdbtnAdd;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnAllowPaging;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnNewYearPlan;
		protected System.Web.UI.HtmlControls.HtmlTable TableToolbar;
		protected System.Web.UI.HtmlControls.HtmlTable TableToolbarPlan1;
		protected System.Web.UI.WebControls.Label lblIYear;
		protected System.Web.UI.HtmlControls.HtmlTable TableToolbarPlan2;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnDeleteYearPlan;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnBatchEditPlan;
		protected System.Web.UI.WebControls.DataGrid dgPlanList;
		protected System.Web.UI.HtmlControls.HtmlTable tbPlanList;
	
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
			this.dgList.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgList_PageIndexChanged);

		}
		#endregion

		private void IniPage() 
		{
			try 
			{
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];

				//权限
				this.btnAdd.Visible = base.user.HasRight("010402");

				LoadDataGrid();

				((PBSUnitHintEmpty)this.ucPBSUnitHintEmpty).SetProject(this.txtProjectCode.Value);
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

		private void LoadDataGrid() 
		{
			try 
			{
				PBSUnitStrategyBuilder sb = new PBSUnitStrategyBuilder("V_PBSUnit");

				string ProjectCode = this.txtProjectCode.Value;
				if (ProjectCode != "")
					sb.AddStrategy( new Strategy( PBSUnitStrategyName.ProjectCode, ProjectCode));

//				string PBSUnitCode = this.txtSearchPBSUnitCode.Value.Trim();
//				if (PBSUnitCode != "")
//					sb.AddStrategy(new Strategy(PBSUnitStrategyName.PBSUnitCode, PBSUnitCode));

				string PBSUnitName = this.txtSearchPBSUnitName.Value.Trim();
				if (PBSUnitName != "")
					sb.AddStrategy(new Strategy(PBSUnitStrategyName.PBSUnitNameLike, PBSUnitName));

				sb.AddOrder("PBSUnitName", true);

				string sql = sb.BuildMainQueryString();

				QueryAgent qa = new QueryAgent();
				EntityData entity = qa.FillEntityData( "PBSUnit",sql );
				qa.Dispose();

				string[] arrField = {"TotalBuildArea", "PInvest", "Invest"};
				decimal[] arrValue = BLL.MathRule.SumColumn(entity.CurrentTable, arrField);
				dgList.Columns[3].FooterText = arrValue[0].ToString("N");
				dgList.Columns[4].FooterText = arrValue[1].ToString("N");
				dgList.Columns[5].FooterText = arrValue[2].ToString("N");

				dgList.DataSource = entity;
				dgList.DataBind();
				entity.Dispose();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

		private void dgList_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.dgList.CurrentPageIndex = e.NewPageIndex;
			LoadDataGrid();
		}

		protected void btnSearch_ServerClick(object sender, System.EventArgs e)
		{
			this.dgList.CurrentPageIndex = 0;
			LoadDataGrid();
		}

	}
}
