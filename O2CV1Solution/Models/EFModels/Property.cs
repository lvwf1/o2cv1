namespace O2V1Web.Models.EFModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Property")]
    public partial class Property
    {
        public Guid PropertyId { get; set; }

        [StringLength(80)]
        public string FederalIPSCode { get; set; }

        [StringLength(80)]
        public string AcessorParcelNumber { get; set; }

       
        public int? APN_Sequence { get; set; }

        [StringLength(80)]
        public string County { get; set; }

        [StringLength(80)]
        public string LandUse { get; set; }

        [StringLength(80)]
        public string Mode { get; set; }

        public bool? OwnerOccupant { get; set; }

        [StringLength(80)]
        public string FullAddress { get; set; }

        [StringLength(80)]
        public string LoanType { get; set; }

        [StringLength(80)]
        public string HouseNumber { get; set; }

        [StringLength(80)]
        public string StreetDirection { get; set; }

        [StringLength(80)]
        public string Street { get; set; }


        [StringLength(80)]
        public string City { get; set; }

        [StringLength(80)]
        public string State { get; set; }

        [StringLength(80)]
        public string Zip { get; set; }

        [StringLength(80)]
        public string FullZip { get; set; }

        [StringLength(80)]
        public string CarrierRouteCode { get; set; }

        [StringLength(80)]
        public string Land_Use { get; set; }

        
        public String SaleDate { get; set; }

       
        public String SaleRecordingDate { get; set; }

        public long? SalePrice { get; set; }

        public double? Loan_Amount { get; set; }

        public long? HomeValue { get; set; }
      
        public int? O2_ID { get; set; }

        [StringLength(80)]
        public string PropertyValueRange { get; set; }
        [StringLength(80)]
        public string Zip5 { get; set; }

        [StringLength(80)]
        public string Zip4 { get; set; }

        public double? LoanToValue { get; set; }
    }
}
