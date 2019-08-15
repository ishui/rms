namespace TiannuoPM.Data.Bases
{
    using System;
    using TiannuoPM.Data;
    using TiannuoPM.Entities;
    [CLSCompliant(true)]
    public class DictionaryItemParameterBuilder : ParameterizedSqlFilterBuilder<DictionaryItemColumn>
    {
        public DictionaryItemParameterBuilder()
        {
        }

        public DictionaryItemParameterBuilder(bool ignoreCase) : base(ignoreCase)
        {
        }

        public DictionaryItemParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd)
        {
        }
    }
}

