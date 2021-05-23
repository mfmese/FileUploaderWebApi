using FileUploader.Domain.Ports;
using FileUploader.Infrastructure.Contexts;
using FileUploader.Infrastructure.Entities;
using System;
using System.Threading.Tasks;

namespace FileUploader.Infrastructure.Repositories
{
    internal class LogRepository : ILogPort
    {
        private readonly FileUploadDataContext _dbContext;
        public LogRepository(FileUploadDataContext fileUploadDataContext)
        {
            _dbContext = fileUploadDataContext;
        }
 
        public async Task<bool> InsertLogAsync(string messageText, Exception exception, string messageCode = "300")
        {
            try
            {
                var logEntity = new ApplicationLog
                {
                    MessageCode = messageCode,
                    MessageText = messageText,
                    Exception = exception.Message,
                    CratedDate = DateTime.Now
                };

                _dbContext.Add(logEntity);
                return await _dbContext.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Log Exception: " + ex.Message);
                return false;
            }
        }
    }
}
