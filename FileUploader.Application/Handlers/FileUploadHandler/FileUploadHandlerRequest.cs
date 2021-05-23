using FileUploader.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileUploader.Application.Handlers.FileUploadHandler
{
    public class FileUploadHandlerRequest
    {
        public string FileName { get; set; }
        public string FileType { get; set; }
        public byte[] FileContent { get; set; }
        public long FileSize { get; set; }

        public static FileInfo Map(FileUploadHandlerRequest source)
        {
            if (source == null) throw new Exception("FileUploadHandlerRequest cannot be null");

            return new FileInfo
            {
                FileContent = source.FileContent,
                FileName = source.FileName,
                FileType = source.FileType,
                FileSize = source.FileSize
            };
        }
    }
}
