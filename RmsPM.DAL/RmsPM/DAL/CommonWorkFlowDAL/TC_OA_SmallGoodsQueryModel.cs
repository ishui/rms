namespace RmsPM.DAL.CommonWorkFlowDAL
{
    using System;
    using System.Data;

    public class TC_OA_SmallGoodsQueryModel : QueryBaseModel
    {
        public DateTime ApplayDateEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" ApplayDate=@ApplayDate ");
                    base.InsertParameter("@ApplayDate", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public int AuditingEqual
        {
            set
            {
                if (value != 0)
                {
                    base.QueryConditionStrAdd(" Auditing=@Auditing ");
                    base.InsertParameter("@Auditing", SqlDbType.Int, 4, value);
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

        public string GoodsCodeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" GoodsCode=@GoodsCode ");
                    base.InsertParameter("@GoodsCode", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string NameEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Name=@Name ");
                    base.InsertParameter("@Name", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string TypeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Type=@Type ");
                    base.InsertParameter("@Type", SqlDbType.VarChar, 50, value);
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

        public string UserCodeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" UserCode=@UserCode ");
                    base.InsertParameter("@UserCode", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string UseWayEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" UseWay=@UseWay ");
                    base.InsertParameter("@UseWay", SqlDbType.VarChar, 50, value);
                }
            }
        }
    }
}

