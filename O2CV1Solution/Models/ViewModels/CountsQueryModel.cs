using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Antlr.Runtime.Misc;
using O2.Common.Interfaces;
using O2V1Web.Models.O2V1DataMartContext;

namespace O2V1Web.Models.ViewModels
{
    public class CountsQueryModel : ICountsQueryModel
    {
        public CountsQueryModel()
        {
            _tables = new List<TableDropDownItem>();
            _columns = new ListStack<ColumnDropDownItem>();
            CriteriaModel = new CriteriaModel();
        }

        public List<TableDropDownItem> _tables;
        public List<ColumnDropDownItem> _columns;
        [Display(Name = "Datamart Tables")]
        [Required]
        public string SelectedTable { get; set; }

        public IEnumerable<SelectListItem> SelectTables
        {
            get
            {
                var allTables = _tables.Select(f => new SelectListItem
                {
                    Value = f.TableNameValue,
                    Text = f.TableNameDisplay
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
                    Value = f.ColumnNameValue,
                    Text = f.ColumnNameDisplay
                });
                return allColumns;

            }
        }

        public CriteriaModel CriteriaModel { get; set; }

    }
}
