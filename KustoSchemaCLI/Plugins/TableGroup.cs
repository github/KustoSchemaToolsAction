﻿using KustoSchemaTools.Model;

namespace KustoSchemaCLI.Plugins
{
    public class TableGroup
    {
        public List<string> AppliesTo { get; set; } = new List<string>();
        public Table Table { get; set; }
    }

}
