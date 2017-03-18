using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using O2V1DataAccess.models;

namespace O2V1DataAccess
{
    public class O2V1AdoDataAccess
    {
        private readonly string _dbConnectionString;
        private SqlConnection _sqlConnection;

        public O2V1AdoDataAccess(string connectionString)
        {
            _dbConnectionString = connectionString;
        }

        public long GetCountOfAllRows(string sql)
        {
            try
            {

            

                var sqlToexecute = ReplaceSelectWith("select count(*) as countRows  ", sql.ToLowerInvariant());

                _sqlConnection = new SqlConnection(_dbConnectionString);
                _sqlConnection.Open();
                var metaDataList = new List<AdoQueryFieldsModel>();
                var cmd = new SqlCommand(sqlToexecute, _sqlConnection);
                var countRows = cmd.ExecuteScalar();

                _sqlConnection.Close();
                return Convert.ToInt64(countRows);
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public AdoQueryFieldsModel ExecuteQueryCommand(string sql)
        {
            var queryCollection = new AdoQueryFieldsModel {TotalRowsCount = GetCountOfAllRows(sql)};


            var sqlToexecute = ReplaceSelectWith("select top 10 * ", sql.ToLowerInvariant());

            _sqlConnection = new SqlConnection(_dbConnectionString);
            _sqlConnection.Open();
            var metaDataList = new List<AdoQueryFieldsModel>();


            var cmd = new SqlCommand(sqlToexecute, _sqlConnection);

          
            var firstRow = false;

            using (var reader = cmd.ExecuteReader())
            {
                var hasRows = reader.HasRows;
                while (reader.Read())
                {
                    var queryRow = new AdoQueryRow();
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

            _sqlConnection.Close();
            return queryCollection;
        }

        private string ReplaceSelectWith(string newValue, string sql)
        {
            var startOfFrom = sql.ToLower().IndexOf(" from ", StringComparison.Ordinal);

            var startOfSelect = sql.ToLower().IndexOf(" select ", StringComparison.Ordinal);

            return newValue + sql.Substring(startOfFrom);
        }
    }
}