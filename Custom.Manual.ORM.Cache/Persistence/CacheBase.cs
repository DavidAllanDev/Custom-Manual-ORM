using System;
using System.Linq;
using System.Collections.Generic;
using Custom.Manual.ORM.Cache.Model;

namespace Custom.Manual.ORM.Cache.Persistence
{
    public abstract class CacheBase<T> : ICache<T> where T : class
    {

        protected int _relevance = new int();

        public CacheBase(int relevance) : this()
        {
            _relevance = relevance;
        }

        protected readonly List<T> _entityList;

        protected CacheBase()
        {
            _entityList = new List<T>() { };
        }

        public void Add(T entity)
        {
            _entityList.Add(entity);
        }

        public void AddRange(List<T> entity)
        {
            _entityList.AddRange(entity);
        }

        public void Clear()
        {
            _entityList.Clear();
        }

        public int Estimate()
        {
            return _entityList.Count;
        }

        public T Get(string id)
        {
            return _entityList.FindAll(l => l.Equals(id)).FirstOrDefault();
        }

        public List<T> GetAll()
        {
            return _entityList;
        }

        public List<T> GetRange(int page, int size)
        {
            return _entityList.GetRange(page, size);
        }

        public int Relevance()
        {
            return _relevance;
        }

        public void Remove(string id)
        {
            Remove(Get(id));
        }

        public void Remove(T entity)
        {
            _entityList.Remove(entity);
        }

        public void RemoveRange(List<T> entity)
        {
            _entityList.RemoveRange(entity.IndexOf(entity.FirstOrDefault()), entity.Count);
        }

        public void Update(T entity)
        {
            _entityList.Remove(entity);
            _entityList.Add(entity);
        }

        public void UpdateRange(List<T> entity)
        {
            _entityList.RemoveRange(entity.IndexOf(entity.FirstOrDefault()), entity.Count);
            _entityList.AddRange(entity);
        }
    }
}
