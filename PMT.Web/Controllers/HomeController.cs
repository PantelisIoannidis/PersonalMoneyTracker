using PMT.Contracts.Repositories;
using PMT.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMT.Web.Controllers
{
    public class HomeController : Controller
    {
        ICategoryRepository categoryRepository;
        public HomeController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
        public ActionResult Index()
        {
            var a = categoryRepository.GetAll();
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
    }
}