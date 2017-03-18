using System.Collections.Generic;
using O2V1DataAccess.models;

namespace O2V1Web.Models.ViewModels
{
    public class AdoQueryFieldsViewModel
    {
        public AdoQueryFieldsViewModel()
        {
            ColumnHeaders = new List<string>();
            QueryRows = new List<AdoQueryRow>();
         
        }

       public List<string> ColumnHeaders {get; set; }
       public List<AdoQueryRow> QueryRows { get; set; }
       public long TotalRowsCount { get; set; }
       public int FieldsToShow { get; set; }
    }


}
