namespace O2.DataMart.Models.EFModel
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Mortgage
    {
        public Guid MortgageId { get; set; }

        [StringLength(80)]
        public string LenderName { get; set; }

        public long? LoanAmount { get; set; }

        [StringLength(80)]
        public string LoanAmountRange { get; set; }


        public String OriginationDate { get; set; }


        public String RecordingDate { get; set; }

     
        public int? MortgageTerm { get; set; }

        public int? TrustPosition { get; set; }

        

        [StringLength(80)]
        public string LoanType { get; set; }

        [StringLength(80)]
        public string MortgageType { get; set; }

        public double? InterestRate { get; set; }

        [StringLength(80)]
        public string InterestRateType { get; set; }

        
        public double? LoanToValue { get; set; }

        [StringLength(80)]
        public string LoanToValueRange { get; set; }

        [StringLength(80)]
        public string DueDate { get; set; }

        [StringLength(80)]
        public string DeepType { get; set; }
    }
}
