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
    public class AddProductsCommandHandle : IRequestHandler<AddProductsCommandRequest, ErrorOr<bool>>
    {
        private readonly IProductRepository _repository;

        public AddProductsCommandHandle(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<bool>> Handle(AddProductsCommandRequest request, CancellationToken cancellationToken)
        {
            return await _repository.AddProductAsync(request.Product);

        }
    }
}
