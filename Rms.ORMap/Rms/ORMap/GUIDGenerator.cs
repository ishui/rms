namespace Rms.ORMap
{
    using System;

    public sealed class GUIDGenerator
    {
        private GUIDGenerator()
        {
        }

        public static string GetNewGUID()
        {
            return Guid.NewGuid().ToString();
        }
    }
}

