using AlpataTech.MeetingAppDemo.DAL.Context;
using AlpataTech.MeetingAppDemo.DAL.Repository;
using AlpataTech.MeetingAppDemo.Services.Mapper;
using AlpataTech.MeetingAppDemo.Services.UserService;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database Context
builder.Services.AddDbContext<ApplicationDbContext>();

// Repositories
builder.Services.AddScoped<UserRepository>();

// Services
builder.Services.AddScoped<IUserService, UserService>();

// Automapper Profiles
builder.Services.AddAutoMapper(typeof(UserProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
