namespace O2V1BusinesLayer.QueryModels
{
    public class CriteriaDto
    {
        public CriteriaDto()
        {

        }
        public string QueryTable { get; set; }
        public string QueryJson { get; set; }
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