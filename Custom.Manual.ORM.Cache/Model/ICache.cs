using System.Collections.Generic;

namespace Custom.Manual.ORM.Cache.Model
{
    public interface ICache<T> where T : class
    {
        void Add(T entity);
        void AddRange(List<T> entity);
        void Update(T entity);
        void UpdateRange(List<T> entity);
        T Get(string id);
        List<T> GetRange(int page, int size);
        List<T> GetAll();
        void Remove(string id);
        void Remove(T entity);
        void RemoveRange(List<T> entity);
        void Clear();
        int Estimate();
        int Relevance();
    }
}
