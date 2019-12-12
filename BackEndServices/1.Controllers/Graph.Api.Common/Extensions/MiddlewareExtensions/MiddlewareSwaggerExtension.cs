using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

public static class MiddlewareSwaggerExtension
{
    public static void MiddlewareSwagger(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseOpenApi();
            app.UseSwaggerUi3(c => c.DocumentPath = "/swagger/{documentName}/swagger.json");
        }
    }
}