using Custom.Manual.ORM.Data.Connection;
using Custom.Manual.ORM.Data.Repositories.Context;
using Custom.Manual.ORM.Data.Repositories.Managers;

namespace Custom.Manual.ORM.Data.Repositories
{
    public class DemoRepository : ICustomBusinessPersistenceDbContext
    {
        public DemoRepository(IDBConnection dbConnection)
        {
            DemoClasses = new DemoClassManager(dbConnection);
        }

        public DemoClassManager DemoClasses { get; set; }
    }
}