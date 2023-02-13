using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace NON.EXE.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string PassWord { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        //public string Token { get; set; } = string.Empty;
        //public DateTime Created { get; set; } = DateTime.Now;

    }
}
