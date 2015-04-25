using System;
using System.Collections.Generic;
using System.Linq;

namespace TaskFlamingo.Data
{
  using System.Configuration;
  using System.Data.SqlClient;

  using Dapper;

  using TaskFlamingo.Models;


  public class TaskRetriever
  {
    public static SqlConnection GetOpenConnection()
    {
      var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
      return new SqlConnection(connectionString);
    }

    private bool IsSupervisor(Guid personId)
    {
      using (var connection = GetOpenConnection())
      {
        return connection.Query<bool>(@"SELECT IsSupervisor FROM dbo.People WHERE PersonId = @personId", new { personId }).First();
      }
    }

    public IEnumerable<DashboardTask> GetDashboardTasks(Guid personId)
    {
      var isSupervisor = this.IsSupervisor(personId);

      if (isSupervisor)
      {
        // show all tasks
        using (var connection = GetOpenConnection())
        {
          return connection.Query<DashboardTask>(@"
          SELECT 
            TaskId, 
            Name, 
            DueDate, 
            Status
          FROM
            dbo.vw_DashboardTask
         ");
        }

      }
      // show published tasks assigned to this person only
      using (var connection = GetOpenConnection())
      {
        return connection.Query<DashboardTask>(@"
          SELECT 
            TaskId, 
            Name, 
            DueDate, 
            Status
          FROM
            dbo.vw_DashboardTask
          WHERE
            PersonId = @personId
          AND Status = @published
         ", new { personId, published = (int)Status.Published });
      }      
    } 
  }
}