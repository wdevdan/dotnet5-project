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
    public partial class LeatherTypeController : ControllerBase
    {
        private readonly ILeatherTypeService _service;
        public LeatherTypeController(ILeatherTypeService service)
        {
            _service = service;
        }

        [HttpGet, Authorize]
        public Response<IEnumerable<LeatherTypeDto>> Get() => _service.GetAll();

        [HttpGet("{page}/{size}"), Authorize]
        public Response<Pagination<LeatherTypeDto>> Search(int page, int size, [FromQuery] string field, [FromQuery] string key) => _service.Search(page, size, field, key);

        [HttpGet("{id}"), Authorize]
        public Response<LeatherTypeDto> Get(int id) => _service.GetById(id);

        [HttpPost, Authorize]
        public Response<LeatherTypeDto> Post([FromBody] LeatherTypeDto value) => _service.Add(value);

        [HttpPut("{id}"), Authorize]
        public Response Put(int id, [FromBody] LeatherTypeDto value) => _service.Update(id, value);

        [HttpDelete("{id}"), Authorize]
        public Response Delete(int id) => _service.Delete(id);
    }
}
