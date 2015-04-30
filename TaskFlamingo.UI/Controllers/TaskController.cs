namespace TaskFlamingo.UI.Controllers
{
  using System;
  using System.Web.Mvc;

  using TaskFlamingo.UI.Models;
  using TaskFlamingo.UI.Services;

  public class TaskController : Controller
  {
    public ActionResult Index(Guid personId)
    {
      return View(new TaskService().GetMyTasksAsync(personId).Result);
    }
  }
}