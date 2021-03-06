﻿using System;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using Custom.Manual.ORM.Base.Interfaces;
using Custom.Manual.ORM.Data.Connection;
using Custom.Manual.ORM.Base.Data.Fields;
using Custom.Manual.ORM.Base.Data.SQL;
using System.Data.SqlClient;

namespace Custom.Manual.ORM.Base.Data
{
    public abstract class EntityManager<T, TId> : SQLBase<T, TId>, IEntityManager<T, TId> where T : IIdentifiable<TId> 
    {
        private readonly IDBConnection _dbConnection;

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

                string databasefieldName = MapPropertyInfoToColumnName(property);

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
                TId id = (TId)Convert.ChangeType(_dbConnection.SetInsertCommand(sql), typeof(TId));

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
            bool deleteSuccessful = _dbConnection.SetUpdateCommand(GetSQLDeleteById(id));
            if (!deleteSuccessful)
            {
                throw new Exception("BaseDataManager.Update: Error during a entity delete");
            }
        }

        public T Get(TId id)
        {
            _dbConnection.SetCommand(GetSQLGetById(id));
            SqlDataReader reader = _dbConnection.Result();

            if (reader != null)
            {
                DataTable table = new DataTable();
                table.Load(reader);
                List<string> dbColumns = table.Columns.Cast<DataColumn>().Select(p => p.ColumnName).ToList();

                foreach (DataRow row in table.Rows)
                {
                    T valueObject = (T)Activator.CreateInstance(typeof(T));

                    foreach (PropertyInfo property in typeof(T).GetProperties())
                    {
                        string columnName = MapPropertyInfoToColumnName(property);
                        if (!dbColumns.Any(col => col.Equals(columnName, StringComparison.InvariantCultureIgnoreCase)))
                            continue;

                        object value = row[columnName];
                        if (property.PropertyType == typeof(bool))
                        {
                            if (value == DBNull.Value) value = "N";

                            SetBoolPropertyValue(valueObject, property, value);
                        }
                        else
                        {
                            object columnValue = row[columnName];
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
            SqlDataReader reader = _dbConnection.Result();

            if (reader != null)
            {
                DataTable table = new DataTable();
                table.Load(reader);
                List<string> dbColumns = table.Columns.Cast<DataColumn>().Select(p => p.ColumnName).ToList();

                foreach (DataRow row in table.Rows)
                {
                    T valueObject = (T)Activator.CreateInstance(typeof(T));

                    foreach (PropertyInfo property in typeof(T).GetProperties())
                    {
                        string columnName = MapPropertyInfoToColumnName(property);
                        if (!dbColumns.Any(col => col.Equals(columnName, StringComparison.InvariantCultureIgnoreCase)))
                            continue;

                        object value = row[columnName];
                        if (property.PropertyType == typeof(bool))
                        {
                            if (value == DBNull.Value) value = "N";

                            SetBoolPropertyValue(valueObject, property, value);
                        }
                        else
                        {
                            object columnValue = row[columnName];
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

        public int GetCount()
        {
            int result = new int();

            string sql = GetSQLGetCount();

            result = _dbConnection.SetCountCommand(sql);

            _dbConnection.Connection().Close();

            return result;
        }

        public List<T> GetCustom(string sql)
        {
            List<T> resultsList = new List<T>();

            _dbConnection.SetCommand(sql);
            SqlDataReader reader = _dbConnection.Result();

            if (reader != null)
            {
                DataTable table = new DataTable();
                table.Load(reader);
                List<string> dbColumns = table.Columns.Cast<DataColumn>().Select(p => p.ColumnName).ToList();

                foreach (DataRow row in table.Rows)
                {
                    var valueObject = (T)Activator.CreateInstance(typeof(T));

                    foreach (PropertyInfo property in typeof(T).GetProperties())
                    {
                        string columnName = MapPropertyInfoToColumnName(property);
                        if (!dbColumns.Any(col => col.Equals(columnName, StringComparison.InvariantCultureIgnoreCase)))
                            continue;

                        object value = row[columnName];
                        if (property.PropertyType == typeof(bool))
                        {
                            if (value == DBNull.Value) value = "N";

                            SetBoolPropertyValue(valueObject, property, value);
                        }
                        else
                        {
                            object columnValue = row[columnName];
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

        private void SetBoolPropertyValue(T valueObject, PropertyInfo property, object value)
        {
            if (value is string)
            {
                if ((string)value == "T") value = "Y";

                bool columnValue = (string)value == "Y" ? true : false;
                property.SetValue(valueObject, columnValue, null);
            }
            else if (value is byte)
            {
                bool columnValue = (byte)value == 1 ? true : false;
                property.SetValue(valueObject, columnValue, null);
            }
            else if (value is int || value is uint)
            {
                bool columnValue = (int)value == 1 ? true : false;
                property.SetValue(valueObject, columnValue, null);
            }
            else
            {
                object columnValue = value; //in this case it is alredy bool
                if (columnValue == DBNull.Value) columnValue = null;
                property.SetValue(valueObject, columnValue, null);
            }
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

                string columnName = MapPropertyInfoToColumnName(property);
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

            bool updateSuccessful = _dbConnection.SetUpdateCommand(sql);
            if (!updateSuccessful)
            {
                throw new Exception("BaseDataManager.Update: Error during the entity update");
            }
        }

        private IList<PropertyInfo> GetPersistenceProperties(object o)
        {
            PropertyInfo[] objectProperties = o.GetType().GetProperties();
            List<PropertyInfo> persistenceProperties = new List<PropertyInfo>();
            foreach (PropertyInfo property in objectProperties)
            {
                bool ignore = property.GetCustomAttributes(typeof(IgnoreAttribute), false).Any();
                if (ignore || property.Name == KeyFields.Id) continue;

                persistenceProperties.Add(property);
            }

            return persistenceProperties;
        }

        private string MapPropertyInfoToColumnName(PropertyInfo propertyInfo)
        {
            bool hasDataAnnotation = propertyInfo.GetCustomAttributes(typeof(ColumnName), false).Any();

            if (hasDataAnnotation)
            {
                ColumnName attribute = (ColumnName)propertyInfo.GetCustomAttributes(typeof(ColumnName), false).First();

                if (attribute != null && attribute.Name != null)
                {
                    return attribute.Name;
                }
                else
                {
                    return MapPropertyNameToColumnName(propertyInfo.Name);
                }
            }
            else
            {
                return MapPropertyNameToColumnName(propertyInfo.Name);
            }
        }
    }
}
