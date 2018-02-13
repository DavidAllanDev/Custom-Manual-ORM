using System;
using System.Data;
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

            try
            {
                string sql = GetSQLAddEntity(colNamePlaceHolder, colValuePlaceHolder);
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
            Delete(entity.Id);
        }

        public void Delete(TId id)
        {
            var deleteSuccessful = _dbConnection.SetUpdateCommand(GetSQLDeleteById(id));
            if (!deleteSuccessful)
            {
                throw new Exception("BaseDataManager.Update: Error during a entity delete");
            }
        }

        public T Get(TId id)
        {
            _dbConnection.SetCommand(GetSQLGetById(id));
            var reader = _dbConnection.Result();

            if (reader != null)
            {
                DataTable table = new DataTable();
                table.Load(reader);
                List<string> dbColumns = table.Columns.Cast<DataColumn>().Select(p => p.ColumnName).ToList();

                foreach (DataRow itemline in table.Rows)
                {
                    var valueObject = (T)Activator.CreateInstance(typeof(T));

                    foreach (var property in typeof(T).GetProperties())
                    {
                        var columnName = MapPropertyNameToColumnName(property.Name);
                        if (!dbColumns.Any(col => col.Equals(columnName, StringComparison.InvariantCultureIgnoreCase)))
                            continue;


                        var value = itemline[columnName];
                        if (property.PropertyType == typeof(bool))
                        {
                            if (value == DBNull.Value) value = "N";
                            if ((string)value == "T") value = "Y";

                            bool columnValue = (string)value == "Y" ? true : false;
                            property.SetValue(valueObject, columnValue, null);
                        }
                        else
                        {
                            var columnValue = itemline[columnName];
                            if (columnValue == DBNull.Value) columnValue = null;
                            property.SetValue(valueObject, columnValue, null);
                        }
                    }

                    return valueObject;
                }
            }
            _dbConnection.Connection().Close();
            throw new Exception("BaseDataManager.GetCustom: Error accessing database (SqlDataReader returned as NULL)");
        }

        public List<T> GetAll()
        {
            List<T> resultsList = new List<T>();

            _dbConnection.SetCommand(GetSQLGetAll());
            var reader = _dbConnection.Result();

            if (reader != null)
            {
                DataTable table = new DataTable();
                table.Load(reader);
                List<string> dbColumns = table.Columns.Cast<DataColumn>().Select(p => p.ColumnName).ToList();

                foreach (DataRow itemline in table.Rows)
                {
                    var valueObject = (T)Activator.CreateInstance(typeof(T));

                    foreach (var property in typeof(T).GetProperties())
                    {
                        var columnName = MapPropertyNameToColumnName(property.Name);
                        if (!dbColumns.Any(col => col.Equals(columnName, StringComparison.InvariantCultureIgnoreCase)))
                            continue;


                        var value = itemline[columnName];
                        if (property.PropertyType == typeof(bool))
                        {
                            if (value == DBNull.Value) value = "N";
                            if ((string)value == "T") value = "Y";

                            bool columnValue = (string)value == "Y" ? true : false;
                            property.SetValue(valueObject, columnValue, null);
                        }
                        else
                        {
                            var columnValue = itemline[columnName];
                            if (columnValue == DBNull.Value) columnValue = null;
                            property.SetValue(valueObject, columnValue, null);
                        }
                    }

                    resultsList.Add(valueObject);
                }
            }
            else
            {
                _dbConnection.Connection().Close();
                throw new Exception("BaseDataManager.GetCustom: Error accessing database (SqlDataReader returned as NULL)");
            }

            _dbConnection.Connection().Close();

            return resultsList;
        }

        private string GetSQLGetAll()
        {
            return String.Format("SELECT * FROM {0} ", TableName);
        }

        public int GetCount()
        {
            int result = new int();

            string sql = GetSQLGetCount();

            result = _dbConnection.SetCountCommand(sql);

            _dbConnection.Connection().Close();

            return result;
        }

        private string GetSQLGetCount()
        {
            return String.Format("SELECT COUNT(*) FROM {0} ", TableName);
        }

        public List<T> GetCustom(string sql)
        {
            List<T> resultsList = new List<T>();

            _dbConnection.SetCommand(sql);
            var reader = _dbConnection.Result();

            if (reader != null)
            {
                DataTable table = new DataTable();
                table.Load(reader);
                List<string> dbColumns = table.Columns.Cast<DataColumn>().Select(p => p.ColumnName).ToList();

                foreach (DataRow itemline in table.Rows)
                {
                    var valueObject = (T)Activator.CreateInstance(typeof(T));

                    foreach (var property in typeof(T).GetProperties())
                    {
                        var columnName = MapPropertyNameToColumnName(property.Name);
                        if (!dbColumns.Any(col => col.Equals(columnName, StringComparison.InvariantCultureIgnoreCase)))
                            continue;


                        var value = itemline[columnName];
                        if (property.PropertyType == typeof(bool))
                        {
                            if (value == DBNull.Value) value = "N";
                            if ((string)value == "T") value = "Y";

                            bool columnValue = (string)value == "Y" ? true : false;
                            property.SetValue(valueObject, columnValue, null);
                        }
                        else
                        {
                            var columnValue = itemline[columnName];
                            if (columnValue == DBNull.Value) columnValue = null;
                            property.SetValue(valueObject, columnValue, null);
                        }
                    }

                    resultsList.Add(valueObject);
                }
            }
            else
            {
                _dbConnection.Connection().Close();
                throw new Exception("BaseDataManager.GetCustom: Error accessing database (SqlDataReader returned as NULL)");
            }

            _dbConnection.Connection().Close();

            return resultsList;
        }

        public void Update(T entity)
        {
            string colSetPlaceHolder = string.Empty;
            bool firstColumn = true;

            IList<PropertyInfo> persistanceProperties = GetPersistenceProperties(entity);
            foreach (PropertyInfo property in persistanceProperties)
            {
                if (!firstColumn)
                {
                    colSetPlaceHolder += ", ";
                }

                firstColumn = false;

                string columnName = MapPropertyNameToColumnName(property.Name);
                object columnValue = null;

                if (property.PropertyType.IsEnum)
                {
                    columnValue = Convert.ToInt32(property.GetValue(entity, null));
                }
                else
                {
                    columnValue = property.GetValue(entity, null) ?? DBNull.Value;
                }

                colSetPlaceHolder += string.Format("[{0}] = {1}", columnName, columnValue);
            }

            string sql = GetSQLUpdateEntity(entity, colSetPlaceHolder);

            var updateSuccessful = _dbConnection.SetUpdateCommand(sql);
            if (!updateSuccessful)
            {
                throw new Exception("BaseDataManager.Update: Error during the entity update");
            }
        }

        private string GetSQLUpdateEntity(T entity, string colSetPlaceHolder)
        {
            return String.Format("UPDATE {0} SET {1} WHERE {2} = {3}", TableName, colSetPlaceHolder, MapPropertyNameToColumnName(KeyFields.Id), entity.Id);

        }

        private string GetSQLGetById(TId id)
        {
            string where = GetSQLWhereId(id);

            return String.Format("SELECT * FROM {0} {1}", TableName, where);
        }

        private string GetSQLDeleteById(TId id)
        {
            string where = GetSQLWhereId(id);

            return String.Format("DELETE FROM {0} {1}", TableName, where);
        }

        private string GetSQLWhereId(TId id)
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

        private string GetSQLAddEntity(string colNamePlaceHolder, string colValuePlaceHolder)
        {
            return string.Format("INSERT INTO {0} ({1}) VALUES ({2}) SELECT SCOPE_IDENTITY()", TableName, colNamePlaceHolder, colValuePlaceHolder);
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
