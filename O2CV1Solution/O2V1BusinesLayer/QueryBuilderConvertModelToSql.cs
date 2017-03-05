using System;
using System.Collections.Generic;
using System.Configuration;
using CodeEngine.Framework.QueryBuilder;
using Newtonsoft.Json;
using O2V1BusinesLayer.QueryModels;
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
                    query.SelectAllColumns();

                if (queryBuilderParms.MaxRowsToReturn > 0)
                    query.TopRecords = queryBuilderParms.MaxRowsToReturn;

                if (queryBuilderParms.JoinConditions.Count > 0)
                    AddJoinsClauses(queryBuilderParms.JoinConditions, query);

                if (queryBuilderParms.WhereConditions.Count > 0)
                    AddWhereClauses(queryBuilderParms.WhereConditions, query);

                string statement = $"Query built by BuildQuery:  {query.BuildQuery()}";

                return statement;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void AddJoinsClauses(List<JoinCondition> joins, SelectQueryBuilder query)
        {
            foreach (var clause in joins)
                query.AddJoin(clause.TypeOfJoin,
                    $"{clause.JoinLeftTable}", $"{clause.JoinOnLeftColumn}",
                    clause.JoinCompareType,
                    $"{clause.JoinRightTable}", $"{clause.JoinOnRightColumn}");
        }

        private static void AddWhereClauses(List<WhereConditions> whereConditions, SelectQueryBuilder query)
        {
            foreach (var clause in whereConditions)
                query.AddWhere($"{clause.WhereLeftTable}.{clause.WhereLeftColumn}", clause.WhereOperator,
                    clause.WhereLiteral ?? clause.WhereRightColumn);
        }
    }
}