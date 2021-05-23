using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUploader.Infrastructure.Entities
{
    internal class ApplicationLog
    {
        [Key]
        public int Id { get; set; }
        public string MessageCode { get; set; }
        public string MessageText { get; set; }
        public string Exception { get; set; }
        public DateTime CratedDate { get; set; }
    }
}
