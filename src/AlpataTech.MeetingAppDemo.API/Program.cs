using AlpataTech.MeetingAppDemo.DAL.Extensions;
using AlpataTech.MeetingAppDemo.DAL.Repository;
using AlpataTech.MeetingAppDemo.Services.Common.Mapper;
using AlpataTech.MeetingAppDemo.Services.Common.FileStorageService;
using AlpataTech.MeetingAppDemo.Services.Common.LocalFileStorageService;
using AlpataTech.MeetingAppDemo.Services.MeetingService;
using AlpataTech.MeetingAppDemo.Services.UserService;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database Configuration
var dbConnectionString = builder.Configuration.GetConnectionString("Development");
builder.Services.SetupDbContext(dbConnectionString);

// Repositories
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<MeetingRepository>();

// Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMeetingService, MeetingService>();

// Storage Service
var storageOption = builder.Configuration.GetValue<string>("FileStorageOptions:StorageType");

switch (storageOption)
{
    case "local":
        // Register the local file storage service with the storage path
        var localFileStoragePath = builder.Configuration.GetValue<string>("FileStorageOptions:LocalFileStoragePath");
        builder.Services.AddScoped<IFileStorageService>(
            serviceProvider => new LocalFileStorageService(localFileStoragePath));
        break;
    case "azure":
        // Here just for demo
        builder.Services.AddScoped<IFileStorageService, AzureBlobStorageService>();
        break;
    default:
        throw new Exception("Cannot fetch file storage FileStorageOptions from appsettings.json");
}

// Automapper Profiles
builder.Services.AddAutoMapper(typeof(UserProfile));
builder.Services.AddAutoMapper(typeof(MeetingProfile));
builder.Services.AddAutoMapper(typeof(MeetingParticipantProfile));
builder.Services.AddAutoMapper(typeof(MeetingDocumentProfile));

// JSON Web Token Authentication
builder.Services.AddAuthentication().AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("String"))
        };
    }
);


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
