using AG.Domain.Products.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AG.Domain.Products.Repositories
{
    public interface IProductRepository
    {
        Task<bool> AddProductAsync(ProductEntity product);
        Task<bool> UpdateProductAsync(ProductEntity product);
        Task<ProductEntity> GetProductByIdAsync(long id);
        Task<List<ProductEntity>> GetProductsAsync(int page, int itemQuantity);
        Task<bool> InactivateProductAsync(long id);
    }
}
