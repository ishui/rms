namespace Rms.ORMap
{
    using System;

    public class SingleEntityDAO : AbstractEntityDAO
    {
        public SingleEntityDAO(string entityName) : base(entityName)
        {
        }

        public SingleEntityDAO(string entityName, IDBCommon cdb) : base(entityName, cdb)
        {
        }
    }
}

