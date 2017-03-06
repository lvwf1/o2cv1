using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using CodeEngine.Framework.QueryBuilder.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using O2V1BusinesLayer;
using O2V1BusinesLayer.QueryModels.QueryBuilderModels;

namespace O2V1BusinessLayerTests
{


    [TestClass]
    public class JoinTableQueryTests
    {
        private SqlConnection _sqlConnection;
        [TestInitialize]
        public void TestInitialize()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["O2DataMart"].ConnectionString;
            _sqlConnection = new SqlConnection(connectionString);
        }

        [TestMethod]
        public void InnerJoinTwoTablesGetAllColumnsFromOneTable()
        {
            var queryBuilderParms = new QueryBuilderParms
            {
                PrimaryTable = "Mortgages",
                JoinConditionsList = new List<JoinCondition>
                {
                    new JoinCondition
                    {
                        JoinCompareType = Comparison.Equals,
                        JoinRightTable = "MortgateTypes",
                        JoinOnLeftColumn = "MortgageType",
                        JoinOnRightColumn = "MortgageType",
                        TypeOfJoin = JoinType.InnerJoin

                    }
                }
            };

            var queryBuilderConvertModelToSql = new QueryBuilderConvertModelToSql();
            var sqlFromQueryBuilder = queryBuilderConvertModelToSql.ConvertSimpleTableQuery(queryBuilderParms);
            Assert.IsTrue(sqlFromQueryBuilder.Contains("SELECT Mortgages.* FROM Mortgages INNER JOIN MortgateTypes ON Mortgages.MortgageType = MortgateTypes.MortgageType "));
            Assert.IsTrue(ExecuteQuery(sqlFromQueryBuilder));
        }

