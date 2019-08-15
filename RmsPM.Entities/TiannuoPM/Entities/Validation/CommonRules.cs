namespace TiannuoPM.Entities.Validation
{
    using System;
    using System.Collections;
    using System.Reflection;
    using System.Text.RegularExpressions;

    public static class CommonRules
    {
        private static bool CompareValues<T>(object target, CompareValueRuleArgs<T> e, CompareType compareType)
        {
            bool flag = true;
            if (e != null)
            {
                T a;
                T b = e.CompareValue;
                PropertyInfo property = target.GetType().GetProperty(e.PropertyName);
                try
                {
                    a = (T) property.GetValue(target, null);
                }
                catch (Exception)
                {
                    return true;
                }
                if (property.PropertyType.Name.Equals(typeof(Nullable<>).Name) && (a == null))
                {
                    return true;
                }
                int num = Comparer.DefaultInvariant.Compare(a, b);
                switch (compareType)
                {
                    case CompareType.LessThanOrEqualTo:
                        flag = num <= 0;
                        if (!flag && string.IsNullOrEmpty(e.Description))
                        {
                            e.Description = string.Format("{0} can not exceed {1}", e.PropertyName, b.ToString());
                        }
                        break;

                    case CompareType.LessThan:
                        flag = num < 0;
                        if (!flag && string.IsNullOrEmpty(e.Description))
                        {
                            e.Description = string.Format("{0} must be less than {1}", e.PropertyName, b.ToString());
                        }
                        break;

                    case CompareType.EqualTo:
                        flag = num == 0;
                        if (!flag && string.IsNullOrEmpty(e.Description))
                        {
                            e.Description = string.Format("{0} must equal {1}", e.PropertyName, b.ToString());
                        }
                        break;

                    case CompareType.GreaterThan:
                        flag = num > 0;
                        if (!flag && string.IsNullOrEmpty(e.Description))
                        {
                            e.Description = string.Format("{0} must exceed {1}", e.PropertyName, b.ToString());
                        }
                        break;

                    case CompareType.GreaterThanOrEqualTo:
                        flag = num >= 0;
                        if (!flag && string.IsNullOrEmpty(e.Description))
                        {
                            e.Description = string.Format("{0} must be greater than or equal to {1}", e.PropertyName, b.ToString());
                        }
                        break;
                }
                if (!flag)
                {
                }
            }
            return flag;
        }

        public static bool EqualsValue<T>(object target, ValidationRuleArgs e)
        {
            return CompareValues<T>(target, e as CompareValueRuleArgs<T>, CompareType.EqualTo);
        }

        public static bool GreaterThanOrEqualToValue<T>(object target, ValidationRuleArgs e)
        {
            return CompareValues<T>(target, e as CompareValueRuleArgs<T>, CompareType.GreaterThanOrEqualTo);
        }

        public static bool GreaterThanValue<T>(object target, ValidationRuleArgs e)
        {
            return CompareValues<T>(target, e as CompareValueRuleArgs<T>, CompareType.GreaterThan);
        }

        public static bool InRange<T>(object target, ValidationRuleArgs e)
        {
            bool flag = true;
            RangeRuleArgs<T> args = e as RangeRuleArgs<T>;
            if (args == null)
            {
                throw new ArgumentException("Must be of type RangeRuleArgs.", "e");
            }
            T local = (T) target.GetType().GetProperty(e.PropertyName).GetValue(target, null);
            flag = args.Range.Contains(local);
            if (!flag && string.IsNullOrEmpty(e.Description))
            {
                e.Description = string.Format("{0} must be between {1} and {2}.", args.PropertyName, args.Range.MinValue, args.Range.MaxValue);
            }
            return flag;
        }

        public static bool LessThanOrEqualToValue<T>(object target, ValidationRuleArgs e)
        {
            return CompareValues<T>(target, e as CompareValueRuleArgs<T>, CompareType.LessThanOrEqualTo);
        }

        public static bool LessThanValue<T>(object target, ValidationRuleArgs e)
        {
            return CompareValues<T>(target, e as CompareValueRuleArgs<T>, CompareType.LessThan);
        }

        public static bool MaxWords(object target, ValidationRuleArgs e)
        {
            MaxWordsRuleArgs args = e as MaxWordsRuleArgs;
            if (args == null)
            {
                throw new ArgumentException("Invalid ValidationRuleArgs. e must be of type MaxWordsRuleArgs.");
            }
            string pattern = @"\b\w+\b";
            PropertyInfo property = target.GetType().GetProperty(e.PropertyName);
            if (property == null)
            {
                throw new ArgumentException(string.Format("Property \"{0}\" not found on object \"{1}\"", e.PropertyName, target.GetType().ToString()));
            }
            if (property.PropertyType != typeof(string))
            {
                throw new ArgumentException(string.Format("Property \"{0}\" is not of type String.", e.PropertyName));
            }
            string input = (string) property.GetValue(target, null);
            if (Regex.Matches(input, pattern).Count > args.MaxLength)
            {
                if (e.Description == string.Empty)
                {
                    e.Description = string.Format("{0} exceed the maximum number of words", e.PropertyName, pattern);
                }
                return false;
            }
            return true;
        }

        public static bool NotNull(object target, ValidationRuleArgs e)
        {
            PropertyInfo property = target.GetType().GetProperty(e.PropertyName);
            if (property == null)
            {
                throw new ArgumentException(string.Format("Property \"{0}\" not found on object \"{1}\"", e.PropertyName, target.GetType().ToString()));
            }
            if (property.GetValue(target, null) == null)
            {
                if (string.IsNullOrEmpty(e.Description))
                {
                    e.Description = string.Format("{0} can not be null.", e.PropertyName);
                }
                return false;
            }
            return true;
        }

        public static bool RegexIsMatch(object target, ValidationRuleArgs e)
        {
            RegexRuleArgs args = e as RegexRuleArgs;
            if (args == null)
            {
                throw new ArgumentException("Invalid ValidationRuleArgs.  e must be of type RegexRuleArgs.");
            }
            string pattern = args.Expression;
            PropertyInfo property = target.GetType().GetProperty(e.PropertyName);
            if (property == null)
            {
                throw new ArgumentException(string.Format("Property \"{0}\" not found on object \"{1}\"", e.PropertyName, target.GetType().ToString()));
            }
            if (property.PropertyType != typeof(string))
            {
                throw new ArgumentException(string.Format("Property \"{0}\" is not of type String.", e.PropertyName));
            }
            string input = (string) property.GetValue(target, null);
            if ((input == null) || !Regex.IsMatch(input, pattern))
            {
                if (string.IsNullOrEmpty(e.Description))
                {
                    e.Description = string.Format("{0} do not match the regular expression {1}", e.PropertyName, pattern);
                }
                return false;
            }
            return true;
        }

        public static bool StringMaxLength(object target, ValidationRuleArgs e)
        {
            MaxLengthRuleArgs args = e as MaxLengthRuleArgs;
            if (args == null)
            {
                throw new ArgumentException("Invalid ValidationRuleArgs.  e must be of type MaxLengthRuleArgs.");
            }
            int maxLength = args.MaxLength;
            PropertyInfo property = target.GetType().GetProperty(e.PropertyName);
            if (property == null)
            {
                throw new ArgumentException(string.Format("Property \"{0}\" not found on object \"{1}\"", e.PropertyName, target.GetType().ToString()));
            }
            if (property.PropertyType != typeof(string))
            {
                throw new ArgumentException(string.Format("Property \"{0}\" is not of type String.", e.PropertyName));
            }
            string text = (string) property.GetValue(target, null);
            if (!string.IsNullOrEmpty(text) && (text.Length > maxLength))
            {
                if (string.IsNullOrEmpty(e.Description))
                {
                    e.Description = string.Format("{0} can not exceed {1} characters", e.PropertyName, maxLength.ToString());
                }
                return false;
            }
            return true;
        }

        public static bool StringRequired(object target, ValidationRuleArgs e)
        {
            PropertyInfo property = target.GetType().GetProperty(e.PropertyName);
            if (property == null)
            {
                throw new ArgumentException(string.Format("Property \"{0}\" not found on object \"{1}\"", e.PropertyName, target.GetType().ToString()));
            }
            string text = (string) property.GetValue(target, null);
            if (string.IsNullOrEmpty(text))
            {
                if (string.IsNullOrEmpty(e.Description))
                {
                    e.Description = e.PropertyName + " required";
                }
                return false;
            }
            return true;
        }

        private enum CompareType
        {
            LessThanOrEqualTo,
            LessThan,
            EqualTo,
            GreaterThan,
            GreaterThanOrEqualTo
        }

        public class CompareValueRuleArgs<T> : ValidationRuleArgs
        {
            private T _compareValue;

            public CompareValueRuleArgs(string propertyName, T compareValue) : base(propertyName)
            {
                this._compareValue = compareValue;
            }

            public override string ToString()
            {
                return (base.ToString() + "!" + this._compareValue.ToString());
            }

            public T CompareValue
            {
                get
                {
                    return this._compareValue;
                }
            }
        }

        public class MaxLengthRuleArgs : ValidationRuleArgs
        {
            private int _maxLength;

            public MaxLengthRuleArgs(string propertyName, int maxLength) : base(propertyName)
            {
                this._maxLength = maxLength;
            }

            public override string ToString()
            {
                return (base.ToString() + "!" + this._maxLength.ToString());
            }

            public int MaxLength
            {
                get
                {
                    return this._maxLength;
                }
            }
        }

        public class MaxWordsRuleArgs : ValidationRuleArgs
        {
            private int _maxLength;

            public MaxWordsRuleArgs(string propertyName, int maxLength) : base(propertyName)
            {
                this._maxLength = maxLength;
            }

            public override string ToString()
            {
                return (base.ToString() + "!" + this._maxLength.ToString());
            }

            public int MaxLength
            {
                get
                {
                    return this._maxLength;
                }
            }
        }

        public class Range<T>
        {
            private readonly T maxValue;
            private readonly T minValue;

            public Range(T minValue, T maxValue)
            {
                if (Comparer.DefaultInvariant.Compare(minValue, maxValue) <= 0)
                {
                    this.minValue = minValue;
                    this.maxValue = maxValue;
                }
                else
                {
                    this.minValue = maxValue;
                    this.maxValue = minValue;
                }
            }

            public bool Contains(T value)
            {
                return ((Comparer.DefaultInvariant.Compare(value, this.MinValue) >= 0) && (Comparer.DefaultInvariant.Compare(value, this.MaxValue) <= 0));
            }

            public override string ToString()
            {
                return (base.ToString() + "!" + this.minValue.ToString() + "-" + this.maxValue.ToString());
            }

            public T MaxValue
            {
                get
                {
                    return this.maxValue;
                }
            }

            public T MinValue
            {
                get
                {
                    return this.minValue;
                }
            }
        }

        public class RangeRuleArgs<T> : ValidationRuleArgs
        {
            private CommonRules.Range<T> range;

            public RangeRuleArgs(string propertyName, CommonRules.Range<T> range) : base(propertyName)
            {
                this.range = range;
            }

            public RangeRuleArgs(string propertyName, T minValue, T maxValue) : base(propertyName)
            {
                this.range = new CommonRules.Range<T>(minValue, maxValue);
            }

            public override string ToString()
            {
                return (base.ToString() + "!" + this.range.ToString());
            }

            public CommonRules.Range<T> Range
            {
                get
                {
                    return this.range;
                }
            }
        }

        public class RegexRuleArgs : ValidationRuleArgs
        {
            private string _expression;

            public RegexRuleArgs(string propertyName, string expression) : base(propertyName)
            {
                this._expression = expression;
            }

            public override string ToString()
            {
                return (base.ToString() + "!" + this._expression);
            }

            public string Expression
            {
                get
                {
                    return this._expression;
                }
            }
        }
    }
}

