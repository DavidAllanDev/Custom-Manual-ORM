using Custom.Manual.ORM.Base.Data;
using Custom.Manual.ORM.Data.Connection;
using Custom.Manual.ORM.Domain.DomainClasses;
using Custom.Manual.ORM.Base.Interfaces;
using Custom.Manual.ORM.Domain.DomainMapping;

namespace Custom.Manual.ORM.Data.Repositories.Managers
{
    public class DemoClassManager : EntityManager<DemoClass, int>
    {
        public DemoClassManager(IDBConnection dbConnection) : base(dbConnection)
        {
            IEntityMap mapper = new DemoMap();
            TableName = mapper.EntityTableName();
            ExplicitMappings = mapper.EntityMapper();
        }
    }
}
