using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Finance.Core.MediatR
{
    public static class MediatRConfiguration
    {

        /// <summary>
        /// Extension to setup MediaR
        /// </summary>
        /// <param name="services">The IServiceCollection</param>
        /// <returns></returns>
        public static void SetupMediaR(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        }
    }
}
