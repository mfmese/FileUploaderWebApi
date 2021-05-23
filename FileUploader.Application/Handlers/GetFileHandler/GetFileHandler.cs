using FileUploader.Domain.Models;
using FileUploader.Domain.Ports;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileUploader.Application.Handlers.FileUploadHandler
{
    public class GetFileHandler
    {
        private readonly IFileUploadPort _fileUploadPort;
        public GetFileHandler(IFileUploadPort fileUploadPort)
        {
            _fileUploadPort = fileUploadPort;
        }
        public async Task<GetFilesResponse> HandleAsync()
        {
            return await _fileUploadPort.GetAllFilesAsync();
        }
    }
}
