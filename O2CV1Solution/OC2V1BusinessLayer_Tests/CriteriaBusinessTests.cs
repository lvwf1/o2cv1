using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using CodeEngine.Framework.QueryBuilder.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using O2V1BusinesLayer;
using O2V1BusinesLayer.QueryModels.QueryBuilderModels;

namespace OC2V1BusinessLayer_Tests
{


    [TestClass]
    public class CriteriaBusinessTests
    {
        private SqlConnection _sqlConnection;
        private string _sqlConnectionString;
        [TestInitialize]
        public void TestInitialize()
        {
            var _sqlConnectionString = ConfigurationManager.ConnectionStrings["O2DataMart"].ConnectionString;
            _sqlConnection = new SqlConnection(_sqlConnectionString);
        }

        [TestMethod]
        public void TestSimpleOneTableFromOneCriteriFromDaBase()
        {
            var criteriaBusiness = new CriteriaBusiness(_sqlConnectionString);

            var sqlFromQueryBuilder =  criteriaBusiness.BuildSqlFromQuery("12");

            Assert.IsTrue(sqlFromQueryBuilder.Trim().Contains(@"SELECT Mortgages.* FROM Mortgages  WHERE  (dbo.Mortgages.LoanType = 'TR')"));
            Assert.IsTrue(ExecuteQuery(sqlFromQueryBuilder));

        }
        private bool ExecuteQuery(string query)
        {
            using (var con = _sqlConnection)
            {
                using (var cmd = new SqlCommand())
                {
                    SqlDataReader reader;
                    var topQuery = query.Replace("SELECT ", "Select Top 100 ");
                    cmd.CommandText = topQuery;
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;

                    con.Open();

                    reader = cmd.ExecuteReader();

                    return reader.HasRows;
                }
            }
        }
    }
}
