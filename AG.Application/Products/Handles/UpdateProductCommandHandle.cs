using AG.Application.Products.Requests;
using AG.Domain.Products.Repositories;
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
    public class UpdateProductCommandHandle : IRequestHandler<UpdateProductCommandRequest, ErrorOr<bool>>
    {
        private readonly IProductRepository _repository;

        public UpdateProductCommandHandle(IProductRepository repository)
        {
            _repository = repository;
        }
        public async Task<ErrorOr<bool>> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            return await _repository.UpdateProductAsync(request.Product);
        }
    }
}
