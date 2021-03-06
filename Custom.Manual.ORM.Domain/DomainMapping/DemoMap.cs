﻿using System;
using System.Collections.Generic;
using Custom.Manual.ORM.Base.Data.Fields;
using Custom.Manual.ORM.Base.Interfaces;

namespace Custom.Manual.ORM.Domain.DomainMapping
{
    public class DemoMap : IEntityMap
    {
        public Dictionary<string, string> EntityMapper() => new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                {KeyFields.Id, "DemoMapSQLTableName_intId"},
                {"Name" ,"DemoMapSQLTableName_strName"}
            };

        public string EntityTableName() => "DemoMapSQLTableName";
    }
}
