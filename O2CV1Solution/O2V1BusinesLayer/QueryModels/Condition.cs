using System.Collections.Generic;

namespace O2V1BusinesLayer.QueryModels
{
    public class Condition
    {
        public Condition()
        {
            expressions = new List<Expression>();
        }
        public bool justAdded { get; set; }
        public string typeName { get; set; }
        public bool enabled { get; set; }
        public string operatorID { get; set; }
        public List<Expression> expressions { get; set; }
    }
}