        [TestMethod]
        public void InnerJoinTwoTablesGetSpecificColumnsFromOneTable()
        {
            var queryBuilderParms = new QueryBuilderParms
            {
                PrimaryTable = "Mortgages",
                JoinConditionsList = new List<JoinCondition>
                {
                    new JoinCondition
                    {
                        JoinCompareType = Comparison.Equals,
                        JoinRightTable = "MortgateTypes",
                        JoinOnLeftColumn = "MortgageType",
                        JoinOnRightColumn = "MortgageType",
                        TypeOfJoin = JoinType.InnerJoin

                    }
                },
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
            Assert.AreEqual("SELECT  Mortgages.MortgageId,  Mortgages.LenderName  FROM Mortgages INNER JOIN MortgateTypes ON Mortgages.MortgageType = MortgateTypes.MortgageType", sqlFromQueryBuilder.Trim());
            Assert.IsTrue(ExecuteQuery(sqlFromQueryBuilder));
        }

        //[TestMethod]
        //[TestCategory("Where clause")]
        //public void SingleTableTwoWhereClauses()
        //{
        //    var queryBuilderParms = new QueryBuilderParms
        //    {
        //        PrimaryTable = "Mortgages",
        //        WhereConditionsList = new List<WhereConditions>
        //        {
        //            new WhereConditions
        //            {
        //                WhereLeftColumn = "LenderName",
        //                WhereLeftTable = "Mortgages",
        //                WhereOperator = Comparison.Equals,
        //                WhereLiteral = "WELLS FARGO BK NA",
        //                SubClauses = new List<WhereSubConditions>()

        //            },
        //                                new WhereConditions
        //            {
        //                WhereLeftColumn = "LoanType",
        //                WhereLeftTable = "Mortgages",
        //                WhereOperator = Comparison.Equals,
        //                WhereLiteral = "SE"

        //            }
        //        }

        //    };
        //    var queryBuilderConvertModelToSql = new QueryBuilderConvertModelToSql();
        //    var sqlFromQueryBuilder = queryBuilderConvertModelToSql.ConvertSimpleTableQuery(queryBuilderParms);
        //    Assert.IsTrue(sqlFromQueryBuilder.Contains(@"SELECT Mortgages.* FROM Mortgages  WHERE  ((Mortgages.LenderName = 'WELLS FARGO BK NA') AND (Mortgages.LoanType = 'SE'))"));
        //    Assert.IsTrue(ExecuteQuery(sqlFromQueryBuilder));
        //}

        //[TestMethod]
        //[TestCategory("Where clause")]
        //public void SingleTableOneWhereClause()
        //{
        //    var queryBuilderParms = new QueryBuilderParms
        //    {
        //        PrimaryTable = "Mortgages",
        //        WhereConditionsList = new List<WhereConditions>
        //        {
        //            new WhereConditions
        //            {
        //                WhereLeftColumn = "LenderName",
        //                WhereLeftTable = "Mortgages",
        //                WhereOperator = Comparison.Equals,
        //                WhereLiteral = "TD BK NA",
        //                SubClauses = new List<WhereSubConditions>()

        //            },

        //        }

        //    };
        //    var queryBuilderConvertModelToSql = new QueryBuilderConvertModelToSql();
        //    var sqlFromQueryBuilder = queryBuilderConvertModelToSql.ConvertSimpleTableQuery(queryBuilderParms);
        //    Assert.IsTrue(sqlFromQueryBuilder.Contains(@"SELECT Mortgages.* FROM Mortgages  WHERE  (Mortgages.LenderName = 'TD BK NA')"));
        //}

        //[TestMethod]
        //public void SingleTableOneWhereWithSubClauseUsingOrCondition()
        //{
        //    var queryBuilderParms = new QueryBuilderParms
        //    {
        //        PrimaryTable = "Mortgages",
        //        WhereConditionsList = new List<WhereConditions>
        //        {
        //            new WhereConditions
        //            {
        //                WhereLeftColumn = "LenderName",
        //                WhereLeftTable = "Mortgages",
        //                WhereOperator = Comparison.Equals,
        //                WhereRightColumn = "TD BK NA",
        //                SubClauses = new List<WhereSubConditions>
        //                {
        //                    new WhereSubConditions
        //                    {
        //                        Connector = LogicOperator.Or,
        //                        WhereLeftColumn = "LoanType",
        //                        WhereLeftTable = "Mortgages",
        //                        CompareOperator = Comparison.Equals,
        //                        WhereRightColumn = "SE"
        //                    }
        //                }
        //            }
        //        } 

        //    };
        //    var queryBuilderConvertModelToSql = new QueryBuilderConvertModelToSql();
        //    var sqlFromQueryBuilder = queryBuilderConvertModelToSql.ConvertSimpleTableQuery(queryBuilderParms);
        //    Assert.AreEqual(@"SELECT Mortgages.* FROM Mortgages  WHERE  (LenderName = 'TD BK NA' OR LenderName = 'SE')  ", sqlFromQueryBuilder);
        //    Assert.IsTrue(ExecuteQuery(sqlFromQueryBuilder));

        //}

        //[TestMethod]
        //public void SingleTableOneWhereClauseOrderByOneValue()
        //{
        //    var queryBuilderParms = new QueryBuilderParms
        //    {
        //        PrimaryTable = "Mortgages",
        //        WhereConditionsList = new List<WhereConditions>
        //        {
        //            new WhereConditions
        //            {
        //                WhereLeftColumn = "LenderName",
        //                WhereLeftTable = "Mortgages",
        //                WhereOperator = Comparison.Equals,
        //                WhereLiteral = "TD BK NA",
        //                SubClauses = new List<WhereSubConditions>()

        //            }
        //        },
        //        ColumnSortAscDesc = new List<QueryBuilderOrderByClause>
        //        {
        //            new QueryBuilderOrderByClause
        //            {
        //                ColumnName = "LoanAmountRange",
        //                ColumnOrderbyDirection = Sorting.Descending
        //            }

        //        }

        //    };
        //    var queryBuilderConvertModelToSql = new QueryBuilderConvertModelToSql();
        //    var sqlFromQueryBuilder = queryBuilderConvertModelToSql.ConvertSimpleTableQuery(queryBuilderParms);
        //    Assert.AreEqual(@"SELECT Mortgages.* FROM Mortgages  WHERE  (Mortgages.LenderName = 'TD BK NA')   ORDER BY LoanAmountRange DESC ", sqlFromQueryBuilder);
        //}

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
