using Microsoft.AspNetCore.Authorization;
using Serilog;
using Microsoft.Extensions.Configuration;
using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Finance.Core.Infrastructure._Core.Log;
using Elastic.Apm.NetCoreAll;
using Finance.Core.Infrastructure._Core.Documentations;
using Finance.Core.Infrastructure._Core.Database;

var environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");

var builder = WebApplication.CreateBuilder(args);

//Configuration
var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                              .AddJsonFile($"appsettings.{environment}.json", optional: true)
                                              .Build();
                                              
Log.Logger.createLoggerElasticSearch(configuration);

builder.Host.UseSerilog();

//Add Access HttpContex
builder.Services.AddHttpContextAccessor();

//Add Web Api Config
builder.Services.AddCors();

//Add Controllers
builder.Services.AddControllers();

//Add Healtcheck
builder.Services.AddHealthChecks();

//Add Swagger
builder.Services.AddSwaggerConfiguration(AppContext.BaseDirectory);

//Add MongoDB
builder.Services.AddMongoDBConfiguration(configuration);


var app = builder.Build();

//Adding Apm
app.UseAllElasticApm(app.Configuration);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseDeveloperExceptionPage();
    app.UseSwaggerSetup();
}

if (!app.Environment.IsDevelopment()) {
    app.UseHttpsRedirection();
}

//Add Routing
app.UseRouting();
app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

//Adding compression request
//app.UseResponseCompression();

app.UseEndpoints(endpoints => {
    endpoints.MapControllers();
    endpoints.MapHealthChecks("health-check").WithMetadata(new AllowAnonymousAttribute());
});

//Add Static Files
app.UseStaticFiles();

app.Run();