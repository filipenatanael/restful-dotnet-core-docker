using System;
using System.Collections.Generic;
using System.Threading;
using RESTfulAPIDesign.Models;

namespace RESTfulAPIDesign.Services.Implementations
{
    public class PersonServiceImpl : IPersonService
    {
        private volatile count;

        public Person Create(Person person)
        {
            return person;
        }

        public void Delete(long id)
        {
        }

        public List<Person> FindAll()
        {
            List<Person> persons = new List<Person>();

            for(int i = 0; i < 8; i++)
            {
                Person person = MockPerson(i);
                persons.Add(person);
            }
            return persons;
        }

        public Person FindById(long id)
        {
            return new Person
            {
                Id = 1,
                FirstName = "Filipe",
                SecondName = "Natanael",
                Address = "Belo Horizonte - Minas Gerais",
                Gender = "Male"
            };
        }

        public Person Update(Person person)
        {
            return person;
        }

        private Person MockPerson(int i)
        {
            return new Person
            {
                Id = Increment(),
                FirstName = "Filipe" + i,
                SecondName = "Natanael" + i,
                Address = "Belo Horizonte - Minas Gerais" + i,
                Gender = "Male"
            };
        }

        private long Increment()
        {
            return Interlocked.Increment(ref count);
        }
    }
}
