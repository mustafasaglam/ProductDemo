using ProductDemo.Core.Infrastructure;
using ProductDemo.Data.DataContext;
using ProductDemo.Data.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProductDemo.Core.Repository
{
    public class ProductFeatureRepository : IProductFeatureRepository
    {
        private readonly ProductDemoContext _context = new ProductDemoContext();
        public int Count()
        {
            return _context.ProductFeature.Count();
        }

        public void Delete(int id)
        {
            var productfeature = GetById(id);
            if (productfeature!=null)
            {
                _context.ProductFeature.Remove(productfeature);
            }
        }

        public ProductFeature Get(Expression<Func<ProductFeature, bool>> expression)
        {
            return _context.ProductFeature.FirstOrDefault(expression);
        }

        public IEnumerable<ProductFeature> GetAll()
        {
            return _context.ProductFeature.Select(x => x);
        }

        public ProductFeature GetById(int id)
        {
            return _context.ProductFeature.FirstOrDefault(x => x.ProductFeatureId == id);
        }

        public IQueryable<ProductFeature> GetMany(Expression<Func<ProductFeature, bool>> expression)
        {
            return _context.ProductFeature.Where(expression);
        }

        public void Insert(ProductFeature obj)
        {
            _context.ProductFeature.Add(obj);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(ProductFeature obj)
        {
            _context.ProductFeature.AddOrUpdate(obj);
        }
    }
}
