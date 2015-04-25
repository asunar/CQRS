using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Http;
using TaskFlamingo.Domain;

namespace TaskFlamingo.Controllers
{
  using System.Data.SqlClient;

  using Dapper;

  public class TaskController : ApiController
  {
    public ScheduleTaskDto Get(string id)
    {
      return new ScheduleTaskDto { Name = "moo" };
    }

    public void Post([FromBody]ScheduleTaskDto dto)
    {
      var command = new ScheduleTaskCommand(dto);
      var commandHandler = new CommandHandler();
      commandHandler.Handle(command);
    }

    // PUT api/values/5
    public void Put(int id, [FromBody]string value)
    {
    }

    // DELETE api/values/5
    public void Delete(int id)
    {
    }
  }

  public class CommandHandler
  {
    public void Handle(ScheduleTaskCommand command)
    {
      var task = CreateTaskFrom(command.ScheduleTaskDto);
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
                                 new { taskId = task.TaskId, 
                                   name = task.Name, 
                                   dueDate = task.DueDate, 
                                   instructions = task.Instructions,
                                   status = task.Status,
                                   completionDate = task.CompletionDate,
                                   completionComment = task.CompletionComment});
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
  }

  public class ScheduleTaskCommand
  {
    private readonly ScheduleTaskDto _dto;

    public ScheduleTaskCommand(ScheduleTaskDto dto)
    {
      this._dto = dto;
    }

    public ScheduleTaskDto ScheduleTaskDto
    {
      get
      {
        return this._dto;
      }
    }
  }

  public class ScheduleTaskDto
  {
    public string Name { get; set; }
    public string Instructions { get; set; }
    public DateTime DueDate { get; set; }
    public List<string> Assignees { get; set; }
  }

  public class ScheduleTask
  {
    public string Name { get; set; }
    public string Instructions { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime CompletionDate { get; set; }
    public string CompletionComment { get; set; }
  }


}