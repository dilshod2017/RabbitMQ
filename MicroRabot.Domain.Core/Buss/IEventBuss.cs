using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MicroRabot.Domain.Core.Commands;
using MicroRabot.Domain.Core.Events;

namespace MicroRabot.Domain.Core.Buss
{
    public interface IEventBuss
    {
        Task SendCommand<T>(T command) where T : Command;
        void Publish<T>(T @event) where T : Event;

        void Subscribe<T, TH>() where T : Event where TH : IEventHandler<T>;
    }
}
