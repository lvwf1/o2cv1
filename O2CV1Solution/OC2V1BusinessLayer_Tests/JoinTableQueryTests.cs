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
    public class JoinTableQueryTests
    {
        private SqlConnection _sqlConnection;
        private string _sqlConnectionString;
        [TestInitialize]
        public void TestInitialize()
        {
            _sqlConnectionString = ConfigurationManager.ConnectionStrings["O2DataMart"].ConnectionString;
            _sqlConnection = new SqlConnection(_sqlConnectionString);
        }

        [TestMethod]
        public void TestJoinMortgateToPersonTableFromOneCriteriFromDaBase()
        {
            var criteriaBusiness = new O2CV1Business(_sqlConnectionString);

            var sqlFromQueryBuilder = criteriaBusiness.BuildSqlFromQuery("19");

            Assert.IsTrue(sqlFromQueryBuilder.Trim().Contains(@"SELECT Mortgages.* FROM Mortgages  WHERE  ((dbo.Mortgages.LenderName LIKE '%lend%') AND (dbo.Mortgages.LoanAmountRange = '$175K - $209K'))"));
            Assert.IsTrue(ExecuteQuery(sqlFromQueryBuilder));

        }

        [TestMethod]
        public void InnerJoinPropertyAndPersonsAndMorgageOredWheres()
        {
            var queryBuilderParms = new QueryBuilderParms
            {
                PrimaryTable = "Mortgages",
                JoinConditionsList = new List<JoinCondition>
                {
                    new JoinCondition
                    {
                        JoinLeftTable = "BackBone",
                        JoinCompareType = Comparison.Equals,
                        JoinRightTable = "BackBone",
                        JoinOnLeftColumn = "MortgageId",
                        JoinOnRightColumn = "MortgageId",
                        TypeOfJoin = JoinType.InnerJoin
                        

                    },
                     new JoinCondition
                    {
                        JoinLeftTable = "BackBone",
                        JoinCompareType = Comparison.Equals,
                        JoinRightTable = "Property",
                        JoinOnLeftColumn = "PropertyId",
                        JoinOnRightColumn = "PropertyId",
                        TypeOfJoin = JoinType.InnerJoin

                    },
                       new JoinCondition
                    {
                         JoinLeftTable = "BackBone",
                        JoinCompareType = Comparison.Equals,
                        JoinRightTable = "Persons",
                        JoinOnLeftColumn = "PersonId",
                        JoinOnRightColumn = "PersonId",
                        TypeOfJoin = JoinType.InnerJoin
                    }


                },
                IncludeColumns = new List<QueryBuilderColumnsToInclude>
                {
                    new QueryBuilderColumnsToInclude
                    {
                        TableName = "Mortgages",
                        ColumnName = "MortgageId"


                    },
                    new QueryBuilderColumnsToInclude
                    {
                         TableName = "Mortgages",
                        ColumnName = "LenderName"
                    },
                     new QueryBuilderColumnsToInclude
                    {
                        TableName = "Mortgages",
                        ColumnName = "MortgageId"


                    },
                    new QueryBuilderColumnsToInclude
                    {
                         TableName = "Property",
                        ColumnName = "State"
                    },
                     new QueryBuilderColumnsToInclude
                    {
                         TableName = "Persons",
                        ColumnName = "LastName"
                    }
                },
                WhereConditionsList = new List<WhereCondition>
                {
                    new WhereCondition
                    {
                        WhereLeftColumn = "State",
                        WhereLeftTable = "Persons",
                        WhereOperator = Comparison.Equals,
                        WhereRightColumn = "WA",
                        WhereAndOr = LogicOperator.Or
                        

                    },
                    new WhereCondition
                    {
                        WhereLeftColumn = "State",
                        WhereLeftTable = "Property",
                        WhereOperator = Comparison.Like,
                        WhereRightColumn = "%a%",
                        WhereAndOr = LogicOperator.Or
                    },
                      new WhereCondition
                    {
                        WhereLeftColumn = "LoanToValueRange",
                        WhereLeftTable = "Mortgages",
                        WhereOperator = Comparison.Equals,
                        WhereRightColumn = "100% - 119.99%",
                    }

                }
            };

            var queryBuilderConvertModelToSql = new QueryBuilderConvertModelToSql();
            var sqlFromQueryBuilder = queryBuilderConvertModelToSql.ConvertSimpleTableQuery(queryBuilderParms);
            Assert.IsTrue(sqlFromQueryBuilder.Trim().Contains(@"SELECT  Mortgages.MortgageId,  Mortgages.LenderName,  Mortgages.MortgageId,  Property.State,  Persons.LastName  FROM Mortgages INNER JOIN BackBone ON BackBone.MortgageId = BackBone.MortgageId INNER JOIN Property ON BackBone.PropertyId = Property.PropertyId INNER JOIN Persons ON BackBone.PersonId = Persons.PersonId  WHERE  ((Persons.State = 'WA') OR (Property.State LIKE '%a%') OR (Mortgages.LoanToValueRange = '100% - 119.99%'))"));
            Assert.IsTrue(ExecuteQuery(sqlFromQueryBuilder));

        }

        [TestMethod]
        public void InnerJoinPropertyAndPersonsAndMorgageAndedWheres()
        {
            var queryBuilderParms = new QueryBuilderParms
            {
                PrimaryTable = "Mortgages",
                JoinConditionsList = new List<JoinCondition>
                {
                    new JoinCondition
                    {
                        JoinLeftTable = "BackBone",
                        JoinCompareType = Comparison.Equals,
                        JoinRightTable = "BackBone",
                        JoinOnLeftColumn = "MortgageId",
                        JoinOnRightColumn = "MortgageId",
                        TypeOfJoin = JoinType.InnerJoin

                    },
                     new JoinCondition
                    {
                        JoinLeftTable = "BackBone",
                        JoinCompareType = Comparison.Equals,
                        JoinRightTable = "Property",
                        JoinOnLeftColumn = "PropertyId",
                        JoinOnRightColumn = "PropertyId",
                        TypeOfJoin = JoinType.InnerJoin

                    },
                       new JoinCondition
                    {
                         JoinLeftTable = "BackBone",
                        JoinCompareType = Comparison.Equals,
                        JoinRightTable = "Persons",
                        JoinOnLeftColumn = "PersonId",
                        JoinOnRightColumn = "PersonId",
                        TypeOfJoin = JoinType.InnerJoin
                    }


                },
                IncludeColumns = new List<QueryBuilderColumnsToInclude>
                {
                    new QueryBuilderColumnsToInclude
                    {
                        TableName = "Mortgages",
                        ColumnName = "MortgageId"
                         

                    },
                    new QueryBuilderColumnsToInclude
                    {
                         TableName = "Mortgages",
                        ColumnName = "LenderName"
                    },
                     new QueryBuilderColumnsToInclude
                    {
                        TableName = "Mortgages",
                        ColumnName = "MortgageId"


                    },
                    new QueryBuilderColumnsToInclude
                    {
                         TableName = "Property",
                        ColumnName = "State"
                    },
                     new QueryBuilderColumnsToInclude
                    {
                         TableName = "Persons",
                        ColumnName = "LastName"
                    }
                },
                WhereConditionsList = new List<WhereCondition>
                {
                    new WhereCondition
                    {
                       WhereLeftColumn = "State",
                        WhereLeftTable = "Persons",
                        WhereOperator = Comparison.Equals,
                        WhereRightColumn = "WA",
         
                    },
                    new WhereCondition
                    {
                        WhereLeftColumn = "State",
                        WhereLeftTable = "Property",
                        WhereOperator = Comparison.Like,
                        WhereRightColumn = "%a%",
                    },
                      new WhereCondition
                    {
                        WhereLeftColumn = "LoanToValueRange",
                        WhereLeftTable = "Mortgages",
                        WhereOperator = Comparison.Equals,
                        WhereRightColumn = "100% - 119.99%",
                    }

                }
            };

            var queryBuilderConvertModelToSql = new QueryBuilderConvertModelToSql();
            var sqlFromQueryBuilder = queryBuilderConvertModelToSql.ConvertSimpleTableQuery(queryBuilderParms);
            Assert.IsTrue(sqlFromQueryBuilder.Contains(@"SELECT  Mortgages.MortgageId,  Mortgages.LenderName,  Mortgages.MortgageId,  Property.State,  Persons.LastName  FROM Mortgages INNER JOIN BackBone ON BackBone.MortgageId = BackBone.MortgageId INNER JOIN Property ON BackBone.PropertyId = Property.PropertyId INNER JOIN Persons ON BackBone.PersonId = Persons.PersonId  WHERE  ((Persons.State = 'WA') AND (Property.State LIKE '%a%') AND (Mortgages.LoanToValueRange = '100% - 119.99%'))"));
            Assert.IsTrue(ExecuteQuery(sqlFromQueryBuilder));

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

        [TestMethod]
        [TestCategory("Where clause")]
        public void JoinTwoTablesWithOneWhereClauses()
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
                WhereConditionsList = new List<WhereCondition>
                {
                    new WhereCondition
                    {
                        WhereLeftColumn = "LenderName",
                        WhereLeftTable = "Mortgages",
                        WhereOperator = Comparison.Equals,
                        WhereLiteral = "WELLS FARGO BK NA",
                        SubClauses = new List<WhereSubConditions>()

                    } 
                }

            };
            var queryBuilderConvertModelToSql = new QueryBuilderConvertModelToSql();
            var sqlFromQueryBuilder = queryBuilderConvertModelToSql.ConvertSimpleTableQuery(queryBuilderParms);
            Assert.AreEqual(@"SELECT Mortgages.* FROM Mortgages INNER JOIN MortgateTypes ON Mortgages.MortgageType = MortgateTypes.MortgageType  WHERE  (Mortgages.LenderName = 'WELLS FARGO BK NA')", sqlFromQueryBuilder.Trim());
            Assert.IsTrue(ExecuteQuery(sqlFromQueryBuilder));
        }

        [TestMethod]
        [TestCategory("Where clause")]
        public void JoinTwoTablesWithWhereClauses()
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
                WhereConditionsList = new List<WhereCondition>
                {
                    new WhereCondition
                    {
                        WhereLeftColumn = "LenderName",
                        WhereLeftTable = "Mortgages",
                        WhereOperator = Comparison.Equals,
                        WhereLiteral = "WELLS FARGO BK NA",
                        SubClauses = new List<WhereSubConditions>()

                    },
                                        new WhereCondition
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
            Assert.AreEqual(@"SELECT Mortgages.* FROM Mortgages INNER JOIN MortgateTypes ON Mortgages.MortgageType = MortgateTypes.MortgageType  WHERE  ((Mortgages.LenderName = 'WELLS FARGO BK NA') AND (Mortgages.LoanType = 'SE'))", sqlFromQueryBuilder.Trim());
            Assert.IsTrue(ExecuteQuery(sqlFromQueryBuilder));
        }

        [TestMethod]
        public void JoinTwoTablesUseWhereWithSubClauseUsingOrCondition()
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
                WhereConditionsList = new List<WhereCondition>
                {
                    new WhereCondition
                    {
                        WhereLeftColumn = "LenderName",
                        WhereLeftTable = "Mortgages",
                        WhereOperator = Comparison.Equals,
                        WhereRightColumn = "TD BK NA",
                        SubClauses = new List<WhereSubConditions>
                        {
                            new WhereSubConditions
                            {
                                Connector = LogicOperator.Or,
                                WhereLeftColumn = "LenderName",
                                WhereLeftTable = "Mortgages",
                                CompareOperator = Comparison.Equals,
                                WhereRightColumn = "FROST BK"
                            }
                        }
                    }
                }

            };
            var queryBuilderConvertModelToSql = new QueryBuilderConvertModelToSql();
            var sqlFromQueryBuilder = queryBuilderConvertModelToSql.ConvertSimpleTableQuery(queryBuilderParms);
            Assert.AreEqual(@"SELECT Mortgages.* FROM Mortgages INNER JOIN MortgateTypes ON Mortgages.MortgageType = MortgateTypes.MortgageType  WHERE  (Mortgages.LenderName = '.TD BK NA' OR Mortgages.LenderName = 'FROST BK')", sqlFromQueryBuilder.Trim());
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
