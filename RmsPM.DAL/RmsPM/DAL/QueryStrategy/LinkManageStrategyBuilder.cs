namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class LinkManageStrategyBuilder : StandardQueryStringBuilder
    {
        public LinkManageStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("LinkManage", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((LinkManageStrategyName) strategy.Name))
            {
                case LinkManageStrategyName.LinkManageCode:
                    strategy.RelationFieldName = "LinkManageCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case LinkManageStrategyName.Linkname:
                    strategy.RelationFieldName = "Linkname";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case LinkManageStrategyName.LinkUrl:
                    strategy.RelationFieldName = "LinkUrl";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case LinkManageStrategyName.CreateDate:
                    strategy.RelationFieldName = "CreateDate";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case LinkManageStrategyName.state:
                    strategy.RelationFieldName = "state";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case LinkManageStrategyName.flag:
                    strategy.RelationFieldName = "flag";
                    strategy.Type = StrategyType.StringEqual;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            LinkManageStrategyName name = (LinkManageStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

