﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace O2V1BusinesLayer.QueryModels
{
    public class QueryDto
    {
        public string QuerySql { get; set; }

        public string QueryName { get; set; }

        public Int64 Id { get; set; }

        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string Description { get; set; }


        public bool? Deleted { get; set; }

        public DateTime? DeletedDate { get; set; }
        public string DeletedBy { get; set; }


    }
}
