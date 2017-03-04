namespace O2V1Web.Models.EFModels
{
    using System.ComponentModel.DataAnnotations;

    public partial class LoanAmountRanges
    {
        [Key]
        [StringLength(80)]
        public string Label { get; set; }

        public long? StartValue { get; set; }

        public long? EndValue { get; set; }
    }
}
