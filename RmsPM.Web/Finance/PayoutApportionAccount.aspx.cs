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
using RmsPM.DAL.EntityDAO;
using RmsPM.DAL.QueryStrategy;
using Rms.Web;

namespace RmsPM.Web.Finance
{
	/// <summary>
	/// PayoutApportionAccount 的摘要说明。
	/// </summary>
	public partial class PayoutApportionAccount : PageBase
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack)
			{
				LoadData();
			}
		}

		private void LoadData()
		{
			try
			{
				string projectCode = Request["ProjectCode"]+"";

				BLL.PageFacade.LoadBuildingAreaFieldSelect(this.sltBuildingAreaField, BLL.CostRule.GetApportionAreaField(projectCode));

				//取项目的付款总额
				this.lblTotalCost.Text = BLL.StringRule.AddUnit(BLL.CostRule.GetTotalCost(projectCode).ToString("N"), "元");

				/*
				DataTable dt = BLL.CostRule.ApportionAllPayout( projectCode, BLL.CostRule.GetApportionAreaField(projectCode));
				this.dgGridApportion.DataSource = new DataView( dt,"","SortID,BuildingName",DataViewRowState.CurrentRows);
				this.dgGridApportion.Columns[4].FooterText = BLL.MathRule.SumColumn(dt,"ApportionMoney").ToString("N");
				this.dgGridApportion.DataBind();
				dt.Dispose();
				*/

			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write( Rms.Web.JavaScript.Alert(true,ex.Message) );
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
			this.btnSave.ServerClick += new System.EventHandler(this.btnSave_ServerClick);
		}
		#endregion



		private void btnSave_ServerClick(object sender, System.EventArgs e)
		{
			string projectCode = Request["ProjectCode"]+"";
			try
			{
				//保存面积字段
				BLL.SystemRule.UpdateProjectConfigValue(projectCode, BLL.SystemRule.m_CostApportionBuildingAreaField, this.sltBuildingAreaField.Value);

				BLL.CostRule.ProjectCostApportion(projectCode, BLL.CostRule.GetApportionAreaField(projectCode));
				CloseWindow();
				//Response.End();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write( Rms.Web.JavaScript.Alert(true,ex.Message) );
			}

		}

		/// <summary>
		/// 返回
		/// </summary>
		private void CloseWindow() 
		{
			Response.Write(Rms.Web.JavaScript.ScriptStart);
			Response.Write(Rms.Web.JavaScript.OpenerReload(false));
			Response.Write(Rms.Web.JavaScript.WinClose(false));
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
		}

	}
}
