namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.QueryStrategy;

    public class StyleOperation
    {
        public static void DeleUserControl(object userid)
        {
            EntityData data = new EntityData("UserDesktop");
            try
            {
                try
                {
                    UserDesktopStrategyBuilder.DeleteUserDesktop(userid);
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }
            finally
            {
                data.Dispose();
            }
        }

        public static DataSet GetLeftSytleByID(object sytleid)
        {
            DataSet set2;
            V_StyleInfoStrategyBuilder builder = new V_StyleInfoStrategyBuilder();
            builder.AddStrategy(new Strategy(V_StyleInfoStrategyName.StyleID, sytleid.ToString()));
            builder.AddStrategy(new Strategy(V_StyleInfoStrategyName.TableID, "LeftPane"));
            string queryString = builder.BuildMainQueryString() + " order by ControlOrder";
            QueryAgent agent = new QueryAgent();
            try
            {
                set2 = agent.ExecSqlForDataSet(queryString);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                agent.Dispose();
            }
            return set2;
        }

        public static DataSet GetRightSytleByID(object sytleid)
        {
            DataSet set2;
            V_StyleInfoStrategyBuilder builder = new V_StyleInfoStrategyBuilder();
            builder.AddStrategy(new Strategy(V_StyleInfoStrategyName.StyleID, sytleid.ToString()));
            builder.AddStrategy(new Strategy(V_StyleInfoStrategyName.TableID, "RightPane"));
            string queryString = builder.BuildMainQueryString() + " order by ControlOrder";
            QueryAgent agent = new QueryAgent();
            try
            {
                set2 = agent.ExecSqlForDataSet(queryString);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                agent.Dispose();
            }
            return set2;
        }

        public static DataTable GetStationConfig(string station, string userID)
        {
            DataRow row;
            int num = 0;
            if (station == "")
            {
                throw new Exception("你没有岗位,无权查看桌面");
            }
            string[] textArray = station.Split(new char[] { ',' });
            DataTable stationStyle = GetStationStyle(textArray[0]);
            DataView view = new DataView(stationStyle);
            int count = stationStyle.Rows.Count;
            if (textArray.Length <= 2)
            {
                return ShowUserControl(new DataView(stationStyle), userID);
            }
            DataTable table = stationStyle.Clone();
            int length = textArray.Length;
            table = stationStyle.Clone();
            foreach (DataRowView view2 in view)
            {
                row = table.NewRow();
                row.ItemArray = view2.Row.ItemArray;
                table.Rows.Add(row);
            }
            for (int i = 1; i < (length - 1); i++)
            {
                DataView view3 = new DataView(GetStationStyle(textArray[i]));
                foreach (DataRowView view4 in view3)
                {
                    num = 0;
                    foreach (DataRowView view2 in view)
                    {
                        num++;
                        if (view4["ControlID"].Equals(view2["ControlID"]))
                        {
                            break;
                        }
                        if (num >= count)
                        {
                            row = table.NewRow();
                            row.ItemArray = view4.Row.ItemArray;
                            table.Rows.Add(row);
                            num = 0;
                        }
                    }
                }
            }
            DataView dv = new DataView(table);
            dv.Sort = "ControlOrder Asc";
            return ShowUserControl(dv, userID);
        }

        public static DataSet GetStationList()
        {
            DataSet set;
            EntityData allStation = new EntityData();
            try
            {
                allStation = StationDesktop.GetAllStation();
                set = allStation;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                allStation.Dispose();
            }
            return set;
        }

        public static DataTable GetStationStyle(string stationID)
        {
            DataTable table2;
            V_DesktopStrategyBuilder builder = new V_DesktopStrategyBuilder();
            builder.AddStrategy(new Strategy(V_DesktopStrategyName.StationID, stationID));
            string queryString = builder.BuildMainQueryString() + " order by ControlOrder";
            QueryAgent agent = new QueryAgent();
            try
            {
                DataTable stationConfig = agent.ExecSqlForDataSet(queryString).Tables[0];
                if (stationConfig.Rows.Count <= 0)
                {
                    stationConfig = GetStationConfig("-1", "-2");
                }
                table2 = stationConfig;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                agent.Dispose();
            }
            return table2;
        }

        public static DataSet GetStyleList()
        {
            DataSet set;
            EntityData allStyleList = new EntityData();
            try
            {
                allStyleList = StyleList.GetAllStyleList();
                set = allStyleList;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                allStyleList.Dispose();
            }
            return set;
        }

        public static DataSet GetUserControl(object userid)
        {
            DataSet userControlByID;
            QueryAgent agent = new QueryAgent();
            try
            {
                userControlByID = UserDesktopStrategyBuilder.GetUserControlByID(userid);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                agent.Dispose();
            }
            return userControlByID;
        }

        public static DataView GetUserControl(string stationID, object userid)
        {
            DataSet userControl = GetUserControl(userid);
            DataView view = new DataView(GetStationConfig(stationID, "-2"));
            view.Table.Columns.Add("IsShow", Type.GetType("System.String"));
            int num = 0;
            int count = view.Table.Rows.Count;
            if (userControl.Tables[0].Rows.Count <= 0)
            {
                foreach (DataRowView view2 in view)
                {
                    view2["IsShow"] = "true";
                }
                return view;
            }
            foreach (DataRow row in userControl.Tables[0].Rows)
            {
                num = 0;
                foreach (DataRowView view2 in view)
                {
                    num++;
                    if (row["ControlID"].Equals(view2["ControlID"]))
                    {
                        view2["IsShow"] = "true";
                        break;
                    }
                    if (num == count)
                    {
                        view2["IsShow"] = "false";
                        break;
                    }
                }
            }
            return view;
        }

        private static void InsertStationDesktop(object stationId, object styleId)
        {
            EntityData entity = new EntityData("StationDesktop");
            try
            {
                try
                {
                    DataRow newRecord = entity.GetNewRecord();
                    newRecord["StationID"] = stationId;
                    newRecord["StyleID"] = styleId;
                    entity.AddNewRecord(newRecord);
                    StationDesktop.InsertStationDesktop(entity);
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }
            finally
            {
                entity.Dispose();
            }
        }

        public static void InsertUserControl(object userid, object controlID)
        {
            EntityData entity = new EntityData("UserDesktop");
            try
            {
                try
                {
                    DataRow newRecord = entity.GetNewRecord();
                    newRecord["UserID"] = userid;
                    newRecord["ControlID"] = controlID;
                    entity.AddNewRecord(newRecord);
                    UserDesktopStrategyBuilder.InsertUserDesktop(entity);
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }
            finally
            {
                entity.Dispose();
            }
        }

        private static DataSet IsStationStyle(object stationId)
        {
            DataSet set2;
            StationDesktopStrategyBuilder builder = new StationDesktopStrategyBuilder();
            builder.AddStrategy(new Strategy(StationDesktopStrategyName.StationID, stationId.ToString()));
            string queryString = builder.BuildMainQueryString();
            QueryAgent agent = new QueryAgent();
            try
            {
                set2 = agent.ExecSqlForDataSet(queryString);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                agent.Dispose();
            }
            return set2;
        }

        public static void SetStationStyle(object stationId, object styleId)
        {
            if (IsStationStyle(stationId).Tables[0].Rows.Count > 0)
            {
                UpdateStationDesktop(stationId, styleId);
            }
            else
            {
                InsertStationDesktop(stationId, styleId);
            }
        }

        public static DataTable ShowUserControl(DataView dv, string userID)
        {
            DataSet userControl = GetUserControl(userID);
            if (userControl.Tables[0].Rows.Count <= 0)
            {
                return dv.Table;
            }
            DataTable table = dv.Table.Clone();
            foreach (DataRow row in userControl.Tables[0].Rows)
            {
                foreach (DataRowView view in dv)
                {
                    if (row["ControlID"].Equals(view["ControlID"]))
                    {
                        DataRow row2 = table.NewRow();
                        row2.ItemArray = view.Row.ItemArray;
                        table.Rows.Add(row2);
                        break;
                    }
                }
            }
            return table;
        }

        private static void UpdateStationDesktop(object stationId, object styleId)
        {
            EntityData data = new EntityData("StationDesktop");
            try
            {
                try
                {
                    StationDesktop.UpdateStationDesktop(stationId, styleId);
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }
            finally
            {
                data.Dispose();
            }
        }
    }
}

