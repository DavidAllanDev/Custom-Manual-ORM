using System;
using System.Collections.Generic;
using Custom.Manual.ORM.Base.Interfaces;

namespace Custom.Manual.ORM.Base.Data
{
    public abstract class EntityManager<T, TId> : IEntityManager<T, TId> where T : IIdentifiable<TId>
    {
        public TId Add(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public T Get(TId id)
        {
            throw new NotImplementedException();
        }

        public List<T> GetAll(T entity)
        {
            throw new NotImplementedException();
        }

        public int GetCount(T entity)
        {
            throw new NotImplementedException();
        }

        public List<T> GetCustom(string sql)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
