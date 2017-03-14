using System.Collections.Generic;
using O2.Common.Interfaces;
using O2V1BusinesLayer.QueryModels.QueryBuilderModels;

namespace O2V1BusinesLayer
{
    public class ParmsFromCountViewModel
    {
        public QueryBuilderParms GetQueryParmFromCountView(string selectedTable)
        {
            var queryBuilderParms = new QueryBuilderParms {PrimaryTable = selectedTable};
            return queryBuilderParms;
        }
    }

    public class ParmsFromQueryTable
    {
        public ParmsFromQueryTable()
        {
            queryBuilderParms = new List<QueryBuilderParms>();
        }

        List<QueryBuilderParms> queryBuilderParms = new List<QueryBuilderParms>();
    }
}