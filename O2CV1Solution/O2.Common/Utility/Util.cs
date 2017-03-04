namespace O2.Common.Utility
{
    public static class Util
    {
        public static Security Security
        {
            get { return security ?? (security = new Security()); }
        }
        private static Security security;

        public static Database Database
        {
            get { return database ?? (database = new Database()); }
        }
        private static Database database;

        public static Query Query
        {
            get { return query ?? (query = new Query()); }
        }
        private static Query query;

        public static Assembly Assembly
        {
            get { return assembly ?? (assembly = new Assembly()); }
        }
        private static Assembly assembly;

        public static Model Model
        {
            get { return model ?? (model = new Model()); }
        }
        private static Model model;
    }
}