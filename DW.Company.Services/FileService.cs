using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using DW.Company.Contracts.Data;
using DW.Company.Contracts.Helpers;
using DW.Company.Contracts.Services;
using DW.Company.Contracts.Settings;
using DW.Company.Entities.Dto;
using DW.Company.Entities.Entity;
using DW.Company.Entities.Exceptions;
using DW.Company.Entities.Value;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace DW.Company.Services
{
    public class FileService : IFileService
    {
        private readonly IDBContext _db;
        private readonly IDBHelper _dbHelper;
        private readonly IMapper _mapper;
        private readonly IEnvironmentSettings _environmentSettings;
        
        public FileService(
            IDBContext db,
            IMapper mapper,
            IDBHelper dbHelper,
            IEnvironmentSettings environmentSettings
        )
        {
            _db = db;
            _mapper = mapper;
            _dbHelper = dbHelper;
            _environmentSettings = environmentSettings;
        }

        public FileItem GetFileItemByMD5(string md5)
        {
            return _db.FileItems
                .Where(w => w.Md5.Equals(md5))
                .AsNoTracking()
                .FirstOrDefault();
        }

        public Response<FileItemDto> GetFileById(int fileID)
        {
            var file = _db.FileItems.Where(w => w.Id == fileID).AsNoTracking().FirstOrDefault();

            return new Response<FileItemDto>
            {
                Content = _mapper.Map<FileItemDto>(file)
            };
        }

        public Response<FileItemDto> Save(FileItem fileData)
        {
            var res = new Response<FileItemDto>();

            try
            {
                var result = _db.FileItems.Add(fileData);
                _db.SaveChanges();

                res.Content = _mapper.Map<FileItemDto>(result.Entity);
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = ex.Message;
            }

            return res;
        }

        public Response<IEnumerable<FileItemDto>> GetByProduct(int pid)
        {
            var _response = new Response<IEnumerable<FileItemDto>>();
            var _products = _db.ProductFiles.Where(w => w.ProductId == pid).AsNoTracking().ToList();
            var _files = _db.FileItems.Where(w => _products.Select(s => s.FileItemId).Contains(w.Id)).AsNoTracking().ToList();

            _response.Content = _files?.Select(s => _mapper.Map<FileItemDto>(s));
            return _response;
        }

        public Response Delete(int id)
        {
            var _target = _db.ProductFiles.Where(w => w.FileItemId == id).AsNoTracking().FirstOrDefault();
            _db.ProductFiles.Remove(_mapper.Map<ProductFile>(_target));

            _db.SaveChanges();
            return new Response();
        }

        private string GetFileExtension(IFormFile file) => file.ContentType.Split('/')[1];

        private string GetFilePath(IFormFile file)
        {
            var _type = file.ContentType.Split('/')[0];
            if (_type.Equals("image")) return _environmentSettings.IMAGESDIRECTORY;
            else if (_type.Equals("video")) return _environmentSettings.VIDEOSDIRECTORY;
            throw new BadRequestException(ExceptionMessages.ERR0041);
        }

        private string GetFileName(IFormFile file) => $"{Guid.NewGuid()}.{GetFileExtension(file)}";

        private FileItemDto Add(IFormFile file)
        {
            var _hash = new MD5CryptoServiceProvider().ComputeHash(file.OpenReadStream());
            var _hashString = BitConverter.ToString(_hash).Replace("-", "").ToLowerInvariant();

            var _savedFile = GetFileItemByMD5(_hashString);
            if (_savedFile != null)
            {
                var _file = Path.Combine(Directory.GetCurrentDirectory(), _savedFile.Path, _savedFile.CurrentName);
                if (File.Exists(_file))
                    return _mapper.Map<FileItemDto>(_savedFile);
            }

            FileItemDto _result = null;

            using (var _transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    if (_savedFile == null)
                    {
                        _savedFile = new FileItem
                        {
                            Md5 = _hashString,
                            CreatedAt = DateTime.Now.ToLocalTime(),
                        };
                    }
                    _savedFile.CurrentName = GetFileName(file);
                    _savedFile.Extension = GetFileExtension(file);
                    _savedFile.OriginName = file.FileName;
                    _savedFile.Path = GetFilePath(file);
                    _savedFile.UpdatedAt = DateTime.Now.ToLocalTime();

                    var _pathDirectory = Path.Combine(Directory.GetCurrentDirectory(), _savedFile.Path);
                    if (!Directory.Exists(_pathDirectory)) Directory.CreateDirectory(_pathDirectory);
                    var _pathToSave = Path.Combine(_pathDirectory, _savedFile.CurrentName);
                    using (var _fs = new FileStream(_pathToSave, FileMode.Create))
                        file.CopyTo(_fs);

                    _result = Save(_savedFile).Content;

                    _db.SaveChanges();
                    _transaction.Commit();
                }
                catch
                {
                    _transaction.Rollback();
                    throw;
                }
            }
            return _result;
        }

        public Response<IEnumerable<FileItemDto>> AddAll(IFormCollection formCollection) => new Response<IEnumerable<FileItemDto>> { Content = AddAllFiles(formCollection) };

        private IEnumerable<FileItemDto> AddAllFiles(IFormCollection formCollection)
        {
            if (!formCollection.Files.Any()) throw new BadRequestException(ExceptionMessages.ERR0042);
            foreach (var _file in formCollection.Files)
                yield return Add(_file);
        }
        public FileDto GetMedia(string source)
        {       
            var _file = _db.FileItems.Where(w => w.CurrentName == source).AsNoTracking().FirstOrDefault();
            if (_file != null)
            {
                if (_environmentSettings.VIDEOSEXTENSIONS.Any(extension => _file.Extension.Equals(extension)))
                {
                    var _path = Path.Combine(Directory.GetCurrentDirectory(), _environmentSettings.VIDEOSDIRECTORY, source);
                    if (File.Exists(_path))
                    {
                        var _bytes = File.ReadAllBytes(_path);
                        return new FileDto
                        {
                            Content = _bytes,
                            ContentType = $"Video/{_file.Extension}",
                            FileDownloadName = _file.OriginName,
                        };
                    }
                } else if (_environmentSettings.IMAGESEXTENSIONS.Any(extension => _file.Extension.Equals(extension)))
                {
                    var _path = Path.Combine(Directory.GetCurrentDirectory(), _environmentSettings.IMAGESDIRECTORY, source);
                    if (File.Exists(_path))
                    {
                        var _bytes = File.ReadAllBytes(_path);
                        return new FileDto
                        {
                            Content = _bytes,
                            ContentType = $"Image/{_file.Extension}",
                            FileDownloadName = _file.OriginName,
                        };
                    }
                }
            }
            return new FileDto
            {
                Content = new byte[0]
            };
        }
    }
}
