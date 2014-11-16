using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Cloud_Vibe.Data.Repositories
{
    public interface IRepository<T>  where T : class
    {
        IQueryable<T> All();

        T GetById(object id);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        void Delete(object id);

        void Detach(T entity);

        void UpdateValues(Expression<Func<T, object>> entity);
    }
}