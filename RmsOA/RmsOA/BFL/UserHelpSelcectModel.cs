namespace RmsOA.BFL
{
    using System;

    public class UserHelpSelcectModel
    {
        private int groupCode;
        private string groupName;
        private string ownerCode;
        private string userCode;
        private string userName;

        public int GroupCode
        {
            get
            {
                return this.groupCode;
            }
            set
            {
                this.groupCode = value;
            }
        }

        public string GroupName
        {
            get
            {
                return this.groupName;
            }
            set
            {
                this.groupName = value;
            }
        }

        public string OwnerCode
        {
            get
            {
                return this.ownerCode;
            }
            set
            {
                this.ownerCode = value;
            }
        }

        public string UserCode
        {
            get
            {
                return this.userCode;
            }
            set
            {
                this.userCode = value;
            }
        }

        public string UserName
        {
            get
            {
                return this.userName;
            }
            set
            {
                this.userName = value;
            }
        }
    }
}

