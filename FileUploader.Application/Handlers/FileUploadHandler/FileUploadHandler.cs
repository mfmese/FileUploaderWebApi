using FileUploader.Domain.Models;
using FileUploader.Domain.Ports;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FileUploader.Application.Handlers.FileUploadHandler
{
    public class FileUploadHandler
    {
        private readonly IConfiguration _configuration;
        private readonly IFileUploadPort _fileUploadPort;
        public FileUploadHandler(IConfiguration configuration, IFileUploadPort fileUploadPort)
        {
            _configuration = configuration;
            _fileUploadPort = fileUploadPort;
        }
        public async Task<FileUploadResponse> HandleAsync(FileUploadHandlerRequest request)
        {
            var response = new FileUploadResponse();

            if (!validateFileType(request.FileName))
            {
                response.SetError("File Type is not in a correct format", null, request.FileName);
                return response;
            }

            if (!validateFileSize(request.FileSize))
            {
                response.SetError("File Size exceeded max file size", null, request.FileName);
                return response;
            }

            return await _fileUploadPort.CreateFileAsync(FileUploadHandlerRequest.Map(request));
        }

        private bool validateFileType(string fileName)
        {
            int startIndex = fileName.LastIndexOf('.');
            if (startIndex < 0)
                return false;

            string fileType = fileName[startIndex..];
            string allowedFileTypeConfiguration = _configuration.GetSection("File:AllowedTypes").Value;

            string[] allowedTypes = allowedFileTypeConfiguration.Split(',');

            foreach (var allowedType in allowedTypes)
                if (allowedType == fileType)
                    return true;
            return false;
        }

        private bool validateFileSize(long fileLength)
        {
            try
            {
                int maxFileSizeConfiguration = Convert.ToInt32(_configuration.GetSection("File:MaxSize").Value);
                if (maxFileSizeConfiguration < fileLength)
                    return false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
