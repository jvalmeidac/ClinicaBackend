using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace backend.Domain.Interfaces.Repositories.Base
{
    public interface IRepositoryBase<T>
        where T : class
    {
        IEnumerable<T> GetAll();
        T GetOne(params Expression<Func<T, object>>[] includePropertie);
        T Add(T entity);
        T Edit(T entity);
        void Remove(Guid id);

        bool Exists(Func<T, bool> where);
    }
}
