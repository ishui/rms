namespace TiannuoPM.Data.Bases
{
    using System;
    using TiannuoPM.Data;
    using TiannuoPM.Entities;
    [CLSCompliant(true)]
    public class DictionaryItemFilterBuilder : SqlFilterBuilder<DictionaryItemColumn>
    {
        public DictionaryItemFilterBuilder()
        {
        }

        public DictionaryItemFilterBuilder(bool ignoreCase) : base(ignoreCase)
        {
        }

        public DictionaryItemFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd)
        {
        }
    }
}

