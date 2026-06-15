using Microsoft.VisualStudio.TestTools.UnitTesting;
using Custom.Manual.ORM.Domain.DomainMapping;

namespace Custom.Manual.ORM.Test
{
    [TestClass]
    public class DemoMapTests
    {
        [TestMethod]
        public void DemoMap_ReturnsExpectedTableAndMappings()
        {
            // Arrange
            var map = new DemoMap();

            // Act
            var tableName = map.EntityTableName();
            var mappings = map.EntityMapper();

            // Assert
            Assert.AreEqual("DemoMapSQLTableName", tableName);
            Assert.IsTrue(mappings.ContainsKey("Id") || mappings.ContainsKey("id"));
            Assert.IsTrue(mappings.ContainsValue("DemoMapSQLTableName_intId"));
            Assert.IsTrue(mappings.ContainsValue("DemoMapSQLTableName_strName"));
        }
    }
}
