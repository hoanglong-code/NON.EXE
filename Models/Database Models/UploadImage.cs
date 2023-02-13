using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace NON.EXE.Models
{
    public class UploadImage
    {
        [Key]
        public int ImageId { get; set; }
        public string origin_url { get; set; } = string.Empty;
    }
}
