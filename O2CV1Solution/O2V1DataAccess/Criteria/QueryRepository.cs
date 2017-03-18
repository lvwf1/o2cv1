using System;
using System.Collections.Generic;
using System.Linq;
using O2CV1EntityDtos;
using O2V1DataAccess.Criteria;
using static System.DateTime;

namespace O2V1DataAccess.O2CV1Query
{
    public class QueryRepository
    {
        private readonly CriteriaDataContext _context;
        private string _conn;

        public QueryRepository(string connection)
        {
            _conn = connection;
            _context = new CriteriaDataContext();
        }

        public O2CV1QueryDto GetQueryById(string queryId)
        {
            using (var dc = new CriteriaDataContext())
            {
                var cq = dc.O2CVQueries.FirstOrDefault(x => x.Id == Convert.ToInt64(queryId));
                if (cq == null)
                    return null;

                var qdto = new O2CV1QueryDto
                {
                    CreatedBy = cq.CreatedBy,
                    CreatedDate = cq.CreatedDate,
                    Deleted = cq.Deleted,
                    DeletedBy = cq.DeletedBy,
                    DeletedDate = cq.DeletedDate,
                    Description = cq.Description,
                    QueryId = cq.Id.ToString(),
                    QueryName = cq.QueryName,
                    QuerySql = cq.QuerySql
                };


                return qdto;
            }
        }

        public O2CV1QueryDto GetQueryByName(string queryName)
        {
            using (var dc = new CriteriaDataContext())
            {
                var cq = dc.O2CVQueries.FirstOrDefault(x => x.QueryName == queryName);
                if (cq == null)
                    return null;

                var qdto = new O2CV1QueryDto
                {
                    CreatedBy = cq.CreatedBy,
                    CreatedDate = cq.CreatedDate,
                    Deleted = cq.Deleted,
                    DeletedBy = cq.DeletedBy,
                    DeletedDate = cq.DeletedDate,
                    Description = cq.Description,
                    QueryId = cq.Id.ToString(),
                    QueryName = cq.QueryName,
                    QuerySql = cq.QuerySql
                };


                return qdto;
            }
        }

        public static List<O2CV1QueryDto> GetQueries()
        {
            var queryDtoList = new List<O2CV1QueryDto>();
            using (var dc = new CriteriaDataContext())
            {
                var result = dc.O2CVQueries;
                foreach (var cq in dc.O2CVQueries)
                {
                    var qdto = new O2CV1QueryDto();
                    qdto.CreatedBy = cq.CreatedBy;
                    qdto.CreatedDate = cq.CreatedDate;
                    qdto.Deleted = cq.Deleted;
                    qdto.DeletedBy = cq.DeletedBy;
                    qdto.DeletedDate = cq.DeletedDate;
                    qdto.Description = cq.Description;
                    qdto.QueryId = cq.Id.ToString();
                    qdto.QueryName = cq.QueryName;
                    qdto.QuerySql = cq.QuerySql;

                    queryDtoList.Add(qdto);
                }


                return queryDtoList;
            }
        }

        public void CreateQuery(O2CV1QueryDto queryDto)
        {
            using (var dc = new CriteriaDataContext())
            {
                var q = new O2CVQuery
                {
                    QueryName = queryDto.QueryName,
                    QuerySql = queryDto.QuerySql,
                    CreatedBy = queryDto.CreatedBy,
                    Deleted = queryDto.Deleted,
                    Description = queryDto.Description,
                    CreatedDate = Now
                };
                dc.O2CVQueries.InsertOnSubmit(q);
                dc.SubmitChanges();
            }
        }

        public O2CV1QueryDto SaveQuery(O2CV1QueryDto queryDto)
        {
            using (var dc = new CriteriaDataContext())
            {
                var cq = dc.O2CVQueries.FirstOrDefault(x => x.Id == Convert.ToInt64(queryDto.QueryId));
                if (cq == null)
                {
                    throw new Exception(
                        $"Could not find query to save - query id {queryDto.QueryId} query name {queryDto.QueryName}");
                }
                cq.QueryName = queryDto.QueryName;
                cq.QuerySql = queryDto.QuerySql;
                cq.Deleted = queryDto.Deleted;
                cq.Description = queryDto.Description;
                cq.ModifiedBy = queryDto.ModifiedBy;
                cq.ModifiedDate = Now;
                dc.SubmitChanges();

                return queryDto;

            }
        }

        public void SaveQuery(string queryId, string sql)
        {
            using (var dc = new CriteriaDataContext())
            {
                var cq = dc.O2CVQueries.FirstOrDefault(x => x.Id == Convert.ToInt64(queryId));
                if (cq == null)
                {
                    throw new Exception(
                        $"Could not find query to save - query id {queryId} ");
                }
                 cq.QuerySql = sql;
                 cq.ModifiedDate = Now;
                dc.SubmitChanges();
 
            }
        }
    }
}