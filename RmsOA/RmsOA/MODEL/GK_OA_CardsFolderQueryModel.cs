namespace RmsOA.MODEL
{
    using System;
    using System.Data;

    public class GK_OA_CardsFolderQueryModel : QueryBaseModel
    {
        public int AgeEqual
        {
            set
            {
                if (value != 0)
                {
                    base.QueryConditionStrAdd(" Age=@Age ");
                    base.InsertParameter("@Age", SqlDbType.Int, 4, value);
                }
            }
        }

        public DateTime BirthdayEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" Birthday=@Birthday ");
                    base.InsertParameter("@Birthday", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public string CardTypeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" CardType=@CardType ");
                    base.InsertParameter("@CardType", SqlDbType.VarChar, 50, value);
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

        public string CompanyAddressEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" CompanyAddress=@CompanyAddress ");
                    base.InsertParameter("@CompanyAddress", SqlDbType.VarChar, 500, value);
                }
            }
        }

        public string CompanyNameEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" CompanyName=@CompanyName ");
                    base.InsertParameter("@CompanyName", SqlDbType.VarChar, 200, value);
                }
            }
        }

        public DateTime ContactTimeEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" ContactTime=@ContactTime ");
                    base.InsertParameter("@ContactTime", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public string DeptEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Dept=@Dept ");
                    base.InsertParameter("@Dept", SqlDbType.VarChar, 200, value);
                }
            }
        }

        public string EmailEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Email=@Email ");
                    base.InsertParameter("@Email", SqlDbType.VarChar, 200, value);
                }
            }
        }

        public string FaxEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Fax=@Fax ");
                    base.InsertParameter("@Fax", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string HeadshipEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Headship=@Headship ");
                    base.InsertParameter("@Headship", SqlDbType.VarChar, 200, value);
                }
            }
        }

        public string HobbyEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Hobby=@Hobby ");
                    base.InsertParameter("@Hobby", SqlDbType.VarChar, 400, value);
                }
            }
        }

        public string HomeAddressEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" HomeAddress=@HomeAddress ");
                    base.InsertParameter("@HomeAddress", SqlDbType.VarChar, 200, value);
                }
            }
        }

        public string HomePhoneEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" HomePhone=@HomePhone ");
                    base.InsertParameter("@HomePhone", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string MobileEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Mobile=@Mobile ");
                    base.InsertParameter("@Mobile", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string NativePlaceEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" NativePlace=@NativePlace ");
                    base.InsertParameter("@NativePlace", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string NetAddressEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" NetAddress=@NetAddress ");
                    base.InsertParameter("@NetAddress", SqlDbType.VarChar, 200, value);
                }
            }
        }

        public string PhoneEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Phone=@Phone ");
                    base.InsertParameter("@Phone", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string PostalcodeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Postalcode=@Postalcode ");
                    base.InsertParameter("@Postalcode", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string PublicStatusEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" PublicStatus=@PublicStatus ");
                    base.InsertParameter("@PublicStatus", SqlDbType.VarChar, 10, value);
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

        public string SexEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Sex=@Sex ");
                    base.InsertParameter("@Sex", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string UserIdEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" UserId=@UserId ");
                    base.InsertParameter("@UserId", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string UserNameEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" UserName=@UserName ");
                    base.InsertParameter("@UserName", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string WedlockEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Wedlock=@Wedlock ");
                    base.InsertParameter("@Wedlock", SqlDbType.VarChar, 50, value);
                }
            }
        }
    }
}

