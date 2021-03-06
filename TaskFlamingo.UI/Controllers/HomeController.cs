﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaskFlamingo.UI.Controllers
{
  using TaskFlamingo.UI.Services;

  public class HomeController : Controller
  {
    public ActionResult Index()
    {
      var people = new PersonService().GetAllPeopleAsync().Result;
      return View(people);
    }

    public ActionResult About()
    {
      ViewBag.Message = "Your application description page.";

      return View();
    }

    public ActionResult Contact()
    {
      ViewBag.Message = "Your contact page.";

      return View();
    }
  }
}