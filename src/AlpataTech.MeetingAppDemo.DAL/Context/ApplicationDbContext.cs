using Microsoft.EntityFrameworkCore;
using AlpataTech.MeetingAppDemo.Entities;

namespace AlpataTech.MeetingAppDemo.DAL.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Replace MSSQL Server String TODO: use settings file
            optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=TestDb;trusted_connection=true;trustservercertificate=true;");
        }
        public DbSet<User> Users { get; set; }
    }
}
