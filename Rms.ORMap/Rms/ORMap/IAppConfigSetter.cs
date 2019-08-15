namespace Rms.ORMap
{
    using System;

    public interface IAppConfigSetter
    {
        string GetDBConnectionString();
        string GetDBType();
        string GetEntityDefinePath();
    }
}

