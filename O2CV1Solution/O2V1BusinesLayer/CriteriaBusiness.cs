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

        public Int64 CreateNextCriteriaForQuery(QueryDto queryDto, CriteriaDto criteriaDto )
        {

            var criteriaRepository = new CriteriaRepository(_dbConnectionString);

            if (criteriaRepository.DoesQueryExist(queryDto.QueryName))
            {
                var currentCountCriteria = criteriaRepository.GetCountOfCriteriaForQuery(queryDto.QueryName);
                criteriaDto.Sequence = currentCountCriteria += 1;
                criteriaRepository.AddCriteriaToQuery(queryDto, criteriaDto);
            }
            else
            {
                criteriaRepository.AddQueryAndFirstCriteriaToQuery(queryDto, criteriaDto);
            }

            return 0;
        }
    }

}
