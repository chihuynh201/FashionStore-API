using FashionStore.Application.Common.Behaviors;
using FashionStore.Application.Common.Mappings;
using FluentValidation;
using Mapster;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FashionStore.Application;
public static class DependencyInjection
{
    public static void AddApplicationDI(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        // Add FluentValidation validators
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        // Add Mapster for object mapping
        services.AddMapster();
        services.RegisterMapsterConfiguration();

    }
}
