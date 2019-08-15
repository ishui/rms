namespace RmsOA.MODEL
{
    using System;
    using System.Data;

    public class YF_AssetTransferQueryModel : QueryBaseModel
    {
        public string ApplyerEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Applyer=@Applyer ");
                    base.InsertParameter("@Applyer", SqlDbType.VarChar, 50, value);
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

        public string PostDeptEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" PostDept=@PostDept ");
                    base.InsertParameter("@PostDept", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string PreDeptEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" PreDept=@PreDept ");
                    base.InsertParameter("@PreDept", SqlDbType.VarChar, 50, value);
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

        public string TransferTimeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" TransferTime=@TransferTime ");
                    base.InsertParameter("@TransferTime", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string UnitCodeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" UnitCode=@UnitCode ");
                    base.InsertParameter("@UnitCode", SqlDbType.VarChar, 50, value);
                }
            }
        }
    }
}

