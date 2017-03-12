using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Antlr.Runtime.Misc;
using O2.Common.Interfaces;
using O2V1Web.Models.O2V1DataMartContext;

namespace O2V1Web.Models.ViewModels
{
    public class CountsQueryModel
    {
        public CountsQueryModel()
        {
            _tables = new List<DropDownItem>();
            _columns = new ListStack<DropDownItem>();
            CriteriaModel = new CriteriaModel();
            QueryCriteria = new List<CriteriaGridViewModel>();
        }

        public List<DropDownItem> _tables;
        public List<DropDownItem> _columns;
        [Display(Name = "Datamart Tables")]
        [Required]
        public string SelectedTable { get; set; }

        [Display(Name = "Column")]
        [Required]
        public string SelectedColumn { get; set; }
        [Required]
        public string QueryName { get; set; }
        public string QueryId { get; set; }

        public IEnumerable<SelectListItem> SelectTables
        {
            get
            {
                var allTables = _tables.Select(f => new SelectListItem
                {
                    Value = f.DropDownValue,
                    Text = f.DropDownDisplay
                });
                return allTables;

            }
        }
        public IEnumerable<SelectListItem> SelectColumns
        {
            get
            {
                var allColumns = _columns.Select(f => new SelectListItem
                {
                    Value = f.DropDownValue,
                    Text = f.DropDownDisplay
                });
                return allColumns;

            }
        }

        public CriteriaModel CriteriaModel { get; set; }
        public List<CriteriaGridViewModel> QueryCriteria { get; set; }


    }
}
