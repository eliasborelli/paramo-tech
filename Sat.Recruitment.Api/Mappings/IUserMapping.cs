using Sat.Recruitment.Api.Dtos.Request;
using Sat.Recruitment.Core.Entities;

namespace Sat.Recruitment.Api.Mappings
{
    public interface IUserMapping
    {
        User MapFromUserRequestDtoToUserDomain(UserRequestDTO userRequestDTO);
    }
}
