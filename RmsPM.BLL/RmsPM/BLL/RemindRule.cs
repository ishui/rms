namespace RmsPM.BLL
{
    using System;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;

    public class RemindRule
    {
        public static string GetFieldName(string Code, string FieldName)
        {
            string text2;
            try
            {
                string text = "";
                if (Code == "")
                {
                    return text;
                }
                EntityData remindObjectByCode = RemindDAO.GetRemindObjectByCode(Code);
                if (remindObjectByCode.HasRecord() && remindObjectByCode.CurrentTable.Columns.Contains(FieldName))
                {
                    if (remindObjectByCode.CurrentTable.Columns[FieldName].DataType == Type.GetType("System.Int32"))
                    {
                        return remindObjectByCode.GetIntString(FieldName);
                    }
                    if (remindObjectByCode.CurrentTable.Columns[FieldName].DataType == Type.GetType("System.Decimal"))
                    {
                        return remindObjectByCode.GetDecimalString(FieldName);
                    }
                    if (remindObjectByCode.CurrentTable.Columns[FieldName].DataType == Type.GetType("System.DateTime"))
                    {
                        return remindObjectByCode.GetDateTimeOnlyDate(FieldName);
                    }
                    if (remindObjectByCode.CurrentTable.Columns[FieldName].DataType == Type.GetType("System.String"))
                    {
                        return remindObjectByCode.GetString(FieldName);
                    }
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetObjectName(string code)
        {
            string text2;
            try
            {
                string text = "";
                if (code == "")
                {
                    return text;
                }
                EntityData remindObjectByCode = RemindDAO.GetRemindObjectByCode(code);
                if (remindObjectByCode.HasRecord())
                {
                    text = remindObjectByCode.GetString("ObjectName");
                }
                remindObjectByCode.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }
    }
}

