namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class RoleOperation : StandardQueryStringBuilder
    {
        public RoleOperation()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("RoleOperation", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            RoleOperationName name = (RoleOperationName) strategy.Name;
            if (name == RoleOperationName.RoleCode)
            {
                strategy.RelationFieldName = "UserCode";
                strategy.Type = StrategyType.StringEqual;
            }
            else
            {
                strategy.Type = StrategyType.Other;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            RoleOperationName name = (RoleOperationName) strategy.Name;
            string text2 = strategy.Type.ToString();
            string text3 = StrategyType.Other.ToString();
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case RoleOperationName.UserCode:
                        return "";
                }
                return string.Format("RoleCode  in ( select RoleCode from Station where StationCode in (select StationCode from UserRole where UserCode = '{0}') ) ", strategy.GetParameter(0));
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

