using System;
using System.Collections.Generic;
using System.Linq;
using RESTfulAPIDesign.Models;
using RESTfulAPIDesign.Models.Context;

namespace RESTfulAPIDesign.Services.Implementations
{
    public class PersonRepositoryImpl : IPersonRepository
    {
        private MySQLContext context;

        public PersonRepositoryImpl(MySQLContext context)
        {
            this.context = context;
        }
        
        // Method to create persons
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

        // Method to find all persons
        public List<Person> FindAll()
        {
            return this.context.Persons.ToList();
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
            if (!Exists(person.Id)) return new Person();

            // Search for a person who ID is equals to person.Id received by param
            var registry = this.context.Persons.SingleOrDefault(p => p.Id.Equals(person.Id));

            try
            {
                this.context.Entry(registry).CurrentValues.SetValues(person);
                this.context.SaveChanges();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return person;
        }

        public void Delete(long id)
        {
            // Search for a person who ID is equals to person.Id received by param
            var registry = this.context.Persons.SingleOrDefault(p => p.Id.Equals(id));

            try
            {   
                if(registry != null)
                {
                    this.context.Persons.Remove(registry);
                    this.context.SaveChanges();
                }
                
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public bool Exists(long? id)
        {
            return this.context.Persons.Any(p => p.Id.Equals(id));
        }
    }
}
