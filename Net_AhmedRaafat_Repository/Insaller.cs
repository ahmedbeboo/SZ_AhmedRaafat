using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;

namespace Net_AhmedRaafat_Repository
{

    public static class Installer
    {
        public static void ConfigureServices(IServiceCollection services, string dbConnString)
        {
            // for init db context             
            services.AddDbContext<SQLContext>(opt => opt.UseSqlServer(dbConnString));



        }

        public static void Configure(IServiceScope serviceScope)
        {
            serviceScope.ServiceProvider.GetService<SQLContext>().Database.Migrate();
        }
    }


}
