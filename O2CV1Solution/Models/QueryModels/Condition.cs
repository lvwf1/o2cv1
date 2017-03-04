using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Korzh.EasyQuery.Mvc.Demo.EF.Models.QueryModels
{
    public class Condition
    {
        public bool justAdded { get; set; }
        public string typeName { get; set; }
        public bool enabled { get; set; }
        public string operatorID { get; set; }
        public List<Expression> expressions { get; set; }
        public string blockId { get; set; }
    }
}