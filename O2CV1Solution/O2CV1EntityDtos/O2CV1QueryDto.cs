using System;

namespace O2CV1EntityDtos
{
    public class O2CV1QueryDto
    {
        
        public string QuerySql { get; set; }

        public string QueryName { get; set; }

        public string QueryId { get; set; }

        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string Description { get; set; }


        public bool? Deleted { get; set; }

        public DateTime? DeletedDate { get; set; }
        public string DeletedBy { get; set; }
    }
}
