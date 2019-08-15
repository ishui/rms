namespace TiannuoPM.Data.Bases
{
    using System;
    using TiannuoPM.Data;
    using TiannuoPM.Entities;
    [CLSCompliant(true)]
    public class DictionaryNameParameterBuilder : ParameterizedSqlFilterBuilder<DictionaryNameColumn>
    {
        public DictionaryNameParameterBuilder()
        {
        }

        public DictionaryNameParameterBuilder(bool ignoreCase) : base(ignoreCase)
        {
        }

        public DictionaryNameParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd)
        {
        }
    }
}

