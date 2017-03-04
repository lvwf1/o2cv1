using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace O2V1BusinesLayer.QueryModels
{
    public class Options
    {
        public Options()
        {
            options = new List<string>();
        }
        private List<string> options { get; set; }
    }
}
