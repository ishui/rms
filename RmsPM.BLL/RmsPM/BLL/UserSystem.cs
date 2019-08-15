namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.QueryStrategy;

    public class UserSystem
    {
        public static DataView AddToUserMessage(DataView dv)
        {
            dv.Table.Columns.Add("stationNameHtml", Type.GetType("System.String"));
            foreach (DataRowView view in dv)
            {
                view["stationNameHtml"] = SystemRule.GetUserStationNameHtml(view["UserCode"].ToString());
            }
            return dv;
        }

        public static string BinUserSelectSql(string Name, string textValue, ref int i)
        {
            if ((textValue != "") & (i == 0))
            {
                i++;
                return (Name + " like '%" + textValue + "%'");
            }
            if ((textValue != "") & (i > 0))
            {
                i++;
                return (" and " + Name + " like '%" + textValue + "%'");
            }
            return "";
        }

        public static DataView GetUserMessage(string projectCode)
        {
            DataView view2;
            try
            {
                UserStrategyBuilder builder = new UserStrategyBuilder();
                if (projectCode != "")
                {
                    builder.AddStrategy(new Strategy(UserStrategyName.ProjectCode, projectCode));
                }
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                DataView dv = agent.ExecSqlForDataSet(queryString).Tables[0].DefaultView;
                agent.Dispose();
                view2 = AddToUserMessage(dv);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return view2;
        }

        public static DataView SearchFromUserMessageResult(DataView dv, string sql)
        {
            DataView view;
            try
            {
                if (sql != "")
                {
                    return new DataView(dv.Table, sql, "UserID", DataViewRowState.CurrentRows);
                }
                view = dv;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return view;
        }

        public static DataView SortUserMessage(DataView dv, string sortsql)
        {
            DataView view;
            try
            {
                if (sortsql != "")
                {
                    dv.Sort = sortsql;
                    return dv;
                }
                view = dv;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return view;
        }
    }
}

