using Custom.Manual.ORM.Data.Repositories.Managers;

namespace Custom.Manual.ORM.Data.Repositories.Context
{
    public interface ICustomBusinessPersistenceDbContext
    {
        DemoClassManager DemoClasses { get; }
    }
}
