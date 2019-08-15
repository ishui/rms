﻿namespace RmsOA.MODEL
{
    using System;
    using System.Data;

    public class GK_OAPersonWorkQueryModel : QueryBaseModel
    {
        public DateTime BeginDateEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" BeginDate=@BeginDate ");
                    base.InsertParameter("@BeginDate", SqlDbType.DateTime, 8, value);
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

        public string DutyEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Duty=@Duty ");
                    base.InsertParameter("@Duty", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public DateTime EndDateEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" EndDate=@EndDate ");
                    base.InsertParameter("@EndDate", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public string JobPostEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" JobPost=@JobPost ");
                    base.InsertParameter("@JobPost", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string PersonIDEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" PersonID=@PersonID ");
                    base.InsertParameter("@PersonID", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string WorkUnitEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" WorkUnit=@WorkUnit ");
                    base.InsertParameter("@WorkUnit", SqlDbType.VarChar, 50, value);
                }
            }
        }
    }
}

