using MicroRabot.Domain.Core.Buss;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MicroRabot.Domain.Core.Commands;
using MicroRabot.Domain.Core.Events;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace MicroRabbit.Infra.Buss
{
    public sealed class RabbitMQBuss : IEventBuss
    {
        private IMediator _mediator { get; }
        private Dictionary<string, List<Type>> _handlers { get; }
        private List<Type> _eventTypes { get; }

        public RabbitMQBuss(IMediator mediator)
        {
            //container 
            _mediator = mediator;
            _handlers = new Dictionary<string, List<Type>>();
            _eventTypes = new List<Type>();
        }
        public Task SendCommand<T>(T command) where T : Command
        {
            return command != null ? _mediator?.Send(command) : Task.CompletedTask;
        }

        public void Publish<T>(T @event) where T : Event
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost"
            };
            using var connection = factory.CreateConnection();
            using var channel = connection?.CreateModel();
            var eventName = @event?.GetType().Name;
            channel?.QueueDeclare(eventName, false, false, false, null);
            var message = JsonConvert.SerializeObject(@event);
            var body = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish("", eventName,null,body);

        }

        public void Subscribe<T, TH>() where T : Event where TH : IEventHandler<T>
        {
            throw new NotImplementedException();
        }
    }
}
