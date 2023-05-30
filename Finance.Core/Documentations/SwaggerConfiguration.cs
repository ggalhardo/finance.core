using System;
using System.IO;
using System.Reflection;
using Finance.Core.Documentations.Const;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

namespace Finance.Core.Documentations
{

    public static class SwaggerConfigurations
    {

        /// <summary>
        /// Extension to register Swagger services
        /// </summary>
        /// <param name="services">The appsettings</param>
        /// <param name="baseDirectory">It's a base directory to access/create files</param>
        public static void AddSwaggerConfiguration(this IServiceCollection services, string baseDirectory)
        {

            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddSwaggerGen(s =>
            {

                //API Docs definitions
                s.SwaggerDoc(ApiConst.TITLE, new OpenApiInfo
                {
                    Version = ApiConst.VERSION,
                    Title = ApiConst.TITLE,
                    Description = ApiConst.DESCRIPTION,
                    Contact = new OpenApiContact { Name = ApiConst.NAME, Email = ApiConst.EMAIL, Url = new Uri(ApiConst.URL) }
                });

                //Ignore Acions decorated with "Obsolete"
                s.IgnoreObsoleteActions();

                //Setup to get comments from XML docs to improve swagger docs
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(baseDirectory, xmlFile);

                //Setup order by actions
                s.OrderActionsBy((apiDesc) => $"{apiDesc.GroupName}_{apiDesc.HttpMethod}");

                //Setup group services api
                s.TagActionsBy(api =>
                {
                    if (api.GroupName != null)
                    {
                        return new[] { api.GroupName };
                    }
                    if (api.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
                    {
                        return new[] { controllerActionDescriptor.ControllerName };
                    }
                    throw new InvalidOperationException("Unable to determine tag for endpoint.");
                });

                s.DocInclusionPredicate((name, api) => true);

                //Mapping Types
                s.MapType<DateTime>(() => new OpenApiSchema { Type = "string", Format = "date", Example = new OpenApiDate(new DateTime(2020, 1, 1)) });
            });
        }

        /// <summary>
        /// Extension to register Swagger middlewares
        /// </summary>
        /// <param name="app">The IApplicationBuilder</param>
        public static void UseSwaggerSetup(this IApplicationBuilder app)
        {

            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            app.UseSwagger(c =>
            {
                c.RouteTemplate = "swagger/{documentName}/swagger.json";
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{ApiConst.TITLE}/swagger.json", ApiConst.TITLE);
                c.RoutePrefix = "swagger";
                c.InjectStylesheet("/wwwroot/css/custom-swagger.css");
                c.DefaultModelsExpandDepth(-1);
            });

            app.UseStaticFiles();
        }
    }
}