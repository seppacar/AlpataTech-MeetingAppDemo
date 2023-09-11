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
            modelBuilder.Entity<MeetingParticipant>()
                .HasOne(mp => mp.Meeting)
                .WithMany(m => m.Participants)
                .HasForeignKey(mp => mp.MeetingId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete MeetingParticipants when Meeting is deleted

            modelBuilder.Entity<MeetingParticipant>()
                .HasOne(mp => mp.User)
                .WithMany()
                .HasForeignKey(mp => mp.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete MeetingParticipants when User is deleted

            // Exclude cascading delete for the Organizer
            modelBuilder.Entity<Meeting>()
                .HasOne(m => m.Organizer)
                .WithOne()
                .HasForeignKey<Meeting>(m => m.OrganizerId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading deletion of the Organizer (User)

            modelBuilder.Entity<User>().HasData(
                new User[]
                {
        new User()
        {
            Id = 1,
            FirstName = "User1 FN",
            LastName = "User1 LN",
            Email = "user1@test.com",
            PhoneNumber = "1234567890",
            Password = "1234567890",
            ProfileImage = "test.png"
        },
        new User()
        {
            Id = 2,
            FirstName = "User2 FN",
            LastName = "User2 LN",
            Email = "user2@test.com",
            PhoneNumber = "1234567890",
            Password = "1234567890",
            ProfileImage = "test.png"
        },
        new User()
        {
            Id = 3,
            FirstName = "User3 FN",
            LastName = "User3 LN",
            Email = "user3@test.com",
            PhoneNumber = "1234567890",
            Password = "1234567890",
            ProfileImage = "test.png"
        }
                }
            );
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<MeetingParticipant> MeetingParticipants { get; set; }
        public DbSet<MeetingDocument> MeetingDocuments { get; set; }
    }
}
