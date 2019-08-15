namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    /// <summary>
    /// 查询规则
    /// </summary>
    public class AttrSetFrameItemStrategyBuilder : StandardQueryStringBuilder
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public AttrSetFrameItemStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("AttrSetFrameItem", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        /// <summary>
        /// 添加策略
        /// </summary>
        /// <param name="strategy"></param>
        public override void AddStrategy(Strategy strategy)
        {
            switch (((AttrSetFrameItemStrategyName) strategy.Name))
            {
                case AttrSetFrameItemStrategyName.AttrSetFrameItemCodeEq:
                    strategy.RelationFieldName = "AttrSetFrameItemCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case AttrSetFrameItemStrategyName.AttrSetFrameCodeEq:
                    strategy.RelationFieldName = "AttrSetFrameCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case AttrSetFrameItemStrategyName.AttrSetFrameCode2Eq:
                    strategy.RelationFieldName = "AttrSetFrameCode2";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case AttrSetFrameItemStrategyName.AttrSetItemCodeEq:
                    strategy.RelationFieldName = "AttrSetItemCode";
                    strategy.Type = StrategyType.StringEqual;
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
            AttrSetFrameItemStrategyName name = (AttrSetFrameItemStrategyName) strategy.Name;
            string text = "";
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case AttrSetFrameItemStrategyName.AttrSetFrameCodeIn:
                        return string.Format(" AttrSetFrameCode in ( {0} )", strategy.GetParameter(0));

                    case AttrSetFrameItemStrategyName.AttrSetFrameCode2Eq:
                    case AttrSetFrameItemStrategyName.AttrSetItemCodeEq:
                        return text;

                    case AttrSetFrameItemStrategyName.AttrSetFrameCode2In:
                        return string.Format(" AttrSetFrameCode2 in ( {0} )", strategy.GetParameter(0));

                    case AttrSetFrameItemStrategyName.AttrSetItemCodeIn:
                        return string.Format(" AttrSetItemCode in ( {0} )", strategy.GetParameter(0));
                }
                return text;
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

