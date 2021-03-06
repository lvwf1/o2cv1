﻿using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using O2DataAccess;

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
            var result = schemaRepository.GetSchemaTableAndColumns("Cities");
            Assert.AreEqual(2,result.metaData.Count);
        }

        [TestMethod]
        public void SchemaRepositoryReturnTableColumnInformationMortgages()
        {
            var result = schemaRepository.GetSchemaTableAndColumns("Mortgages");
            Assert.AreEqual(14, result.metaData.Count);
        }

        [TestMethod]
        public void SchemaRepositoryReturnTableNames()
        {
            var result = schemaRepository.GetSchemaTables();
            Assert.AreEqual(24, result.Count);
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