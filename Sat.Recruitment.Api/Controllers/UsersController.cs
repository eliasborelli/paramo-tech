using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sat.Recruitment.Api.Dtos.Request;
using Sat.Recruitment.Api.Mappings;
using Sat.Recruitment.Core.Entities;
using Sat.Recruitment.Infraestructure.Commons;
using Sat.Recruitment.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{
    [Route("api/users")]
    public class UsersController : BaseController<UsersController>
    {
        private readonly IUserService _userService;
        private readonly IUserMapping _userMapping;
        public UsersController(IUserService userService, IUserMapping userMapping, ILogger<UsersController> logger) : base(logger)
        {
            _userService = userService;
            _userMapping = userMapping;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserRequestDTO userRequestDTO)
        {
            _logger.LogInformation($"CreateUser - {JsonConvert.SerializeObject(userRequestDTO)}");
            var result = await _userService.CreateUser(_userMapping.MapFromUserRequestDtoToUserDomain(userRequestDTO));
            if (result.Succeeded)
                return FromResult<string>(Result.Success<string>(result.Value));
            
            return FromResult<string>(result);
        }
    }
}
