using AG.Application.Products.Models;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AG.Application.Products.Requests
{
    public class GetProductsQueryRequest : IRequest<ErrorOr<List<ProductReponseModel>>>
    {
        public int Page { get; set; }
        public int PageQuantity { get; set; }
    }
}
