using System;
using System.Collections.Generic;
using System.Linq;

namespace FileUploader.Infrastructure.Entities
{
    internal static class FileInfoMap
    {
        public static FileUploader.Domain.Models.FileInfo Map(this FileInfo source)
        {
            if (source == null) return null;

            return new FileUploader.Domain.Models.FileInfo
            {
                FileName = source.FileName,
                FileSize = source.FileSize,
                FileType = source.FileType,
                UploadedDate = source.UploadedDate
            };
        }

        public static List<FileUploader.Domain.Models.FileInfo> Map(this List<FileInfo> source)
        {
            return source.Select(x => x.Map()).ToList();
        }


    }
}
