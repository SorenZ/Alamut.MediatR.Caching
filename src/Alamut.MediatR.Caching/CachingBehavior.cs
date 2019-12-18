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
        private readonly ILoggerFactory _loggerFactory;

        public CachingBehavior(IDistributedCache cache, ILoggerFactory loggerFactory)
        {
            _cache = cache;
            _loggerFactory = loggerFactory;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, 
            RequestHandlerDelegate<TResponse> next)
        {
            if (request is ICacheable cacheable)
            {
                if (cacheable.Key == null)
                { throw new ArgumentNullException(nameof(cacheable.Key)); }

                var (exist, returnValue) = await _cache.TryGetAsync<TResponse>(cacheable.Key, cancellationToken);

                if (exist)
                {
                    return returnValue;
                }
                else
                {
                    var value = await next();

                    if(value == null)
                    { return value; }

                    await _cache.SetAsync(cacheable.Key, value, cacheable.Options.GetCacheEntryOptions(), cancellationToken);
                    
                    _loggerFactory
                        .CreateLogger("CachingBehavior")
                        .LogTrace($"insert to cache object {{{typeof(TRequest).Name}}} with key {{{cacheable.Key}}}");

                    return value;
                }

            }

            return await next();
        }
    }
}
