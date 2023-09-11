using Microsoft.EntityFrameworkCore;
using AlpataTech.MeetingAppDemo.Entities;

namespace AlpataTech.MeetingAppDemo.DAL.Context
{
    public class ApplicationDbContext : DbContext
    {
        //public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {
        //}
        public ApplicationDbContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Replace MSSQL Server String TODO: use settings file
            optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=TestDb;trusted_connection=true;trustservercertificate=true;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User()
                {
                    Id = 1,
                    FirstName = "Test User 1 FN",
                    LastName = "Test User 1 LN"
                },
                new User()
                {
                    Id = 2,
                    FirstName = "Test User 2 FN",
                    LastName = "Test User 2 LN"
                }
                );
        }
        public DbSet<User> Users { get; set; }
    }
}
