using System;
using System.Collections.Generic;
using System.Text;
using MicroRabbit.Infra.Buss;
using MicroRabot.Domain.Core.Buss;
using Microsoft.Extensions.DependencyInjection;

namespace MicroRabbit.Infra.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection service)
        {
            //domain 
            service.AddTransient<IEventBuss, RabbitMQBuss>();
        }
    }
}
