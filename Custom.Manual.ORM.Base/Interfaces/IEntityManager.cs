using System.Collections.Generic;

namespace Custom.Manual.ORM.Base.Interfaces
{
    public interface IEntityManager<T, TId>
    {
        T Get(TId id);
        TId Add(T entity);
        void Update(T entity);
        void Delete(TId id);
        void Delete(T entity);
        int GetCount();
        List<T> GetAll();
        List<T> GetCustom(string sql);
    }
}
