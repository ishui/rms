namespace RmsOA.MODEL
{
    using System;
    using System.Data;

    public class YF_AssetMainRecordQueryModel : QueryBaseModel
    {
        public string ChangeDetailEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" ChangeDetail=@ChangeDetail ");
                    base.InsertParameter("@ChangeDetail", SqlDbType.VarChar, 0x7d0, value);
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

        public string ContentEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Content=@Content ");
                    base.InsertParameter("@Content", SqlDbType.VarChar, 0x7d0, value);
                }
            }
        }

        public decimal CostMoneyEqual
        {
            set
            {
                if (value != 0M)
                {
                    base.QueryConditionStrAdd(" CostMoney=@CostMoney ");
                    base.InsertParameter("@CostMoney", SqlDbType.Decimal, 9, value);
                }
            }
        }

        public int CostTimeEqual
        {
            set
            {
                if (value != 0)
                {
                    base.QueryConditionStrAdd(" CostTime=@CostTime ");
                    base.InsertParameter("@CostTime", SqlDbType.Int, 4, value);
                }
            }
        }

        public int FKCodeEqual
        {
            set
            {
                if (value != 0)
                {
                    base.QueryConditionStrAdd(" FKCode=@FKCode ");
                    base.InsertParameter("@FKCode", SqlDbType.Int, 4, value);
                }
            }
        }

        public DateTime MainTimeEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" MainTime=@MainTime ");
                    base.InsertParameter("@MainTime", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public string MainUserEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" MainUser=@MainUser ");
                    base.InsertParameter("@MainUser", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string ResultEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Result=@Result ");
                    base.InsertParameter("@Result", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string UserSignEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" UserSign=@UserSign ");
                    base.InsertParameter("@UserSign", SqlDbType.VarChar, 50, value);
                }
            }
        }
    }
}

