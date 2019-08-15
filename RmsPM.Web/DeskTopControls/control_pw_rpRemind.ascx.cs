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
                LoadRemind();
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "获取首页数据失败");
            }
        }
        #region 提醒的处理
        private void LoadRemind()
        {

            //创建提醒数据表
            DataTable dtRemind = new DataTable();
            dtRemind.Columns.Add("Type", System.Type.GetType("System.String"));
            dtRemind.Columns.Add("Title", System.Type.GetType("System.String"));
            dtRemind.Columns.Add("Url", System.Type.GetType("System.String"));
            dtRemind.Columns.Add("RemindTime", System.Type.GetType("System.String"));
            dtRemind.Columns.Add("RemindRand", System.Type.GetType("System.String"));//提醒级别


            DataView dv = new DataView(user.m_EntityDataAccessProject.CurrentTable, "", "ProjectName", DataViewRowState.CurrentRows);

            //DataRow drRemindflow = dtRemind.NewRow();
            //drRemindflow["Type"] = ">>流程超期";
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
                //招投标到期 New do it
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
            //付款超期提醒条件
            PaymentStrategyBuilder sb = new PaymentStrategyBuilder("V_Payment");
            //某个项目
            sb.AddStrategy(new Strategy(PaymentStrategyName.ProjectCode, ProjectCode));

            //当天之前
            ArrayList ar = new ArrayList();
            ar.Add("1900-1-1");
            ar.Add(DateTime.Now.ToShortDateString());
            sb.AddStrategy(new Strategy(PaymentStrategyName.PayDate, ar));

            //未付清的
            sb.AddStrategy(new Strategy(DAL.QueryStrategy.PaymentStrategyName.NotPayout));

            //状态是已审核的
            string status = "1";
            sb.AddStrategy(new Strategy(PaymentStrategyName.Status, status));

            //当前用户
            //sb.AddStrategy( new Strategy( PaymentStrategyName.ApplyPerson,user.UserCode));
            //权限
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
                drRemind["Type"] = "&nbsp;&nbsp;[付款到期]";
                //传递参数：ProjectCode/ACT=1应付款/PayLoad=1付款到期
                drRemind["Url"] = "Finance/PaymentList.aspx?Act=1&Pay_Load=1&ProjectCode=" + ProjectCode;
                drRemind["Title"] = entity.Tables[0].Rows.Count.ToString() + "个";
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
                drRemind["Type"] = "&nbsp;&nbsp;[招投标到期]";
                drRemind["Url"] = "BiddingManage/BiddingQuery.aspx?State=0&ProjectCode=" + ProjectCode;
                drRemind["Title"] = bidding.GetBiddings().Rows.Count.ToString() + "个";
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
                drRemind["Type"] = "&nbsp;&nbsp;[合同签约到期]";
                drRemind["Url"] = "BiddingManage/BiddingQuery.aspx?State=4&ProjectCode=" + ProjectCode;
                drRemind["Title"] = bidding.GetBiddings().Rows.Count.ToString() + "个";
                dtRemind.Rows.Add(drRemind);
            }
            return bidding.GetBiddings().Rows.Count;
        }

        //private int WorkFlowLoadRemind(DataTable dtRemind, string Status)
        //{
        //    // ***********************流程待审核*****************************
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
        //            sb.AddStrategy(new Strategy(WorkFlowActStrategyName.ProcedureCodeIn, BLL.WorkFlowRule.GetProcedureCodeListByName(ProcedureName))); // 各种流程
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
        //                    FlowStatusName = "待办";
        //                else if (Status == "'DealWith'")
        //                    FlowStatusName = "在办";
        //                DataRow drRemind = dtRemind.NewRow();
        //                drRemind["Type"] = "&nbsp;&nbsp;" + entity.GetString("Description") + "<font color=\"red\">(" + FlowStatusName + "超期)</font>";
        //                drRemind["Title"] = tempcount.ToString() + "个";
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
