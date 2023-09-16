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
            base.OnModelCreating(modelBuilder);

            // AutoInclude navigation properties
            modelBuilder.Entity<Meeting>()
                .Navigation(m => m.Documents)
                .AutoInclude();
            modelBuilder.Entity<Meeting>()
                .Navigation(m => m.Participants)
                .AutoInclude();

            // Seed data for Users
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", PasswordHash = "password123", ProfileImage = "/images/johndoe.jpg" },
                new User { Id = 2, FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com", PasswordHash = "password456", ProfileImage = "/images/janesmith.jpg" },
                new User { Id = 3, FirstName = "Michael", LastName = "Johnson", Email = "michael.johnson@example.com", PasswordHash = "password789", ProfileImage = "/images/michaeljohnson.jpg" }
            );

            // Seed data for Meetings
            modelBuilder.Entity<Meeting>().HasData(
                new Meeting { Id = 1, Title = "Team Meeting", Description = "Discuss project updates", StartTime = DateTime.Now, EndTime = DateTime.Now.AddHours(2), OrganizerId = 1 },
                new Meeting { Id = 2, Title = "Board Meeting", Description = "Financial review", StartTime = DateTime.Now.AddHours(3), EndTime = DateTime.Now.AddHours(5), OrganizerId = 2 },
                new Meeting { Id = 3, Title = "Product Launch Meeting", Description = "Plan the product launch event", StartTime = DateTime.Now.AddHours(1), EndTime = DateTime.Now.AddHours(3), OrganizerId = 3 }
            );

            // Seed data for Meeting Documents
            modelBuilder.Entity<MeetingDocument>().HasData(
                new MeetingDocument { Id = 1, MeetingId = 1, DocumentTitle = "Agenda", DocumentPath = "/documents/agenda.pdf" },
                new MeetingDocument { Id = 2, MeetingId = 2, DocumentTitle = "Financial Report", DocumentPath = "/documents/financial_report.pdf" },
                new MeetingDocument { Id = 3, MeetingId = 3, DocumentTitle = "Launch Event Plan", DocumentPath = "/documents/launch_event_plan.pdf" }
            );

            // Seed data for MeetingParticipants
            modelBuilder.Entity<MeetingParticipant>().HasData(
                new MeetingParticipant { Id = 1, MeetingId = 1, UserId = 2 }, // Jane Smith is participating in Team Meeting
                new MeetingParticipant { Id = 2, MeetingId = 1, AttendeeFirstName = "Alex", AttendeeLastName = "Johnson", AttendeeEmail = "alex.johnson@example.com" }, // Alex Johnson is participating in Team Meeting as a non-user
                new MeetingParticipant { Id = 3, MeetingId = 2, UserId = 3 } // Michael Johnson is participating in Board Meeting
            );
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<MeetingParticipant> MeetingParticipants { get; set; }
        public DbSet<MeetingDocument> MeetingDocuments { get; set; }
    }
}
