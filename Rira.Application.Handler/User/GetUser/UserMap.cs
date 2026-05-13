using Rira.Application.DTO;
using Rira.Core.Domain.User.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rira.Application.Handler.User.GetUser
{
    public static class UserMap
    {
        public static UserDTO ToDto(this Core.Domain.User.Entity.User User)
        {
            var user= new UserDTO(
                User.Id,
                User.FirstName,
                User.LastName,
                User.SSN,
                User.NationalCode);

            user.RowVersion = User.RowVersion;

            return user;
        }

        public static List<UserDTO> ToDtos(this List<Core.Domain.User.Entity.User> Users)
        {
            var data = new List<UserDTO>();

            foreach (var item in Users) {
                data.Add(new UserDTO(
                item.Id,
                item.FirstName,
                item.NationalCode,
                item.SSN,
                item.LastName));
            }

            return data;
        }
    }
}
