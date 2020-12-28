using System;
using System.Collections.Generic;
using System.Text;
using MicroRabot.Domain.Core.Events;

namespace MicroRabot.Domain.Core.Commands
{
    public abstract class Command : Message
    {
        public DateTime Timestamp { get; protected set; } = DateTime.Now;
    }
}
