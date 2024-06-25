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
    public class GetProductsQueryHandle : IRequestHandler<GetProductsQueryRequest, ErrorOr<List<ProductReponseModel>>>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public GetProductsQueryHandle(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ErrorOr<List<ProductReponseModel>>> Handle(GetProductsQueryRequest request, CancellationToken cancellationToken)
        {
            var products = await _repository.GetProductsAsync(request.Page, request.PageQuantity);

            if (products == null)
                return Error.NotFound();


            return _mapper.Map<List<ProductReponseModel>>(products);
        }
    }
}
