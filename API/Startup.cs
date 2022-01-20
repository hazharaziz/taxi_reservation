using Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repository;
using Repository.Interfaces;
using Repository.Repositories;
using Service;
using Service.Managers;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSingleton<IRepository<User>, UserRepository>();
            services.AddSingleton<IRepository<Passenger>, PassengerRepository>();
            services.AddSingleton<IRepository<Driver>, DriverRepository>();
            services.AddSingleton<ITripRepository, TripRepository>();

            services.AddSingleton<CreditManager>();
            services.AddSingleton<DriverManager>();
            services.AddSingleton<TripManager>();
            services.AddSingleton<MapService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
