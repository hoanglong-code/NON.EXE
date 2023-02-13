namespace NON.EXE.Models
{
    public class SuperHeroResponseDTO
    {
        public List<SuperHero> SuperHeros { get; set; } = new List<SuperHero>();
        public int Pages { get; set; }
        public int CurrentPage { get; set; }

    }
}
