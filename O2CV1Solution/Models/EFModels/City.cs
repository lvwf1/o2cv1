namespace O2V1Web.Models.EFModels
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class City
    {
        [Key]
        [Column("City", Order = 0)]
        [StringLength(80)]
        public string City1 { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(80)]
        public string County { get; set; }
    }
}
