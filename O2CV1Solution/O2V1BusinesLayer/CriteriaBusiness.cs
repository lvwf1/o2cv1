using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using O2.Common.Interfaces;
using O2CV1EntityDtos;
using O2V1DataAccess.Criteria;

namespace O2V1BusinesLayer
{
    public class CriteriaBusiness
    {
        private readonly string _dbConnectionString;

        public CriteriaBusiness(string connectionString)
        {
            _dbConnectionString = connectionString;
        }

        public string CreateNextCriteriaForQuery(QueryDto queryDto, CriteriaDto criteriaDto )
        {

            var criteriaRepository = new CriteriaRepository(_dbConnectionString);

            if (criteriaRepository.DoesQueryExist(queryDto.QueryName))
            {
                var currentCountCriteria = criteriaRepository.GetCountOfCriteriaForQuery(Convert.ToInt64(queryDto.QueryId));
                criteriaDto.Sequence = currentCountCriteria += 1;
                criteriaRepository.AddCriteriaToQuery(queryDto, criteriaDto);
            }
            else
            {
                criteriaRepository.AddQueryAndFirstCriteriaToQuery(queryDto, criteriaDto);
            }

           return criteriaRepository.GetIdOfQuery(queryDto.QueryName);
        }

        public string BuildSqlFromQuery(string queryId)
        {
            var criteriaRepository = new CriteriaRepository(_dbConnectionString);
            var criteriaForQuery = CriteriaRepository.GetCriteriaForQuery(Convert.ToInt64(queryId));

            var parmsFromCountViewModel = new ParmsFromCountViewModel();
            var queryBuilderParms = parmsFromCountViewModel.GetQueryParmFromCountView(queryId);
            var queryBuilderConvertModelToSql = new QueryBuilderConvertModelToSql();
            var sqlFromQueryBuilder = queryBuilderConvertModelToSql.ConvertSimpleTableQuery(queryBuilderParms);
            return string.Empty;

        }
    }

}
