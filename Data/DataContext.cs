using Microsoft.EntityFrameworkCore;
using NON.EXE.Models;

namespace NON.EXE.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<SuperHero> SuperHeroes { get; set; }
        public DbSet<UploadImage> UploadImages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<LOL> LOLs { get; set; }
        public DbSet<MailRequest> MailRequests { get; set; }
    }
}
