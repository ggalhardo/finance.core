using Finance.Core.Infrastructure._Core.Database;
using Finance.Core.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace Finance.Core.Infrastructure._Core.IoC {
    public static class ApplicationInjector {

        /// <summary>
        /// Extension to register services in application
        /// </summary>
        /// <param name="services">The IServiceCollection</param>
        public static void RegisterServices(this IServiceCollection services) {

            //Add Database Map
            DatabasePersistence.Configure();
        }

    }
}
