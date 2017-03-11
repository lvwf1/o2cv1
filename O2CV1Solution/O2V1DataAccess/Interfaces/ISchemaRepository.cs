using System.Collections.Generic;
using O2.DataMart.Models.SchemaModels;

namespace O2V1DataAccess.Interfaces
{
    public interface ISchemaRepository
    {
        TableSchemaModel GetSchemaTableAndColumns(string tableName);
        List<string> GetSchemaTables();

        List<string> GetSchemaBackBoneRelatedTables();

    }
}