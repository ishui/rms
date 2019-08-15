namespace RmsOA.MODEL
{
    using System;
    using System.Data;

    public class OAPersonHomeQueryModel : QueryBaseModel
    {
        public string appnameEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" appname=@appname ");
                    base.InsertParameter("@appname", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string cnameEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" cname=@cname ");
                    base.InsertParameter("@cname", SqlDbType.VarChar, 50, value);
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

        public string dutyEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" duty=@duty ");
                    base.InsertParameter("@duty", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string educationalEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" educational=@educational ");
                    base.InsertParameter("@educational", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string idcardEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" idcard=@idcard ");
                    base.InsertParameter("@idcard", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string monthearnEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" monthearn=@monthearn ");
                    base.InsertParameter("@monthearn", SqlDbType.VarChar, 50, value);
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

        public string phoneEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" phone=@phone ");
                    base.InsertParameter("@phone", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string polityEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" polity=@polity ");
                    base.InsertParameter("@polity", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string workplaceEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" workplace=@workplace ");
                    base.InsertParameter("@workplace", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string yesnoEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" yesno=@yesno ");
                    base.InsertParameter("@yesno", SqlDbType.VarChar, 50, value);
                }
            }
        }
    }
}

