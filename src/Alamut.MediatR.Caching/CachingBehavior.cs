using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

using Alamut.Abstractions.Caching;
using Alamut.Extensions.Caching.Distributed;
using Alamut.MediatR.Caching.Helpers;

using MediatR;

namespace Alamut.MediatR.Caching
{
    public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
    {
        private readonly IDistributedCache _cache;
        private readonly ILogger _logger;

        public CachingBehavior(IDistributedCache cache, ILoggerFactory loggerFactory)
        {
            _cache = cache;
            _logger = loggerFactory.CreateLogger("CachingBehavior");
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, 
            RequestHandlerDelegate<TResponse> next)
        {
            if (request is ICacheable cacheable)
            {
                if (cacheable.Key == null)
                { throw new ArgumentNullException(nameof(cacheable.Key)); }

                var cacheResult = await _cache.TryGetAsync<TResponse>(cacheable.Key);

                if (cacheResult.exist)
                {
                    _logger.LogTrace("read from cache : " + typeof(TRequest).Name);

                    return cacheResult.returnValue;
                }
                else
                {
                    var value = await next();

                    await _cache.SetAsync(cacheable.Key, value, cacheable.Options.GetCacheEntryOptions(), cancellationToken);
                    
                    _logger.LogTrace("Write to cache : " + typeof(TRequest).Name);

                    return value;
                }

            }

            return await next();
        }
    }
}
