namespace O2V1Web.Models.EFModels
{
    using System.ComponentModel.DataAnnotations;

    public partial class MortgateType
    {
        [Key]
        [StringLength(80)]
        public string MortgageType { get; set; }
    }
}
