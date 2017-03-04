using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace O2V1Web.Models.ViewModels
{
    public class TableDropDownItem
    {
        public string TableNameDisplay { get; set; }
        public string TableNameValue { get; set; }
    }
}