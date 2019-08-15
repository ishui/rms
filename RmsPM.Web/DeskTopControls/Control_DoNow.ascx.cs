namespace  RmsPM.Web.DeskTopControl
{
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
	using RmsPM.BLL;
    using System.Collections.Generic;


	/// <summary>
	///		Control_DoNow 的摘要说明。
	/// </summary>
	public partial class Control_DoNow :  Components.BaseControl
	{
		protected int intListAuditNum=6;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			DefaultSet();
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
		///		设计器支持所需的方法 - 不要使用代码编辑器
		///		修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion
		private void DefaultSet()
		{
			try
			{
				LoadDoNow();
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"获取首页数据失败");
			}
		}
		#region 显示在办箱
		private void LoadDoNow()
		{
			//EntityData entity = DAL.EntityDAO.WorkFlowDAO.GetAllWorkFlowProcedure();
			DataRow drAudit;
			///////////////////////////////////////////////////////////////////////////////////

			// 取得流程待审核，即在办箱单据 unm add 2005.4.4
			// ***********************流程待审核*****************************

			DataTable dtAuditclm = new DataTable();
			dtAuditclm.Columns.Add("Type",System.Type.GetType("System.String"));
			dtAuditclm.Columns.Add("Title",System.Type.GetType("System.String"));
			dtAuditclm.Columns.Add("Url",System.Type.GetType("System.String"));
			dtAuditclm.Columns.Add("AuditTime",System.Type.GetType("System.String"));
			dtAuditclm.Columns.Add("ProjectCode",System.Type.GetType("System.String"));

			EntityData entityclm = DAL.EntityDAO.WorkFlowDAO.GetAllWorkFlowProcedure();
			int iCountclm = entityclm.CurrentTable.Rows.Count;
            List<string> ProcedureNameList = new List<string>();
            for (int i = 0; i < iCountclm; i++)
            {
                entityclm.SetCurrentRow(i);
                // 取得每个类别的个数
                string ProcedureName = entityclm.GetString("ProcedureName");
                string ProcedureCodeclm = entityclm.GetString("ProcedureCode");
                if (!ProcedureNameList.Contains(ProcedureName))
                {
                    WorkFlowActStrategyBuilder sbclm = new WorkFlowActStrategyBuilder();
                    sbclm.AddStrategy(new Strategy(WorkFlowActStrategyName.Status, "'DealWith'")); // 收件箱
                    sbclm.AddStrategy(new Strategy(WorkFlowActStrategyName.InActUser, user.UserCode));
                    sbclm.AddStrategy(new Strategy(WorkFlowActStrategyName.ProcedureCodeIn, BLL.WorkFlowRule.GetProcedureCodeListByName(ProcedureName))); // 各种流程
                    //还有待问问包哥
                    //sbclm.AddStrategy(new Strategy(WorkFlowActStrategyName.ProcedureCode, ProcedureCodeclm)); // 各种流程
                    string sqlclm = sbclm.BuildMainQueryString();
                    QueryAgent qaclm = new QueryAgent();
                    EntityData entity1clm = qaclm.FillEntityData("WorkFlowAct", sqlclm);
                    qaclm.Dispose();

                    if (entity1clm.CurrentTable.Rows.Count > 0)
                    {
                        drAudit = dtAuditclm.NewRow();
                        drAudit["Type"] = entityclm.GetString("Description");
                        drAudit["Title"] = entity1clm.CurrentTable.Rows.Count;
                        drAudit["Url"] = "WorkFlowContral/WorkFlowInBox.aspx?ProcedureName=" + Server.UrlEncode(ProcedureName); //暂时去掉?ProcedureCode=" + entityclm.GetString("ProcedureCode");
                        drAudit["AuditTime"] = System.DBNull.Value;
                        dtAuditclm.Rows.Add(drAudit);
                    }
                    entity1clm.Dispose();
                    ProcedureNameList.Add(ProcedureName);
                }
            }
			entityclm.Dispose();

			// ***********************流程待审核*****************************
			
			
			DataView dvclm = new DataView(dtAuditclm);
			dvclm.Sort = " AuditTime desc ";
            
			// 取得全部审核的前n条
			DataTable dtTmpclm = new DataTable();
			dtTmpclm = dtAuditclm.Clone();
			int jclm=0;
			foreach(DataRowView drv in dvclm)
			{
				drAudit = dtTmpclm.NewRow();
				drAudit.ItemArray = drv.Row.ItemArray;
				dtTmpclm.Rows.Add(drAudit);

				jclm++;
				if(jclm>=this.intListAuditNum) break;
			}
			this.Repeater1.DataSource = dtTmpclm;
			this.Repeater1.DataBind();
		}
	}
	#endregion
}
