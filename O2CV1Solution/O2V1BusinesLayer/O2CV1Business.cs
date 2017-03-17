using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using CodeEngine.Framework.QueryBuilder.Enums;
using O2CV1EntityDtos;
using O2V1BusinesLayer.QueryModels.QueryBuilderModels;
using O2V1DataAccess;
using O2V1DataAccess.Criteria;
using O2V1DataAccess.O2CV1Query;
using static System.String;

namespace O2V1BusinesLayer
{
    public class O2CV1Business
    {
        private readonly string _dbConnectionString;

        public O2CV1Business(string connectionString)
        {
            _dbConnectionString = connectionString;
        }

        public string CreateNextCriteriaForQuery(O2CV1QueryDto queryDto, CriteriaDto criteriaDto)
        {
            var criteriaRepository = new CriteriaRepository(_dbConnectionString);

            if (criteriaRepository.DoesQueryExist(queryDto.QueryName))
            {
                var currentCountCriteria =
                    criteriaRepository.GetCountOfCriteriaForQuery(Convert.ToInt64(queryDto.QueryId));
                criteriaDto.Sequence = currentCountCriteria += 1;
                criteriaRepository.AddCriteriaToQuery(queryDto, criteriaDto);
            }
            else
            {
                criteriaRepository.AddQueryAndFirstCriteriaToQuery(queryDto, criteriaDto);
            }

            return criteriaRepository.GetIdOfQuery(queryDto.QueryName);
        }

        public void SaveQuery(O2CV1QueryDto queryDto)
        {
            var queryRepository = new QueryRepository(_dbConnectionString);
            queryRepository.SaveQuery(queryDto);
        }

        public string BuildSqlFromQuery(string queryId)
        {
            var connection = ConfigurationManager.ConnectionStrings["O2DataMart"].ConnectionString;
            var schemaRepository = new SchemaRepository(connection);

            var criteriaRepository = new CriteriaRepository(_dbConnectionString);
            var criteriaForQuery =
                criteriaRepository.GetCriteriaForQuery(Convert.ToInt64(queryId))
                    .OrderBy(x => x.TableName)
                    .ThenBy(x => x.Sequence)
                    .ToList();
            var primaryTable = criteriaForQuery[0].TableName;
            var queryBuilderParms = new QueryBuilderParms();
            queryBuilderParms.PrimaryTable = primaryTable;

            var groupByTable = criteriaForQuery.GroupBy(b => b.TableName);

            var groupedTables = groupByTable as IGrouping<string, CriteriaDto>[] ?? groupByTable.ToArray();
            if (groupedTables.Count() > 1)
                BuildJoinsForQuery(queryBuilderParms, criteriaForQuery, schemaRepository, groupedTables);


            var criteriaSeqLastProcessed = 1;

            if (!criteriaForQuery.Any()) return Empty;
            {
                var currentTableName = criteriaForQuery[0].TableName;

                QueryBuilderParmsForThisCritera(criteriaForQuery[0], schemaRepository, queryBuilderParms);

                criteriaForQuery.RemoveAll(x => x.Sequence == criteriaSeqLastProcessed);

                foreach (var criteriaItem in criteriaForQuery)
                {
                    AddWhereClauseToCurrentQuery(criteriaItem, queryBuilderParms, schemaRepository);
                    criteriaSeqLastProcessed = criteriaItem.Sequence;
                }


                var queryBuilderConvertModelToSql = new QueryBuilderConvertModelToSql();
                return queryBuilderConvertModelToSql.ConvertSimpleTableQuery(queryBuilderParms);
            }
        }

