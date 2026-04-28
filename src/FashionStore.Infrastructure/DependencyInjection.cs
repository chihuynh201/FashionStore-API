using FashionStore.Application.Common.Interfaces.Authentication;
using FashionStore.Application.Interfaces;
using FashionStore.Infrastructure.Authentication;
using FashionStore.Infrastructure.Persistence;
using FashionStore.Infrastructure.Persistence.Repositories;
using FashionStore.Infrastructure.Services.Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FashionStore.Infrastructure;
public static class DependencyInjection
{
    public static void AddInfrastructureDI(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<FashionStoreDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddJwtConfiguration(configuration);

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();

        services.AddAzureBlobStorage(configuration);
    }
}
