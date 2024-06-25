using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols;


namespace AG.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

            services.AddMediatR(typeof(DependencyInjection));

            return services;

        }
    }
}
