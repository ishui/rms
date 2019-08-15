namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class Design_MessageStrategyBuilder : StandardQueryStringBuilder
    {
        public Design_MessageStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("Design_Message", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((Design_MessageStrategyName) strategy.Name))
            {
                case Design_MessageStrategyName.DesignCode:
                    strategy.RelationFieldName = "DesignCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case Design_MessageStrategyName.DesignName:
                    strategy.RelationFieldName = "DesignName";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case Design_MessageStrategyName.DesignID:
                    strategy.RelationFieldName = "DesignID";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case Design_MessageStrategyName.ProjectName:
                    strategy.RelationFieldName = "ProjectName";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case Design_MessageStrategyName.ContractName:
                    strategy.RelationFieldName = "ContractName";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case Design_MessageStrategyName.ContractCode:
                    strategy.RelationFieldName = "ContractCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case Design_MessageStrategyName.ContractID:
                    strategy.RelationFieldName = "ContractID";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case Design_MessageStrategyName.DesignReason:
                    strategy.RelationFieldName = "DesignReason";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case Design_MessageStrategyName.DesignSupplier:
                    strategy.RelationFieldName = "DesignSupplier";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case Design_MessageStrategyName.DesignPerson:
                    strategy.RelationFieldName = "DesignPerson";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case Design_MessageStrategyName.DesignLastTime:
                    strategy.RelationFieldName = "DesignLastTime";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case Design_MessageStrategyName.DesignBeingTime:
                    strategy.RelationFieldName = "DesignBeingTime";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case Design_MessageStrategyName.DesignJionTime:
                    strategy.RelationFieldName = "DesignJionTime";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case Design_MessageStrategyName.DesignFlag:
                    strategy.RelationFieldName = "DesignFlag";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case Design_MessageStrategyName.DesignState:
                    strategy.RelationFieldName = "DesignState";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case Design_MessageStrategyName.DesignDepartMentID:
                    strategy.RelationFieldName = "DesignDepartMentID";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case Design_MessageStrategyName.DesignRemark:
                    strategy.RelationFieldName = "DesignRemark";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case Design_MessageStrategyName.ProjectCode:
                    strategy.RelationFieldName = "ProjectCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            Design_MessageStrategyName name = (Design_MessageStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

