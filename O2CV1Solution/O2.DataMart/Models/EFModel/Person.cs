namespace O2.DataMart.Models.EFModel
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Persons")]
    public partial class Person
    {
        public Guid PersonId { get; set; }

        [StringLength(80)]
        public string FullName { get; set; }

        [StringLength(80)]
        public string FirstName { get; set; }

        [StringLength(80)]
        public string LastName { get; set; }

        [StringLength(80)]
        public string FullAddress { get; set; }

        [StringLength(80)]
        public string HouseNumber { get; set; }

        [StringLength(80)]
        public string StreetDirection { get; set; }

        [StringLength(80)]
        public string Street { get; set; }

        [StringLength(80)]
        public string City { get; set; }

        [StringLength(80)]
        public string StateName { get; set; }

        [StringLength(80)]
        public string Zip { get; set; }

        
        [StringLength(80)]
        public string Carrier { get; set; }

        public long? HomeValue { get; set; }

        [StringLength(80)]
        public string HomeValueRange { get; set; }
    }
}
