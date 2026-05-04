using FashionStore.Application.Features.Categories.Commands.CreateCategory;
using FashionStore.Application.Features.Products.Commands.CreateProduct;
using FashionStore.Domain.Entities;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace FashionStore.Application.Common.Mappings;
public static class MapsterConfig
{
    public static void RegisterMapsterConfiguration(this IServiceCollection services)
    {
        TypeAdapterConfig.GlobalSettings.Default.IgnoreNullValues(true);

        TypeAdapterConfig<CreateProductCommand, Product>.NewConfig()
         .Map(dest => dest.IsEnabled, src => true);

        TypeAdapterConfig<CreateCategoryCommand, Category>.NewConfig()
            .Map(dest => dest.IsEnabled, src => true);
    }
}
