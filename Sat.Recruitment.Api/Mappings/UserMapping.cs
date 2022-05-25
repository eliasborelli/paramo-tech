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
            var user = new User();
            switch ((UserType)Enum.Parse(typeof(UserType), userRequestDTO.UserType))
            {
                case UserType.SuperUser:
                    user = new SuperUser() { Address = userRequestDTO.Address, Email = userRequestDTO.Email, Name = userRequestDTO.Name, Phone = userRequestDTO.Phone, Money = userRequestDTO.Money, UserType = UserType.SuperUser };
                    break;
                case UserType.Normal:
                    user = new NormalUser() { Address = userRequestDTO.Address, Email = userRequestDTO.Email, Name = userRequestDTO.Name, Phone = userRequestDTO.Phone, Money = userRequestDTO.Money, UserType = UserType.Normal };
                    break;
                case UserType.Premium:
                    user = new PremiumUser() { Address = userRequestDTO.Address, Email = userRequestDTO.Email, Name = userRequestDTO.Name, Phone = userRequestDTO.Phone, Money = userRequestDTO.Money, UserType = UserType.Premium };
                    break;
                default:
                    break;
            }
            return user;
        }

    }
}