        private void BuildJoinsForQuery(QueryBuilderParms queryBuilderParms, List<CriteriaDto> criteriaForQuery,
            SchemaRepository schemaRepository, IEnumerable<IGrouping<string, CriteriaDto>> groupedTables)
        {
            var criteriaArray = criteriaForQuery.ToArray();
            int currentCriteria = 0;
            int totalCriter = criteriaForQuery.Count;


            if (criteriaArray[currentCriteria].TableName.ToLower().Contains("mortgage"))
                queryBuilderParms.JoinConditionsList.Add(
                    new JoinCondition
                    {
                        JoinLeftTable = "BackBone",
                        JoinCompareType = Comparison.Equals,
                        JoinRightTable = "BackBone",
                        JoinOnLeftColumn = "MortgageId",
                        JoinOnRightColumn = "MortgageId",
                        TypeOfJoin = JoinType.InnerJoin
                    });


            if (criteriaArray[currentCriteria].TableName.ToLower().Contains("persons"))
                queryBuilderParms.JoinConditionsList.Add(
                    new JoinCondition
                    {
                        JoinLeftTable = "BackBone",
                        JoinCompareType = Comparison.Equals,
                        JoinRightTable = "BackBone",
                        JoinOnLeftColumn = "PersonId",
                        JoinOnRightColumn = "PersonId",
                        TypeOfJoin = JoinType.InnerJoin
                    });

            if (criteriaArray[currentCriteria].TableName.ToLower().Contains("persons"))
                queryBuilderParms.JoinConditionsList.Add(
                    new JoinCondition
                    {
                        JoinLeftTable = "BackBone",
                        JoinCompareType = Comparison.Equals,
                        JoinRightTable = "BackBone",
                        JoinOnLeftColumn = "PropertyId",
                        JoinOnRightColumn = "PropertyId",
                        TypeOfJoin = JoinType.InnerJoin
                    });


            currentCriteria += 1;

            for (var i = currentCriteria; i <= totalCriter - 1; i++)
            {
                if (criteriaArray[i].TableName.ToLower().Contains("persons"))
                    queryBuilderParms.JoinConditionsList.Add(
                        new JoinCondition
                        {
                            JoinLeftTable = "BackBone",
                            JoinCompareType = Comparison.Equals,
                            JoinRightTable = criteriaArray[i].TableName,
                            JoinOnLeftColumn = "PersonId",
                            JoinOnRightColumn = "PersonId",
                            TypeOfJoin = JoinType.InnerJoin
                        });

                if (criteriaArray[i].TableName.ToLower().Contains("property"))
                    queryBuilderParms.JoinConditionsList.Add(
                        new JoinCondition
                        {
                            JoinLeftTable = "BackBone",
                            JoinCompareType = Comparison.Equals,
                            JoinRightTable = criteriaArray[i].TableName,
                            JoinOnLeftColumn = "PropertyId",
                            JoinOnRightColumn = "PropertyId",
                            TypeOfJoin = JoinType.InnerJoin
                        });
             
        
        }

    }

        private void AddWhereClauseToCurrentQuery(CriteriaDto criteriaDto, QueryBuilderParms queryBuilderParms,
            SchemaRepository schemaRepository)
        {
            var colDataType =
                schemaRepository.GetSchemaTableColumns(criteriaDto.TableColumn)
                    .MetaData.Select(x => x.DbType)
                    .ToString();

            queryBuilderParms.WhereConditionsList.Add(new WhereCondition
            {
                WhereLeftColumn = criteriaDto.TableColumn.Replace($"{criteriaDto.TableName}.", ""),
                WhereLeftTable = $"dbo.{criteriaDto.TableName}",
                WhereOperator = GetComparison(criteriaDto.CompareOperator),
                WhereRightColumn = FixQuotes(colDataType, criteriaDto.CompareValue)
            });
        }

        private QueryBuilderParms QueryBuilderParmsForThisCritera(CriteriaDto criteriaDto,
            SchemaRepository schemaRepository, QueryBuilderParms queryBuilderParms)
        {
            queryBuilderParms.PrimaryTable = criteriaDto.TableName;
            if (IsNullOrEmpty(criteriaDto.CompareOperator)) return queryBuilderParms;
            var colDataType =
                schemaRepository.GetSchemaTableColumns(criteriaDto.TableColumn)
                    .MetaData.Select(x => x.DbType)
                    .ToString();

            queryBuilderParms.WhereConditionsList = new List<WhereCondition>
            {
                new WhereCondition
                {
                    WhereLeftColumn = criteriaDto.TableColumn.Replace($"{criteriaDto.TableName}.", ""),
                    WhereLeftTable = $"dbo.{criteriaDto.TableName}",
                    WhereOperator = GetComparison(criteriaDto.CompareOperator), // Comparison.Equals,
                    WhereRightColumn = FixQuotes(colDataType, criteriaDto.CompareValue)
                }
            };
            return queryBuilderParms;
        }

        private string FixQuotes(string colDataType, string criteriaDtoCompareValue)
        {
            switch (colDataType.ToLower())
            {
                case "nvarchar":
                case "date":
                case "varchar":
                case "uniqueidentifier":
                    return $"{criteriaDtoCompareValue}";
                    break;
                case "int":
                case "bigint":
                    return $"{criteriaDtoCompareValue}";
                    break;
                case "float":
                    return $"{criteriaDtoCompareValue}";
                default:
                    return $"{criteriaDtoCompareValue}";
                    break;
            }
        }

        private Comparison GetComparison(string criteriaDtoCompareOperator)
        {
            switch (criteriaDtoCompareOperator)
            {
                case " = ":
                    return Comparison.Equals;
                    break;
                case " < ":
                    return Comparison.LessThan;
                    break;
                case " > ":
                    return Comparison.GreaterThan;
                    break;
                case " != ":
                    return Comparison.NotEquals;
                    break;
                case " in ":
                    return Comparison.In;
                    break;
                case " not like ":
                    return Comparison.NotLike;
                    break;
                case " like ":
                    return Comparison.Like;
                    break;
                default:
                    return Comparison.Equals;
                    break;
            }
        }
    }
}