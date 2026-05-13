using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rira.Utility.Framework.Domain
{
    public class BaseEvent
    {
        public interface IDomainEvent : INotification { }

        public List<IDomainEvent> DomainEvents { get; } = new List<IDomainEvent>();

        public void AddDomainEvent(IDomainEvent @event)
        {
            DomainEvents.Add(@event);
        }


    }
}
