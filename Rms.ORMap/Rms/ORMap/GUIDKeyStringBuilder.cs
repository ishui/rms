namespace Rms.ORMap
{
    using System;

    public class GUIDKeyStringBuilder : IKeyStringBuilder
    {
        public string GetKeyString()
        {
            return GUIDGenerator.GetNewGUID();
        }
    }
}

