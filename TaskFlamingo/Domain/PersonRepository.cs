using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace TaskFlamingo.Domain
{
  public class PersonRepository
  {

    public Person Get(Guid id)
    {
      
      using (var connection = GetOpenConnection())
      {
        const string sql = @"SELECT PersonId, Name, IsSupervisor FROM People WHERE PersonId=@id";
        return connection.Query<Person>(sql, new {id}).FirstOrDefault();
      }
    }

    public IEnumerable<Person> GetAll()
    {
      using (var connection = GetOpenConnection())
      {
        const string sql = @"SELECT PersonId, Name, IsSupervisor FROM People";
        return connection.Query<Person>(sql);
      }
    }

    public SqlConnection GetOpenConnection()
    {
      var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
      return new SqlConnection(connectionString);
    }

  }
}