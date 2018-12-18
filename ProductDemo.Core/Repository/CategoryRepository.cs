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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ProductDemoContext _context=new ProductDemoContext();

        public int Count()
        {
            return _context.Category.Count();
        }

        public void Delete(int id)
        {
            var category = GetById(id);
            if (category!=null)
            {
                _context.Category.Remove(category);
            }
            
        }

        public Category Get(Expression<Func<Category, bool>> expression)
        {
            return _context.Category.FirstOrDefault(expression);
        }

        public IEnumerable<Category> GetAll()
        {
            return _context.Category.Select(x => x);
        }

        public Category GetById(int id)
        {
            return _context.Category.FirstOrDefault(x => x.CategoryId == id);
        }

        public IQueryable<Category> GetMany(Expression<Func<Category, bool>> expression)
        {
            return _context.Category.Where(expression);
        }

        public void Insert(Category obj)
        {
            _context.Category.Add(obj);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Category obj)
        {
            _context.Category.AddOrUpdate(obj); // Migrations ekle ctrl+. ile AddOrUpdate yaz sonra kütüphane ekle
        }
    }
}
