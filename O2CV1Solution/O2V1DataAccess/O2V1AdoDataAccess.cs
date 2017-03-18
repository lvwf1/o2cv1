using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using O2V1DataAccess.models;

namespace O2V1DataAccess
{
    public class O2V1AdoDataAccess
    {
        private readonly string _dbConnectionString;
        private SqlConnection sqlConnection;

        public O2V1AdoDataAccess(string connectionString)
        {
            _dbConnectionString = connectionString;
        }

        public AdoQueryFieldsModel ExecuteQueryCommand(string sql)
        {
            sqlConnection = new SqlConnection(_dbConnectionString);
            sqlConnection.Open();
            var metaDataList = new List<AdoQueryFieldsModel>();


            var cmd = new SqlCommand(sql, sqlConnection);

            var queryCollection = new AdoQueryFieldsModel();
            var firstRow = false;

            using (var reader = cmd.ExecuteReader())
            {
                var hasRows = reader.HasRows;
                while (reader.Read())
                {
                    AdoQueryRow queryRow = new AdoQueryRow();
                    for (var i = 0; i < reader.FieldCount; i++)
                    {
                        var queryField = new AdoQueryField();
                        var columnName = reader.GetName(i);
                        var value = reader[i];
                        var dotNetType = reader.GetFieldType(i);
                        //var sqlType = reader.GetDataTypeName(i);
                        //var specificType = reader.GetProviderSpecificFieldType(i);
                        queryField.Name = columnName;
                        queryField.Value = value.ToString();
                        if (dotNetType != null) queryField.Type = dotNetType.ToString();
                        queryRow.QueryRowFields.Add(queryField);

                    }
                    queryCollection.QueryRows.Add(queryRow);
                    if (firstRow == false)
                    {
                        queryCollection.ColumnHeaders =
                            queryRow.QueryRowFields.Select(x => x.Name).Distinct().ToList();
                        firstRow = true;
                    }
                }
            }

            sqlConnection.Close();
            return queryCollection;
        }
    }
}