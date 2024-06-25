using AG.Application.Products.Models;
using AG.Application.Products.Requests;
using AG.Domain.Products.Entities;
using AG.Domain.Products.Repositories;
using AutoMapper;
using ErrorOr;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AG.Application.Products.Handles
{
    public class GetProductByIdQueryHandle : IRequestHandler<GetProductByIdQueryRequest, ErrorOr<ProductReponseModel>>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public GetProductByIdQueryHandle(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ErrorOr<ProductReponseModel>> Handle(GetProductByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetProductByIdAsync(request.Id);

            //if (product == null)
            //    return 

            return _mapper.Map<ProductReponseModel>(product);

        }
    }
}
