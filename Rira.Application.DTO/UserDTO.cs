using Rira.Utility.Framework.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rira.Application.DTO
{
    public class UserDTO : BaseDTO
    {
        public string FirstName { get; private set; } = default!;

        public string LastName { get; private set; } = default!;

        public string SSN { get; private set; } = default!;

        public string NationalCode { get; private set; } = default!;


        public UserDTO(Guid id,string firstName, string lastName, string sSN, string nationalCode)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            SSN = sSN;
            NationalCode = nationalCode;
        }
    }
}
