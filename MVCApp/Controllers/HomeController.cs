using MVCApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static DataLibrary.BusinessLogic.EmployeeProcessor;

namespace MVCApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
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

        // The View portion of the GET request. This method gets called in _Layout.cshtml on line 24 and pulls vars from EmployeeModel
        public ActionResult SignUp()
        {
            ViewBag.Message = "Employee Sign Up";

            return View();
        }

        // The View portion of the POST method. When I send data from the web form, it'll send to this method
        // [ValidateAntiForgeryToken] links to SignUp.cshtml and its .AntiForgeryToken on ln 12
        // SignUp() is passing in the frontend MVCApp version of EmployeeModel, but it's using CreateEmployee() from EmployeeProcessor.
        // This is also bridging the gap between front- and backend. We can use this CreateEmployee() like so because of the new using
        // statement on ln 7 that's importing a static EmployeeProcessor class. 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(EmployeeModel model)
        {
            // Check that model complies w/ all annotation rules from EmployeeModel; extra security to go w/JS on client side
            // 1:00:00: ModelState is now valid
            // Add 'using static DataLibrary.BusinessLogic.EmployeeProcessor' to just call CreateEmployee() directly
            if (ModelState.IsValid)
            {
                int recordsCreated = CreateEmployee(model.EmployeeId, 
                    model.FirstName, 
                    model.LastName, 
                    model.EmailAddress);
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}