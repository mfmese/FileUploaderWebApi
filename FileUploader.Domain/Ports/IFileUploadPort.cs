using FileUploader.Domain.Models;
using System.Threading.Tasks;

namespace FileUploader.Domain.Ports
{
    public interface IFileUploadPort
    {
        Task<GetFilesResponse> GetAllFilesAsync();
        Task<FileUploadResponse> CreateFileAsync(FileUploader.Domain.Models.FileInfo file);
    }
}
