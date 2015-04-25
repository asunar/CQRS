using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Transactions;
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

    public SqlConnection GetOpenConnection()
    {
      var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
      return new SqlConnection(connectionString);
    }

    private void SaveTask(Task task)
    {
      //save task command to db
      using (var scope = new TransactionScope(TransactionScopeOption.Required))
      {
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

          foreach (var assignee in task.Assignees)
          {
            connection.Execute(@"INSERT INTO Tasks_People (TaskId, PersonId) VALUES (@taskId, @personId)",
              new {taskId = task.TaskId, personId = assignee.PersonId});
      }
    }

        scope.Complete();
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
      var personRepo = new PersonRepository();
      foreach (var personId in dto.Assignees)
      {
        var person = personRepo.Get(personId);
        if (person != null)
        {
          task.Assignees.Add(person);
        }
      }
      return task;
    }

    public static SqlConnection GetOpenConnection()
    {
      var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
      return new SqlConnection(connectionString);
    }

    public void CompleteTask(Guid taskId, CompleteTaskDto dto)
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