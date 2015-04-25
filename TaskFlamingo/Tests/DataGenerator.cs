using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Ploeh.AutoFixture;
using TaskFlamingo.Domain;

namespace TaskFlamingo.Tests
{
  [TestFixture]
  public class DataGenerator
  {
    [Test]
    public void generate_random_tasks()
    {
      var fixture = new Fixture();
      var repo = new TaskRepository();
      var personRepo = new PersonRepository();
      var people = personRepo.GetAll();
      var tasks = fixture.Build<Task>()
        .With(t => t.Assignees, GetRandomAssignees(people))
        .CreateMany(50);
      foreach (var task in tasks)
      {
        repo.SaveTask(task);
      }
    }

    private List<Person> GetRandomAssignees(IEnumerable<Person> people)
    {
      var random = new Random(DateTime.Now.Millisecond);
      var numberOfPeople = random.Next(Math.Min(3, people.Count()));
      var assignees = new List<Person>();
      for (var i = 0; i < numberOfPeople; i++)
      {
        assignees.Add(people.ElementAt((numberOfPeople + i)%people.Count()));
      }
      return assignees;
    }
  }
}