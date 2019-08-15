namespace TiannuoPM.Entities
{
    using System;
    using System.Collections;

    public sealed class Expressions : CollectionBase
    {
        public Expressions()
        {
        }

        public Expressions(string holeFilterExpression)
        {
            this.SplitFilter(holeFilterExpression);
        }

        public int Add(Expression value)
        {
            return base.List.Add(value);
        }

        public Expression Item(int Index)
        {
            return (Expression) base.List[Index];
        }

        public void Remove(Expression value)
        {
            base.List.Remove(value);
        }

        public void SplitFilter(string HoleFilterExpression)
        {
            int startIndex = 0;
            for (int i = 5; i <= (HoleFilterExpression.Length - 5); i++)
            {
                if (HoleFilterExpression.Substring(i - 5, 5).ToUpper() == " AND ")
                {
                    this.Add(new Expression(HoleFilterExpression.Substring(startIndex, (i - startIndex) - 5)));
                    startIndex = i;
                }
            }
            for (int j = 4; j <= (HoleFilterExpression.Length - 4); j++)
            {
                if (HoleFilterExpression.Substring(j - 4, 4).ToUpper() == " OR ")
                {
                    this.Add(new Expression(HoleFilterExpression.Substring(startIndex, (j - startIndex) - 4)));
                    startIndex = j;
                }
            }
            this.Add(new Expression(HoleFilterExpression.Substring(startIndex)));
        }
    }
}

