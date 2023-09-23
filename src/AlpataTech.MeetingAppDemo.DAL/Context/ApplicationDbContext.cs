using AlpataTech.MeetingAppDemo.Entities;
using Microsoft.EntityFrameworkCore;

namespace AlpataTech.MeetingAppDemo.DAL.Context
{
    public class ApplicationDbContext : DbContext
    {
        //public ApplicationDbContext() { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // TODO: Cannot generate migrations rn, remove this later
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Replace MSSQL Server String TODO: use settings file
            //optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=DEV_AlpataTechMeetingApp;trusted_connection=true;trustservercertificate=true;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Set alternate key "Email" or maybe set Unique index?
            modelBuilder.Entity<User>()
                .HasAlternateKey(u => u.Email);

            // Configure a composite unique index for UserId and MeetingId
            modelBuilder.Entity<MeetingParticipant>()
                .HasIndex(e => new { e.UserId, e.MeetingId })
                .IsUnique();

            // AutoInclude navigation properties
            modelBuilder.Entity<Meeting>()
                .Navigation(m => m.Documents)
                .AutoInclude();
            modelBuilder.Entity<Meeting>()
                .Navigation(m => m.Participants)
                .AutoInclude();
            modelBuilder.Entity<Meeting>()
                .Navigation(m => m.Organizer)
                .AutoInclude(); 
            modelBuilder.Entity<User>()
                .Navigation(u => u.Roles)
                .AutoInclude();
            modelBuilder.Entity<UserRole>()
                .Navigation(ur => ur.Role)
                .AutoInclude();
            modelBuilder.Entity<MeetingParticipant>()
                .Navigation(mp => mp.User)
                .AutoInclude();

            // Seed roles
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin" },
                new Role { Id = 2, Name = "User" }
            // Add other roles as needed
            );
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRole {  get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<MeetingParticipant> MeetingParticipants { get; set; }
        public DbSet<MeetingDocument> MeetingDocuments { get; set; }
    }
}
