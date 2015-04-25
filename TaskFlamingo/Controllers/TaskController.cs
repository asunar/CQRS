using System;
using System.Collections.Generic;
using System.Web.Http;
using TaskFlamingo.Domain;

namespace TaskFlamingo.Controllers
{
  public class TaskController : ApiController
  {
    public ScheduleTaskDto Get(string id)
    {
      return new ScheduleTaskDto { Name = "moo" };
    }

    public void Post([FromBody]ScheduleTaskDto dto)
    {
      var taskService = new TaskService();
      taskService.ScheduleTask(dto);
    }

    [HttpPost]
    public void Complete(Guid id, [FromBody] CompleteTaskDto dto)
    {
      var taskService = new TaskService();
      taskService.CompleteTask(id, dto);
    }

  }

  public class CompleteTaskDto
  {
    public DateTime CompleteDate { get; set; }
    public string CompleteComment { get; set; }
  }

  public class ScheduleTaskDto
  {
    public string Name { get; set; }
    public string Instructions { get; set; }
    public DateTime DueDate { get; set; }
    public List<Guid> Assignees { get; set; }
  }

}