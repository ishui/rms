namespace RmsOA.MODEL
{
    using System;
    using System.Data;

    public class GK_OA_AssetTransferQueryModel : QueryBaseModel
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

        public DateTime EndSubTimeEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" SubTime=@EndTime ");
                    base.InsertParameter("@EndTime", SqlDbType.DateTime, 8, value);
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
                    base.InsertParameter("@Name", SqlDbType.VarChar, 200, value);
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
                    base.InsertParameter("@Number", SqlDbType.VarChar, 50, value);
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

        public string PostUserEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" PostUser=@PostUser ");
                    base.InsertParameter("@PostUser", SqlDbType.VarChar, 50, value);
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
                    base.InsertParameter("@QualityNO", SqlDbType.VarChar, 50, value);
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

        public string SNRuleEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" SNRule=@SNRule ");
                    base.InsertParameter("@SNRule", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string SortEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Sort=@Sort ");
                    base.InsertParameter("@Sort", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public DateTime StartSubTimeEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" SubTime=@StartTime ");
                    base.InsertParameter("@StartTime", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public string StatusEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Status in (" + value + ")");
                }
            }
        }

        public string SubmiterEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Submiter=@Submiter ");
                    base.InsertParameter("@Submiter", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public DateTime SubTimeEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" SubTime=@SubTime ");
                    base.InsertParameter("@SubTime", SqlDbType.DateTime, 8, value);
                }
            }
        }
    }
}

