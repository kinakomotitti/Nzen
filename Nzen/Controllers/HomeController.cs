﻿namespace Nzen.Controllers
{
    #region using
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Nzen.Manager;
    using Nzen.Models;
    #endregion

    public class HomeController : Controller
    {
        #region Common

        public IActionResult Index(HomeModel model)
        {
            return View(model);
        }

        public IActionResult Welcome(HomeModel model)
        {
            return View(model);
        }

        #endregion

        #region user

        public IActionResult Contact(HomeModel model)
        {
                ViewData["Message"] = "Your contact page.";

            return View(model);
        }
        #endregion

        #region presenter

        public IActionResult PresenterPrepare(HomeModel model)
        {
            return View(model);
        }

        public IActionResult PresenterContact(HomeModel model)
        {
            model.Env = ApplicationEnv.Env;
            return View(model);
        }

        #endregion

        #region system error

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #endregion

    }
}
