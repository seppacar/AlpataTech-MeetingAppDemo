using AlpataTech.MeetingAppDemo.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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
