using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Alamut.MediatR.Caching.DependencyInjection
{
    public static class CachingBehaviorServiceCollectionExtensions
    {
        /// <summary>
        /// adds CachingBehavior to the DependencyInjection ServiceCollection
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCachingBehavior(this IServiceCollection services) =>
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));
    }
}
