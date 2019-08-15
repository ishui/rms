namespace TiannuoPM.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    [CLSCompliant(true)]
    public abstract class ExpressionParserBase
    {
        private SqlComparisonType comparisonType;
        private bool ignoreCase;
        private string propertyName;

        protected ExpressionParserBase(string propertyName, SqlComparisonType comparisonType, bool ignoreCase)
        {
            this.PropertyName = propertyName;
            this.ComparisonType = comparisonType;
            this.IgnoreCase = ignoreCase;
        }

        protected abstract void AppendAnd();
        protected abstract void AppendOr();
        protected abstract void AppendSearchText(string searchText);
        protected abstract void AppendSpace();
        protected abstract void CloseGrouping();
        private bool IsKeyWord(string word)
        {
            return ((word != null) && (SqlUtil.AND.Equals(word, StringComparison.OrdinalIgnoreCase) || SqlUtil.OR.Equals(word, StringComparison.OrdinalIgnoreCase)));
        }

        protected abstract void OpenGrouping();
        protected void ParseCore(string searchText)
        {
            IList<string> quotedValues = new List<string>();
            int num = 0;
            int num2 = 0;
            int num3 = -1;
            bool flag = false;
            int num4 = 0;
            bool flag2 = false;
            StringTokenizer tokenizer = new StringTokenizer(this.ParseQuotes(searchText, quotedValues), "( ),\t\r\n", true);
            while (tokenizer.HasMoreTokens)
            {
                string word = tokenizer.NextToken.Trim();
                if (word.Equals(SqlUtil.LEFT))
                {
                    num++;
                    if (flag2)
                    {
                        this.AppendAnd();
                    }
                    this.OpenGrouping();
                    flag2 = false;
                    flag = false;
                }
                else if (word.Equals(SqlUtil.RIGHT))
                {
                    num2++;
                    this.CloseGrouping();
                    flag2 = true;
                    flag = false;
                }
                else if (word.Equals(SqlUtil.COMMA))
                {
                    this.AppendOr();
                    flag2 = false;
                    flag = false;
                }
                else if (this.IsKeyWord(word))
                {
                    num4++;
                    if (num4 == 1)
                    {
                        flag2 = true;
                        this.AppendSearchText(word);
                    }
                    else if ((num4 == 2) && (tokenizer.CountTokens <= 1))
                    {
                        this.AppendAnd();
                        this.AppendSearchText(word);
                    }
                    else if (flag)
                    {
                        flag2 = true;
                        flag = false;
                        this.AppendSearchText(word);
                    }
                    else if (tokenizer.CountTokens <= 1)
                    {
                        this.AppendAnd();
                        this.AppendSearchText(word);
                    }
                    else
                    {
                        if (SqlUtil.AND.Equals(word, StringComparison.OrdinalIgnoreCase))
                        {
                            this.AppendAnd();
                        }
                        else if (SqlUtil.OR.Equals(word, StringComparison.OrdinalIgnoreCase))
                        {
                            this.AppendOr();
                        }
                        flag2 = false;
                        flag = true;
                    }
                }
                else if (word.Equals(" "))
                {
                    this.AppendSpace();
                }
                else if (!word.Equals(""))
                {
                    if (word.Equals(SqlUtil.TOKEN))
                    {
                        num4++;
                        if (flag2)
                        {
                            this.AppendAnd();
                        }
                        flag2 = true;
                        flag = false;
                        num3++;
                        this.AppendSearchText(quotedValues[num3]);
                    }
                    else
                    {
                        num4++;
                        if (flag2)
                        {
                            this.AppendAnd();
                        }
                        flag2 = true;
                        flag = false;
                        this.AppendSearchText(word);
                    }
                }
            }
            if (num != num2)
            {
                throw new ArgumentException("Syntax Error: mismatched parenthesis.");
            }
        }

        private string ParseQuotes(string searchText, IList<string> quotedValues)
        {
            if (string.IsNullOrEmpty(searchText) || (searchText.IndexOf('"') < 0))
            {
                return searchText;
            }
            string[] textArray = searchText.Split(new char[] { '"' });
            StringBuilder builder = new StringBuilder();
            bool flag = true;
            foreach (string text in textArray)
            {
                flag = !flag;
                if (flag)
                {
                    builder.Append(SqlUtil.TOKEN);
                    quotedValues.Add(text);
                }
                else
                {
                    builder.Append(text);
                }
            }
            if (flag)
            {
                throw new ArgumentException("Syntax Error: mismatched quotes.");
            }
            return builder.ToString();
        }

        public SqlComparisonType ComparisonType
        {
            get
            {
                return this.comparisonType;
            }
            set
            {
                this.comparisonType = value;
            }
        }

        public bool IgnoreCase
        {
            get
            {
                return this.ignoreCase;
            }
            set
            {
                this.ignoreCase = value;
            }
        }

        public string PropertyName
        {
            get
            {
                return this.propertyName;
            }
            set
            {
                this.propertyName = value;
            }
        }
    }
}

