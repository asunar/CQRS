using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using TaskFlamingo.Domain;

namespace TaskFlamingo.Controllers
{
  using Data;
  using Models;

  public class TaskController : ApiController
  {
    [Route("api/tasks")]
    public IEnumerable<DashboardTask> Get(Guid personId)
    {
      return new TaskRetriever().GetDashboardTasks(personId);
    }

    [Route("api/tasks/create")]
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
      public ScheduleTaskDto()
      {
          this.Assignees = Enumerable.Empty<Guid>().ToList();
      }
    public string Name { get; set; }
    public string Instructions { get; set; }
    public DateTime? DueDate { get; set; }
    public List<Guid> Assignees { get; set; }
  }

}