using System;
using System.Collections.Generic;
using System.Text;
using static Rira.Utility.Framework.Domain.BaseEvent;

namespace Rira.Core.Domain.User.Event
{
    public class UserUpdatedEvent : IDomainEvent
    {
        public Guid? Id { get; private set; }

        public string? FirstName { get; private set; }

        public string? LastName { get; private set; }

        public string? SSN { get; private set; }

        public string? NationalCode { get; private set; }


        public UserUpdatedEvent(Guid? id, string? firstName, string? lastName, string? sSN, string? nationalCode)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            SSN = sSN;
            NationalCode = nationalCode;
        }
    }
}
