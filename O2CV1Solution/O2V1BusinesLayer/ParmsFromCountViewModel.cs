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
}