using FileUploader.Application.Handlers.FileUploadHandler;
using FileUploader.Domain.Models;
using FileUploader.WebApi.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Threading.Tasks;

namespace FileUploader.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class FileUploadController : Controller
    {
        private readonly IConfiguration _configuration;
        private FileUploadHandler _fileUploadHandler;
        private readonly GetFileHandler _getFileHandler;

        [ActivatorUtilitiesConstructor]
        public FileUploadController( IConfiguration configuration, FileUploadHandler fileUploadHandler, GetFileHandler getFileHandler)
        {
            _configuration = configuration;
            _fileUploadHandler = fileUploadHandler;
            _getFileHandler = getFileHandler;
        }
        [HttpGet]
        public async Task<GetFilesResponse> GetAllFiles()
        {
            return await _getFileHandler.HandleAsync();
        }

        [HttpPost]
        public async Task<JsonResult> UploadFile([FromForm] UploadFileRequest request)
        {
            var response = new FileUploadResponse();

            if (request.File != null)
            {
                byte[] body;
                using (MemoryStream ms = new MemoryStream())
                {
                    request.File.CopyTo(ms);
                    body = ms.ToArray();
                }

                var model = new FileUploadHandlerRequest
                {
                    FileContent = body,
                    FileName = request.File.FileName,
                    FileType = request.File.ContentType,
                    FileSize = request.File.Length,
                };

                response = await _fileUploadHandler.HandleAsync(model);
            }

            return Json(response);
        }
    }
}
