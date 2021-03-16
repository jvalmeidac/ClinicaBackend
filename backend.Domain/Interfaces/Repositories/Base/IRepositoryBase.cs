using System;
using System.Collections.Generic;

namespace backend.Domain.Interfaces.Repositories.Base
{
    public interface IRepositoryBase<T>
        where T : class
    {
        IEnumerable<T> GetAll();
        T GetOne(Guid id);
        void Add(T entity);
        T Edit(T entity);
        void Remove(Guid id);

        bool Exists(Func<T, bool> where);
    }
}
