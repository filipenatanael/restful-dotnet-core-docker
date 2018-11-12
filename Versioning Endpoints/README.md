# Introduction to API Versioning Best Practices

- Go to NuGet and install: Microsoft.AspNetCore.Mvc.Versioning 2.3.0

*http://localhost:8080/api/v1.0/persons*
```C#
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PersonsController : Controller { ...  }
```

[Versioning via the URL Path](https://github.com/Microsoft/aspnet-api-versioning/wiki/Versioning-via-the-URL-Path)
[API Versioning Options](https://github.com/Microsoft/aspnet-api-versioning/wiki/API-Versioning-Options)
[Introduction to API Versioning Best Practices](https://nordicapis.com/introduction-to-api-versioning-best-practices)
[RESTful API Designing guidelines - The best practices](https://hackernoon.com/restful-api-designing-guidelines-the-best-practices-60e1d954e7c9)
[LINQ - Lambda Expressions](https://www.youtube.com/watch?v=3EEP9JxqLpE)