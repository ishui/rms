namespace TiannuoPM.Entities
{
    using System;

    [Serializable, CLSCompliant(true)]
    public class Contract : ContractBase
    {
        public double AHCash0
        {
            get
            {
                return 0;
            }
            set
            {
            }
        }

        public double AHCash1
        {
            get
            {
                return 0;
            }
            set
            {
            }
        }

        public string ContractTotalMoney
        {
            get
            {
                return string.Empty;
            }
            set
            {
            }
        }

        public string GetAccountStatusName
        {
            get
            {
                int valueOrDefault = this.AccountStatus.GetValueOrDefault();
                if (this.AccountStatus.HasValue)
                {
                    switch (valueOrDefault)
                    {
                        case 0:
                            return string.Empty;

                        case 1:
                            return "结算申请";

                        case 2:
                            return "已结";

                        case 3:
                            return "结算审核中";
                    }
                }
                return string.Empty;
            }
            set
            {
            }
        }

        public string GetChangeStatusName
        {
            get
            {
                int valueOrDefault = this.ChangeStatus.GetValueOrDefault();
                if (this.ChangeStatus.HasValue)
                {
                    switch (valueOrDefault)
                    {
                        case 0:
                            return string.Empty;

                        case 1:
                            return "变更申请";

                        case 2:
                            return "已变更";

                        case 3:
                            return "变更审核中";
                    }
                }
                return string.Empty;
            }
            set
            {
            }
        }

        public string GetContractStatusName
        {
            get
            {
                int valueOrDefault = this.Status.GetValueOrDefault();
                if (this.Status.HasValue)
                {
                    switch (valueOrDefault)
                    {
                        case 0:
                            return "草稿";

                        case 1:
                            return "已审批";

                        case 2:
                            return "已审批";

                        case 3:
                            return "审批中";
                    }
                }
                return string.Empty;
            }
            set
            {
            }
        }
    }
}

