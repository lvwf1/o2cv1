using System.Data.Entity;
using O2.Common.Utility;
using O2.DataMart.DBScripts;
using O2.DataMart.Models.EFModel;
using Database = O2.Common.Utility.Database;

namespace O2.DataMart.Utility
{
    public static class DatabaseExtensions
    {
        public static void ResetData(this Database database)
        {
            Util.Database.RunDBScript("DataMart-Reset.sql");
        }

        public static void RunDBScript(this Database database, string dbScriptName)
        {
            using ( var db = new O2DataMart() )
            {
                var sql = Util.Database.GetDBScript(dbScriptName);
                db.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, sql);
            }
        }

        public static string GetDBScript(this Database database, string dbScriptName)
        {
            return Util.Assembly.GetTextResource<DBScriptsFolder>(dbScriptName);
        }
    }
}
