using System;

namespace O2V1Web.Models.ViewModels
{
    public class CriteriaGridViewModel
    {
        public CriteriaGridViewModel()
        {

        }
        public Int64 QueryId { get; set; }
        public int Sequence { get; set; }
        public string TableColumn { get; set; }
        public string TableName { get; set; }
        public string CompareValue { get; set; }
        public string CompareOperator { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public bool Disabled { get; set; }


    }
}