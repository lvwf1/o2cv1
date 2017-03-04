namespace O2.DataMart.Models.EFModel
{
    using System.ComponentModel.DataAnnotations;

    public partial class HomeValueRanges
    {
        [Key]
        [StringLength(80)]
        public string Label { get; set; }

        public long? StartValue { get; set; }

        public long? EndValue { get; set; }
    }
}
