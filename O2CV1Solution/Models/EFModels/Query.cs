using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace O2V1Web.Models.EFModels
{
    [Table("Queries", Schema = "mgt")]
    public partial class Query
    {
        [Key]
        public Guid QueryId { get; set; }

        public Guid UserId { get; set; }

        [StringLength(30)]
        public string Tag { get; set; }

        public string Text { get; set; }
    }
}
