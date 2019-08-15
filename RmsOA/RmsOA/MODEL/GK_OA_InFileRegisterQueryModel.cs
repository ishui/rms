namespace RmsOA.MODEL
{
    using System;
    using System.Data;

    public class GK_OA_InFileRegisterQueryModel : QueryBaseModel
    {
        public string AuditingMailCodeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" AuditingMailCode=@AuditingMailCode ");
                    base.InsertParameter("@AuditingMailCode", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public int CodeEqual
        {
            set
            {
                if (value != 0)
                {
                    base.QueryConditionStrAdd(" Code=@Code ");
                    base.InsertParameter("@Code", SqlDbType.Int, 4, value);
                }
            }
        }

        public string Field1Equal
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Field1=@Field1 ");
                    base.InsertParameter("@Field1", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string FileNumberEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" FileNumber=@FileNumber ");
                    base.InsertParameter("@FileNumber", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string FileTypeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" FileType=@FileType ");
                    base.InsertParameter("@FileType", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string InFileCodeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" InFileCode=@InFileCode ");
                    base.InsertParameter("@InFileCode", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public DateTime InFileDateEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" InFileDate=@InFileDate ");
                    base.InsertParameter("@InFileDate", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public string OriginalFileCodeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" OriginalFileCode=@OriginalFileCode ");
                    base.InsertParameter("@OriginalFileCode", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string RegisterMainCodeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" RegisterMainCode=@RegisterMainCode ");
                    base.InsertParameter("@RegisterMainCode", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string RemarkEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Remark=@Remark ");
                    base.InsertParameter("@Remark", SqlDbType.VarChar, 500, value);
                }
            }
        }
    }
}

