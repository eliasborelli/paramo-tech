using Sat.Recruitment.Core.Entities;
using Sat.Recruitment.Infraestructure.Commons;
using System.Threading.Tasks;

namespace Sat.Recruitment.Services.Interfaces
{
    public interface IUserService
    {
        Task<Result<string>> CreateUser(User user);
    }
}
