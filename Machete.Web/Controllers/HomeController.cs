﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Machete.Service;
using System.Web.Helpers;
using System.Globalization;
using Machete.Helpers;

namespace Machete.Web.Controllers
{
    [ElmahHandleError]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
           
           return View();

        }

        public ActionResult About()
        {
            return View();
        }        
    }   
}
