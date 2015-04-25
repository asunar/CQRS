using System;

namespace TaskFlamingo.Models
{
  public class DashboardTask
  {
    public string Name { get; set; }

    public Guid TaskId { get; set; }

    public DateTime DueDate { get; set; }

    public Status Status { get; set; }
  }

  public enum Status
  {
    Created,
    Completed,
    Canceled,
    Tabled,
    Published
  }
}