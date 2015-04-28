namespace TaskFlamingo.UI.Controllers
{
  using System.Web.Mvc;

  using TaskFlamingo.UI.Services;

  public class TaskController : Controller
  {
    public ActionResult Index()
    {
       return View(new TaskService().GetMyTasksAsync().Result);
    }
  }
}