using System;
using System.Collections.Generic;
using System.Text;
using Custom.Manual.ORM.Base.Interfaces;

namespace Custom.Manual.ORM.Domain.DomainClasses
{
    public class DemoClass : IIdentifiable<int>
    {
        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
