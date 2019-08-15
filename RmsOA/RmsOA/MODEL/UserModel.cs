namespace RmsOA.MODEL
{
    using System;

    public class UserModel
    {
        private string userCode;
        private string userName;

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

