using System;
using Alamut.Abstractions.Caching;
using Alamut.MediatR.Caching.SampleApi.Model;
using MediatR;

namespace Alamut.MediatR.Caching.SampleApi.Application.Queries
{
    public class GetFooByIdQuery : IRequest<FooModel>, ICacheable
    {
        public GetFooByIdQuery(int id)
        {
            Id = id;
            Key = $"Foo_{id}";
            Options = new ExpirationOptions(TimeSpan.FromSeconds(60));
        }

        public int Id { get;  }
        public string Key { get; }
        public ExpirationOptions Options { get; }
    }
}
