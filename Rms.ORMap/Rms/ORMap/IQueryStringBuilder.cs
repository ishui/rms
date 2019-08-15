namespace Rms.ORMap
{
    using System;

    public interface IQueryStringBuilder
    {
        void AddOrder(string name, bool IsASC);
        void AddStrategy(Strategy strategy);
        string BuildStrategysString();
    }
}

