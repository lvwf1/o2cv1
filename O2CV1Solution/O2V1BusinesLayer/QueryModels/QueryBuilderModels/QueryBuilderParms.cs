using System.Collections.Generic;
using CodeEngine.Framework.QueryBuilder.Enums;

namespace O2V1BusinesLayer.QueryModels.QueryBuilderModels
{
    public class QueryBuilderParms
    {
        public List<QueryBuilderSortByAndDirection> ColumnSortAscDesc;
        public List<QueryBuilderColumnsToInclude> IncludeColumns;
        public List<JoinCondition> JoinConditions;
        public List<WhereConditions> WhereConditions;


        public QueryBuilderParms()
        {
            ColumnSortAscDesc = new List<QueryBuilderSortByAndDirection>();
            WhereConditions = new List<WhereConditions>();
            JoinConditions = new List<JoinCondition>();
            IncludeColumns = new List<QueryBuilderColumnsToInclude>();
        }

        public string PrimaryTable { get; set; }
        public int MaxRowsToReturn { get; set; }
    }

    public struct QueryBuilderSortByAndDirection
    {
        public string ColumnName { get; set; }
        public string ColumnOrderbyDirection { get; set; }
    }

    public struct QueryBuilderColumnsToInclude
    {
        public string TableNme { get; set; }
        public string ColumnName { get; set; }
    }

    public struct JoinCondition
    {
        public string JoinLeftTable { get; set; }
        public string JoinOnLeftColumn { get; set; }
        public string JoinRightTable { get; set; }
        public string JoinOnRightColumn { get; set; }
        public JoinType TypeOfJoin { get; set; }
        public Comparison JoinCompareType { get; set; }
    }

    public struct WhereConditions
    {
        public string WhereLeftTable { get; set; }
        public Comparison WhereOperator { get; set; }
        public string WhereRightTable { get; set; }
        public string WhereRightColumn { get; set; }
        public string WhereLeftColumn { get; set; }
        public string WhereLiteral { get; set; }
    }
}