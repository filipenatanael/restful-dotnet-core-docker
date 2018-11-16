# HATEOAS

**Installed:**
- Tapioca.HATEOAS

**Startup.cs**
```C#
 		public void ConfigureServices(IServiceCollection services)
        {
	            var filterOptions = new HyperMediaFilterOptions();
	            filterOptions.ObjectContentResponseEnricherList.Add(new PersonEnricher());
	            services.AddSingleton(filterOptions);
		}
	    
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
	            if (env.IsDevelopment())
	            {
	                app.UseDeveloperExceptionPage();
	            }

	            app.UseMvc(routes => {
	                routes.MapRoute(
	                    name: "DefaultApi",
	                    template: "{controller=Values}/{id?}"
	                    );
	            });
       	 }

```

**PersonEnricher.cs**
```C#
    public class PersonEnricher : ObjectContentResponseEnricher<PersonVO>
    {
        protected override Task EnrichModel(PersonVO content, IUrlHelper urlHelper)
        {
	            var path = "api/persons/v1.0";
	            var url = new { controller = path, id = content.Id };
	            content.Links.Add(new HyperMediaLink()
	            {
	                Action = HttpActionVerb.GET,
	                Href = urlHelper.Link("DefaultApi", url),
	                Rel = RelationType.self,
	                Type = ResponseTypeFormat.DefaultGet
	            });
		  }
     }
```

**PersonVO**
```C#
       public class PersonVO : ISupportsHyperMedia 
	   {
			    [DataMember(Order = 6)]
		        public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
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
