namespace RmsOA.MODEL
{
    using System;
    using System.Data;

    public class ConferenceUserListQueryModel : QueryBaseModel
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

        public int ConferenceCodeEqual
        {
            set
            {
                if (value != 0)
                {
                    base.QueryConditionStrAdd(" ConferenceCode=@ConferenceCode ");
                    base.InsertParameter("@ConferenceCode", SqlDbType.Int, 4, value);
                }
            }
        }

        public string StateEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" State=@State ");
                    base.InsertParameter("@State", SqlDbType.VarChar, 50, value);
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

        public string UserNameEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" UserName=@UserName ");
                    base.InsertParameter("@UserName", SqlDbType.VarChar, 50, value);
                }
            }
        }
    }
}

