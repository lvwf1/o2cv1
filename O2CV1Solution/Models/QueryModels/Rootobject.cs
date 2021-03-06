﻿using System.Collections.Generic;

namespace Korzh.EasyQuery.Mvc.Demo.EF.Models.QueryModels
{
    public class RootObject
    {
        public Root root { get; set; }
        public List<Column> columns { get; set; }
        public List<object> justsorted { get; set; }
        public string modelId { get; set; }
        public object modelName { get; set; }
    }
}