namespace RmsOA.MODEL
{
    using System;
    using System.Data;

    public class YF_AssetManageQueryModel : QueryBaseModel
    {
        public string AreaEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Area=@Area ");
                    base.InsertParameter("@Area", SqlDbType.VarChar, 100, value);
                }
            }
        }

        public DateTime BookINTimeEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" BookINTime=@BookINTime ");
                    base.InsertParameter("@BookINTime", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public string BuyCorpEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" BuyCorp=@BuyCorp ");
                    base.InsertParameter("@BuyCorp", SqlDbType.VarChar, 100, value);
                }
            }
        }

        public DateTime BuyDateEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" BuyDate=@BuyDate ");
                    base.InsertParameter("@BuyDate", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public string BuyTypeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" BuyType=@BuyType ");
                    base.InsertParameter("@BuyType", SqlDbType.VarChar, 50, value);
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

        public string CodeNOEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" CodeNO=@CodeNO ");
                    base.InsertParameter("@CodeNO", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string CountsEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Counts=@Counts ");
                    base.InsertParameter("@Counts", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string CountUnitEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" CountUnit=@CountUnit ");
                    base.InsertParameter("@CountUnit", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string DeptUnitEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" DeptUnit=@DeptUnit ");
                    base.InsertParameter("@DeptUnit", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string EquiTypeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" EquiType=@EquiType ");
                    base.InsertParameter("@EquiType", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string FacilityNameEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" FacilityName=@FacilityName ");
                    base.InsertParameter("@FacilityName", SqlDbType.VarChar, 100, value);
                }
            }
        }

        public string FreeMainEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" FreeMain=@FreeMain ");
                    base.InsertParameter("@FreeMain", SqlDbType.VarChar, 20, value);
                }
            }
        }

        public string LayCorpEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" LayCorp=@LayCorp ");
                    base.InsertParameter("@LayCorp", SqlDbType.VarChar, 100, value);
                }
            }
        }

        public string LayPlaceEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" LayPlace=@LayPlace ");
                    base.InsertParameter("@LayPlace", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string MainCardPlaceEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" MainCardPlace=@MainCardPlace ");
                    base.InsertParameter("@MainCardPlace", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string ModelNOEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" ModelNO=@ModelNO ");
                    base.InsertParameter("@ModelNO", SqlDbType.VarChar, 100, value);
                }
            }
        }

        public decimal PriceEqual
        {
            set
            {
                if (value != 0M)
                {
                    base.QueryConditionStrAdd(" Price=@Price ");
                    base.InsertParameter("@Price", SqlDbType.Decimal, 9, value);
                }
            }
        }

        public string ProdAreaEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" ProdArea=@ProdArea ");
                    base.InsertParameter("@ProdArea", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string ProducerEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Producer=@Producer ");
                    base.InsertParameter("@Producer", SqlDbType.VarChar, 100, value);
                }
            }
        }

        public string ProviderEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Provider=@Provider ");
                    base.InsertParameter("@Provider", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public DateTime QueryEndTimeEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" BuyDate <= @EndTime ");
                    base.InsertParameter("@EndTime", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public DateTime QueryStartTimeEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" BuyDate >= @StartTime ");
                    base.InsertParameter("@StartTime", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public string RegisterEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Register=@Register ");
                    base.InsertParameter("@Register", SqlDbType.VarChar, 50, value);
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

        public string SortNOEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" SortNO=@SortNO ");
                    base.InsertParameter("@SortNO", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string SortTypeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" SortType=@SortType ");
                    base.InsertParameter("@SortType", SqlDbType.VarChar, 50, value);
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

        public string StoreStatusEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" StoreStatus=@StoreStatus ");
                    base.InsertParameter("@StoreStatus", SqlDbType.VarChar, 50, value);
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

        public string UseDeptEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" UseDept=@UseDept ");
                    base.InsertParameter("@UseDept", SqlDbType.VarChar, 50, value);
                }
            }
        }
    }
}

