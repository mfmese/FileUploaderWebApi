using FileUploader.Domain.Models;
using FileUploader.Domain.Ports;
using FileUploader.Infrastructure.Contexts;
using FileUploader.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FileUploader.Infrastructure.Repositories
{
    internal class FileUploadRepository : IFileUploadPort
    {
        private readonly FileUploadDataContext _fileUploadDataContext;
        private readonly ILogPort _log;
        public FileUploadRepository(FileUploadDataContext fileUploadDataContext, ILogPort log)
        {
            _fileUploadDataContext = fileUploadDataContext;
            _log = log;
        }

        public async Task<GetFilesResponse> GetAllFilesAsync()
        {
            var response = new GetFilesResponse();
            try
            {
                var fileInfos = await _fileUploadDataContext.FileInfos.ToListAsync();

                response.Data = fileInfos.Map();
                response.SetSuccess();
                return response;
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.SetError("Error occured while getting files");
                await _log.InsertLogAsync("Error occured while getting files", ex);
                return response;
            }
        }
        public async Task<FileUploadResponse> CreateFileAsync(FileUploader.Domain.Models.FileInfo file)
        {
            var response = new FileUploadResponse();
            try
            {
                var isFileExists = _fileUploadDataContext.FileInfos.Any(x => x.FileName == file.FileName);
                if(isFileExists)
                {
                    response.Data.IsFileUploaded = false;
                    response.SetError("File name already exists",null, file.FileName);
                    return response;
                }

                var path = AppDomain.CurrentDomain.BaseDirectory + "fileUploads\\";
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                using (FileStream fileStream = File.Create(path + file.FileName))
                {
                    MemoryStream stream = new MemoryStream(file.FileContent);
                    stream.CopyTo(fileStream);
                    fileStream.Flush();
                }

                var fileInfoEntity = new Entities.FileInfo
                {
                    FileName = file.FileName,
                    FileSize = file.FileSize,
                    FileType = file.FileType,
                    UploadedDate = DateTime.Now
                };

                _fileUploadDataContext.Add(fileInfoEntity);
                bool result = await _fileUploadDataContext.SaveChangesAsync() > 0;

                response.Data.IsFileUploaded = result;
                response.SetSuccess();
                return response;
            }
            catch (Exception ex)
            {
                response.Data.IsFileUploaded = false;
                response.SetError("Error occured while uploading file", ex, file.FileName);
                return response;
            }
        }
    }
}
