using Microsoft.Extensions.DependencyInjection;
using Stofipy.BL.Facades.Interfaces;
using Stofipy.BL.Mappers.InterfaceBase;
using Stofipy.DAL;

namespace Stofipy.BL;

public static class BLInstaller
{
    public static IServiceCollection AddBLServices(this IServiceCollection services, DALOptions options)
    {
        if (options.MauiApp)
        {
            services.Scan(selector => selector
                .FromAssemblyOf<BusinessLogic>()
                .AddClasses(filter => filter.AssignableTo(typeof(IFacade<,,>)))
                .AsMatchingInterface()
                .WithSingletonLifetime());

            services.Scan(selector => selector
                .FromAssemblyOf<BusinessLogic>()
                .AddClasses(filter => filter.AssignableTo(typeof(IModelMapper<,,>)))
                .AsSelfWithInterfaces()
                .WithSingletonLifetime());
        }
        else
        {
            services.Scan(selector => selector
                .FromAssemblyOf<BusinessLogic>()
                .AddClasses(filter => filter.AssignableTo(typeof(IFacade<,,>)))
                .AsMatchingInterface()
                .WithTransientLifetime());

            services.Scan(selector => selector
                .FromAssemblyOf<BusinessLogic>()
                .AddClasses(filter => filter.AssignableTo(typeof(IModelMapper<,,>)))
                .AsSelfWithInterfaces()
                .WithTransientLifetime());
        }

        return services;
    }
}
