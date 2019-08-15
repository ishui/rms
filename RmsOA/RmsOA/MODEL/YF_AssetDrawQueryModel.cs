namespace RmsOA.MODEL
{
    using System;
    using System.Data;

    public class YF_AssetDrawQueryModel : QueryBaseModel
    {
        public DateTime BackTimeEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" BackTime=@BackTime ");
                    base.InsertParameter("@BackTime", SqlDbType.DateTime, 8, value);
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

        public DateTime DrawDateEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" DrawDate=@DrawDate ");
                    base.InsertParameter("@DrawDate", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public string DrawPersonEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" DrawPerson=@DrawPerson ");
                    base.InsertParameter("@DrawPerson", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string DrawUnitEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" DrawUnit=@DrawUnit ");
                    base.InsertParameter("@DrawUnit", SqlDbType.VarChar, 50, value);
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

        public string StatusEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Status=@Status ");
                    base.InsertParameter("@Status", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string UnitEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Unit=@Unit ");
                    base.InsertParameter("@Unit", SqlDbType.VarChar, 50, value);
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

