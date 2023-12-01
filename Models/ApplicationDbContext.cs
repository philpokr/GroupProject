namespace GroupProject.Models
{
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : DbContext
    {
        private DbSet<FavoriteFoods> favoriteFoods;

        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<Hobby> Hobbies { get; set; }
        public DbSet<FavoriteFoods> FavoriteFoods { get => favoriteFoods; set => favoriteFoods = value; }

        // Constructor with DbContextOptions parameter
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    }
}
