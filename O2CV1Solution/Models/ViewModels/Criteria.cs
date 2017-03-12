using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace O2V1Web.Models.ViewModels
{
    public class CriteriaModel
    {
        public CriteriaModel()
        {
            _criteria = new List<CriteraDropDownItem>();
            SelectedCriteria = string.Empty;
        }
        public List<CriteraDropDownItem> _criteria;

        [Display(Name = "Criteria")]
        [Required]
        public string SelectedCriteria { get; set; }

        private string CurrentCriteriaColumn { get; set; }
        private string CurrentCriteriaTable { get; set; }
        [Required]
        public string CriteriaCompareValue { get; set; }
 

        public IEnumerable<SelectListItem> SelectCriteria
        {
            get
            {
                var allCriteria = _criteria.Select(f => new SelectListItem
                {
                    Value = f.CriteraNameValue,
                    Text = f.CriteraNameDisplay
                });
                return allCriteria;

            }
        }

    }
}