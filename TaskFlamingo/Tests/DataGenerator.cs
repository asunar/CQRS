using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Ploeh.AutoFixture;
using TaskFlamingo.Domain;

namespace TaskFlamingo.Tests
{
  using System.Configuration;
  using System.Data.SqlClient;

  using Dapper;

  [TestFixture]
  public class DataGenerator
  {
    private SqlConnection GetOpenConnection()
    {
      var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
      return new SqlConnection(connectionString);
    }

    [Test]
    public void RefreshDatabase()
    {
      this.ClearDatabase();
      this.GeneratePeople();
    }

    private void GeneratePeople()
    {
      var people = new List<Person>()
                     {
                       new Person() { Name = "John Created" },
                       new Person() { Name = "Jack Completed" },
                       new Person() { Name = "Sally Canceled" },
                       new Person() { Name = "Harry Tabled" },
                       new Person() { Name = "Jane Published" },
                       new Person() { Name = "Joe Supervisor", IsSupervisor = true},
                     };

      var personRepo = new PersonRepository();
      var taskRepo = new TaskRepository();
      foreach (var person in people)
      {
        personRepo.SavePerson(person);
        TaskStatus taskStatus;
        Enum.TryParse(person.Name.Split()[1], out taskStatus);

        for (int i = 1; i < 3; i++)
        {
          taskRepo.SaveTask(new Task()
          {
            Name = "Task " + i,
            Assignees = (new[] { person }).ToList(),
            Status = taskStatus,
            CompletionComment = taskStatus == TaskStatus.Completed ? "Completed by " + person.Name : null,
            CompletionDate = taskStatus == TaskStatus.Completed ? DateTime.Today.AddDays(-1 * i) : default(DateTime?),
            DueDate = DateTime.Today.AddMonths(i),
            Instructions = "Instructions " + i,
          });
        }
      }






    }

    private void ClearDatabase()
    {
      var sql = @"
      DELETE FROM dbo.Tasks_People
      DELETE FROM dbo.Tasks
      DELETE FROM dbo.People
";

      using (var connection = this.GetOpenConnection())
      {
        connection.Execute(sql);
      }
    }


  }
}