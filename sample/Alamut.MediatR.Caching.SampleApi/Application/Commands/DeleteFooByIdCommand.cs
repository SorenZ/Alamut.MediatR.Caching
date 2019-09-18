using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Alamut.MediatR.Caching.SampleApi.Application.Commands
{
    public class DeleteFooByIdCommand : IRequest
    {
        public DeleteFooByIdCommand(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}
