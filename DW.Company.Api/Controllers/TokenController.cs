using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DW.Company.Contracts.Services;
using DW.Company.Entities.Dto;
using DW.Company.Entities.Value;

namespace DW.Company.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        public TokenController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpPost]
        [AllowAnonymous]
        public Response<Token> Authenticate([FromBody] LoginDto loginDto)
        {
            var _response = _userService.GetAuthenticationResponse(loginDto);
            if (_response.Success)
                return _tokenService.Get(_response);

            return new Response<Token>
            {
                Success = _response.Success,
                Message = _response.Message
            };
        }

    }
}
