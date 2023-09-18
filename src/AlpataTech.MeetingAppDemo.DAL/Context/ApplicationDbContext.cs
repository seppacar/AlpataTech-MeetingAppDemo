using Microsoft.EntityFrameworkCore;
using AlpataTech.MeetingAppDemo.Entities;

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
            // AutoInclude navigation properties
            modelBuilder.Entity<Meeting>()
                .Navigation(m => m.Documents)
                .AutoInclude();
            modelBuilder.Entity<Meeting>()
                .Navigation(m => m.Participants)
                .AutoInclude();

            // Seed Users
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
                        PasswordHash = "1234567890",
                        ProfileImage = "test.png"
                    },
                    new User()
                    {
                        Id = 2,
                        FirstName = "User2 FN",
                        LastName = "User2 LN",
                        Email = "user2@test.com",
                        PhoneNumber = "1234567890",
                        PasswordHash = "1234567890",
                        ProfileImage = "test.png"
                    },
                    new User()
                    {
                        Id = 3,
                        FirstName = "User3 FN",
                        LastName = "User3 LN",
                        Email = "user3@test.com",
                        PhoneNumber = "1234567890",
                        PasswordHash = "1234567890",
                        ProfileImage = "test.png"
                    },
                    new User()
                    {
                        Id = 4,
                        FirstName = "User4 FN",
                        LastName = "User4 LN",
                        Email = "user3@test.com",
                        PhoneNumber = "1234567890",
                        PasswordHash = "1234567890",
                        ProfileImage = "test.png"
                    }
            }
            );

            // Seed Meetings
            modelBuilder.Entity<Meeting>().HasData(
                new Meeting
                {
                    Id = 1,
                    Title = "Meeting 1",
                    Description = "Description for Meeting 1",
                    StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddHours(2),
                    OrganizerId = 1, // Assign an organizer (User with Id 1)
                },
                new Meeting
                {
                    Id = 2,
                    Title = "Meeting 2",
                    Description = "Description for Meeting 2",
                    StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddHours(2),
                    OrganizerId = 2, // Assign an organizer (User with Id 2)
                }
            );

            // Seed Meeting Documents
            modelBuilder.Entity<MeetingDocument>().HasData(
                new MeetingDocument
                {
                    Id = 1,
                    MeetingId = 1, // Associated with Meeting 1
                    DocumentTitle = "Document 1",
                    DocumentPath = "path/to/document1.pdf",
                },
                new MeetingDocument
                {
                    Id = 2,
                    MeetingId = 2, // Associated with Meeting 2
                    DocumentTitle = "Document 2",
                    DocumentPath = "path/to/document2.pdf",
                }
            );

            // Seed Meeting Participants (Link Users to Meetings)
            modelBuilder.Entity<MeetingParticipant>().HasData(
                new MeetingParticipant
                {
                    Id = 1,
                    MeetingId = 1, // Meeting 1
                    UserId = 1,     // User 1
                },
                new MeetingParticipant
                {
                    Id = 2,
                    MeetingId = 2, // Meeting 2
                    UserId = 2,     // User 2
                },
                new MeetingParticipant
                {
                    Id = 3,
                    MeetingId = 2, // Meeting 2
                    UserId = 3,     // User 2
                },
                new MeetingParticipant
                {
                    Id = 4,
                    MeetingId = 2, // Meeting 2
                    UserId = 4,     // User 2
                }
            );
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<MeetingParticipant> MeetingParticipants { get; set; }
        public DbSet<MeetingDocument> MeetingDocuments { get; set; }
    }
}
