namespace RmsDM.MODEL
{
    using System;
    using System.Data;

    public class FileTemplateTypeQueryModel : QueryBaseModel
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

        public string CodeIn
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
                        text = text + string.Format("@Code{0}", i.ToString());
                        base.InsertParameter(string.Format("@Code{0}", i.ToString()), SqlDbType.Int, 4, textArray[i]);
                    }
                    base.QueryConditionStrAdd(" Code in (" + text + ")");
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

        public string FileTemplateTypeNameEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" FileTemplateTypeName=@FileTemplateTypeName ");
                    base.InsertParameter("@FileTemplateTypeName", SqlDbType.VarChar, 50, value);
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

