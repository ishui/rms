namespace TiannuoPM.MODEL
{
    using System;
    using System.Data;

    public class LocaleViseCostQueryModel : QueryBaseModel
    {
        public int ViseCode
        {
            set
            {
                if (base.QueryConditionStr.Length == 0)
                {
                    base.QueryConditionStr = base.QueryConditionStr + " where ViseCode=@ViseCode ";
                }
                else
                {
                    base.QueryConditionStr = base.QueryConditionStr + " and ViseCode=@ViseCode ";
                }
                base.InsertParameter("@ViseCode", SqlDbType.Int, 4, value);
            }
        }

        public int ViseCostCode
        {
            set
            {
                if (base.QueryConditionStr.Length == 0)
                {
                    base.QueryConditionStr = base.QueryConditionStr + " where ViseCostCode=@ViseCostCode ";
                }
                else
                {
                    base.QueryConditionStr = base.QueryConditionStr + " and ViseCostCode=@ViseCostCode ";
                }
                base.InsertParameter("@ViseCostCode", SqlDbType.Int, 4, value);
            }
        }
    }
}

