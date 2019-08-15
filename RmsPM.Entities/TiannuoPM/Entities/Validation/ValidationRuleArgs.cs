namespace TiannuoPM.Entities.Validation
{
    using System;

    public class ValidationRuleArgs
    {
        private string _description;
        private string _propertyName;

        public ValidationRuleArgs(string propertyName)
        {
            this._propertyName = propertyName;
        }

        public override string ToString()
        {
            return this._propertyName;
        }

        public string Description
        {
            get
            {
                return this._description;
            }
            set
            {
                this._description = value;
            }
        }

        public string PropertyName
        {
            get
            {
                return this._propertyName;
            }
        }
    }
}

