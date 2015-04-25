using System;
using System.Collections.Generic;
using System.Web.Http;
using TaskFlamingo.Domain;

namespace TaskFlamingo.Controllers
{
  using TaskFlamingo.Data;
  using TaskFlamingo.Models;

  public class TaskController : ApiController
  {
    public IEnumerable<DashboardTask> Get(Guid personId)
    {
      return new TaskRetriever().GetDashboardTasks(personId);
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

    [HttpPost]
    public void Publish(Guid id)
    {
      var taskService = new TaskService();
      taskService.PublishTask(id);
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