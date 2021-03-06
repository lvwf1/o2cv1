﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace O2V1Web.Models
{
    [Table("CountsUsers")]
    public class CountUser
    {
         
        public Guid CountUserId { get; set; }
        public String UserName { get; set; }
        [Required(ErrorMessage = "Please Provide Username", AllowEmptyStrings = false)]
        public String Usermail { get; set; }
        [Required(ErrorMessage = "Please provide password", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        public String UserPassword { get; set; }

    }
}