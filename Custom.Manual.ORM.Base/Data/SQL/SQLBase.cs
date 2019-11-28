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

            if (id is string)
            {
                return string.Format("WHERE {0}='{1}'", columnName, id);
            }
            else if (id.GetType() == typeof(char[]))
            {
                return string.Format("WHERE {0}='{1}'", columnName, id);
            }
            else if (id is char)
            {
                return string.Format("WHERE {0}='{1}'", columnName, id);
            }
            else if (id is int)
            {
                return string.Format("WHERE {0}={1}", columnName, id);
            }
            else if (id is byte)
            {
                return string.Format("WHERE {0}={1}", columnName, id);
            }
            else if (id is sbyte)
            {
                return string.Format("WHERE {0}={1}", columnName, id);
            }
            else if (id is decimal)
            {
                return string.Format("WHERE {0}={1}", columnName, id);
            }
            else if (id is double)
            {
                return string.Format("WHERE {0}={1}", columnName, id);
            }
            else if (id is float)
            {
                return string.Format("WHERE {0}={1}", columnName, id);
            }
            else if (id is uint)
            {
                return string.Format("WHERE {0}={1}", columnName, id);
            }
            else if (id is long)
            {
                return string.Format("WHERE {0}={1}", columnName, id);
            }
            else if (id is ulong)
            {
                return string.Format("WHERE {0}={1}", columnName, id);
            }
            else if (id is short)
            {
                return string.Format("WHERE {0}={1}", columnName, id);
            }
            else if (id is ushort)
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
