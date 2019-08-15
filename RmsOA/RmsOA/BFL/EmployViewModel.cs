namespace RmsOA.BFL
{
    using System;

    public class EmployViewModel
    {
        private int code;
        private int index;
        private int score;
        private string userCode;
        private string userName;

        public int Code
        {
            get
            {
                return this.code;
            }
            set
            {
                this.code = value;
            }
        }

        public int Index
        {
            get
            {
                return this.index;
            }
            set
            {
                this.index = value;
            }
        }

        public int Score
        {
            get
            {
                return this.score;
            }
            set
            {
                this.score = value;
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

