# Working With Value Objects

**IParser.cs**
```C#

    public interface IParser<Origin, Destiny>
    {
        /* Origin can be a Value Objects(VO) and Destiny can be a entity */
        Destiny Parse(Origin origin);
        List<Destiny> ParseList(List<Origin> origin);
    }
	
```


**PersonConverter.cs**
```C#

  // public class ParsonConverter : IParser<Origin, Destiny>
    public class PersonConverter : IParser<PersonVO, Person>, IParser<Person, PersonVO>
    {
        public Person Parse(PersonVO origin)
        {
            if (origin == null) return new Person();
            return new Person
            {
                Id = origin.Id,
                FirstName = origin.FirstName,
                LastName = origin.LastName,
                Address = origin.Address,
                Gender = origin.Gender
            };
        }
		{ ... }
	}
		
```
**PersonVO.cs**
```C#

    public class PersonVO
    {
        public long? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
    }
		
```

[Versioning via the URL Path](https://github.com/Microsoft/aspnet-api-versioning/wiki/Versioning-via-the-URL-Path)
[API Versioning Options](https://github.com/Microsoft/aspnet-api-versioning/wiki/API-Versioning-Options)
[Introduction to API Versioning Best Practices](https://nordicapis.com/introduction-to-api-versioning-best-practices)
[RESTful API Designing guidelines - The best practices](https://hackernoon.com/restful-api-designing-guidelines-the-best-practices-60e1d954e7c9)
[LINQ - Lambda Expressions](https://www.youtube.com/watch?v=3EEP9JxqLpE)
[Exception InnerException](https://docs.microsoft.com/pt-br/dotnet/api/system.exception.innerexception?view=netframework-4.7.2)

[Postman Endpoits](/RESTfulAPIDesign/Postman)
