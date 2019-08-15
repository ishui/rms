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

namespace RmsPM.Web.CostBudget
{
	/// <summary>
	/// CostPlanFrame 的摘要说明。
	/// </summary>
	public partial class CostPlanFrame : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlTable TableToolbar;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnUpdate;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack)
			{
				IniPage();
			}
		}

		private void IniPage()
		{
			try 
			{
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];

                switch (this.up_sPMName.ToUpper())
                {
                    case "SHIMAOPM":
                        //世茂：只显示本部门的预算表 xyq 2007.7.25
                        BLL.PageFacade.LoadCostBudgetSetSelect(this.sltCostBudgetSet, "", this.txtProjectCode.Value, user.m_EntityDataAccessUnit);
                        break;

                    default:
                        BLL.PageFacade.LoadCostBudgetSetSelect(this.sltCostBudgetSet, "", this.txtProjectCode.Value);
                        break;

                }

				//权限
				if (!base.user.HasRight("050302"))
					this.btnModify.Visible = false;
            }
			catch ( Exception ex )
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

	}
}
