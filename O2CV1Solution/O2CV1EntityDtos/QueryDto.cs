namespace O2CV1EntityDtos
{
    public class QueryDto
    {
        public string QueryName { get; set; } 
        public string CreatedBy { get; set; }
        public bool Deleted { get; set; }
        public string Description { get; set; }
        public string QueryId { get; set; }
        public string QuerySql { get; set; }
    }
}
