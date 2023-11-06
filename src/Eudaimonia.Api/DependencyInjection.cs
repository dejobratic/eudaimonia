using Eudaimonia.Application;
using Eudaimonia.Infrastructure;
using Eudaimonia.Presentation;

namespace Eudaimonia.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddProjectDependencies(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddPresentationDependencies(configuration);
        services.AddApplicationDependencies();
        services.AddInfrastructureDependencies(configuration);

        return services;
    }
}