namespace RmsPM.BLL
{
    using System;
    using Rms.ORMap;
    using RmsPM.DAL.QueryStrategy;

    public sealed class WorkFlowActUserSelect
    {
        private static string BuildSqlString(string ActCode)
        {
            WorkFlowActUserStrategyBuilder builder = new WorkFlowActUserStrategyBuilder();
            builder.AddStrategy(new Strategy(WorkFlowActUserStrategyName.ActCode, ActCode));
            return builder.BuildMainQueryString();
        }

        public static string GetWorkFlowActUser(string ActCode)
        {
            string text = "";
            string queryString = BuildSqlString(ActCode);
            QueryAgent agent = new QueryAgent();
            EntityData data = agent.FillEntityData("WorkFlowActUser", queryString);
            agent.Dispose();
            for (int i = 0; i < data.Tables[0].Rows.Count; i++)
            {
                text = text + SystemRule.GetUserName(data.Tables[0].Rows[i][3].ToString());
            }
            data.Dispose();
            return text;
        }

        public static string GetWorkFlowActUser(string ActCode, string ProjectCode)
        {
            string text = "";
            string queryString = BuildSqlString(ActCode);
            QueryAgent agent = new QueryAgent();
            EntityData data = agent.FillEntityData("WorkFlowActUser", queryString);
            agent.Dispose();
            for (int i = 0; i < data.Tables[0].Rows.Count; i++)
            {
                text = text + SystemRule.GetUserNameByProjectCode(data.Tables[0].Rows[i][3].ToString(), ProjectCode, null);
            }
            data.Dispose();
            return text;
        }
    }
}

