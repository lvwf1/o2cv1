using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeEngine.Framework.QueryBuilder.Enums;
using O2.Common.Interfaces;
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
            SchemaRepository schemaRepository = new SchemaRepository(connection);

            var criteriaRepository = new CriteriaRepository(_dbConnectionString);
            var criteriaForQuery =
                criteriaRepository.GetCriteriaForQuery(Convert.ToInt64(queryId)).OrderBy(x => x.TableName).ToList();

            if (!criteriaForQuery.Any()) return string.Empty;
            {
                var queryBuilderParms = new QueryBuilderParms();

                var criteriaDto = criteriaForQuery.First();

                queryBuilderParms.PrimaryTable = criteriaForQuery.First().TableName;
                if (!IsNullOrEmpty(criteriaDto.CompareOperator))
                {
                    var colDataType =
                        schemaRepository.GetSchemaTableColumns(criteriaDto.TableColumn).MetaData.Select(x => x.DbType).ToString();

                    queryBuilderParms.WhereConditionsList = new List<WhereCondition>
                    {
                        new WhereCondition
                        {
                            WhereLeftColumn = criteriaDto.TableColumn.Replace($"{criteriaDto.TableName}.",""),
                            WhereLeftTable = $"dbo.{criteriaDto.TableName}",
                            WhereOperator = GetComparison(criteriaDto.CompareOperator), // Comparison.Equals,
                            WhereRightColumn = FixQuotes(colDataType, criteriaDto.CompareValue)

                        }

                    };
                }
                var queryBuilderConvertModelToSql = new QueryBuilderConvertModelToSql();
                return queryBuilderConvertModelToSql.ConvertSimpleTableQuery(queryBuilderParms);
            }
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
