using System;
using Xunit;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using Alamut.MediatR.Caching.UnitTest.Helpers;

namespace Alamut.MediatR.Caching.UnitTest
{
    public class CachingBehaviorTest
    {
        private readonly IDistributedCache _cache;
        private readonly ILoggerFactory _loggerFactory;
        

        public CachingBehaviorTest()
        {
            _cache = new FakeDistributedCache();
            _loggerFactory = new NullLoggerFactory();
            
        }

        [Fact]
        public async void CachingBehavior_CallHandlerWithNormalRequest_ReturnExpectedObject()
        {
            //Given
            var request = new FooRequest();
            var actual = new RefTypeObject
            {
                Foo = 1,
                Bar = "bar",
                Created = DateTime.UtcNow
            };
            var sut = new CachingBehavior<FooRequest, RefTypeObject>(_cache, _loggerFactory);
            

            //When
            var expected = await sut.Handle(request, CancellationToken.None, 
                async () => await Task.FromResult(actual));

            //Then
            Assert.Equal(expected, actual);
        }

        [Fact]
        // ReSharper disable once TooManyDeclarations
        public async void CachingBehavior_CallHandlerWithCacheableRequest_ReturnCachedObject()
        {
            // Arrange
            var cacheableRequest = new FooRequestCacheable(TimeSpan.FromMilliseconds(10));
            var cachedObjectTime = DateTime.UtcNow;
            var actual = new RefTypeObject
            {
                Foo = 1,
                Bar = "bar",
                Created = cachedObjectTime
            };
            var sut = new CachingBehavior<FooRequestCacheable, RefTypeObject>(_cache, _loggerFactory);
                
            // Act
            
            // call delegate, cache and return result
            await sut.Handle(cacheableRequest, CancellationToken.None, 
                async () => await Task.FromResult(actual));

            // waiting to spend some time
            await Task.Delay(5);

            // return cached result, even-though wrong result expected from delegate
            var expected = await sut.Handle(cacheableRequest, CancellationToken.None,
                async () => await Task.FromResult(new RefTypeObject()));

            // Assert
            Assert.Equal(expected.Created, cachedObjectTime);

        }

    }
}
