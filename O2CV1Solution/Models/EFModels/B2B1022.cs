using System.ComponentModel.DataAnnotations;

namespace O2V1Web.Models.EFModels
{
    public partial class B2B1022
    {
        [Key]
        [StringLength(80)]
        public string Label { get; set; }

        public long? StartValue { get; set; }

        public long? EndValue { get; set; }
    }
}
