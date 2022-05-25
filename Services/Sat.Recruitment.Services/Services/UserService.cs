using Sat.Recruitment.Core.Entities;
using Sat.Recruitment.Core.Interfaces;
using Sat.Recruitment.Infraestructure.Commons;
using Sat.Recruitment.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<string>> CreateUser(User user)
        {
            var usertype = user.GetUserType();
            usertype.CalculateMoney();

            var users = await _userRepository.GetUsers();

            //Find Duplicated
            var duplicated = users.FirstOrDefault(u => (u.Email == user.Email || u.Phone == user.Phone) || (u.Name == user.Name && u.Address == user.Address));

            if ((duplicated is null) is false)
                return Result.Failed<string>("User is duplicated");

            await _userRepository.Add(user);

            return Result.Success<string>("User Created");
        }
    }
}
