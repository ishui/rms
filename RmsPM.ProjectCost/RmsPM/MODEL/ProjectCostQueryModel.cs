namespace TiannuoPM.MODEL
{
    using System;
    using System.Data;
    using RmsPM.DAL.QueryStrategy;

    public class ProjectCostQueryModel : QueryBaseModel
    {
        public string AccessRange
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    string[] textArray = value.Split(new char[] { "\n"[0] });
                    string conditionStr = AccessRanggeQuery.BuildAccessRangeString(textArray[0], textArray[1], textArray[2], SystemClassDescription.GetItemTableName("ProjectCost"), SystemClassDescription.GetItemKeyColumnName("ProjectCost"), SystemClassDescription.GetItemTypeColumnName("ProjectCost"), SystemClassDescription.GetItemCreateUserColumnName("ProjectCost"));
                    base.QueryConditionStrAdd(conditionStr);
                }
            }
        }

        public string AreaRange1
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" Area>=@AreaRange1 ");
                    base.InsertParameter("@AreaRange1", SqlDbType.Decimal, 9, value);
                }
            }
        }

        public string AreaRange2
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" Area<=@AreaRange2 ");
                    base.InsertParameter("@AreaRange2", SqlDbType.Decimal, 9, value);
                }
            }
        }

        public string GroupCodeEqual
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" GroupCode in ( select GroupCode from SystemGroup where FullID  like (select FullID from SystemGroup where GroupCode = @GroupCode) + '%'  ) ");
                    base.InsertParameter("@GroupCode", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string InputDateRange1
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" InputDate>=@InputDateRange1 ");
                    base.InsertParameter("@InputDateRange1", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public string InputDateRange2
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" InputDate<@InputDateRange2 + 1 ");
                    base.InsertParameter("@InputDateRange2", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public string InputPersonEqual
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" InputPerson=@InputPerson ");
                    base.InsertParameter("@InputPerson", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string MoneyRange1
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" Money>=@MoneyRange1 ");
                    base.InsertParameter("@MoneyRange1", SqlDbType.Decimal, 9, value);
                }
            }
        }

        public string MoneyRange2
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" Money<=@MoneyRange2 ");
                    base.InsertParameter("@MoneyRange2", SqlDbType.Decimal, 9, value);
                }
            }
        }

        public string PriceRange1
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" Price>=@PriceRange1 ");
                    base.InsertParameter("@PriceRange1", SqlDbType.Decimal, 9, value);
                }
            }
        }

        public string PriceRange2
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" Price<=@PriceRange2 ");
                    base.InsertParameter("@PriceRange2", SqlDbType.Decimal, 9, value);
                }
            }
        }

        public string ProjectCostCodeEqual
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" ProjectCostCode=@ProjectCostCode ");
                    base.InsertParameter("@ProjectCostCode", SqlDbType.Int, 4, value);
                }
            }
        }

        public string ProjectNameEqual
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" ProjectName=@ProjectName ");
                    base.InsertParameter("@ProjectName", SqlDbType.VarChar, 100, value);
                }
            }
        }

        public string QtyRange1
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" Qty>=@QtyRange1 ");
                    base.InsertParameter("@QtyRange1", SqlDbType.Decimal, 9, value);
                }
            }
        }

        public string QtyRange2
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" Qty<=@QtyRange2 ");
                    base.InsertParameter("@QtyRange2", SqlDbType.Decimal, 9, value);
                }
            }
        }

        public string RemarkEqual
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" Remark=@Remark ");
                    base.InsertParameter("@Remark", SqlDbType.VarChar, 800, value);
                }
            }
        }

        public string UnitEqual
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" Unit=@Unit ");
                    base.InsertParameter("@Unit", SqlDbType.VarChar, 50, value);
                }
            }
        }
    }
}

