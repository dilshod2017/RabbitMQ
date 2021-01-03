using System;
using System.Text.Json.Serialization;
using LinqToDB.Data;
using MicroRabbit.Infrastructure.IoC;
using MicroRabbit.Users.Data.Context;
using MicroRabbit.Users.Domain.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MicroRabbit.Users.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            DataConnection.DefaultSettings = new DbSetting(Configuration.GetConnectionString("local"));
        }

        public void ConfigureServices(IServiceCollection services)
        {
             services.AddHttpClient();
             services.AddControllers()
                .AddJsonOptions(x => x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
            (services ?? throw new ArgumentNullException(nameof(services))).AddSingleton<DbLoggerCategory.Database>();
            DependencyContainer.RegisterServices(services);
        }

         public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(c => c.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
