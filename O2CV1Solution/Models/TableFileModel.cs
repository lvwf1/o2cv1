using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace O2V1Web.Models
{
     [Table("TableFiles")]
    public class TableFileModel
    {
        
        public Guid ID{get;set;}
        public String FileName { get; set; }

    }
}