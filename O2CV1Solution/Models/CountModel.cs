using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace O2V1Web.Models
{
     [Table("Counts")]
    public class CountModel
    {
       
        public CountModel()
        {

        }
        
        [Key]
        public Guid CountModelID { get; set; }

        [Display(Name="Customer Name")]
        public String Customername { get; set; }

         [Display(Name = "Customer Table")]
        public Guid CustomerTable { get; set; }

        [Display(Name = "Base Template")]
        public Guid BaseTemplate { get; set; }

         [Display(Name = "Assign To")]
        public Guid AssignTo { get; set; }

         [Display(Name = "Access Level")]
        public int? AccessLevel { get; set; }
         public Guid CountOwner { get; set; }
        public ICollection<CountUser> countuser { get; set; }

    }
}