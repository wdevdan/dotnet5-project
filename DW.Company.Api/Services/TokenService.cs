using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using DW.Company.Contracts.Services;
using DW.Company.Entities.Dto;
using DW.Company.Entities.Value;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DW.Company.Api.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private Claim[] GetUserClaims(UserDto user)
        {
            return new Claim[] {
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim(ClaimTypes.Name, user.FirstName),
                    new Claim(ClaimTypes.Surname, user.LastName),
                    new Claim(ClaimTypes.NameIdentifier, user.Login),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(CustomClaimTypes.USERID, user.Id.ToString()),
                    new Claim(CustomClaimTypes.VALIDUNTIL, user.ValidUntil.ToString("dd/MM/yyyy"))
            };
        }

        private string GetSecret()
        {
            var _section = _configuration.GetSection("Jwt");
            return _section.GetValue(typeof(string), "KeySecret").ToString();
        }

        public Response<Token> Get(Response<UserDto> response)
        {
            var _response = new Response<Token>();
            var _user = response.Content;
            var _tokenHandler = new JwtSecurityTokenHandler();
            var _keyBytes = Encoding.ASCII.GetBytes(GetSecret());
            var _tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(GetUserClaims(_user)),
                Expires = DateTime.Now.AddYears(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_keyBytes), SecurityAlgorithms.HmacSha256Signature)
            };
            var _token = _tokenHandler.CreateToken(_tokenDescriptor);
            _response.Content = new Token
            {
                Value = _tokenHandler.WriteToken(_token),
                User = _user
            };
            return _response;
        }
    }
}
