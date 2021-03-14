using System;
using System.Collections.Generic;

namespace backend.Domain.Interfaces.Repositories.Base
{
    public interface IRepositoryBase<T>
        where T : class
    {
        IEnumerable<T> GetAll();
        T GetOne(T entity);
        T Add(T entity);
        T Edit(T entity);
        void Remove(T entity);

        bool Exists(Func<T, bool> where);
    }
}
