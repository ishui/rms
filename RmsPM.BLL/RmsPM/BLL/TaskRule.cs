namespace RmsPM.BLL
{
    using System;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;

    public sealed class TaskRule
    {
        public static string ConcatTaskRelaName(string RelaTypeName, string RelaName)
        {
            string text2;
            try
            {
                string text = "";
                if ((RelaTypeName != "") && (RelaName != ""))
                {
                    text = RelaTypeName + " - " + RelaName;
                }
                else
                {
                    text = RelaTypeName;
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetProjectTaskCodeByProject(string ProjectCode)
        {
            string text2;
            try
            {
                string text = "";
                if (ProjectCode == "")
                {
                    return text;
                }
                EntityData projectTaskByProject = WBSDAO.GetProjectTaskByProject(ProjectCode);
                if (projectTaskByProject.HasRecord())
                {
                    text = projectTaskByProject.GetString("WBSCode");
                }
                projectTaskByProject.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetTaskRelaName(string RelaType, string RelaCode)
        {
            string text4;
            try
            {
                string relaTypeName = "";
                string relaName = "";
                switch (RelaType.ToUpper())
                {
                    case "U":
                        relaTypeName = "单位工程";
                        relaName = PBSRule.GetPBSUnitName(RelaCode);
                        break;

                    case "B":
                        relaTypeName = "楼栋";
                        relaName = ProductRule.GetBuildingName(RelaCode);
                        break;

                    default:
                        relaTypeName = "一般工作项";
                        break;
                }
                text4 = ConcatTaskRelaName(relaTypeName, relaName);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text4;
        }
    }
}

