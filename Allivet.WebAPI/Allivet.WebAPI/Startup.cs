using Allivet.WebAPI.Application;
using Allivet.WebAPI.Domain.Context;
using Allivet.WebAPI.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using System;

namespace Allivet.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            var dbconnectionString = Configuration["ConnectionStrings:AllivetWebApiDb"];

            services.AddApplication()
                .AddMediatR(typeof(Startup))
                .AddAutoMapper(typeof(Startup))
                .AddInfrastructure(Configuration)
                .AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Allivet.WebAPI", Version = "v1" });
            });

            services.AddDbContext<AllivetWebAPIDbContext>(options => options.UseSqlServer(dbconnectionString));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            try
            {
                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                    app.UseSwagger();
                    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Allivet.WebAPI v1"));
                }

                app.UseHttpsRedirection();

                app.UseRouting();

                app.UseAuthorization();

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });

                //migrations and seeds from json files
                using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    if (Configuration["ConnectionStrings:UseInMemoryDatabase"] == "false" && !serviceScope.ServiceProvider.GetService<AllivetWebAPIDbContext>().AllMigrationsApplied())
                    {
                        if (Configuration["ConnectionStrings:UseMigrationService"] == "true")
                            serviceScope.ServiceProvider.GetService<AllivetWebAPIDbContext>().Database.Migrate();
                    }
                    //it will seed tables on aservice run from json files if tables empty
                    if (Configuration["ConnectionStrings:UseSeedService"] == "true")
                        serviceScope.ServiceProvider.GetService<AllivetWebAPIDbContext>().EnsureSeeded();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
          
        }
    }
}
