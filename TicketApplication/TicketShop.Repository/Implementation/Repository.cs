using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicketShop.Domain.DomainModels;
using TicketShop.Repository.Interface;

namespace TicketShop.Repository.Implementation
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext context;
        private DbSet<T> entitites;
        string errorMessage = string.Empty;

        public Repository(ApplicationDbContext context)
        {
            this.context = context; 
            entitites=context.Set<T>();
        }
        public IEnumerable<T> GetAll() { 
            
            return entitites.AsEnumerable();
        }

        public T Get(Guid? id)
        {
            return entitites.SingleOrDefault(z=> z.Id == id);
        }

        public void Insert(T entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entitites.Add(entity);
            context.SaveChanges();
        }

        public void Update(T entity)
        {
                if(entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                entitites.Update(entity);
            context.SaveChanges();
        }

        public void Delete(T entity) {
            if(entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entitites.Remove(entity);
            context.SaveChanges();
        }
    }
}

