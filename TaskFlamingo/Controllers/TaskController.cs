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
    public void Publish(Guid id, [FromBody] CompleteTaskDto dto)
    {
      var taskService = new TaskService();
      taskService.PublishTask(id, dto);
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
    public List<string> Assignees { get; set; }
  }

}