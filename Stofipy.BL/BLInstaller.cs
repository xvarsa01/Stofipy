using Microsoft.Extensions.DependencyInjection;
using Stofipy.BL.Facades.Interfaces;
using Stofipy.BL.Mappers.InterfaceBase;

namespace Stofipy.BL;

public static class BLInstaller
{
    public static IServiceCollection AddBLServices(this IServiceCollection services)
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

        return services;
    }
}
