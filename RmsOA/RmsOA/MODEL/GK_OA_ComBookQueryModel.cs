namespace RmsOA.MODEL
{
    using System;
    using System.Data;

    public class GK_OA_ComBookQueryModel : QueryBaseModel
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

        public string CompanyEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Company=@Company ");
                    base.InsertParameter("@Company", SqlDbType.VarChar, 50, value);
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
                    base.InsertParameter("@Email", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string HandleTelephoneEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" HandleTelephone=@HandleTelephone ");
                    base.InsertParameter("@HandleTelephone", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string MSNEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" MSN=@MSN ");
                    base.InsertParameter("@MSN", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string PrepField1Equal
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" PrepField1=@PrepField1 ");
                    base.InsertParameter("@PrepField1", SqlDbType.VarChar, 200, value);
                }
            }
        }

        public string PrepField2Equal
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" PrepField2=@PrepField2 ");
                    base.InsertParameter("@PrepField2", SqlDbType.VarChar, 200, value);
                }
            }
        }

        public string PrepField3Equal
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" PrepField3=@PrepField3 ");
                    base.InsertParameter("@PrepField3", SqlDbType.VarChar, 200, value);
                }
            }
        }

        public string QQEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" QQ=@QQ ");
                    base.InsertParameter("@QQ", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string TelephoneEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Telephone=@Telephone ");
                    base.InsertParameter("@Telephone", SqlDbType.VarChar, 50, value);
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

        public string UrgePhoneEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" UrgePhone=@UrgePhone ");
                    base.InsertParameter("@UrgePhone", SqlDbType.VarChar, 100, value);
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

