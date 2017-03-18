using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using O2V1DataAccess;

namespace O2DataAccessTests
{
    [TestClass]
    public class O2V1AdoDataAccessTests
    {
        private string connection;
        private O2V1AdoDataAccess _o2V1AdoRepository;
        private string Query1Test = @" SELECT Mortgages.* FROM Mortgages 
  INNER JOIN BackBone ON BackBone.MortgageId = BackBone.MortgageId 
  INNER JOIN Persons ON BackBone.PersonId = Persons.PersonId 
  INNER JOIN Property ON BackBone.PropertyId = Property.PropertyId
    WHERE  ((dbo.Mortgages.LenderName LIKE '%lend%') AND (dbo.Persons.State = 'AK') AND (dbo.Property.State = 'AK'))";



        [TestInitialize]
        public void TestInitialize()
        {
            connection = ConfigurationManager.ConnectionStrings["O2DataMart"].ConnectionString;
            _o2V1AdoRepository = new O2V1AdoDataAccess(connection);
        }

        [TestMethod]
        public void SchemaRepositoryReturnTableColumnInformationCities()
        {
            var result =   _o2V1AdoRepository.ExecuteQueryCommand(Query1Test);
            Assert.IsTrue(result.QueryRows.Count > 1);
        }

        [TestMethod]
        public void GetCountTest()
        {
            var result = _o2V1AdoRepository.GetCountOfAllRows(Query1Test);
            Assert.IsTrue(result > 1);
        }

    }
}