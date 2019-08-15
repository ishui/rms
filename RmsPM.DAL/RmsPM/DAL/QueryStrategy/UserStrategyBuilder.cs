namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;

    /// <summary>
    /// 用户检索策略生成器
    /// </summary>
    public class UserStrategyBuilder : StandardQueryStringBuilder
    {
        private string QueryCountString = "";

        public UserStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("SystemUser", "SelectAll").SqlString;
            this.QueryCountString = SqlManager.GetSqlStruct("SystemUser", "QueryCountString").SqlString;
            base.IsNeedWhere = true;
        }

        /// <summary>
        /// 添加策略
        /// </summary>
        /// <param name="strategy"></param>
        public override void AddStrategy(Strategy strategy)
        {
            switch (((UserStrategyName) strategy.Name))
            {
                case UserStrategyName.UserCode:
                    strategy.RelationFieldName = "UserCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case UserStrategyName.UserID:
                    strategy.RelationFieldName = "UserID";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case UserStrategyName.UserName:
                    strategy.RelationFieldName = "UserName";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case UserStrategyName.PassWord:
                    strategy.RelationFieldName = "PassWord";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case UserStrategyName.OwnName:
                    strategy.RelationFieldName = "OwnName";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case UserStrategyName.Status:
                    strategy.RelationFieldName = "Status";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                case UserStrategyName.Sex:
                    strategy.RelationFieldName = "Sex";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case UserStrategyName.SortID:
                    strategy.RelationFieldName = "SortID";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case UserStrategyName.ShortUserName:
                    strategy.RelationFieldName = "ShortUserName";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                default:
                    strategy.Type = StrategyType.Other;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public string BuildQueryCountString()
        {
            return (this.QueryCountString + this.BuildStrategysString());
        }

        /// <summary>
        /// 生成SQL语句
        /// </summary>
        /// <param name="strategy"></param>
        /// <returns></returns>
        public override string BuildSingleStrategyString(Strategy strategy)
        {
            UserStrategyName name = (UserStrategyName) strategy.Name;
            string text = "";
            if (strategy.Type == StrategyType.Other)
            {
                string text2 = "";
                string unitFullCode = "";
                string text4 = "";
                string text5 = "";
                string parameter = "";
                switch (name)
                {
                    case UserStrategyName.UserCodes:
                        return string.Format(" UserCode in ( {0} )", strategy.GetParameter(0));

                    case UserStrategyName.UsersAndStations:
                        parameter = strategy.GetParameter(0);
                        text5 = strategy.GetParameter(1);
                        return string.Format(" (  ( UserCode in ( {0} ) ) or   exists ( select 1 from UserRole where UserRole.UserCode=SystemUser.UserCode and UserRole.StationCode in ( {1} )  ) ) ", parameter, text5);

                    case UserStrategyName.UserID:
                    case UserStrategyName.UserName:
                    case UserStrategyName.PassWord:
                    case UserStrategyName.OwnName:
                    case UserStrategyName.Status:
                    case UserStrategyName.Sex:
                    case UserStrategyName.SortID:
                        return text;

                    case UserStrategyName.StationCode:
                    {
                        string text7 = strategy.GetParameter(0);
                        return string.Format("  exists ( select 1 from UserRole where UserRole.UserCode=SystemUser.UserCode and UserRole.StationCode ='{0}'  )  ", text7);
                    }
                    case UserStrategyName.StationCodes:
                        text5 = strategy.GetParameter(0);
                        return string.Format("  exists ( select 1 from UserRole where UserRole.UserCode=SystemUser.UserCode and UserRole.StationCode in ( {0} )  )  ", text5);

                    case UserStrategyName.NoStation:
                        return " not exists (select * from UserRole where UserRole.UserCode = SystemUser.UserCode)";

                    case UserStrategyName.RoleCode:
                        text4 = strategy.GetParameter(0);
                        return string.Format("  exists ( select 1 from UserRole where UserRole.UserCode=SystemUser.UserCode and exists ( select 1 from Station where Station.StationCode=UserRole.StationCode and Station.RoleCode='{0}' ) ) ", strategy.GetParameter(0));

                    case UserStrategyName.ProjectCode:
                        unitFullCode = OBSDAO.GetUnitFullCode(ProjectDAO.GetProjectUnitCode(strategy.GetParameter(0)));
                        return string.Format("  exists ( select 1 from UserRole where UserRole.UserCode=SystemUser.UserCode and exists ( select 1 from Station where Station.StationCode=UserRole.StationCode and exists (  select 1 from Unit where Station.UnitCode=Unit.UnitCode and Unit.FullCode like '{0}%' ) ) )  ", unitFullCode);

                    case UserStrategyName.UnitCodeEx:
                        unitFullCode = OBSDAO.GetUnitFullCode(strategy.GetParameter(0));
                        return string.Format("  exists ( select 1 from UserRole where UserRole.UserCode=SystemUser.UserCode and exists ( select 1 from Station where Station.StationCode=UserRole.StationCode and exists (  select 1 from Unit where Station.UnitCode=Unit.UnitCode and Unit.FullCode like '{0}%' ) ) )  ", unitFullCode);

                    case UserStrategyName.UnitCode:
                        text2 = strategy.GetParameter(0);
                        return string.Format("  exists ( select 1 from UserRole where UserRole.UserCode=SystemUser.UserCode and exists ( select 1 from Station where Station.StationCode=UserRole.StationCode and Station.UnitCode='{0}' ) )  ", text2);

                    case UserStrategyName.WBSCode:
                        return string.Format(" exists (select * from TaskPerson where TaskPerson.UserCode = [SystemUser].UserCode and TaskPerson.WBSCode ='{0}' and Type = 2 )", strategy.GetParameter(0));

                    case UserStrategyName.UserIdorUserName:
                        return string.Format(" ( userid ='{0}' or username='{0}' )", Strategy.ReplaceSingleQuote(strategy.GetParameter(0)));
                }
                return text;
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }

        public string GetDefaultOrder()
        {
            return " order by case isnull(sortid, '') when '' then '做' else sortid end, UserName";
        }
    }
}

