using System;
using System.Threading;
using System.Threading.Tasks;
using Alamut.MediatR.Caching.SampleApi.Model;
using MediatR;

namespace Alamut.MediatR.Caching.SampleApi.Application.Queries
{
    public class GetFooByIdHandler : IRequestHandler<GetFooByIdQuery,FooModel>
    {
        public async Task<FooModel> Handle(GetFooByIdQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(new FooModel
            {
                Id = request.Id,
                CreatedTime = DateTime.Now
            });
        }
    }
}