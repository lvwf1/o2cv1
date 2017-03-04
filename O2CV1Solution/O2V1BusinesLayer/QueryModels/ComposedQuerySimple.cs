using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace O2V1BusinesLayer.QueryModels
{
    class ComposedQuerySimple
    {
        public ComposedQuerySimple()
        {
            justsorted = new List<string>();
        }
        public Root root { get; set; }
        public List<Column> columns { get; set; }
        public List<string> justsorted { get; set; } 
        public string modelId { get; set; }
        public string modelName { set; get; }

    }
}
