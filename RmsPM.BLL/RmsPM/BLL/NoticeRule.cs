namespace RmsPM.BLL
{
    using System;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;

    public class NoticeRule
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
                EntityData noticeByCode = RemindDAO.GetNoticeByCode(Code);
                if (noticeByCode.HasRecord() && noticeByCode.CurrentTable.Columns.Contains(FieldName))
                {
                    if (noticeByCode.CurrentTable.Columns[FieldName].DataType == Type.GetType("System.Int32"))
                    {
                        return noticeByCode.GetIntString(FieldName);
                    }
                    if (noticeByCode.CurrentTable.Columns[FieldName].DataType == Type.GetType("System.Decimal"))
                    {
                        return noticeByCode.GetDecimalString(FieldName);
                    }
                    if (noticeByCode.CurrentTable.Columns[FieldName].DataType == Type.GetType("System.DateTime"))
                    {
                        return noticeByCode.GetDateTimeOnlyDate(FieldName);
                    }
                    if (noticeByCode.CurrentTable.Columns[FieldName].DataType == Type.GetType("System.String"))
                    {
                        return noticeByCode.GetString(FieldName);
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

        public static string GetNoticeName(string Code)
        {
            EntityData noticeByCode = RemindDAO.GetNoticeByCode(Code);
            if (!noticeByCode.HasRecord())
            {
                return "";
            }
            return noticeByCode.GetString("Title");
        }
    }
}

