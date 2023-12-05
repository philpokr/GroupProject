namespace GroupProject.Models
{
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : DbContext
    {
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<Hobby> Hobbies { get; set; }
        public DbSet<FavoriteFoods> FavoriteFoods { get => favoriteFoods; set => favoriteFoods = value; }
        public DbSet<DreamJob> DreamJobs { get; set; }
        public DbSet<FavoriteSport> FavoriteSports { get; set; }
        

        // Constructor with DbContextOptions parameter
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    }
}
