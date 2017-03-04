using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace O2V1Web.Models
{
    [Table("Queries")]
    public class Queries
    {

        public Guid QueryId { get; set; }
        public String UserId { get; set; }
        
        public String Tag { get; set; }
        
        public String Text { get; set; }

    }
}