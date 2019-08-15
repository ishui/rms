namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;

    public class ProjectStrategyBuilder : StandardQueryStringBuilder
    {
        public string QueryViewString = "";

        public ProjectStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("Project", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((ProjectStrategyName) strategy.Name))
            {
                case ProjectStrategyName.ProjectCode:
                    strategy.RelationFieldName = "ProjectCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case ProjectStrategyName.ProjectName:
                    strategy.RelationFieldName = "ProjectName";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case ProjectStrategyName.ProjectNameLike:
                    strategy.RelationFieldName = "ProjectName";
                    strategy.Type = StrategyType.StringLike;
                    strategy.SetParameter(0, "%" + strategy.GetParameter(0) + "%");
                    break;

                case ProjectStrategyName.kgYear:
                    strategy.RelationFieldName = "kgDate";
                    strategy.Type = StrategyType.DateTimeEqualYear;
                    break;

                case ProjectStrategyName.jgYear:
                    strategy.RelationFieldName = "jgDate";
                    strategy.Type = StrategyType.DateTimeEqualYear;
                    break;

                case ProjectStrategyName.Status:
                    strategy.RelationFieldName = "Status";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case ProjectStrategyName.SalProjectCode:
                    strategy.RelationFieldName = "SalProjectCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                default:
                    strategy.Type = StrategyType.Other;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public string BuildQueryViewString()
        {
            return (this.QueryViewString + base.BuildStrategysString() + base.BuildOrderString());
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            ProjectStrategyName name = (ProjectStrategyName) strategy.Name;
            string text = "";
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case ProjectStrategyName.False:
                        return "1=2";

                    case ProjectStrategyName.ProjectCode:
                        return text;

                    case ProjectStrategyName.ProjectCodeNot:
                        return string.Format("ProjectCode <> '{0}'", strategy.GetParameter(0));

                    case ProjectStrategyName.ProjectCodeIn:
                        return string.Format("ProjectCode in ('{0}')", strategy.GetParameter(0).Replace(",", "','"));

                    case ProjectStrategyName.UnitCode:
                    {
                        string unitFullCode = OBSDAO.GetUnitFullCode(strategy.GetParameter(0));
                        if (unitFullCode == "")
                        {
                            return " 1 = 2";
                        }
                        return string.Format(" projectCode in ( select relaCode from unit where unitType='项目' and FullCode like '{0}%' ) ", unitFullCode);
                    }
                }
                return text;
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

