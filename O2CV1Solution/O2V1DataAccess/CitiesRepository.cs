using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.Objects;
using O2V1DataAccess;


namespace O2DataAccess

{
    public class CityRepository : AdoRepository<City>
    {
        public CityRepository(string connectionString)
            : base(connectionString)
        {
        }

        public IEnumerable<City> GetAll()
        {
            // DBAs across the country are having strokes 
            //  over this next command!
            using (var command = new SqlCommand("SELECT * FROM City"))
            {
                return GetRecords(command);
            }
        }

        public City GetById(string id)
        {
            // PARAMETERIZED QUERIES!
            using (var command = new SqlCommand("SELECT * FROM City WHERE City1 = @id"))
            {
                command.Parameters.Add(new ObjectParameter("city1", id));
                return GetRecord(command);
            }
        }

        public override City PopulateRecord(SqlDataReader reader)
        {
            return new City
            {
                City1 = reader.GetString(0),
                County = reader.GetString(1)
            };
        }
    }
}
