namespace TiannuoPM.Entities
{
    using System;

    public sealed class Expression
    {
        private string TmpOperator;
        private string TmpPropertyValue;
        private string TmpUserValue;

        public Expression()
        {
        }

        public Expression(string wholeExpression)
        {
            string[] textArray = new string[2];
            textArray = wholeExpression.Split(new char[] { ' ' }, 3);
            this.PropertyName = textArray[0];
            this.Operator = textArray[1].Trim();
            this.UserValue = textArray[2].Trim();
        }

        public Expression(string PropValue, string Opr, string Usrvalue)
        {
            this.PropertyName = PropValue;
            this.Operator = Opr;
            this.UserValue = this.UserValue = Usrvalue;
        }

        public string Operator
        {
            get
            {
                return this.TmpOperator;
            }
            set
            {
                this.TmpOperator = value;
            }
        }

        public string PropertyName
        {
            get
            {
                return this.TmpPropertyValue;
            }
            set
            {
                this.TmpPropertyValue = value;
            }
        }

        public string UserValue
        {
            get
            {
                return this.TmpUserValue;
            }
            set
            {
                this.TmpUserValue = value;
            }
        }
    }
}

