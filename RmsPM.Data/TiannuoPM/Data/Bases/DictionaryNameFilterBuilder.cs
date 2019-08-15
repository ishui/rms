namespace TiannuoPM.Data.Bases
{
    using System;
    using TiannuoPM.Data;
    using TiannuoPM.Entities;
    [CLSCompliant(true)]
    public class DictionaryNameFilterBuilder : SqlFilterBuilder<DictionaryNameColumn>
    {
        public DictionaryNameFilterBuilder()
        {
        }

        public DictionaryNameFilterBuilder(bool ignoreCase) : base(ignoreCase)
        {
        }

        public DictionaryNameFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd)
        {
        }
    }
}

