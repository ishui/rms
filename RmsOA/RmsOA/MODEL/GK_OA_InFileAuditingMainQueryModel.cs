namespace RmsOA.MODEL
{
    using System;
    using System.Data;

    public class GK_OA_InFileAuditingMainQueryModel : QueryBaseModel
    {
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

        public string FileCodeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" FileCode=@FileCode ");
                    base.InsertParameter("@FileCode", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string StatusEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Status in (" + value + ")");
                }
            }
        }

        public string SystemCodeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" SystemCode=@SystemCode ");
                    base.InsertParameter("@SystemCode", SqlDbType.VarChar, 50, value);
                }
            }
        }
    }
}

