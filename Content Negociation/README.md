# Content Negociation

**Installed:**
- Microsoft.AspNetCore.Mvc.Formatters.Xml

**Startup.cs**
```C#
       		using Microsoft.Net.Http.Headers;
	 
            services.AddMvc(options =>
            {
	                options.RespectBrowserAcceptHeader = true;
	                options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("text/xml"));
	                options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("application/json"));
            }).AddXmlSerializerFormatters();

```

**Postman configuration headers:**
- KEY: Accept VALUE: text/xml
- KEY: Accept VALUE: application/json

[Versioning via the URL Path](https://github.com/Microsoft/aspnet-api-versioning/wiki/Versioning-via-the-URL-Path)
[API Versioning Options](https://github.com/Microsoft/aspnet-api-versioning/wiki/API-Versioning-Options)
[Introduction to API Versioning Best Practices](https://nordicapis.com/introduction-to-api-versioning-best-practices)
[RESTful API Designing guidelines - The best practices](https://hackernoon.com/restful-api-designing-guidelines-the-best-practices-60e1d954e7c9)
[LINQ - Lambda Expressions](https://www.youtube.com/watch?v=3EEP9JxqLpE)
[Exception InnerException](https://docs.microsoft.com/pt-br/dotnet/api/system.exception.innerexception?view=netframework-4.7.2)

[Postman Endpoits](/RESTfulAPIDesign/Postman)
