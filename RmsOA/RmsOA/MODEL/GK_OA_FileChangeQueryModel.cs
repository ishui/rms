namespace RmsOA.MODEL
{
    using System;
    using System.Data;

    public class GK_OA_FileChangeQueryModel : QueryBaseModel
    {
        public string ChangeReasonEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" ChangeReason=@ChangeReason ");
                    base.InsertParameter("@ChangeReason", SqlDbType.VarChar, 500, value);
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

        public string FileCodeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" FileCode=@FileCode ");
                    base.InsertParameter("@FileCode", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string FileNameEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" FileName=@FileName ");
                    base.InsertParameter("@FileName", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string FileSystemCodeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" FileSystemCode=@FileSystemCode ");
                    base.InsertParameter("@FileSystemCode", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string NewContextEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" NewContext=@NewContext ");
                    base.InsertParameter("@NewContext", SqlDbType.VarChar, 500, value);
                }
            }
        }

        public string NewVersionNumberEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" NewVersionNumber=@NewVersionNumber ");
                    base.InsertParameter("@NewVersionNumber", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string OldContextEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" OldContext=@OldContext ");
                    base.InsertParameter("@OldContext", SqlDbType.VarChar, 500, value);
                }
            }
        }

        public string OldVersionNumberEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" OldVersionNumber=@OldVersionNumber ");
                    base.InsertParameter("@OldVersionNumber", SqlDbType.VarChar, 50, value);
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

        public DateTime SubmitDateEqualEnd
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" SubmitDate<=@SubmitDateEnd");
                    base.InsertParameter("@SubmitDateEnd", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public DateTime SubmitDateEqualStart
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" SubmitDate>=@SubmitDateStart ");
                    base.InsertParameter("@SubmitDateStart", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public string SystemCodeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" SystemCode=@SystemCode ");
                    base.InsertParameter("@SystemCode", SqlDbType.VarChar, 50, value);
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
    }
}

