namespace O2V1Web.Models.EFModels
{
    using System.ComponentModel.DataAnnotations;

    public partial class LoanToValueRanges
    {
        [Key]
        [StringLength(80)]
        public string Label { get; set; }

        public float? StartValue { get; set; }

        public float? EndValue { get; set; }
    }
}
