using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using Dapper;
using TaskFlamingo.Controllers;

namespace TaskFlamingo.Data
{
  public class PersonRetriever
  {
     
    public SqlConnection GetOpenConnection()
    {
      var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
      return new SqlConnection(connectionString);
    }

    public IEnumerable<PersonListItem> GetAll()
    {
      using (var connection = GetOpenConnection())
      {
        const string sql = @"SELECT PersonId, Name FROM People";
        return connection.Query<PersonListItem>(sql);
      }
    }
  }
}