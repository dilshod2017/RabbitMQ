using MediatR;
using MicroRabbit.Domain.Core.Bus;
using MicroRabbit.Infrastructure.Bus;
using Microsoft.Extensions.DependencyInjection;

namespace MicroRabbit.Infrastructure.IoC
{
    public class DependencyContainer
    {
        public static IServiceCollection RegisterServices(IServiceCollection services)
        {
            //domain bus
            services?.AddTransient<IEventBus, RabbitMQBuss>();
            return services;
        }
    }
}
