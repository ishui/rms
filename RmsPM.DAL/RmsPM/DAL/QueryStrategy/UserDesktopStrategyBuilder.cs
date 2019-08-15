namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using Rms.ORMap;

    public class UserDesktopStrategyBuilder : StandardQueryStringBuilder
    {
        public UserDesktopStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("TaskAttention", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((UserDesktopStrategyName) strategy.Name))
            {
                case UserDesktopStrategyName.UserID:
                    strategy.RelationFieldName = "UserID";
                    strategy.Type = StrategyType.StringIn;
                    break;

                case UserDesktopStrategyName.ControlID:
                    strategy.RelationFieldName = "ControlID";
                    strategy.Type = StrategyType.StringIn;
                    break;

                default:
                    strategy.Type = StrategyType.Other;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            UserDesktopStrategyName name = (UserDesktopStrategyName) strategy.Name;
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }

        public static void DeleteUserDesktop(object userid)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["RmsPM.Data.ConnectionString"].ConnectionString);
            SqlCommand command = new SqlCommand("Delete from UserDesktop where userid =" + userid.ToString(), connection);
            try
            {
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }
            finally
            {
                connection.Close();
            }
        }

        public static DataSet GetUserControlByID(object userid)
        {
            DataSet set2;
            SqlConnection selectConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RmsPM.Data.ConnectionString"].ConnectionString);
            string selectCommandText = "select * from UserDesktop where userid =" + userid.ToString();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(selectCommandText, selectConnection);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                set2 = dataSet;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                selectConnection.Close();
            }
            return set2;
        }

        public static void InsertUserDesktop(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("UserDesktop"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}

