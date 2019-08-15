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
	///		Control_DoNow ��ժҪ˵����
	/// </summary>
	public partial class Control_DoNow :  Components.BaseControl
	{
		protected int intListAuditNum=6;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			DefaultSet();
		}

		#region Web ������������ɵĴ���
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: �õ����� ASP.NET Web ���������������ġ�
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		///		�����֧������ķ��� - ��Ҫʹ�ô���༭��
		///		�޸Ĵ˷��������ݡ�
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
				ApplicationLog.WriteLog(this.ToString(),ex,"��ȡ��ҳ����ʧ��");
			}
		}
		#region ��ʾ�ڰ���
		private void LoadDoNow()
		{
			//EntityData entity = DAL.EntityDAO.WorkFlowDAO.GetAllWorkFlowProcedure();
			DataRow drAudit;
			///////////////////////////////////////////////////////////////////////////////////

			// ȡ�����̴���ˣ����ڰ��䵥�� unm add 2005.4.4
			// ***********************���̴����*****************************

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
                // ȡ��ÿ�����ĸ���
                string ProcedureName = entityclm.GetString("ProcedureName");
                string ProcedureCodeclm = entityclm.GetString("ProcedureCode");
                if (!ProcedureNameList.Contains(ProcedureName))
                {
                    WorkFlowActStrategyBuilder sbclm = new WorkFlowActStrategyBuilder();
                    sbclm.AddStrategy(new Strategy(WorkFlowActStrategyName.Status, "'DealWith'")); // �ռ���
                    sbclm.AddStrategy(new Strategy(WorkFlowActStrategyName.InActUser, user.UserCode));
                    sbclm.AddStrategy(new Strategy(WorkFlowActStrategyName.ProcedureCodeIn, BLL.WorkFlowRule.GetProcedureCodeListByName(ProcedureName))); // ��������
                    //���д����ʰ���
                    //sbclm.AddStrategy(new Strategy(WorkFlowActStrategyName.ProcedureCode, ProcedureCodeclm)); // ��������
                    string sqlclm = sbclm.BuildMainQueryString();
                    QueryAgent qaclm = new QueryAgent();
                    EntityData entity1clm = qaclm.FillEntityData("WorkFlowAct", sqlclm);
                    qaclm.Dispose();

                    if (entity1clm.CurrentTable.Rows.Count > 0)
                    {
                        drAudit = dtAuditclm.NewRow();
                        drAudit["Type"] = entityclm.GetString("Description");
                        drAudit["Title"] = entity1clm.CurrentTable.Rows.Count;
                        drAudit["Url"] = "WorkFlowContral/WorkFlowInBox.aspx?ProcedureName=" + Server.UrlEncode(ProcedureName); //��ʱȥ��?ProcedureCode=" + entityclm.GetString("ProcedureCode");
                        drAudit["AuditTime"] = System.DBNull.Value;
                        dtAuditclm.Rows.Add(drAudit);
                    }
                    entity1clm.Dispose();
                    ProcedureNameList.Add(ProcedureName);
                }
            }
			entityclm.Dispose();

			// ***********************���̴����*****************************
			
			
			DataView dvclm = new DataView(dtAuditclm);
			dvclm.Sort = " AuditTime desc ";
            
			// ȡ��ȫ����˵�ǰn��
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
