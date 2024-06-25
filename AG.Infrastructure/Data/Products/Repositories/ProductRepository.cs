using AG.Domain.Products.Entities;
using AG.Domain.Products.Repositories;
using AG.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AG.Infrastructure.Data.Products.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly SqlContext _sqlcontext;

        public ProductRepository(SqlContext sqlcontext)
        {
            _sqlcontext = sqlcontext;
        }

        public async Task<bool> AddProductAsync(ProductEntity product)
        {
            _sqlcontext.Products.Add(product);
            var response = await _sqlcontext.SaveChangesAsync();

            if (response > 0)
                return true;
            else
                return false;

        }

        public async Task<ProductEntity> GetProductByIdAsync(long id)
        {
            return await _sqlcontext.Products.FindAsync(id);
        }

        public async Task<List<ProductEntity>> GetProductsAsync(int page, int itemQuantity)
        {
            int skip = (page - 1) * itemQuantity;

            var products = await _sqlcontext.Products
                                    .Skip(skip)
                                    .Take(itemQuantity)
                                    .ToListAsync();


            return products;
        }

        public async Task<bool> InactivateProductAsync(long id)
        {
            var result = await _sqlcontext.Products.FindAsync(id);

            result.IsEnabled = false;
            _sqlcontext.Entry(result).State = EntityState.Modified;

            var response = await _sqlcontext.SaveChangesAsync();

            if (response > 0)
                return true;
            else
                return false;

        }

        public async Task<bool> UpdateProductAsync(ProductEntity product)
        {
            _sqlcontext.Entry(product).State = EntityState.Modified;

            var response = await _sqlcontext.SaveChangesAsync();

            if (response > 0)
                return true;
            else
                return false;
        }
    }
}
