using System;
using System.Collections.Generic;
using Custom.Manual.ORM.Base.Data.Fields;
using Custom.Manual.ORM.Base.Interfaces;

namespace Custom.Manual.ORM.Base.Data.SQL
{
    public abstract class SQLBase<T, TId> where T : IIdentifiable<TId>
    {
        protected string TableName;
        protected Dictionary<string, string> ExplicitMappings;

        protected string MapPropertyNameToColumnName(string propertyName)
        {
            if (ExplicitMappings == null)
                return propertyName;

            return ExplicitMappings.ContainsKey(propertyName) ? ExplicitMappings[propertyName] : propertyName;
        }

        protected string GetSQLGetCount()
        {
            return String.Format("SELECT COUNT(*) FROM {0} ", TableName);
        }
        
        protected string GetSQLUpdateEntity(T entity, string colSetPlaceHolder)
        {
            return String.Format("UPDATE {0} SET {1} WHERE {2} = {3}", TableName, colSetPlaceHolder, MapPropertyNameToColumnName(KeyFields.Id), entity.Id);
        }

        protected string GetSQLGetById(TId id)
        {
            string where = GetSQLWhereId(id);

            return String.Format("SELECT * FROM {0} {1}", TableName, where);
        }

        protected string GetSQLDeleteById(TId id)
        {
            string where = GetSQLWhereId(id);

            return String.Format("DELETE FROM {0} {1}", TableName, where);
        }

        protected string GetSQLWhereId(TId id)
        {
            string where = String.Empty;

            if (id.GetType() == typeof(string))
            {
                where = String.Format("WHERE {0}='{1}'", MapPropertyNameToColumnName(KeyFields.Id), id);
            }
            else if (id.GetType() == typeof(int))
            {
                where = String.Format("WHERE {0}={1}", MapPropertyNameToColumnName(KeyFields.Id), id);
            }
            else
            {
                where = String.Format("WHERE {0}={1}", MapPropertyNameToColumnName(KeyFields.Id), id);
            }

            return where;
        }

        protected string GetSQLAddEntity(string colNamePlaceHolder, string colValuePlaceHolder)
        {
            return string.Format("INSERT INTO {0} ({1}) VALUES ({2}) SELECT SCOPE_IDENTITY()", TableName, colNamePlaceHolder, colValuePlaceHolder);
        }

        protected string GetSQLGetAll()
        {
            return String.Format("SELECT * FROM {0} ", TableName);
        }
    }
}
