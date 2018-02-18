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
            return String.Format("SELECT COUNT(*) FROM {0} ", TableName);
        }
        
        protected string GetSQLUpdateEntity(T entity, string colSetPlaceHolder)
        {
            return String.Format("UPDATE {0} SET {1} WHERE {2} = {3}", TableName, colSetPlaceHolder, MapPropertyNameToColumnName(KeyFields.Id), entity.Id);
        }

        protected string GetSQLGetById(TId id)
        {
            return String.Format("{0} {1} {2}", _sqlSelectFrom, TableName, GetSQLWhereId(id));
        }

        protected string GetSQLDeleteById(TId id)
        {
            return String.Format("DELETE FROM {0} {1}", TableName, GetSQLWhereId(id));
        }

        protected string GetSQLWhereId(TId id)
        {
            if (id.GetType() == typeof(string))
            {
                return String.Format("WHERE {0}='{1}'", MapPropertyNameToColumnName(KeyFields.Id), id);
            }
            else if (id.GetType() == typeof(char[]))
            {
                return String.Format("WHERE {0}='{1}'", MapPropertyNameToColumnName(KeyFields.Id), id);
            }
            else if (id.GetType() == typeof(char))
            {
                return String.Format("WHERE {0}='{1}'", MapPropertyNameToColumnName(KeyFields.Id), id);
            }
            else if (id.GetType() == typeof(int))
            {
                return String.Format("WHERE {0}={1}", MapPropertyNameToColumnName(KeyFields.Id), id);
            }
            else if (id.GetType() == typeof(byte))
            {
                return String.Format("WHERE {0}={1}", MapPropertyNameToColumnName(KeyFields.Id), id);
            }
            else if (id.GetType() == typeof(sbyte))
            {
                return String.Format("WHERE {0}={1}", MapPropertyNameToColumnName(KeyFields.Id), id);
            }
            else if (id.GetType() == typeof(decimal))
            {
                return String.Format("WHERE {0}={1}", MapPropertyNameToColumnName(KeyFields.Id), id);
            }
            else if (id.GetType() == typeof(double))
            {
                return String.Format("WHERE {0}={1}", MapPropertyNameToColumnName(KeyFields.Id), id);
            }
            else if (id.GetType() == typeof(float))
            {
                return String.Format("WHERE {0}={1}", MapPropertyNameToColumnName(KeyFields.Id), id);
            }
            else if (id.GetType() == typeof(uint))
            {
                return String.Format("WHERE {0}={1}", MapPropertyNameToColumnName(KeyFields.Id), id);
            }
            else if (id.GetType() == typeof(long))
            {
                return String.Format("WHERE {0}={1}", MapPropertyNameToColumnName(KeyFields.Id), id);
            }
            else if (id.GetType() == typeof(ulong))
            {
                return String.Format("WHERE {0}={1}", MapPropertyNameToColumnName(KeyFields.Id), id);
            }
            else if (id.GetType() == typeof(short))
            {
                return String.Format("WHERE {0}={1}", MapPropertyNameToColumnName(KeyFields.Id), id);
            }
            else if (id.GetType() == typeof(ushort))
            {
                return String.Format("WHERE {0}={1}", MapPropertyNameToColumnName(KeyFields.Id), id);
            }
            else
            {
                return String.Format("WHERE {0}='{1}'", MapPropertyNameToColumnName(KeyFields.Id), id);
            }
        }

        protected string GetSQLAddEntity(string colNamePlaceHolder, string colValuePlaceHolder)
        {
            return string.Format("INSERT INTO {0} ({1}) VALUES ({2}) SELECT SCOPE_IDENTITY()", TableName, colNamePlaceHolder, colValuePlaceHolder);
        }

        protected string GetSQLGetAll()
        {
            return String.Format("{0} {1}", _sqlSelectFrom, TableName);
        }
    }
}
