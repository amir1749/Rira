using Rira.Core.Domain.Common.Exception;
using Rira.Core.Domain.User.Event;
using Rira.Utility.Framework.Domain;
using Rira.Utility.Framework.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rira.Core.Domain.User.Entity
{
    public sealed class User : BaseAuditEntity, IAggregateRoot
    {
        public string FirstName { get; private set; } = default!;

        public string LastName { get; private set; } = default!;

        public string SSN { get; private set; } = default!;

        public string NationalCode { get; private set; } = default!;


        private User()
        {

        }

        public User(
            string? firstName,
            string? lastName,
            string? ssn,
            string? nationalCode)
        {
            SetFirstName(firstName);
            SetLastName(lastName);
            SetSSN(ssn);
            SetNationalCode(nationalCode);

            AddDomainEvent(new UserCreatedEvent(FirstName,LastName,SSN,NationalCode));
        }

        public void Update(
            Guid? id,
            string? firstName,
            string? lastName,
            string? ssn,
            string? nationalCode)
        {
            SetId(id);
            SetFirstName(firstName);
            SetLastName(lastName);
            SetSSN(ssn);
            SetNationalCode(nationalCode);

            AddDomainEvent(new UserUpdatedEvent(Id,FirstName,LastName,SSN,NationalCode));
        }

        private void SetId(Guid? id)
        {
            if (id==null)
                throw new DomainException("id is required.");
            Id = id.Value;
        }

        private void SetFirstName(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new DomainException("First name is required.");

            firstName = firstName.Trim();

            if (firstName.Length < 2 || firstName.Length > 100)
                throw new DomainException("First name length must be between 2 and 100 characters.");

            FirstName = firstName;
        }

        private void SetLastName(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
                throw new DomainException("Last name is required.");

            lastName = lastName.Trim();

            if (lastName.Length < 2 || lastName.Length > 100)
                throw new DomainException("Last name length must be between 2 and 100 characters.");

            LastName = lastName;
        }

        private void SetSSN(string ssn)
        {
            if (string.IsNullOrWhiteSpace(ssn))
                throw new DomainException("SSN is required.");

            ssn = ssn.Trim();

            if (ssn.Length != 9)
                throw new DomainException("SSN must be 9 characters.");

            if (!ssn.All(char.IsDigit))
                throw new DomainException("SSN must contain only digits.");

            SSN = ssn;
        }

        private void SetNationalCode(string nationalCode)
        {
            if (string.IsNullOrWhiteSpace(nationalCode))
                throw new DomainException("National code is required.");

            nationalCode = nationalCode.Trim();

            if (nationalCode.Length != 10)
                throw new DomainException("National code must be 10 digits.");

            if (!nationalCode.All(char.IsDigit))
                throw new DomainException("National code must contain only digits.");

            if (!IsValidNationalCode(nationalCode))
                throw new DomainException("National code is invalid.");

            NationalCode = nationalCode;
        }

        private static bool IsValidNationalCode(string nationalCode)
        {
            if (nationalCode.Length != 10)
                return false;

            var check = nationalCode[9] - '0';

            var sum = 0;

            for (var i = 0; i < 9; i++)
            {
                sum += (nationalCode[i] - '0') * (10 - i);
            }

            var remainder = sum % 11;

            return remainder < 2
                ? check == remainder
                : check == 11 - remainder;
        }
    }
}
