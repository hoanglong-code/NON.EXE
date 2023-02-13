namespace NON.EXE.Models
{
    public class ImageResponseDTO
    {
        public List<UploadImage> UploadImages { get; set; } = new List<UploadImage>();
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
    }
}
