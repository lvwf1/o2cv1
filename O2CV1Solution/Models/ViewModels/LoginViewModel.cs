using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace O2V1Web.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please Provide Username", AllowEmptyStrings = false)]
        public String Username { get; set; }
        [Required(ErrorMessage = "Please Provide Password", AllowEmptyStrings = false)]
        public String Password { get; set; }
    }
}