using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DW.Company.Contracts.Services;
using DW.Company.Entities.Dto;
using DW.Company.Entities.Value;
using System.Collections.Generic;

namespace DW.Company.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class UserController : ControllerBase
    {
        private readonly IUserService _service;
        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet, Authorize]
        public Response<IEnumerable<UserDto>> Get() => _service.GetAll();

        [HttpGet("{page}/{size}"), Authorize]
        public Response<Pagination<UserDto>> Get(int page, int size, [FromQuery] string field, [FromQuery] string key) => _service.Search(page, size, field, key);

        [HttpGet("{id}"), Authorize]
        public Response<UserDto> Get(int id) => _service.GetById(id);

        [HttpGet("[action]"), Authorize]
        public Response<UserDto> GetByToken() => _service.GetByToken();

        [HttpPost, Authorize]
        public Response<UserDto> Post([FromBody] UserChangeDto value) => _service.Add(value);

        [HttpPut("{id}"), Authorize]
        public Response Put(int id, [FromBody] UserChangeDto value) => _service.Update(id, value);

        [HttpDelete("{id}"), Authorize]
        public Response Delete(int id) => _service.Delete(id);

        [HttpPut("{id}/[action]"), Authorize]
        public Response ChangePassword(int id, [FromBody] ChangePasswordDto value) => _service.ChangePassword(id, value);
    }
}
