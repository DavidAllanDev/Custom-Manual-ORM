using Microsoft.VisualStudio.TestTools.UnitTesting;
using Custom.Manual.ORM.Data.Connection;
using Custom.Manual.ORM.Data.Repositories.Managers;
using Custom.Manual.ORM.Domain.DomainClasses;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Custom.Manual.ORM.IntegrationTest
{
    [TestClass]
    public class DemoClassIntegrationTests
    {
        private class FakeConnection : IDBConnection
        {
            public Custom.Manual.ORM.Data.Setting.IConnectionSetting Settings => null;

            public SqlConnection Connection() => null;

            public void SetCommand(string sql) { }

            public object SetInsertCommand(string sql) => 1;

            public int SetCountCommand(string sql) => 0;

            public bool SetUpdateCommand(string sql) => true;

            public SqlDataReader Result() => null;
        }

        [TestMethod]
        public void DemoClassManager_Add_ReturnsId()
        {
            // Arrange
            var conn = new FakeConnection();
            var manager = new DemoClassManager(conn);
            var demo = new DemoClass { Id = 0, Name = "name", Active = true };

            // Act
            var id = manager.Add(demo);

            // Assert
            Assert.IsNotNull(id);
        }
    }
}
