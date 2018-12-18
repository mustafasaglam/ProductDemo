using ProductDemo.Admin.ViewModel;
using ProductDemo.Core.Infrastructure;
using ProductDemo.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductDemo.Admin.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;
        private readonly IProductImageRepository _productImageRepository;
        private readonly IProductFeatureRepository _productFeatureRepository;
        public HomeController(ICategoryRepository categoryRepository, IProductRepository productRepository, IProductImageRepository productImageRepository, IProductFeatureRepository  productFeatureRepository)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _productImageRepository = productImageRepository;
            _productFeatureRepository = productFeatureRepository;
        }

        public ActionResult Index()
        {
            var pageModel = new HomePageModel();
            {
                pageModel.CategoryCount = _categoryRepository.Count();
                pageModel.ProductCount = _productRepository.Count();
                pageModel.ProductImageCount = _productImageRepository.Count();
                pageModel.ProductFeatureCount = _productFeatureRepository.Count();
               

            };
            
            return View(pageModel);
        }

    }
}