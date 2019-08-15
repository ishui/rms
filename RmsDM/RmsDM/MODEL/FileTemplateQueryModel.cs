namespace RmsDM.MODEL
{
    using System;
    using System.Data;

    public class FileTemplateQueryModel : QueryBaseModel
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

        public string FileTemplateNameEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" FileTemplateName=@FileTemplateName ");
                    base.InsertParameter("@FileTemplateName", SqlDbType.VarChar, 100, value);
                }
            }
        }

        public int FileTemplateTypeCodeEqual
        {
            set
            {
                if (value != 0)
                {
                    base.QueryConditionStrAdd(" FileTemplateTypeCode=@FileTemplateTypeCode ");
                    base.InsertParameter("@FileTemplateTypeCode", SqlDbType.Int, 4, value);
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
                    base.InsertParameter("@SortCode", SqlDbType.VarChar, 500, value);
                }
            }
        }
    }
}

