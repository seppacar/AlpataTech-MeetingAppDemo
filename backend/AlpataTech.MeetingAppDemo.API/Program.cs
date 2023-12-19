using AlpataTech.MeetingAppDemo.DAL.Extensions;
using AlpataTech.MeetingAppDemo.DAL.Repository;
using AlpataTech.MeetingAppDemo.Services.AuthService;
using AlpataTech.MeetingAppDemo.Services.Common.EmailService;
using AlpataTech.MeetingAppDemo.Services.Common.FileStorageService;
using AlpataTech.MeetingAppDemo.Services.Common.LocalFileStorageService;
using AlpataTech.MeetingAppDemo.Services.Common.Mapper;
using AlpataTech.MeetingAppDemo.Services.MeetingService;
using AlpataTech.MeetingAppDemo.Services.UserService;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

//Enable gzip compression
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true; // Enable compression for HTTPS connections
    options.Providers.Add<GzipCompressionProvider>(); // Enable Gzip compression
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer",
        new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Enter the JWT string as following: `Bearer Generated-JWT`",
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey
        }
        );
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] { }
            }
        });
});


// Database Configuration
var dbConnectionString = builder.Configuration.GetConnectionString("Development");
builder.Services.SetupDbContext(dbConnectionString);

// Repositories
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<RoleRepository>();
builder.Services.AddScoped<MeetingRepository>();
builder.Services.AddScoped<MeetingDocumentRepository>();

// Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMeetingService, MeetingService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddScoped<IImageService, ImageService>();
// Storage Service
var storageOption = builder.Configuration["FileStorageOptions:StorageType"];

switch (storageOption)
{
    case "local":
        // Register the local file storage service with the storage path
        var localFileStoragePath = builder.Configuration["FileStorageOptions:LocalFileStoragePath"];
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
builder.Services.AddAutoMapper(typeof(FileUploadModelProfile));

// JSON Web Token Authentication
builder.Services.AddAuthentication().AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Authentication:JWT:Secret"])),
            ValidAudience = builder.Configuration["Authentication:JWT:Audience"],
            ValidIssuer = builder.Configuration["Authentication:JWT:Issuer"]
        };
    }
);

// CORS
builder.Services.AddCors(options => options.AddPolicy(name: "NgOrigins", policy =>
{
    policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
}));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("NgOrigins");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
