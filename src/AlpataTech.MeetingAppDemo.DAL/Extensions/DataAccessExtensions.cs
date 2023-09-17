using AlpataTech.MeetingAppDemo.DAL.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace AlpataTech.MeetingAppDemo.DAL.Extensions
{
    public static class DataAccessExtensions
    {
        public static IServiceCollection SetupDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            return services;
        }
    }
}
