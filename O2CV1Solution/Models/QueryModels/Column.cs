using System.Collections.Generic;

namespace Korzh.EasyQuery.Mvc.Demo.EF.Models.QueryModels
{
    public class Column
    {
        public string caption { get; set; }
        public string sorting { get; set; }
        public int sortIndex { get; set; }
        public Expr expr { get; set; }
        public List<object> @params { get; set; }
        public string blockId { get; set; }
    }
}