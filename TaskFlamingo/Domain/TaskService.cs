using System;
using System.Configuration;
using System.Data.SqlClient;
using Dapper;
using TaskFlamingo.Controllers;

namespace TaskFlamingo.Domain
{
  public class TaskService
  {
    public void ScheduleTask(ScheduleTaskDto dto)
    {
      var task = CreateTaskFrom(dto);
      SaveTask(task);
    }

    private void SaveTask(Task task)
    {
      //save task command to db
      using (var connection = GetOpenConnection())
      {
        connection.Execute(@"
            INSERT INTO [dbo].[Tasks]
                       ([TaskId]
                       ,[Name]
                       ,[DueDate]
                       ,[Instructions]
                       ,[Status]
                       ,[CompletionDate]
                       ,[CompletionComment])
                 VALUES
                       (@taskId
                       ,@name
                       ,@dueDate
                       ,@instructions
                       ,@status
                       ,@completionDate
                       ,@completionComment)",
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

    private Task CreateTaskFrom(ScheduleTaskDto dto)
    {
      var task = new Task
      {
        TaskId = Guid.NewGuid(),
        Name = dto.Name,
        DueDate = dto.DueDate,
        Status = TaskStatus.Created,
        Instructions = dto.Instructions,
        CompletionDate = null
      };
      return task;
    }

    public static SqlConnection GetOpenConnection()
    {
      var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
      return new SqlConnection(connectionString);
    }

    public void PublishTask(Guid taskId, CompleteTaskDto dto)
    {
      var repo = new TaskRepository();
      var task = repo.Get(taskId);
      task.Status = TaskStatus.Completed;
      task.CompletionDate = dto.CompleteDate;
      task.CompletionComment = dto.CompleteComment;
      repo.Update(task);
    }
  }
}