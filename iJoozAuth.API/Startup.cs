using System;
using System.Data.Common;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using iJoozAuth.API.Service;
using iJoozAuth.API.UserServices;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;

namespace iJoozAuth.API
{
    public class Startup
    {
        private readonly IHostingEnvironment _environment;

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            _environment = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowMyOrigin",
                    builder => builder
                        .WithOrigins("https://fvmembership-ui.firebaseapp.com", "https://localhost:8100")
                        .AllowAnyHeader()
                );
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSingleton<FacebookService, FacebookService>();
            services.AddSingleton<JwtTokenGenerator, JwtTokenGenerator>();
            if (Configuration["AuthConfigLocation"] == "InMemory")
            {
                IdentityServer4InMemorySetup(services);
            }
            else
            {
                IdentityServer4DbSetup(services);
            }
        }

        private void IdentityServer4InMemorySetup(IServiceCollection services)
        {
            services.AddIdentityServer()
                .AddSigningCredential(GetSigningCredential())
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryClients(Config.GetClients())
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddCustomUserStore();
        }

        DbConnection GetDbConnection()
        {
            var connectionString = new MySqlConnectionStringBuilder(
                Configuration["ConnectionStrings:AuthDb"])
            {
                // Connecting to a local proxy that does not support ssl.
                SslMode = MySqlSslMode.None,
            };
            DbConnection connection = new MySqlConnection(connectionString.ConnectionString);

            return connection;
        }

        private void IdentityServer4DbSetup(IServiceCollection services)
        {
            var dbConnection = GetDbConnection();

            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddIdentityServer()
                .AddSigningCredential(GetSigningCredential())
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                        builder.UseMySql(dbConnection.ConnectionString,
                            sql => sql.MigrationsAssembly(migrationsAssembly));
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                        builder.UseMySql(dbConnection.ConnectionString,
                            sql => sql.MigrationsAssembly(migrationsAssembly));

                    // this enables automatic token cleanup. this is optional.
                    options.EnableTokenCleanup = true;
                    options.TokenCleanupInterval = 30; // interval in seconds
                })
                .AddCustomUserStore();
        }

        private X509Certificate2 GetSigningCredential()
        {
            byte[] signingKey = Convert.FromBase64String(Environment.GetEnvironmentVariable("SigningKey"));
            var password = Environment.GetEnvironmentVariable("SigningKeyPassword");
            return new X509Certificate2(new X509Certificate(signingKey, password));
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseCors("AllowMyOrigin");
            if (Configuration["AuthConfigLocation"] != "InMemory")
            {
                InitializeDatabase(app);
            }

            loggerFactory.AddLog4Net();
            app.UseIdentityServer();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
//                app.UseGoogleExceptionLogging();
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }

        private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

                var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                context.Database.Migrate();
                if (!context.Clients.Any())
                {
                    foreach (var client in Config.GetClients())
                    {
                        context.Clients.Add(client.ToEntity());
                    }

                    context.SaveChanges();
                }

                if (!context.IdentityResources.Any())
                {
                    foreach (var resource in Config.GetIdentityResources())
                    {
                        context.IdentityResources.Add(resource.ToEntity());
                    }

                    context.SaveChanges();
                }

                if (!context.ApiResources.Any())
                {
                    foreach (var resource in Config.GetApiResources())
                    {
                        context.ApiResources.Add(resource.ToEntity());
                    }

                    context.SaveChanges();
                }
            }
        }
    }
}