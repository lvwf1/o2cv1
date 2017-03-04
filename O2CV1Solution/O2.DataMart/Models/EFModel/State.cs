namespace O2.DataMart.Models.EFModel
{
    using System.ComponentModel.DataAnnotations;

    public partial class State
    {
        [Key]
        [StringLength(80)]
        public string Name { get; set; }
    }
}
