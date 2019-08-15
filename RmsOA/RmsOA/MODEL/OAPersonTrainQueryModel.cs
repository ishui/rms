namespace RmsOA.MODEL
{
    using System;
    using System.Data;

    public class OAPersonTrainQueryModel : QueryBaseModel
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

        public string TRAIN_CONTENTEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" TRAIN_CONTENT=@TRAIN_CONTENT ");
                    base.InsertParameter("@TRAIN_CONTENT", SqlDbType.VarChar, 0x10, value);
                }
            }
        }

        public string TRAIN_HOUREqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" TRAIN_HOUR=@TRAIN_HOUR ");
                    base.InsertParameter("@TRAIN_HOUR", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string TRAIN_METHODEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" TRAIN_METHOD=@TRAIN_METHOD ");
                    base.InsertParameter("@TRAIN_METHOD", SqlDbType.VarChar, 50, value);
                }
            }
        }
    }
}

