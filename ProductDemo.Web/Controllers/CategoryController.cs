using ProductDemo.Core.Infrastructure;
using ProductDemo.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductDemo.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        // GET: Category
        public ActionResult Index()
        {
            var products = _categoryRepository.GetAll();
        
            return View(products);
        }
    }
}