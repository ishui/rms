namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class UserRoleStrategyBuilder : StandardQueryStringBuilder
    {
        public UserRoleStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("SystemUser", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((UserRoleStrategyName) strategy.Name))
            {
                case UserRoleStrategyName.UserCode:
                    strategy.RelationFieldName = "UserCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case UserRoleStrategyName.UnitCode:
                    strategy.RelationFieldName = "UnitCode";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case UserRoleStrategyName.RoleCode:
                    strategy.RelationFieldName = "RoleCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                default:
                    strategy.Type = StrategyType.Other;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            UserRoleStrategyName name = (UserRoleStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case UserRoleStrategyName.UnitCodeEx:
                    {
                    } break;
                }
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

