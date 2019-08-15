namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class FunctionStructureStrategyBuilder : StandardQueryStringBuilder
    {
        public FunctionStructureStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("FunctionStructure", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((FunctionStructureStrategyName) strategy.Name))
            {
                case FunctionStructureStrategyName.FunctionStructureCode:
                    strategy.RelationFieldName = "FunctionStructureCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case FunctionStructureStrategyName.ParentCode:
                    strategy.RelationFieldName = "ParentCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case FunctionStructureStrategyName.IsAvailable:
                    strategy.RelationFieldName = "IsAvailable";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                case FunctionStructureStrategyName.IsRightControlPoint:
                    strategy.RelationFieldName = "IsRightControlPoint";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                case FunctionStructureStrategyName.IsRoleControlPoint:
                    strategy.RelationFieldName = "IsRoleControlPoint";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                case FunctionStructureStrategyName.IsSystemClass:
                    strategy.RelationFieldName = "IsSystemClass";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                case FunctionStructureStrategyName.Deep:
                    strategy.RelationFieldName = "Deep";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                default:
                    strategy.Type = StrategyType.Other;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            FunctionStructureStrategyName name = (FunctionStructureStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                //switch (name)
                //{
                //    case FunctionStructureStrategyName.ChildTreeNode:
                //        return "";
                //}
                return string.Format("  FunctionStructureCode like '{0}%'  ", strategy.GetParameter(0));
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

