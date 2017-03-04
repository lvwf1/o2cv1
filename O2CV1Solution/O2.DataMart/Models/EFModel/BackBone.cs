namespace O2.DataMart.Models.EFModel
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("BackBone")]
    public partial class BackBone
    {
        [Key]
        public Guid BackBoneId { get; set; }

        public Guid FeedId { get; set; }

        public Guid PropertyId { get; set; }

        public Property Property { get; set; }

        public Guid PersonId { get; set; }

        public Person Person { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PersonTrustPosition { get; set; }

        public Guid MortgageId { get; set; }

        public Mortgage Mortgage { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MortgageTrustPosition { get; set; }
    }
}
