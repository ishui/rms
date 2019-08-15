namespace TiannuoPM.Entities.Validation
{
    using System;
    using System.ComponentModel;
    using System.Text;

    [Serializable]
    public class BrokenRulesList : BindingList<BrokenRule>
    {
        
        internal BrokenRulesList()
        {
        }

        internal void Add(ValidationRuleInfo rule)
        {
            this.Remove(rule);
            base.Add(new BrokenRule(rule));
        }

        public BrokenRule GetFirstBrokenRule(string property)
        {
            foreach (BrokenRule rule in Items)
            {
                if (rule.Property == property)
                {
                    return rule;
                }
            }
            return null;
        }

        public string GetPropertyErrorDescriptions(string propertyName)
        {
            StringBuilder builder = new StringBuilder();
            bool flag = true;
            foreach (BrokenRule rule in Items)
            {
                if (string.IsNullOrEmpty(propertyName) || rule.Property.Equals(propertyName))
                {
                    if (flag)
                    {
                        flag = false;
                    }
                    else
                    {
                        builder.Append(Environment.NewLine);
                    }
                    builder.Append(rule.Description);
                }
            }
            return builder.ToString();
        }

        internal void Remove(ValidationRuleInfo rule)
        {
            for (int i = base.Count - 1; i >= 0; i--)
            {
                if (base[i].RuleName == rule.RuleName)
                {
                    base.RemoveAt(i);
                    break;
                }
            }
        }

        public override string ToString()
        {
            return this.GetPropertyErrorDescriptions(null);
        }
    }
}

