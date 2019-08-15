namespace TiannuoPM.Data.Bases
{
    using System;
    using System.Configuration;

    public class NetTiersServiceSection : ConfigurationSection
    {
        [StringValidator(MinLength=1), ConfigurationProperty("defaultProvider", DefaultValue="SqlNetTiersProvider")]
        public string DefaultProvider
        {
            get
            {
                return (string) base["defaultProvider"];
            }
            set
            {
                base["defaultProvider"] = value;
            }
        }

        [ConfigurationProperty("providers")]
        public ProviderSettingsCollection Providers
        {
            get
            {
                return (ProviderSettingsCollection) base["providers"];
            }
        }
    }
}

