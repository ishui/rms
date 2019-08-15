namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;

    public class RoleStrategyBuilder : StandardQueryStringBuilder
    {
        public RoleStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("Role", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((RoleStrategyName) strategy.Name))
            {
                case RoleStrategyName.RoleCode:
                    strategy.RelationFieldName = "RoleCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case RoleStrategyName.RoleName:
                    strategy.RelationFieldName = "RoleName";
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
            RoleStrategyName name = (RoleStrategyName) strategy.Name;
            string projectUnitCode = "";
            string unitFullCode = "";
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case RoleStrategyName.ProjectCode:
                        projectUnitCode = ProjectDAO.GetProjectUnitCode(strategy.GetParameter(0));
                        return string.Format(" exists ( select * from userRole where userrole.RoleCode = Role.RoleCode and unitCode ='{0}' )  ", unitFullCode);

                    case RoleStrategyName.ProjectCodeEx:
                        unitFullCode = OBSDAO.GetUnitFullCode(ProjectDAO.GetProjectUnitCode(strategy.GetParameter(0)));
                        return string.Format(" exists ( select * from userRole where userrole.RoleCode = Role.RoleCode and unitCode in ( select unitCode from unit where fullCode like '{0}%'))  ", unitFullCode);

                    case RoleStrategyName.UnitCode:
                        unitFullCode = OBSDAO.GetUnitFullCode(strategy.GetParameter(0));
                        return string.Format(" exists ( select * from userRole where userrole.RoleCode = Role.RoleCode and unitCode in ( select unitCode from unit where fullCode like '{0}%'))  ", unitFullCode);

                    case RoleStrategyName.ModuleCode:
                        return string.Format(" RoleCode in ( select RoleCode from RoleModule where ModuleCode='{0}' ) ", strategy.GetParameter(0));

                    case RoleStrategyName.UserCode:
                        return string.Format(" RoleCode in ( select RoleCode from UserRole where UserCode='{0}' ) ", strategy.GetParameter(0));
                }
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

