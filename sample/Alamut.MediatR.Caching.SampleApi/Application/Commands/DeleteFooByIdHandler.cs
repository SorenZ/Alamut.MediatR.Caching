using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace Alamut.MediatR.Caching.SampleApi.Application.Commands
{
    public class DeleteFooByIdHandler : IRequestHandler<DeleteFooByIdCommand>
    {
        private readonly IDistributedCache _cache;

        public DeleteFooByIdHandler(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<Unit> Handle(DeleteFooByIdCommand request, CancellationToken cancellationToken)
        {
            await _cache.RemoveAsync($"Foo_{request.Id}", cancellationToken);
            
            return Unit.Value;
        }
    }
}