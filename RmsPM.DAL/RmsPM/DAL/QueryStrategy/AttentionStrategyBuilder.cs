namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class AttentionStrategyBuilder : StandardQueryStringBuilder
    {
        public AttentionStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("TaskAttention", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        /// <summary>
        /// 添加策略
        /// </summary>
        /// <param name="strategy"></param>
        public override void AddStrategy(Strategy strategy)
        {
            switch (((AttentionStrategyName) strategy.Name))
            {
                case AttentionStrategyName.ProjectCode:
                    strategy.RelationFieldName = "ProjectCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case AttentionStrategyName.UserCode:
                    strategy.RelationFieldName = "UserCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case AttentionStrategyName.Url:
                    strategy.RelationFieldName = "Url";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case AttentionStrategyName.AddModule:
                    strategy.RelationFieldName = "AddModule";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case AttentionStrategyName.AddTitle:
                    strategy.RelationFieldName = "AddTitle";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case AttentionStrategyName.AddTime:
                    strategy.RelationFieldName = "AddTime";
                    strategy.Type = StrategyType.DateTimeRangeOnlyDate;
                    break;

                default:
                    strategy.Type = StrategyType.Other;
                    break;
            }
            base.AddStrategy(strategy);
        }

        /// <summary>
        /// 生成SQL语句
        /// </summary>
        /// <param name="strategy"></param>
        /// <returns></returns>
        public override string BuildSingleStrategyString(Strategy strategy)
        {
            AttentionStrategyName name = (AttentionStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case AttentionStrategyName.AccessRange:
                        return "";
                }
                return AccessRanggeQuery.BuildAccessRangeString(strategy.GetParameter(0), strategy.GetParameter(1), strategy.GetParameter(2), "taskattention", "MasterCode", "UserCode");
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

