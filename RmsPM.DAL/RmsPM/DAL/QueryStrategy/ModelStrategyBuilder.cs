namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class ModelStrategyBuilder : StandardQueryStringBuilder
    {
        public string QueryDoorNumString = "";

        public ModelStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("Model", "SelectAll").SqlString;
            this.QueryDoorNumString = SqlManager.GetSqlStruct("Model", "SelectDoorNum").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((ModelStrategyName) strategy.Name))
            {
                case ModelStrategyName.ProjectCode:
                    strategy.RelationFieldName = "ProjectCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case ModelStrategyName.ModelCode:
                    strategy.RelationFieldName = "ModelCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case ModelStrategyName.ModelName:
                    strategy.RelationFieldName = "ModelName";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case ModelStrategyName.ModelNameLike:
                    strategy.RelationFieldName = "ModelName";
                    strategy.Type = StrategyType.StringLike;
                    strategy.SetParameter(0, "%" + strategy.GetParameter(0) + "%");
                    break;

                default:
                    strategy.Type = StrategyType.Other;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public string BuildQueryDoorNumString()
        {
            return (this.QueryDoorNumString + base.BuildStrategysString() + base.BuildOrderString());
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            ModelStrategyName name = (ModelStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                ModelStrategyName name2 = name;
                if (name2 != ModelStrategyName.ModelCodeNot)
                {
                    if (name2 != ModelStrategyName.BuildingCode)
                    {
                        return "";
                    }
                }
                else
                {
                    return string.Format("ModelCode <> '{0}'", strategy.GetParameter(0));
                }
                return string.Format("exists(select * from Room b where BuildingCode = '{0}' and ModelCode = a.ModelCode)", strategy.GetParameter(0));
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

