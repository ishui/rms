namespace RmsPM.DAL.CommonWorkFlowDAL
{
    using System;
    using System.Data;

    public class InquirePriceQueryModel : QueryBaseModel
    {
        public int AduitingEqual
        {
            set
            {
                if (value != 0)
                {
                    base.QueryConditionStrAdd(" Aduiting=@Aduiting ");
                    base.InsertParameter("@Aduiting", SqlDbType.Int, 4, value);
                }
            }
        }

        public string Field1Equal
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Field1=@Field1 ");
                    base.InsertParameter("@Field1", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string InquireObjectEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" InquireObject=@InquireObject ");
                    base.InsertParameter("@InquireObject", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public int InquirePriceCodeEqual
        {
            set
            {
                if (value != 0)
                {
                    base.QueryConditionStrAdd(" InquirePriceCode=@InquirePriceCode ");
                    base.InsertParameter("@InquirePriceCode", SqlDbType.Int, 4, value);
                }
            }
        }

        public string ProjectNameEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" ProjectName=@ProjectName ");
                    base.InsertParameter("@ProjectName", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string RequirementEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Requirement=@Requirement ");
                    base.InsertParameter("@Requirement", SqlDbType.VarChar, 0x10, value);
                }
            }
        }
    }
}

