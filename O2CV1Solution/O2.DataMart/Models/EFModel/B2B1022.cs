namespace O2.DataMart.Models.EFModel
{

    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public partial class B2B1022
    {
        [Key]
        [StringLength(80)]
        public string Label { get; set; }

        public long? StartValue { get; set; }

        public long? EndValue { get; set; }
    }
}
