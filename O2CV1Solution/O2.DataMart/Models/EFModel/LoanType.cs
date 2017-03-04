namespace O2.DataMart.Models.EFModel
{
    using System.ComponentModel.DataAnnotations;

    public partial class LoanType
    {
        [Key]
        [StringLength(80)]
        public string MortgageType { get; set; }
    }
}
