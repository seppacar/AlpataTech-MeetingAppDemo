using AlpataTech.MeetingAppDemo.DAL.Extensions;
using AlpataTech.MeetingAppDemo.DAL.Repository;
using AlpataTech.MeetingAppDemo.Services.Mapper;
using AlpataTech.MeetingAppDemo.Services.MeetingService;
using AlpataTech.MeetingAppDemo.Services.UserService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// TODO: Database Configuration
var dbConnectionString = builder.Configuration.GetConnectionString("Development");
builder.Services.SetupDbContext(dbConnectionString);
// Repositories
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<MeetingRepository>();

// Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMeetingService, MeetingService>();

// Automapper Profiles
builder.Services.AddAutoMapper(typeof(UserProfile));
builder.Services.AddAutoMapper(typeof(MeetingProfile));
builder.Services.AddAutoMapper(typeof(MeetingParticipantProfile));
builder.Services.AddAutoMapper(typeof(MeetingDocumentProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure static files
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
