using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace TaskFlamingo.Domain
{
  public class TaskRepository
  {

    public Task Get(Guid id)
    {
      using (var connection = GetOpenConnection())
      {
        const string sql = @"SELECT TaskId, Name, Instructions, DueDate, Status, CompletionDate, CompletionComment FROM Tasks WHERE TaskId=@id";
        return connection.Query<Task>(sql, new {id}).FirstOrDefault();
      }
    }

    public void Update(Task task)
    {
      //save task command to db
      using (var connection = GetOpenConnection())
      {
        connection.Execute(@"
            UPDATE [dbo].[Tasks]
            SET Name = @name,
                DueDate = @dueDate,
                Instructions = @instructions,
                Status = @status,
                CompletionDate = @completionDate,
                CompletionComment = @completionComment
            WHERE TaskId = @taskId",
          new
          {
            taskId = task.TaskId,
            name = task.Name,
            dueDate = task.DueDate,
            instructions = task.Instructions,
            status = task.Status,
            completionDate = task.CompletionDate,
            completionComment = task.CompletionComment
          });
      }
    }

    public SqlConnection GetOpenConnection()
    {
      var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
      return new SqlConnection(connectionString);
    }

  }
}