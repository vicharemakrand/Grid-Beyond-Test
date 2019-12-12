using Lamar;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

public static class ConfigureIocExtension
{
    public static void ConfigureIoc(this ServiceRegistry serviceRegistry, string iocAssemblies)
    {

         GetServiceRegistry(serviceRegistry, iocAssemblies)
                         .RegisterSpecialIoc();
    }

    public static ServiceRegistry RegisterSpecialIoc(this ServiceRegistry services)
    {
         services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

        return services;
    }


    private static ServiceRegistry GetServiceRegistry(ServiceRegistry serviceRegistry, string iocAssemblies)
    {
        var namespaces = iocAssemblies.Replace("*.dll", string.Empty).Split("|").ToList();
        serviceRegistry.Scan(scanner =>
        {
            scanner.WithDefaultConventions();
            scanner.AssembliesFromApplicationBaseDirectory(x => namespaces.Any(l => x.FullName.Contains(l)));
        });

        return serviceRegistry;
    }
}