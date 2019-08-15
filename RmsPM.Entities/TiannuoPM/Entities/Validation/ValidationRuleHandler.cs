namespace TiannuoPM.Entities.Validation
{
    using System;
    using System.Runtime.CompilerServices;

    public delegate bool ValidationRuleHandler(object target, ValidationRuleArgs e);
}

