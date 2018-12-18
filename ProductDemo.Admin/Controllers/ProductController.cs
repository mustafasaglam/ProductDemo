using ProductDemo.Core.Infrastructure;
using ProductDemo.Data.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ProductDemo.Admin.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryrepository;
        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryrepository = categoryRepository;
        }
        // GET: Product
        public ActionResult Index()
        {
            var productList = _productRepository.GetAll().ToList();
            return View(productList);
        }


        public ActionResult Create()
        {
            SetCategoryList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product, HttpPostedFileBase ProductImage)
        {
            if (!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadGateway);
            }
           
            if (ProductImage != null && ProductImage.ContentLength > 0)
            {
                var img = new ProductImage
                {
                    ImageName = Path.GetFileName(ProductImage.FileName),
                    ContentType = ProductImage.ContentType
                };

                using (var reader = new BinaryReader(ProductImage.InputStream))
                {
                    img.Content = reader.ReadBytes(ProductImage.ContentLength);
                }
                product.ProductImages = new List<ProductImage> { img };
            }
            _productRepository.Insert(product);
            _productRepository.Save();
            return RedirectToAction("Index");
        }

        private void SetCategoryList(object category = null)
        {
            var categoryList = _categoryrepository.GetAll().ToList();
            var selecList = new SelectList(categoryList, "CategoryId", "CategoryName", category);
            ViewData.Add("CategoryId", selecList);
        }
    }
}