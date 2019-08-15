namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;


    /// <summary>
    /// 科目检索策略生成器
    /// </summary>
    public class AccessRangeStrategyBuilder : StandardQueryStringBuilder
    {
        public string QueryViewString = "";

        public AccessRangeStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("AccessRange", "SelectAll").SqlString;
            this.QueryViewString = SqlManager.GetSqlStruct("AccessRange", "SelectView").SqlString;
            base.IsNeedWhere = true;
        }

        /// <summary>
        /// 添加策略
        /// </summary>
        /// <param name="strategy"></param>
        public override void AddStrategy(Strategy strategy)
        {
            switch (((AccessRangeStrategyName) strategy.Name))
            {
                case AccessRangeStrategyName.AccessRangeCode:
                    strategy.RelationFieldName = "AccessRangeCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case AccessRangeStrategyName.ResourceCode:
                    strategy.RelationFieldName = "ResourceCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case AccessRangeStrategyName.UnitCode:
                    strategy.RelationFieldName = "UnitCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case AccessRangeStrategyName.OperationCode:
                    strategy.RelationFieldName = "OperationCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case AccessRangeStrategyName.OperationCodeLike:
                    strategy.RelationFieldName = "OperationCode";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case AccessRangeStrategyName.GroupCode:
                    strategy.RelationFieldName = "GroupCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case AccessRangeStrategyName.RoleLevel:
                    strategy.RelationFieldName = "RoleLevel";
                    strategy.Type = StrategyType.IntegerRange;
                    break;

                default:
                    strategy.Type = StrategyType.Other;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public string BuildQueryViewString()
        {
            return (this.QueryViewString + this.BuildStrategysString() + this.BuildOrderString());
        }

        /// <summary>
        /// 生成SQL语句
        /// </summary>
        /// <param name="strategy"></param>
        /// <returns></returns>
        public override string BuildSingleStrategyString(Strategy strategy)
        {
            AccessRangeStrategyName name = (AccessRangeStrategyName) strategy.Name;
            string parameter = "";
            string text3 = "";
            if (strategy.Type != StrategyType.Other)
            {
                return StandardStrategyStringBuilder.BuildStrategyString(strategy);
            }
            switch (name)
            {
                case AccessRangeStrategyName.OperationCodeIn:
                    return string.Format(" OperationCode in ({0})", strategy.GetParameter(0));

                case AccessRangeStrategyName.AccessRelation0:
                    parameter = strategy.GetParameter(0);
                    text3 = strategy.GetParameter(1);
                    return string.Format(" AccessRangeType='{0}' and relationCode='{1}' ", parameter, text3);

                case AccessRangeStrategyName.AccessRelation1:
                {
                    string text4 = strategy.GetParameter(0);
                    string text5 = strategy.GetParameter(1);
                    string text6 = "";
                    foreach (string text7 in text5.Split(new char[] { ',' }))
                    {
                        if (text6 != "")
                        {
                            text6 = text6 + ",";
                        }
                        text6 = text6 + "'" + text7 + "'";
                    }
                    if (text6.Length == 0)
                    {
                        return string.Format(" ( AccessRangeType=0 and relationCode = '{0}' )   ", text4);
                    }
                    return string.Format(" (( AccessRangeType=0 and relationCode = '{0}' ) or ( AccessRangeType=1 and relationCode in ( {1} ) ))  ", text4, text6);
                }
            }
            return "";
        }
    }
}

