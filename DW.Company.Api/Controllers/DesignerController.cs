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
    public partial class DesignerController : ControllerBase
    {
        private readonly IDesignerService _service;
        public DesignerController(IDesignerService service)
        {
            _service = service;
        }

        [HttpGet, AllowAnonymous]
        public Response<IEnumerable<DesignerDto>> Get() => _service.GetAll();

        [HttpGet("{page}/{size}"), AllowAnonymous]
        public Response<Pagination<DesignerDto>> Get(int page, int size, [FromQuery] string field, [FromQuery] string key) => _service.Search(page, size, field, key);

        [HttpGet("{id}"), Authorize]
        public Response<DesignerDto> Get(int id) => _service.GetById(id);

        [HttpPost, Authorize]
        public Response<DesignerDto> Post([FromBody] DesignerDto value) => _service.Add(value);

        [HttpPut("{id}"), Authorize]
        public Response Put(int id, [FromBody] DesignerDto value) => _service.Update(id, value);

        [HttpDelete("{id}"), Authorize]
        public Response Delete(int id) => _service.Delete(id);
    }
}
