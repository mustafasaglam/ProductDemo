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
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDemoContext _context = new ProductDemoContext();
        public int Count()
        {
            return _context.Product.Count();
        }

        public void Delete(int id)
        {
            var product = GetById(id);
            if (product!=null)
            {
                _context.Product.Remove(product);
            }
        }

        public Product Get(Expression<Func<Product, bool>> expression)
        {
            return _context.Product.FirstOrDefault(expression);
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Product.Select(x => x);
        }

        public Product GetById(int id)
        {
            return _context.Product.FirstOrDefault(x=>x.ProductId==id);
        }

        public IQueryable<Product> GetMany(Expression<Func<Product, bool>> expression)
        {
            return _context.Product.Where(expression);
        }

        public void Insert(Product obj)
        {
            _context.Product.Add(obj);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Product obj)
        {
            _context.Product.AddOrUpdate(obj);
        }
    }
}
