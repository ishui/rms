namespace TiannuoPM.Entities.Validation
{
    using System;

    [Serializable]
    public class BrokenRule
    {
        private string _description;
        private string _property;
        private string _ruleName;

        private BrokenRule()
        {
        }

        internal BrokenRule(ValidationRuleInfo rule)
        {
            this._ruleName = rule.RuleName;
            this._description = rule.ValidationRuleArgs.Description;
            this._property = rule.ValidationRuleArgs.PropertyName;
        }

        public string Description
        {
            get
            {
                return this._description;
            }
        }

        public string Property
        {
            get
            {
                return this._property;
            }
        }

        public string RuleName
        {
            get
            {
                return this._ruleName;
            }
        }
    }
}

