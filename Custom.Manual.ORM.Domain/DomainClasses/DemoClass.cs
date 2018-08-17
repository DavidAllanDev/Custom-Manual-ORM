using Custom.Manual.ORM.Base.Interfaces;
using Custom.Manual.ORM.Base.Data.Fields;

namespace Custom.Manual.ORM.Domain.DomainClasses
{
    public class DemoClass : IIdentifiable<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// On this example we have a data Column Name that differ from the property of the class
        /// On the next versions of this ORM it will be getting it from this data annotation
        /// </summary>
        [ColumnName(Name = "Enable")]
        public bool Active { get; set; }
    }
}
