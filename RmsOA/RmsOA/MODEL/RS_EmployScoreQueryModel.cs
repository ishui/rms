namespace RmsOA.MODEL
{
    using System;
    using System.Data;

    public class RS_EmployScoreQueryModel : QueryBaseModel
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

        public int ManageCodeEqual
        {
            set
            {
                if (value != 0)
                {
                    base.QueryConditionStrAdd(" ManageCode=@ManageCode ");
                    base.InsertParameter("@ManageCode", SqlDbType.Int, 4, value);
                }
            }
        }

        public int ScoreEqual
        {
            set
            {
                if (value != 0)
                {
                    base.QueryConditionStrAdd(" Score=@Score ");
                    base.InsertParameter("@Score", SqlDbType.Int, 4, value);
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
                    base.InsertParameter("@UserCode", SqlDbType.VarChar, 10, value);
                }
            }
        }
    }
}

