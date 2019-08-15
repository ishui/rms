namespace Rms.WorkFlow
{
    using System;

    public class RoleTypeManage
    {
        public RoleType GetStringRoleTypeItem(string StringItem)
        {
            Type type = typeof(RoleType);
            switch (StringItem)
            {
                case "Unit":
                    return RoleType.Unit;

                case "Porson":
                    return RoleType.Porson;

                case "Station":
                    return RoleType.Station;

                case "All":
                    return RoleType.All;

                case "Other":
                    return RoleType.Other;
            }
            return RoleType.Other;
        }
    }
}

