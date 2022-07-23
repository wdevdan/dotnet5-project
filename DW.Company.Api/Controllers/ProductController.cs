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
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;
        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpGet, Authorize]
        public Response<IEnumerable<ProductDto>> Get() => _service.GetAll();

        [HttpGet("{page}/{size}"), AllowAnonymous]
        public Response<Pagination<ProductDto>> Get(int page, int size, [FromQuery] int? categoryId, [FromQuery] int? designerId, [FromQuery] string field, [FromQuery] string key) => _service.Search(page, size, categoryId, designerId, field, key);

        [HttpGet("{id}"), Authorize]
        public Response<ProductDto> Get(int id) => _service.GetById(id);

        [HttpPost, Authorize]
        public Response<ProductDto> Post([FromBody] ProductDto value) => _service.Add(value);

        [HttpPut("{id}"), Authorize]
        public Response Put(int id, [FromBody] ProductDto value) => _service.Update(id, value);

        [HttpDelete("{id}"), Authorize]
        public Response Delete(int id) => _service.Delete(id);
    }
}
