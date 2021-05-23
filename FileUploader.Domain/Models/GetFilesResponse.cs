using FileUploader.Domain.Models;
using System.Collections.Generic;

namespace FileUploader.Domain.Models
{
    public class GetFilesResponse: BaseResponse<List<FileInfo>>
    {
    }
}
