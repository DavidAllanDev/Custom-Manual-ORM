using Custom.Manual.ORM.Base.Interfaces;

namespace Custom.Manual.ORM.Domain.DomainClasses
{
    public class DemoClass : IIdentifiable<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
