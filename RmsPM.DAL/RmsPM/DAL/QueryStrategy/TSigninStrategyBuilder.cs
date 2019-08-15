namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class TSigninStrategyBuilder : StandardQueryStringBuilder
    {
        public TSigninStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("TSignin", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((TSigninStrategyName) strategy.Name))
            {
                case TSigninStrategyName.TSigninCode:
                    strategy.RelationFieldName = "TSigninCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case TSigninStrategyName.SigninDepartment:
                    strategy.RelationFieldName = "SigninDepartment";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case TSigninStrategyName.Signinman:
                    strategy.RelationFieldName = "Signinman";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case TSigninStrategyName.Date:
                    strategy.RelationFieldName = "Date";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case TSigninStrategyName.LeitMotiv:
                    strategy.RelationFieldName = "LeitMotiv";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case TSigninStrategyName.Remark:
                    strategy.RelationFieldName = "Remark";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case TSigninStrategyName.State:
                    strategy.RelationFieldName = "State";
                    strategy.Type = StrategyType.StringIn;
                    break;

                case TSigninStrategyName.Flag:
                    strategy.RelationFieldName = "Flag";
                    strategy.Type = StrategyType.StringEqual;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            TSigninStrategyName name = (TSigninStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

