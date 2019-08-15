namespace RmsPM.BLL
{
    using System;

    public class ComSource
    {
        public static string[][] arImportant = new string[][] { new string[] { "0", "一般" }, new string[] { "1", "重要" } };
        public static string[][] arMaterialStatus = new string[][] { new string[] { "0", "" }, new string[] { "1", "请购待审核" }, new string[] { "2", "请购已审核" }, new string[] { "3", "核价待审核" }, new string[] { "4", "核价已审核" } };
        public static string[][] arRemind = new string[][] { new string[] { "0", "工作提醒", "" }, new string[] { "3", "工作变更提醒", "" }, new string[] { "1", "应付未付提醒", "" }, new string[] { "2", "工程计划提醒", "" }, new string[] { "41", "待办事项提醒", "" } };
        public static string[][] arStatus = new string[][] { new string[] { "0", "未开始" }, new string[] { "1", "进行中" }, new string[] { "2", "暂停" }, new string[] { "3", "取消" }, new string[] { "4", "已完成" } };
        public static string[][] arWorkType = new string[][] { new string[] { "0", "参与人" }, new string[] { "1", "监督人" }, new string[] { "2", "负责人" }, new string[] { "3", "工作报告分发对象" } };

        public static string GetImportantName(string strValue)
        {
            string text = "";
            for (int i = 0; i < arImportant.Length; i++)
            {
                if (strValue == arImportant[i][0])
                {
                    text = arImportant[i][1];
                }
            }
            return text;
        }

        public static string GetMaterialStatusName(string strValue)
        {
            string text = "";
            for (int i = 0; i < arMaterialStatus.Length; i++)
            {
                if (strValue == arMaterialStatus[i][0])
                {
                    text = arMaterialStatus[i][1];
                }
            }
            return text;
        }

        public static string GetRemindType(string strValue)
        {
            string text = "";
            for (int i = 0; i < arRemind.Length; i++)
            {
                if (strValue == arRemind[i][0])
                {
                    text = arRemind[i][1];
                }
            }
            return text;
        }

        public static string GetTaskStatusName(string strValue)
        {
            string text = "";
            for (int i = 0; i < arStatus.Length; i++)
            {
                if (strValue == arStatus[i][0])
                {
                    text = arStatus[i][1];
                }
            }
            return text;
        }

        public static string GetWorkTypeName(string strValue)
        {
            string text = "";
            for (int i = 0; i < arWorkType.Length; i++)
            {
                if (strValue == arWorkType[i][0])
                {
                    text = arWorkType[i][1];
                }
            }
            return text;
        }
    }
}

