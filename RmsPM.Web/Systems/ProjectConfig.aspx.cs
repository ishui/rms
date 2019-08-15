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

namespace RmsPM.Web.Systems
{
	/// <summary>
	/// 预算控制力度： 1:  不控制； 2： 总额控制； 3： 精确控制
	/// </summary>
	public partial class ProjectConfig : PageBase
	{

	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack)
			{
				IniPage();
				LoadData();
			}
		}


		private void IniPage()
		{
		}

		private void LoadData()
		{
			string projectCode = Request["ProjectCode"]+"";
			try
			{

				ProjectConfigStrategyBuilder sb = new ProjectConfigStrategyBuilder();
				sb.AddStrategy( new Strategy( ProjectConfigStrategyName.ProjectCode , projectCode ) );
				string sql = sb.BuildMainQueryString();
				QueryAgent qa = new QueryAgent();
				EntityData projectConfig = qa.FillEntityData( "ProjectConfig",sql );
				qa.Dispose();
				
				DataRow[] drSelects = projectConfig.CurrentTable.Select( String.Format(  " ConfigName='BudgetControl'" ));
				if ( drSelects.Length>0)
				{
					if ( !drSelects[0].IsNull("ConfigData"))
						this.rblBudgetControl.SelectedValue = (string)drSelects[0]["ConfigData"];
				}
				projectConfig.Dispose();
			}
			catch ( Exception ex)
			{
				ApplicationLog.WriteLog( this.ToString(),ex,"" );
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

		protected void btnSave_ServerClick(object sender, System.EventArgs e)
		{


			string projectCode = Request["ProjectCode"]+"";
			try
			{

				ProjectConfigStrategyBuilder sb = new ProjectConfigStrategyBuilder();
				sb.AddStrategy( new Strategy( ProjectConfigStrategyName.ProjectCode , projectCode ) );
				string sql = sb.BuildMainQueryString();
				QueryAgent qa = new QueryAgent();
				EntityData projectConfig = qa.FillEntityData( "ProjectConfig",sql );
				qa.Dispose();
				
				
				DataRow[] drSelects = projectConfig.CurrentTable.Select( String.Format(  " ConfigName='BudgetControl'" ));
				DataRow drC = null;
				if ( drSelects.Length > 0 )
					drC = drSelects[0];
				else
				{
					drC = projectConfig.GetNewRecord();
					drC["ProjectConfigCode"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("ProjectConfigCode");
					drC["ProjectCode"] = projectCode;
					drC["ConfigName"] = "BudgetControl";
					projectConfig.AddNewRecord(drC);
				}
				drC["ConfigData"] = this.rblBudgetControl.SelectedValue;

				DAL.EntityDAO.SystemManageDAO.SubmitAllProjectConfig(projectConfig);
				projectConfig.Dispose();
				Response.Write(Rms.Web.JavaScript.ScriptStart);
				Response.Write(Rms.Web.JavaScript.WinClose(false));
				Response.Write(Rms.Web.JavaScript.ScriptEnd);
				Response.End();
			}
			catch ( Exception ex)
			{
				ApplicationLog.WriteLog( this.ToString(),ex,"" );
			}
		
		}
	}
}
