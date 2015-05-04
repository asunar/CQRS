using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using TaskFlamingo.Controllers;

namespace TaskFlamingo.Domain
{
  public class TaskService
  {
    public void ScheduleTask(ScheduleTaskDto dto)
    {
      var task = CreateTaskFrom(dto);
      var repo = new TaskRepository();
      repo.SaveTask(task);
    }

    public SqlConnection GetOpenConnection()
    {
      var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
      return new SqlConnection(connectionString);
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
        dto.Assignees = dto.Assignees;
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

    public void CompleteTask(Guid taskId, CompleteTaskDto dto)
    {
      var repo = new TaskRepository();
      var task = repo.Get(taskId);
      task.Complete(dto.CompleteDate, dto.CompleteComment);
      repo.Update(task);
    }

    public void PublishTask(Guid taskId)
    {
      var repo = new TaskRepository();
      var task = repo.Get(taskId);
      task.Publish();
      repo.Update(task);
    }
  }
}