using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUploader.Domain.Ports
{
    public interface ILogPort
    {
        Task<bool> InsertLogAsync(string messageText, Exception exception, string messageCode = "300");
    }
}
