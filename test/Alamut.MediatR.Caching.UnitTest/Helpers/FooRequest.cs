using System;
using Alamut.Abstractions.Caching;
using MediatR;

namespace Alamut.MediatR.Caching.UnitTest.Helpers
{
    public class FooRequest : IRequest
    {
        public int FooId { get; set; }
    }

    public class FooRequestCacheable : IRequest, ICacheable
    {
        public FooRequestCacheable(TimeSpan expirationTimeSpan)
        {
            Key = "FooRequestCacheable";
            Options = new ExpirationOptions(expirationTimeSpan);
        }

        public int FooId { get; set; }
        public string Key { get; }
        public ExpirationOptions Options { get; }
    }
}