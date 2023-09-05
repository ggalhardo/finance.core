using Finance.Application.Payments.Commands;
using Finance.Application.Payments.Handlers;
using Finance.Core.Logging;
using Finance.Domain._Core.Response;
using Finance.Infrastructure.Persistence;
using Finance.Infrastructure.Persistence.Repository;
using Finance.Infrastructure.Persistence.Repository.Abstractions;
using Finance.Infrastructure.Services.Payments;
using Finance.Infrastructure.Services.Payments.Abstractions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Finance.Core.IoC
{
    public static class ApplicationInjector
    {

        /// <summary>
        /// Extension to register services in application
        /// </summary>
        /// <param name="services">The IServiceCollection</param>
        public static void RegisterServices(this IServiceCollection services)
        {

            //Logging
            services.AddScoped<LoggingTracking>(x => new LoggingTracking(Guid.NewGuid()));

            //Add Database Map
            DatabasePersistence.Configure();

            //Handlers
            services.AddScoped<IRequestHandler<PaymentCreatorCommand, ResponseModel<bool>>, PaymentCreatorHandler>();
            services.AddScoped<IRequestHandler<PaymentUpdaterCommand, ResponseModel<bool>>, PaymentUpdaterHandler>();

            //Services
            services.AddScoped<IPaymentCreator, PaymentCreator>();
            services.AddScoped<IPaymentUpdater, PaymentUpdater>();

            //Repository
            services.AddScoped<IPaymentRepository, PaymentRepository>();
        }

    }
}
