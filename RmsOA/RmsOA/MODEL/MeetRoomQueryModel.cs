namespace RmsOA.MODEL
{
    using System;
    using System.Data;

    public class MeetRoomQueryModel : QueryBaseModel
    {
        public string CapacityEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Capacity=@Capacity ");
                    base.InsertParameter("@Capacity", SqlDbType.VarChar, 50, value);
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

        public string HardConditionEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" HardCondition=@HardCondition ");
                    base.InsertParameter("@HardCondition", SqlDbType.VarChar, 500, value);
                }
            }
        }

        public string PlaceEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Place=@Place ");
                    base.InsertParameter("@Place", SqlDbType.VarChar, 500, value);
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
                    base.InsertParameter("@Remark", SqlDbType.VarChar, 0x7d0, value);
                }
            }
        }

        public string RoomNameEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" RoomName=@RoomName ");
                    base.InsertParameter("@RoomName", SqlDbType.VarChar, 200, value);
                }
            }
        }
    }
}

