using Custom.Manual.ORM.Cache.Model.Formats;

namespace Custom.Manual.ORM.Cache.Model
{
    public interface IFileCache<T> : ICache<T> where T : class
    {
        string FileName();
        string FilePath();
        bool Stored();
        bool Busy();
        bool MarkedToRemove();
        T CacheItem();
        FileTypes FileType { get; set; }
    }
}
