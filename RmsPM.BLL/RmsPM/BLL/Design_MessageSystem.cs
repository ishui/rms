namespace RmsPM.BLL
{
    using System;
    using Rms.ORMap;

    public class Design_MessageSystem
    {
        public static string ShowStateMessage(string DesignState)
        {
            switch (DesignState)
            {
                case "0":
                    return "审请";

                case "1":
                    return "审批中";

                case "2":
                    return "已审批";

                case "3":
                    return "未通过";
            }
            return "未知状态";
        }

        public static void UpdateDesignState(string DesignCode, string state, StandardEntityDAO dao)
        {
            Design_Message message = new Design_Message();
            message.dao = dao;
            message.DesignState = state;
            message.DesignCode = DesignCode;
            message.Design_MessageSubmit();
        }
    }
}

