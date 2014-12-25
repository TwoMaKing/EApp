using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xpress.Chart.Application;
using Xpress.Chart.ServiceContracts;

namespace Xpress.Life.Controllers
{
    public class HomeController : Controller
    {
        private IPostService postService; //= new PostService();

        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost()]
        public ActionResult PublishPost(int topic, string content) 
        {


            return View();
        }

    }
}
