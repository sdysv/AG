using AG.Application.Products.Models;
using AG.Domain.Products.Entities;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AG.Application.Products.Requests
{
    public class AddProductsCommandRequest : IRequest<ErrorOr<bool>>
    {
        public ProductEntity Product { get; set; }
    }
}
