using Microsoft.AspNetCore.Authorization;
using Serilog;
using Microsoft.Extensions.Configuration;
using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Finance.Core.Infrastructure.Log;
using Elastic.Apm.NetCoreAll;
using Finance.Core.Infrastructure.Documentations;

var environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");

var builder = WebApplication.CreateBuilder(args);

//Configuration
var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                              .AddJsonFile($"appsettings.{environment}.json", optional: true)
                                              .Build();
                                              
Log.Logger = new LoggerConfiguration().WriteTo.Elasticsearch(LoggingConfiguration.createElasticOptions(configuration)).CreateLogger();

builder.Host.UseSerilog();

//Get App Setting
//builder.Services.Configure<OpenIDSettings>(configuration.GetSection("OpenIDSettings"));
//builder.Services.Configure<JwtBearerSettings>(configuration.GetSection("JwtSettings"));
//builder.Services.Configure<AppSettingsVipCommerceApi>(configuration.GetSection("VipCommerceApi"));
//builder.Services.Configure<SettingsLogger>(configuration.GetSection("SettingsLogger"));

//var noSqlSettings = configuration.GetSection("MongoSettings").Get<MongoDbSettings>();
//builder.Services.AddSingleton(noSqlSettings);

//builder.Services.SetupRabbitMq();
//builder.Services.SetupPublishers();

//Add Access HttpContex
builder.Services.AddHttpContextAccessor();

//Add Web Api Config
builder.Services.AddCors();

//Add database configuration
//builder.Services.AddDatabaseNoSqlConfiguration();

//Add IoC configurations Libs/Core/Domain
//builder.Services.AddInfrastructureConfiguration(configuration);

//Add Controllers
builder.Services.AddControllers();

//Add Healtcheck
builder.Services.AddHealthChecks();

//Add Swagger
builder.Services.AddSwaggerConfiguration(AppContext.BaseDirectory);


/*******************************/
/**** START APP SETUPS   *******/
/*******************************/
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

//Add Middleware Global Error Handler
//app.UseMiddleware<ErrorHandlingMiddleware>();

//Add Routing
app.UseRouting();
app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

//app.UseRabbitMq();

//Adding compression request
//app.UseResponseCompression();

app.UseEndpoints(endpoints => {
    endpoints.MapControllers();
    endpoints.MapHealthChecks("health-check").WithMetadata(new AllowAnonymousAttribute());
});

//Add Static Files
app.UseStaticFiles();

app.Run();