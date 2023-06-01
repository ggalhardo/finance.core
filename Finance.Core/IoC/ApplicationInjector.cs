using Finance.Application.Payments.Commands;
using Finance.Application.Payments.Handlers;
using Finance.Domain._Core.Response;
using Finance.Infrastructure.Persistence;
using Finance.Infrastructure.Persistence.Repository;
using Finance.Infrastructure.Persistence.Repository.Abstractions;
using Finance.Infrastructure.Services.Payments;
using Finance.Infrastructure.Services.Payments.Abstractions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

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

            //Add Database Map
            DatabasePersistence.Configure();

            //Handlers
            services.AddScoped<IRequestHandler<PaymentCreatorCommand, ResponseModel<bool>>, PaymentCreatorHandler>();

            //Services
            services.AddScoped<IPaymentCreator, PaymentCreator>();

            //Repository
            services.AddScoped<IPaymentRepository, PaymentRepository>();
        }

    }
}
