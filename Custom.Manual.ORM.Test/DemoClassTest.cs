using Microsoft.VisualStudio.TestTools.UnitTesting;
using Custom.Manual.ORM.Domain.DomainClasses;

namespace Custom.Manual.ORM.Test
{
    [TestClass]
    public class DemoClassTest
    {
        protected DemoClass demo;

        public DemoClassTest()
        {
            demo = new DemoClass();
        }


        [TestMethod]
        public void canInstanceDemoClassTest()
        {
            //Arrange
            int id = 1;
            string name = "nameof";
            bool enable = true;

            //Act
            demo = new DemoClass() { Id = id, Name = name, Active = enable };

            //Assert
            Assert.IsNotNull(demo);
        }
    }
}
