namespace RmsPM.Web.DeskTopControl
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
    ///		Control_rpAudit ��ժҪ˵����
    /// </summary>
    public partial class Control_rpAudit : Components.BaseControl
    {
        protected int intListAuditNum;

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
                LoadAudit();
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "��ȡ��ҳ����ʧ��");
            }
        }

        #region ��ͬ�������õ����
        private void LoadAudit()
        {
            DataTable dtAudit = new DataTable();
            dtAudit.Columns.Add("Type", System.Type.GetType("System.String"));
            dtAudit.Columns.Add("Title", System.Type.GetType("System.String"));
            dtAudit.Columns.Add("Url", System.Type.GetType("System.String"));
            dtAudit.Columns.Add("AuditTime", System.Type.GetType("System.String"));
            dtAudit.Columns.Add("ProjectCode", System.Type.GetType("System.String"));
            DataRow drAudit;

            // �Ƿ��к�ͬ���Ȩ��
            bool isContractRight = user.HasOperationRight("050105") && user.HasOperationRight("0802");
            if (isContractRight)
            {
                // ��ͬǰn������
                int intContractNum = 6;
                DataTable dtContract = this.GetContact(intContractNum);
                //DataGrid1.DataSource = dtContract;
                //DataGrid1.DataBind();
                //			for(int i=0;i<dtContract.Rows.Count;i++)
                //			{
                //				drAudit = dtAudit.NewRow();
                //				drAudit["Type"] = "��ͬ���";
                //				drAudit["Title"] = dtContract.Rows[i]["ContractName"].ToString();
                //				drAudit["Url"] = "Contract/ContractInfo.aspx?ProjectCode="+projectCode+"&ContractCode="+dtContract.Rows[i]["ContractCode"].ToString();
                //				drAudit["AuditTime"] = dtContract.Rows[i]["CreateDate"].ToString();
                //				dtAudit.Rows.Add(drAudit);
                //			}
                if (dtContract.Rows.Count > 0)
                {
                    //Response.Write(dtContract.Rows.Count);
                    drAudit = dtAudit.NewRow();
                    drAudit["Type"] = "��ͬ���";
                    drAudit["Title"] = dtContract.Rows.Count;
                    drAudit["Url"] = "Contract/Contract.aspx?Status=1";//&ProjectCode="+dtContract.Rows[0]["ProjectCode"].ToString();
                    drAudit["AuditTime"] = dtContract.Rows[0]["CreateDate"].ToString();
                    dtAudit.Rows.Add(drAudit);
                }
            }

            // �Ƿ���������Ȩ��
            bool isPaymentRight = user.HasOperationRight("060105") && user.HasOperationRight("0802");
            if (isPaymentRight)
            {
                // ���ǰn������
                int intPaymentNum = 6;
                DataTable dtPayment = this.GetPayment(intPaymentNum);
                //			for(int i=0;i<dtPayment.Rows.Count;i++)
                //			{
                //				drAudit = dtAudit.NewRow();
                //				drAudit["Type"] = "������";
                //				drAudit["Title"] = dtPayment.Rows[i]["PaymentCode"].ToString();
                //				drAudit["Url"] = "Finance/PaymentInfo.aspx?PaymentCode="+dtPayment.Rows[i]["PaymentCode"].ToString();
                //				drAudit["AuditTime"] = dtPayment.Rows[i]["ApplyDate"].ToString();
                //				dtAudit.Rows.Add(drAudit);
                //			}
                if (dtPayment.Rows.Count > 0)
                {
                    drAudit = dtAudit.NewRow();
                    drAudit["Type"] = "������";
                    drAudit["Title"] = dtPayment.Rows.Count;
                    drAudit["Url"] = "Finance/PaymentList.aspx?Status=0";//&ProjectCode="+dtPayment.Rows[0]["ProjectCode"].ToString();
                    drAudit["AuditTime"] = dtPayment.Rows[0]["ApplyDate"].ToString();
                    dtAudit.Rows.Add(drAudit);
                }
            }


            bool isDynamicCostRight = user.HasOperationRight("040403");
            if (isDynamicCostRight)
            {
                // ����ǰn������ ,�ڶ�̬���ã�����̬�����еĴ����
                int intDynamicCostNum = 6;
                DataTable dtDynamicCost = this.GetDynamicCost(intDynamicCostNum);
                //			for(int i=0;i<dtDynamicCost.Rows.Count;i++)
                //			{
                //				drAudit = dtAudit.NewRow();
                //				drAudit["Type"] = "�������";
                //				drAudit["Title"] = dtDynamicCost.Rows[i]["BudgetName"].ToString();
                //				drAudit["Url"] = "Cost/DynamicApplyInfo.aspx?ProjectCode="+projectCode+"&BudgetCode="+dtDynamicCost.Rows[i]["BudgetCode"].ToString();
                //				drAudit["AuditTime"] = dtDynamicCost.Rows[i]["MakeDate"].ToString();
                //				dtAudit.Rows.Add(drAudit);
                //			}
                if (dtDynamicCost.Rows.Count > 0)
                {
                    drAudit = dtAudit.NewRow();
                    drAudit["Type"] = "��̬�������";
                    drAudit["Title"] = dtDynamicCost.Rows.Count;
                    drAudit["Url"] = "Cost/DynamicCostApplyList.aspx?ProjectCode=" + dtDynamicCost.Rows[0]["ProjectCode"].ToString();
                    drAudit["AuditTime"] = dtDynamicCost.Rows[0]["MakeDate"].ToString();
                    dtAudit.Rows.Add(drAudit);
                }
            }

            // ȡ�����̴���ˣ����ڰ��䵥�� unm add 2005.4.4
            // ***********************���̴����*****************************
            EntityData entity = DAL.EntityDAO.WorkFlowDAO.GetAllWorkFlowProcedure();
            int iCount = entity.CurrentTable.Rows.Count;
            List<string> ProcedureNameList = new List<string>();
            for (int i = 0; i < iCount; i++)
            {
                entity.SetCurrentRow(i);
                // ȡ��ÿ�����ĸ���
                string ProcedureName = entity.GetString("ProcedureName");
                if (!ProcedureNameList.Contains(ProcedureName))
                {

                    WorkFlowActStrategyBuilder sb = new WorkFlowActStrategyBuilder();
                    sb.AddStrategy(new Strategy(WorkFlowActStrategyName.StatusBegin, "Begin")); // �ռ���
                    sb.AddStrategy(new Strategy(WorkFlowActStrategyName.InActUser, user.UserCode));
                    sb.AddStrategy(new Strategy(WorkFlowActStrategyName.ProcedureCodeIn, BLL.WorkFlowRule.GetProcedureCodeListByName(ProcedureName))); // ��������
                    sb.AddStrategy(new Strategy(WorkFlowActStrategyName.ActMeetOrder, ""));
                    string sql = sb.BuildMainQueryString();
                    QueryAgent qa = new QueryAgent();
                    EntityData entity1 = qa.FillEntityData("WorkFlowAct", sql);
                    qa.Dispose();

                    if (entity1.CurrentTable.Rows.Count > 0)
                    {
                        drAudit = dtAudit.NewRow();
                        drAudit["Type"] = entity.GetString("Description");
                        drAudit["Title"] = entity1.CurrentTable.Rows.Count;
                        drAudit["Url"] = "WorkFlowContral/WorkFlowReceiveBox.aspx?ProcedureName=" + Server.UrlEncode(ProcedureName); //��ʱ����?ProcedureCode=" + entity.GetString("ProcedureCode");
                        drAudit["AuditTime"] = System.DBNull.Value;
                        dtAudit.Rows.Add(drAudit);
                    }
                    entity1.Dispose();
                    ProcedureNameList.Add(ProcedureName);
                }
            }
            entity.Dispose();

            // ***********************���̴����*****************************


            DataView dv = new DataView(dtAudit);
            dv.Sort = " AuditTime desc ";
            // ȡ��ȫ����˵�ǰn��
            DataTable dtTmp = new DataTable();
            dtTmp = dtAudit.Clone();
            //int j=0;
            //foreach(DataRowView drv in dv)
            //{
            //drAudit = dtTmp.NewRow();
            ///drAudit.ItemArray = drv.Row.ItemArray;
            //dtTmp.Rows.Add(drAudit);

            //j++;
            //if(j>=this.intListAuditNum) break;
            //}
            this.rpAudit.DataSource = dtAudit;
            this.rpAudit.DataBind();

            ///////////////////////////////////////////////////////////////////////////////////



            // ***********************���̴����*****************************


            /*DataView dvclm = new DataView(dtAuditclm);
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
            //this.Repeater1.DataSource = dtTmpclm;
            //this.Repeater1.DataBind();*/

            ///////////////////////////////////////////////////////////////////////////////////
        }

        /// <summary>
        /// ȡ�����µĺ�ͬ
        /// </summary>
        /// <returns></returns>
        private DataTable GetContact(int num)
        {
            RmsPM.DAL.QueryStrategy.ContractStrategyBuilder CSB = new RmsPM.DAL.QueryStrategy.ContractStrategyBuilder();
            string ud_sProjectCode = Request["ProjectCode"] + "";

            ArrayList arA = new ArrayList();
            arA.Add("050105");
            arA.Add(base.user.UserCode);
            arA.Add(base.user.BuildStationCodes());
            CSB.AddStrategy(new Strategy(DAL.QueryStrategy.ContractStrategyName.Status, "1"));
            if (ud_sProjectCode != "")
            {
                CSB.AddStrategy(new Strategy(ContractStrategyName.ProjectCode, ud_sProjectCode));
            }
            CSB.AddStrategy(new Strategy(DAL.QueryStrategy.ContractStrategyName.AccessRange, arA));
            CSB.AddOrder("ContractDate", false);
            string sql = CSB.BuildMainQueryString();
            QueryAgent qa = new QueryAgent();
            //qa.SetTopNumber(num);
            EntityData entity = qa.FillEntityData("Contract", sql);
            DataTable dt = entity.CurrentTable;
            qa.Dispose();
            return dt;
        }

        /// <summary>
        /// ȡ�����µ����
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        private DataTable GetPayment(int num)
        {
            PaymentStrategyBuilder sb = new PaymentStrategyBuilder("V_Payment");
            ArrayList arA = new ArrayList();
            //arA.Add("060102");
            arA.Add(base.user.UserCode);
            arA.Add(base.user.BuildStationCodes());
            sb.AddStrategy(new Strategy(DAL.QueryStrategy.PaymentStrategyName.AccessRange, arA));
            sb.AddStrategy(new Strategy(PaymentStrategyName.Status, "0")); // 0Ϊ����
            sb.AddOrder("ApplyDate", false);
            string sql = sb.BuildMainQueryString();
            QueryAgent qa = new QueryAgent();
            //qa.SetTopNumber(num);
            EntityData entity = qa.FillEntityData("V_Payment", sql);
            DataTable dt = entity.CurrentTable;
            qa.Dispose();
            return dt;
        }

        /// <summary>
        /// ȡ�����µĶ�̬����
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        private DataTable GetDynamicCost(int num)
        {
            BudgetStrategyBuilder sb = new BudgetStrategyBuilder();
            ArrayList arA = new ArrayList();
            arA.Add("040403");
            arA.Add(base.user.UserCode);
            arA.Add(base.user.BuildStationCodes());
            sb.AddStrategy(new Strategy(BudgetStrategyName.AccessRange, arA));
            sb.AddStrategy(new Strategy(BudgetStrategyName.IsDynamic, "2"));	//��̬����
            sb.AddStrategy(new Strategy(BudgetStrategyName.Flag, "1"));		//�������״̬
            sb.AddOrder("MakeDate", false);
            string sql = sb.BuildMainQueryString();
            QueryAgent qa = new QueryAgent();
            //qa.SetTopNumber(num);
            EntityData budgets = qa.FillEntityData("Budget", sql);
            DataTable dt = budgets.CurrentTable;
            qa.Dispose();
            return dt;
        }

        #endregion
    }
}
