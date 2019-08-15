namespace TiannuoPM.MODEL
{
    using System;
    using System.Data;

    public class DesignChangeQueryModel : QueryBaseModel
    {
        public string ChangeMoneyEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" ChangeMoney=@ChangeMoney ");
                    base.InsertParameter("@ChangeMoney", SqlDbType.VarChar, 0x7d0, value);
                }
            }
        }

        public string ChangeType
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" ChangeType=@ChangeType ");
                    base.InsertParameter("@ChangeType", SqlDbType.VarChar, 50, value);
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

        public string ContractEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Contract=@Contract ");
                    base.InsertParameter("@Contract", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public DateTime DateEndCheck
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" Date<@DateEnd ");
                    base.InsertParameter("@DateEnd", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public DateTime DateEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" Date=@Date ");
                    base.InsertParameter("@Date", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public DateTime DateStartCheck
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" Date>@DateStart ");
                    base.InsertParameter("@DateStart", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public string DesignerEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Designer=@Designer ");
                    base.InsertParameter("@Designer", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string FlagEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Flag=@Flag ");
                    base.InsertParameter("@Flag", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string NumberEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd("Number Like '%' + @Number + '%' ");
                    base.InsertParameter("@Number", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string PersonEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Person=@Person ");
                    base.InsertParameter("@Person", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string ProjectNameEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" ProjectName=@ProjectName ");
                    base.InsertParameter("@ProjectName", SqlDbType.VarChar, 50, value);
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

        public int ReferCode
        {
            set
            {
                if (value != 0)
                {
                    base.QueryConditionStrAdd(" ReferCode=@ReferCode ");
                    base.InsertParameter("@ReferCode", SqlDbType.Int, 4, value);
                }
            }
        }

        public string RelationNumberEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" RelationNumber=@RelationNumber ");
                    base.InsertParameter("@RelationNumber", SqlDbType.VarChar, 50, value);
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
                    base.InsertParameter("@Remark", SqlDbType.VarChar, 0x7d0, value);
                }
            }
        }

        public string SolutionNameLike
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" SolutionName like @SolutionNamelike ");
                    base.InsertParameter("@SolutionNamelike", SqlDbType.VarChar, 50, "%" + value + "%");
                }
            }
        }

        public string SpecialtyEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Specialty=@Specialty ");
                    base.InsertParameter("@Specialty", SqlDbType.VarChar, 50, value);
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

        public string StateIn
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" State in (" + value + ") ");
                }
            }
        }

        public string SupplierEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Supplier=@Supplier ");
                    base.InsertParameter("@Supplier", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public DateTime TerminatingDateEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" TerminatingDate=@TerminatingDate ");
                    base.InsertParameter("@TerminatingDate", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public string TerminatingPersonEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" TerminatingPerson=@TerminatingPerson ");
                    base.InsertParameter("@TerminatingPerson", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public decimal TotalMoneyEqual
        {
            set
            {
                if (value != 0M)
                {
                    base.QueryConditionStrAdd(" TotalMoney=@TotalMoney ");
                    base.InsertParameter("@TotalMoney", SqlDbType.Decimal, 9, value);
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
    }
}

