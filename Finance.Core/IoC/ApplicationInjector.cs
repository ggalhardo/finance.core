using Finance.Application.Payments.Commands;
using Finance.Application.Payments.Handlers;
using Finance.Application.Payments.Queries;
using Finance.Core.Logging;
using Finance.Domain._Core.Response;
using Finance.Domain.Payments;
using Finance.Infrastructure.Persistence;
using Finance.Infrastructure.Persistence.Repository.Payments;
using Finance.Infrastructure.Persistence.Repository.Payments.Abstractions;
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

            //LoggingTracking
            services.AddScoped<LoggingTracking>(x => new LoggingTracking(Guid.NewGuid()));

            //Add Database Map
            DatabasePersistence.Configure();

            //Handlers
            services.AddScoped<IRequestHandler<PaymentCreatorCommand, ResponseModel<string>>, PaymentCreatorHandler>();
            services.AddScoped<IRequestHandler<PaymentUpdaterCommand, ResponseModel<string>>, PaymentUpdaterHandler>();
            services.AddScoped<IRequestHandler<PaymentDeleterCommand, ResponseModel<string>>, PaymentDeleterHandler>();
            services.AddScoped<IRequestHandler<PaymentSearchOneQuery, ResponseModel<Payment>>, PaymentSearchOneHandler>();

            //Services
            services.AddScoped<IPaymentCreator, PaymentCreator>();
            services.AddScoped<IPaymentUpdater, PaymentUpdater>();
            services.AddScoped<IPaymentDeleter, PaymentDeleter>();
            services.AddScoped<IPaymentSearch, PaymentSearch>();

            //Repository
            services.AddScoped<IPaymentRepository, PaymentRepository>();
        }

    }
}
