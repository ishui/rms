namespace RmsOA.MODEL
{
    using System;
    using System.Data;

    public class YF_UseCarFlowQueryModel : QueryBaseModel
    {
        public string AdressToEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" AdressTo=@AdressTo ");
                    base.InsertParameter("@AdressTo", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string ApplayDateTimeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" ApplayDateTime=@ApplayDateTime ");
                    base.InsertParameter("@ApplayDateTime", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string BillCodeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" BillCode=@BillCode ");
                    base.InsertParameter("@BillCode", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string CarCodeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" CarCode=@CarCode ");
                    base.InsertParameter("@CarCode", SqlDbType.VarChar, 50, value);
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
                    base.InsertParameter("@Content", SqlDbType.VarChar, 0x10, value);
                }
            }
        }

        public string DepartCodeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" DepartCode=@DepartCode ");
                    base.InsertParameter("@DepartCode", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string DriverEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Driver=@Driver ");
                    base.InsertParameter("@Driver", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public DateTime EndDateTimeEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" EndDateTime<=@EndDateTime ");
                    base.InsertParameter("@EndDateTime", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public string GusetPersonEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" GusetPerson=@GusetPerson ");
                    base.InsertParameter("@GusetPerson", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string RunKilometresEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" RunKilometres=@RunKilometres ");
                    base.InsertParameter("@RunKilometres", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public DateTime StartDateTimeEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" StartDateTime>=@StartDateTime ");
                    base.InsertParameter("@StartDateTime", SqlDbType.DateTime, 8, value);
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

        public string UsePersonEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" UsePerson=@UsePerson ");
                    base.InsertParameter("@UsePerson", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public DateTime UserCarDateTimeEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" UserCarDateTime=@UserCarDateTime ");
                    base.InsertParameter("@UserCarDateTime", SqlDbType.DateTime, 8, value);
                }
            }
        }
    }
}

