using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NON.EXE.Models
{
    public class MailRequest
    {
        [Key]
        public int MailId { get; set; }
        public string ToEmail { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public string Attachments { get; set; } = string.Empty;
        [NotMapped]
        public List<IFormFile>? MailFile { get; set; }

    }
}
