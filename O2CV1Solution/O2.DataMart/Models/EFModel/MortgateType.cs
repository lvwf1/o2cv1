namespace O2.DataMart.Models.EFModel
{
    using System.ComponentModel.DataAnnotations;

    public partial class MortgateType
    {
        [Key]
        [StringLength(80)]
        public string MortgageType { get; set; }
    }
}
