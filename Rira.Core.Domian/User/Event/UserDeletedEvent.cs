using System;
using System.Collections.Generic;
using System.Text;
using static Rira.Utility.Framework.Domain.BaseEvent;

namespace Rira.Core.Domain.User.Event
{
    public class UserDeletedEvent: IDomainEvent
    {
        public Guid? Id { get; private set; }

        public UserDeletedEvent(Guid? id)
        {
            Id = id;
        }
    }
}
