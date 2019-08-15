namespace TiannuoPM.Entities.Validation
{
    using System;

    internal class ValidationRuleInfo
    {
        private TiannuoPM.Entities.Validation.ValidationRuleArgs _args;
        private ValidationRuleHandler _handler;
        private string _ruleName;
        private object _target;

        public ValidationRuleInfo(object target, ValidationRuleHandler handler, string propertyName) : this(target, handler, new TiannuoPM.Entities.Validation.ValidationRuleArgs(propertyName))
        {
        }

        public ValidationRuleInfo(object target, ValidationRuleHandler handler, TiannuoPM.Entities.Validation.ValidationRuleArgs args)
        {
            this._ruleName = string.Empty;
            this._target = target;
            this._handler = handler;
            this._args = args;
            this._ruleName = this._handler.Method.Name + "!" + this._args.ToString();
        }

        public bool Invoke()
        {
            return this._handler(this._target, this._args);
        }

        public override string ToString()
        {
            return this._ruleName;
        }

        public string RuleName
        {
            get
            {
                return this._ruleName;
            }
        }

        public TiannuoPM.Entities.Validation.ValidationRuleArgs ValidationRuleArgs
        {
            get
            {
                return this._args;
            }
        }
    }
}

