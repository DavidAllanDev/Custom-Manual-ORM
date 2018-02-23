using Custom.Manual.ORM.Cache.Model.Formats;

namespace Custom.Manual.ORM.Cache.Model
{
    public interface IMemoryCache<T> : ICache<T> where T : class
    {
        bool Busy();
        bool MarkedToDispose();
        T CacheItem();
        ref object Refence { get; }
        MemoryTypes MemoryType { get; }        
    }
}
