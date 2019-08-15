namespace RmsDM.MODEL
{
    using System;
    using System.Data;

    public class DocumentDirectoryQueryModel : QueryBaseModel
    {
        public int CodeEqual
        {
            set
            {
                base.QueryConditionStrAdd(" Code=@Code ");
                base.InsertParameter("@Code", SqlDbType.Int, 4, value);
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

        public int DeepEqual
        {
            set
            {
                if (value != 0)
                {
                    base.QueryConditionStrAdd(" Deep=@Deep ");
                    base.InsertParameter("@Deep", SqlDbType.Int, 4, value);
                }
            }
        }

        public string DepartmentCodeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" DepartmentCode=@DepartmentCode ");
                    base.InsertParameter("@DepartmentCode", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string DirectoryNameEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" DirectoryName=@DirectoryName ");
                    base.InsertParameter("@DirectoryName", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string DirectoryNodeCodeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" DirectoryNodeCode=@DirectoryNodeCode ");
                    base.InsertParameter("@DirectoryNodeCode", SqlDbType.VarChar, 50, value);
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

        public string FullIDEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" FullID=@FullID ");
                    base.InsertParameter("@FullID", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public int OrderByIDEqual
        {
            set
            {
                if (value != 0)
                {
                    base.QueryConditionStrAdd(" OrderByID=@OrderByID ");
                    base.InsertParameter("@OrderByID", SqlDbType.Int, 4, value);
                }
            }
        }

        public int ParentCodeEqual
        {
            set
            {
                base.QueryConditionStrAdd(" ParentCode=@ParentCode ");
                base.InsertParameter("@ParentCode", SqlDbType.Int, 4, value);
            }
        }
    }
}

