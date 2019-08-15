namespace RmsOA.MODEL
{
    using System;
    using System.Data;

    public class UserHelpGroupQueryModel : QueryBaseModel
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

        public DateTime CreateTimeEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" CreateTime=@CreateTime ");
                    base.InsertParameter("@CreateTime", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public string GroupNameEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" GroupName=@GroupName ");
                    base.InsertParameter("@GroupName", SqlDbType.VarChar, 200, value);
                }
            }
        }

        public string UserCodeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" UserCode=@UserCode ");
                    base.InsertParameter("@UserCode", SqlDbType.VarChar, 50, value);
                }
            }
        }
    }
}

