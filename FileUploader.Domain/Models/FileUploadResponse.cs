using FileUploader.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileUploader.Domain.Models
{
    public class FileUploadResponse: BaseResponse<FileUploadResult>
    {
       
    }

    public class FileUploadResult
    {
        public bool IsFileUploaded { get; set; }
    }
}
