namespace RmsOA.MODEL
{
    using System;
    using System.Data;

    public class GK_OA_MaterialTransferQueryModel : QueryBaseModel
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

        public DateTime EndDateEqual
        {
            set
            {
                base.QueryConditionStrAdd(" TransferDate <= @EndDate ");
                base.InsertParameter("@EndDate", SqlDbType.DateTime, 8, value);
            }
        }

        public string LaterDeptEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" LaterDept=@LaterDept ");
                    base.InsertParameter("@LaterDept", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string LaterUserEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" LaterUser=@LaterUser ");
                    base.InsertParameter("@LaterUser", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string NameEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Name=@Name ");
                    base.InsertParameter("@Name", SqlDbType.VarChar, 100, value);
                }
            }
        }

        public string NumberEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Number=@Number ");
                    base.InsertParameter("@Number", SqlDbType.VarChar, 100, value);
                }
            }
        }

        public string NumUnitEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" NumUnit=@NumUnit ");
                    base.InsertParameter("@NumUnit", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public decimal OriginalPriceEqual
        {
            set
            {
                if (value != 0M)
                {
                    base.QueryConditionStrAdd(" OriginalPrice=@OriginalPrice ");
                    base.InsertParameter("@OriginalPrice", SqlDbType.Decimal, 9, value);
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
                    base.InsertParameter("@PreDept", SqlDbType.VarChar, 100, value);
                }
            }
        }

        public string PreUserEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" PreUser=@PreUser ");
                    base.InsertParameter("@PreUser", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string QualityNOEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" QualityNO=@QualityNO ");
                    base.InsertParameter("@QualityNO", SqlDbType.VarChar, 100, value);
                }
            }
        }

        public string ReasonEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Reason=@Reason ");
                    base.InsertParameter("@Reason", SqlDbType.VarChar, 0x7d0, value);
                }
            }
        }

        public DateTime ReciveDateEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" ReciveDate=@ReciveDate ");
                    base.InsertParameter("@ReciveDate", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public string ReciveHanderEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" ReciveHander=@ReciveHander ");
                    base.InsertParameter("@ReciveHander", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string ReciveMasterEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" ReciveMaster=@ReciveMaster ");
                    base.InsertParameter("@ReciveMaster", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string SNRuleEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" SNRule=@SNRule ");
                    base.InsertParameter("@SNRule", SqlDbType.VarChar, 100, value);
                }
            }
        }

        public DateTime StartDateEqual
        {
            set
            {
                base.QueryConditionStrAdd(" TransferDate >=@StarteDate ");
                base.InsertParameter("@StarteDate", SqlDbType.DateTime, 8, value);
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

        public DateTime TransferDateEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" TransferDate=@TransferDate ");
                    base.InsertParameter("@TransferDate", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public string TransferHanderEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" TransferHander=@TransferHander ");
                    base.InsertParameter("@TransferHander", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string TransferMasterEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" TransferMaster=@TransferMaster ");
                    base.InsertParameter("@TransferMaster", SqlDbType.VarChar, 50, value);
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
                    base.InsertParameter("@Type", SqlDbType.VarChar, 100, value);
                }
            }
        }
    }
}

