using ProductDemo.Core.Infrastructure;
using ProductDemo.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductDemo.Web.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        public HomeController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public ActionResult Index()
        {
            var categoryList = _categoryRepository.GetAll().ToList() ;
            return View(categoryList);
        }

        public ActionResult ProductList1()
        {
            var product = _categoryRepository.GetAll().ToList();

            return View(product);
        }

        
    }
}