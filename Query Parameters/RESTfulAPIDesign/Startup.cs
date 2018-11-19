using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using System.Collections.Generic;
using Swashbuckle.AspNetCore.Swagger;
using Tapioca.HATEOAS;

using RESTfulAPIDesign.Hypermidia;
using RESTfulAPIDesign.Models.Context;
using RESTfulAPIDesign.Repository.Generic;
using RESTfulAPIDesign.Security.Configuration;
using RESTfulAPIDesign.Services;
using RESTfulAPIDesign.Services.Implementations;
using RESTfulAPIDesign.Repository;

namespace RESTfulAPIDesign
{
    public class Startup
    {
        private readonly ILogger logger;
        public IConfiguration configuration { get; }
        public IHostingEnvironment environment { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment environment, ILogger<Startup> logger)
        {
            this.configuration = configuration;
            this.environment = environment;
            this.logger = logger;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = configuration["MySqlConnection:MySqlConnectionString"];
            services.AddDbContext<MySQLContext>(options => options.UseMySql(connectionString));

            /*
             * Adding Migrations
            */
            ExecuteMigrations(connectionString);

            /*
             * Authorization
            */
            var signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations);

            var tokenConfigurations = new TokenConfiguration();

            new ConfigureFromConfigurationOptions<TokenConfiguration>(
                this.configuration.GetSection("TokenConfigurations")
            )
            .Configure(tokenConfigurations);

            services.AddSingleton(tokenConfigurations);

            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = signingConfigurations.Key;
                paramsValidation.ValidAudience = tokenConfigurations.Audience;
                paramsValidation.ValidIssuer = tokenConfigurations.Issuer;

                // Validates the signing of a received token
                paramsValidation.ValidateIssuerSigningKey = true;

                // Checks if a received token is still valid
                paramsValidation.ValidateLifetime = true;

                // Tolerance time for the expiration of a token (used in case
                // of time synchronization problems between different
                // computers involved in the communication process)
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });

            // Enables the use of the token as a means of
            // authorizing access to this project's resources
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                    .RequireAuthenticatedUser().Build());
            });


            /*
             * Content negociation - Support to XML and JSON
            */
            services.AddMvc(options =>
            {
                options.RespectBrowserAcceptHeader = true;
                options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("text/xml"));
                options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("application/json"));
            }).AddXmlSerializerFormatters();

            /*
             * HATEOAS filter definitions
            */
            var filterOptions = new HyperMediaFilterOptions();
            filterOptions.ObjectContentResponseEnricherList.Add(new PersonEnricher());
            filterOptions.ObjectContentResponseEnricherList.Add(new BookEnricher());
            services.AddSingleton(filterOptions);

            /*
            * Versioning Endpoints
            */
            services.AddApiVersioning();

            /* 
             * Swagger Configuration
            */
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new Info
                {
                    Title = "Restful API With ASP.NET Core 2.0",
                    Version = "v1.0"
                });
            });

            /* Dependency Injection:as
             * 
             * This mapping between the interface and the concrete type defines, 
             * that everytime you request a type of IPersonService, you'll get a new instance of the PersonServiceImpl. 
            */
            services.AddScoped<IPersonService, PersonServiceImpl>();
            services.AddScoped<IBookService, BookServiceImpl>();
            services.AddScoped<ILoginService, LoginServiceImpl>();
            services.AddScoped<IUserRepository, UserRepositoryImpl>();
            services.AddScoped<IPersonRepository, PersonRepositoryImpl>();
            /* 
             * GenericRepository
            */
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
        }


        private void ExecuteMigrations(string connectionString)
        {
            if (this.environment.IsDevelopment())
            {
                try
                {
                    var evolveConnection = new MySql.Data.MySqlClient.MySqlConnection(connectionString);
                    var evolve = new Evolve.Evolve("evolve.json", evolveConnection, msg => logger.LogInformation(msg))
                    {
                        // Where are the magrations:
                        Locations = new List<string> { "Database/migrations" },
                        // Does not clean database
                        IsEraseDisabled = true
                    };
                    evolve.Migrate();

                }
                catch (Exception exception)
                {
                    this.logger.LogCritical("Database migration failed: ", exception);
                    throw;
                }
            }
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            // Starting our API in Swagger page
            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "DefaultApi",
                    template: "{controller=Values}/{id?}"
                    );
            });
        }
    }
}
