namespace RmsOA.BFL
{
    using System;

    public class WrongModel
    {
        private int index;
        private string message;
        private int noIndex;

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

        public string Message
        {
            get
            {
                return this.message;
            }
            set
            {
                this.message = value;
            }
        }

        public int NoIndex
        {
            get
            {
                return this.noIndex;
            }
            set
            {
                this.noIndex = value;
            }
        }
    }
}

