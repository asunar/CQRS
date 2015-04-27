using System;
using System.Collections.Generic;

namespace TaskFlamingo.Domain
{
  public class Task
  {
    public Task()
    {
      this.TaskId = Guid.NewGuid();
    }
    public Guid TaskId { get; set; }
    public string Name { get; set; }
    public DateTime DueDate { get; set; }
    public string Instructions { get; set; }
    public List<Person> Assignees { get; set; }
    public DateTime? CompletionDate { get; set; }
    public string CompletionComment { get; set; }
    public TaskStatus Status { get; set; }

    public void Publish()
    {
      Status = TaskStatus.Published;
    }

    public void Complete(DateTime CompleteDate, string Comment)
    {
      CompletionDate = CompleteDate;
      CompletionComment = Comment;
      Status = TaskStatus.Completed;
    }
  }

  public class Person
  {
    public Person()
    {
      this.PersonId = Guid.NewGuid();
    }
    public Guid PersonId { get; set; }
    public string Name { get; set; }
    public bool IsSupervisor { get; set; }
  }

  public enum TaskStatus
  {
    Created,
    Completed,
    Canceled,
    Tabled,
    Published
  }
}