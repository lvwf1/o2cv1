using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Net.Configuration;
using Newtonsoft.Json;
using O2.DataMart.Models.SchemaModels;
using O2V1BusinesLayer.QueryModels;
using O2V1DataAccess;

namespace O2V1BusinesLayer
{
    public class ConvertModelToJson
    {
        private readonly string connection;
        private readonly SchemaRepository schemaRepository;
        public ConvertModelToJson()
        {
            connection = ConfigurationManager.ConnectionStrings["O2DataMart"].ConnectionString;
            schemaRepository = new SchemaRepository(connection);
        }

        public string ConvertOptionsToJson()
        {
            try
            {
                Options options = new Options();
                return JsonConvert.SerializeObject(options);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public string ConvertSimpleTableQuery(string tableName)
        {
            try
            {


                TableSchemaModel columnData = schemaRepository.GetSchemaTableAndColumns(tableName);
                var simpleQuery = new ComposedQuerySimple
                {
                    root = new Root
                    {
                        enabled = true,
                        linkType = "All"
                    },
                    modelId = Guid.NewGuid().ToString(),
                    modelName = null
                };

                var columns = new List<Column>();

                foreach (var item in columnData.metaData)
                {
                    var column = new Column
                    {
                        caption = $"{tableName} {item.Name}",
                        sortIndex = -1,
                        sorting = "None",
                        expr = new Expr
                        {
                            typeName = "ENTATTR",
                            id = $"{tableName}.{item.Name.ToString()}",

                        },
                        ParmObjects = new List<object>()
                    };
                    columns.Add(column);
                }
                simpleQuery.columns = columns;

                return JsonConvert.SerializeObject(simpleQuery);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}