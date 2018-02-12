using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using Custom.Manual.ORM.Base.Interfaces;
using Custom.Manual.ORM.Data.Connection;
using Custom.Manual.ORM.Base.Data.Fields;

namespace Custom.Manual.ORM.Base.Data
{
    public abstract class EntityManager<T, TId> : IEntityManager<T, TId> where T : IIdentifiable<TId>
    {
        private readonly IDBConnection _dbConnection;
        protected Dictionary<string, string> ExplicitMappings;
        public string TableName;

        public EntityManager(IDBConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public TId Add(T entity)
        {
            string colNamePlaceHolder = string.Empty;
            string colValuePlaceHolder = string.Empty;
            bool firstColumn = true;

            IList<PropertyInfo> persistanceProperties = GetPersistenceProperties(entity);
            foreach (PropertyInfo property in persistanceProperties)
            {
                if (KeyFields.Id.Equals(property.Name)) continue;

                if (!firstColumn)
                {
                    colNamePlaceHolder += ",";
                    colValuePlaceHolder += ",";
                }
                firstColumn = false;

                string databasefieldName = MapPropertyNameToColumnName(property.Name);

                colNamePlaceHolder += Environment.NewLine + "[" + databasefieldName + "]";

                if (property.PropertyType.IsEnum)
                {
                    colValuePlaceHolder += Convert.ToInt32(property.GetValue(entity, null));
                }
                else
                {
                    colValuePlaceHolder += property.GetValue(entity, null) ?? DBNull.Value;
                }
            }

            string sql = String.Format("INSERT INTO {0} ({1}) VALUES ({2}) SELECT SCOPE_IDENTITY()", TableName, colNamePlaceHolder, colValuePlaceHolder);

            try
            {
                var id = (TId)Convert.ChangeType(_dbConnection.SetInsertCommand(sql), typeof(TId));

                entity.Id = id;

                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public T Get(TId id)
        {
            throw new NotImplementedException();
        }

        public List<T> GetAll(T entity)
        {
            throw new NotImplementedException();
        }

        public int GetCount(T entity)
        {
            throw new NotImplementedException();
        }

        public List<T> GetCustom(string sql)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }

        private string MapPropertyNameToColumnName(string propertyName)
        {
            if (ExplicitMappings == null)
                return propertyName;

            return ExplicitMappings.ContainsKey(propertyName) ? ExplicitMappings[propertyName] : propertyName;
        }

        private IList<PropertyInfo> GetPersistenceProperties(object o)
        {
            PropertyInfo[] objectProperties = o.GetType().GetProperties();
            var persistenceProperties = new List<PropertyInfo>();
            foreach (PropertyInfo property in objectProperties)
            {
                bool ignore = property.GetCustomAttributes(typeof(IgnoreAttribute), false).Any();
                if (ignore || property.Name == KeyFields.Id) continue;

                persistenceProperties.Add(property);
            }

            return persistenceProperties;
        }
    }
}
