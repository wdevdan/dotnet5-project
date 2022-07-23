using Microsoft.AspNetCore.Builder;
using DW.Company.Api.Middlewares;
using DW.Company.Api.Services;

namespace DW.Company.Api.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static void Middlewares(this IApplicationBuilder app)
        {
            app.UseMiddleware<GlobalErrorHandlingMiddleware>();
            app.UseMiddleware<GlobalAuthMiddleware>();
        }

        public static void ImageHandler(this IApplicationBuilder app)
        {
            app.MapWhen(
                context => context.Request.Path.ToString().Equals("/IH"),
                appBranch =>
                {
                    appBranch.UseMiddleware<ImageHandlerMiddleware>();
                }
            );
        }

        public static void Swagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "swagger";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "DW.Company v1");
            });
        }
    }
}
