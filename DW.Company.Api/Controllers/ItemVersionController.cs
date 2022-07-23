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
    public partial class ItemVersionController : ControllerBase
    {
        private readonly IItemVersionService _service;
        public ItemVersionController(IItemVersionService service)
        {
            _service = service;
        }

        [HttpGet, Authorize]
        public Response<IEnumerable<ItemVersionDto>> Get() => _service.GetAll();

        [HttpGet("{page}/{size}"), Authorize]
        public Response<Pagination<ItemVersionDto>> Get(int page, int size, [FromQuery] string field, [FromQuery] string key) => _service.Search(page, size, field, key);

        [HttpGet("{id}"), Authorize]
        public Response<ItemVersionDto> Get(int id) => _service.GetById(id);

        [HttpPost, Authorize]
        public Response<ItemVersionDto> Post([FromBody] ItemVersionDto value) => _service.Add(value);

        [HttpPut("{id}"), Authorize]
        public Response Put(int id, [FromBody] ItemVersionDto value) => _service.Update(id, value);

        [HttpDelete("{id}"), Authorize]
        public Response Delete(int id) => _service.Delete(id);
    }
}
