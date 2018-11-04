using System;
using System.Collections.Generic;
using System.Threading;
using RESTfulAPIDesign.Models;
using RESTfulAPIDesign.Models.Context;

namespace RESTfulAPIDesign.Services.Implementations
{
    public class PersonServiceImpl : IPersonService
    {
        private MySQLContext context;
        private volatile int count;

        public PersonServiceImpl(MySQLContext context)
        {
            this.context = context;
        }

        public Person Create(Person person)
        {
            try
            {
                this.context.Add(person);
                this.context.SaveChanges();
            }
            catch(Exception exception)
            {
                throw exception;
            }
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
                LastName = "Natanael",
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
                FirstName = "Person Name" + i,
                LastName = "Person Second Name" + i,
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
