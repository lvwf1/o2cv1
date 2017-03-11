using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using O2V1DataAccess.SchemaModels;

namespace O2.DataMart.Models.SchemaModels
{
    public class TableSchemaModel
    {
        public TableSchemaModel()
        {
            MetaData = new List<TableMetaData>();
        }
        public List<TableMetaData> MetaData { get; set; }
        public string TableName { get; set; }
    }
}
