namespace TiannuoPM.MODEL
{
    using System;
    using System.Data;
    using RmsPM.DAL.QueryStrategy;

    public class MaterialQueryModel : QueryBaseModel
    {
        public string AccessRange
        {
            set
            {
                if (value != null)
                {
                    string[] textArray = value.Split(new char[] { "\n"[0] });
                    string conditionStr = AccessRanggeQuery.BuildAccessRangeString(textArray[0], textArray[1], textArray[2], SystemClassDescription.GetItemTableName("Material"), SystemClassDescription.GetItemKeyColumnName("Material"), SystemClassDescription.GetItemTypeColumnName("Material"), SystemClassDescription.GetItemCreateUserColumnName("Material"));
                    base.QueryConditionStrAdd(conditionStr);
                }
            }
        }

        public string GroupCodeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" GroupCode in ( select GroupCode from SystemGroup where FullID  like (select FullID from SystemGroup where GroupCode = @GroupCode) + '%'  ) ");
                    base.InsertParameter("@GroupCode", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public DateTime InputDateRange1
        {
            get
            {
                return DateTime.MinValue;
            }
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" InputDate>=@InputDateRange1 ");
                    base.InsertParameter("@InputDateRange1", SqlDbType.DateTime, 4, value);
                }
            }
        }

        public DateTime InputDateRange2
        {
            get
            {
                return DateTime.MinValue;
            }
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" InputDate<@InputDateRange2+1 ");
                    base.InsertParameter("@InputDateRange2", SqlDbType.DateTime, 4, value);
                }
            }
        }

        public string InputPerson
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" InputPerson=@InputPerson ");
                    base.InsertParameter("@InputPerson", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public int MaterialCodeEqual
        {
            set
            {
                if (value != 0)
                {
                    base.QueryConditionStrAdd(" MaterialCode=@MaterialCode ");
                    base.InsertParameter("@MaterialCode", SqlDbType.Int, 4, value);
                }
            }
        }

        public string MaterialNameEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" MaterialName like '%' + @MaterialName + '%' ");
                    base.InsertParameter("@MaterialName", SqlDbType.VarChar, 100, value);
                }
            }
        }

        public string RemarkEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Remark like '%' + @Remark + '%' ");
                    base.InsertParameter("@Remark", SqlDbType.VarChar, 800, value);
                }
            }
        }

        public string SpecEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Spec like '%' + @Spec + '%' ");
                    base.InsertParameter("@Spec", SqlDbType.VarChar, 200, value);
                }
            }
        }

        public string StandardPriceRange1
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    try
                    {
                        decimal num = decimal.Parse(value);
                        base.QueryConditionStrAdd(" StandardPrice>=@StandardPriceRange1 ");
                        base.InsertParameter("@StandardPriceRange1", SqlDbType.Decimal, 9, num);
                    }
                    catch
                    {
                    }
                }
            }
        }

        public string StandardPriceRange2
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    try
                    {
                        decimal num = decimal.Parse(value);
                        base.QueryConditionStrAdd(" StandardPrice<=@StandardPriceRange2 ");
                        base.InsertParameter("@StandardPriceRange2", SqlDbType.Decimal, 9, num);
                    }
                    catch
                    {
                    }
                }
            }
        }

        public string UnitEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Unit like '%' + @Unit + '%' ");
                    base.InsertParameter("@Unit", SqlDbType.VarChar, 50, value);
                }
            }
        }
    }
}

