namespace RmsOA.MODEL
{
    using System;
    using System.Data;

    public class OAPersonContractQueryModel : QueryBaseModel
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

        public string ContractCodeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" ContractCode=@ContractCode ");
                    base.InsertParameter("@ContractCode", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string EndDateEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" EndDate=@EndDate ");
                    base.InsertParameter("@EndDate", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string PersonIDEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" PersonID=@PersonID ");
                    base.InsertParameter("@PersonID", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string RegisterDateEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" RegisterDate=@RegisterDate ");
                    base.InsertParameter("@RegisterDate", SqlDbType.VarChar, 50, value);
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
                    base.InsertParameter("@Remark", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string StartDateEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" StartDate=@StartDate ");
                    base.InsertParameter("@StartDate", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string StationCodeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" StationCode=@StationCode ");
                    base.InsertParameter("@StationCode", SqlDbType.VarChar, 50, value);
                }
            }
        }
    }
}

