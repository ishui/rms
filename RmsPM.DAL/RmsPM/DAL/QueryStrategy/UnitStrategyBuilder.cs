namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;

    public class UnitStrategyBuilder : StandardQueryStringBuilder
    {
        public string QueryChildCountOnlyHasUserString = "";
        public string QueryChildCountString = "";
        public string QueryFullNameString = "";
        public string QueryOBSOnlyHasUserString = "";
        public string QueryOBSString = "";

        public UnitStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("Unit", "SelectAll").SqlString;
            this.QueryChildCountString = SqlManager.GetSqlStruct("Unit", "SelectChildCount").SqlString;
            this.QueryOBSString = SqlManager.GetSqlStruct("Unit", "SelectOBS").SqlString;
            this.QueryChildCountOnlyHasUserString = SqlManager.GetSqlStruct("Unit", "SelectChildCountOnlyHasUser").SqlString;
            this.QueryOBSOnlyHasUserString = SqlManager.GetSqlStruct("Unit", "SelectOBSOnlyHasUser").SqlString;
            this.QueryFullNameString = SqlManager.GetSqlStruct("Unit", "SelectFullName").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((UnitStrategyName) strategy.Name))
            {
                case UnitStrategyName.UnitCode:
                    strategy.RelationFieldName = "UnitCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case UnitStrategyName.ParentUnitCode:
                    strategy.RelationFieldName = "ParentUnitCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case UnitStrategyName.UnitName:
                    strategy.RelationFieldName = "UnitName";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case UnitStrategyName.RelaCode:
                    strategy.RelationFieldName = "RelaCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case UnitStrategyName.UnitType:
                    strategy.RelationFieldName = "UnitType";
                    strategy.Type = StrategyType.StringRange;
                    break;

                case UnitStrategyName.SubjectSetCode:
                    strategy.RelationFieldName = "SubjectSetCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case UnitStrategyName.SelfAccount:
                    strategy.RelationFieldName = "SelfAccount";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                case UnitStrategyName.SortID:
                    strategy.RelationFieldName = "SortID";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                default:
                    strategy.Type = StrategyType.Other;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public string BuildQueryChildCountOnlyHasUserString()
        {
            this.AddStrategy(new Strategy(UnitStrategyName.OnlyHasUser));
            return (this.QueryChildCountOnlyHasUserString + base.BuildStrategysString() + base.BuildOrderString());
        }

        public string BuildQueryChildCountString()
        {
            return (this.QueryChildCountString + base.BuildStrategysString() + base.BuildOrderString());
        }

        public string BuildQueryFullNameString()
        {
            return (this.QueryFullNameString + base.BuildStrategysString() + base.BuildOrderString());
        }

        public string BuildQueryOBSOnlyHasUserString()
        {
            this.AddStrategy(new Strategy(UnitStrategyName.OnlyHasUser));
            return (this.QueryOBSOnlyHasUserString + base.BuildStrategysString() + base.BuildOrderString());
        }

        public string BuildQueryOBSString()
        {
            return (this.QueryOBSString + base.BuildStrategysString() + base.BuildOrderString());
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            UnitStrategyName name = (UnitStrategyName) strategy.Name;
            string text = "";
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case UnitStrategyName.OnlyHasUser:
                        return " dbo.GetUnitUserCount(UnitCode) > 0";

                    case UnitStrategyName.SortID:
                        return text;

                    case UnitStrategyName.ProjectCode:
                        return string.Format(" FullCode LIKE dbo.GetUnitFullCodeByProjectCode('{0}') + '%'", strategy.GetParameter(0));

                    case UnitStrategyName.UnderUnitCode:
                    {
                        string unitFullCode = OBSDAO.GetUnitFullCode(strategy.GetParameter(0));
                        return string.Format("  FullCode like '{0}%' ", strategy.GetParameter(0));
                    }
                }
                return text;
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }

        public string GetDefaultOrder()
        {
            return " order by case isnull(sortid, '') when '' then '做' else sortid end, UnitName";
        }
    }
}

