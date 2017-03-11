using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using CodeEngine.Framework.QueryBuilder;
using CodeEngine.Framework.QueryBuilder.Clauses;
using CodeEngine.Framework.QueryBuilder.Enums;
using O2V1BusinesLayer.QueryModels.QueryBuilderModels;
using O2V1DataAccess;

namespace O2V1BusinesLayer
{
    public class QueryBuilderConvertModelToSql
    {
        private readonly string connection;
        private readonly SchemaRepository schemaRepository;

        public QueryBuilderConvertModelToSql()
        {
            connection = ConfigurationManager.ConnectionStrings["O2DataMart"].ConnectionString;
            schemaRepository = new SchemaRepository(connection);
        }

        public string ConvertSimpleTableQuery(QueryBuilderParms queryBuilderParms)
        {
            try
            {
                var query = new SelectQueryBuilder();
                query.SelectFromTable(queryBuilderParms.PrimaryTable);
                if (queryBuilderParms.IncludeColumns.Count < 1)
                {
                    query.SelectAllColumns();
                }
                else
                {
                    if (queryBuilderParms.IncludeColumns.Count > 0)
                        IncludeColumns(queryBuilderParms.IncludeColumns, query, queryBuilderParms.PrimaryTable);
                }

                if (queryBuilderParms.MaxRowsToReturn > 0)
                    query.TopRecords = queryBuilderParms.MaxRowsToReturn;

                if (queryBuilderParms.JoinConditionsList.Count > 0)
                    AddJoinsClauses(queryBuilderParms.JoinConditionsList, query, queryBuilderParms.PrimaryTable);

                if (queryBuilderParms.WhereConditionsList.Count > 0)
                    AddWhereClauses(queryBuilderParms.WhereConditionsList, query);

                if (queryBuilderParms.ColumnSortAscDesc.Count > 0)
                    AddOrderByClause(queryBuilderParms.ColumnSortAscDesc, query);

                string statement = $"{query.BuildQuery()}";

                var finalStatement = PostProcessStatement(statement, queryBuilderParms);
                return finalStatement;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static string PostProcessStatement(string statement, QueryBuilderParms queryBuilderParms)
        {
            return statement;
        }

        private void IncludeColumns(List<QueryBuilderColumnsToInclude> includeColumns, SelectQueryBuilder query,
            string primaryTable)
        {
            var sb = new StringBuilder();
            foreach (var column in includeColumns)
            {
                if (column.TableName == null)
                    sb.Append(" " + primaryTable + "." + column.ColumnName + ", ");
                else
                    sb.Append(" " + column.TableName + "." + column.ColumnName + ", ");
                if (sb.Length <= 3) continue;
                query.SelectColumns(sb.ToString().Remove(sb.Length - 2));
            }
        }

        private static void AddOrderByClause(IEnumerable<QueryBuilderOrderByClause> orderBys, SelectQueryBuilder query)
        {
            foreach (var clause in orderBys)
                query.AddOrderBy(clause.ColumnName, clause.ColumnOrderbyDirection);
        }

        private static void AddJoinsClauses(IEnumerable<JoinCondition> joins, SelectQueryBuilder query, string primaryTable)
        {

            foreach (var clause in joins)
            {
                var leftTable = clause.JoinLeftTable ?? primaryTable;
                query.AddJoin(clause.TypeOfJoin,
                    $"{clause.JoinRightTable}", $"{clause.JoinOnRightColumn}",
                    clause.JoinCompareType,
                    $"{leftTable}", $"{clause.JoinOnLeftColumn}");
            }
        }

        private static void AddWhereClauses(IEnumerable<WhereConditions> whereConditions, SelectQueryBuilder query)
        {
       
            foreach (var clause in whereConditions)
            {
                var myWhereClause = new WhereClause($"{clause.WhereLeftTable}.{clause.WhereLeftColumn}", clause.WhereOperator, $"{clause.WhereRightTable}.{clause.WhereRightColumn}", clause.WhereAndOr);

                if (clause.SubClauses?.Count > 0)
                {
                    foreach (var subClause in clause.SubClauses)
                    {
                        myWhereClause.AddClause(subClause.Connector, subClause.CompareOperator, subClause.WhereRightColumn);
                    }

                    query.AddWhere(myWhereClause);

                }
                else
                {
                    query.AddWhere($"{clause.WhereLeftTable}.{clause.WhereLeftColumn}", clause.WhereOperator,
                        clause.WhereLiteral ?? clause.WhereRightColumn, clause.WhereAndOr);
                }

            }
        }
    }
}