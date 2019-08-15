namespace Rms.ORMap
{
    using System;
    using System.Collections;
    using System.Text;

    public abstract class AbstractQueryStringBuilder : IQueryStringBuilder
    {
        public bool IsNeedWhere = true;
        private ArrayList orders = new ArrayList();
        public string QueryKeyString;
        public string QueryMainString;
        public string QueryOtherString;
        private ArrayList strategys = new ArrayList();

        public virtual void AddOrder(string Name, bool IsASC)
        {
            this.RemoveOrder(Name);
            this.orders.Add(new QueryOrder(Name, IsASC));
        }

        public virtual void AddStrategy(Strategy strategy)
        {
            int parameterCount = strategy.GetParameterCount();
            for (int i = 0; i < parameterCount; i++)
            {
                string param = strategy.GetParameter(i);
                param.Replace("'", "\"");
                strategy.SetParameter(i, param);
            }
            this.strategys.Add(strategy);
        }

        public virtual void AddStrategy(Strategy strategy, bool isUnique)
        {
            if (isUnique)
            {
                this.ReomveStrategy(strategy.Name);
            }
            this.AddStrategy(strategy);
        }

        public virtual string BuildKeyQueryString()
        {
            return (this.QueryKeyString + this.BuildStrategysString() + this.BuildOrderString());
        }

        /// <summary>
        /// 建立SQL语句：参数、排序
        /// </summary>
        /// <returns></returns>
        public virtual string BuildMainQueryString()
        {
            return (this.QueryMainString + this.BuildStrategysString() + this.BuildOrderString());
        }

        public virtual string BuildOrderString()
        {
            if (this.orders.Count == 0)
            {
                return "";
            }
            StringBuilder builder = new StringBuilder();
            int count = this.orders.Count;
            for (int i = 0; i < count; i++)
            {
                if (i != 0)
                {
                    builder.Append(" , ");
                }
                else
                {
                    builder.Append(" Order By ");
                }
                QueryOrder order = (QueryOrder) this.orders[i];
                string name = order.Name;
                bool sort = order.Sort;
                builder.Append(name);
                if (sort)
                {
                    builder.Append(" ASC ");
                }
                else
                {
                    builder.Append(" DESC ");
                }
            }
            return builder.ToString();
        }

        public virtual string BuildOtherQueryString()
        {
            return (this.QueryOtherString + this.BuildStrategysString() + this.BuildOrderString());
        }

        public virtual string BuildSingleStrategyString(Strategy strategy)
        {
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }

        /// <summary>
        /// SQL判断参数
        /// </summary>
        /// <returns></returns>
        public virtual string BuildStrategysString()
        {
            if (this.strategys.Count == 0)
            {
                return "";
            }
            int num = 0;
            StringBuilder builder = new StringBuilder();
            if (this.IsNeedWhere)
            {
                builder.Append(" where ");
            }
            else
            {
                builder.Append(" and ");
            }
            IEnumerator enumerator = this.strategys.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (num != 0)
                {
                    builder.Append(" and ");
                }
                Strategy current = (Strategy) enumerator.Current;
                builder.Append(this.BuildSingleStrategyString(current));
                num++;
            }
            return builder.ToString();
        }

        public virtual void ClearStrategy()
        {
            this.strategys.Clear();
        }

        public virtual ArrayList GetStategys()
        {
            return this.strategys;
        }

        public virtual void RemoveOrder(string name)
        {
            int count = this.orders.Count;
            if (count != 0)
            {
                for (int i = count - 1; i >= 0; i--)
                {
                    QueryOrder order = (QueryOrder) this.orders[i];
                    if (order.Name == name)
                    {
                        this.orders.Remove(order);
                    }
                }
            }
        }

        public virtual void ReomveStrategy(object strategyName)
        {
            int count = this.strategys.Count;
            if (count != 0)
            {
                for (int i = count - 1; i >= 0; i--)
                {
                    Strategy strategy = (Strategy) this.strategys[i];
                    if (strategyName == strategy.Name)
                    {
                        this.strategys.Remove(strategy);
                    }
                }
            }
        }
    }
}

