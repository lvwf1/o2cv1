namespace O2V1Web.Models.EFModels
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class County
    {
        [Column("County")]
        [StringLength(80)]
        public string County1 { get; set; }

        [Key]
        [StringLength(80)]
        public string State { get; set; }
    }
}
