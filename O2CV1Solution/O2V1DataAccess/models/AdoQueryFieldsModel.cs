using System.Collections.Generic;

namespace O2V1DataAccess.models
{
    public class AdoQueryFieldsModel
    {
        public AdoQueryFieldsModel()
        {
            ColumnHeaders = new List<string>();
            QueryRows = new List<AdoQueryRow>();
            
         
        }

       public List<string> ColumnHeaders {get; set; }
       public List<AdoQueryRow> QueryRows { get; set; }
       
    }

    public class AdoQueryField
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }

    }

    public class AdoQueryRow
    {
        public AdoQueryRow()
        {
            QueryRowFields = new List<AdoQueryField>();
        }
        public List<AdoQueryField> QueryRowFields { get; set; }

    }
}
