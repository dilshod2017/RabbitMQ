using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MicroRabot.Domain.Core.Events;

namespace MicroRabot.Domain.Core.Buss
{
    public interface IEventHandler<in TEvent> : IEventHandler where TEvent : Event
    {
        Task Handle(TEvent @event);
    }
    public interface IEventHandler { }
}
