namespace RmsOA.MODEL
{
    using System;
    using System.Data;

    public class UserHelpUserQueryModel : QueryBaseModel
    {
        public DateTime AddDateEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" AddDate=@AddDate ");
                    base.InsertParameter("@AddDate", SqlDbType.DateTime, 8, value);
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

        public int GroupCodeEqual
        {
            set
            {
                if (value != 0)
                {
                    base.QueryConditionStrAdd(" GroupCode=@GroupCode ");
                    base.InsertParameter("@GroupCode", SqlDbType.Int, 4, value);
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

