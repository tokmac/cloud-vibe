using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cloud_Vibe.Data.Repositories
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> All();

        T Find(object id);

        void Add(T entity);

        void Update(T entity);

        T Delete(T entity);

        T Delete(object id);

        int SaveChanges();
    }
}