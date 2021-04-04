using backend.Domain.Pagination;
using System;
using System.Collections.Generic;

namespace backend.Domain.Interfaces.Repositories.Base
{
    public interface IRepositoryBase<T>
        where T : class
    {
        List<T> GetAll(PageParameters pageParameters);
        T GetOne(Guid id);
        void Add(T entity);
        T Edit(T entity);
        void Remove(Guid id);

        bool Exists(Guid id);
        
    }
}
