using Sat.Recruitment.Api.Dtos.Request;
using Sat.Recruitment.Core.Entities;
using Sat.Recruitment.Core.Enums;
using System;

namespace Sat.Recruitment.Api.Mappings
{
    public class UserMapping : IUserMapping
    {
        public User MapFromUserRequestDtoToUserDomain(UserRequestDTO userRequestDTO)
        {
            return new User()
            {
                Address = userRequestDTO.Address,
                Email = userRequestDTO.Email.ToLower().Trim(),
                Money = userRequestDTO.Money,
                Name = userRequestDTO.Name,
                Phone = userRequestDTO.Phone,
                UserType = (UserType)Enum.Parse(typeof(UserType), userRequestDTO.UserType)
            };
        }
    }
}
