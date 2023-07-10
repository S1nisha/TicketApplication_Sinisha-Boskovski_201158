using System;
using System.Collections.Generic;
using System.Text;
using TicketShop.Domain.DomainModels;

namespace TicketShop.Repository.Interface
{
    public interface IRepository<T> where T:  BaseEntity
    {
        IEnumerable<T> GetAll();
        
        T Get(Guid? id);

        void Insert(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
