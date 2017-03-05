using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using O2.Common.Interfaces;
using O2V1Web.Models.O2V1DataMartContext;

namespace O2V1Web.Models.ViewModels
{
    public class CountsQueryModel : ICountsQueryModel
    {
        public List<TableDropDownItem> _tables;
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
    }
}
