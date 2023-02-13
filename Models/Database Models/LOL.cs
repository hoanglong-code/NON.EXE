using System.ComponentModel.DataAnnotations;

namespace NON.EXE.Models
{
    public class LOL
    {
        [Key]
        public int TeamId { get; set; }
        public string Team { get; set; } = string.Empty;
        public string Top { get; set; } = string.Empty;
        public string Jungle { get; set; } = string.Empty;
        public string Mid { get; set; } = string.Empty;
        public string Adc { get; set; } = string.Empty;
        public string Sp { get; set; } = string.Empty;
    }
}
