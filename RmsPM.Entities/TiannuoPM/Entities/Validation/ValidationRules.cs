namespace TiannuoPM.Entities.Validation
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class ValidationRules
    {
        private TiannuoPM.Entities.Validation.BrokenRulesList _brokenRules;
        [NonSerialized]
        private Dictionary<string, List<ValidationRuleInfo>> _rulesList;
        [NonSerialized]
        private object _target;

        internal ValidationRules(object businessEntity)
        {
            this.Target = businessEntity;
        }

        public void AddRule(ValidationRuleHandler handler, string propertyName)
        {
            this.AddRule(handler, new ValidationRuleArgs(propertyName));
        }

        public void AddRule(ValidationRuleHandler handler, ValidationRuleArgs args)
        {
            this.GetPropertyRules(args.PropertyName).Add(new ValidationRuleInfo(this._target, handler, args));
        }

        public void Clear()
        {
            this._rulesList.Clear();
        }

        public TiannuoPM.Entities.Validation.BrokenRulesList GetBrokenRules()
        {
            return this.BrokenRulesList;
        }

        private List<ValidationRuleInfo> GetPropertyRules(string propertyName)
        {
            List<ValidationRuleInfo> list = null;
            if (this.RulesList.ContainsKey(propertyName))
            {
                list = this.RulesList[propertyName];
            }
            if (list == null)
            {
                list = new List<ValidationRuleInfo>();
                this.RulesList.Add(propertyName, list);
            }
            return list;
        }

        private void ValidateRuleList(List<ValidationRuleInfo> ruleList)
        {
            foreach (ValidationRuleInfo info in ruleList)
            {
                if (info.Invoke())
                {
                    this.BrokenRulesList.Remove(info);
                }
                else
                {
                    this.BrokenRulesList.Add(info);
                }
            }
        }

        public void ValidateRules()
        {
            foreach (KeyValuePair<string, List<ValidationRuleInfo>> pair in this.RulesList)
            {
                this.ValidateRuleList(pair.Value);
            }
        }

        public void ValidateRules(string propertyName)
        {
            if (this.RulesList.ContainsKey(propertyName))
            {
                List<ValidationRuleInfo> ruleList = this.RulesList[propertyName];
                if (ruleList != null)
                {
                    this.ValidateRuleList(ruleList);
                }
            }
        }

        private TiannuoPM.Entities.Validation.BrokenRulesList BrokenRulesList
        {
            get
            {
                if (this._brokenRules == null)
                {
                    this._brokenRules = new TiannuoPM.Entities.Validation.BrokenRulesList();
                }
                return this._brokenRules;
            }
        }

        internal bool IsValid
        {
            get
            {
                return (this.BrokenRulesList.Count == 0);
            }
        }

        private Dictionary<string, List<ValidationRuleInfo>> RulesList
        {
            get
            {
                if (this._rulesList == null)
                {
                    this._rulesList = new Dictionary<string, List<ValidationRuleInfo>>();
                }
                return this._rulesList;
            }
        }

        internal object Target
        {
            get
            {
                return this._target;
            }
            set
            {
                this._target = value;
            }
        }
    }
}

