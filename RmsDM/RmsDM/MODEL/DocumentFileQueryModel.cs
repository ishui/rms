namespace RmsDM.MODEL
{
    using System;
    using System.Data;

    public class DocumentFileQueryModel : QueryBaseModel
    {
        public DateTime ApplyDateTimeEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" ApplyDateTime=@ApplyDateTime ");
                    base.InsertParameter("@ApplyDateTime", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public string ApplyDepartmentCodeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" ApplyDepartmentCode=@ApplyDepartmentCode ");
                    base.InsertParameter("@ApplyDepartmentCode", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string ApplyUserCodeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" ApplyUserCode=@ApplyUserCode ");
                    base.InsertParameter("@ApplyUserCode", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public DateTime ArchiveDatetimeEqualEnd
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" ArchiveDatetime<=@ArchiveDatetimeEnd ");
                    base.InsertParameter("@ArchiveDatetimeEnd", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public DateTime ArchiveDatetimeEqualStart
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" ArchiveDatetime>=@ArchiveDatetimeStart ");
                    base.InsertParameter("@ArchiveDatetimeStart", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public string ArchiveStateEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" ArchiveState=@ArchiveState ");
                    base.InsertParameter("@ArchiveState", SqlDbType.VarChar, 100, value);
                }
            }
        }

        public string ArchiveTypeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" ArchiveType=@ArchiveType ");
                    base.InsertParameter("@ArchiveType", SqlDbType.VarChar, 200, value);
                }
            }
        }

        public DateTime AuditingDatetimeEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" AuditingDatetime=@AuditingDatetime ");
                    base.InsertParameter("@AuditingDatetime", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public string AuditingStateEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" AuditingState=@AuditingState ");
                    base.InsertParameter("@AuditingState", SqlDbType.VarChar, 100, value);
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

        public int CountsEqual
        {
            set
            {
                if (value != 0)
                {
                    base.QueryConditionStrAdd(" Counts=@Counts ");
                    base.InsertParameter("@Counts", SqlDbType.Int, 4, value);
                }
            }
        }

        public DateTime CreateDateEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" CreateDate=@CreateDate ");
                    base.InsertParameter("@CreateDate", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public string CreateUserCodeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" CreateUserCode=@CreateUserCode ");
                    base.InsertParameter("@CreateUserCode", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string DeleteFlagEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" DeleteFlag=@DeleteFlag ");
                    base.InsertParameter("@DeleteFlag", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string DoucmentMarkingSNEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" DoucmentMarkingSN like @DoucmentMarkingSN ");
                    base.InsertParameter("@DoucmentMarkingSN", SqlDbType.VarChar, 100, "%" + value + "%");
                }
            }
        }

        public string FileCodeEqul
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" FileCode like @FileCode ");
                    base.InsertParameter("@FileCode", SqlDbType.VarChar, 100, "%" + value + "%");
                }
            }
        }

        public int FileTemplateCodeEqual
        {
            set
            {
                if (value != 0)
                {
                    base.QueryConditionStrAdd(" FileTemplateCode=@FileTemplateCode ");
                    base.InsertParameter("@FileTemplateCode", SqlDbType.Int, 4, value);
                }
            }
        }

        public string FileTemplateCodeIn
        {
            set
            {
                if (value != null)
                {
                    string[] textArray = value.Split(",".ToCharArray());
                    string text = "";
                    for (int i = 0; i < textArray.Length; i++)
                    {
                        if (text != "")
                        {
                            text = text + ",";
                        }
                        text = text + string.Format("@FileTemplateCode{0}", i.ToString());
                        base.InsertParameter(string.Format("@FileTemplateCode{0}", i.ToString()), SqlDbType.Int, 4, textArray[i]);
                    }
                    base.QueryConditionStrAdd(" FileTemplateCode in (" + text + ")");
                }
            }
        }

        public string LastModifyByUserCodeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" LastModifyByUserCode=@LastModifyByUserCode ");
                    base.InsertParameter("@LastModifyByUserCode", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public DateTime LastModifyDatetimeEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" LastModifyDatetime=@LastModifyDatetime ");
                    base.InsertParameter("@LastModifyDatetime", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public int LeavesEqual
        {
            set
            {
                if (value != 0)
                {
                    base.QueryConditionStrAdd(" Leaves=@Leaves ");
                    base.InsertParameter("@Leaves", SqlDbType.Int, 4, value);
                }
            }
        }

        public string OperationTypeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" OperationType=@OperationType ");
                    base.InsertParameter("@OperationType", SqlDbType.VarChar, 50, value);
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
                    base.InsertParameter("@Remark", SqlDbType.VarChar, 0x1f40, value);
                }
            }
        }

        public string SortCodeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" SortCode=@SortCode ");
                    base.InsertParameter("@SortCode", SqlDbType.VarChar, 0x3e8, value);
                }
            }
        }

        public string SubjectEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Subject like @Subject ");
                    base.InsertParameter("@Subject", SqlDbType.VarChar, 100, "%" + value + "%");
                }
            }
        }

        public string VersionNumberEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" VersionNumber=@VersionNumber ");
                    base.InsertParameter("@VersionNumber", SqlDbType.VarChar, 100, value);
                }
            }
        }
    }
}

