using System;
using System.Collections.Generic;
using Custom.Manual.ORM.Base.Data.Fields;
using Custom.Manual.ORM.Base.Interfaces;

namespace Custom.Manual.ORM.Base.Data.SQL
{
    public abstract class SQLBase<T, TId> where T : IIdentifiable<TId>
    {
        private readonly string _sqlSelectFrom = "SELECT * FROM";
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
            return string.Format("SELECT COUNT(*) FROM {0} ", TableName);
        }
        
        protected string GetSQLUpdateEntity(T entity, string colSetPlaceHolder)
        {
            return string.Format("UPDATE {0} SET {1} WHERE {2} = {3}", TableName, colSetPlaceHolder, MapPropertyNameToColumnName(KeyFields.Id), entity.Id);
        }

        protected string GetSQLGetById(TId id)
        {
            return string.Format("{0} {1} {2}", _sqlSelectFrom, TableName, GetSQLWhereId(id));
        }

        protected string GetSQLDeleteById(TId id)
        {
            return string.Format("DELETE FROM {0} {1}", TableName, GetSQLWhereId(id));
        }

        protected string GetSQLWhereId(TId id)
        {
            string columnName = MapPropertyNameToColumnName(KeyFields.Id);

            if (id.GetType() == typeof(string))
            {
                return String.Format("WHERE {0}='{1}'", columnName, id);
            }
            else if (id.GetType() == typeof(char[]))
            {
                return String.Format("WHERE {0}='{1}'", columnName, id);
            }
            else if (id.GetType() == typeof(char))
            {
                return String.Format("WHERE {0}='{1}'", columnName, id);
            }
            else if (id.GetType() == typeof(int))
            {
                return String.Format("WHERE {0}={1}", columnName, id);
            }
            else if (id.GetType() == typeof(byte))
            {
                return String.Format("WHERE {0}={1}", columnName, id);
            }
            else if (id.GetType() == typeof(sbyte))
            {
                return String.Format("WHERE {0}={1}", columnName, id);
            }
            else if (id.GetType() == typeof(decimal))
            {
                return string.Format("WHERE {0}={1}", columnName, id);
            }
            else if (id.GetType() == typeof(double))
            {
                return string.Format("WHERE {0}={1}", columnName, id);
            }
            else if (id.GetType() == typeof(float))
            {
                return string.Format("WHERE {0}={1}", columnName, id);
            }
            else if (id.GetType() == typeof(uint))
            {
                return string.Format("WHERE {0}={1}", columnName, id);
            }
            else if (id.GetType() == typeof(long))
            {
                return string.Format("WHERE {0}={1}", columnName, id);
            }
            else if (id.GetType() == typeof(ulong))
            {
                return string.Format("WHERE {0}={1}", columnName, id);
            }
            else if (id.GetType() == typeof(short))
            {
                return string.Format("WHERE {0}={1}", columnName, id);
            }
            else if (id.GetType() == typeof(ushort))
            {
                return string.Format("WHERE {0}={1}", columnName, id);
            }
            else
            {
                return string.Format("WHERE {0}='{1}'", columnName, id);
            }
        }

        protected string GetSQLAddEntity(string colNamePlaceHolder, string colValuePlaceHolder)
        {
            return string.Format("INSERT INTO {0} ({1}) VALUES ({2}) SELECT SCOPE_IDENTITY()", TableName, colNamePlaceHolder, colValuePlaceHolder);
        }

        protected string GetSQLGetAll()
        {
            return string.Format("{0} {1}", _sqlSelectFrom, TableName);
        }
    }
}
