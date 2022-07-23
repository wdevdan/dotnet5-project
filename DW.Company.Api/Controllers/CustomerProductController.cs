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
    public partial class CustomerProductController : ControllerBase
    {
        private readonly ICustomerProductService _service;
        public CustomerProductController(ICustomerProductService service)
        {
            _service = service;
        }

        [HttpGet, Authorize]
        public Response<IEnumerable<CustomerProductDto>> Get() => _service.GetAll();

        [HttpGet("{id}/{page}/{size}"), Authorize]
        public Response<Pagination<CustomerProductDto>> Search(int id, int page, int size, [FromQuery] string field, [FromQuery] string key) => _service.Search(id, page, size, field, key);

        [HttpGet("[action]/{id}/{page}/{size}"), Authorize]
        public Response<Pagination<ProductDto>> SearchUnselectedProducts(int id, int page, int size, [FromQuery] string field, [FromQuery] string key) => _service.SearchUnselectedProducts(id, page, size, field, key);

        [HttpGet("{id}"), Authorize]
        public Response<CustomerProductDto> Get(int id) => _service.GetById(id);

        [HttpPost, Authorize]
        public Response<CustomerProductDto> Post([FromBody] CustomerProductDto value) => _service.Add(value);

        [HttpPut("{id}"), Authorize]
        public Response<CustomerProductDto> Put(int id, [FromBody] CustomerProductDto value) => _service.Update(id, value);

        [HttpDelete("{id}"), Authorize]
        public Response Delete(int id) => _service.Delete(id);
    }
}
