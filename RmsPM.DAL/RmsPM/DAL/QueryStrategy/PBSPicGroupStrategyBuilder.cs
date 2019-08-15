namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class PBSPicGroupStrategyBuilder : StandardQueryStringBuilder
    {
        public PBSPicGroupStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("PBSPicGroup", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((PBSPicGroupStrategyName) strategy.Name))
            {
                case PBSPicGroupStrategyName.PBSPicGroupCodeEq:
                    strategy.RelationFieldName = "PBSPicGroupCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PBSPicGroupStrategyName.MasterTypeEq:
                    strategy.RelationFieldName = "MasterType";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PBSPicGroupStrategyName.MasterCodeEq:
                    strategy.RelationFieldName = "MasterCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PBSPicGroupStrategyName.GroupNameEq:
                    strategy.RelationFieldName = "GroupName";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PBSPicGroupStrategyName.PicNumberEq:
                    strategy.RelationFieldName = "PicNumber";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                case PBSPicGroupStrategyName.CreatePersonEq:
                    strategy.RelationFieldName = "CreatePerson";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PBSPicGroupStrategyName.CreateDateRa:
                    strategy.RelationFieldName = "CreateDate";
                    strategy.Type = StrategyType.DateTimeRange;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            PBSPicGroupStrategyName name = (PBSPicGroupStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

