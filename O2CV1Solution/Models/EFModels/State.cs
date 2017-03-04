namespace O2V1Web.Models.EFModels
{
    using System.ComponentModel.DataAnnotations;

    public partial class State
    {
        [Key]
        [StringLength(80)]
        public string Name { get; set; }
    }
}
