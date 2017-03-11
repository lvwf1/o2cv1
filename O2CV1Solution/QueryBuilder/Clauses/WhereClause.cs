using System.Collections.Generic;
using CodeEngine.Framework.QueryBuilder.Enums;

//
// Class: WhereClause
// Copyright 2006 by Ewout Stortenbeker
// Email: 4ewout@gmail.com
//
// This class is part of the CodeEngine Framework.
// You can download the framework DLL at http://www.code-engine.com/
//
namespace CodeEngine.Framework.QueryBuilder.Clauses
{
    /// <summary>
    /// Represents a WHERE clause on 1 database column, containing 1 or more comparisons on 
    /// that column, chained together by logic operators: eg (UserID=1 or UserID=2 or UserID>100)
    /// This can be achieved by doing this:
    /// WhereClause myWhereClause = new WhereClause("UserID", Comparison.Equals, 1);
    /// myWhereClause.AddClause(LogicOperator.Or, Comparison.Equals, 2);
    /// myWhereClause.AddClause(LogicOperator.Or, Comparison.GreaterThan, 100);
    /// </summary>
    public struct WhereClause
    {
        internal struct SubClause
        {
            public LogicOperator LogicOperator;
            public Comparison ComparisonOperator;
            public object Value;
            public SubClause(LogicOperator logic, Comparison compareOperator, object compareValue)
            {
                LogicOperator = logic;
                ComparisonOperator = compareOperator;
                Value = compareValue;
            }
        }
        internal List<SubClause> SubClauses;	// Array of SubClause

        /// <summary>
        /// Gets/sets the name of the database column this WHERE clause should operate on
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// Gets/sets the comparison method
        /// </summary>
        public Comparison ComparisonOperator { get; set; }

        public LogicOperator WhereConnectorOperator { get; set; }

        /// <summary>
        /// Gets/sets the value that was set for comparison
        /// </summary>
        public object Value { get; set; }

        public WhereClause(string field, Comparison firstCompareOperator, object firstCompareValue, LogicOperator whereConnectorOperator)
        {
            FieldName = field;
            ComparisonOperator = firstCompareOperator;
            Value = firstCompareValue;
            WhereConnectorOperator = whereConnectorOperator;
            SubClauses = new List<SubClause>();
        }
        public void AddClause(LogicOperator logic, Comparison compareOperator, object compareValue)
        {
            SubClause newSubClause = new SubClause(logic, compareOperator, compareValue);
            SubClauses.Add(newSubClause);
        }
    }

}
