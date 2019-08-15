namespace TiannuoPM.Data.Bases
{
    using System;
    using System.Configuration.Provider;
    using System.Reflection;

    public class NetTiersProviderCollection : ProviderCollection
    {
        public void Add(NetTiersProvider provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException("provider");
            }
            if (provider == null)
            {
                throw new ArgumentException("Invalid provider type", "provider");
            }
            base.Add(provider);
        }

        public NetTiersProvider this[string name]
        {
            get
            {
                return (NetTiersProvider) base[name];
            }
        }
    }
}

