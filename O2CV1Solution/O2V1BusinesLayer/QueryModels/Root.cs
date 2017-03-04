using System.Collections.Generic;

namespace O2V1BusinesLayer.QueryModels
{
    public class Root
    {
        public Root()
        {
            conditions = new List<Condition>(); 
        }
        public string linkType { get; set; }
        public bool enabled { get; set; }
        public List<Condition> conditions { get; set; }
    }
}