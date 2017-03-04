using System.Collections.Generic;

namespace Korzh.EasyQuery.Mvc.Demo.EF.Models.QueryModels
{
    public class Root
    {
        public string linkType { get; set; }
        public bool enabled { get; set; }
        public List<Condition> conditions { get; set; }
    }
}