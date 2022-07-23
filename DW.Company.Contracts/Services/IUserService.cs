using DW.Company.Entities.Dto;
using DW.Company.Entities.Entity;
using DW.Company.Entities.Value;
using System.Collections.Generic;

namespace DW.Company.Contracts.Services
{
    public interface IUserService
    {
        Response<IEnumerable<UserDto>> GetAll();
        Response<Pagination<UserDto>> Search(int page, int size, string field, string key);
        Response<UserDto> Add(UserChangeDto value);
        Response<UserDto> GetAuthenticationResponse(LoginDto value);
        User GetUserById(int id);
        Response<UserDto> GetById(int id);
        Response<UserDto> GetByToken();
        Response Update(int id, UserChangeDto value);
        Response Delete(int id);
        Response ChangePassword(int id, ChangePasswordDto value);
    }
}
