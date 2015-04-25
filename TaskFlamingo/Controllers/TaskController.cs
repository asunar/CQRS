using System;
using System.Collections.Generic;
using System.Web.Http;

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
      //save task command to db
      using (var connection = GetOpenConnection())
      {
        connection.Execute(@"
            INSERT INTO [dbo].[Tasks]
                       ([TaskId]
                       ,[Name]
                       ,[DueDate]
                       ,[Instructions])
                 VALUES
                       (@taskId
                       ,@name
                       ,@dueDate
                       ,@instructions)", new { taskId = Guid.NewGuid(), name = command.ScheduleTaskDto.Name, dueDate = command.ScheduleTaskDto.DueDate, instructions = command.ScheduleTaskDto.Instructions });
      }

    }

    public static SqlConnection GetOpenConnection()
    {
      return new SqlConnection(@"Server=localhost\sqlserver2014;Database=TaskFlamingo;Integrated Security=True;");
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