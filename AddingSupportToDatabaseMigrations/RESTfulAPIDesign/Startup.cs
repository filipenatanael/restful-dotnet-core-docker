using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RESTfulAPIDesign.Models.Context;
using RESTfulAPIDesign.Services;
using RESTfulAPIDesign.Services.Implementations;

namespace RESTfulAPIDesign
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
            var connection = Configuration["MySqlConnection:MySqlConnectionString"];
            services.AddDbContext<MySQLContext>(options => options.UseMySql(connection));

            services.AddMvc();

            services.AddApiVersioning();

            /* Dependency Injection:
             * 
             * This mapping between the interface and the concrete type defines, 
             * that everytime you request a type of IPersonService, you'll get a new instance of the PersonServiceImpl. 
            */
            services.AddScoped<IPersonService, PersonServiceImpl>();
            services.AddScoped<IPersonRepository, PersonRepositoryImpl>();
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
