using AG.Application.Products.Models;
using AG.Application.Products.Requests;
using AG.Domain.Products.Repositories;
using AutoMapper;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AG.Application.Products.Handles
{
    public class InactivateProductCommandHandle : IRequestHandler<InactivateProductCommandRequest, ErrorOr<bool>>
    {

        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public InactivateProductCommandHandle(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ErrorOr<bool>> Handle(InactivateProductCommandRequest request, CancellationToken cancellationToken)
        {
            return await _repository.InactivateProductAsync(request.Id);
        }
    }
}
