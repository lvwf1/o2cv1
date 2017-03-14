using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data.SqlClient;
using O2.DataMart.Models.SchemaModels;
using O2CV1EntityDtos;
using static System.DateTime;
using O2V1DataAccess.Criteria;

namespace O2V1DataAccess.O2CV1Query
{
    public class QueryRepository
    {
        private string _conn;
        private readonly CriteriaDataContext _context;

        public QueryRepository(string connection)
        {
            _conn = connection;
            _context = new CriteriaDataContext();
        }

        
        public static List<O2CV1QueryDto> GetQueries()
        {

            var queryDtoList = new List<O2CV1QueryDto>();
            using (var dc = new CriteriaDataContext())
            {
                var result = dc.O2CVQueries;
                foreach (O2CVQuery cq in dc.O2CVQueries) {
                    O2CV1QueryDto qdto = new O2CV1QueryDto();
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

      
    }
}