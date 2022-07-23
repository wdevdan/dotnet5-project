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
    public partial class CustomerController : ControllerBase
    {
        private readonly ICustomerService _service;
        public CustomerController(ICustomerService service)
        {
            _service = service;
        }

        [HttpGet, Authorize]
        public Response<IEnumerable<CustomerDto>> Get() => _service.GetAll();

        [HttpGet("{page}/{size}"), Authorize]
        public Response<Pagination<CustomerDto>> Search(int page, int size, [FromQuery] string field, [FromQuery] string key) => _service.Search(page, size, field, key);

        [HttpGet("{id}"), Authorize]
        public Response<CustomerDto> Get(int id) => _service.GetById(id);

        [HttpPost, Authorize]
        public Response<CustomerDto> Post([FromBody] CustomerDto value) => _service.Add(value);

        [HttpPut("{id}"), Authorize]
        public Response Put(int id, [FromBody] CustomerDto value) => _service.Update(id, value);

        [HttpPut("{id}/{productLinkType}"), Authorize]
        public Response Put(int id, int productLinkType) => _service.UpdateProductLinkType(id, productLinkType);

        [HttpDelete("{id}"), Authorize]
        public Response Delete(int id) => _service.Delete(id);
    }
}
