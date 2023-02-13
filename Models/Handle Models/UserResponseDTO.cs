namespace NON.EXE.Models
{
    public class UserResponseDTO
    {
        public List<User> Users { get; set; } = new List<User>();
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
    }
}
