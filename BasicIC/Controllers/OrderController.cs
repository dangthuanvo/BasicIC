﻿using System.Web.Mvc;

namespace BasicIC.Controllers
{
    public class OrderController : Controller
    {
        public ActionResult Index()
        {
            return View(); // Return the view for the home page
        }

    }
}
