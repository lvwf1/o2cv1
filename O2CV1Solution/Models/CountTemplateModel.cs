using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace O2V1Web.Models
{
       [Table("CountsTemplates")]
    public class CountTemplateModel
    {
      
        [Key]
        public Guid CountTemplateID { get; set; }
        public String Templatename { get; set; }
        public Guid TableFileID { get; set; }
        public Guid CountUserId { get; set; }
        public int? accesstype { get; set; }
    }
}