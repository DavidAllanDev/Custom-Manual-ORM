using System.Collections.Generic;

namespace Custom.Manual.ORM.Base.Interfaces
{
    public interface IEntityMap
    {
        string EntityTableName();
        Dictionary<string, string> EntityMapper();
    }
}
