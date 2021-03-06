﻿using System;
using System.Collections.Generic;
using System.Linq;
using O2CV1EntityDtos;
using static System.DateTime;

namespace O2V1DataAccess.Criteria
{
    public class CriteriaRepository
    {
        private readonly CriteriaDataContext _context;
        private string _conn;

        public CriteriaRepository(string connection)
        {
            _conn = connection;
            _context = new CriteriaDataContext();
        }

        public bool DoesQueryExist(string queryName)
        {
            using (var dc = new CriteriaDataContext())
            {
                var query = dc.O2CVQueries.Where(q => q.QueryName == queryName);
                return query.ToList().Count > 0;
            }
        }

        public string GetIdOfQuery(string queryName)
        {
            using (var dc = new CriteriaDataContext())
            {
                var result =
                    dc.O2CVQueries.Where(x => x.QueryName == queryName).Select(x => x.Id.ToString()).FirstOrDefault();
                return result ?? "0";
            }
        }

        public int GetCountOfCriteriaForQuery(long queryId)
        {
            using (var dc = new CriteriaDataContext())
            {
                var queryCriteria = dc.O2CVQueryCriterias.Where(q => q.CriteriaParentQueryId == queryId);
                return queryCriteria.ToList().Count;
            }
        }

        public List<CriteriaDto> GetCriteriaForQuery(long queryId)
        {
            var criteriaDtoList = new List<CriteriaDto>();
            using (var dc = new CriteriaDataContext())
            {
                var result = (from c in dc.O2CVQueryCriterias
                    where c.CriteriaParentQueryId == queryId
                    select new
                    {
                        O2CVQueryCriteria = c
                    }).OrderBy(cr => cr.O2CVQueryCriteria.Sequence);

                criteriaDtoList.AddRange(result.Select(criteria => new CriteriaDto
                {
                    CompareOperator = criteria.O2CVQueryCriteria.CompareOperator,
                    CompareValue = criteria.O2CVQueryCriteria.CompareValue,
                    Createdby = criteria.O2CVQueryCriteria.CreatedBy,
                    Description = criteria.O2CVQueryCriteria.Description,
                    Disabled = criteria.O2CVQueryCriteria.Disabled ?? false,
                    Name = criteria.O2CVQueryCriteria.Name,
                    Sequence = criteria.O2CVQueryCriteria.Sequence,
                    TableColumn = criteria.O2CVQueryCriteria.TableColumn,
                    TableName = criteria.O2CVQueryCriteria.TableName
                }));

                return criteriaDtoList;
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

        public void AddQueryAndFirstCriteriaToQuery(O2CV1QueryDto queryDto, CriteriaDto criteriaDto)
        {
            using (var dc = new CriteriaDataContext())
            {
                var query = new O2CVQuery
                {
                    QueryName = queryDto.QueryName,
                    QuerySql = queryDto.QuerySql,
                    CreatedBy = queryDto.CreatedBy,
                    Deleted = queryDto.Deleted,
                    Description = queryDto.Description,
                    CreatedDate = Now
                };

                var criteria = new O2CVQueryCriteria
                {
                    Sequence = 1,
                    TableName = criteriaDto.TableName,
                    TableColumn = criteriaDto.TableColumn,
                    Name = criteriaDto.Description,
                    CompareOperator = criteriaDto.CompareOperator,
                    CompareValue = criteriaDto.CompareValue,
                    Description = criteriaDto.Description,
                    DisableBy = criteriaDto.Disabled ? criteriaDto.Createdby : null,
                    CreatedBy = criteriaDto.Createdby,
                    Disabled = criteriaDto.Disabled,
                    CreatedDate = Now
                };

                if (criteria.Disabled == true)
                    criteria.DisabledDate = Now;
                query.O2CVQueryCriterias.Add(criteria);
                dc.O2CVQueries.InsertOnSubmit(query);
                dc.SubmitChanges();
            }
        }

        public void AddCriteriaToQuery(O2CV1QueryDto queryDto, CriteriaDto criteriaDto)
        {
            using (var dc = new CriteriaDataContext())
            {
                var query = dc.O2CVQueries.Single(q => q.QueryName == queryDto.QueryName);

                var criteria = new O2CVQueryCriteria
                {
                    Sequence = criteriaDto.Sequence,
                    TableName = criteriaDto.TableName,
                    TableColumn = criteriaDto.TableColumn,
                    CompareOperator = criteriaDto.CompareOperator,
                    CompareValue = criteriaDto.CompareValue,
                    Description = criteriaDto.Description,
                    Name =
                        $"{criteriaDto.TableName} {criteriaDto.TableColumn} {criteriaDto.CompareOperator} {criteriaDto.CompareValue}",
                    DisableBy = criteriaDto.Disabled ? criteriaDto.Createdby : null,
                    CreatedBy = criteriaDto.Createdby,
                    Disabled = criteriaDto.Disabled,
                    CreatedDate = Now
                };

                if (criteria.Disabled == true)
                    criteria.DisabledDate = Now;
                query.O2CVQueryCriterias.Add(criteria);
                dc.SubmitChanges();
            }
        }

        public string GetSqlForQuery(string queryId)
        {

            long queryIdLong = Convert.ToInt64(queryId);

            using (var dc = new CriteriaDataContext())
            {
                var query = dc.O2CVQueries.Single(q => q.Id == queryIdLong);

                return query.QuerySql;
            }
        }
    }
}