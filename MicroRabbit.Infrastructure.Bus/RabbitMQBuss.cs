using MicroRabbit.Domain.Core.Bus;
using MicroRabbit.Domain.Core.Commands;
using MicroRabbit.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MicroRabbit.Infrastructure.Bus
{
    public sealed class RabbitMQBuss : IEventBus
    {
        private IMediator _mediator { get; }
        private Dictionary<string, List<Type>> _handlers { get; set; } = new Dictionary<string, List<Type>>();
        private List<Type> _events { get; set; } = new List<Type>();
        public RabbitMQBuss(IMediator mediator)
        {
            _mediator = mediator;
        }
        public Task SendCommand<T>(T command) where T : Command
        {
            return command != null ? _mediator?.Send(command) : Task.CompletedTask;
        }

        public void Publish<T>(T @event) where T : Event
        {
            var factory = new ConnectionFactory() {HostName = "local"};
            using var connection = factory.CreateConnection();
            using var channel = connection?.CreateModel();
            if (channel is null) return;
            var eventName = @event.GetType().Name;
            channel.QueueDeclare(eventName, false, false, false, null);
            var message = JsonConvert.SerializeObject(@event);
            var body = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish("", eventName,null,body);
        }


        /// <summary>
        /// Extensions are used,
        /// Subscribers will get all the handlers type that is described here
        /// </summary>
        /// <typeparam name="T">Event</typeparam>
        /// <typeparam name="TH">IEventHandler</typeparam>
        /// <returns>Void</returns>
        public void Subscribe<T, TH>()
            where T : Event
            where TH : IEventHandler<T>
        {
            var eventName = typeof(T).Name;
            var handlerType = typeof(TH);
            _events.AddItemIfMisssing(typeof(T));
            _handlers.AddKeyIfMissing(eventName);
            if (_handlers[eventName].ContainsType(handlerType))
            {
                throw new ArgumentException(
                    $"Handler type {handlerType.Name} is already registered under {eventName} key");
            }
            _handlers[eventName].Add(handlerType);
            StartBasicConsume<T>();
        }

        private void StartBasicConsume<T>() where T : Event
        {
            var factory = new ConnectionFactory() {HostName = "local", DispatchConsumersAsync = true};
            using var connection = factory.CreateConnection();
            using var channel = connection?.CreateModel();
            if (channel is not null)
            {
                var eventName = typeof(T).Name;
                channel.QueueDeclare(eventName);
                var consumer = new AsyncEventingBasicConsumer(channel);
                consumer.Received += Consumer_Received;
                channel.BasicConsume(eventName, true, consumer);
            }
        }

        private async Task Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            var eventName = e.RoutingKey;
            var body = e.Body.ToArray();
            var message = Encoding.UTF8.GetString(body ?? Array.Empty<byte>());
            try
            {
                await ProcessEvent(eventName, message).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

        }

        private async Task ProcessEvent(string eventName, string message)
        {
            var key = _handlers.Keys.FirstOrDefault(x => x == eventName);
            if (key is not null)
            {
                var subscriptions = _handlers[key];
                foreach 
                (
                    var (handler, @event, concreteType) 
                    in from s1 in subscriptions
                        let handler = Activator.CreateInstance(s1)
                        where handler is not null
                        let eventType = _events.FirstOrDefault(s => s.Name == key)
                        where eventType is not null
                        let @event = JsonConvert.DeserializeObject(message, eventType)
                        let concreteType = typeof(IEventHandler<>).MakeGenericType(eventType)
                        select (handler, @event, concreteType)
                )
                {
                    await (Task)concreteType.GetMethod("Handle").Invoke(handler, new object[] { @event });
                }
            }
        }
    }
}
