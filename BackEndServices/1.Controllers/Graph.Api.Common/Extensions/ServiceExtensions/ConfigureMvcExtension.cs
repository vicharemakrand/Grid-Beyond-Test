using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Reflection;
using PopCorn.Api.Common.Filters;

public static class ConfigureMvcExtension
{
    public static void ConfigureMvc(this IServiceCollection services) {
        
        services.AddMvc(config =>
        {
            // Add XML Content Negotiation
            config.RespectBrowserAcceptHeader = true;
            config.Filters.Add(new ValidationAttribute());
        });

        services.AddTransient(x =>
        {
            var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
            var factory = x.GetRequiredService<IUrlHelperFactory>();
            return factory.GetUrlHelper(actionContext);
        });
    }
}