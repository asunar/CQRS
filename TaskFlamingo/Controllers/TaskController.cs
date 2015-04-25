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

    // PUT api/values/5
    public void Put(int id, [FromBody]string value)
    {
    }

    // DELETE api/values/5
    public void Delete(int id)
    {
    }
  }

  public class ScheduleTaskDto
  {
    public string Name { get; set; }
    public string Instructions { get; set; }
    public DateTime DueDate { get; set; }
    public List<string> Assignees { get; set; }
  }

}