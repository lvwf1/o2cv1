namespace O2V1Web.Models.EFModels
{
    using System.ComponentModel.DataAnnotations;

    public partial class StreetDirection
    {
        [Key]
        [StringLength(80)]
        public string Direction { get; set; }
    }
}
