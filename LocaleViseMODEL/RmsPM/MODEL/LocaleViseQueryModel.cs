namespace TiannuoPM.MODEL
{
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class LocaleViseQueryModel : QueryBaseModel
    {
        private int GetStatusDataByEnum(ViseStatusEnum Value)
        {
            switch (Value)
            {
                case ViseStatusEnum.wait:
                    return 1;

                case ViseStatusEnum.process:
                    return 2;

                case ViseStatusEnum.ispass:
                    return 3;

                case ViseStatusEnum.nopass:
                    return 4;
            }
            return 0;
        }

        public ViseBalanceStatusEnum ViseBalanceStatus
        {
            get
            {
                return ViseBalanceStatusEnum.unknown;
            }
            set
            {
                if (value != ViseBalanceStatusEnum.unknown)
                {
                    base.QueryConditionStrAdd(" ViseBalanceStatus=@ViseBalanceStatus ");
                    int num = 0;
                    switch (value)
                    {
                        case ViseBalanceStatusEnum.nobalance:
                            num = 1;
                            break;

                        case ViseBalanceStatusEnum.isbalance:
                            num = 2;
                            break;
                    }
                    base.InsertParameter("@ViseBalanceStatus", SqlDbType.Int, 4, num);
                }
            }
        }

        public List<ViseBalanceStatusEnum> ViseBalanceStatusIn
        {
            set
            {
                if ((value != null) && (value.Count > 0))
                {
                    string text = "0";
                    foreach (ViseBalanceStatusEnum enum2 in value)
                    {
                        switch (enum2)
                        {
                            case ViseBalanceStatusEnum.nobalance:
                                text = text + ",1";
                                break;

                            case ViseBalanceStatusEnum.isbalance:
                                text = text + ",2";
                                break;
                        }
                    }
                    base.QueryConditionStrAdd(" ViseBalanceStatus in (" + text + ") ");
                }
            }
        }

        public string ViseBalanceStatusInStr
        {
            set
            {
                if ((value != null) && (value.Length > 0))
                {
                    base.QueryConditionStrAdd(" ViseBalanceStatus in (" + value + ") ");
                }
            }
        }

        public int ViseCode
        {
            get
            {
                return 0;
            }
            set
            {
                if (value != 0)
                {
                    base.QueryConditionStrAdd(" ViseCode=@ViseCode ");
                    base.InsertParameter("@ViseCode", SqlDbType.Int, 4, value);
                }
            }
        }

        public string ViseCodeInStr
        {
            get
            {
                return null;
            }
            set
            {
                if ((value != null) && (value.Length > 0))
                {
                    base.QueryConditionStrAdd(" ViseCode in (" + value + ") ");
                }
            }
        }

        public string ViseContractCode
        {
            get
            {
                return null;
            }
            set
            {
                if ((value != null) && (value.Length > 0))
                {
                    base.QueryConditionStrAdd(" ViseContractCode = @ViseContractCode ");
                    base.InsertParameter("@ViseContractCode", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public DateTime ViseDateEnd
        {
            get
            {
                return DateTime.MinValue;
            }
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" ViseDate<@ViseDateEnd ");
                    base.InsertParameter("@ViseDateEnd", SqlDbType.DateTime, 4, value);
                }
            }
        }

        public DateTime ViseDateStart
        {
            get
            {
                return DateTime.MinValue;
            }
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" ViseDate>@ViseDateStart ");
                    base.InsertParameter("@ViseDateStart", SqlDbType.DateTime, 4, value);
                }
            }
        }

        public DateTime ViseEndDateEnd
        {
            get
            {
                return DateTime.MinValue;
            }
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" ViseEndDate<@ViseEndDateEnd ");
                    base.InsertParameter("@ViseEndDateEnd", SqlDbType.DateTime, 4, value);
                }
            }
        }

        public DateTime ViseEndDateStart
        {
            get
            {
                return DateTime.MinValue;
            }
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" ViseEndDate>@ViseEndDateStart ");
                    base.InsertParameter("@ViseEndDateStart", SqlDbType.DateTime, 4, value);
                }
            }
        }

        public string ViseId
        {
            get
            {
                return null;
            }
            set
            {
                if ((value != null) && (value.Length > 0))
                {
                    base.QueryConditionStrAdd("ViseId Like '%'+ @ViseId + '%' ");
                    base.InsertParameter("@ViseId", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string ViseID2
        {
            get
            {
                return null;
            }
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" ViseID2=@ViseID2 ");
                    base.InsertParameter("@ViseID2", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string ViseName
        {
            get
            {
                return null;
            }
            set
            {
                if ((value != null) && (value.Length > 0))
                {
                    base.QueryConditionStrAdd(" ViseName like @ViseName");
                    base.InsertParameter("@ViseName", SqlDbType.VarChar, 50, "%" + value + "%");
                }
            }
        }

        public string VisePerson
        {
            get
            {
                return null;
            }
            set
            {
                if ((value != null) && (value.Length > 0))
                {
                    base.QueryConditionStrAdd(" VisePerson = @VisePerson ");
                    base.InsertParameter("@VisePerson", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string ViseProject
        {
            get
            {
                return null;
            }
            set
            {
                if ((value != null) && (value.Length > 0))
                {
                    base.QueryConditionStrAdd(" ViseProject = @ViseProject ");
                    base.InsertParameter("@ViseProject", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public int ViseReferCode
        {
            get
            {
                return 0;
            }
            set
            {
                if (value != 0)
                {
                    base.QueryConditionStrAdd("ViseReferCode = @ViserReferCode");
                    base.InsertParameter("@ViserReferCode", SqlDbType.Int, 4, value);
                }
            }
        }

        public ViseStatusEnum ViseStatus
        {
            get
            {
                return ViseStatusEnum.unknown;
            }
            set
            {
                if (value != ViseStatusEnum.unknown)
                {
                    base.QueryConditionStrAdd(" ViseStatus=@ViseStatus ");
                    base.InsertParameter("@ViseStatus", SqlDbType.Int, 4, this.GetStatusDataByEnum(value));
                }
            }
        }

        public List<ViseStatusEnum> ViseStatusIn
        {
            set
            {
                if ((value != null) && (value.Count > 0))
                {
                    string text = "0";
                    foreach (ViseStatusEnum enum2 in value)
                    {
                        text = text + "," + this.GetStatusDataByEnum(enum2).ToString();
                    }
                    base.QueryConditionStrAdd(" ViseStatus in (" + text + ") ");
                }
            }
        }

        public string ViseStatusInStr
        {
            set
            {
                if ((value != null) && (value.Length > 0))
                {
                    base.QueryConditionStrAdd(" ViseStatus in (" + value + ") ");
                }
            }
        }

        public string ViseSupplier
        {
            get
            {
                return null;
            }
            set
            {
                if ((value != null) && (value.Length > 0))
                {
                    base.QueryConditionStrAdd(" ViseSupplier = @ViseSupplier ");
                    base.InsertParameter("@ViseSupplier", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string ViseType
        {
            get
            {
                return null;
            }
            set
            {
                if ((value != null) && (value.Length > 0))
                {
                    base.QueryConditionStrAdd(" ViseType = @ViseType ");
                    base.InsertParameter("@ViseType", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string ViseUnit
        {
            get
            {
                return null;
            }
            set
            {
                if ((value != null) && (value.Length > 0))
                {
                    base.QueryConditionStrAdd(" ViseUnit = @ViseUnit ");
                    base.InsertParameter("@ViseUnit", SqlDbType.VarChar, 50, value);
                }
            }
        }
    }
}

