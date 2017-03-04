using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace O2.DataMart.Models.EFModel
{
    [Table("FeedDetails", Schema = "mgt")]
    public partial class FeedDetail
    {
        [Key]
        public Guid FeedId { get; set; }

        public string FeedFile { get; set; }

        public DateTime Date { get; set; }
    }
}
