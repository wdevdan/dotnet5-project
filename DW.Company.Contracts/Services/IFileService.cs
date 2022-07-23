using Microsoft.AspNetCore.Http;
using DW.Company.Entities.Dto;
using DW.Company.Entities.Entity;
using DW.Company.Entities.Value;
using System.Collections.Generic;
using System.IO;

namespace DW.Company.Contracts.Services
{
    public interface IFileService
    {
        FileItem GetFileItemByMD5(string md5);
        Response<FileItemDto> GetFileById(int fileID);
        Response<FileItemDto> Save(FileItem fileData);
        Response<IEnumerable<FileItemDto>> GetByProduct(int pid);
        Response Delete(int pid);
        Response<IEnumerable<FileItemDto>> AddAll(IFormCollection formCollection);
        FileDto GetMedia(string source);
    }
}
