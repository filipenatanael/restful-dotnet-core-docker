using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore; 
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using RESTfulAPIDesign.Models.Context;
using RESTfulAPIDesign.Repository.Generic;
using RESTfulAPIDesign.Services;
using RESTfulAPIDesign.Services.Implementations;
using System;
using System.Collections.Generic;

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

            if(this.environment.IsDevelopment())
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

                } catch(Exception exception)
                {
                    this.logger.LogCritical("Database migration failed: ", exception);
                    throw;
                }
            }

            services.AddMvc(options =>
            {
                options.RespectBrowserAcceptHeader = true;
                options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("text/xml"));
                options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("application/json"));
            }).AddXmlSerializerFormatters();

            services.AddApiVersioning();

            /* Dependency Injection:as
             * 
             * This mapping between the interface and the concrete type defines, 
             * that everytime you request a type of IPersonService, you'll get a new instance of the PersonServiceImpl. 
            */
            services.AddScoped<IPersonService, PersonServiceImpl>();
            services.AddScoped<IBookService, BookServiceImpl>();
            services.AddScoped<IPersonRepository, PersonRepositoryImpl>();

            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
