using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using O2.DataMart.Models.SchemaModels;
using O2V1DataAccess.Interfaces;
using O2V1DataAccess.SchemaModels;

namespace O2V1DataAccess
{
    public class SchemaRepository : ISchemaRepository
    {
        private readonly int _colName = 3;
        private readonly int _colType = 7;
        private readonly string _connectionString;
        private readonly O2DataMartLinqToSqlDataContext _context;
        private readonly int _tblName = 2;

        private readonly List<string> _backBoneTables = new List<string>
        {
            "Feee", "Property", "Person", "Mortgage"
        };

        public SchemaRepository(string connectionString)
        {
            _connectionString = connectionString;
            _context = new O2DataMartLinqToSqlDataContext();
        }

        public TableSchemaModel GetSchemaTableColumns(string tableName)
        {
            var tableSchemaModel = new TableSchemaModel();

            using (var connection = new SqlConnection(_connectionString))
            {
                var restrictions = new string[4] {null, null, tableName, null};
                connection.Open();

                var columnList =
                    connection.GetSchema("Columns", restrictions)
                        .AsEnumerable()
                        .ToList();

                foreach (var colinfo in columnList)
                {
                    var columnData = new TableMetaData
                    {
                        Name = colinfo.ItemArray[_colName].ToString(),
                        IsPrimeKey = false,
                        DbType = colinfo[_colType].ToString()
                    };
                    tableSchemaModel.MetaData.Add(columnData);
                }
                return tableSchemaModel;
            }
        }

        public MappedTables LinqToSqlMapping()
        {
            var mappedTables = new MappedTables();

            foreach (var table in _context.Mapping.GetTables())
            {
                var tableSchemaModel = new TableSchemaModel();

                var results = from dm in table.RowType.DataMembers
                    where dm.DbType != null
                    select new {dm.Name, dm.DbType, dm.Type, dm.MappedName, dm.IsPrimaryKey};

                foreach (var colinfo in results)
                {
                    var columnData = new TableMetaData
                    {
                        Name = colinfo.Name,
                        IsPrimeKey = colinfo.IsPrimaryKey,
                        DbType = colinfo.DbType,
                        Type = colinfo.Type
                    };
                    tableSchemaModel.MetaData.Add(columnData);
                }
                tableSchemaModel.TableName = table.TableName;
                mappedTables.TablesMapped.Add(tableSchemaModel);
            }

            return mappedTables;
        }

        public List<string> GetSchemaTables()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var schema = connection.GetSchema("Tables");
                return (from DataRow row in schema.Rows select row[_tblName].ToString()).ToList();
            }
        }

        public TableSchemaModel GetSchemaTableAndColumns(string tableName)
        {
            throw new System.NotImplementedException();
        }

        public List<string> GetSchemaBackBoneRelatedTables()
        {
            var tableNames = new List<string>();

            var backBoneColumns = GetSchemaTableColumns("BackBone");

            if (!(backBoneColumns?.TableName.Length > 0)) return tableNames;
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var schema = connection.GetSchema("Tables");

                // return (from DataRow row in schema.Rows select row[_tblName].ToString()).ToList())
                tableNames.AddRange(from DataRow row in schema.Rows where _backBoneTables.Contains(row[_tblName].ToString().Trim()) select row[_tblName].ToString());
            }
            return tableNames;
        }
    }
}