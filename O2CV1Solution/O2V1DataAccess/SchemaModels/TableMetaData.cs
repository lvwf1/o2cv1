namespace O2.DataMart.Models.SchemaModels
{
    public class TableMetaData
    {
        public string Name { get; set; }
        public string DbType { get; set; }

        public System.Type Type { get; set; }

        public bool IsPrimeKey { get; set; }
    }
}