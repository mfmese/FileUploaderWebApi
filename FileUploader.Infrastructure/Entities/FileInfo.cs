using System;
using System.ComponentModel.DataAnnotations;

namespace FileUploader.Infrastructure.Entities
{
    internal class FileInfo
    {
        [Key]
        public int Id { get; set; }
        public string FileName { get; set; }
        public long FileSize { get; set; }
        public string FileType { get; set; }
        public DateTime UploadedDate { get; set; }
    }
}
