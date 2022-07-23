using DW.Company.Entities.Dto;
using DW.Company.Entities.Value;

namespace DW.Company.Contracts.Services
{
    public interface ITokenService
    {
        Response<Token> Get(Response<UserDto> value);
    }
}
