namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class PBSPicStrategyBuilder : StandardQueryStringBuilder
    {
        public PBSPicStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("PBSPic", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((PBSPicStrategyName) strategy.Name))
            {
                case PBSPicStrategyName.PBSPicCodeEq:
                    strategy.RelationFieldName = "PBSPicCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PBSPicStrategyName.MasterTypeEq:
                    strategy.RelationFieldName = "MasterType";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PBSPicStrategyName.MasterCodeEq:
                    strategy.RelationFieldName = "MasterCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PBSPicStrategyName.PicTitleEq:
                    strategy.RelationFieldName = "PicTitle";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PBSPicStrategyName.PicRemarkLike:
                    strategy.RelationFieldName = "PicRemark";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case PBSPicStrategyName.FileNameEq:
                    strategy.RelationFieldName = "FileName";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PBSPicStrategyName.PicWidthEq:
                    strategy.RelationFieldName = "PicWidth";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                case PBSPicStrategyName.PicHeightEq:
                    strategy.RelationFieldName = "PicHeight";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                case PBSPicStrategyName.Content_TypeEq:
                    strategy.RelationFieldName = "Content_Type";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PBSPicStrategyName.LengthEq:
                    strategy.RelationFieldName = "Length";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                case PBSPicStrategyName.CreatePersonEq:
                    strategy.RelationFieldName = "CreatePerson";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PBSPicStrategyName.CreateDateRa:
                    strategy.RelationFieldName = "CreateDate";
                    strategy.Type = StrategyType.DateTimeRange;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            PBSPicStrategyName name = (PBSPicStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

