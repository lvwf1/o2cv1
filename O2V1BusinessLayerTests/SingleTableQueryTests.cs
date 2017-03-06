using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using CodeEngine.Framework.QueryBuilder.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using O2DataAccess;
using O2V1BusinesLayer;
using O2V1BusinesLayer.QueryModels;
using O2V1BusinesLayer.QueryModels.QueryBuilderModels;
using O2V1DataAccess;

namespace O2V1BusinessLayerTests
{
  
    
    [TestClass]
    public class SingleTableQueryTests
    {
        private SqlConnection _sqlConnection;
        [TestInitialize]
        public void TestInitialize()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["O2DataMart"].ConnectionString;
            _sqlConnection = new SqlConnection(connectionString);
        }

        [TestMethod]
        public void SingleTableSimpleTestAllColumns()
        {
            var queryBuilderParms = new QueryBuilderParms();
            queryBuilderParms.PrimaryTable = "Mortgages";
            var queryBuilderConvertModelToSql = new QueryBuilderConvertModelToSql();
            var sqlFromQueryBuilder = queryBuilderConvertModelToSql.ConvertSimpleTableQuery(queryBuilderParms);
            Assert.IsTrue(sqlFromQueryBuilder.Contains("SELECT Mortgages.* FROM Mortgages"));
        }

        [TestMethod]
        public void SingleTableSimpleTestSpecificColumns()
        {
            var queryBuilderParms = new QueryBuilderParms
            {
                PrimaryTable = "Mortgages",
                IncludeColumns = new List<QueryBuilderColumnsToInclude>
                {
                    new QueryBuilderColumnsToInclude
                    {
                        ColumnName = "MortgageId"
                        
                    },
                    new QueryBuilderColumnsToInclude
                    {
                        ColumnName = "LenderName"
                    }
                }
                
            };
            var queryBuilderConvertModelToSql = new QueryBuilderConvertModelToSql();
            var sqlFromQueryBuilder = queryBuilderConvertModelToSql.ConvertSimpleTableQuery(queryBuilderParms);
            Assert.AreEqual("SELECT  Mortgages.MortgageId,  Mortgages.LenderName  FROM Mortgages ", sqlFromQueryBuilder);
        }
        [TestMethod]
        [TestCategory("Where clause")]
        public void SingleTableTwoWhereClauses()
        {
            var queryBuilderParms = new QueryBuilderParms
            {
                PrimaryTable = "Mortgages",
                WhereConditions = new List<WhereConditions>
                {
                    new WhereConditions
                    {
                        WhereLeftColumn = "LenderName",
                        WhereLeftTable = "Mortgages",
                        WhereOperator = Comparison.Equals,
                        WhereLiteral = "WELLS FARGO BK NA"

                    },
                                        new WhereConditions
                    {
                        WhereLeftColumn = "LoanType",
                        WhereLeftTable = "Mortgages",
                        WhereOperator = Comparison.Equals,
                        WhereLiteral = "SE"

                    }
                }

            };
            var queryBuilderConvertModelToSql = new QueryBuilderConvertModelToSql();
            var sqlFromQueryBuilder = queryBuilderConvertModelToSql.ConvertSimpleTableQuery(queryBuilderParms);
            Assert.IsTrue(sqlFromQueryBuilder.Contains(@"SELECT Mortgages.* FROM Mortgages  WHERE  ((Mortgages.LenderName = 'WELLS FARGO BK NA') AND (Mortgages.LoanType = 'SE'))"));
            Assert.IsTrue(ExecuteQuery(sqlFromQueryBuilder));
        }

        [TestMethod]
        [TestCategory("Where clause")]
        public void SingleTableOneWhereClause()
        {
            var queryBuilderParms = new QueryBuilderParms
            {
                PrimaryTable = "Mortgages",
                WhereConditions = new List<WhereConditions>
                {
                    new WhereConditions
                    {
                        WhereLeftColumn = "LenderName",
                        WhereLeftTable = "Mortgages",
                        WhereOperator = Comparison.Equals,
                        WhereLiteral = "TD BK NA"
                        
                    }
                }
                
            };
            var queryBuilderConvertModelToSql = new QueryBuilderConvertModelToSql();
            var sqlFromQueryBuilder = queryBuilderConvertModelToSql.ConvertSimpleTableQuery(queryBuilderParms);
            Assert.IsTrue(sqlFromQueryBuilder.Contains(@"SELECT Mortgages.* FROM Mortgages  WHERE  (Mortgages.LenderName = 'TD BK NA')"));
        }
        [TestMethod]
        public void SingleTableOneWhereClauseOrderByOneValue()
        {
            var queryBuilderParms = new QueryBuilderParms
            {
                PrimaryTable = "Mortgages",
                WhereConditions = new List<WhereConditions>
                {
                    new WhereConditions
                    {
                        WhereLeftColumn = "LenderName",
                        WhereLeftTable = "Mortgages",
                        WhereOperator = Comparison.Equals,
                        WhereLiteral = "TD BK NA"

                    }
                },
                ColumnSortAscDesc = new List<QueryBuilderOrderByClause>
                {
                    new QueryBuilderOrderByClause
                    {
                        ColumnName = "LoanAmountRange",
                        ColumnOrderbyDirection = Sorting.Descending
                    }

                }
 
            };
            var queryBuilderConvertModelToSql = new QueryBuilderConvertModelToSql();
            var sqlFromQueryBuilder = queryBuilderConvertModelToSql.ConvertSimpleTableQuery(queryBuilderParms);
            Assert.AreEqual(@"SELECT Mortgages.* FROM Mortgages  WHERE  (Mortgages.LenderName = 'TD BK NA')   ORDER BY LoanAmountRange DESC ", sqlFromQueryBuilder);
        }

        private bool ExecuteQuery(string query)
        {
            using (var con = _sqlConnection)
            {
                using (var cmd = new SqlCommand())
                {
                    SqlDataReader reader;

                    cmd.CommandText = query;
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
