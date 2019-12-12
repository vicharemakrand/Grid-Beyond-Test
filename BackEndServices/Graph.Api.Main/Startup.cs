using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using AutoMapper;
using AutoMapper.Data;
using Graph.IDomainServices.AutoMapper;
using Graph.Utility;
using Lamar;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog;

namespace Graph.Api.Main
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            LogManager.LoadConfiguration(String.Concat(env.ContentRootPath, "/nlog.config"));

            Configuration = configuration;
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }


        public void ConfigureContainer(ServiceRegistry services)
        {
            services.ConfigureMvc();
            services.AddAutoMapper(cfg =>
                {
                    cfg.AddDataReaderMapping();
                },
                Assembly.GetAssembly(typeof(ModelAutoMapperProfiler))
            );
            services.AddApiVersioning(o => o.ApiVersionReader = new HeaderApiVersionReader("api-version"));
            services.ConfigureCors();
             services.AddSwaggerDocument();
            services.ConfigureIoc(Configuration.GetValue<string>("IocAssemblies"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AutoMapper.IConfigurationProvider autoMapper)
        {

            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });

            app.MiddlewareSwagger(env);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute().RequireAuthorization();
            });
            app.Run(async (context) =>
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync("Page not found");
            });
        }
    }

}
