using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace O2.DataMart.Models.SchemaModels
{
    public class TableSchemaModel
    {
        public TableSchemaModel()
        {
            metaData = new List<TableMetaData>();
        }
        public List<TableMetaData> metaData { get; set; }
        public string TableName { get; set; }
    }
}
