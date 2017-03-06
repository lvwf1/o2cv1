using System;
using System.Collections.Generic;
using CodeEngine.Framework.QueryBuilder.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using O2V1BusinesLayer;
using O2V1BusinesLayer.QueryModels;
using O2V1BusinesLayer.QueryModels.QueryBuilderModels;

namespace O2V1BusinessLayerTests
{
    [TestClass]
    public class SingleTableQueryTests
    {
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
    }
}
