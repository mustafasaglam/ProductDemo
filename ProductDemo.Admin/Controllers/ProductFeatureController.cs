using ProductDemo.Core.Infrastructure;
using ProductDemo.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ProductDemo.Admin.Controllers
{
    public class ProductFeatureController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductFeatureRepository _productFeatureRepository;
        public ProductFeatureController(IProductRepository productRepository, IProductFeatureRepository productFeatureRepository)
        {
            _productRepository = productRepository;
            _productFeatureRepository = productFeatureRepository;
        }
        // GET: ProductFeature
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = GetCurrentProduct(id.Value);
            var productFeature = product.ProductFeatures;
            ViewBag.SelectedProduct = product;
            return View(productFeature);
        }

        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = _productRepository.GetById(id.Value);
            ViewBag.SelectedProduct = product;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductFeature productFeature, int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            productFeature.ProductId = id.Value;
            _productFeatureRepository.Insert(productFeature);
            _productFeatureRepository.Save();
            return RedirectToAction("Index", new { id = id.Value });
        }

        public ActionResult Edit(int? id, int? productId)
        {
            if (id == null || productId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = _productRepository.GetById(productId.Value);
            ViewBag.SelectedProduct = product;

            var productFeature = _productFeatureRepository.GetById(id.Value);
            if (productFeature==null)
            {
                return HttpNotFound();
            }

            return View(productFeature);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, ProductFeature productFeature)
        {
            if (!ModelState.IsValid)
            {
                return View(productFeature);
            }
            var feature = _productFeatureRepository.GetById(id.Value);

            _productFeatureRepository.Update(productFeature);
            _productFeatureRepository.Save();
            return RedirectToAction("Index", new { id = feature.ProductId });
        }

        public ActionResult Details(int? id,int? productId)
        {
            if (id == null || productId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = _productRepository.GetById(productId.Value);
            ViewBag.SelectedProduct = product;

            var productFeature = _productFeatureRepository.GetById(id.Value);
            if (productFeature == null)
            {
                return HttpNotFound();
            }
            return View(productFeature);

        }

        public ActionResult Delete(int? id)
        {
            if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var productFeature = _productFeatureRepository.GetById(id.Value);
            if (productFeature==null)
            {
                return HttpNotFound();
            }
            return View(productFeature);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var productFeature = _productFeatureRepository.GetById(id);
            _productFeatureRepository.Delete(id);
            _productFeatureRepository.Save();
            return RedirectToAction("Index",new { id=productFeature.ProductId});
        }







        private Product GetCurrentProduct(int id)
        {
            var product = _productRepository.GetById(id);
            return product;
        }
    }
}