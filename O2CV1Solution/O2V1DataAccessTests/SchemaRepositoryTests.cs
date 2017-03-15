using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using O2V1DataAccess;

namespace O2DataAccessTests
{
    [TestClass]
    public class SchemaRepositoryTests
    {
        private string connection;
        private SchemaRepository schemaRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            connection = ConfigurationManager.ConnectionStrings["O2DataMart"].ConnectionString;
            schemaRepository = new SchemaRepository(connection);
        }

        [TestMethod]
        public void SchemaRepositoryReturnTableColumnInformationCities()
        {
            var result = schemaRepository.GetSchemaTableColumns("Cities");
            Assert.AreEqual(2,result.MetaData.Count);
        }

        [TestMethod]
        public void SchemaRepositoryReturnSpecificeTableColumnMetaDataNvarchar()
        {
            var result = schemaRepository.GetSchemaTableColumnMetaData("Cities.County");
            Assert.AreEqual(1, result.MetaData.Count);
            Assert.IsTrue("nvarchar" == result.MetaData[0].DbType);
        }

        [TestMethod]
        public void SchemaRepositoryReturnSpecificeTableColumnMetaDataInt()
        {
            var result = schemaRepository.GetSchemaTableColumnMetaData("Mortgages.MortgageTerm");
            Assert.AreEqual(1, result.MetaData.Count);
            Assert.IsTrue("int" == result.MetaData[0].DbType);
        }


        [TestMethod]
        public void SchemaRepositoryReturnTableColumnInformationMortgages()
        {
            var result = schemaRepository.GetSchemaTableColumns("Mortgages");
            Assert.AreEqual(14, result.MetaData.Count);
        }

        [TestMethod]
        public void SchemaRepositoryReturnTableNames()
        {
            var result = schemaRepository.GetSchemaTables();
            Assert.AreEqual(27, result.Count);
            Assert.IsTrue(result.Contains("Mortgages"));
        }
        [TestMethod]
        public void SchemaRepositoryReturnTableNamesThatAreBackBoneRelated()
        {
            var result = schemaRepository.GetSchemaBackBoneRelatedTables();
            Assert.AreEqual(4, result.Count);
            Assert.IsTrue(result.Contains("Mortgages"));
        }

        [TestMethod]
        public void SchemaRepositoryLinqToSqlReturnTableColumnInformationMortgages()
        {
            var result = schemaRepository.LinqToSqlMapping();
            Assert.AreEqual(23, result.TablesMapped.Count);
        }

    }
}