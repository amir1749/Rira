using System;
using System.Collections.Generic;
using System.Text;
using static Rira.Utility.Framework.Domain.BaseEvent;

namespace Rira.Core.Domain.User.Event
{
    public class UserCreatedEvent : IDomainEvent
    {
        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string SSN { get; private set; }

        public string NationalCode { get; private set; }


        public UserCreatedEvent(string firstName, string lastName, string sSN, string nationalCode)
        {
            FirstName = firstName;
            LastName = lastName;
            SSN = sSN;
            NationalCode = nationalCode;
        }
    }
}
