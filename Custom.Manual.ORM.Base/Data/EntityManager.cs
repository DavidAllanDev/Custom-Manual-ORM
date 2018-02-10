using System;
using System.Collections.Generic;
using Custom.Manual.ORM.Base.Interfaces;
using Custom.Manual.ORM.Data.Connection;

namespace Custom.Manual.ORM.Base.Data
{
    public abstract class EntityManager<T, TId> : IEntityManager<T, TId> where T : IIdentifiable<TId>
    {
        private readonly IDBConnection _dbConnection;

        public EntityManager(IDBConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

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
