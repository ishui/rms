namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class OALINKMANStrategyBuilder : StandardQueryStringBuilder
    {
        public OALINKMANStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("OALINKMAN", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((OALINKMANStrategyName) strategy.Name))
            {
                case OALINKMANStrategyName.OALINKMANCODE:
                    strategy.RelationFieldName = "NoticeCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OALINKMANStrategyName.LINKMAN_NAME:
                    strategy.RelationFieldName = "LINKMAN_NAME";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OALINKMANStrategyName.UNIT_NAME:
                    strategy.RelationFieldName = "UNIT_NAME";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case OALINKMANStrategyName.MOBILE:
                    strategy.RelationFieldName = "MOBILE";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case OALINKMANStrategyName.INPUTTER:
                    strategy.RelationFieldName = "INPUTTER";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OALINKMANStrategyName.INFO_CODE:
                    strategy.RelationFieldName = "INFO_CODE";
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
            OALINKMANStrategyName name = (OALINKMANStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case OALINKMANStrategyName.OALINKMANCODE:
                        return "";
                }
                return string.Format("{0}", strategy.GetParameter(0));
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

