namespace TiannuoPM.Entities
{
    using Microsoft.Practices.EnterpriseLibrary.Caching;
    using System;

    internal interface IEntityCacheItem
    {
        TimeSpan EntityCacheDuration { get; set; }

        ICacheItemExpiration EntityCacheItemExpiration { get; set; }

        CacheItemPriority EntityCacheItemPriority { get; set; }

        ICacheItemRefreshAction EntityCacheItemRefreshAction { get; set; }
    }
}

