using AG.Domain.Products.Repositories;
using AG.Infrastructure.Data.Context;
using AG.Infrastructure.Data.Products.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AG.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            // Adiciona o contexto do Entity Framework Core
            services.AddDbContext<SqlContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("AG.Api")));

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            // Registra os repositórios no contêiner de injeção de dependência
            services.AddScoped<IProductRepository, ProductRepository>();

            return services;
        }
    }
}
