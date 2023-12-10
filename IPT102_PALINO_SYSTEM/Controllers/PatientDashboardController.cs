using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IPT102_PALINO_SYSTEM.Controllers
{
    public class PatientDashboardController : Controller
    {
        // GET: PatientDashboard
        public ActionResult Index()
        {
            return View();
        }
    }
}