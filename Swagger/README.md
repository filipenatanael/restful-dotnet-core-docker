# Swagger

**Installed:**
- Swashbuckle.AspNetCore

**Startup.cs**
```C#
          services.AddSwaggerGen(config =>
          {
                config.SwaggerDoc("v1", new Info
                {
                    Title = "Restful API With ASP.NET Core 2.0",
                    Version = "v1.0"
                });
          });
		  
		 public void Configure(IApplicationBuilder app, IHostingEnvironment env)
         {
			    app.UseSwaggerUI(c => {
	               c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
	            });

	            var option = new RewriteOptions();
	            option.AddRedirect("^$", "swagger");
	            app.UseRewriter(option);
		 }
```

**PersonsController.cs**
```C#
        [HttpGet]
        [ProducesResponseType(typeof(List<PersonVO>), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get()
        {
            return new OkObjectResult(this.personService.FindAll());
        }
```


[HATEOAS ](https://pt.stackoverflow.com/questions/49492/por-que-hateoas-%C3%A9-importante)
[Hardcoding](https://pt.wikipedia.org/wiki/Codifica%C3%A7%C3%A3o_r%C3%ADgida)
[Versioning via the URL Path](https://github.com/Microsoft/aspnet-api-versioning/wiki/Versioning-via-the-URL-Path)
[API Versioning Options](https://github.com/Microsoft/aspnet-api-versioning/wiki/API-Versioning-Options)
[Introduction to API Versioning Best Practices](https://nordicapis.com/introduction-to-api-versioning-best-practices)
[RESTful API Designing guidelines - The best practices](https://hackernoon.com/restful-api-designing-guidelines-the-best-practices-60e1d954e7c9)
[LINQ - Lambda Expressions](https://www.youtube.com/watch?v=3EEP9JxqLpE)
[Exception InnerException](https://docs.microsoft.com/pt-br/dotnet/api/system.exception.innerexception?view=netframework-4.7.2)

[Postman Endpoits](/RESTfulAPIDesign/Postman)
