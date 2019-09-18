using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;

namespace Alamut.MediatR.Caching.UnitTest.Helpers
{
    public class FakeDistributedCache : IDistributedCache
    {
        private readonly Dictionary<string, byte[]> storage = new Dictionary<string, byte[]>();

        public byte[] Get(string key)
        {
            // return storage[key];
            return storage.TryGetValue(key, out var value)
                ? value
                : null;
        }

        public async Task<byte[]> GetAsync(string key, CancellationToken token = default)
        {
            return storage.TryGetValue(key, out var value)
                ? await Task.FromResult(value)
                : await Task.FromResult((byte[])null);
        }

        public void Refresh(string key)
        {
            throw new System.NotImplementedException();
        }

        public Task RefreshAsync(string key, CancellationToken token = default)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(string key)
        {
            storage.Remove(key);
        }

        public Task RemoveAsync(string key, CancellationToken token = default)
        {
            storage.Remove(key);
            return Task.CompletedTask;
        }

        public void Set(string key, byte[] value, DistributedCacheEntryOptions options)
        {
            storage[key] = value;
        }

        public Task SetAsync(string key, byte[] value, DistributedCacheEntryOptions options, CancellationToken token = default)
        {
            storage[key] = value;
            return Task.CompletedTask;
        }
    }
}