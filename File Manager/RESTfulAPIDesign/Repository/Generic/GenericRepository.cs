using Microsoft.EntityFrameworkCore;
using RESTfulAPIDesign.Models.Base;
using RESTfulAPIDesign.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RESTfulAPIDesign.Repository.Generic
{   
    // public class GenericRepository<Book> : IRepository<Book> where Book : BaseEntity
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly MySQLContext context;
        private DbSet<T> dataset;

        public GenericRepository(MySQLContext _context)
        {
            this.context = _context;
            this.dataset = this.context.Set<T>();
        }

        public List<T> FindAll()
        {
            // (Not valied code): MySQLContext->Users->ToList();
            return this.dataset.ToList();
        }

        public T FindById(long id)
        {
            return this.dataset.SingleOrDefault(p => p.Id.Equals(id));
        }

        public T Create(T entity)
        {
            try
            {
                // Example: this.book.Add(book);
                this.dataset.Add(entity);
                this.context.SaveChanges();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return entity;
        }

        public T Update(T entity)
        {
            if (!Exists(entity.Id)) return null;

            var result = dataset.SingleOrDefault(b => b.Id == entity.Id);
            if (result != null)
            {
                try
                {
                    this.context.Entry(result).CurrentValues.SetValues(entity);
                    this.context.SaveChanges();
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }
            return result;
        }

        public void Delete(long id)
        {
            // Search for a person who ID is equals to person.Id received by param
            var registry = this.dataset.SingleOrDefault(p => p.Id.Equals(id));

            try
            {
                if (registry != null)
                {
                    this.dataset.Remove(registry);
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
            return this.dataset.Any(p => p.Id.Equals(id));
        }

        public List<T> FindWithPagedSearch(string query)
        {
            return dataset.FromSql<T>(query).ToList();
        }

        public int GetCount(string query)
        {
            // https://stackoverflow.com/questions/40557003/entity-framework-core-count-does-not-have-optimal-performance
            var result = "";
            using (var connection = this.context.Database.GetDbConnection())
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    result = command.ExecuteScalar().ToString();
                }
            }

            return Int32.Parse(result);
        }
    }
}
