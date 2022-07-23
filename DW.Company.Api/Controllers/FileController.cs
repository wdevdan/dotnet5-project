using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using AutoMapper;

using DW.Company.Contracts.Services;
using DW.Company.Entities.Value;
using DW.Company.Entities.Dto;

namespace DW.Company.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class FileController : ControllerBase
    {
        private readonly IFileService _service;
        private readonly IMapper _mapper;

        public FileController(IFileService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [RequestFormLimits(ValueLengthLimit = 2147483647, MultipartBodyLengthLimit = 209715200)]
        [DisableRequestSizeLimit]
        [Authorize, HttpPost]
        public Response<IEnumerable<FileItemDto>> Upload() => _service.AddAll(HttpContext.Request.Form);

        [HttpGet("[action]/{pid}"), Authorize(Roles = "master, master_customer")]
        public Response<IEnumerable<FileItemDto>> GetByProduct(int pid) => _service.GetByProduct(pid);

        [HttpGet("[action]/{id}"), Authorize(Roles = "master, master_customer")]
        public Response<FileItemDto> GetFileById(int id) => _service.GetFileById(id);

        [HttpDelete("[action]/{id}"), Authorize(Roles = "master, master_customer")]
        public Response Delete(int id) => _service.Delete(id);

        [HttpGet("[action]/{src}")]
        public FileResult GetMedia(string src, [FromQuery] bool? streaming)
        {
            var _file = _service.GetMedia(src);
            return File(_file.Content, _file.ContentType, _file.FileDownloadName, streaming ?? false);
        }
    }
}