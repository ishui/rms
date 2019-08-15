namespace RmsPM.Web.DeskTopControl
{
    using System;
    using System.Data;
    using System.Configuration;
    using System.Collections;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.WebControls.WebParts;
    using System.Web.UI.HtmlControls;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;
    using RmsPM.DAL.QueryStrategy;
    using RmsPM.BLL;
    using System.Collections.Generic;

    public partial class control_pw_rpRemind : Components.BaseControl
    {
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
                LoadRemind();
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "��ȡ��ҳ����ʧ��");
            }
        }
        #region ���ѵĴ���
        private void LoadRemind()
        {

            //�����������ݱ�
            DataTable dtRemind = new DataTable();
            dtRemind.Columns.Add("Type", System.Type.GetType("System.String"));
            dtRemind.Columns.Add("Title", System.Type.GetType("System.String"));
            dtRemind.Columns.Add("Url", System.Type.GetType("System.String"));
            dtRemind.Columns.Add("RemindTime", System.Type.GetType("System.String"));
            dtRemind.Columns.Add("RemindRand", System.Type.GetType("System.String"));//���Ѽ���


            DataView dv = new DataView(user.m_EntityDataAccessProject.CurrentTable, "", "ProjectName", DataViewRowState.CurrentRows);

            //DataRow drRemindflow = dtRemind.NewRow();
            //drRemindflow["Type"] = ">>���̳���";
            //drRemindflow["Url"] = "#";
            //drRemindflow["Title"] = "";
            //dtRemind.Rows.Add(drRemindflow);
            //int BeginCount = WorkFlowLoadRemind(dtRemind, "'Begin'");
            //int DealWithCount = WorkFlowLoadRemind(dtRemind, "'DealWith'");
            //if ((BeginCount + DealWithCount) == 0)
            //    dtRemind.Rows.Remove(drRemindflow);

            for (int i = 0; i < dv.Count; i++)
            {
                DataRow drRemind = dtRemind.NewRow();
                drRemind["Type"] = ">>" + dv[i].Row["ProjectName"].ToString();
                drRemind["Url"] = "#";
                drRemind["Title"] = "";
                dtRemind.Rows.Add(drRemind);

                int projectItemCount = 0;
                if (user.HasOperationRight("060231"))
                    projectItemCount += PayLoad(dtRemind, dv[i].Row["ProjectCode"].ToString());
                //��Ͷ�굽�� New do it
                //if(user.HasOperationRight("2102"))
                //    projectItemCount += BiddingLoad(dtRemind,dv[i].Row["ProjectCode"].ToString());

                if (user.HasOperationRight("050160"))
                    projectItemCount += ContractLoad(dtRemind, dv[i].Row["ProjectCode"].ToString());

                if (projectItemCount == 0)
                    dtRemind.Rows.Remove(drRemind);
            }
            this.rpRemind.DataSource = dtRemind;
            this.rpRemind.DataBind();
            dtRemind.Dispose();
        }

        private int PayLoad(DataTable dtRemind, string ProjectCode)
        {
            //�������������
            PaymentStrategyBuilder sb = new PaymentStrategyBuilder("V_Payment");
            //ĳ����Ŀ
            sb.AddStrategy(new Strategy(PaymentStrategyName.ProjectCode, ProjectCode));

            //����֮ǰ
            ArrayList ar = new ArrayList();
            ar.Add("1900-1-1");
            ar.Add(DateTime.Now.ToShortDateString());
            sb.AddStrategy(new Strategy(PaymentStrategyName.PayDate, ar));

            //δ�����
            sb.AddStrategy(new Strategy(DAL.QueryStrategy.PaymentStrategyName.NotPayout));

            //״̬������˵�
            string status = "1";
            sb.AddStrategy(new Strategy(PaymentStrategyName.Status, status));

            //��ǰ�û�
            //sb.AddStrategy( new Strategy( PaymentStrategyName.ApplyPerson,user.UserCode));
            //Ȩ��
            ArrayList arA = new ArrayList();
            arA.Add(user.UserCode);
            arA.Add(user.BuildStationCodes());
            sb.AddStrategy(new Strategy(DAL.QueryStrategy.PaymentStrategyName.AccessRange, arA));

            string sql = sb.BuildMainQueryString();

            QueryAgent qa = new QueryAgent();
            EntityData entity = qa.FillEntityData("Payment", sql);
            qa.Dispose();

            if (entity.Tables[0].Rows.Count != 0)
            {
                DataRow drRemind = dtRemind.NewRow();
                drRemind["Type"] = "&nbsp;&nbsp;[�����]";
                //���ݲ�����ProjectCode/ACT=1Ӧ����/PayLoad=1�����
                drRemind["Url"] = "Finance/PaymentList.aspx?Act=1&Pay_Load=1&ProjectCode=" + ProjectCode;
                drRemind["Title"] = entity.Tables[0].Rows.Count.ToString() + "��";
                dtRemind.Rows.Add(drRemind);
            }
            return entity.Tables[0].Rows.Count;
        }
        private int BiddingLoad(DataTable dtRemind, string ProjectCode)
        {
            BLL.Bidding bidding = new BLL.Bidding();
            ArrayList ar = new ArrayList();
            ar.Add("1900-1-1");
            ar.Add(DateTime.Now.ToShortDateString());
            bidding.ArrangedDateEx = ar;
            bidding.State = "0";
            bidding.ProjectCode = ProjectCode;

            if (bidding.GetBiddings().Rows.Count != 0)
            {
                DataRow drRemind = dtRemind.NewRow();
                drRemind["Type"] = "&nbsp;&nbsp;[��Ͷ�굽��]";
                drRemind["Url"] = "BiddingManage/BiddingQuery.aspx?State=0&ProjectCode=" + ProjectCode;
                drRemind["Title"] = bidding.GetBiddings().Rows.Count.ToString() + "��";
                dtRemind.Rows.Add(drRemind);
            }
            return bidding.GetBiddings().Rows.Count;
        }
        private int ContractLoad(DataTable dtRemind, string ProjectCode)
        {
            BLL.Bidding bidding = new BLL.Bidding();
            ArrayList ar = new ArrayList();
            ar.Add("1900-1-1");
            ar.Add(DateTime.Now.ToShortDateString());
            bidding.ConfirmDateEx = ar;
            bidding.State = "4";
            bidding.ProjectCode = ProjectCode;

            if (bidding.GetBiddings().Rows.Count != 0)
            {
                DataRow drRemind = dtRemind.NewRow();
                drRemind["Type"] = "&nbsp;&nbsp;[��ͬǩԼ����]";
                drRemind["Url"] = "BiddingManage/BiddingQuery.aspx?State=4&ProjectCode=" + ProjectCode;
                drRemind["Title"] = bidding.GetBiddings().Rows.Count.ToString() + "��";
                dtRemind.Rows.Add(drRemind);
            }
            return bidding.GetBiddings().Rows.Count;
        }

        //private int WorkFlowLoadRemind(DataTable dtRemind, string Status)
        //{
        //    // ***********************���̴����*****************************
        //    EntityData entity = DAL.EntityDAO.WorkFlowDAO.GetAllWorkFlowProcedure();
        //    int iCount = entity.CurrentTable.Rows.Count;
        //    int LoadCount = 0;
        //    List<string> ProcedureNameList = new List<string>();
        //    for (int i = 0; i < iCount; i++)
        //    {
        //        entity.SetCurrentRow(i);
        //        string ProcedureName = entity.GetString("ProcedureName");
        //        if (!ProcedureNameList.Contains(ProcedureName))
        //        {

        //            WorkFlowActStrategyBuilder sb = new WorkFlowActStrategyBuilder();
        //            sb.AddStrategy(new Strategy(WorkFlowActStrategyName.Status, Status));
        //            sb.AddStrategy(new Strategy(WorkFlowActStrategyName.InActUser, user.UserCode));
        //            sb.AddStrategy(new Strategy(WorkFlowActStrategyName.ProcedureCodeIn, BLL.WorkFlowRule.GetProcedureCodeListByName(ProcedureName))); // ��������
        //            string sql = sb.BuildMainQueryString();
        //            QueryAgent qa = new QueryAgent();
        //            EntityData entity1 = qa.FillEntityData("WorkFlowAct", sql);
        //            qa.Dispose();

        //            int tempcount = 0;
        //            for (int m = 0; m < entity1.CurrentTable.Rows.Count; m++)
        //            {
        //                if (RmsPM.BLL.WorkFlowRule.GetCheckExecdableDate((DateTime)entity1.CurrentTable.Rows[m]["FromDate"], DateTime.Parse("1900-1-1")))
        //                    tempcount++;
        //            }

        //            if (tempcount > 0)
        //            {
        //                string FlowStatusName = "";
        //                if (Status == "'Begin'")
        //                    FlowStatusName = "����";
        //                else if (Status == "'DealWith'")
        //                    FlowStatusName = "�ڰ�";
        //                DataRow drRemind = dtRemind.NewRow();
        //                drRemind["Type"] = "&nbsp;&nbsp;" + entity.GetString("Description") + "<font color=\"red\">(" + FlowStatusName + "����)</font>";
        //                drRemind["Title"] = tempcount.ToString() + "��";
        //                if (Status == "'Begin'")
        //                    drRemind["Url"] = "WorkFlowContral/WorkFlowReceiveBox.aspx?ProcedureName=" + Server.UrlEncode(ProcedureName);
        //                else if (Status == "'DealWith'")
        //                    drRemind["Url"] = "WorkFlowContral/workflowinbox.aspx?ProcedureName=" + Server.UrlEncode(ProcedureName);
        //                drRemind["RemindTime"] = System.DBNull.Value;
        //                dtRemind.Rows.Add(drRemind);
        //                LoadCount++;
        //            }
        //            entity1.Dispose();
        //            ProcedureNameList.Add(ProcedureName);
        //        }
        //    }
        //    entity.Dispose();
        //    return LoadCount;
        //}


        #endregion
    }
}
