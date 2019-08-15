namespace RmsPM.BLL
{
    using System;
    using System.Collections;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;

    public class OARule
    {
        public static string GetAllUser()
        {
            string text = "";
            DataView view = new DataView(SystemManageDAO.GetAllSystemUser().CurrentTable, "Status='0'", "", DataViewRowState.CurrentRows);
            foreach (DataRowView view2 in view)
            {
                if (text != "")
                {
                    text = text + ",";
                }
                text = text + view2["UserCode"].ToString();
            }
            return text;
        }

        public static string GetDictItemText(string strItemCode)
        {
            if (strItemCode.Length >= 1)
            {
                EntityData dictionaryItemByCode = SystemManageDAO.GetDictionaryItemByCode(strItemCode);
                if (dictionaryItemByCode.HasRecord())
                {
                    return dictionaryItemByCode.GetString("Name");
                }
            }
            return "";
        }

        public static string GetVehicleInfo(string code, string column)
        {
            if (code.Length >= 1)
            {
                if (column.Length < 1)
                {
                    return "";
                }
                EntityData oAVehicleInfoByCode = OADAO.GetOAVehicleInfoByCode(code);
                if (oAVehicleInfoByCode.HasRecord())
                {
                    return oAVehicleInfoByCode.GetString(column);
                }
            }
            return "";
        }

        public static void SaveRS(ArrayList arOperator, string strUser, string strStation, string strOption)
        {
            AccessRange range;
            if (strUser.Length > 0)
            {
                foreach (string text in strUser.Split(new char[] { ',' }))
                {
                    if (text != "")
                    {
                        range = new AccessRange();
                        range.AccessRangeType = 0;
                        range.RelationCode = text;
                        range.Operations = strOption;
                        arOperator.Add(range);
                    }
                }
            }
            if (strStation.Length > 0)
            {
                foreach (string text2 in strStation.Split(new char[] { ',' }))
                {
                    if (text2 != "")
                    {
                        range = new AccessRange();
                        range.AccessRangeType = 1;
                        range.RelationCode = text2;
                        range.Operations = strOption;
                        arOperator.Add(range);
                    }
                }
            }
        }

        public static void ViewRemindUpDate(string type, string masterCode, string userCode)
        {
            EntityData entity = RemindDAO.GetRemindObjectByMasterUser(type, masterCode, userCode);
            if (entity.HasRecord())
            {
                entity.CurrentRow["IsDesk"] = "0";
                RemindDAO.UpdateRemindObject(entity);
            }
            entity.Dispose();
        }
    }
}

