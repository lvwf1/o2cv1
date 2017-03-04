using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using O2V1Web.Models;

namespace O2.Web.Context
{
    public class CountsContext : DbContext
    {
        public CountsContext()
            : base("name=O2DataMart")
        {
            //Database.SetInitializer<CountsContext>(new DropCreateDatabaseAlways<CountsContext>());
        }
        public virtual DbSet<CountModel> Countmodels { get; set; }
        public virtual DbSet<CountUser> CountUsers { get; set; }

        public virtual DbSet<CosUser> CosUsers { get; set; }


        public virtual DbSet<TableFileModel> tableFiles { get; set; }

        public virtual DbSet<CountTemplateModel> countemplate { get; set; }


        public string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        public string Decrypt(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

    }
    public class MyDataContextInitializer
: DropCreateDatabaseIfModelChanges<CountsContext>
    {//IfModelChanges
        protected override void Seed(CountsContext context)
        {
            var tablefilemodel1 = new TableFileModel { ID = new Guid(), FileName = "Automobile File" };
            context.tableFiles.Add(tablefilemodel1);
            var tablefilemodel2 = new TableFileModel { ID = new Guid(), FileName = "Business Phone File" };
            context.tableFiles.Add(tablefilemodel2);
            var tablefilemodel3 = new TableFileModel { ID = new Guid(), FileName = "True New Movers" };
            context.tableFiles.Add(tablefilemodel3);
            base.Seed(context);
        }
    }
}