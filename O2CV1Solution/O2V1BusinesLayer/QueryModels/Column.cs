using System.Collections.Generic;
using Newtonsoft.Json;

namespace O2V1BusinesLayer.QueryModels
{
    public class Column
    {
        public Column()
        {
            ParmObjects = new List<object>();
        }

        public string caption { get; set; }
        public string sorting { get; set; }
        public int sortIndex { get; set; }
        public Expr expr { get; set; }

        [JsonProperty(PropertyName = "params")]
        public List<object> ParmObjects { get; set; }
    
    }
}