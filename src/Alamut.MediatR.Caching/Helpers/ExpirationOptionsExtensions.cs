using Alamut.Abstractions.Caching;
using Microsoft.Extensions.Caching.Distributed;

namespace Alamut.MediatR.Caching.Helpers
{
    public static class ExpirationOptionsExtensions
    {
        public static DistributedCacheEntryOptions GetCacheEntryOptions(this ExpirationOptions source) =>
            new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = source.AbsoluteExpiration,
                AbsoluteExpirationRelativeToNow = source.AbsoluteExpirationRelativeToNow,
                SlidingExpiration = source.SlidingExpiration
            };
    }
}