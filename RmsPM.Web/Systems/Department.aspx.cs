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
using Rms.Web;
using Rms.ORMap;
using RmsPM.DAL.EntityDAO;

namespace RmsPM.Web.Systems
{
	/// <summary>
	/// Department 的摘要说明。
	/// </summary>
	public partial class Department : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlGenericControl spanTitle;
		protected RmsPM.WebControls.ToolsBar.ToolsButton tbtnAdd;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack) 
			{
				string ProjectCode = Request["ProjectCode"] +"" ;
				if (ProjectCode != "") 
				{
					this.txtRootUnitCode.Value = "-1";
					this.txtProjectCode.Value = ProjectCode;

					EntityData entity = DAL.EntityDAO.ProjectDAO.GetProjectByCode(ProjectCode);
					if (entity.CurrentTable.Rows.Count > 0) 
					{
						this.txtRootUnitCode.Value = entity.GetString("UnitCode");
					}
					entity.Dispose();
				}
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
