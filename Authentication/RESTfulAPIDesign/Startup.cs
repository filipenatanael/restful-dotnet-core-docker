using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore; 
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using RESTfulAPIDesign.Hypermidia;
using RESTfulAPIDesign.Models.Context;
using RESTfulAPIDesign.Repository.Generic;
using RESTfulAPIDesign.Services;
using RESTfulAPIDesign.Services.Implementations;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using Tapioca.HATEOAS;

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

            // Content negociation - Support to XML and JSON
            services.AddMvc(options =>
            {
                options.RespectBrowserAcceptHeader = true;
                options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("text/xml"));
                options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("application/json"));
            }).AddXmlSerializerFormatters();

            // HATEOAS filter definitions
            var filterOptions = new HyperMediaFilterOptions();
            filterOptions.ObjectContentResponseEnricherList.Add(new PersonEnricher());
            filterOptions.ObjectContentResponseEnricherList.Add(new BookEnricher());
            services.AddSingleton(filterOptions);

            // Versioning
            services.AddApiVersioning();


            // Swagger Configuration
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
            services.AddScoped<IPersonRepository, PersonRepositoryImpl>();

            // GenericRepository
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            // Starting our API in Swagger page
            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);

            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "DefaultApi",
                    template: "{controller=Values}/{id?}"
                    );
            });
        }
    }
}
