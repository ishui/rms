namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.QueryStrategy;

    public class EmailTimer
    {
        private static string GetUserEmailContent(string UserCode, string url, string contentflag)
        {
            string text4;
            try
            {
                string text5;
                string text = "";
                WorkFlowActStrategyBuilder builder = new WorkFlowActStrategyBuilder();
                if (contentflag == "1")
                {
                    builder.AddStrategy(new Strategy(WorkFlowActStrategyName.StatusBeginAndDealWith));
                }
                else
                {
                    builder.AddStrategy(new Strategy(WorkFlowActStrategyName.StatusBegin, "Begin"));
                }
                builder.AddStrategy(new Strategy(WorkFlowActStrategyName.InActUser, UserCode));
                builder.AddStrategy(new Strategy(WorkFlowActStrategyName.ActMeetOrder, ""));
                string queryString = builder.BuildMainQueryString();
                string text3 = "FromDate desc";
                if (text3 != "")
                {
                    queryString = queryString + " order by " + text3;
                }
                text = SystemRule.GetUserName(UserCode) + ",您好！<br/>  以下为您的待处理事宜：<br/>";
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("WorkFlowAct", queryString);
                if (data.CurrentTable.Rows.Count != 0)
                {
                    text = text + "<table cellspacing=\"0\" cellpadding=\"0\" border=\"1\" width=\"100%\" style=\"font-size: 13px;color:Black;background-color:LightGoldenrodYellow;border-color:Tan;border-width:1px;border-style:solid;border-collapse:collapse;\"><tr style=\"background-color:Tan;font-weight:bold;font-size: 13px;height:23pt;\"><td align=\"center\">流水号</td><td align=\"center\">项目</td><td nowrap=\"nowrap\"  align=\"center\">流程名称</td><td align=\"center\" nowrap=\"nowrap\">任 务</td><td align=\"center\">主题</td><td align=\"center\" nowrap=\"nowrap\">发件人</a></td><td align=\"center\" nowrap=\"nowrap\">发件日期</a></td><td align=\"center\" nowrap=\"nowrap\">手送资料</a></td></tr>";
                    foreach (DataRow row in data.CurrentTable.Select())
                    {
                        text5 = text;
                        text = text5 + "<tr style=\"font-size: 13px;height:23pt;\"><td>" + WorkFlowRule.GetWorkFlowNumber(row["CaseCode"].ToString()) + "</td><td><span>" + WorkFlowRule.GetWorkFlowCaseProjectName(row["CaseCode"].ToString()) + "</span></td><td>" + row["ProcedureName"].ToString() + "</td><td nowrap=\"nowrap\">" + row["ToTaskName"].ToString() + "</td><td>" + WorkFlowRule.GetWorkFlowCaseTitle(row["CaseCode"].ToString()) + "</td><td nowrap=\"nowrap\">" + SystemRule.GetUserName(row["FromUserCode"].ToString()) + "</td><td nowrap=\"nowrap\">" + WorkFlowRule.GetFormatExcedableDate((DateTime) row["FromDate"]) + "</td><td>" + WorkFlowRule.GetWorkFlowHandmade(row["CaseCode"].ToString()) + "</td></tr>";
                    }
                }
                else
                {
                    text = "";
                }
                data.Clear();
                object obj2 = ("Select B.Title,B.BiddingRemark1,BE.EmitDate,BE.EndDate,BE.PrejudicationDate,BE.State" + " From BiddingEmit BE") + " Inner Join Bidding B on BE.BiddingCode=B.BiddingCode" + " Inner Join BiddingOpen BO On BE.BiddingEmitCode=BO.BiddingEmitCode";
                queryString = string.Concat(new object[] { obj2, " Where BO.OpenerCode='", UserCode, "' And BE.state=0 And BE.PrejudicationDate Between DateAdd(day,1,'", DateTime.Today, "') And 'DateAdd(day,2,'", DateTime.Today, "')" });
                data = agent.FillEntityData("BiddingOpen", queryString);
                if (data.CurrentTable.Rows.Count != 0)
                {
                    text = text + "<table cellspacing=\"0\" cellpadding=\"0\" border=\"1\" width=\"100%\" style=\"font-size: 13px;color:Black;background-color:LightGoldenrodYellow;border-color:Tan;border-width:1px;border-style:solid;border-collapse:collapse;\"><tr style=\"background-color:Tan;font-weight:bold;font-size: 13px;height:23pt;\"><td align=\"center\">拟定标段 </td><td align=\"center\">承办部门</td><td nowrap=\"nowrap\"  align=\"center\">发标日期</td><td align=\"center\" nowrap=\"nowrap\">截标日期</td><td align=\"center\">定标日期</td><td align=\"center\" nowrap=\"nowrap\">当前状态</a></td></tr>";
                    foreach (DataRow row in data.CurrentTable.Select())
                    {
                        text5 = text;
                        text = text5 + "<tr style=\"font-size: 13px;height:23pt;\"><td>" + row["Title"].ToString() + "</td><td><span>" + row["BiddingRemark1"].ToString() + "</span></td><td>" + row["EmitDate"].ToString() + "</td><td nowrap=\"nowrap\">" + row["EndDate"].ToString() + "</td><td>" + row["PrejudicationDate"].ToString() + "</td><td nowrap=\"nowrap\">" + row["State"].ToString() + "</td></tr>";
                    }
                }
                else
                {
                    text = "";
                }
                agent.Dispose();
                data.Dispose();
                if (text != "")
                {
                    text5 = text;
                    text = text5 + "</table> <br/> 请您登录“<a href=\"" + url + "\" target=\"_blank\">" + url + "</a>”进行处理，谢谢。";
                }
                text4 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text4;
        }

        public static void SendEmail(string url, string contentflag)
        {
            try
            {
                DataView userMessage = UserSystem.GetUserMessage("");
                foreach (DataRowView view2 in userMessage)
                {
                    if (view2["Status"].ToString() != "1")
                    {
                        string text = GetUserEmailContent(view2["UserCode"].ToString(), url, contentflag);
                        if ((text != "") && (view2["MailBox"].ToString() != ""))
                        {
                            MailRule rule = new MailRule();
                            rule.Title = "项目管理系统待办业务提醒(日期：" + DateTime.Now.ToShortDateString() + ")";
                            rule.Body = text;
                            rule.ToMail = view2["MailBox"].ToString();
                            rule.sendMail();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}

