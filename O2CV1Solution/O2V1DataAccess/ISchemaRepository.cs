using System.Collections.Generic;
using O2.DataMart.Models.SchemaModels;

namespace O2DataAccess
{
    public interface ISchemaRepository
    {
        TableSchemaModel GetSchemaTableAndColumns(string tableName);
        List<string> GetSchemaTables();
    }
}