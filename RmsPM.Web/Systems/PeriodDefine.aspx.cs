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
	/// 计划周期定义； 定义计划的开始和结束时间，周期长度，每个周期的名称
	/// </summary>
	public partial class PeriodDefine : PageBase
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
			string projectCode = Request["ProjectCode"] + "";
			int iStartYear = DateTime.Now.Year;
			int iEndYear = 2020;
			int periodMonth = 12;
			int maxPeriods = 10;

			for ( int i=iStartYear; i<=iEndYear; i++)
				this.sltYear.Items.Add( new ListItem(i.ToString(),i.ToString()));
			this.sltYear.Value=DateTime.Now.Year.ToString();

			for ( int i=1;i<=periodMonth;i++)
				this.sltMonth.Items.Add(new ListItem(i.ToString(),i.ToString()));
			this.sltMonth.Value = "1";

			for ( int i=1;i<=maxPeriods;i++)
				this.sltTotalPeriods.Items.Add( new ListItem(i.ToString(),i.ToString()));
			this.sltTotalPeriods.Value = "5";

		}

		private void LoadData()
		{
			string projectCode = Request["ProjectCode"]+"";
			try
			{

				int totalPeriods = 0;
				int periodMonth = 12;

				ProjectConfigStrategyBuilder sb = new ProjectConfigStrategyBuilder();
				sb.AddStrategy( new Strategy( ProjectConfigStrategyName.ProjectCode , projectCode ) );
				string sql = sb.BuildMainQueryString();
				QueryAgent qa = new QueryAgent();
				EntityData projectConfig = qa.FillEntityData( "ProjectConfig",sql );
				qa.Dispose();
				
				DataRow[] drSelects = projectConfig.CurrentTable.Select( String.Format(  " ConfigName='PeriodMonth'" ));
				if ( drSelects.Length>0)
				{
					if ( !drSelects[0].IsNull("ConfigData"))
					{
						periodMonth = int.Parse((string)drSelects[0]["ConfigData"]);
						this.sltPeriodMonth.Value = periodMonth.ToString();
					}

				}

				drSelects = projectConfig.CurrentTable.Select( String.Format(  " ConfigName='TotalPeriods'" ));
				if ( drSelects.Length>0)
				{
					if ( !drSelects[0].IsNull("ConfigData"))
					{
						totalPeriods = int.Parse((string)drSelects[0]["ConfigData"]);
						this.sltTotalPeriods.Value = totalPeriods.ToString();
					}
				}
				projectConfig.Dispose();

				EntityData periodDefine = DAL.EntityDAO.SystemManageDAO.GetPeriodDefineByProjectCode(projectCode);
				this.dgYearName.DataSource= new DataView( periodDefine.CurrentTable,"","PeriodIndex",DataViewRowState.CurrentRows) ;
				this.dgYearName.DataBind();
				Session["PeriodDefineData"]=periodDefine;
				Session["totalPeriods"]=totalPeriods.ToString();
				Session["periodMonth"]=periodMonth.ToString();
				periodDefine.Dispose();

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
				int totalPeriods = int.Parse((string) Session["totalPeriods"]);
				int periodMonths = int.Parse((string)Session["periodMonth"]);
				EntityData periodDefine = (EntityData)Session["PeriodDefineData"];

				foreach ( DataGridItem li in this.dgYearName.Items)
				{
					string code = li.Cells[0].Text;
					string name = ((HtmlInputText)li.FindControl("txtYearName")).Value.Trim();
					foreach ( DataRow dr in periodDefine.CurrentTable.Select( String.Format( "PeriodDefineCode='{0}'" ,code),"",DataViewRowState.CurrentRows ))
					{
						dr["PeriodName"]=name;
					}
				}

				DAL.EntityDAO.SystemManageDAO.SubmitAllPeriodDefine(periodDefine);
				periodDefine.Dispose();

				ProjectConfigStrategyBuilder sb = new ProjectConfigStrategyBuilder();
				sb.AddStrategy( new Strategy( ProjectConfigStrategyName.ProjectCode , projectCode ) );
				string sql = sb.BuildMainQueryString();
				QueryAgent qa = new QueryAgent();
				EntityData projectConfig = qa.FillEntityData( "ProjectConfig",sql );
				qa.Dispose();
				
				DataRow[] drSelects = projectConfig.CurrentTable.Select( String.Format(  " ConfigName='TotalPeriods'" ));
				DataRow drC = null;
				if ( drSelects.Length > 0 )
					drC = drSelects[0];
				else
				{
					drC = projectConfig.GetNewRecord();
					drC["ProjectConfigCode"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("ProjectConfigCode");
					drC["ProjectCode"] = projectCode;
					drC["ConfigName"] = "TotalPeriods";
					projectConfig.AddNewRecord(drC);
				}
				drC["ConfigData"] = totalPeriods.ToString();

				drSelects = projectConfig.CurrentTable.Select( String.Format(  " ConfigName='PeriodMonths'" ));
				drC = null;
				if ( drSelects.Length > 0 )
					drC = drSelects[0];
				else
				{
					drC = projectConfig.GetNewRecord();
					drC["ProjectConfigCode"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("ProjectConfigCode");
					drC["ProjectCode"] = projectCode;
					drC["ConfigName"] = "PeriodMonths";
					projectConfig.AddNewRecord(drC);
				}
				drC["ConfigData"] = periodMonths.ToString();

				DAL.EntityDAO.SystemManageDAO.SubmitAllProjectConfig(projectConfig);
				projectConfig.Dispose();
				Session["PeriodDefineData"]=null;

				Response.Write(Rms.Web.JavaScript.ScriptStart);
				Response.Write(Rms.Web.JavaScript.Alert(false,"设置完成 ！"));
				Response.Write(Rms.Web.JavaScript.WinClose(false));
				Response.Write(Rms.Web.JavaScript.ScriptEnd);
				Response.End();

			}
			catch ( Exception ex)
			{
				ApplicationLog.WriteLog( this.ToString(),ex,"" );
			}
		}

		protected void btnReSetPeriod_ServerClick(object sender, System.EventArgs e)
		{
			string projectCode = Request["ProjectCode"]+"";
			try
			{
				int totalPeriods = int.Parse(this.sltTotalPeriods.Value);
				int periodMonth = int.Parse(this.sltPeriodMonth.Value);
				EntityData periodDefine = (EntityData)Session["PeriodDefineData"];
				int oldTotalPeriods = periodDefine.CurrentTable.Rows.Count;

				// 期数有没有变化
				if ( totalPeriods == oldTotalPeriods )
				{
				}
				else if ( oldTotalPeriods > totalPeriods)
				{
					foreach ( DataRow dr in periodDefine.CurrentTable.Select("","",DataViewRowState.CurrentRows))
						dr.Delete();
				}
				else if ( oldTotalPeriods < totalPeriods)
				{
					for ( int i=oldTotalPeriods+1;i<=totalPeriods;i++)
					{
						DataRow dr = periodDefine.GetNewRecord();
						dr["PeriodDefineCode"]=DAL.EntityDAO.SystemManageDAO.GetNewSysCode("PeriodDefineCode");
						dr["ProjectCode"]=projectCode;
						dr["PeriodIndex"]=i;
						periodDefine.AddNewRecord(dr);
					}
				}

				// 重新计算年度的开始和结束的时间
				int iStartYear = int.Parse( this.sltYear.Value );
				int iStartMonth = int.Parse(this.sltMonth.Value);
				DateTime startDate = DateTime.Parse( String.Format( "{0}-{1}-1"  ,iStartYear,iStartMonth ));

				foreach ( DataRow dr in periodDefine.CurrentTable.Select("","",DataViewRowState.CurrentRows))
				{
					int index = (int)dr["PeriodIndex"];
					DateTime sd = startDate.AddMonths( periodMonth*( index-1));
					DateTime ed = startDate.AddMonths( periodMonth*( index)).AddDays(-1);
					dr["StartDate"]=sd;
					dr["EndDate"]=ed;
					dr["PeriodName"]=sd.Year.ToString();
				}

				this.dgYearName.DataSource= new DataView( periodDefine.CurrentTable,"","PeriodIndex",DataViewRowState.CurrentRows) ;
				this.dgYearName.DataBind();
				Session["PeriodDefineData"]=periodDefine;
				Session["totalPeriods"]=totalPeriods.ToString();
				Session["periodMonth"]=periodMonth.ToString();
				periodDefine.Dispose();

			}
			catch ( Exception ex)
			{
				ApplicationLog.WriteLog( this.ToString(),ex,"" );
			}
		}


	}
}
