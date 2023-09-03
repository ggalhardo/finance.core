using Serilog;
using Microsoft.Extensions.Configuration;
using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Elastic.Apm.NetCoreAll;
using HealthChecks.MongoDb;
using Finance.Core.Logging;
using Finance.Core.IoC;
using Finance.Core.Database;
using Finance.Core.Documentations;
using Finance.Core.MediatR;
using Finance.Core.AutoMapper;

var environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");

var builder = WebApplication.CreateBuilder(args);

//Configuration
var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                              .AddJsonFile($"appsettings.{environment}.json", optional: true)
                                              .Build();
                                              
Log.Logger = LoggingConfiguration.CreateLogger(configuration, environment);

builder.Host.UseSerilog();

//Add Access HttpContex
builder.Services.AddHttpContextAccessor();

//Add Web Api Config
builder.Services.AddCors();

//Add Controllers
builder.Services.AddControllers();

//Add Healtcheck
builder.Services.AddHealthChecks();

//Add AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperConfiguration));

//Add Swagger
builder.Services.AddSwaggerConfiguration(AppContext.BaseDirectory);

//Add MongoDB
builder.Services.AddMongoDBConfiguration(configuration);

//Add MongoDbHealthCheck
builder.Services.AddHealthChecks().AddCheck<MongoDbHealthCheck>("MongoDB health-check", null, null);

//Add services
builder.Services.RegisterServices();

//Add MediaR
builder.Services.SetupMediaR();

var app = builder.Build();

//Adding Apm
app.UseAllElasticApm(app.Configuration);

// Configure the HTTP request pipeline.
if (!app.Environment.IsProduction()) {
    app.UseDeveloperExceptionPage();
    app.UseSwaggerSetup();
}

if (app.Environment.IsProduction()) {
    app.UseHttpsRedirection();
}

//Add Routing
app.UseRouting();
app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

//Adding compression request
//app.UseResponseCompression();

app.UseEndpoints(endpoints => {
    endpoints.MapControllers();
    endpoints.MapHealthChecks("health-check");
});

//Add Static Files
//app.UseStaticFiles();

app.Run();