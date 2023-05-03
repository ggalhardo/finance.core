using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

namespace Finance.Core.Infrastructure.Documentations; 

public static class SwaggerConfigurations {
    
    /// <summary>
    ///
    /// </summary>
    public static void AddSwaggerConfiguration(this IServiceCollection services, string baseDirectory) {
        
        if (services == null) {
            throw new ArgumentNullException(nameof(services));
        }
        
        services.AddSwaggerGen(s => {
            
            //API Docs definitions
            s.SwaggerDoc(ApiConstants.V1_TITLE, new OpenApiInfo {
                Version = "v1",
                Title = "Finance.Core",
                Description = "Personal project",
                Contact = new OpenApiContact { Name = "Personal Project", Email = "glauco.galhardo@gmail.com", Url = new Uri("https://github.com/ggalhardo/finance.core") }
            });
            
            //Security Definitions
            /*s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
                Description = "Input the JWT like: Bearer {your token}",
                Name = "Authorization",
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey
            });
            
            s.AddSecurityRequirement(new OpenApiSecurityRequirement{
                {
                    new OpenApiSecurityScheme{
                        Reference = new OpenApiReference{
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });*/
            
            //s.ResolveConflictingActions(x => x.First());
            //Ignore Acions decorated with "Obsolete"
            s.IgnoreObsoleteActions();
            //Setup to get comments from XML docs to improve swagger docs
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(baseDirectory, xmlFile);
            //Setup order by actions
            s.OrderActionsBy((apiDesc) => $"{apiDesc.GroupName}_{apiDesc.HttpMethod}");
            //Setup group services api
            s.TagActionsBy(api => {
                if (api.GroupName != null) {
                    return new[] { api.GroupName };
                }
                if (api.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor) {
                    return new[] { controllerActionDescriptor.ControllerName };
                }
                throw new InvalidOperationException("Unable to determine tag for endpoint.");
            });
            
            s.DocInclusionPredicate((name, api) => true);
            //Mapping Types
            //s.SchemaFilter<CountryRegion>(x => new OpenApiObject(){});
            s.MapType<DateTime>(() => new OpenApiSchema { Type = "string", Format = "date", Example = new OpenApiDate(new DateTime(2020, 1, 1)) });
        });
    }
    
    /// <summary>
    ///
    /// </summary>
    public static void UseSwaggerSetup(this IApplicationBuilder app) {
        if (app == null) {
            throw new ArgumentNullException(nameof(app));
        }
        app.UseSwagger(c => {
            c.RouteTemplate = "swagger/{documentName}/swagger.json";
        });
        app.UseSwaggerUI(c => {
            c.SwaggerEndpoint($"/swagger/{ApiConstants.V1_TITLE}/swagger.json", ApiConstants.V1_TITLE);
            c.RoutePrefix = "swagger";
            c.InjectStylesheet("/wwwroot/css/custom-swagger.css");
            c.DefaultModelsExpandDepth(-1);
        });
        app.UseStaticFiles();
    }
}