namespace RmsOA.MODEL
{
    using System;
    using System.Data;

    public class OAPersonStudyQueryModel : QueryBaseModel
    {
        public string BEGIN_DATEEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" BEGIN_DATE=@BEGIN_DATE ");
                    base.InsertParameter("@BEGIN_DATE", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string CertifierEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Certifier=@Certifier ");
                    base.InsertParameter("@Certifier", SqlDbType.VarChar, 50, value);
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

        public string DEGREEEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" DEGREE=@DEGREE ");
                    base.InsertParameter("@DEGREE", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string END_DATEEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" END_DATE=@END_DATE ");
                    base.InsertParameter("@END_DATE", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string LETTER_NAMEEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" LETTER_NAME=@LETTER_NAME ");
                    base.InsertParameter("@LETTER_NAME", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string personidEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" personid=@personid ");
                    base.InsertParameter("@personid", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string SCHOOL_NAMEEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" SCHOOL_NAME=@SCHOOL_NAME ");
                    base.InsertParameter("@SCHOOL_NAME", SqlDbType.VarChar, 100, value);
                }
            }
        }

        public string SPECIALITYEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" SPECIALITY=@SPECIALITY ");
                    base.InsertParameter("@SPECIALITY", SqlDbType.VarChar, 100, value);
                }
            }
        }
    }
}

