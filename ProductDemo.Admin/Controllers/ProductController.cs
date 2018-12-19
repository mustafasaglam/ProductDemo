using Microsoft.Ajax.Utilities;
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
        private readonly IProductImageRepository _productImagerepository;
        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository,IProductImageRepository productImageRepository)
        {
            _productRepository = productRepository;
            _categoryrepository = categoryRepository;
            _productImagerepository = productImageRepository;
        }
        // GET: Product
        public ActionResult Index()
        {
            var productList = _productRepository.GetAll().ToList();
            return View(productList);
        }


        public ActionResult Create()
        {
            ViewBag.Category = _productRepository.GetAll().ToList();//amaçsız yazdık github test
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

        public ActionResult Edit(int? id)
        {
            if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = _productRepository.GetById(id.Value);
            if (product==null)
            {
                return HttpNotFound();
            }
            SetCategoryList(product.ProductId);
            return View(product);          

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product,HttpPostedFileBase ProductImage)
        {
            if (!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }            
            _productRepository.Update(product);
            _productRepository.Save();

            if (ProductImage==null || ProductImage.ContentLength<=0)
            {
                return RedirectToAction("Index");
            }
            var img = new ProductImage()
            {
                ImageName = Path.GetFileName(ProductImage.FileName),
                ContentType = ProductImage.ContentType

            };
            using(var reader=new BinaryReader(ProductImage.InputStream))
            {
                img.Content = reader.ReadBytes(ProductImage.ContentLength);
                img.ProductId = product.ProductId;
            }
            var existingImage = _productRepository.GetById(product.ProductId).ProductImages;
            if (existingImage!=null && existingImage.Count >0)
            {
                existingImage.ForEach(x => _productImagerepository.Delete(x.ProductImageId));
            }
            _productImagerepository.Insert(img);
            _productImagerepository.Save();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = _productRepository.GetById(id.Value);
            return View(product);
        }

        public ActionResult Delete(int? id)
        {
            if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = _productRepository.GetById(id.Value);
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _productRepository.Delete(id);
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