namespace O2V1DataAccess.SchemaModels
{
    public class TableMetaData
    {
        public string Name { get; set; }
        public string DbType { get; set; }

        public System.Type Type { get; set; }

        public bool IsPrimeKey { get; set; }
    }
}