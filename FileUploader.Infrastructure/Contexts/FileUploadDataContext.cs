using Microsoft.EntityFrameworkCore;

namespace FileUploader.Infrastructure.Contexts
{
    internal partial class FileUploadDataContext: DbContext
    {
        public FileUploadDataContext(DbContextOptions<FileUploadDataContext> options) : base(options) { }

        public virtual DbSet<Entities.FileInfo> FileInfos { get; set; }
        public virtual DbSet<Entities.ApplicationLog> ApplicationLogs { get; set; }
    }
}
