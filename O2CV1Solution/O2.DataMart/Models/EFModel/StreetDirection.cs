namespace O2.DataMart.Models.EFModel
{
    using System.ComponentModel.DataAnnotations;

    public partial class StreetDirection
    {
        [Key]
        [StringLength(80)]
        public string Direction { get; set; }
    }
}
