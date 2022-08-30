using Arvato.Task.DB.Interfaces;
using Arvato.Task.DB.Repositories;
using Arvato.Task.Fixer.Interfaces;
using Arvato.Task.Fixer.Managers;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Arvato.Task.Fixer.Mapper;
using Microsoft.AspNetCore.Hosting;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using PetaPoco;
using PetaPoco.Providers;

namespace Arvato.Task.Console
{
    public class Startup
    {
        IConfiguration Configuration { get; }

        public Startup()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add Hangfire services.
            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(Configuration.GetConnectionString("hangfireConnection"), new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true
                }));

            // Add the processing server as IHostedService
            services.AddHangfireServer();
            services.BuildServiceProvider().GetRequiredService<IGlobalConfiguration>();

            services.AddLogging();
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddTransient<IFixerManager, FixerManager>();

            services.AddTransient<IBaseRepository, BaseRepository>();
            services.AddTransient<ICurrencyRepository, CurrencyRepository>();
            services.AddTransient<IRateRepository, RateRepository>();
            services.AddAutoMapper(typeof(FixerProfile));
            services.AddScoped<IDatabase, Database>(ctx => new Database<SqlServerDatabaseProvider>(Configuration["ConnectionStrings:umbracoDbDSN"]));
            services.AddMvc();

            //IDatabase _db = DatabaseConfiguration.Build()
            //        .UsingConnectionString(Configuration["ConnectionStrings:umbracoDbDSN"])
            //        .UsingProvider<SqlServerDatabaseProvider>()
            //        .Create();
            //services.AddSingleton(_db);
        }


        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHangfireDashboard();
            });
            app.UseHangfireDashboard();

        }
    }
}
