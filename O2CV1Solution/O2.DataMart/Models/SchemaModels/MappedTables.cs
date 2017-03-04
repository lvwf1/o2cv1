using System.Collections.Generic;
using O2.DataMart.Models.SchemaModels;

namespace O2.DataMart.Models.SchemaModels
{
    public class MappedTables
    {

        public List<TableSchemaModel> TablesMapped { get; set; }
        public MappedTables()
        {
            TablesMapped = new List<TableSchemaModel>();
        }
    }
}