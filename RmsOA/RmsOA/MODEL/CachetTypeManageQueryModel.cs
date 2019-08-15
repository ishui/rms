namespace RmsOA.MODEL
{
    using System;
    using System.Data;

    public class CachetTypeManageQueryModel : QueryBaseModel
    {
        public string CachetNameEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" CachetName=@CachetName ");
                    base.InsertParameter("@CachetName", SqlDbType.VarChar, 100, value);
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

        public string TypeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Type=@Type ");
                    base.InsertParameter("@Type", SqlDbType.VarChar, 50, value);
                }
            }
        }
    }
}

